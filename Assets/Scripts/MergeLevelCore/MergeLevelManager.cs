using Ivy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡类型
/// </summary>
public enum MergeLevelType
{
    none = 0,
    mainLine = 1,
    dungeon1 = 2,
    dungeon2 = 3,
    dungeon3 = 4,
    dungeon4 = 5,
    dungeon5 = 6,
    branch1 = 7,
    halloween = 8,
    christmas = 9,
    lover = 10,
    branch_halloween=11,
    branch_christmas = 12,
    daily1 = 13,
    daily2 = 14,
    daily3 = 15,
    daily4 = 16,
    daily5 = 17,
    branch_SpurLine4=18,
    daily6 = 19,
    branch_SpurLine5 = 20,
    branch_SpurLine6 = 21,
#if UNITY_EDITOR
    DebugMap_FreeGrid = -1,
#endif
}

/// <summary>
/// 关卡管理
/// </summary>
public class MergeLevelManager
{
    public static MergeLevelManager Instance { get; private set; } = new MergeLevelManager();

    // 主关卡索引
    public static string MapName_Main => "MergeMapConfig";
    public static string MapName_Dungeon1 => "MergeDungeon1Config";
    public static string MapName_Dungeon2 => "MergeDungeon2Config";
    public static string MapName_Dungeon3 => "MergeDungeon3Config";
    public static string MapName_Dungeon4 => "MergeDungeon4Config";
    public static string MapName_Dungeon5 => "MergeDungeon5Config";
    public static string MapName_Branch1 => "MergeSpurLine1Config";
    public static string MapName_Branch4 => "MergeSpurLine4Config";
    public static string MapName_Branch5 => "MergeSpurLine5Config";
    public static string MapName_Branch6 => "MergeSpurLine6Config";
    public static string MapName_Halloween => "MergeFestival1Config";
    public static string MapName_Christmas => "MergeFestival2Config";
    public static string MapName_Lover => "MergeFestival3Config";
    public static string MapName_BranchHalloween => "MergeSpurLine2Config";
    public static string MapName_BranchChristmas => "MergeSpurLine3Config";

    public static string MapName_Daily1 => "MergeDailyEvent1Config";
    public static string MapName_Daily2 => "MergeDailyEvent2Config";
    public static string MapName_Daily3 => "MergeDailyEvent3Config";
    public static string MapName_Daily4 => "MergeDailyEvent4Config";
    public static string MapName_Daily5 => "MergeDailyEvent5Config";
    public static string MapName_Daily6 => "MergeDailyEvent6Config";

    /// <summary>
    /// 所有的关卡信息
    /// </summary>
    public Dictionary<string, MergeMapData> totalMapDataDict { get; private set; }

    /// <summary>
    /// 当前关卡的数据
    /// </summary>
    public MergeMapData CurrentMapData { get; private set; }

    //当前关卡类型
    public MergeLevelType CurrentLevelType { get; private set; }
    public bool IsFestivalBranch(MergeLevelType levelType)
    {
        return levelType == MergeLevelType.halloween
            || levelType == MergeLevelType.christmas
            || levelType == MergeLevelType.lover;
    }

    public bool IsDailyActive(MergeLevelType levelType)
    {
        return levelType == MergeLevelType.daily1
            || levelType == MergeLevelType.daily2
            || levelType == MergeLevelType.daily3
            || levelType == MergeLevelType.daily4
            || levelType == MergeLevelType.daily5
        || levelType == MergeLevelType.daily6;
    }

    public bool IsBranch(MergeLevelType levelType)
    {
        return levelType == MergeLevelType.branch1
            || levelType == MergeLevelType.branch_christmas
            || levelType == MergeLevelType.branch_halloween
            || levelType == MergeLevelType.branch_SpurLine4
            || levelType == MergeLevelType.branch_SpurLine5
            || levelType == MergeLevelType.branch_SpurLine6;
    }
    public void InitManager()
    {
        //取所有的关卡信息
        totalMapDataDict = new Dictionary<string, MergeMapData>();
        foreach (var item in MapDefinition.TotalDefinitionsDict)
        {
            MergeMapData mapData = new MergeMapData(item.Value.LevelName, item.Value.gridDataDict, item.Value.itemDataDict);
            if (string.IsNullOrEmpty(mapData.LevelName))
            {
                GameDebug.LogError("levelName is null");
            }
            mapData.LoadLevelData();
            mapData.LoadLevelStorePackData();
            mapData.LoadBubbleData();
            totalMapDataDict.Add(mapData.LevelName, mapData);
        }
    }

#if UNITY_EDITOR

