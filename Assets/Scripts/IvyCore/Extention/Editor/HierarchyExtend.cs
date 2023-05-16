using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HierarchyExtend {

    [MenuItem("GameObject/同步到Prefab",priority = 48)]
    public static void ApplyToPrefab()
    {
        GameObject selectGo = Selection.activeGameObject;
        if (selectGo == null)
        {
            Debug.LogError("请选中需要Apply的Prefab实例");
            return;
        }
        PrefabType pType = PrefabUtility.GetPrefabType(selectGo);
        if (pType != PrefabType.PrefabInstance)
        {
            Debug.LogError("选中的实例不是Prefab实例");
            return;
        }
        //这里必须获取到prefab实例的根节点，否则ReplacePrefab保存不了
        GameObject prefabGo = GetPrefabInstanceParent(selectGo);
        UnityEngine.Object prefabAsset = null;
        if (prefabGo != null)
        {
            prefabAsset = PrefabUtility.GetPrefabParent(prefabGo);
            if (prefabAsset != null)
            {
                PrefabUtility.ReplacePrefab(prefabGo, prefabAsset, ReplacePrefabOptions.ConnectToPrefab);
            }
        }
        AssetDatabase.SaveAssets();
    }

    static GameObject GetPrefabInstanceParent(GameObject go)
    {
        if (go == null)
        {
            return null;
        }
        PrefabType pType = PrefabUtility.GetPrefabType(go);
        if (pType != PrefabType.PrefabInstance)
        {
            return null;
        }
        if (go.transform.parent == null)
        {
            return go;
        }
        pType = PrefabUtility.GetPrefabType(go.transform.parent.gameObject);
        if (pType != PrefabType.PrefabInstance)
        {
            return go;
        }
        return GetPrefabInstanceParent(go.transform.parent.gameObject);
    }
}
