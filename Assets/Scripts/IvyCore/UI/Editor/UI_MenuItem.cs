using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.Utilities;

namespace IvyCore
{
    public class UI_MenuItem
    {
        static float UIEditorWindow_Width = 800;
        static float UIEditorWindow_Height = 600;
        [MenuItem("IvyCore/UI/UI管理器")]
        static void showUIEditorWindow()
        {
            var window = EditorWindow.GetWindow<UIEditorWindow>(false, "UI管理器", true);
            window.position = Sirenix.Utilities.Editor.GUIHelper.GetEditorWindowRect().AlignCenter(UIEditorWindow_Width, UIEditorWindow_Height);
            
        }
    }
}

