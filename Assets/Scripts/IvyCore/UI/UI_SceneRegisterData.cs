using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;
#if UNITY_EDITOR
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
#endif

namespace IvyCore
{
    [System.Serializable]
    public class UI_SceneRegisterAtomData
    {
        [System.NonSerialized, OdinSerialize]
        public string scenePath_;

        [System.NonSerialized, OdinSerialize]
        public Dictionary<string, List<string>> sceneLayerDic_ = new Dictionary<string, List<string>>();

        public UI_SceneRegisterAtomData(string sa)
        {
            scenePath_ = sa;
        }
        public void deleteLayerDataByName(string name)
        {
            if (sceneLayerDic_.ContainsKey(name))
            {
                sceneLayerDic_.Remove(name);
            }
        }

        public void deleteWindowDataByName(string layerName, string windowName)
        {
            if (sceneLayerDic_.ContainsKey(layerName))
            {
                if (sceneLayerDic_[layerName].Contains(windowName))
                {
                    sceneLayerDic_[layerName].Remove(windowName);
                    if (sceneLayerDic_[layerName].Count <= 0)
                    {
                        sceneLayerDic_.Remove(layerName);
                    }
                }
            }
        }

        public void moveForwardWindowDataByName(string layerName, string windowName)
        {
            if (sceneLayerDic_.ContainsKey(layerName))
            {
                if (sceneLayerDic_[layerName].Contains(windowName))
                {
                    var index = sceneLayerDic_[layerName].IndexOf(windowName);
                    if (index == 0)
                    {
                        return;
                    }
                    sceneLayerDic_[layerName].RemoveAt(index);
                    sceneLayerDic_[layerName].Insert(index - 1, windowName);
                }
            }
        }

        public void moveBackwardWindowDataByName(string layerName, string windowName)
        {
            if (sceneLayerDic_.ContainsKey(layerName))
            {
                if (sceneLayerDic_[layerName].Contains(windowName))
                {
                    var index = sceneLayerDic_[layerName].IndexOf(windowName);
                    if (index == sceneLayerDic_[layerName].Count - 1)
                    {
                        return;
                    }
                    sceneLayerDic_[layerName].RemoveAt(index);
                    sceneLayerDic_[layerName].Insert(index + 1, windowName);
                }
            }
        }
#if UNITY_EDITOR

        [Button("添加数据", ButtonSizes.Medium)]
        public void addData()
        {
            GenericMenu menu = new GenericMenu();

            var layerData = AssetSystem.Instance.LoadAsset<UI_LayerRegisterData>("IVYData/UI_LayerRegisterData", null).Result;
            var windowData = AssetSystem.Instance.LoadAsset<UI_WindowRegisterData>("IVYData/UI_WindowRegisterData", null).Result;

            if (layerData.dataList_.Count <= 0)
            {
                menu.AddDisabledItem(new GUIContent("无可用层次数据"));
            }
            else
            {
                foreach (var layerAtomData in layerData.dataList_)
                {
                    var layerName = layerAtomData.registName_;
                    if (windowData.dataList_.Count <= 0)
                    {
                        menu.AddDisabledItem(new GUIContent(layerName + "/无可用界面数据"));
                    }
                    foreach (var windowAtomData in windowData.dataList_)
                    {
                        var windowName = windowAtomData.registName_;
                        if (sceneLayerDic_.ContainsKey(layerName) && sceneLayerDic_[layerName].Contains(windowName))
                        {
                            menu.AddDisabledItem(new GUIContent(layerName + "/" + windowName));
                        }
                        else
                            menu.AddItem(new GUIContent(layerName + "/" + windowName), false, () =>
                            {
                                if (!sceneLayerDic_.ContainsKey(layerName))
                                {
                                    sceneLayerDic_[layerName] = new List<string>();
                                }
                                sceneLayerDic_[layerName].Add(windowName);
                            });
                    }
                }
            }

            menu.ShowAsContext();
        }
#endif
        /*[AssetsOnly,ValidateInput("CheckNodeVaild","必须使用Prefab对象")]
        public GameObject normalWindowPrefab_ = null;
#if UNITY_EDITOR
        bool CheckNodeVaild(GameObject obj)
        {
            if (obj == null)
            {
                return true;
            }
            var preType = PrefabUtility.GetPrefabType(obj);
            if (preType != PrefabType.None)
            {
                return true;
            }
            return false;
        }

#endif
        */

    }

    [System.Serializable]
    public class UI_SceneRegisterData : SerializedScriptableObject
    {
        public const string DataFilePath = "Resources_moved/IVYData";
        public const string DataFileName = "UI_SceneRegisterData.asset";

        [System.NonSerialized, OdinSerialize]
        public List<UI_SceneRegisterAtomData> dataList_ = new List<UI_SceneRegisterAtomData>();

