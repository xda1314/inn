using BDUnity.Utils;
using ivy.game;
using System;
using System.Collections.Generic;
using UnityEngine;



public class LootTable
{

    public string id;
    public MergeRewardItemPool pool;


    public readonly static Dictionary<string, LootTable> LootTableDic = new Dictionary<string, LootTable>();

    public static void Init()
    {
        Dictionary<string, CustomJSONObject> dict = SingletonMono<GameConfig>.Instance.GetConfig("LootTable").dict;
        if (dict != null && dict.Count == 1)
        {
            Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
            enumerator.MoveNext();
            Dictionary<string, CustomJSONObject> dict2 = enumerator.Current.Value.dict;
            enumerator = dict2.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Dictionary<string, CustomJSONObject> dict3 = enumerator.Current.Value.dict;
                if (!dict3.TryGetValue("Loot_Table_Name", out CustomJSONObject id_Json))
                {
                    continue;
                }

                string id = id_Json.ToString();
                LootTable lootTable = new LootTable();
                lootTable.id = id;
                lootTable.pool = new MergeRewardItemPool(lootTable.id);
                for (int i = 1; i <= 12; i++)
                {
                    string itemKey = "Item_" + i;
                    string chanceKey = "Chance_" + i;
                    if (dict3.TryGetValue(itemKey, out CustomJSONObject itemKey_Json)
                        && itemKey_Json != null
                        && !string.IsNullOrEmpty(itemKey_Json.ToString())
                        && dict3.TryGetValue(chanceKey, out CustomJSONObject chanceKey_Json)
                        && chanceKey_Json != null
                        && !string.IsNullOrEmpty(chanceKey_Json.ToString()))
                    {
                        if (!string.IsNullOrEmpty(itemKey_Json.ToString()) && int.TryParse(chanceKey_Json.ToString(), out var chance))
                        {
                            MergeRewardItemWithWidget rewardItem = new MergeRewardItemWithWidget(new MergeRewardItem
                            {
                                name = itemKey_Json.ToString(),
                                num = 1
                            }, chance);
                            lootTable.pool.TryAddMergeRewardItem(rewardItem);
                        }
                        else
                        {
                            Ivy.LogUtils.LogError($"[LootTable] 解析失败！Loot_Table_Name：{id}; itemKey:{itemKey_Json}; Chance:{chanceKey_Json}");
                        }
                    }

                }
                LootTableDic.Add(id, lootTable);
            }

        }
    }




}
