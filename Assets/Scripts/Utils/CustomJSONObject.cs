using System;
using System.Collections;
using System.Collections.Generic;

namespace BDUnity.Utils
{
    public class CustomJSONObject
    {
        private object _o;

        public object o
        {
            get { return this._o; }
        }

        public bool isNull
        {
            get { return this._o == null; }
        }

        public bool isDict
        {
            get { return this._o is IDictionary; }
        }

        public bool isList
        {
            get { return this._o is IList; }
        }

        public Dictionary<string, CustomJSONObject> dict
        {
            get { return this._o as Dictionary<string, CustomJSONObject>; }
        }

        public List<CustomJSONObject> list
        {
            get { return this._o as List<CustomJSONObject>; }
        }

        public int count
        {
            get
            {
                IDictionary dictionary = this._o as IDictionary;
                if (dictionary != null)
                {
                    return dictionary.Count;
                }

                IList list = this._o as IList;
                if (list != null)
                {
                    return list.Count;
                }

                return -1;
            }
        }

        public string s
        {
            get { return this._o.ToString(); }
        }

        public bool b
        {
            get { return Convert.ToBoolean(this._o); }
        }

        public long l
        {
            get { return Convert.ToInt64(this._o); }
        }

        public int i
        {
            get { return Convert.ToInt32(this._o); }
        }

        public int si
        {
            get { return Convert.ToInt16(this._o); }
        }

        public double d
        {
            get { return Convert.ToDouble(this._o); }
        }

        public float f
        {
            get { return Convert.ToSingle(this._o); }
        }

        public CustomJSONObject this[int index]
        {
            get { return this.Get(index); }
            set { this.Set(index, value); }
        }

        public CustomJSONObject this[string key]
        {
            get { return this.Get(key); }
            set { this.Set(key, value); }
        }

        public CustomJSONObject(object o)
        {
            this._o = o;
        }

        public void ChangeKeyName(string beforeName, string newName)
        {
            CustomJSONObject mTJSONObject = this.Get(beforeName);
            if (mTJSONObject != (CustomJSONObject) null)
            {
                this.Remove(beforeName);
                this.Add(newName, mTJSONObject);
            }
        }

        public static CustomJSONObject CreateDict()
        {
            return new CustomJSONObject(new Dictionary<string, CustomJSONObject>());
        }

        public static CustomJSONObject CreateList()
        {
            return new CustomJSONObject(new List<CustomJSONObject>());
        }

        public bool TryGet(string key, out CustomJSONObject json)
        {
            json = this.Get(key);
            return json != (CustomJSONObject) null;
        }

        public CustomJSONObject Get(string key)
        {
            Dictionary<string, CustomJSONObject> dict = this.dict;
            CustomJSONObject result = default(CustomJSONObject);
            dict.TryGetValue(key, out result);
            return result;
        }

        public CustomJSONObject Get(int index)
        {
            List<CustomJSONObject> list = this.list;
            if (index >= 0 && index < list.Count)
            {
                return list[index];
            }

            return null;
        }

        public string GetString(string key, string def = "")
        {
            Dictionary<string, CustomJSONObject> dict = this.dict;
            CustomJSONObject mTJSONObject = default(CustomJSONObject);
            dict.TryGetValue(key, out mTJSONObject);
            if (mTJSONObject != (CustomJSONObject) null)
            {
                return mTJSONObject.s;
            }

            return def;
        }

        public float GetFloat(string key, float def = 0f)
        {
            Dictionary<string, CustomJSONObject> dict = this.dict;
            CustomJSONObject mTJSONObject = default(CustomJSONObject);
            dict.TryGetValue(key, out mTJSONObject);
            if (mTJSONObject != (CustomJSONObject) null)
            {
                return mTJSONObject.f;
            }

            return def;
        }

        public double GetDouble(string key, double def = 0.0)
        {
            Dictionary<string, CustomJSONObject> dict = this.dict;
            CustomJSONObject mTJSONObject = default(CustomJSONObject);
            dict.TryGetValue(key, out mTJSONObject);
            if (mTJSONObject != (CustomJSONObject) null)
            {
                return mTJSONObject.d;
            }

            return def;
        }

