using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace IvyCore
{
    public class GameActionManager : Singleton<GameActionManager>
    {
        //[RuntimeInitializeOnLoadMethod]
        public static void InitWhenEngineStart()
        {
            GameActionManager.Instance.InitWhenGameActionRefresh();
        }

        public void InitWhenGameActionRefresh()
        {
            m_GameActionsData = GameActions.getData();
            InitFindDic();
        }

        public void InitFindDic()
        {
            m_GameActionFindByNameDic.Clear();
            m_GameActionFindByDescriptionDic.Clear();
            m_GameActionsDescription.Clear();
            if (m_GameActionsData != null)
            {
                var itr = m_GameActionsData.m_Actions.GetEnumerator();
                while (itr.MoveNext())
                {
                    if (!m_GameActionFindByNameDic.ContainsKey(itr.Current.m_Name))
                    {
                        m_GameActionFindByNameDic.Add(itr.Current.m_Name, itr.Current);
                    }
                    else
                    {
                        Debug.LogFormat("GameAction 名称重复:{0}", itr.Current.m_Name);
                    }
                    if (!m_GameActionFindByDescriptionDic.ContainsKey(itr.Current.m_Description))
                    {
                        m_GameActionFindByDescriptionDic.Add(itr.Current.m_Description, itr.Current);
                        m_GameActionsDescription.Add(itr.Current.m_Description);
                    }
                    else
                    {
                        Debug.LogFormat("GameAction 描述重复:{0}", itr.Current.m_Description);
                    }
                }
            }
        }

        public bool IsActionNameNotRegisted(string name)
        {
            return !m_GameActionFindByNameDic.ContainsKey(name);
        }

        public bool IsActionDescriptionNotRegisted(string description)
        {
            return !m_GameActionFindByDescriptionDic.ContainsKey(description);
        }

        public bool DeleteGameActionByName(string name)
        {
            if (!IsActionNameNotRegisted(name))
            {
                var action = m_GameActionFindByNameDic[name];
                if (!IsActionDescriptionNotRegisted(action.m_Description))
                {
                    m_GameActionFindByDescriptionDic.Remove(action.m_Description);
                }
                m_GameActionFindByNameDic.Remove(name);
                var index = m_GameActionsData.m_Actions.IndexOf(action);
                m_GameActionsData.m_Actions.RemoveAt(index);
                m_GameActionsDescription.RemoveAt(index);
            }
            return false;
        }

        public bool DeleteGameActionByDescription(string des)
        {
            if (!IsActionDescriptionNotRegisted(des))
            {
                var action = m_GameActionFindByDescriptionDic[des];
                if (!IsActionNameNotRegisted(action.m_Name))
                {
                    m_GameActionFindByNameDic.Remove(action.m_Name);
                }
                m_GameActionFindByDescriptionDic.Remove(des);
                var index = m_GameActionsData.m_Actions.IndexOf(action);
                m_GameActionsData.m_Actions.RemoveAt(index);
                m_GameActionsDescription.RemoveAt(index);
            }
            return false;
        }

        public GameAction AddGameAction(string name, string des, ActionSequence clone = null)
        {
            if (m_GameActionsData == null)
            {
                return null;
            }
            if (IsActionNameNotRegisted(name) && IsActionDescriptionNotRegisted(des))
            {
                GameAction newAction = null;
                if (clone != null)
                    newAction = new GameAction(name, des, clone);
                else
                    newAction = new GameAction(name, des);
                m_GameActionsData.m_Actions.Add(newAction);
                m_GameActionFindByNameDic.Add(newAction.m_Name, newAction);
                m_GameActionFindByDescriptionDic.Add(newAction.m_Description, newAction);
                m_GameActionsDescription.Add(newAction.m_Description);
                return newAction;
            }
            return null;
        }


        public GameAction GetActionByName(string name)
        {
            if (m_GameActionFindByNameDic.ContainsKey(name))
            {
                return m_GameActionFindByNameDic[name];
            }
            return null;
        }

        public GameAction GetActionByDescription(string des)
        {
            if (m_GameActionFindByDescriptionDic.ContainsKey(des))
            {
                return m_GameActionFindByDescriptionDic[des];
            }
            return null;
        }

        public List<string> GetAllActionsDescription()
        {
            if (m_GameActionsData == null || (m_GameActionsData != null && m_GameActionsData.m_Actions.Count <= 0))
                InitWhenGameActionRefresh();
            return m_GameActionsDescription;
        }

        GameActions m_GameActionsData = null;

        public IvyCore.GameActions GameActionsData
        {
            get
            {
                if (m_GameActionsData == null)
                {
                    InitWhenGameActionRefresh();
                }
                return m_GameActionsData;
            }
        }

        Dictionary<string, GameAction> m_GameActionFindByNameDic = new Dictionary<string, GameAction>();
        Dictionary<string, GameAction> m_GameActionFindByDescriptionDic = new Dictionary<string, GameAction>();
        List<string> m_GameActionsDescription = new List<string>();
    }
}