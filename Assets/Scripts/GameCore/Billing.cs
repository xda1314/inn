using System;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using Ivy;
using Ivy.Purchase;
using Ivy.Firebase;

public class Billing : BillingSystemBase<Billing>
{
    public override void InitSystem()
    {
        base.InitSystem();
        InitializeInstance();
    }

    private void InitializeInstance()
    {
        if (PayPackDefinition.Definitions != null && PayPackDefinition.Definitions.Count > 0)
        {
            List<(BillingDataBase, Action<BillingPriceInfo>)> pays = new List<(BillingDataBase, Action<BillingPriceInfo>)>();
            foreach (var item in PayPackDefinition.Definitions)
            {
                (PayPackDefinition, Action<BillingPriceInfo>) tuple = (item, null);
                pays.Add(tuple);
            }
            Billing.SearchPriceInfoAsync_List(pays);
        }
    }


    private Vector3 fly_pos;
    public void TryMakePurchase(PayPackDefinition premiumPack, Vector3 pos, Action<PayPackDefinition> successCB = null)
    {
        try
        {
            GameManager.Instance.SavePlayerData();

            fly_pos = pos;
            MakePurchase(premiumPack, (BillingDataBase dataBase) =>
            {
                PayPackDefinition payPackDefinition = dataBase as PayPackDefinition;
                if (payPackDefinition != null)
                    successCB?.Invoke(payPackDefinition);
            });
            AnalyticsUtil.TrackEvent("click_iap_checkout", premiumPack.SaveID, premiumPack.Cost.ToString(), 0);
        }
        catch (Exception ex)
        {
            LogUtils.LogErrorToSDK(ex + "TryMakePurchase() failed in Billing.cs when trying to purchase pack: " + premiumPack.SaveID + "; " + ex.Message);
        }

    }


    public override void GiveNormalReward(BillingDataBase billingData)
    {
        if (billingData == null)
            return;
        PayPackDefinition payPackDefinition = billingData as PayPackDefinition;
        if (payPackDefinition == null)
            return;
        postProcessPurchase(payPackDefinition);
    }

    private void postProcessPurchase(PayPackDefinition premiumCurrencyPackDefinition)
    {
        try
        {
            if (AudioManager.audioClipSO != null && AudioManager.Instance != null)
            {
                ////AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.paySuccess);
            }


            //发放奖励
            if (GameManager.Instance != null)
            {
                MergeLevelType mergeLevelType;
                if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.none)
                    mergeLevelType = MergeLevelType.mainLine;
                else
                    mergeLevelType = MergeLevelManager.Instance.CurrentLevelType;
                AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Shop_Buy);
                if(int.TryParse(premiumCurrencyPackDefinition.UnityID,out int unity_ID)&& unity_ID>12)
                    GameManager.Instance.GiveRewardItem(premiumCurrencyPackDefinition.RewardItems, "Billing", mergeLevelType);
                else
                    GameManager.Instance.GiveRewardItem(premiumCurrencyPackDefinition.RewardItems, "Billing", fly_pos, mergeLevelType);
            }

            //购买后存储ad广告时间
            if (premiumCurrencyPackDefinition.SaveID.Equals("skipAd")) 
            {
                PlayerData.BuyADTime = TimeManager.Instance.UtcNow();
                GameManager.Instance.SavePlayerData_ReceivebuyADTime();
            }

            if (premiumCurrencyPackDefinition.SaveID.Equals("monthlyCard") || premiumCurrencyPackDefinition.SaveID.Equals("card_Weekly"))
            {
                DateTimeOffset timeDate = TimeManager.ServerUtcNow();
                PlayerData.BuyMonthlyCardTime = timeDate;
                PlayerData.ReceiveRewardsTime = timeDate;

                GameManager.Instance.SavePlayerData_BuyMonthlyCardTime();
                GameManager.Instance.SavePlayerData_ReceiveMonthlyCardRewardTime();
            }

