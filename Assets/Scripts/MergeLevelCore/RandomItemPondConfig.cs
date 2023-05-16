using BDUnity;
using BDUnity.Utils;
using ivy.game;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子产出池配表
/// </summary>
public class RandomItemPondConfig
{
    private static string configName = "RandomItemPond";

    public static Dictionary<int, RandomItemPool> randomItemDict = new Dictionary<int, RandomItemPool>();
    private struct PoolStruct
    {
        public int pondID;
        public string itemPrefabName;
        public int itemNum;
        public string SameKind;
    }
    private static PoolStruct LoadPool(Dictionary<string, CustomJSONObject> dataDic)
    {
        PoolStruct poolStruct = new PoolStruct();
        if (dataDic.TryGetValue("Item", out CustomJSONObject prefab_Json))
        {
            poolStruct.itemPrefabName = prefab_Json.ToString();
        }
        if (dataDic.TryGetValue("PondID", out CustomJSONObject PondID_Json))
        {
            if (int.TryParse(PondID_Json.ToString(), out int PondID_num))
            {
                poolStruct.pondID = PondID_num;
            }
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
        if (dataDic.TryGetValue("SameKind", out CustomJSONObject SameKind_Json))
        {
            poolStruct.SameKind = SameKind_Json.ToString();
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
                    RandomItem randomItem = new RandomItem(poolStruct.pondID,
                        poolStruct.itemPrefabName,
                        poolStruct.itemNum,
                        poolStruct.SameKind);
                    if (randomItemDict.TryGetValue(randomItem.pondID, out var poolList))
                    {
                        poolList.TryAddMergeRewardItem(randomItem);
                    }
                    else
                    {
                        poolList = new RandomItemPool(randomItem.pondID);
                        poolList.TryAddMergeRewardItem(randomItem);
                        randomItemDict.Add(randomItem.pondID, poolList);
                    }
                }
            }

        }

    }
}

public class RandomItemPool
{
    public int poolID;
    public List<RandomItem> rewardItemsList = new List<RandomItem>();


    public RandomItemPool(int poolIdOrPrefab)
    {
        this.poolID = poolIdOrPrefab;
    }

    public bool HasMergeRewardItem()
    {
        return rewardItemsList.Count > 0;
    }

    public void TryAddMergeRewardItem(RandomItem item)
    {
        if (item == null)
        {
            return;
        }
        rewardItemsList.Add(item);
    }

    /// <summary>
    /// 获取随机的物品
    /// </summary>
    /// <returns></returns>
    public MergeRewardItem GetRandomRewardItem()
    {
        int random = UnityEngine.Random.Range(0, rewardItemsList.Count);
        int index = 0;
        foreach (var item in rewardItemsList)
        {
            if (index >= random)
            {
                return item.rewardItem;
            }
            index += 1;
        }
        return new MergeRewardItem();
    }
}

public class RandomItem
{
    public int pondID;
    public string itemPrefabName;
    public int itemNum;
    public string SameKind;
    public MergeRewardItem rewardItem;
    public RandomItem(int pondID, string itemPrefabName, int itemNum, string SameKind)
    {
        this.pondID = pondID;
       
        this.itemPrefabName = itemPrefabName;
        this.itemNum = itemNum;
        this.SameKind = SameKind;
        rewardItem = new MergeRewardItem()
        {
            name = itemPrefabName,
            num = itemNum
        };
    }

}