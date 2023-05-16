using BDUnity;
using BDUnity.Utils;
using ivy.game;
using Ivy.Addressable;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子产出池配表
/// </summary>
public class SonPondConfig
{
    private static string configName = "SonPond";

    //public static Dictionary<string, SonPondConfig> sonPondDict = new Dictionary<string, SonPondConfig>();


    public string sonPondId { get; private set; }
    public MergeRewardItemPool SpawnPool { get; private set; } = new MergeRewardItemPool();//产出池

    public static void Init()
    {
        //Dictionary<string, CustomJSONObject> dict = SingletonMono<GameConfig>.Instance.GetConfig(configName).dict;
        //if (dict != null && dict.Count == 1)
        //{
        //    Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
        //    enumerator.MoveNext();
        //    Dictionary<string, CustomJSONObject> dict2 = enumerator.Current.Value.dict;
        //    enumerator = dict2.GetEnumerator();
        //    while (enumerator.MoveNext())
        //    {
        //        Dictionary<string, CustomJSONObject> dict3 = enumerator.Current.Value.dict;

        //        if (dict3.TryGetValue("SonPondID", out CustomJSONObject id_Json))
        //        {
        //            SonPondConfig sonPondConfig = new SonPondConfig();
        //            sonPondConfig.sonPondId = id_Json.ToString();
        //            sonPondConfig.LoadBaseData(dict3, sonPondConfig);
        //            sonPondDict.Add(sonPondConfig.sonPondId, sonPondConfig);
        //        }
        //    }
        //}
        //else
        //{
        //    GameDebug.LogError("MonthPond::Init: Config is null.");
        //}
    }
    private void LoadBaseData(Dictionary<string, CustomJSONObject> dataDic, SonPondConfig config)
    {
        int weightSum = 0;
        int num = (dataDic.Count - 1) / 2;
        for (int i = 1; i <= num; i++)
        {
            string prefabName = string.Empty;
            int weight = -1;
            if (dataDic.TryGetValue("Prefab" + i.ToString(), out CustomJSONObject prefab_Json))
            {
                prefabName = prefab_Json.ToString();

            }
            if (dataDic.TryGetValue("Weight" + i.ToString(), out CustomJSONObject weight_Json))
            {
                int.TryParse(weight_Json.ToString(), out weight);
                weightSum += weight;
            }
            if (!string.IsNullOrEmpty(prefabName) && weight != -1)
            {
                MergeRewardItem itemTemp = new MergeRewardItem()
                {
                    name = prefabName,
                    num = 1
                };
                MergeRewardItemWithWidget rewardWithWeight = new MergeRewardItemWithWidget(itemTemp, weight);
                config.SpawnPool.TryAddMergeRewardItem(rewardWithWeight);

                //GameConfig.CheckMergeRewardItemValid(configName, itemTemp);
                GameConfig.CheckPrefabNameValid(configName, itemTemp.name);
            }
        }
    }
}
