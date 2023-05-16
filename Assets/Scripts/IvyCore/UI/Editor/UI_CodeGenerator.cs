using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace IvyCore
{
    public class WindowCodeData
    {
        public string WindowName;
        public Dictionary<string, GameObject> CheckNameConflictDic = new Dictionary<string, GameObject>();
        public readonly List<MarkedObjInfo> MarkedObjInfos = new List<MarkedObjInfo>();

        public List<StandaloneComponentData> StandaloneComponentDatasList = new List<StandaloneComponentData>();
    }

    public class StandaloneComponentData : WindowCodeData
    {
        public string ComponentName;
        public string ComponentType;
    }


    public class MarkedObjInfo
    {
        public string Name;
        public UI_Base MarkObj;
    }

    public class UI_CodeGenerator
    {
        [MenuItem("Assets/@IVY 生成界面代码",true)]
        public static bool IsCreateUICodeVaild()
        {
            var objs = Selection.GetFiltered(typeof(GameObject), SelectionMode.Assets | SelectionMode.TopLevel);
            foreach(GameObject obj in objs)
            {
                var windowComponent = obj.GetComponent<UI_Window>();
                if(windowComponent==null)
                {
                    return false;
                }
            }
            if(objs.Length>0)
            {
                return true;
            }
            return false;
        }
        [MenuItem("Assets/@IVY 生成界面代码",false)]
        public static void CreateUICode()
        {
            var objs = Selection.GetFiltered(typeof(GameObject), SelectionMode.Assets | SelectionMode.TopLevel);
            var displayProgress = objs.Length >= 1;
            if (displayProgress) EditorUtility.DisplayProgressBar("", "创建 UIPrefab 代码中...", 0);
            for (var i = 0; i < objs.Length; i++)
            {
                instance_.CreateCode(objs[i] as GameObject, AssetDatabase.GetAssetPath(objs[i]));
                if (displayProgress)
                    EditorUtility.DisplayProgressBar("", "创建 UIPrefab 代码中...", (float)(i + 1) / objs.Length);
            }

            AssetDatabase.Refresh();
            if (displayProgress) EditorUtility.ClearProgressBar();
        }

        

        private void CreateCode(GameObject obj, string uiPrefabPath)
        {
            if (obj!=null)
            {
                var prefabType = PrefabUtility.GetPrefabType(obj);
                if (PrefabType.Prefab != prefabType)
                {
                    return;
                }

                var clone = PrefabUtility.InstantiatePrefab(obj) as GameObject;
                if (null == clone)
                {
                    return;
                }

                windowCodeData_ = new WindowCodeData();
                windowCodeData_.WindowName = clone.name.Replace("(clone)", string.Empty);
                bool willCreateCode = true;
                Dictionary<string, GameObject> checkNameConflictInWindowAndStandaloneControlDic = new Dictionary<string, GameObject>();
                checkNameConflictInWindowAndStandaloneControlDic.Add(windowCodeData_.WindowName, clone.gameObject);
                //取得所有标记控件信息
                GetAllMarkObjectInfo(clone.transform , ref willCreateCode ,checkNameConflictInWindowAndStandaloneControlDic);
                if(willCreateCode)
                {
                    ////创建相关代码
                    CreateUIWindowCode(obj, uiPrefabPath);
                    ////记录信息
                    AddSerializeUIPrefab(obj, windowCodeData_);
                }
                else
                {
                    Debug.LogErrorFormat("窗体 {0} 代码生成失败,详见Log信息", windowCodeData_.WindowName);
                }

                GameObject.DestroyImmediate(clone);
            }
        }

        private void GetAllMarkObjectInfoInStandaloneComponent(Transform curTrans, StandaloneComponentData scd, ref bool willCreateCode, Dictionary<string, GameObject> checkNameConflictInWindowAndStandaloneControlDic)
        {
            foreach (Transform childTrans in curTrans)
            {
                var uiBase = childTrans.GetComponent<UI_Base>();
                bool serchChildren = true;
                if (null != uiBase)
                {
                    if (uiBase.generateMember_)
                    {
                        if (!StringTools.IsStringOnlyContainEnglishNumber(uiBase.transform.name))
                        {
                            Debug.LogErrorFormat("控件名称不符合规范,仅可以使用英文,数字及下划线,将导致绑定无效=> 界面[{0}] 控件名称[{1}] 控件路径[{2}]", windowCodeData_.WindowName, uiBase.transform.name, PathToParent(uiBase.transform, windowCodeData_.WindowName));
                            willCreateCode = false;
                        }
                        else if (string.IsNullOrEmpty(uiBase.componentName_))
                        {
                            Debug.LogErrorFormat("控件已添加生成变量标识,未指定变量类型=> 界面[{0}] 控件名称[{1}] 控件路径[{2}]", windowCodeData_.WindowName, uiBase.transform.name, PathToParent(uiBase.transform, windowCodeData_.WindowName));
                            willCreateCode = false;
                        }
                        else
                        {
                            if (uiBase.generateStandaloneComponent_)
                            {
                                serchChildren = false;
                                StandaloneComponentData newScd = new StandaloneComponentData();
                                newScd.WindowName = windowCodeData_.WindowName;
                                newScd.ComponentName = uiBase.gameObject.name;
                                newScd.ComponentType = uiBase.componentName_;
                                GetAllMarkObjectInfoInStandaloneComponent(uiBase.transform, newScd, ref willCreateCode,checkNameConflictInWindowAndStandaloneControlDic);
                                if (!checkNameConflictInWindowAndStandaloneControlDic.ContainsKey(newScd.ComponentName))
                                {
                                    checkNameConflictInWindowAndStandaloneControlDic.Add(newScd.ComponentName, uiBase.gameObject);
                                }
                                else
                                {
                                    Debug.LogErrorFormat("独立控件名称冲突 => 界面[{0}] 独立控件名称[{1}] 控件路径[{2}]", windowCodeData_.WindowName, uiBase.transform.name, PathToParent(uiBase.transform, windowCodeData_.WindowName));
                                    willCreateCode = false;
                                }
                                scd.StandaloneComponentDatasList.Add(newScd);
                            }
                            if (!scd.CheckNameConflictDic.ContainsKey(uiBase.transform.name))
                            {
                                scd.CheckNameConflictDic.Add(uiBase.transform.name,uiBase.gameObject);
                                scd.MarkedObjInfos.Add(new MarkedObjInfo
                                {
                                    Name = uiBase.transform.name,
                                    MarkObj = uiBase
                                });
                            }
                            else
                            {
                                Debug.LogErrorFormat("控件名称冲突=> 界面[{0}] 控件名称[{1}] 冲突控件路径[{2}] 已添加控件路径[{3}]", windowCodeData_.WindowName, uiBase.transform.name, PathToParent(uiBase.transform, windowCodeData_.WindowName), PathToParent(windowCodeData_.CheckNameConflictDic[uiBase.transform.name].transform, windowCodeData_.WindowName));
                                willCreateCode = false;
                            }
                        }
                    }
                }
                if (serchChildren)
                    GetAllMarkObjectInfoInStandaloneComponent(childTrans,scd, ref willCreateCode, checkNameConflictInWindowAndStandaloneControlDic);
            }
        }

        private void GetAllMarkObjectInfo(Transform curTrans,ref bool willCreateCode,Dictionary<string, GameObject> checkNameConflictInWindowAndStandaloneControlDic)
        {
            foreach (Transform childTrans in curTrans)
            {
                var uiBase = childTrans.GetComponent<UI_Base>();
                bool serchChildren = true;
                if(null != uiBase)
                {
                    if(uiBase.generateMember_)
                    {
                        if(!StringTools.IsStringOnlyContainEnglishNumber(uiBase.transform.name))
                        {
                            Debug.LogErrorFormat("控件名称不符合规范,仅可以使用英文,数字及下划线,将导致绑定无效=> 界面[{0}] 控件名称[{1}] 控件路径[{2}]", windowCodeData_.WindowName, uiBase.transform.name, PathToParent(uiBase.transform, windowCodeData_.WindowName));
                            willCreateCode = false;
                        }
                        else if(string.IsNullOrEmpty( uiBase.componentName_))
                        {
                            Debug.LogErrorFormat("控件已添加生成变量标识,未指定变量类型=> 界面[{0}] 控件名称[{1}] 控件路径[{2}]" , windowCodeData_.WindowName, uiBase.transform.name, PathToParent(uiBase.transform, windowCodeData_.WindowName));
                            willCreateCode = false;
                        }
                        else
                        {
                            if(uiBase.generateStandaloneComponent_)
                            {
                                serchChildren = false;
                                StandaloneComponentData scd = new StandaloneComponentData();
                                scd.WindowName = windowCodeData_.WindowName;
                                scd.ComponentName = uiBase.gameObject.name;
                                scd.ComponentType = uiBase.componentName_;
                                if(!checkNameConflictInWindowAndStandaloneControlDic.ContainsKey(scd.ComponentName))
                                {
                                    checkNameConflictInWindowAndStandaloneControlDic.Add(scd.ComponentName, uiBase.gameObject);
                                }
                                else
                                {
                                    Debug.LogErrorFormat("独立控件名称冲突 => 界面[{0}] 独立控件名称[{1}] 控件路径[{2}]", windowCodeData_.WindowName, uiBase.transform.name, PathToParent(uiBase.transform, windowCodeData_.WindowName));
                                    willCreateCode = false;
                                }
                                GetAllMarkObjectInfoInStandaloneComponent(uiBase.transform,scd, ref willCreateCode, checkNameConflictInWindowAndStandaloneControlDic);
                                windowCodeData_.StandaloneComponentDatasList.Add(scd);
                            }
                            if(!windowCodeData_.CheckNameConflictDic.ContainsKey(uiBase.transform.name))
                            {
                                windowCodeData_.CheckNameConflictDic.Add(uiBase.transform.name, uiBase.gameObject);
                                windowCodeData_.MarkedObjInfos.Add(new MarkedObjInfo
                                {
                                    Name = uiBase.transform.name,
                                    MarkObj = uiBase
                                });
                            }
                            else
                            {
                                Debug.LogErrorFormat("控件名称冲突=> 界面[{0}] 控件名称[{1}] 冲突控件路径[{2}] 已添加控件路径[{3}]", windowCodeData_.WindowName, uiBase.transform.name, PathToParent(uiBase.transform,windowCodeData_.WindowName), PathToParent(windowCodeData_.CheckNameConflictDic[uiBase.transform.name].transform, windowCodeData_.WindowName));
                                willCreateCode = false;
                            }
                        }
                    }
                }
                if(serchChildren)
                    GetAllMarkObjectInfo(childTrans, ref willCreateCode, checkNameConflictInWindowAndStandaloneControlDic);
            }
        }

        private static void AddSerializeUIPrefab(GameObject uiPrefab, WindowCodeData panelData)
        {
            var prefabPath = AssetDatabase.GetAssetPath(uiPrefab);
            if (string.IsNullOrEmpty(prefabPath))
                return;
            var pathStr = EditorPrefs.GetString("AutoGenUIPrefabPath");
            if (string.IsNullOrEmpty(pathStr))
            {
                pathStr = prefabPath;
            }
            else
            {
                pathStr += ";" + prefabPath;
            }

            EditorPrefs.SetString("AutoGenUIPrefabPath", pathStr);
        }

        private void CreateUIWindowCode(GameObject uiPrefab, string uiPrefabPath)
        {
            if (null == uiPrefab)
                return;

            var behaviourName = uiPrefab.name;

            var strFilePath = string.Empty;

            strFilePath = "Assets/Scripts/UI/Window/"+ behaviourName;
            strFilePath.CreateDirIfNotExists();

            string mainWindowCodeCreatePath = strFilePath + "/" + behaviourName + ".cs";
            if (File.Exists(mainWindowCodeCreatePath) == false)
            {
                UI_WindowCodeTemplate.Generate(mainWindowCodeCreatePath, behaviourName, "IvyCore");
            }
            foreach(var wcs in windowCodeData_.StandaloneComponentDatasList)
            {
                CreateUIWindowStandaloneControlCode(strFilePath,wcs);
            }

            CreateUIWindowComponentsCode(behaviourName, strFilePath);

            foreach(var wcs in windowCodeData_.StandaloneComponentDatasList)
            {
                CreateUIWindowStandaloneComponentControlCode(wcs.ComponentName, strFilePath,wcs);
            }
            Debug.Log(">>>>>>>创建 UIPrefab: " + behaviourName+" 代码成功");
        }

        void CreateUIWindowStandaloneControlCode(string createPath,StandaloneComponentData wcd)
        {
            var path = createPath +"/"+ wcd.ComponentName + "SAComponent.cs";
            if(File.Exists(path) == false)
            {
                UI_WindowStandaloneControlCodeTemplate.Generate(path, wcd.ComponentName, "IvyCore");
            }
            foreach(var wcs in wcd.StandaloneComponentDatasList)
            {
                CreateUIWindowStandaloneControlCode(createPath, wcs);
            }
        }

        void CreateUIWindowStandaloneComponentControlCode(string componentName, string uiUIPanelfilePath,StandaloneComponentData data)
        {
            var dir = uiUIPanelfilePath.Replace(componentName + ".cs", "");
            var generateFilePath = dir + "/" + componentName + "SAComponents.cs";

            if (File.Exists(generateFilePath))
            {
                File.Delete(generateFilePath);
            }
            UI_WindowStandaloneControlComponentCodeTemplate.Generate(generateFilePath, componentName, "IvyCore", data);
            foreach(var newData in data.StandaloneComponentDatasList)
            {
                CreateUIWindowStandaloneComponentControlCode(newData.ComponentName, uiUIPanelfilePath, newData);
            }
        }

        private void CreateUIWindowComponentsCode(string behaviourName, string uiUIPanelfilePath)
        {
            var dir = uiUIPanelfilePath.Replace(behaviourName + ".cs", "");
            var generateFilePath = dir + "/"+behaviourName + "Components.cs";

            if(File.Exists(generateFilePath))
            {
                File.Delete(generateFilePath);
            }
            UI_WindowComponentCodeTemplate.Generate(generateFilePath, behaviourName, "IvyCore", windowCodeData_);
        }

        public static string GetLastDirName(string absOrAssetsPath)
        {
            var name = absOrAssetsPath.Replace("\\", "/");
            var dirs = name.Split('/');

            return dirs[dirs.Length - 2];
        }

        private static string PathToParent(Transform trans, string parentName)
        {
            var retValue = new System.Text.StringBuilder(trans.name);
            var curV = trans.name;
            while (trans.parent != null)
            {
                if (trans.parent.name.Equals(parentName))
                {
                    break;
                }
                retValue.Remove(0, retValue.Length);
                retValue.Append(trans.parent.name).Append("/").Append(curV);
                curV = retValue.ToString();
                trans = trans.parent;
            }
            return curV;
        }
        [MenuItem("Wsq/Test")]
        public static void test()
        {
            var pathStr = "Assets/Prefab/PopUp_LevelWin.prefab";
            if (string.IsNullOrEmpty(pathStr))
                return;

            var assembly = ReflectionExtension.GetAssemblyCSharp();
            var assemblyFirstPass = ReflectionExtension.GetAssemblyCSharpFirstPass();

            var paths = pathStr.Split(new[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
            var displayProgress = paths.Length > 0;
            if (displayProgress) EditorUtility.DisplayProgressBar("", "序列化 UIPrefab...", 0);

            for (var i = 0; i < paths.Length; i++)
            {
                var uiPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(paths[i]);
                AttachSerializeObj(uiPrefab, uiPrefab.name, assembly, assemblyFirstPass);

                // uibehaviour
                if (displayProgress)
                    EditorUtility.DisplayProgressBar("", "序列化 UIPrefab..." + uiPrefab.name, (float)(i + 1) / paths.Length);
                Debug.Log(">>>>>>>序列化成功 UIPrefab: " + uiPrefab.name);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            if (displayProgress) EditorUtility.ClearProgressBar();


        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void SerializeUIPrefab()
        {
            var pathStr = EditorPrefs.GetString("AutoGenUIPrefabPath");
            if (string.IsNullOrEmpty(pathStr))
                return;

            EditorPrefs.DeleteKey("AutoGenUIPrefabPath");
            Debug.Log(">>>>>>>序列化UIPrefab: " + pathStr);

            var assembly = ReflectionExtension.GetAssemblyCSharp();
            var assemblyFirstPass = ReflectionExtension.GetAssemblyCSharpFirstPass();

            var paths = pathStr.Split(new[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
            var displayProgress = paths.Length > 0;
            if (displayProgress) EditorUtility.DisplayProgressBar("", "序列化 UIPrefab...", 0);

            for (var i = 0; i < paths.Length; i++)
            {
                var uiPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(paths[i]);
                AttachSerializeObj(uiPrefab, uiPrefab.name, assembly, assemblyFirstPass);

                // uibehaviour
                if (displayProgress)
                    EditorUtility.DisplayProgressBar("", "序列化 UIPrefab..." + uiPrefab.name, (float)(i + 1) / paths.Length);
                Debug.Log(">>>>>>>序列化成功 UIPrefab: " + uiPrefab.name);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            if (displayProgress) EditorUtility.ClearProgressBar();

        }


        private static void AttachSerializeObj(GameObject obj, string behaviourName, System.Reflection.Assembly assembly, System.Reflection.Assembly assemblyFirstPass,
            List<UI_Base> processedMarks = null, SerializedObject serializedObject=null)
        {
            if (null == processedMarks)
            {
                processedMarks = new List<UI_Base>();
            }

            var uiMark = obj.GetComponent<UI_Base>();
            var uiWindow = obj.GetComponent<UI_Window>();
            var className = string.Empty;

            if(uiWindow!=null)
            {
                className = "IvyCore."+ obj.name;
            }
            if(string.IsNullOrEmpty(className))
            {
                if (uiMark != null && uiMark.generateMember_ && !uiMark.generateStandaloneComponent_)
                {
                    if (uiMark.componentName_.StartsWith("IvyCore."))
                        className = uiMark.componentName_;
                    else
                        className = uiMark.componentName_;
                }
                else if (uiMark != null && uiMark.generateMember_ && uiMark.generateStandaloneComponent_)
                {
                    if (uiMark.gameObject.name.StartsWith("IvyCore."))
                        className = uiMark.gameObject.name + "SAComponent";
                    else
                        className = "IvyCore." + uiMark.gameObject.name + "SAComponent";
                }
                else
                {
                    if (uiMark != null && uiMark.componentName_.StartsWith("IvyCore."))
                        className = behaviourName;
                    else
                        className = behaviourName;
                }
            }
            
            var t = assembly.GetType(className);

            if(t==null)
            {
                t = assemblyFirstPass.GetType(className);
            }
            var com1 = obj.GetComponent(t);
            
            var com = obj.GetComponent(t) ?? obj.AddComponent(t);
            SerializedObject sObj=null;
            if (serializedObject != null)
                sObj = serializedObject;
            else
                sObj = new SerializedObject(com);
            List<UI_Base> uiMarks = new List<UI_Base>();
            obj.GetComponentsInChildrenByRecursion<UI_Base>(ref uiMarks,false,null,(UI_Base curCheckObj)=>
            {
                if(curCheckObj!=null&&curCheckObj.generateStandaloneComponent_)
                {
                    return false;
                }
                return true;
            });

            if (sObj.FindProperty("SrcType") != null)
            {
                var property = sObj.FindProperty("SrcType");
                if (property != null)
                    property.objectReferenceValue = obj;
            }

            foreach (var elementMark in uiMarks)
            {
                if (!elementMark.generateMember_)
                    continue;
                if ((processedMarks.Contains(elementMark) || elementMark.componentName_.Contains("UnityEngine."))&& !elementMark.generateStandaloneComponent_)
                {
                    continue;
                }

                processedMarks.Add(elementMark);

                var uiType = string.Empty;
                if (elementMark.generateStandaloneComponent_)
                {
                    uiType = elementMark.gameObject.name + "SAComponent";
                }
                else
                {
                    uiType = elementMark.componentName_;
                }
                 
                var propertyName = elementMark.transform.name;

                if (sObj.FindProperty(propertyName) == null)
                {
                    Debug.Log(string.Format("sObj is Null:{0} {1} {2}", propertyName, uiType, sObj));
                    continue;
                }
                if(elementMark.generateStandaloneComponent_)
                    AttachSerializeObj(elementMark.transform.gameObject, elementMark.componentName_, assembly,assemblyFirstPass, processedMarks, null);
                else
                    AttachSerializeObj(elementMark.transform.gameObject, elementMark.componentName_, assembly, assemblyFirstPass, processedMarks, sObj);
                if (elementMark.transform.gameObject != null)
                {
                    var property = sObj.FindProperty(propertyName);
                    if (property != null)
                        property.objectReferenceValue = elementMark.transform.gameObject;
                }
            }

            var marks = obj.GetComponentsInChildren<UI_Base>(true);
            foreach (var elementMark in marks)
            {
                if (!elementMark.generateMember_)
                    continue;
                if (processedMarks.Contains(elementMark))
                {
                    continue;
                }

                processedMarks.Add(elementMark);

                var propertyName = elementMark.transform.name;
                var property = sObj.FindProperty(propertyName);
                if(property!=null)
                    property.objectReferenceValue = elementMark.transform.gameObject;
            }

            sObj.ApplyModifiedPropertiesWithoutUndo();
        }
        private WindowCodeData windowCodeData_;
        private static readonly UI_CodeGenerator instance_ = new UI_CodeGenerator();
    }
}
