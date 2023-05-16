using BDUnity.Utils;
using ivy.game;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 物品配置信息
/// </summary>
public class MapDefinition
{
    private static string[] mapName =
    {
        MergeLevelManager.MapName_Main,
        MergeLevelManager.MapName_Dungeon1,
        MergeLevelManager.MapName_Dungeon2,
        MergeLevelManager.MapName_Dungeon3,
        MergeLevelManager.MapName_Dungeon4,
        MergeLevelManager.MapName_Dungeon5,
        MergeLevelManager.MapName_Branch1,
        MergeLevelManager.MapName_Halloween,
        MergeLevelManager.MapName_Christmas,
        MergeLevelManager.MapName_Lover,
        MergeLevelManager.MapName_BranchChristmas,
        MergeLevelManager.MapName_BranchHalloween,
        MergeLevelManager.MapName_Branch4,
        MergeLevelManager.MapName_Branch5,
        MergeLevelManager.MapName_Branch6,
        MergeLevelManager.MapName_Daily1,
        MergeLevelManager.MapName_Daily2,
        MergeLevelManager.MapName_Daily3,
        MergeLevelManager.MapName_Daily4,
        MergeLevelManager.MapName_Daily5,
         MergeLevelManager.MapName_Daily6,
    };

    public static Dictionary<string, MapDefinition> TotalDefinitionsDict { get; private set; } = new Dictionary<string, MapDefinition>();

    // 关卡名称
    public string LevelName;
    // 是否是主关卡
    public bool IsMainLevel => LevelName == MergeLevelManager.MapName_Main;

    // 当前关卡地图信息 <格子坐标,格子>
    public Dictionary<Vector2Int, MergeGridData> gridDataDict { get; private set; } = new Dictionary<Vector2Int, MergeGridData>();
    // 当前关卡物体信息 {格子坐标，物体}
    public Dictionary<Vector2Int, MergeItemData> itemDataDict { get; private set; } = new Dictionary<Vector2Int, MergeItemData>();




    public static void Init()
    {
        for (int i = 0; i < mapName.Length; i++)
        {
            Dictionary<string, CustomJSONObject> dict = SingletonMono<GameConfig>.Instance.GetConfig(mapName[i]).dict;
            if (dict != null && dict.Count == 1)
            {
                Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
                enumerator.MoveNext();
                Dictionary<string, CustomJSONObject> dict2 = enumerator.Current.Value.dict;
                enumerator = dict2.GetEnumerator();
                MapDefinition mapDefinition = new MapDefinition();
                while (enumerator.MoveNext())
                {
                    Dictionary<string, CustomJSONObject> dict3 = enumerator.Current.Value.dict;

                    if (dict3.TryGetValue("Id", out CustomJSONObject id_Json))
                    {
                        if (int.TryParse(id_Json.ToString(), out int id))
                        {
                            mapDefinition.LoadBaseData(mapName[i], dict3);
                        }
                    }
                    else
                    {
                        GameDebug.LogError("grid data error");
                    }
                }
                mapDefinition.LevelName = mapName[i];
                TotalDefinitionsDict.Add(mapDefinition.LevelName, mapDefinition);
            }
            else
            {
                GameDebug.LogError("MergeMapConfig::Init: Config is null.");
            }
        }
        MergeLevelManager.Instance.InitManager();
    }

#if UNITY_EDITOR
    public static MapDefinition Debug_AddMapDefinition(string fileFullPath)
    {
        if (string.IsNullOrEmpty(fileFullPath))
            return null;
        string fileKey = Path.GetFileNameWithoutExtension(fileFullPath);
        Dictionary<string, CustomJSONObject> dict = CustomJSON.Deserialize(File.ReadAllText(fileFullPath), true).dict[fileKey].dict;
        Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
        MapDefinition mapDefinition = new MapDefinition();
        while (enumerator.MoveNext())
        {
            Dictionary<string, CustomJSONObject> dict3 = enumerator.Current.Value.dict;

            if (dict3.TryGetValue("Id", out CustomJSONObject id_Json))
            {
                if (int.TryParse(id_Json.ToString(), out int id))
                {
                    mapDefinition.LoadBaseData(fileKey, dict3);
                }
            }
            else
            {
                GameDebug.LogError("grid data error");
            }
        }
        mapDefinition.LevelName = fileKey;
        TotalDefinitionsDict[mapDefinition.LevelName] = mapDefinition;
        return mapDefinition;
    }
#endif

