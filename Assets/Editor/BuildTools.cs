using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public class BuildTools : MonoBehaviour
{
    [MenuItem("打包工具/清除TMP字体缓存", priority = 1)]
    public static void CleanTMPFontCache()
    {
        List<string> keys = new List<string>()
            {
                // 添加字体
                //TMPFontDynamicSet.TMPFontAssetKey_normal,
                //TMPFontDynamicSet.TMPFontAssetKey_normalBold,
                "CMFont SDF",
                "CMFont SDF_Bold",
                TMPFontDynamicSet.TMPFontAssetKey_cn,
                TMPFontDynamicSet.TMPFontAssetKey_ko
            };
        bool fail = false;
        foreach (var item in keys)
        {
            string path = "Assets/Font/" + item + ".asset";
            TMP_FontAsset tmpFontAsset = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(path);
            if (tmpFontAsset != null)
            {
                tmpFontAsset.ClearFontAssetData();
                EditorUtility.SetDirty(tmpFontAsset);
            }
            else
            {
                fail = true;
                GameDebug.LogError("无法找到TMP字体：" + path);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        if (!fail)
            GameDebug.Log("---------清除TMP字体缓存完成---------");
    }

    [MenuItem("打包工具/清理ServerData目录ab", priority = 100)]
    public static void CleanServerData()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath);
#if UNITY_IOS
        string path = Path.Combine(directoryInfo.Parent.FullName, "ServerData", "iOS");
#else
        string path = Path.Combine(directoryInfo.Parent.FullName, "ServerData", "Android");
#endif
        DirectoryInfo directoryInfoNew = new DirectoryInfo(path);
        if (directoryInfoNew.Exists)
            Directory.Delete(path, true);
        Debug.Log("清理ServerData目录ab成功！" + path);

        AddressableAssetSettings.CleanPlayerContent();
        Debug.Log("清理addressables缓存成功！");
    }


}