    public void Debug_AddMapData(MapDefinition def, bool restart)
    {
        if (def == null)
            return;
        MergeMapData mapData = new MergeMapData(def.LevelName, def.gridDataDict, def.itemDataDict);
        if (restart)
            SaveUtils.DeleteKey(Consts.SaveKey_LevelData_Prefix + mapData.LevelName);
        mapData.LoadLevelData();
        mapData.LoadLevelStorePackData();
        mapData.LoadBubbleData();
        totalMapDataDict[mapData.LevelName] = mapData;
    }

#endif


    public void TryNewDiscoveryItemByEnterNewDungeon()
    {
        string dungeonName = GetLevelNameByLevelType(CurrentLevelType);
        string saveStr = SaveUtils.GetString(Consts.SaveKey_LevelData_Prefix + dungeonName);
        if (string.IsNullOrEmpty(saveStr) && MapDefinition.TotalDefinitionsDict.TryGetValue(dungeonName, out MapDefinition mapDefinition))
        {
            AnalyticsUtil.TrackEvent("event_unlock_" + dungeonName);
            foreach (var item1 in mapDefinition.itemDataDict.Values)
            {
                if (!item1.IsInBox && !item1.IsInBubble && !item1.IsLocked)
                {
                    GameManager.Instance.TryNewDiscoveryItem(item1.PrefabName);
                }
            }
            return;
        }
    }
    public void TryDeleteMapDataByDungeonType(MergeLevelType type)
    {
        string dungeonName = GetLevelNameByLevelType(type);
        if (dungeonName == MapName_Main)
        {
            GameDebug.LogError(dungeonName + "map can not be clear!!!");
            return;
        }
        if (totalMapDataDict.TryGetValue(dungeonName, out MergeMapData mergeMapData) && MapDefinition.TotalDefinitionsDict.TryGetValue(dungeonName, out MapDefinition mapDefinition))
        {
            mergeMapData.TryDeleteMapData();
            MergeMapData data = new MergeMapData(mapDefinition.LevelName, mapDefinition.gridDataDict, mapDefinition.itemDataDict);
            totalMapDataDict[dungeonName] = data;
        }
    }

    /// <summary>
    /// 通过关卡类型切换关卡
    /// </summary>
    /// <param name="type">关卡类型</param>
    /// <returns></returns>
    public bool ChangeMergeLevelByDungeonType(MergeLevelType type)
    {
        var levelName = GetLevelNameByLevelType(type);
        if (totalMapDataDict != null && totalMapDataDict.TryGetValue(levelName, out var mapData))
        {
            if (mapData != null)
            {
                // 先退出当前关卡
                CloseMapData();

                CurrentMapData = mapData;
                CurrentLevelType = type;
                return true;
            }
        }

        return false;
    }

