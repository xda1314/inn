using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using Sirenix.Utilities;
using System.IO;
using System.Text;
using Sirenix.OdinInspector;
using UnityEditor;

namespace IvyCore
{
    public class UIEditorWindow : OdinMenuEditorWindow
    {
        public static UIEditorWindow instance_ = null;
        UI_WindowRegisterData UI_WRD_;
        UI_SceneRegisterData UI_SRD_;
        UI_LayerRegisterData UI_LRD_;

        public UIEditorWindow()
        {
            instance_ = this;
        }

        void SaveData()
        {
            if (UI_WRD_)
                EditorUtility.SetDirty(UI_WRD_);
            if (UI_SRD_)
                EditorUtility.SetDirty(UI_SRD_);
            if (UI_LRD_)
                EditorUtility.SetDirty(UI_LRD_);
            AssetDatabase.SaveAssets();
        }


        protected override void OnDestroy()
        {
            SaveData();
            instance_ = null;
            base.OnDestroy();
        }

        protected override void OnGUI()
        {
            GUILayout.BeginHorizontal();
            if(GUILayout.Button("保存",GUILayout.MaxWidth(50),GUILayout.MaxHeight(25)))
            {
                SaveData();
            }
            GUILayout.EndHorizontal();
            base.OnGUI();
        }

        public static UI_WindowRegisterData createAndGetWindowRegisterData()
        {
            var filePathSB = new StringBuilder();
            filePathSB.Append(Application.dataPath).Append("/").Append(UI_WindowRegisterData.DataFilePath).Append("/").Append(UI_WindowRegisterData.DataFileName);
            if (!File.Exists(filePathSB.ToString()))
            {
                ScriptableObjectUtility.CreateAsset<UI_WindowRegisterData>(UI_WindowRegisterData.DataFileName, "Assets/" + UI_WindowRegisterData.DataFilePath);
            }
            var data = AssetDatabase.LoadAssetAtPath<UI_WindowRegisterData>("Assets/" + UI_WindowRegisterData.DataFilePath + "/" + UI_WindowRegisterData.DataFileName);
            return data;
        }

        public UI_SceneRegisterData createAndGetSceneRegisterData()
        {
            var filePathSB = new StringBuilder();
            filePathSB.Append(Application.dataPath).Append("/").Append(UI_SceneRegisterData.DataFilePath).Append("/").Append(UI_SceneRegisterData.DataFileName);
            if (!File.Exists(filePathSB.ToString()))
            {
                ScriptableObjectUtility.CreateAsset<UI_SceneRegisterData>(UI_SceneRegisterData.DataFileName, "Assets/" + UI_SceneRegisterData.DataFilePath);
            }
            var data = AssetDatabase.LoadAssetAtPath<UI_SceneRegisterData>("Assets/" + UI_SceneRegisterData.DataFilePath + "/" + UI_SceneRegisterData.DataFileName);
            return data;
        }

        public UI_LayerRegisterData createAndGetLayerRegisterData()
        {
            var filePathSB = new StringBuilder();
            filePathSB.Append(Application.dataPath).Append("/").Append(UI_LayerRegisterData.DataFilePath).Append("/").Append(UI_LayerRegisterData.DataFileName);
            if (!File.Exists(filePathSB.ToString()))
            {
                ScriptableObjectUtility.CreateAsset<UI_LayerRegisterData>(UI_LayerRegisterData.DataFileName, "Assets/" + UI_LayerRegisterData.DataFilePath);
            }
            var data = AssetDatabase.LoadAssetAtPath<UI_LayerRegisterData>("Assets/" + UI_LayerRegisterData.DataFilePath + "/" + UI_LayerRegisterData.DataFileName);
            return data;
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            UI_WRD_ = createAndGetWindowRegisterData();
            UI_SRD_ = createAndGetSceneRegisterData();
            UI_LRD_ = createAndGetLayerRegisterData();
            OdinMenuTree tree = new OdinMenuTree(supportsMultiSelect: false)
            {
                { "UI层次注册",                           UI_LRD_,                           EditorIcons.Paperclip      },
                { "UI界面注册",                           UI_WRD_,                           EditorIcons.GridLayout       },
                { "UI场景注册",                           UI_SRD_,                           EditorIcons.House       },

            };
            return tree;
        }
    }
}

