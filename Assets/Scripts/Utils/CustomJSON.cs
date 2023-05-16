// ILSpyBased#2
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace BDUnity.Utils
{
    public static class CustomJSON
    {
        private sealed class Parser : IDisposable
        {
            private enum TOKEN
            {
                NONE,
                COMMENTS,
                CURLY_OPEN,
                CURLY_CLOSE,
                SQUARED_OPEN,
                SQUARED_CLOSE,
                COLON,
                COMMA,
                STRING,
                NUMBER,
                TRUE,
                FALSE,
                NULL
            }

            private const string WhiteSpace = " \t\n\r";

            private const string WordBreak = " \t\n\r{}[],:\"";

            private const string LineBreak = "\n\r";

            private string jsonString;

            private bool omitError = true;

            private StringReader json;

            private StringBuilder strBuff;

            private StringBuilder strBuff2;

            private char PeekChar
            {
                get
                {
                    return Convert.ToChar(this.json.Peek());
                }
            }

            private char NextChar
            {
                get
                {
                    return Convert.ToChar(this.json.Read());
                }
            }

            private string NextWord
            {
                get
                {
                    this.strBuff.Length = 0;
                    while (" \t\n\r{}[],:\"".IndexOf(this.PeekChar) == -1)
                    {
                        this.strBuff.Append(this.NextChar);
                        if (this.json.Peek() == -1)
                        {
                            break;
                        }
                    }
                    return this.strBuff.ToString();
                }
            }

            private TOKEN NextToken
            {
                get
                {
                    this.EatWhitespace();
                    if (this.json.Peek() == -1)
                    {
                        if (!this.omitError)
                        {
                            throw new Exception("Invalid JSON: " + this.GetParseErrorDetails());
                        }
                        return TOKEN.NONE;
                    }
                    switch (this.PeekChar)
                    {
                        case '#':
                        case '/':
                            return TOKEN.COMMENTS;
                        case '{':
                            return TOKEN.CURLY_OPEN;
                        case '}':
                            this.json.Read();
                            return TOKEN.CURLY_CLOSE;
                        case '[':
                            return TOKEN.SQUARED_OPEN;
                        case ']':
                            this.json.Read();
                            return TOKEN.SQUARED_CLOSE;
                        case ',':
                            this.json.Read();
                            return TOKEN.COMMA;
                        case '"':
                            return TOKEN.STRING;
                        case ':':
                            return TOKEN.COLON;
                        case '-':
                        case '.':
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            return TOKEN.NUMBER;
                        default:
                            switch (this.NextWord)
                            {
                                case "false":
                                    return TOKEN.FALSE;
                                case "true":
                                    return TOKEN.TRUE;
                                case "null":
                                    return TOKEN.NULL;
                                default:
                                    if (!this.omitError)
                                    {
                                        throw new Exception("Invalid JSON: " + this.GetParseErrorDetails());
                                    }
                                    return TOKEN.NONE;
                            }
                    }
                }
            }

            private Parser(string jsonString, bool omitError = true)
            {
                this.jsonString = jsonString;
                this.omitError = omitError;
                this.json = new StringReader(jsonString);
                this.strBuff = new StringBuilder();
                this.strBuff2 = new StringBuilder();
            }

            public static CustomJSONObject Parse(string jsonString, bool omitError = true)
            {
                using (Parser parser = new Parser(jsonString, omitError))
                {
                    return parser.ParseValue();
                }
            }

            public void Dispose()
            {
                this.json.Dispose();
                this.json = null;
                this.strBuff = null;
                this.strBuff2 = null;
            }

            private CustomJSONObject ParseObject()
            {
                Dictionary<string, CustomJSONObject> dictionary = new Dictionary<string, CustomJSONObject>();
                this.json.Read();
                while (true)
                {
                    switch (this.NextToken)
                    {
                        case TOKEN.COMMA:
                            break;
                        case TOKEN.NONE:
                            return null;
                        case TOKEN.COMMENTS:
                            this.EatComments();
                            break;
                        case TOKEN.CURLY_CLOSE:
                            return new CustomJSONObject(dictionary);
                        default:
                        {
                            string text = (string)this.ParseString(false);
                            if (text == null)
                            {
                                if (!this.omitError)
                                {
                                    throw new Exception("Invalid JSON: " + this.GetParseErrorDetails());
                                }
                                return null;
                            }
                            if (this.NextToken != TOKEN.COLON)
                            {
                                if (!this.omitError)
                                {
                                    throw new Exception("Invalid JSON: " + this.GetParseErrorDetails());
                                }
                                return null;
                            }
                            this.json.Read();
                            dictionary[text] = this.ParseValue();
                            break;
                        }
                    }
                }
            }

            private CustomJSONObject ParseArray()
            {
                List<CustomJSONObject> list = new List<CustomJSONObject>();
                this.json.Read();
                bool flag = true;
                while (flag)
                {
                    TOKEN nextToken = this.NextToken;
                    switch (nextToken)
                    {
                        case TOKEN.NONE:
                            return null;
                        case TOKEN.COMMENTS:
                            this.EatComments();
                            break;
                        case TOKEN.SQUARED_CLOSE:
                            flag = false;
                            break;
                        default:
                        {
                            CustomJSONObject item = this.ParseByToken(nextToken);
                            list.Add(item);
                            break;
                        }
                        case TOKEN.COMMA:
                            break;
                    }
                }
                return new CustomJSONObject(list);
            }

            private CustomJSONObject ParseValue()
            {
                TOKEN nextToken = this.NextToken;
                return this.ParseByToken(nextToken);
            }

            private CustomJSONObject ParseByToken(TOKEN token)
            {
                while (true)
                {
                    switch (token)
                    {
                        case TOKEN.COMMENTS:
                            break;
                        case TOKEN.STRING:
                            return (CustomJSONObject)this.ParseString(true);
                        case TOKEN.NUMBER:
                            return this.ParseNumber();
                        case TOKEN.CURLY_OPEN:
                            return this.ParseObject();
                        case TOKEN.SQUARED_OPEN:
                            return this.ParseArray();
                        case TOKEN.TRUE:
                            return new CustomJSONObject(true);
                        case TOKEN.FALSE:
                            return new CustomJSONObject(false);
                        case TOKEN.NULL:
                            return null;
                        default:
                            return null;
                    }
                    this.EatComments();
                    token = this.NextToken;
                }
            }

            private object ParseString(bool isValue = true)
            {
                this.strBuff.Length = 0;
                this.json.Read();
                bool flag = true;
                while (flag)
                {
                    if (this.json.Peek() != -1)
                    {
                        char nextChar = this.NextChar;
                        switch (nextChar)
                        {
                            case '"':
                                flag = false;
                                break;
                            case '\\':
                                if (this.json.Peek() == -1)
                                {
                                    flag = false;
                                }
                                else
                                {
                                    nextChar = this.NextChar;
                                    switch (nextChar)
                                    {
                                        case '"':
                                        case '/':
                                        case '\\':
                                            this.strBuff.Append(nextChar);
                                            break;
                                        case 'b':
                                            this.strBuff.Append('\b');
                                            break;
                                        case 'f':
                                            this.strBuff.Append('\f');
                                            break;
                                        case 'n':
                                            this.strBuff.Append('\n');
                                            break;
                                        case 'r':
                                            this.strBuff.Append('\r');
                                            break;
                                        case 't':
                                            this.strBuff.Append('\t');
                                            break;
                                        case 'u':
                                            this.strBuff2.Length = 0;
                                            for (int i = 0; i < 4; i++)
                                            {
                                                this.strBuff2.Append(this.NextChar);
                                            }
                                            this.strBuff.Append((char)(ushort)Convert.ToInt32(this.strBuff2.ToString(), 16));
                                            break;
                                    }
                                }
                                break;
                            default:
                                this.strBuff.Append(nextChar);
                                break;
                        }
                        continue;
                    }
                    flag = false;
                    break;
                }
                if (isValue)
                {
                    return new CustomJSONObject(this.strBuff.ToString());
                }
                return this.strBuff.ToString();
            }

            private CustomJSONObject ParseNumber()
            {
                string nextWord = this.NextWord;
                if (nextWord.IndexOf('.') == -1)
                {
                    return new CustomJSONObject(long.Parse(nextWord, CustomJSON.numberFormat));
                }
                return new CustomJSONObject(double.Parse(nextWord, CustomJSON.numberFormat));
            }

            private void EatWhitespace()
            {
                while (" \t\n\r".IndexOf(this.PeekChar) != -1)
                {
                    this.json.Read();
                    if (this.json.Peek() == -1)
                    {
                        break;
                    }
                }
            }

            private void EatComments()
            {
                while ("\n\r".IndexOf(this.PeekChar) == -1)
                {
                    this.json.Read();
                    if (this.json.Peek() == -1)
                    {
                        break;
                    }
                }
            }

            private string GetParseErrorDetails()
            {
                int length = this.json.ReadToEnd().Length;
                int num = this.jsonString.Length - length;
                int num2 = 1;
                int num3 = 0;
                for (int i = 0; i < num; i++)
                {
                    if (this.jsonString[i] == '\r')
                    {
                        num2++;
                        if (this.jsonString[i + 1] == '\n')
                        {
                            i++;
                        }
                        num3 = i;
                    }
                    else if (this.jsonString[i] == '\n')
                    {
                        num2++;
                        num3 = i;
                    }
                }
                int num4 = num - num3;
                int num5 = num3 + Math.Max(1, num4 - 10);
                int num6 = Math.Min(num + 10, this.jsonString.Length);
                int num7 = num;
                while (num7 < num6)
                {
                    if (this.jsonString[num7] != '\n' && this.jsonString[num7] != '\r')
                    {
                        num7++;
                        continue;
                    }
                    num6 = num7;
                    break;
                }
                string text = this.jsonString.Substring(num5, num6 - num5);
                return "(" + num2 + ":" + num4 + ") " + text;
            }
        }

        private sealed class Serializer
        {
            private bool toUnicodeString = true;

            private StringBuilder builder;

            private Serializer(bool toUnicodeString = true)
            {
                this.toUnicodeString = toUnicodeString;
                this.builder = new StringBuilder();
            }

            public static string Serialize(object obj, bool toUnicodeString = true)
            {
                Serializer serializer = new Serializer(toUnicodeString);
                serializer.SerializeValue(obj);
                return serializer.builder.ToString();
            }

            private void SerializeValue(object value)
            {
                CustomJSONObject mTJSONObject = value as CustomJSONObject;
                if (mTJSONObject != (CustomJSONObject)null)
                {
                    value = mTJSONObject.o;
                }
                string str;
                IList array;
                IDictionary obj;
                if (value == null)
                {
                    this.builder.Append("null");
                }
                else if ((str = (value as string)) != null)
                {
                    this.SerializeString(str);
                }
                else if (value is bool)
                {
                    this.builder.Append(value.ToString().ToLower());
                }
                else if ((array = (value as IList)) != null)
                {
                    this.SerializeArray(array);
                }
                else if ((obj = (value as IDictionary)) != null)
                {
                    this.SerializeObject(obj);
                }
                else if (value is char)
                {
                    this.SerializeString(value.ToString());
                }
                else
                {
                    this.SerializeOther(value);
                }
            }

            private void SerializeObject(IDictionary obj)
            {
                bool flag = true;
                this.builder.Append('{');
                IEnumerator enumerator = obj.Keys.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        object current = enumerator.Current;
                        if (!flag)
                        {
                            this.builder.Append(',');
                        }
                        this.SerializeString(current.ToString());
                        this.builder.Append(':');
                        this.SerializeValue(obj[current]);
                        flag = false;
                    }
                }
                finally
                {
                    IDisposable disposable;
                    if ((disposable = (enumerator as IDisposable)) != null)
                    {
                        disposable.Dispose();
                    }
                }
                this.builder.Append('}');
            }

            private void SerializeArray(IList array)
            {
                this.builder.Append('[');
                bool flag = true;
                IEnumerator enumerator = array.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        object current = enumerator.Current;
                        if (!flag)
                        {
                            this.builder.Append(',');
                        }
                        this.SerializeValue(current);
                        flag = false;
                    }
                }
                finally
                {
                    IDisposable disposable;
                    if ((disposable = (enumerator as IDisposable)) != null)
                    {
                        disposable.Dispose();
                    }
                }
                this.builder.Append(']');
            }

            private void SerializeString(string str)
            {
                this.builder.Append('"');
                char[] array = str.ToCharArray();
                char[] array2 = array;
                foreach (char c in array2)
                {
                    switch (c)
                    {
                        case '"':
                            this.builder.Append("\\\"");
                            break;
                        case '\\':
                            this.builder.Append("\\\\");
                            break;
                        case '\b':
                            this.builder.Append("\\b");
                            break;
                        case '\f':
                            this.builder.Append("\\f");
                            break;
                        case '\n':
                            this.builder.Append("\\n");
                            break;
                        case '\r':
                            this.builder.Append("\\r");
                            break;
                        case '\t':
                            this.builder.Append("\\t");
                            break;
                        default:
                            if (this.toUnicodeString)
                            {
                                int num = Convert.ToInt32(c);
                                if (num >= 32 && num <= 126)
                                {
                                    this.builder.Append(c);
                                }
                                else
                                {
                                    this.builder.Append("\\u" + Convert.ToString(num, 16).PadLeft(4, '0'));
                                }
                            }
                            else
                            {
                                this.builder.Append(c);
                            }
                            break;
                    }
                }
                this.builder.Append('"');
            }

            private void SerializeOther(object value)
            {
                if (value is float || value is int || value is uint || value is long || value is double || value is sbyte || value is byte || value is short || value is ushort || value is ulong || value is decimal)
                {
                    this.builder.Append(value.ToString());
                }
                else
                {
                    this.SerializeString(value.ToString());
                }
            }
        }

        private static NumberFormatInfo numberFormat = new CultureInfo("en-US").NumberFormat;

        public static CustomJSONObject Deserialize(string jsonString, bool omitError = true)
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                if (!omitError)
                {
                    throw new Exception("Invalid JSON: json string is null or empty.");
                }
                return null;
            }
            return Parser.Parse(jsonString, omitError);
        }

        public static string Serialize(object obj, bool toUnicodeString = true)
        {
            return Serializer.Serialize(obj, toUnicodeString);
        }
    }
}


