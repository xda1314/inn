using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace IvyCore
{
    public static class UI_WindowCodeTemplate
    {
        public static void Generate(string generateFilePath, string behaviourName, string nameSpace)
        {
            var sw = new StreamWriter(generateFilePath, false, new UTF8Encoding(false));
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.AppendLine("/****************************************************************************");
            strBuilder.AppendLine(" * Copyright(C),IVY");
            strBuilder.AppendLine(" * Author: IVY CodeAutoGenerator");
            strBuilder.AppendFormat(" * Device: {0}\n", SystemInfo.deviceName);
            strBuilder.AppendFormat(" * Date: {0}\n", DateTime.Now.ToString());
            strBuilder.AppendLine(" ****************************************************************************/");
            strBuilder.AppendLine();

            strBuilder.AppendLine("using System;");
            strBuilder.AppendLine("using System.Collections.Generic;");
            strBuilder.AppendLine("using UnityEngine;");
            strBuilder.AppendLine("using UnityEngine.UI;");

            strBuilder.AppendLine("namespace " + nameSpace);
            strBuilder.AppendLine("{");
            strBuilder.AppendFormat("\tpublic partial class {0} : UI_WindowBase", behaviourName);
            strBuilder.AppendLine();
            strBuilder.AppendLine("\t{");
            strBuilder.Append("\t\t").AppendLine("public override void Init()");
            strBuilder.Append("\t\t").AppendLine("{");
            strBuilder.Append("\t\t").Append("\t").AppendLine("//please add init code here");
            strBuilder.Append("\t\t").AppendLine("}").AppendLine();
            strBuilder.Append("\t\t").AppendLine("public override void OnShow()");
            strBuilder.Append("\t\t").AppendLine("{");
            strBuilder.Append("\t\t\t").AppendLine("base.OnShow();");
            strBuilder.Append("\t\t").AppendLine("}").AppendLine();
            strBuilder.Append("\t\t").AppendLine("public override void OnHide()");
            strBuilder.Append("\t\t").AppendLine("{");
            strBuilder.Append("\t\t\t").AppendLine("base.OnHide();");
            strBuilder.Append("\t\t").AppendLine("}").AppendLine();
            strBuilder.Append("\t\t").AppendLine("public override void OnClose()");
            strBuilder.Append("\t\t").AppendLine("{");
            strBuilder.Append("\t\t\t").AppendLine("base.OnClose();");
            strBuilder.Append("\t\t").AppendLine("}").AppendLine();
            strBuilder.Append("\t\t").AppendLine("void ShowLog(string content)");
            strBuilder.Append("\t\t").AppendLine("{");
            strBuilder.Append("\t\t\t").AppendFormat("Debug.Log(\"[ {0}:]\" + content);", behaviourName).AppendLine();
            strBuilder.Append("\t\t").AppendLine("}");
            strBuilder.Append("\t}").AppendLine();
            strBuilder.Append("}");

            sw.Write(strBuilder);
            sw.Flush();
            sw.Close();
        }
    }

    public static class UI_WindowComponentCodeTemplate
    {
        public static void Generate(string generateFilePath, string behaviourName, string nameSpace,
            WindowCodeData windowCodeData)
        {
            var sw = new StreamWriter(generateFilePath, false, new UTF8Encoding(false));
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine("/****************************************************************************");
            strBuilder.AppendLine(" * Copyright(C),IVY");
            strBuilder.AppendLine(" * Author: IVY CodeAutoGenerator");
            strBuilder.AppendFormat(" * Device: {0}\n", SystemInfo.deviceName);
            strBuilder.AppendFormat(" * Date: {0}\n", DateTime.Now.ToString());
            strBuilder.AppendLine(" ****************************************************************************/");
            strBuilder.AppendLine();
            strBuilder.AppendLine("namespace " + nameSpace);
            strBuilder.AppendLine("{");
            strBuilder.AppendLine("\tusing UnityEngine;");
            strBuilder.AppendLine("\tusing UnityEngine.UI;");
            strBuilder.AppendLine();
            strBuilder.AppendFormat("\tpublic partial class {0}\n", behaviourName);
            strBuilder.AppendLine("\t{");
            strBuilder.AppendFormat("\t\tpublic const string NAME = \"{0}\";", behaviourName).AppendLine();
            strBuilder.AppendLine();
            
            foreach (var objInfo in windowCodeData.MarkedObjInfos)
            {
                if (!string.IsNullOrEmpty(objInfo.MarkObj.componentDescription_))
                {
                    strBuilder.AppendLine("\t\t/// <summary>");
                    strBuilder.Append("\t\t///").Append(objInfo.MarkObj.componentDescription_.Replace("\n", "\n\t\t///"));
                    strBuilder.AppendLine().AppendLine("\t\t/// </summary>");
                }
                var strUIType = string.Empty;
                if(objInfo.MarkObj.generateStandaloneComponent_)
                {
                    strUIType = objInfo.Name + "SAComponent";
                }
                else
                    strUIType = objInfo.MarkObj.componentName_;
                strBuilder.AppendFormat("\t\t[SerializeField] public {0} {1};\r\n",
                    strUIType, objInfo.Name);
            }

            strBuilder.AppendLine();
            strBuilder.Append("\t\t").AppendLine("public override void ClearUIComponents()");
            strBuilder.Append("\t\t").AppendLine("{");
            foreach (var markInfo in windowCodeData.MarkedObjInfos)
            {
                strBuilder.AppendFormat("\t\t\t{0} = null;\r\n",
                    markInfo.Name);
            }

            strBuilder.Append("\t\t").AppendLine("}");
            strBuilder.AppendLine();
            strBuilder.AppendLine("\t}");
            strBuilder.AppendLine("}");

            sw.Write(strBuilder);
            sw.Flush();
            sw.Close();
        }
    }

    public static class UI_WindowStandaloneControlCodeTemplate
    {
        public static void Generate(string generateFilePath, string behaviourName, string nameSpace)
        {
            behaviourName += "SAComponent";
            var sw = new StreamWriter(generateFilePath, false, new UTF8Encoding(false));
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.AppendLine("/****************************************************************************");
            strBuilder.AppendLine(" * Copyright(C),IVY");
            strBuilder.AppendLine(" * Author: IVY CodeAutoGenerator");
            strBuilder.AppendFormat(" * Device: {0}\n", SystemInfo.deviceName);
            strBuilder.AppendFormat(" * Date: {0}\n", DateTime.Now.ToString());
            strBuilder.AppendLine(" ****************************************************************************/");
            strBuilder.AppendLine();

            strBuilder.AppendLine("using System;");
            strBuilder.AppendLine("using System.Collections.Generic;");
            strBuilder.AppendLine("using UnityEngine;");
            strBuilder.AppendLine("using UnityEngine.UI;");

            strBuilder.AppendLine("namespace " + nameSpace);
            strBuilder.AppendLine("{");
            strBuilder.AppendFormat("\tpublic partial class {0} : MonoBehaviour", behaviourName);
            strBuilder.AppendLine();
            strBuilder.AppendLine("\t{");
            strBuilder.Append("\t\t").AppendLine("public void Start()");
            strBuilder.Append("\t\t").AppendLine("{");
            strBuilder.Append("\t\t").Append("\t").AppendLine("//please add init code here");
            strBuilder.Append("\t\t").AppendLine("}").AppendLine();
            strBuilder.Append("\t\t").AppendLine("void ShowLog(string content)");
            strBuilder.Append("\t\t").AppendLine("{");
            strBuilder.Append("\t\t\t").AppendFormat("Debug.Log(\"[ {0}:]\" + content);", behaviourName).AppendLine();
            strBuilder.Append("\t\t").AppendLine("}");
            strBuilder.Append("\t}").AppendLine();
            strBuilder.Append("}");

            sw.Write(strBuilder);
            sw.Flush();
            sw.Close();
        }
    }

    public static class UI_WindowStandaloneControlComponentCodeTemplate
    {
        public static void Generate(string generateFilePath, string behaviourName, string nameSpace,
            StandaloneComponentData panelCodeData)
        {
            behaviourName += "SAComponent";
            var sw = new StreamWriter(generateFilePath, false, new UTF8Encoding(false));
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine("/****************************************************************************");
            strBuilder.AppendLine(" * Copyright(C),IVY");
            strBuilder.AppendLine(" * Author: IVY CodeAutoGenerator");
            strBuilder.AppendFormat(" * Device: {0}\n", SystemInfo.deviceName);
            strBuilder.AppendFormat(" * Date: {0}\n", DateTime.Now.ToString());
            strBuilder.AppendLine(" ****************************************************************************/");
            strBuilder.AppendLine();
            strBuilder.AppendLine("namespace " + nameSpace);
            strBuilder.AppendLine("{");
            strBuilder.AppendLine("\tusing UnityEngine;");
            strBuilder.AppendLine("\tusing UnityEngine.UI;");
            strBuilder.AppendLine();
            strBuilder.AppendFormat("\tpublic partial class {0}\n", behaviourName);
            strBuilder.AppendLine("\t{");
            strBuilder.AppendFormat("\t\tpublic const string NAME = \"{0}\";", behaviourName).AppendLine();
            strBuilder.AppendLine();

            strBuilder.AppendFormat("\t\t[SerializeField] public {0} {1};\r\n",
                    panelCodeData.ComponentType, "SrcType");
            foreach (var objInfo in panelCodeData.MarkedObjInfos)
            {
                if (!string.IsNullOrEmpty(objInfo.MarkObj.componentDescription_))
                {
                    strBuilder.AppendLine("\t\t/// <summary>");
                    strBuilder.Append("\t\t///").Append(objInfo.MarkObj.componentDescription_.Replace("\n", "\n\t\t///"));
                    strBuilder.AppendLine().AppendLine("\t\t/// </summary>");
                }
                var strUIType = string.Empty;
                if (objInfo.MarkObj.generateStandaloneComponent_)
                {
                    strUIType = objInfo.Name + "SAComponent";
                }
                else
                    strUIType = objInfo.MarkObj.componentName_;

                strBuilder.AppendFormat("\t\t[SerializeField] public {0} {1};\r\n",
                    strUIType, objInfo.Name);
            }

            strBuilder.AppendLine();
            strBuilder.Append("\t\t").AppendLine("public void ClearUIComponents()");
            strBuilder.Append("\t\t").AppendLine("{");
            foreach (var markInfo in panelCodeData.MarkedObjInfos)
            {
                strBuilder.AppendFormat("\t\t\t{0} = null;\r\n",
                    markInfo.Name);
            }

            strBuilder.Append("\t\t").AppendLine("}");
            strBuilder.AppendLine();
            strBuilder.AppendLine("\t}");
            strBuilder.AppendLine("}");

            sw.Write(strBuilder);
            sw.Flush();
            sw.Close();
        }
    }
}