        public int GetInt(string key, int def = 0)
        {
            Dictionary<string, CustomJSONObject> dict = this.dict;
            CustomJSONObject mTJSONObject = default(CustomJSONObject);
            dict.TryGetValue(key, out mTJSONObject);
            if (mTJSONObject != (CustomJSONObject) null)
            {
                return mTJSONObject.i;
            }

            return def;
        }

        public long GetLong(string key, long def = 0L)
        {
            Dictionary<string, CustomJSONObject> dict = this.dict;
            CustomJSONObject mTJSONObject = default(CustomJSONObject);
            dict.TryGetValue(key, out mTJSONObject);
            if (mTJSONObject != (CustomJSONObject) null)
            {
                return mTJSONObject.l;
            }

            return def;
        }

        public bool GetBool(string key, bool def = false)
        {
            Dictionary<string, CustomJSONObject> dict = this.dict;
            CustomJSONObject mTJSONObject = default(CustomJSONObject);
            dict.TryGetValue(key, out mTJSONObject);
            if (mTJSONObject != (CustomJSONObject) null)
            {
                return mTJSONObject.b;
            }

            return def;
        }

        public void Set(string key, object value)
        {
            this.Add(key, value);
        }

        public void Set(int index, object item)
        {
            List<CustomJSONObject> list = this.list;
            if (index >= 0)
            {
                while (index >= list.Count)
                {
                    list.Add(null);
                }

                CustomJSONObject mTJSONObject = null;
                if (item != null)
                {
                    mTJSONObject = (item as CustomJSONObject);
                    if (mTJSONObject == (CustomJSONObject) null)
                    {
                        mTJSONObject = new CustomJSONObject(item);
                    }
                }

                list[index] = mTJSONObject;
            }
        }

        public void Add(string key, object value)
        {
            CustomJSONObject mTJSONObject = null;
            if (value != null)
            {
                mTJSONObject = (value as CustomJSONObject);
                if (mTJSONObject == (CustomJSONObject) null)
                {
                    mTJSONObject = new CustomJSONObject(value);
                }
            }

            Dictionary<string, CustomJSONObject> dict = this.dict;
            if (dict.ContainsKey(key))
            {
                dict[key] = mTJSONObject;
            }
            else
            {
                dict.Add(key, mTJSONObject);
            }
        }

        public void Add(object item)
        {
            this.Set(this.list.Count, item);
        }

        public void Remove(string key)
        {
            Dictionary<string, CustomJSONObject> dict = this.dict;
            if (dict.ContainsKey(key))
            {
                dict.Remove(key);
            }
        }

        public void RemoveAt(int index)
        {
            List<CustomJSONObject> list = this.list;
            if (index >= 0 && index < list.Count)
            {
                list.RemoveAt(index);
            }
        }

        public CustomJSONObject Clone()
        {
            if (!this.isDict && !this.isList)
            {
                return new CustomJSONObject(this._o);
            }

            return CustomJSON.Deserialize(CustomJSON.Serialize(this._o, true), true);
        }

        public override string ToString()
        {
            if (!this.isDict && !this.isList)
            {
                return this._o.ToString();
            }

            return CustomJSON.Serialize(this._o, true);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return this._o == null;
            }

            CustomJSONObject mTJSONObject = obj as CustomJSONObject;
            if ((object) mTJSONObject == null)
            {
                return false;
            }

            if (this._o == null && mTJSONObject._o == null)
            {
                return true;
            }

            return this._o.Equals(mTJSONObject._o);
        }

        public bool Equals(CustomJSONObject jsonObj)
        {
            if ((object) jsonObj == null)
            {
                return this._o == null;
            }

            if (this._o == null && jsonObj._o == null)
            {
                return true;
            }

            return this._o.Equals(jsonObj._o);
        }

        public override int GetHashCode()
        {
            return this._o.GetHashCode();
        }

        public static bool operator ==(CustomJSONObject a, CustomJSONObject b)
        {
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }

            if ((object) a == null && b.isNull)
            {
                goto IL_0030;
            }

            if ((object) b == null && a.isNull)
            {
                goto IL_0030;
            }

            if ((object) a != null && (object) b != null)
            {
                return a.o == b.o;
            }

            return false;
            IL_0030:
            return true;
        }

        public static bool operator !=(CustomJSONObject a, CustomJSONObject b)
        {
            return !(a == b);
        }
    }
}