            if (premiumCurrencyPackDefinition.SaveID.Equals("skipAd"))
            {
                DateTimeOffset timeDate = TimeManager.ServerUtcNow();
                PlayerData.BuyADTime = timeDate;
                GameManager.Instance.SavePlayerData_ReceivebuyADTime();
            }

            GameManager.Instance.playerData.Pay_Orders++;
            GameManager.Instance.playerData.Pay_Totals += premiumCurrencyPackDefinition.Cost;

            RiseSdk.Instance.SetUserProperty("paid_total", GameManager.Instance.playerData.Pay_Totals.ToString());
            RiseSdk.Instance.SetUserProperty("avg_order_price", (GameManager.Instance.playerData.Pay_Totals / GameManager.Instance.playerData.Pay_Orders).ToString());
            RiseSdk.Instance.SetUserProperty("last_pay_days", TimeManager.Instance.UtcNow().ToString());
            RiseSdk.Instance.SetUserProperty("total_pay_num", GameManager.Instance.playerData.Pay_Orders.ToString());

            //sync firestore
            GameManager.Instance.SavePlayerDataAndUploadCloud();
        }
        catch (Exception e)
        {
            Debug.LogError("pay_exception" + e);
        }
        finally
        {

        }
    }

    protected override string GetPayLoadData(BillingDataBase pack)
    {
        try
        {
            PayPackDefinition premiumPack = pack as PayPackDefinition;
            Dictionary<string, object> jsonData = new Dictionary<string, object>();
            jsonData.Add("saveid", premiumPack.SaveID);
            jsonData.Add("payid", premiumPack.UnityID);
            jsonData.Add("cost", premiumPack.Cost);
            if (premiumPack.RewardItems != null && premiumPack.RewardItems.Count > 0)
            {
                jsonData.Add("rewarditems", premiumPack.RewardItems);
            }
            string payloadData = Newtonsoft.Json.JsonConvert.SerializeObject(jsonData);
            GameDebug.Log("SerializeObject payloadData:" + payloadData);
            return payloadData;
        }
        catch (Exception e)
        {
            GameDebug.Log("SerializeObject payloadData error:" + e);
            return string.Empty;
        }
    }

    protected override bool ParsePayLoadData(int payID, string payData, out BillingDataBase billingData)
    {
        try
        {
            GameDebug.Log("ParsePayLoadData:" + payData);
            if (string.IsNullOrEmpty(payData))
            {
                billingData = null;
                return false;
            }

            List<MergeRewardItem> rewardItems = new List<MergeRewardItem>();
            string saveID = string.Empty;
            float cost = 0;
            Dictionary<string, object> jsonDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(payData);
            if (jsonDict != null)
            {
                if (jsonDict.TryGetValue("saveid", out object saveidObj))
                {
                    saveID = saveidObj.ToString();
                }
                if (jsonDict.TryGetValue("cost", out object costObj))
                {
                    float.TryParse(costObj.ToString(), out cost);
                }
                if (jsonDict.TryGetValue("rewarditems", out object giftRewardObj))
                {
                    List<MergeRewardItem> tempGift = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MergeRewardItem>>(giftRewardObj.ToString());
                    if (tempGift != null && tempGift.Count > 0)
                    {
                        rewardItems = tempGift;
                    }
                }
                PayPackDefinition billingDataTemp = new PayPackDefinition(saveID, payData.ToString(), rewardItems, cost);
                if (PayPackDefinition.DefinitionMap.TryGetValue(saveID, out var pack))
                {
                    pack.RewardItems = rewardItems;
                    billingData = pack;
                }
                else
                {
                    billingData = billingDataTemp;
                }
                return true;
            }

            billingData = null;
            return false;
        }
        catch (Exception e)
        {
            GameDebug.Log("ParsePayLoadData error:" + e);
            billingData = null;
            return false;
        }
    }
}
