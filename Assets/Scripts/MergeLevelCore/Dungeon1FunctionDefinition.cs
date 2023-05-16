using BDUnity.Utils;
using ivy.game;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 随机池信息
/// </summary>
public class Dungeon1Function
{
    private const string configName = "Dungeon1Function";

    public static List<Dungeon1Function> Definitions { get; private set; } = new List<Dungeon1Function>();


    public string SaveID { get; private set; }
    public string RandomItemPondID { get; private set; }
    public CurrencyID currencyID { get; private set; }

    public int Cost { get; private set; }

    public int purchaseLimit { get; private set; }
    public int refreshTime { get; private set; }
    //目的创建一个数据链表

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

                if (dict3.TryGetValue("ID", out CustomJSONObject id_Json))
                {
                    Dungeon1Function dungeon1Function = new Dungeon1Function();
                    dungeon1Function.SaveID = id_Json.ToString();
                    dungeon1Function.LoadBaseData(dict3);
                    Definitions.Add(dungeon1Function);
                }
            }
        }
    }

    private void LoadBaseData(Dictionary<string, CustomJSONObject> dataDic)
    {
        if (dataDic.TryGetValue("RandomItemPondID", out CustomJSONObject RandomItemPondID_Json))
        {
            RandomItemPondID = RandomItemPondID_Json.ToString();
        }
        if (dataDic.TryGetValue("CostType", out CustomJSONObject currencyType_Json))
        {
            switch (currencyType_Json.ToString())
            {
                case "free":
                    currencyID = CurrencyID.Free;
                    break;
                case "gems":
                    currencyID = CurrencyID.Gems;
                    break;
                case "coins":
                    currencyID = CurrencyID.Coins;
                    break;
                default:
                    break;
            }
        }
        if (dataDic.TryGetValue("Price", out CustomJSONObject cost_Json))
        {
            if (int.TryParse(cost_Json.ToString(), out int cost))
            {
                Cost = cost;
            }
        }
        if (dataDic.TryGetValue("BuyTime", out CustomJSONObject purchaseLimit_Json))
        {
            if (int.TryParse(purchaseLimit_Json.ToString(), out int purchaseLimit))
            {
                this.purchaseLimit = purchaseLimit;
            }
        }
        if (dataDic.TryGetValue("RefreshTime", out CustomJSONObject refreshTime_Json))
        {
            if (int.TryParse(refreshTime_Json.ToString(), out int refreshTime))
            {
                this.refreshTime = refreshTime;
            }
        }
    }
}




