using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

public class TMPFontAssetDynamicSetEditor : MonoBehaviour
{
    [MenuItem("Assets/资源工具/添加TMPFontDynamicSet组件")]
    public static void CheckDynamicSetFontComponent()
    {
        Object select = Selection.activeObject;
        string path = AssetDatabase.GetAssetPath(select);
        var directoryInfo = new DirectoryInfo(path);
        FileInfo[] fileInfos = directoryInfo.GetFiles("*.prefab", SearchOption.AllDirectories);
        foreach (FileInfo fileInfo in fileInfos)
        {
            string assetPath = fileInfo.FullName.Replace(@"\", "/").Replace(Application.dataPath, "Assets");
            //GameDebug.LogError("添加TMPFontDynamicSet组件1:" + Application.dataPath);
            //GameDebug.LogError("添加TMPFontDynamicSet组件2:" + assetPath);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            if (prefab != null)
            {
                var tmp = prefab.GetComponentsInChildren<TMP_Text>(true);
                if (tmp != null && tmp.Length > 0)
                {
                    GameDebug.Log("数量:" + tmp.Length);
                    foreach (var item2 in tmp)
                    {
                        SetFontSetComponent(item2);
                    }
                    EditorUtility.SetDirty(prefab);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }

        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        GameDebug.Log("---------添加TMPFontDynamicSet组件完成---------");
    }

    public static void SetFontSetComponent(TMP_Text tmp_text)
    {
        if (tmp_text == null)
            return;

        var tmps = tmp_text.GetComponentsInChildren<TMPFontDynamicSet>(true);
        if (tmps.Length > 1)
        {
            foreach (var item in tmps)
            {
                DestroyImmediate(item);
            }
        }

        if (tmp_text.TryGetComponent<TMPFontDynamicSet>(out var fontSet))
        {
            fontSet._TMP_text = tmp_text;
        }
        else
        {
            fontSet = tmp_text.gameObject.AddComponent<TMPFontDynamicSet>();
            fontSet._TMP_text = tmp_text;
        }
    }

}
