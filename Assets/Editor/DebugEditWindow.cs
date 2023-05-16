using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using Sirenix.Serialization;
using UnityEditor;
using Sirenix.Utilities;

public class DebugEditWindow : OdinMenuEditorWindow
{
    [MenuItem(itemName: "工具箱/打开测试工具", priority = 200)]
    private static void OpenWindow()
    {
        var window = GetWindow<DebugEditWindow>();
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 700);
        window.titleContent = new GUIContent("测试工具");
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        SO_DebugConfig temp_debugConfig = AssetDatabase.LoadAssetAtPath<SO_DebugConfig>("Assets/ScriptObjects/SO_DebugConfig.asset");
        if (temp_debugConfig == null)
        {
            ScriptableObject sO = ScriptableObject.CreateInstance(typeof(SO_DebugConfig));
            AssetDatabase.CreateAsset(sO, "Assets/ScriptObjects/SO_DebugConfig.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            temp_debugConfig = AssetDatabase.LoadAssetAtPath<SO_DebugConfig>("Assets/ScriptObjects/SO_DebugConfig.asset");
        }

        SO_DebugMap temp_debugMap = AssetDatabase.LoadAssetAtPath<SO_DebugMap>("Assets/ScriptObjects/SO_DebugMap.asset");
        if (temp_debugMap == null)
        {
            ScriptableObject sO = ScriptableObject.CreateInstance(typeof(SO_DebugMap));
            AssetDatabase.CreateAsset(sO, "Assets/ScriptObjects/SO_DebugMap.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            temp_debugMap = AssetDatabase.LoadAssetAtPath<SO_DebugMap>("Assets/ScriptObjects/SO_DebugMap.asset");
        }

        OdinMenuTree tree = new OdinMenuTree(supportsMultiSelect: true)
        {
            {"测试配置", temp_debugConfig},
            {"编辑地图", temp_debugMap},
        };

        return tree;
    }


}
