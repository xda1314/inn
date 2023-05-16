// ILSpyBased#2
using BDUnity;
using BDUnity.Utils;
using ivy.game;
using Ivy.Purchase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PayPackDefinition : BillingDataBase
{
    private const string k_DataResourceFileName = "MergeShopPayConfig";
    public static List<PayPackDefinition> Definitions = new List<PayPackDefinition>();
    //<save_ID,PayPackDefinition>
    public static Dictionary<string, PayPackDefinition> DefinitionMap = new Dictionary<string, PayPackDefinition>();

    public string UnityID;
    public List<MergeRewardItem> RewardItems = new List<MergeRewardItem>();

    public string GooglePlayProductID;
    public string AppleProductID;
    public int SubscriptionDays;

    public bool LimitToOne;
    public string iconPrefab;
    public string ParticleOnBuy;

    public string AnalyticsName;
    public string OutlineType;
    public string locKey_Tips;
    public string prePrice;
    public long refreshTime;
    public string sameKind;
    public string tag;
    public string payType;
    public static void Init()
    {
        Dictionary<string, CustomJSONObject> dict = SingletonMono<GameConfig>.Instance.GetConfig(k_DataResourceFileName).dict;
        if (dict != null && dict.Count == 1)
        {
            Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
            enumerator.MoveNext();
            Dictionary<string, CustomJSONObject> dict2 = enumerator.Current.Value.dict;
            enumerator = dict2.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Dictionary<string, CustomJSONObject> dict3 = enumerator.Current.Value.dict;
                PayPackDefinition premiumCurrencyPackDefinition = new PayPackDefinition(dict3);
                if (dict3.ContainsKey("AppleID"))
                {
                    Definitions.Add(premiumCurrencyPackDefinition);
                    DefinitionMap.Add(premiumCurrencyPackDefinition.SaveID, premiumCurrencyPackDefinition);
                }
                else
                {
                    GameDebug.LogError("this data without key: 'Save ID'");
                }
            }
        }
        else
        {
            GameDebug.LogError("MergeShopPayConfig::Init: Config is null.");
        }
    }


    public PayPackDefinition(string saveId,
        string unityId,
        List<MergeRewardItem> rewardItems,
        float localPrice,
        string prePrice = "",
        long refreshTime = 0,
        string sameKind = "") : base()
    {
        SaveID = saveId;
        UnityID = unityId;
        if (int.TryParse(UnityID, out int PayIDTemp))
        {
            PayID = PayIDTemp;
        }
        LimitToOne = false;
        RewardItems = rewardItems;
        Cost = localPrice;
        this.prePrice = prePrice;
        this.refreshTime = refreshTime;
        this.sameKind = sameKind;
        if (DefinitionMap.ContainsKey(SaveID))
            return;
        DefinitionMap.Add(SaveID, this);
    }


    private PayPackDefinition(Dictionary<string, CustomJSONObject> dataDic)
    {
        if (dataDic.TryGetValue("Save ID", out CustomJSONObject saveId_Json))
        {
            SaveID = saveId_Json.ToString();
        }
        if (dataDic.TryGetValue("Unity ID", out CustomJSONObject unityId_Json))
        {
            UnityID = unityId_Json.ToString();
            if (int.TryParse(UnityID, out int PayIDTemp))
            {
                PayID = PayIDTemp;
            }
        }
        if (dataDic.TryGetValue("Default Reward", out CustomJSONObject defaultReward_Json))
        {
            string reward = defaultReward_Json.ToString();
            string[] strs = reward.Split(';');
            for (int i = 0; i < strs.Length; i++)
            {
                string[] array = strs[i].Split(',');
                if (array.Length == 2 && array[0] != null && int.TryParse(array[1], out int num))
                {
                    MergeRewardItem data = new MergeRewardItem();
                    data.name = array[0];
                    data.num = num;
                    RewardItems.Add(data);
                }
            }
        }
        //if (dataDic.TryGetValue("Type", out CustomJSONObject type_Json))
        //{
        //    ProductType = ProductType.Consumable;
        //}
        if (dataDic.TryGetValue("GooglePlayID", out CustomJSONObject googlePlayID_Json))
        {
            GooglePlayProductID = googlePlayID_Json.ToString();
        }
        if (dataDic.TryGetValue("Save", out CustomJSONObject appleID_Json))
        {
            AppleProductID = appleID_Json.ToString();
        }
        if (dataDic.TryGetValue("Subscription Days", out CustomJSONObject subscriptionDay_Json))
        {
            if (int.TryParse(subscriptionDay_Json.ToString(), out int subscriptionDay))
                SubscriptionDays = subscriptionDay;
        }
        if (dataDic.TryGetValue("Limite to One", out CustomJSONObject liniteToOne_Json))
        {
            LimitToOne = !string.IsNullOrEmpty(liniteToOne_Json.ToString());
        }
        if (dataDic.TryGetValue("Icon Prefab", out CustomJSONObject iconPrefab_Json))
        {
            iconPrefab = iconPrefab_Json.ToString();
        }
        if (dataDic.TryGetValue("Particle on Buy", out CustomJSONObject particleOnBuy_Json))
        {
            ParticleOnBuy = particleOnBuy_Json.ToString();
        }
        if (dataDic.TryGetValue("Cost", out CustomJSONObject cost_Json))
        {
            if (float.TryParse(cost_Json.ToString(), out float cost))
            {
                Cost = cost;
            }
        }
        if (dataDic.TryGetValue("Analytics Name", out CustomJSONObject analyticsName_Json))
        {
            AnalyticsName = analyticsName_Json.ToString();
        }
        if (dataDic.TryGetValue("Reward1", out CustomJSONObject reward1_Json))
        {

        }
        if (dataDic.TryGetValue("Reward2", out CustomJSONObject reward2_Json))
        {

        }
        if (dataDic.TryGetValue("Reward3", out CustomJSONObject reward3_Json))
        {

        }
        if (dataDic.TryGetValue("Outline Type", out CustomJSONObject outlineType_Json))
        {
            OutlineType = outlineType_Json.ToString();
        }
        if (dataDic.TryGetValue("Key - Tips", out CustomJSONObject keyTips_Json))
        {
            locKey_Tips = keyTips_Json.ToString();
        }
    }
}

