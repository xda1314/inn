using BDUnity.Utils;
using ivy.game;
using Ivy.Addressable;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 随机池信息
/// </summary>
public class PoolDefinition
{
    private const string configName = "Pond";

    public static List<MergeRewardItemPool> Definitions = new List<MergeRewardItemPool>();
    //<pondID,MergeRewardItemPoolList>
    public static Dictionary<string, MergeRewardItemPool> PoolDefinitionDict = new Dictionary<string, MergeRewardItemPool>();

    private string pondID;
    private string itemPrefabName;
    private int itemNum;
    private int weight;

    public PoolDefinition(string pondID, string itemPrefabName, int itemNum, int weight)
    {
        this.pondID = pondID;
        this.itemPrefabName = itemPrefabName;
        this.itemNum = itemNum;
        this.weight = weight;
    }

    private struct PoolStruct
    {
        public string itemPrefabName;
        public int itemNum;
        public int weight;
    }

    private static PoolStruct LoadPool(Dictionary<string, CustomJSONObject> dataDic)
    {
        PoolStruct poolStruct = new PoolStruct();
        if (dataDic.TryGetValue("Item", out CustomJSONObject prefab_Json))
        {
            poolStruct.itemPrefabName = prefab_Json.ToString();
        }
        if (dataDic.TryGetValue("Num", out CustomJSONObject Num_Json))
        {
            if (Num_Json.ToString() == "")
            {
                poolStruct.itemNum = 0;
            }
            else
            {
                poolStruct.itemNum = Convert.ToInt32(Num_Json.ToString());
            }

        }
        if (dataDic.TryGetValue("Weight", out CustomJSONObject Weight_Json))
        {
            if (Weight_Json.ToString() == "")
            {
                poolStruct.weight = 0;
            }
            else
            {
                poolStruct.weight = Convert.ToInt32(Weight_Json.ToString());
            }
        }
        return poolStruct;
    }

    public static void LoadDefinitions()
    {
        Dictionary<string, CustomJSONObject> dict = SingletonMono<GameConfig>.Instance.GetConfig(configName).dict;
        if (dict != null && dict.Count == 1)
        {
            Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
            enumerator.MoveNext();
            Dictionary<string, CustomJSONObject> dict2 = enumerator.Current.Value.dict;
            enumerator = dict2.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Dictionary<string, CustomJSONObject> dict3 = enumerator.Current.Value.dict;
                if (dict3.TryGetValue("PondID", out CustomJSONObject id_Json))
                {
                    string PondID = id_Json.ToString();

                    PoolStruct poolStruct = LoadPool(dict3);
                    if (poolStruct.weight == 0 || poolStruct.itemNum == 0)
                    {
                        continue;
                    }
                    PoolDefinition def = new PoolDefinition(PondID, poolStruct.itemPrefabName, poolStruct.itemNum, poolStruct.weight);
                    if (def == null)
                    {
                        continue;
                    }

                    GameConfig.CheckPrefabNameValid(configName, poolStruct.itemPrefabName);

                    MergeRewardItem mergeRewardItem = new MergeRewardItem()
                    {
                        name = def.itemPrefabName,
                        num = def.itemNum
                    };
                    if (PoolDefinitionDict.TryGetValue(def.pondID, out var poolList))
                    {
                        poolList.TryAddMergeRewardItem(new MergeRewardItemWithWidget(mergeRewardItem, def.weight));
                    }
                    else
                    {
                        poolList = new MergeRewardItemPool(def.pondID);
                        poolList.TryAddMergeRewardItem(new MergeRewardItemWithWidget(mergeRewardItem, def.weight));
                        PoolDefinitionDict.Add(def.pondID, poolList);
                    }
                }
            }

        }

    }







}

/// <summary>
/// 权重物品池
/// </summary>
public class MergeRewardItemPool
{
    public string poolID;
    public List<MergeRewardItemWithWidget> rewardItemsList = new List<MergeRewardItemWithWidget>();
    public int totalWeight;


    public MergeRewardItemPool(string poolIdOrPrefab)
    {
        this.poolID = poolIdOrPrefab;
        totalWeight = 0;
    }
    public MergeRewardItemPool()
    {
        totalWeight = 0;
    }

