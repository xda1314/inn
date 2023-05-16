using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using TMPro;

public class OpenConfigFolder : Editor
{
    [MenuItem("工具箱/打开外部配表文件夹", priority = -100)]
    public static void OpenConfigTools()
    {
        DirectoryInfo info = new DirectoryInfo(Application.dataPath);
        EditorUtility.OpenWithDefaultApp(info.Parent + "/Configs");
    }



}