    private void LoadBaseData(string configName, Dictionary<string, CustomJSONObject> dataDic)
    {
        dataDic.TryGetValue("PositionX", out CustomJSONObject positionX_Json);
        dataDic.TryGetValue("PositionY", out CustomJSONObject positionY_Json);
        if (!string.IsNullOrEmpty(positionX_Json.ToString()) && !string.IsNullOrEmpty(positionY_Json.ToString()))
        {
            Vector2Int gridPos = new Vector2Int(0, 0);
            string prefab = string.Empty;
            bool locked = false;
            bool inBox = false;

            if (int.TryParse(positionX_Json.ToString(), out int posX) && int.TryParse(positionY_Json.ToString(), out int posY))
            {
                gridPos = new Vector2Int(posX, posY);
            }
            else
            {
                GameDebug.LogError("gridPos error");
            }
            if (dataDic.TryGetValue("Prefab", out CustomJSONObject prefab_Json))
            {
                prefab = prefab_Json.ToString();

#if UNITY_EDITOR
                if (!string.IsNullOrEmpty(prefab) && !MergeItemDefinition.TotalDefinitionsDict.TryGetValue(prefab, out _))
                {
                    Ivy.LogUtils.LogError($"[MapDefinition] {configName}配表中物品在MergeObjectConfig配表中找不到！名称：" + prefab);
                }
#endif
            }
            if (dataDic.TryGetValue("Lock", out CustomJSONObject lock_Json))
            {
                locked = lock_Json.ToString() == "x" ? true : false;
            }
            if (dataDic.TryGetValue("InBox", out CustomJSONObject inBox_Json))
            {
                inBox = inBox_Json.ToString() == "x" ? true : false;
            }

            MergeGridData gridData = new MergeGridData(gridPos);
            gridDataDict.Add(gridPos, gridData);

            //Debug.LogError(prefab);
            if (!string.IsNullOrEmpty(prefab))
            {
                if (!MergeItemDefinition.TotalDefinitionsDict.TryGetValue(prefab, out MergeItemDefinition mergeObjectItem))
                {
                    Debug.LogError("MergeObjectConfig表中没找到prefabName！" + prefab);
                    mergeObjectItem = null;
                }
                var chargeFinishDate = DateTimeOffset.MaxValue;
                if (mergeObjectItem != null)
                {
                    if (mergeObjectItem.CategoryType == MergeItemCategoryType.wake)
                        chargeFinishDate = TimeManager.Instance.UtcNow().AddSeconds(mergeObjectItem.ChargeLoopCDSecond);
                }
                MergeItemData itemData = new MergeItemData(
                    prefabName: prefab,
                    gridPos,
                    mergeObjectItem,
                    isInBubble: false,
                    locked,
                    inBox,
                    bubbleDieTime: DateTimeOffset.MaxValue,
                    chargeRemainUseTimes: 0,
                    totalChargedCount: mergeObjectItem != null ? mergeObjectItem.TotalChargeCount : 0,
                    ChargeFinishDate: chargeFinishDate,
                    chargeRemainUseTimes_auto: 0,
                    totalChargedCount_auto: mergeObjectItem != null && mergeObjectItem.TotalChargeCount_Auto > 0 ? 1 : 0,
                    ChargeFinishDate_auto: DateTimeOffset.MaxValue,
                    false,
                    boxOpenDelayTime: DateTimeOffset.MinValue,
                    0,
                    0,
                    0,
                    mergeObjectItem.CategoryType == MergeItemCategoryType.swallowC || mergeObjectItem.CategoryType == MergeItemCategoryType.swallowZ || mergeObjectItem.CategoryType == MergeItemCategoryType.swallowF,
                    0,
                    DateTimeOffset.MinValue,
                    false,
                    DateTimeOffset.MaxValue,
                    DateTimeOffset.MinValue,
                    DateTimeOffset.MinValue,
                    string.Empty);

                itemDataDict.Add(gridPos, itemData);
            }
        }
        else
        {
            GameDebug.LogError("地图数据不完整");
        }
    }

}
