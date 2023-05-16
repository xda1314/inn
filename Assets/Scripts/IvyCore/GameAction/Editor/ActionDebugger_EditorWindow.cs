using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace IvyCore
{
    public class ActionDebugger_EditorWindow : EditorWindow
    {
        static ActionDebugger_EditorWindow instance_ = null;
        GameObject m_CurrentSelectObject = null;
        ActionBase m_currentDebugAction = null;
        string m_ActionDescription = "";
        StringBuilder m_stringBuilder = new StringBuilder();
        public static void Init(string actionDes,GameObject go)
        {
            if (instance_==null)
            {
                instance_ = UnityEditor.EditorWindow.GetWindow<ActionDebugger_EditorWindow>(false, "Action Debugger", true);
                instance_.position = Sirenix.Utilities.Editor.GUIHelper.GetEditorWindowRect().AlignCenter(400, 300);
            }
            instance_.SetDebugActionByDescription(actionDes,go);
            instance_.Repaint();
        }

        public void SetDebugActionByDescription(string actiondes,GameObject go)
        {
            m_ActionDescription = actiondes;
            if (!GameActionManager.Instance.IsActionDescriptionNotRegisted(m_ActionDescription))
                m_currentDebugAction = GameActionManager.Instance.GetActionByDescription(m_ActionDescription).m_Action;
            else
                m_currentDebugAction = null;
            m_CurrentSelectObject = go;
        }

        private void OnSelectionChange()
        {
            var go = UnityEditor.Selection.activeGameObject;
            if (go != m_CurrentSelectObject)
                this.Close();
        }

        private void OnGUI()
        {
            if(m_currentDebugAction==null)
            {
                GUILayout.Label("无效Action");
                return;
            }
            m_stringBuilder.Remove(0, m_stringBuilder.Length);
            m_currentDebugAction.GetDebugInfo(m_stringBuilder, m_CurrentSelectObject);
            Sirenix.Utilities.Editor.SirenixEditorGUI.BeginBox();
            Sirenix.Utilities.Editor.SirenixEditorGUI.MessageBox(m_stringBuilder.ToString());
            Sirenix.Utilities.Editor.SirenixEditorGUI.EndBox();
        }
    }
}