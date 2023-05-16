using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IvyCore
{
    [System.Serializable]
    public class UI_LayerRegisterAtomData
    {
        public UI_LayerRegisterAtomData(string name, string des)
        {
            registName_ = name;
            description_ = des;
        }

        [LabelText("注册名称"), ReadOnly, GUIColor(0f, 1f, 0f), OdinSerialize, System.NonSerialized]
        [Title("$registName_", "", TitleAlignments.Centered)]
        public string registName_;

        [LabelText("备注"), MultiLineProperty, OdinSerialize, System.NonSerialized]
        public string description_;
    }

    [System.Serializable]
    public class UI_LayerRegisterData : SerializedScriptableObject
    {
        [System.NonSerialized]
        public const string DataFilePath = "Resources_moved/IVYData";
        [System.NonSerialized]
        public const string DataFileName = "UI_LayerRegisterData.asset";

        [LabelText("UI层次注册数据"), ListDrawerSettings(ShowItemCount = true, HideAddButton = true), OdinSerialize, System.NonSerialized, HideReferenceObjectPicker]
        public List<UI_LayerRegisterAtomData> dataList_ = new List<UI_LayerRegisterAtomData>();

        public void AddNewAtomData(string registName, string des)
        {
            UI_LayerRegisterAtomData data = new UI_LayerRegisterAtomData(registName, des);
            if (!IsDataRegisted(registName))
                dataList_.Add(data);
        }

        public bool IsDataRegisted(string registName)
        {
            if (dataList_.Find((UI_LayerRegisterAtomData data) =>
            {
                if (data.registName_ == registName)
                    return true;
                return false;
            }) != null)
                return true;
            return false;
        }

        public static UI_LayerRegisterData getData()
        {
            var path = DataFilePath.Replace("Resources_moved/", "") + "/" + DataFileName;
            return AssetSystem.Instance.LoadAsset<UI_LayerRegisterData>(path, null).Result;
        }

#if UNITY_EDITOR
        [Button("添加层次注册", ButtonSizes.Large), PropertyOrder(-1)]
        void addAtomData()
        {
            var window = UnityEditor.EditorWindow.GetWindow<UI_LayerRegisterAtomDataCreateWizard>(false, "添加数据", true);
            window.position = Sirenix.Utilities.Editor.GUIHelper.GetEditorWindowRect().AlignCenter(400, 300);
            window.init(this);
        }
#endif
    }
#if UNITY_EDITOR
    public class UI_LayerRegisterAtomDataCreateWizard : Sirenix.OdinInspector.Editor.OdinEditorWindow
    {
        UI_LayerRegisterData data_ = null;

        [LabelText("注册名称")]
        public string registName_ = string.Empty;

        [LabelText("备注"), MultiLineProperty]
        public string description_ = string.Empty;

        bool vaildData = false;

        [LabelText("错误信息"), GUIColor(1, 0, 0), HideIf("vaildData")]
        public string errorInfo = "";

        bool willClose_ = false;

        public void init(UI_LayerRegisterData data)
        {
            data_ = data;
            if (vaildData) { }
        }

        [Button("添加"), EnableIf("vaildData")]
        void addData()
        {
            if (data_.IsDataRegisted(registName_))
            {
                ShowNotification(new GUIContent("已注册同名Layer"));
                return;
            }

            if (!StringTools.IsStringOnlyContainEnglishNumber(registName_))
            {
                ShowNotification(new GUIContent("注册名称仅允许26个英文字母,数字和下划线"));
                return;
            }

            data_.AddNewAtomData(registName_, description_);

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
            if (this.SafeIsUnityNull())
                return;
            if (registName_ == string.Empty)
            {
                errorInfo = "注册名称为空";
                vaildData = false;
                return;
            }

            if (registName_ != null && registName_.Contains("/"))
            {
                errorInfo = "注册名称中不允许出现字符 / ";
                vaildData = false;
                return;
            }

            if (data_ == null)
            {
                errorInfo = "注册对象为空";
                vaildData = false;
                return;
            }
            vaildData = true;
        }
    }
#endif
}


