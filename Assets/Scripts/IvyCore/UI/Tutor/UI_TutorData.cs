using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Text;
using System.IO;
using Sirenix.Utilities;
using Sirenix.Serialization;
using System;


#if UNITY_EDITOR
using UnityEditor;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;
#endif

namespace IvyCore
{
    [System.Serializable, ShowOdinSerializedPropertiesInInspector, HideReferenceObjectPicker]
    public class UI_TutorAtomData
    {
        public UI_TutorAtomData(string name, string des)
        {
            Name = name;
            Description = des;
        }

        public UI_TutorAtomData Clone(string name, string des)
        {
            UI_TutorAtomData newData = new UI_TutorAtomData(name, des);
            for (var i = 0; i < dataList_.Count; ++i)
            {
                newData.dataList_.Add(dataList_[i].Clone());
            }
            return newData;
        }

        [LabelText("教学名称"), Sirenix.OdinInspector.ReadOnly, ShowInInspector, PropertyOrder(-1), SerializeField]
        public string Name { get; set; }
        [LabelText("教学描述"), MultiLineProperty, ShowInInspector, PropertyOrder(-1), SerializeField]
        public string Description { get; set; }
        [LabelText("教学列表"), ListDrawerSettings(CustomAddFunction = "ShowAddMenu", ShowItemCount = true, OnBeginListElementGUI = "OnBeginListElementGUI", OnEndListElementGUI = "OnEndListElementGUI"), OdinSerialize, System.NonSerialized]
        public List<UI_TutorAtomBaseData> dataList_ = new List<UI_TutorAtomBaseData>();

        void OnBeginListElementGUI(int index)
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                var tm = UI_TutorManager.Instance;
                if (tm.GetCurrentRunningTutorData() == this && tm.GetCurrentRunningDataIndex() == index)
                    GUIHelper.PushColor(Color.green);
            }
#endif
        }

        void OnEndListElementGUI(int index)
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
            {

                var tm = UI_TutorManager.Instance;
                if (tm.GetCurrentRunningTutorData() == this && tm.GetCurrentRunningDataIndex() == index)
                    GUIHelper.PopColor();
            }
#endif
        }

        public int GetDataCount()
        {
            return dataList_.Count;
        }

        public UI_TutorAtomBaseData GetDataByIndex(int index)
        {
            if (index >= 0 && index < dataList_.Count)
            {
                return dataList_[index];
            }
            return null;
        }



        public void ShowAddMenu()
        {
#if UNITY_EDITOR
            GenericMenu gm = new GenericMenu();
            var dic = UI_TutorAtomBaseDataManager.Instance.GetRegistCreateTutorAtomBaseDataDic();
            var itr = dic.GetEnumerator();
            while (itr.MoveNext())
            {
                var func = itr.Current.Value;
                gm.AddItem(new GUIContent(itr.Current.Key), false, () => { dataList_.Add(func()); });
            }
            gm.ShowAsContext();
#endif
        }
    }

    [System.Serializable]
    public class UI_TutorData : SerializedScriptableObject
    {
        [System.NonSerialized]
        public const string DataFilePath = "Resources_moved/IVYData";
        [System.NonSerialized]
        public const string DataFileName = "UI_TutorData.asset";
        [LabelText("教学数据"), System.NonSerialized, OdinSerialize]
        public List<UI_TutorAtomData> dataList_ = new List<UI_TutorAtomData>();
        [HideInInspector]
        public Dictionary<string, UI_TutorAtomData> dataDic_ = new Dictionary<string, UI_TutorAtomData>();

        public static UI_TutorData GetData()
        {
            var path = DataFilePath.Replace("Resources_moved/", "") + "/" + DataFileName;
            UI_TutorData data = null;
            AssetSystem.Instance.LoadAsset<UI_TutorData>(path, (dataTemp) =>
            {
                data = dataTemp;
            });
            if (data != null)
            {
                data.Init();
            }
            return data;
        }

        public void Init()
        {
            dataDic_.Clear();
            var itr = dataList_.GetEnumerator();
            while (itr.MoveNext())
            {
                var data = itr.Current;
                dataDic_.Add(data.Name, data);
            }
        }

        public void ClearData()
        {
            dataDic_.Clear();
        }

        public bool IsDataNameExist(string name)
        {
            return dataDic_.ContainsKey(name);
        }

        public UI_TutorAtomData GetTutorAtomData(string name)
        {
            if (IsDataNameExist(name))
            {
                return dataDic_[name];
            }
            return null;
        }

        public void RenameTutorAtomData(string srcName, string newName, string des)
        {
            if (IsDataNameExist(srcName))
            {
                var data = dataDic_[srcName];
                data.Name = newName;
                data.Description = des;
                dataDic_.Remove(srcName);
                dataDic_.Add(newName, data);
            }
        }

        public void CloneAndAddTutorAtomDataByName(string srcName, string desName, string des)
        {
            if (IsDataNameExist(srcName))
            {
                var data = dataDic_[srcName];
                var newData = data.Clone(desName, des);
                dataList_.Add(newData);
                dataDic_.Add(newData.Name, newData);
            }
        }

        public bool AddTutorAtomData(string name, string des)
        {
            if (IsDataNameExist(name))
                return false;
            UI_TutorAtomData newData = new UI_TutorAtomData(name, des);
            dataList_.Add(newData);
            dataDic_.Add(newData.Name, newData);
            return true;
        }

        public bool DeleteTutorData(string name)
        {
            if (IsDataNameExist(name))
            {
                dataDic_.Remove(name);
                var itr = dataList_.GetEnumerator();
                while (itr.MoveNext())
                {
                    if (itr.Current.Name.Equals(name))
                    {
                        dataList_.Remove(itr.Current);
                        break;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
