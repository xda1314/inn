using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MergeMapData
{
    /// <summary>
    /// 关卡名称
    /// </summary>
    public string LevelName;



    // 当前关卡地图信息 {格子坐标，格子}
    public Dictionary<Vector2Int, MergeGridData> gridDataDict { get; private set; } = new Dictionary<Vector2Int, MergeGridData>();
    // 当前关卡物体信息 {格子坐标，物体}
    public Dictionary<Vector2Int, MergeItemData> itemDataDict { get; private set; } = new Dictionary<Vector2Int, MergeItemData>();
    // 当前关卡泡泡信息
    public List<MergeItemData> bubbleDataList { get; private set; } = new List<MergeItemData>();

    // 底部背包中的数据
    public List<MergeItemData> storePackList { get; private set; } = new List<MergeItemData>();


    public MergeMapData(string levelName, Dictionary<Vector2Int, MergeGridData> mapGridData, Dictionary<Vector2Int, MergeItemData> defaultItemData)
    {
        this.LevelName = levelName;
        this.gridDataDict = new Dictionary<Vector2Int, MergeGridData>(mapGridData);
        if (mapGridData == null || mapGridData.Count <= 0)
        {
            Debug.LogError("当前地图数据为空！");
            gridDataDict = new Dictionary<Vector2Int, MergeGridData>();
        }
        this.itemDataDict = new Dictionary<Vector2Int, MergeItemData>(defaultItemData);
    }

    public void ResetData(string levelName, Dictionary<Vector2Int, MergeGridData> mapGridData, Dictionary<Vector2Int, MergeItemData> defaultItemData)
    {
        this.LevelName = levelName;
        this.gridDataDict = new Dictionary<Vector2Int, MergeGridData>(mapGridData);
        if (mapGridData == null || mapGridData.Count <= 0)
        {
            Debug.LogError("当前地图数据为空！");
            gridDataDict = new Dictionary<Vector2Int, MergeGridData>();
        }
        this.itemDataDict = new Dictionary<Vector2Int, MergeItemData>(defaultItemData);
    }

    /// <summary>
    /// 重置副本时删除存档数据
    /// </summary>
    public void TryDeleteMapData()
    {
        if (SaveUtils.HasKey(Consts.SaveKey_LevelData_Prefix + LevelName))
        {
            SaveUtils.DeleteKey(Consts.SaveKey_LevelData_Prefix + LevelName);
        }
    }

    // 读取关卡数据
    public void LoadLevelData()
    {
        if (string.IsNullOrEmpty(LevelName))
        {
            GameDebug.LogError("LevelName is empty");
            return;
        }

        try
        {
            string saveStr = SaveUtils.GetString(Consts.SaveKey_LevelData_Prefix + LevelName);
            if (string.IsNullOrEmpty(saveStr))
            {
                return;
            }
            var list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(saveStr);
            if (list == null)
            {
                return;
            }

            itemDataDict.Clear();
            foreach (var item in list)
            {
                var itemData = MergeItemData.JsonToData(item);
                if (itemData != null)
                {
                    itemDataDict.Add(itemData.GridPos, itemData);
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogError("[LoadSaveData error]" + e);
        }


    }

    // 保存关卡数据
    public void SaveLevelData()
    {
        //保存
        if (itemDataDict.Count <= 0)
        {
            return;
        }

        try
        {
            List<Dictionary<string, object>> dict = new List<Dictionary<string, object>>();
            foreach (var item in itemDataDict)
            {
                var json = MergeItemData.DataToJson(item.Value);
                dict.Add(json);
            }
            string endStr = JsonConvert.SerializeObject(dict);
            SaveUtils.SetString(Consts.SaveKey_LevelData_Prefix + LevelName, endStr);
        }
        catch (Exception e)
        {
            Debug.LogError("[SaveCurrentMergeLevelData error!]" + e);
        }

    }


    public List<Dictionary<string, object>> ItemDatasToList()
    {
        List<Dictionary<string, object>> dict = new List<Dictionary<string, object>>();
        foreach (var item in itemDataDict)
        {
            var json = MergeItemData.DataToJson(item.Value);
            dict.Add(json);
        }
        return dict;
    }

    public void ListToItemDatas(List<Dictionary<string, object>> list)
    {
        if (list == null)
        {
            return;
        }

        itemDataDict.Clear();
        foreach (var item in list)
        {
            var itemData = MergeItemData.JsonToData(item);
            if (itemData != null)
            {
                itemDataDict.Add(itemData.GridPos, itemData);
            }
        }
        SaveLevelData();
    }

    int saveItemCount = 0;
    /// <summary>
    /// 设置格子物体
    /// </summary>
    /// <param name="gridPos"></param>
    /// <param name="item">如果要清空时，传null</param>
    public void ChangeItemData(Vector2Int gridPos, MergeItemData itemData)
    {
        if (gridDataDict == null || !gridDataDict.ContainsKey(gridPos))
        {
            Debug.LogError("地图中没有当前格子!" + gridPos);
            return;
        }

        if (itemData != null)
        {
            itemData.SetData_GridPos(gridPos);
            itemDataDict[gridPos] = itemData;
        }
        else if (itemDataDict.ContainsKey(gridPos))
            itemDataDict.Remove(gridPos);

        MergeActionSystem.OnMergeActionEvent(MergeActionType.MapDataChange);
    }
    public void AddOrRemoveItemData()
    {
        MergeActionSystem.OnMergeActionEvent(MergeActionType.AddOrRemoveItem);
    }
    /// <summary>
    /// 通过名字删除地图和背包中元素数据
    /// </summary>
    public bool DeleteItemInMapAndStore(string prefabName, bool needUnspawnItem)
    {
        foreach (var item in itemDataDict)
        {
            if (!item.Value.IsInBox && !item.Value.IsInBubble && !item.Value.IsLocked && item.Value.PrefabName == prefabName)
            {
                ChangeItemData(item.Key, null);
                AddOrRemoveItemData();
                SaveLevelData();
                if (needUnspawnItem && MergeController.CurrentController != null)
                {
                    if (item.Value == MergeController.CurrentController.currentSelectItemData)
                    {
                        MergeController.CurrentController.SetCurrentSelectData();
                    }
                    AssetSystem.Instance.UnspawnMergeItem(item.Value.ItemGO);
                }
                return true;
            }
        }
        for (int i = 0; i < storePackList.Count; i++)
        {
            if (storePackList[i].PrefabName == prefabName)
            {
                MergeItemData targetItem = storePackList[i];
                storePackList.Remove(targetItem);
                SaveLevelStorePackData();
                if (needUnspawnItem && MergeController.CurrentController != null) 
                {
                    MergeController.CurrentController.RemoveItemFromStore(targetItem);
                }                  
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 减少map中item的CD时间
    /// </summary>
    /// <param name="seconds"></param>
    public void ClearMapItemCD(int seconds)
    {
        foreach (var item in itemDataDict)
        {
            if (!item.Value.IsInBox && !item.Value.IsInBubble && !item.Value.IsLocked)
                item.Value.ClearCD(seconds);
        }

        SaveLevelData();
    }

    #region 背包数据
    /// <summary>
    /// 数据加入底部背包
    /// </summary>
    public void AddToStorePackList(MergeItemData mergeItemData, int index = -1)
    {
        int storeGridCount = StoreManager.Instance.curMaxStoreGridCount;
        if (storePackList.Count < storeGridCount)
        {
            if (index == -1)
            {
                storePackList.Add(mergeItemData);
            }
            else
            {
                storePackList.Insert(index, mergeItemData);
            }
        }
        else
        {

        }
    }

    public void RemoveStorePackList(MergeItemData mergeItemData)
    {
        storePackList.Remove(mergeItemData);
    }
    public int RemoveStorePackListWithCount(MergeItemData mergeItemData)
    {
        List<MergeItemData> l = new List<MergeItemData>();
        foreach (var item in storePackList)
        {
            l.Add(item);
        }
        storePackList.Remove(mergeItemData);
        for (int i = 0; i < storePackList.Count; i++)
        {
            if (l[i] != storePackList[i])
            {
                GameDebug.Log("交换的位置是：" + i);
                return i;
            }
        }
        return -1;
    }

    public void ResetStorePack()
    {
        storePackList.Clear();
    }

    // 读取关卡背包数据
    public void LoadLevelStorePackData()
    {
        try
        {
            storePackList.Clear();
            if (string.IsNullOrEmpty(LevelName))
            {
                GameDebug.LogError("LevelName is empty");
                return;
            }

            if (!SaveUtils.HasKey(Consts.SaveKey_LevelStorePackData_Prefix + LevelName))
            {
                return;
            }

            string saveStr = SaveUtils.GetString(Consts.SaveKey_LevelStorePackData_Prefix + LevelName);
            if (string.IsNullOrEmpty(saveStr))
            {
                return;
            }

            var list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(saveStr);
            if (list == null)
            {
                return;
            }


            foreach (var item in list)
            {
                var itemData = MergeItemData.JsonToData(item);
                if (itemData != null)
                {
                    storePackList.Add(itemData);
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogError("[LoadLevelStorePackData error]" + e);
        }


    }

    // 保存背包数据
    public void SaveLevelStorePackData()
    {
        //保存
        try
        {
            if (storePackList.Count <= 0)
            {
                if (SaveUtils.HasKey(Consts.SaveKey_LevelStorePackData_Prefix + LevelName))
                {
                    SaveUtils.DeleteKey(Consts.SaveKey_LevelStorePackData_Prefix + LevelName);
                }
                return;
            }

            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (var item in storePackList)
            {
                var json = MergeItemData.DataToJson(item);
                list.Add(json);
            }
            string endStr = JsonConvert.SerializeObject(list);
            SaveUtils.SetString(Consts.SaveKey_LevelStorePackData_Prefix + LevelName, endStr);
        }
        catch (Exception e)
        {
            Debug.LogError("[SaveCurrentMergeLevelStaoreData error!]" + e);
        }

    }

    public List<Dictionary<string, object>> LevelStorePackDataToList()
    {
        if (storePackList.Count <= 0)
        {
            return new List<Dictionary<string, object>>();
        }

        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        foreach (var item in storePackList)
        {
            var json = MergeItemData.DataToJson(item);
            list.Add(json);
        }
        return list;
    }

    public void ListToLevelStorePackData(List<Dictionary<string, object>> list)
    {
        if (list == null)
        {
            return;
        }

        storePackList.Clear();
        foreach (var item in list)
        {
            var itemData = MergeItemData.JsonToData(item);
            if (itemData != null)
            {
                storePackList.Add(itemData);
            }
        }
        SaveLevelStorePackData();
    }
    #endregion

    #region 泡泡数据
    public void AddDataToBubbleList(MergeItemData data)
    {
        bubbleDataList.Add(data);
    }
    public void RemoveDataFromBubbleList(MergeItemData data)
    {
        bubbleDataList.Remove(data);
    }

    public void ResetBubbleList()
    {
        bubbleDataList.Clear();
    }

    // 读取泡泡数据
    public void LoadBubbleData()
    {
        try
        {
            bubbleDataList.Clear();
            if (string.IsNullOrEmpty(LevelName))
            {
                GameDebug.LogError("LevelName is empty");
                return;
            }

            if (!SaveUtils.HasKey(Consts.SaveKey_LevelBubbleData_Prefix + LevelName))
                return;

            string saveStr = SaveUtils.GetString(Consts.SaveKey_LevelBubbleData_Prefix + LevelName);
            if (string.IsNullOrEmpty(saveStr))
                return;

            var list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(saveStr);
            if (list == null)
                return;

            foreach (var item in list)
            {
                var itemData = MergeItemData.JsonToData(item);
                if (itemData != null)
                    bubbleDataList.Add(itemData);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("[LoadLevelBubbleData error]" + e);
        }
    }

    //保存泡泡数据
    public void SaveBubbleData()
    {
        try
        {
            if (bubbleDataList.Count <= 0)
            {
                if (SaveUtils.HasKey(Consts.SaveKey_LevelBubbleData_Prefix + LevelName))
                    SaveUtils.DeleteKey(Consts.SaveKey_LevelBubbleData_Prefix + LevelName);
                return;
            }

            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (var item in bubbleDataList)
            {
                var json = MergeItemData.DataToJson(item);
                list.Add(json);
            }
            string endStr = JsonConvert.SerializeObject(list);
            SaveUtils.SetString(Consts.SaveKey_LevelBubbleData_Prefix + LevelName, endStr);
        }
        catch (Exception e)
        {
            Debug.LogError("[SaveCurrentMergeLevelBubbleData error!]" + e);
        }
    }
    #endregion
}
