using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IvyCore
{
    [Serializable]
    public class GameAction
    {
        public GameAction(string name, string des)
        {
            m_Name = name;
            m_Description = des;
            m_Action = new ActionSequence(false);
        }
        public GameAction(string name, string des, ActionSequence clone)
        {
            m_Name = name;
            m_Description = des;
            m_Action = clone.Clone() as ActionSequence;
        }
        public GameAction()
        {
        }
        [NonSerialized, Sirenix.Serialization.OdinSerialize]
        public string m_Name = "";
        [NonSerialized, Sirenix.Serialization.OdinSerialize]
        public string m_Description = "";
        [NonSerialized, Sirenix.Serialization.OdinSerialize]
        public ActionSequence m_Action = new ActionSequence(false);
    }

    [System.Serializable, Sirenix.OdinInspector.ShowOdinSerializedPropertiesInInspector]
    public class GameActions : Sirenix.OdinInspector.SerializedScriptableObject
    {
        [NonSerialized, Sirenix.Serialization.OdinSerialize]
        public List<GameAction> m_Actions = new List<GameAction>();
        [NonSerialized]
        public const string DataFilePath = "Resources_moved/IVYData";
        [NonSerialized]
        public const string DataFileName = "GameActions.asset";

        public static GameActions getData()
        {
            var path = DataFilePath.Replace("Resources_moved/", "") + "/" + DataFileName;
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return UnityEditor.AssetDatabase.LoadAssetAtPath<GameActions>("Assets/" + DataFilePath + "/" + DataFileName);
            }
#endif
            return AssetSystem.Instance.LoadAsset<GameActions>(path, null).Result;
        }
    }
}