    public bool HasMergeRewardItem()
    {
        return rewardItemsList.Count > 0;
    }

    public void TryAddMergeRewardItem(MergeRewardItemWithWidget item)
    {
        if (item == null)
        {
            return;
        }
        rewardItemsList.Add(item);
        totalWeight += item.weight;
    }

    /// <summary>
    /// 获取随机的物品
    /// </summary>
    /// <returns></returns>
    public MergeRewardItem GetRandomRewardItem()
    {
        if (rewardItemsList.Count <= 0)
        {
            Debug.LogError("prefabList is empty");
            return new MergeRewardItem();
        }
        if (rewardItemsList.Count == 1)
            return rewardItemsList[0].rewardItem;
        int random = UnityEngine.Random.Range(1, totalWeight + 1);
        int index = 0;
        foreach (var item in rewardItemsList)
        {
            index += item.weight;
            if (index >= random)
            {
                return item.rewardItem;
            }
        }

        Debug.LogError("out of weight");
        return new MergeRewardItem();
    }


    public static MergeRewardItem FindItemFromPool(string pondID, MergeItemData mergeData)//string pondId, int checkCount = 0)
    {
        //配合教学，特殊处理
        if (IvyCore.UI_TutorManager.Instance.IsTutoring() && mergeData != null
            && mergeData.PrefabName.Equals("Blender_4") && mergeData.poolSpawnCount < 3)
        {
            List<string> tutorItems = new List<string>() { "Desserts_1", "Desserts_1", "Candy_1" };
            var reward = new MergeRewardItem();
            reward.name = tutorItems[mergeData.poolSpawnCount];
            reward.num = 1;
            mergeData.AddPoolSpawnCount();
            return reward;
        }

        if (!pondID.IsNullOrEmpty() && !pondID.StartsWith("Loot_"))
        {
            var temp = new MergeRewardItem();
            temp.name = pondID;
            temp.num = 1;
            return temp;
        }

        int max = 50;
        var currentPond = pondID;
        while (true)
        {
            if (max < 0)
            {
                Ivy.LogUtils.LogError("池子配表存在异常情况!" + pondID);
                break;
            }
            max--;
            if (LootTable.LootTableDic.TryGetValue(currentPond, out var lootTable))
            {
                var reward = lootTable.pool.GetRandomRewardItem();
                if (reward.IsValidReward())
                {
                    if (reward.name.StartsWith("Loot_"))
                    {
                        // 继续抽
                        currentPond = reward.name;
                    }
                    else
                    {
                        mergeData.ResetPoolSpawnCount();
                        return reward;
                    }
                }
            }
            else
            {
                break;
            }
        }

        Ivy.LogUtils.LogError("[LootTable] lootTable里面没有找到池子：" + pondID);
        return new MergeRewardItem();
    }

    public static string FindSwallowPrefabFromPool(string pondId, MergeItemData data)
    {
        if (!pondId.IsNullOrEmpty() && !pondId.StartsWith("Loot_"))
        {
            return pondId;
        }

        int max = 50;
        var currentPond = pondId;
        while (true)
        {
            if (max < 0) break;
            max--;
            if (LootTable.LootTableDic.TryGetValue(currentPond, out var lootTable))
            {
                var reward = lootTable.pool.GetRandomRewardItem();
                if (reward.IsValidReward())
                {
                    if (reward.name.StartsWith("Loot_"))
                    {
                        // 继续抽
                        currentPond = reward.name;
                    }
                    else
                    {
                        data.ResetSwallowCount();
                        return reward.name;
                    }
                }
            }
            else
            {
                break;
            }
        }

        Ivy.LogUtils.LogError("[LootTable] lootTable里面没有找到池子：" + pondId);

        return new MergeRewardItem().name;
    }
}

/// <summary>
/// 带权重的奖励
/// </summary>
public class MergeRewardItemWithWidget
{
    public MergeRewardItem rewardItem;
    public int weight;

    public MergeRewardItemWithWidget(MergeRewardItem rewardItem, int widget)
    {
        this.rewardItem = rewardItem;
        this.weight = widget;
    }

}
