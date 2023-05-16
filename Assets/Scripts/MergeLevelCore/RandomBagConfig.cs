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
public class RandomBagConfig
{
    private static string configName = "RandomBag";

    public static Dictionary<int, RandomBagItem> randomBagDict = new Dictionary<int, RandomBagItem>();

    public static void Init()
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
                RandomBagItem randomBagItem = new RandomBagItem();
                if (dict3.TryGetValue("ID", out CustomJSONObject id_Json))
                {
                    randomBagItem.LoadBaseData(dict3);
                    randomBagDict.Add(Convert.ToInt32(id_Json.ToString()), randomBagItem);
                }
            }       
        }
        else
        {
            GameDebug.LogError("MonthPond::Init: Config is null.");
        }
    }
   
}
public class RandomBagItem
{
    public int Id { get; private set; }
    public int Rareness { get; private set; }// 获取难度

    public String UnityId { get; private set; }
    public string OriginalPrice { get; private set; }

    public int SellDiamond { get; private set; }

    public float SellDollar { get; private set; }

    public int RandomReward1 { get; private set; }// 奖励1

    public MergeRewardItem RandomReward2 { get; private set; }// 奖励2

    public MergeRewardItem FixedReward { get; private set; }// 奖励3

    public int refreshTime { get; private set; }

    public void LoadBaseData(Dictionary<string, CustomJSONObject> dataDic)
    {
        if (dataDic.TryGetValue("ID", out CustomJSONObject itenId_Json))
        {
            if (int.TryParse(itenId_Json.ToString(), out int itemId))
            {
                Id = itemId;
            }
        }
        if (dataDic.TryGetValue("Rareness", out CustomJSONObject Rareness_Json))
        {
            if (int.TryParse(itenId_Json.ToString(), out int Rareness_num))
            {
                Rareness = Rareness_num;
            }
        }
        if (dataDic.TryGetValue("RandomReward1", out CustomJSONObject RandomReward1_Json))
        {
            if (int.TryParse(RandomReward1_Json.ToString(), out int RandomReward1_num))
            {
                RandomReward1 = RandomReward1_num;
            }
        }

        if (dataDic.TryGetValue("RandomReward2", out CustomJSONObject RandomReward2_Json))
        {
            string reward = RandomReward2_Json.ToString();
            string[] strs = reward.Split(';');
            int index = UnityEngine.Random.Range(0, 2);
            string[] array = strs[index].Split(',');
            if (array.Length == 2 && array[0] != null && int.TryParse(array[1], out int num))
            {
                MergeRewardItem data = new MergeRewardItem();
                data.name = array[0];
                data.num = num;
                RandomReward2 = data;
            }
        }

        if (dataDic.TryGetValue("FixedReward", out CustomJSONObject FixedReward_Json))
        {
            string reward = FixedReward_Json.ToString();
            string[] array = reward.Split(',');
            if (array.Length == 2 && array[0] != null && int.TryParse(array[1], out int num))
            {
                MergeRewardItem data = new MergeRewardItem();
                data.name = array[0];
                data.num = num;
                FixedReward=data;
            }
        }

            if (dataDic.TryGetValue("OriginalPrice", out CustomJSONObject OriginalPrice_Json))
        {
                OriginalPrice = OriginalPrice_Json.ToString();
        }
        if (dataDic.TryGetValue("SellDiamond", out CustomJSONObject SellDiamond_Json))
        {
            if (int.TryParse(SellDiamond_Json.ToString(), out int SellDiamond_num))
            {
                SellDiamond = SellDiamond_num;
            }
        }
        if (dataDic.TryGetValue("RefreshTime", out CustomJSONObject RefreshTime_Json))
        {
            if (int.TryParse(RefreshTime_Json.ToString(), out int RefreshTime_num))
            {
                refreshTime = RefreshTime_num;
            }
        }
        if (dataDic.TryGetValue("SellDollar", out CustomJSONObject SellDollar_Json))
        {
            if (float.TryParse(SellDollar_Json.ToString(), out float SellDollar_num))
            {
                SellDollar = SellDollar_num;
            }
        }
        if (dataDic.TryGetValue("Unity ID", out CustomJSONObject UnityID_Json))
        {
                UnityId = UnityID_Json.ToString();
        }
    }
}