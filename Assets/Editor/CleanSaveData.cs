using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CleanSaveData : Editor
{
    [MenuItem("工具箱/清除本地存档", priority = 10000)]
    public static void CleanData()
    {
        PlayerPrefs.DeleteAll();
        if (Directory.Exists(Application.persistentDataPath))
        {
            DirectoryInfo directory = new DirectoryInfo(Application.persistentDataPath);
            DeleteAllData(directory);
        }
        UnityEditor.EditorUtility.DisplayDialog("清理", "清除本地存档成功", "好的");
    }

    private static void DeleteAllData(DirectoryInfo directory)
    {
        foreach (FileInfo file in directory.GetFiles())
        {
            file.Delete();
        }

        foreach (var directoryInfo in directory.GetDirectories())
        {
            DeleteAllData(directoryInfo);
        }

        directory.Delete();
    }

}
