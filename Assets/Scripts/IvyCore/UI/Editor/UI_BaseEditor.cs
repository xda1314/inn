using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace IvyCore
{
    [CustomEditor(typeof(UI_Base))]
    public class UI_BaseEditor : Sirenix.OdinInspector.Editor.OdinEditor
    {
        public void OnSceneGUI()
        {
        }
    }

    [DrawerPriority(0,0,3000)]
    public class EditAndClearActionButtonAttributeDrawer : OdinAttributeDrawer<EditAndClearActionButtonAttribute,string>
    {
        Rect buttonTempRect = new Rect();
        protected override void DrawPropertyLayout(GUIContent label)
        {
            
            // Call the next drawer, which will draw the float field.
            EditorGUILayout.BeginHorizontal();
            Rect rect = EditorGUILayout.GetControlRect();
            
            // Get a rect to draw the health-bar on. You Could also use GUILayout instead, but using rects makes it simpler to draw the health bar.
            if (!string.IsNullOrEmpty(this.ValueEntry.SmartValue))
            {
                buttonTempRect.Set(rect.x + rect.width - rect.height*2, rect.y, rect.height, rect.height);
                if (Sirenix.Utilities.Editor.SirenixEditorGUI.IconButton(buttonTempRect, EditorGUIUtility.FindTexture("CollabEdit Icon") as Texture, ""))
                {
                    GameAction_EditorWindow gew = null;
                    if (GameAction_EditorWindow.instance_==null)
                    {
                        gew = UnityEditor.EditorWindow.GetWindow<GameAction_EditorWindow>(false, "GameAction管理器", true);
                        gew.position = Sirenix.Utilities.Editor.GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
                    }
                    else
                    {
                        gew = GameAction_EditorWindow.instance_;
                    }
                    gew.JumpToTargetDescriptionMenuItem(this.ValueEntry.SmartValue);
                }
                buttonTempRect.Set(rect.x + rect.width - rect.height * 3, rect.y, rect.height, rect.height);
                if (Sirenix.Utilities.Editor.SirenixEditorGUI.IconButton(buttonTempRect, EditorGUIUtility.FindTexture("AudioMixerView Icon") as Texture, ""))
                {
                    ActionDebugger_EditorWindow.Init(this.ValueEntry.SmartValue,UnityEditor.Selection.activeGameObject);
                }
            }
            buttonTempRect.Set(rect.x+rect.width  - rect.height, rect.y, rect.height, rect.height);
            if (Sirenix.Utilities.Editor.SirenixEditorGUI.IconButton(buttonTempRect, EditorGUIUtility.FindTexture("CollabDeleted Icon") as Texture, ""))
            {
                this.ValueEntry.SmartValue = string.Empty;
            }
            EditorGUILayout.EndHorizontal();
            this.CallNextDrawer(label);

        }
    }
}