    public void ShowMergePanelByDungeonType(MergeLevelType type)
    {
        if (ChangeMergeLevelByDungeonType(type))
        {
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_Merge, UIShowLayer.Normal));
        }
        UISystem.Instance.uiMainMenu.SetMainViewSpineOnOrOff(false);
    }

    public string GetLevelNameByLevelType(MergeLevelType levelType)
    {
        string levelName;
        switch (levelType)
        {
            case MergeLevelType.mainLine:
                levelName = MapName_Main;
                break;
            case MergeLevelType.dungeon1:
                levelName = MapName_Dungeon1;
                break;
            case MergeLevelType.dungeon2:
                levelName = MapName_Dungeon2;
                break;
            case MergeLevelType.dungeon3:
                levelName = MapName_Dungeon3;
                break;
            case MergeLevelType.dungeon4:
                levelName = MapName_Dungeon4;
                break;
            case MergeLevelType.dungeon5:
                levelName = MapName_Dungeon5;
                break;
            case MergeLevelType.branch1:
                levelName = MapName_Branch1;
                break;
            case MergeLevelType.halloween:
                levelName = MapName_Halloween;
                break;
            case MergeLevelType.christmas:
                levelName = MapName_Christmas;
                break;
            case MergeLevelType.branch_christmas:
                levelName = MapName_BranchChristmas;
                break;
            case MergeLevelType.branch_SpurLine4:
                levelName = MapName_Branch4;
                break;
            case MergeLevelType.branch_SpurLine5:
                levelName = MapName_Branch5;
                break;
            case MergeLevelType.branch_SpurLine6:
                levelName = MapName_Branch6;
                break;
            case MergeLevelType.branch_halloween:
                levelName = MapName_BranchHalloween;
                break;
            case MergeLevelType.lover:
                levelName = MapName_Lover;
                break;
            case MergeLevelType.daily1:
                levelName = MapName_Daily1;
                break;
            case MergeLevelType.daily2:
                levelName = MapName_Daily2;
                break;
            case MergeLevelType.daily3:
                levelName = MapName_Daily3;
                break;
            case MergeLevelType.daily4:
                levelName = MapName_Daily4;
                break;
            case MergeLevelType.daily5:
                levelName = MapName_Daily5;
                break;
            case MergeLevelType.daily6:
                levelName = MapName_Daily6;
                break;
#if UNITY_EDITOR
            case MergeLevelType.DebugMap_FreeGrid:
                {
                    levelName = MapEditSystem.Instance.MapKey;
                }
                break;
#endif
            default:
                levelName = MapName_Main;
                break;
        }
        return levelName;
    }
    private MergeLevelType GetLevelTypeByName(string levelName)
    {
        MergeLevelType levelType;
        switch (levelName)
        {
            case "MergeMapConfig":
                levelType = MergeLevelType.mainLine;
                break;
            case "MergeDungeon1Config":
                levelType = MergeLevelType.dungeon1;
                break;
            case "MergeDungeon2Config":
                levelType = MergeLevelType.dungeon2;
                break;
            case "MergeDungeon3Config":
                levelType = MergeLevelType.dungeon3;
                break;
            case "MergeDungeon4Config":
                levelType = MergeLevelType.dungeon4;
                break;
            case "MergeDungeon5Config":
                levelType = MergeLevelType.dungeon5;
                break;
            case "MergeSpurLine1Config":
                levelType = MergeLevelType.branch1;
                break;
            case "MergeFestival1Config":
                levelType = MergeLevelType.halloween;
                break;
            case "MergeFestival2Config":
                levelType = MergeLevelType.christmas;
                break;
            case "MergeFestival3Config":
                levelType = MergeLevelType.lover;
                break;
            case "MergeDailyEvent1Config":
                levelType = MergeLevelType.daily1;
                break;
            case "MergeDailyEvent2Config":
                levelType = MergeLevelType.daily2;
                break;
            case "MergeDailyEvent3Config":
                levelType = MergeLevelType.daily3;
                break;
            case "MergeDailyEvent4Config":
                levelType = MergeLevelType.daily4;
                break;
            case "MergeDailyEvent5Config":
                levelType = MergeLevelType.daily5;
                break;
            case "MergeDailyEvent6Config":
                levelType = MergeLevelType.daily6;
                break;
            default:
                levelType = MergeLevelType.none;
                break;
        }
        return levelType;
    }

    public void CloseMapData()
    {
        if (CurrentMapData != null)
        {
            CurrentMapData.SaveLevelData();
            CurrentMapData.SaveLevelStorePackData();
            CurrentMapData.SaveBubbleData();
            CurrentMapData = null;
            CurrentLevelType = MergeLevelType.none;

            try
            {
                SDKHelper.Instance.TrackEngagementTime();
            }
            catch
            {
            }
        }
    }

    /// <summary>
    /// 返回合成界面(包括背包)某个物体数量
    /// </summary>
    /// <param name="prefabNmae"></param>
    /// <returns></returns>
    public int ReturnItemNumByPrefabName(string prefabNmae, MergeLevelType levelType)
    {
        var levelName = GetLevelNameByLevelType(levelType);
        if (totalMapDataDict.TryGetValue(levelName, out MergeMapData mergeMapData))
        {
            int hasItemNumInMap = 0;
            foreach (var item in mergeMapData.itemDataDict.Values)
            {
                if (!item.IsInBox && !item.IsInBubble && !item.IsLocked && item.PrefabName == prefabNmae)
                {
                    hasItemNumInMap++;
                }
            }
            for (int i = 0; i < mergeMapData.storePackList.Count; i++)
            {
                MergeItemData data = mergeMapData.storePackList[i];
                if (data.PrefabName == prefabNmae)
                {
                    hasItemNumInMap++;
                }
            }
            return hasItemNumInMap;
        }
        return 0;
    }

    /// <summary>
    /// 删除相应地图(包括背包)的部分元素数据
    /// </summary>
    /// <param name="dungeonType"></param>
    /// <param name="removeDataList"></param>
    public void RemoveDateFromMap(MergeLevelType levelType, List<MergeRewardItem> removeDataList, bool needUnspawnItem = false)
    {
        if (removeDataList == null)
            return;

        var levelName = GetLevelNameByLevelType(levelType);
        if (totalMapDataDict.TryGetValue(levelName, out MergeMapData mergeMapData))
        {
            foreach (var item in removeDataList)
            {
                var name = item.name;
                int count = item.num;
                while (count > 0)
                {
                    if (mergeMapData.DeleteItemInMapAndStore(name, needUnspawnItem))
                        MergeActionSystem.OnMergeActionEvent(MergeActionType.TaskPrefabComplete, name);
                    else
                        Debug.LogError($"MergeLevelManager_RemoveDateFromMapOrBag_Error,prefabName:{name}");
                    count--;
                }
            }
        }
    }



    #region 云存档
    //地图数据量太大，云存档分开存储（每8个地图一组）
    public Dictionary<string, object> GetSaveDataToFirestore(int mapGroup)
    {
        try
        {
            Dictionary<string, object> allMapDic = new Dictionary<string, object>();
            foreach (var item in totalMapDataDict)
            {
                if (Math.Ceiling((float)GetLevelTypeByName(item.Key) / 8) == mapGroup)
                {
                    Dictionary<string, List<object>> oneMapDataDic = new Dictionary<string, List<object>>();
                    List<object> mapDataList = new List<object>();
                    List<object> storeDataList = new List<object>();
                    oneMapDataDic.Add("mapData", mapDataList);
                    oneMapDataDic.Add("storeData", storeDataList);
                    foreach (var item1 in item.Value.itemDataDict)
                    {
                        var json = MergeItemData.DataToJson(item1.Value);
                        mapDataList.Add(json);
                    }
                    for (int i = 0; i < item.Value.storePackList.Count; i++)
                    {
                        var json = MergeItemData.DataToJson(item.Value.storePackList[i]);
                        storeDataList.Add(json);
                    }
                    allMapDic.Add(item.Key, oneMapDataDic);
                }
            }
            return allMapDic;
        }
        catch (Exception e)
        {
            GameDebug.LogError(e);
            return new Dictionary<string, object>();
        }
    }
    public void SetSaveDataFromFirestore(Dictionary<string, object> dic)
    {
        if (dic == null)
            return;
        foreach (var item in dic)
        {
            string resultStr = JsonConvert.SerializeObject(item.Value);
            Dictionary<string, object> ResultDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(resultStr);
            if (totalMapDataDict.TryGetValue(item.Key, out MergeMapData mergeMapData))
            {
                if (ResultDic.TryGetValue("mapData", out object mapDataObj))
                {
                    List<Dictionary<string, object>> list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(mapDataObj.ToString());
                    mergeMapData.ListToItemDatas(list);
                }
                if (ResultDic.TryGetValue("storeData", out object storeDataObj))
                {
                    List<Dictionary<string, object>> list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(storeDataObj.ToString());
                    mergeMapData.ListToLevelStorePackData(list);
                }
            }
        }
    }
    #endregion

    /// <summary>
    /// 所有容器充能结束时间
    /// </summary>
    /// <returns></returns>
    public int ReturnCurrentChargeOverTime()
    {
        if (totalMapDataDict == null)
        {
            return 0;//防止loading没结束时走推送逻辑
        }
        if (totalMapDataDict.TryGetValue(MapName_Main, out MergeMapData mergeMapData))
        {
            DateTimeOffset minTime = DateTimeOffset.MinValue;
            foreach (var item in mergeMapData.itemDataDict.Values)
            {
                if (item.Definition.CategoryType == MergeItemCategoryType.container
                    && item.IsInCD()
                    && item.ChargeFinishDate != DateTimeOffset.MaxValue
                    && item.ChargeFinishDate > minTime)
                {
                    minTime = item.ChargeFinishDate;
                }
            }
            if (minTime == DateTimeOffset.MinValue)
            {
                return 0;
            }
            else
            {
                return (int)(minTime - TimeManager.Instance.UtcNow()).TotalSeconds;
            }
        }
        return 0;
    }
    /// <summary>
    /// 返回主线所有需要时间打开的宝箱最快开启时间
    /// </summary>
    /// <returns></returns>
    public int ReturnCurrentBoxOpenTime()
    {
        if (totalMapDataDict == null)
        {
            return 0;//防止loading没结束时走推送逻辑
        }
        if (totalMapDataDict.TryGetValue(MapName_Main, out MergeMapData mergeMapData))
        {
            DateTimeOffset minTime = DateTimeOffset.MaxValue;
            foreach (var item in mergeMapData.itemDataDict.Values)
            {
                if ((item.Definition.CategoryType == MergeItemCategoryType.finiteContainer || item.Definition.CategoryType == MergeItemCategoryType.taskBox)
                    && item.boxOpenDelayEndTime != DateTimeOffset.MinValue
                    && item.boxOpenDelayEndTime < minTime)
                {
                    minTime = item.boxOpenDelayEndTime;
                }
            }
            if (minTime == DateTimeOffset.MaxValue)
            {
                return 0;
            }
            else
            {
                return (int)(minTime - TimeManager.Instance.UtcNow()).TotalSeconds;
            }
        }
        return 0;
    }

}
