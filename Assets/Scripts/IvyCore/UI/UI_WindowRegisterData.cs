using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

namespace IvyCore
{
    [System.Serializable]
    public class UI_WindowRegisterAtomData
    {
        public UI_WindowRegisterAtomData(string rn, string des, GameObject go)
        {
            registName_ = rn;
            description_ = des;
            windowPrefab_ = go;
            OnWindowPrefabChanged();
        }

        [LabelText("注册名称"), ReadOnly, GUIColor(0f, 1f, 0f)]
        [Title("$registName_", "", TitleAlignments.Centered)]
        public string registName_;

        [InfoBox("窗体预制体未发现Canvas组件", InfoMessageType.Error, "invaildWindowPrefabData_")]
        [LabelText("窗体预制体"), OnValueChanged("OnWindowPrefabChanged")]
        public GameObject windowPrefab_;

        [LabelText("备注"), MultiLineProperty]
        public string description_;

        bool invaildWindowPrefabData_ = true;

        void OnWindowPrefabChanged()
        {
            if (windowPrefab_ != null)
            {
                var canvas = windowPrefab_.GetComponent<Canvas>();
                if (canvas == null)
                {
                    invaildWindowPrefabData_ = true;
                }
                else
                {
                    invaildWindowPrefabData_ = false;
                }
            }
            else
            {
                invaildWindowPrefabData_ = false;
            }
        }
    }

    [System.Serializable]
    public class UI_WindowRegisterData : SerializedScriptableObject
    {
        public const string DataFilePath = "Resources_moved/IVYData";
        public const string DataFileName = "UI_WindowRegisterData.asset";
        [LabelText("界面注册数据"), ListDrawerSettings(HideAddButton = true, ShowItemCount = true)]
        public List<UI_WindowRegisterAtomData> dataList_ = new List<UI_WindowRegisterAtomData>();

        public UI_WindowRegisterAtomData getWindowRegisterAtomData(string name)
        {
            foreach (var data in dataList_)
            {
                if (data.registName_ == name)
                {
                    return data;
                }
            }
            return null;
        }

        public void AddNewAtomData(string registName, string des, GameObject prefab)
        {
            UI_WindowRegisterAtomData data = new UI_WindowRegisterAtomData(registName, des, prefab);
            if (!IsDataRegisted(registName))
                dataList_.Add(data);
        }

        public bool IsDataRegisted(string registName)
        {
            if (dataList_.Find((UI_WindowRegisterAtomData data) =>
             {
                 if (data.registName_ == registName)
                     return true;
                 return false;
             }) != null)
                return true;
            return false;
        }

        //public static UI_WindowRegisterData getData()
        //{
        //    var path = DataFilePath.Replace("Resources/", "") + "/" + DataFileName.Replace(".asset", "");
        //    return Resources.Load<UI_WindowRegisterData>(path);
        //}

#if UNITY_EDITOR
        [Button("添加界面注册", ButtonSizes.Large), PropertyOrder(-1)]
        void addAtomData()
        {
            var window = UnityEditor.EditorWindow.GetWindow<UI_WindowRegisterAtomDataCreateWizard>(false, "添加数据", true);
            window.position = Sirenix.Utilities.Editor.GUIHelper.GetEditorWindowRect().AlignCenter(400, 300);
            window.init(this);
        }
#endif
    }
#if UNITY_EDITOR
    public class UI_WindowRegisterAtomDataCreateWizard : Sirenix.OdinInspector.Editor.OdinEditorWindow
    {
        UI_WindowRegisterData data_ = null;
        [LabelText("注册名称")]
        public string registName_ = string.Empty;
        [AssetsOnly, OnValueChanged("OnWindowPrefabChanged")]
        public GameObject windowPrefab_ = null;
        [LabelText("备注"), MultiLineProperty]
        public string description_ = string.Empty;

        bool vaildData = false;
        [LabelText("错误信息"), GUIColor(1, 0, 0), HideIf("vaildData")]
        public string errorInfo = "";

        void OnWindowPrefabChanged()
        {
            if (windowPrefab_ != null && string.IsNullOrEmpty(registName_))
            {
                registName_ = windowPrefab_.name;
            }
        }

        bool willClose_ = false;
        public void Update()
        {
            if (willClose_)
            {
                this.Close();
                return;
            }
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
            if (windowPrefab_ == null)
            {
                errorInfo = "窗体预制体为空";
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

        public void init(UI_WindowRegisterData data)
        {
            data_ = data;
            if (vaildData) { }
        }

        [Button("添加"), EnableIf("vaildData")]
        void addData()
        {
            if (data_.IsDataRegisted(registName_))
            {
                ShowNotification(new GUIContent("已注册同名窗体"));
                return;
            }

            if (!StringTools.IsStringOnlyContainEnglishNumber(registName_))
            {
                ShowNotification(new GUIContent("注册名称仅允许26个英文字母,数字和下划线"));
                return;
            }

            var canvas = windowPrefab_.GetComponent<Canvas>();
            if (canvas == null)
            {
                ShowNotification(new GUIContent("窗体预制体未发现Canvas组件"));
                return;
            }
            data_.AddNewAtomData(registName_, description_, windowPrefab_);
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
    }
#endif
}

