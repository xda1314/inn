using BDUnity.Utils;
using ivy.game;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 商城信息
/// </summary>
public class ShopPackDefinition
{
    private const string configName = "MergeShopNormal";

    public static List<ShopPackDefinition> Definitions { get; private set; } = new List<ShopPackDefinition>();
    //<saveID,ShopPackDefinition>
    public static Dictionary<string, ShopPackDefinition> DefinitionMap { get; private set; } = new Dictionary<string, ShopPackDefinition>();
    //按照商店分类
    public static Dictionary<ShopTag, List<ShopPackDefinition>> ShopDefinitionDict { get; private set; } = new Dictionary<ShopTag, List<ShopPackDefinition>>();

    public string SaveID { get; private set; }
    public string PrefabName { get; private set; }
    public int PrefabNum { get; private set; }
    public CurrencyID currencyID { get; private set; }
    public int Cost { get; private set; }
    public int CostIncrement { get; private set; }
    public ShopTag shopTag { get; private set; }

    public string Initialization { get; private set; }
    public string ShopType { get; private set; }

    public int purchaseLimit { get; private set; }
    public int order { get; private set; }
    public string pondID { get; private set; }
    public MergeRewardItemPool itemPool { get; private set; }

    public string initItemName { get; private set; }
    public int refreshTime { get; private set; }

    public bool IsRandomPool => !string.IsNullOrEmpty(pondID);

    private int unlock = -1;
    private int close = 99999;
    public bool Isunlock => TaskGoalsManager.Instance.curLevelIndex>= unlock;
    public bool IsClose=> TaskGoalsManager.Instance.curLevelIndex >= close;
    public enum ShopTag
    {
        personal,
        daily,
        discount,
        boxpack,
        dungeon1Func
    }


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

                if (dict3.TryGetValue("Save ID", out CustomJSONObject id_Json))
                {
                    ShopPackDefinition shopPackDefinition = new ShopPackDefinition();
                    shopPackDefinition.SaveID = id_Json.ToString();
                    shopPackDefinition.LoadBaseData(dict3);

                    Definitions.Add(shopPackDefinition);
                    DefinitionMap.Add(shopPackDefinition.SaveID, shopPackDefinition);
                    if (ShopDefinitionDict.TryGetValue(shopPackDefinition.shopTag, out var list))
                    {
                        list.Add(shopPackDefinition);
                    }
                    else
                    {
                        list = new List<ShopPackDefinition>
                        {
                            shopPackDefinition
                        };
                        ShopDefinitionDict.Add(shopPackDefinition.shopTag, list);
                    }
                }
                else
                {
                    GameDebug.LogError("grid data error");
                }
            }

            foreach (var item in ShopDefinitionDict)
            {
                item.Value.Sort((p1, p2) =>
                {
                    return p1.order.CompareTo(p2.order);
                });
            }
        }
        else
        {
            GameDebug.LogError("MergeMapConfig::Init: Config is null.");
        }

        //MergeLevelManager.Instance.InitManager();


        foreach (var item in ShopDefinitionDict)
        {
            item.Value.Sort((p1, p2) =>
            {
                return p1.order.CompareTo(p2.order);
            });
        }
    }
    private void LoadBaseData(Dictionary<string, CustomJSONObject> dataDic)
    {
        if (dataDic.TryGetValue("PrefabName", out CustomJSONObject prefab_Json))
        {
            PrefabName = prefab_Json.ToString();
        }
        if (dataDic.TryGetValue("Num", out CustomJSONObject num_Json))
        {
            if (int.TryParse(num_Json.ToString(), out int num))
            {
                PrefabNum = num;
            }
            else
            {
                PrefabNum = 1;
            }
        }
        else 
        {
            PrefabNum = 1;
        }
        if (dataDic.TryGetValue("Currency Type", out CustomJSONObject currencyType_Json))
        {
            switch (currencyType_Json.ToString())
            {
                case "free":
                    currencyID = CurrencyID.Free;
                    break;
                case "ad":
                    currencyID = CurrencyID.AD;
                    break;
                case "gems":
                    currencyID = CurrencyID.Gems;
                    break;
                case "coins":
                    currencyID = CurrencyID.Coins;
                    break;
                case "":
                case "gemsorcoins":
                    currencyID = CurrencyID.GemsOrCoins;
                    break;
                default:
                    Debug.LogError("商城配表Currency Type无法解析!:[" + currencyType_Json.ToString() + "]");
                    break;
            }
        }
        if (dataDic.TryGetValue("Unlock", out CustomJSONObject unlock_Json))
        {
            if (int.TryParse(unlock_Json.ToString(), out int unlock))
            {
                this.unlock = unlock;
            }
        }

        if (dataDic.TryGetValue("Close", out CustomJSONObject close_Json))
        {
            if (int.TryParse(close_Json.ToString(), out int close))
            {
                this.close = close;
            }
        }
        if (dataDic.TryGetValue("Cost", out CustomJSONObject cost_Json))
        {
            if (int.TryParse(cost_Json.ToString(), out int cost))
            {
                Cost = cost;
            }
        }
        if (dataDic.TryGetValue("Cost Increment", out CustomJSONObject costIncrement_Json))
        {
            if (int.TryParse(costIncrement_Json.ToString(), out int costIncremengt))
            {
                CostIncrement = CostIncrement;
            }
        }
        if (dataDic.TryGetValue("Tag", out CustomJSONObject tag_Json))
        {
            switch (tag_Json.ToString())
            {
                case "daily":
                    shopTag = ShopTag.daily;
                    break;
                case "discount":
                    shopTag = ShopTag.discount;
                    break;
                case "pack":
                    shopTag = ShopTag.boxpack;
                    break;
                default:
                    Debug.LogError("商城配表Tag无法解析!" + tag_Json.ToString());
                    break;
            }
        }
        if (dataDic.TryGetValue("Purchase Limit", out CustomJSONObject purchaseLimit_Json))
        {
            if (int.TryParse(purchaseLimit_Json.ToString(), out int purchaseLimit))
            {
                this.purchaseLimit = purchaseLimit;
            }
        }

        if (dataDic.TryGetValue("Order", out CustomJSONObject order_Json))
        {
            if (int.TryParse(order_Json.ToString(), out int order))
            {
                this.order = order;
            }
        }
        if (dataDic.TryGetValue("Initialization", out CustomJSONObject Initialization_Json))
        {
            this.Initialization = Initialization_Json.ToString();
        }
        if (dataDic.TryGetValue("ShopType", out CustomJSONObject ShopType_Json))
        {
            this.ShopType = ShopType_Json.ToString();
        }

        if (dataDic.TryGetValue("PondID", out CustomJSONObject pomdId_Json))
        {
            pondID = pomdId_Json.ToString();
        }
        if (dataDic.TryGetValue("Initialization", out CustomJSONObject initialization_Json))
        {
            initItemName = initialization_Json.ToString();
        }
        if (dataDic.TryGetValue("RefreshTime", out CustomJSONObject refreshTime_Json))
        {
            if (int.TryParse(refreshTime_Json.ToString(), out int refreshTime))
            {
                this.refreshTime = refreshTime;
            }
        }
        if (!string.IsNullOrEmpty(pondID))
        {
            if (PoolDefinition.PoolDefinitionDict.TryGetValue(pondID, out var pool))
            {
                itemPool = pool;
            }
        }
    }

}