        public bool IsSceneRegisted(string scene)
        {
            foreach (var data in dataList_)
            {
                if (data.scenePath_ == scene)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddNewSceneRegisterAtomData(string scene)
        {
            if (IsSceneRegisted(scene))
            {
                Debug.LogError("已注册场景");
                return;
            }
            var newData = new UI_SceneRegisterAtomData(scene);
            dataList_.Add(newData);
        }

        public static UI_SceneRegisterData getData()
        {
            var path = DataFilePath.Replace("Resources_moved/", "") + "/" + DataFileName;
            return AssetSystem.Instance.LoadAsset<UI_SceneRegisterData>(path, null).Result;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(UI_SceneRegisterData))]
    public class UI_SceneRegisterDataDrawer : Editor
    {
        List<int> testList = new List<int>() { 1, 2, 3, 4, 5, 6 };

        Vector2 mainScrollViewPos;
        public override void OnInspectorGUI()
        {
            //var UI_LRD = UI_LayerRegisterData.getData();
            //if (UI_LRD == null)
            //{
            //    SirenixEditorGUI.ErrorMessageBox("未发现层次注册数据");
            //    return;
            //}
            //var UI_WRD = UI_WindowRegisterData.getData();
            //if (UI_WRD == null)
            //{
            //    SirenixEditorGUI.ErrorMessageBox("未发现界面注册数据");
            //    return;
            //}

            //var data = target as UI_SceneRegisterData;
            //if (GUILayout.Button("添加场景注册", GUILayout.MinHeight(30)))
            //{
            //    var window = UnityEditor.EditorWindow.GetWindow<UI_SceneRegisterDataCreateWizard>(false, "添加数据", true);
            //    window.position = Sirenix.Utilities.Editor.GUIHelper.GetEditorWindowRect().AlignCenter(400, 300).AddX(-600);
            //    window.init(data);
            //}

            //mainScrollViewPos = GUILayout.BeginScrollView(mainScrollViewPos);
            //GUILayout.BeginVertical();
            //foreach (var dataChild in data.dataList_)
            //{
            //    var DataRect = SirenixEditorGUI.BeginBox();
            //    GUILayout.BeginHorizontal();
            //    GUILayout.BeginVertical(GUILayout.MaxWidth(200));
            //    GUILayout.Label("场景:");
            //    var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(dataChild.scenePath_);
            //    var newScene = SirenixEditorFields.UnityObjectField(oldScene, typeof(SceneAsset), false);
            //    if (newScene != oldScene)
            //    {
            //        if (newScene == null)
            //            dataChild.scenePath_ = "";
            //        else
            //            dataChild.scenePath_ = AssetDatabase.GetAssetPath(newScene);
            //    }

            //    //dataChild.scene_=(ScenePicker)SirenixEditorFields.UnityObjectField(dataChild.scene_, typeof(ScenePicker), false);
            //    if (GUILayout.Button(EditorIcons.Plus.Raw, GUILayout.MaxHeight(25), GUILayout.MaxWidth(25)))
            //    {
            //        dataChild.addData();
            //    }
            //    GUILayout.EndVertical();
            //    GUILayout.BeginVertical();
            //    foreach (var layerDataKV in dataChild.sceneLayerDic_)
            //    {
            //        SirenixEditorGUI.BeginBox();
            //        string layerName = layerDataKV.Key;
            //        if (!UI_LRD.IsDataRegisted(layerName))
            //        {
            //            SirenixEditorGUI.ErrorMessageBox("层次: " + layerName + "不存在");
            //        }
            //        GUILayout.BeginHorizontal();
            //        if (GUILayout.Button(EditorIcons.Minus.Raw, GUILayout.MaxWidth(25), GUILayout.MaxHeight(20)))
            //        {
            //            dataChild.deleteLayerDataByName(layerName);
            //            GUIHelper.RequestRepaint();
            //            return;
            //        }
            //        GUILayout.Label("层次: " + layerName);
            //        GUILayout.EndHorizontal();
            //        GUILayout.BeginHorizontal();
            //        GUIHelper.PushIndentLevel(2);
            //        SirenixEditorGUI.BeginIndentedVertical();

            //        var index = 0;
            //        var dataCount = layerDataKV.Value.Count;
            //        foreach (var windowData in layerDataKV.Value)
            //        {
            //            var dragRect = SirenixEditorGUI.BeginBox();
            //            bool windowExist = false;
            //            windowExist = UI_WRD.IsDataRegisted(windowData);
            //            if (!windowExist)
            //            {
            //                SirenixEditorGUI.ErrorMessageBox("界面: " + windowData + "不存在");
            //            }
            //            GUILayout.BeginHorizontal();
            //            var windowAtomData = UI_WRD.getWindowRegisterAtomData(windowData);
            //            if (windowAtomData != null)
            //                GUILayout.Label(windowData + "(" + windowAtomData.description_ + ")");
            //            else
            //                GUILayout.Label(windowData);

            //            GUILayout.FlexibleSpace();
            //            if (dataCount > 1)
            //            {
            //                if (index > 0)
            //                    if (GUILayout.Button(EditorIcons.ArrowUp.Raw, GUILayout.MaxWidth(25), GUILayout.MaxHeight(20)))
            //                    {
            //                        dataChild.moveForwardWindowDataByName(layerName, windowData);
            //                        GUIHelper.RequestRepaint();
            //                        return;
            //                    }
            //                if (index < layerDataKV.Value.Count - 1)
            //                    if (GUILayout.Button(EditorIcons.ArrowDown.Raw, GUILayout.MaxWidth(25), GUILayout.MaxHeight(20)))
            //                    {
            //                        dataChild.moveBackwardWindowDataByName(layerName, windowData);
            //                        GUIHelper.RequestRepaint();
            //                        return;
            //                    }
            //            }

            //            if (windowExist)
            //            {
            //                if (windowAtomData != null)
            //                {
            //                    if (GUILayout.Button(EditorIcons.Bell.Raw, GUILayout.MaxWidth(25), GUILayout.MaxHeight(20)))
            //                    {
            //                        Selection.activeObject = null;
            //                        EditorGUIUtility.PingObject(windowAtomData.windowPrefab_);
            //                    }
            //                }
            //            }
            //            if (GUILayout.Button(EditorIcons.Minus.Raw, GUILayout.MaxWidth(25), GUILayout.MaxHeight(20)))
            //            {
            //                dataChild.deleteWindowDataByName(layerName, windowData);
            //                GUIHelper.RequestRepaint();
            //                return;
            //            }
            //            GUILayout.EndHorizontal();
            //            SirenixEditorGUI.EndBox();
            //            ++index;
            //        }
            //        SirenixEditorGUI.EndIndentedVertical();

            //        GUIHelper.PopIndentLevel();
            //        GUILayout.EndHorizontal();
            //        SirenixEditorGUI.EndBox();

            //    }
            //    GUILayout.EndVertical();

            //    if (GUILayout.Button(EditorIcons.Minus.Raw, GUILayout.MaxWidth(25), GUILayout.ExpandHeight(true)))
            //    {
            //        data.dataList_.Remove(dataChild);
            //        GUIHelper.RequestRepaint();
            //        return;
            //    }
            //    GUILayout.EndHorizontal();
            //    SirenixEditorGUI.EndBox();

            //}
            //GUILayout.EndVertical();
            //GUILayout.EndScrollView();
        }
        //protected override void DrawPropertyLayout(IPropertyValueEntry<UI_SceneRegisterData> entry, GUIContent label)
        //{
        //    //base.DrawPropertyLayout(entry, label);
        //    UI_SceneRegisterData data = entry.SmartValue;
        //    SirenixEditorGUI.BeginBox();
        //    for(int i=0;i<data.dataList_.Count;++i)
        //    {

        //    }
        //    if(GUILayout.Button("添加"))
        //    {

        //    }
        //    SirenixEditorGUI.EndBox();
        //    entry.SmartValue = data;
        //}
    }

    public class UI_SceneRegisterDataCreateWizard : Sirenix.OdinInspector.Editor.OdinEditorWindow
    {
        UI_SceneRegisterData data_ = null;

        [AssetsOnly]
        public SceneAsset scene_ = null;

        bool vaildData = false;

        [LabelText("错误信息"), GUIColor(1, 0, 0), HideIf("vaildData")]
        public string errorInfo = "";

        bool willClose_ = false;
        public void init(UI_SceneRegisterData data)
        {
            data_ = data;
            if (vaildData) { }
        }

        [Button("添加"), EnableIf("vaildData")]
        void addData()
        {
            var path = AssetDatabase.GetAssetPath(scene_);
            if (data_.IsSceneRegisted(path))
            {
                ShowNotification(new GUIContent("已注册场景"));
                return;
            }

            data_.AddNewSceneRegisterAtomData(path);
            willClose_ = true;
        }

        [Button("取消")]
        void cancelAddData()
        {
            willClose_ = true;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        public void Update()
        {
            if (willClose_)
            {
                this.Close();
                return;
            }
            if (data_ == null)
            {
                errorInfo = "严重Bug";
                vaildData = false;
                return;
            }
            if (scene_ == null)
            {
                errorInfo = "注册场景为空";
                vaildData = false;
                return;
            }
            vaildData = true;
        }
    }
#endif
}

