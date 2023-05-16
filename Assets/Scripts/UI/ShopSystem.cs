using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using ivy.game;

public class ShopSystem
{
    public static ShopSystem Instance { get; private set; } = new ShopSystem();

    #region 礼包数据结构
    public List<ShopPackData> dailyPackDataList { get; private set; } = new List<ShopPackData>();
    public List<ShopPackData> discountPackDataList { get; private set; } = new List<ShopPackData>();
    public List<ShopPackData> discountSpurLinePackDataList { get; private set; } = new List<ShopPackData>();
    public List<ShopPackData> discountDungeon2PackDataList { get; private set; } = new List<ShopPackData>();
    public List<ShopPackData> discountDungeon3PackDataList { get; private set; } = new List<ShopPackData>();
    public List<ShopPackData> boxPackDataList { get; private set; } = new List<ShopPackData>();
    public List<ShopPackData> boxPackDungeon1DataList { get; private set; } = new List<ShopPackData>();
    public List<ShopPackData> boxPackDungeon2DataList { get; private set; } = new List<ShopPackData>();
    public List<ShopPackData> boxPackDungeon3DataList { get; private set; } = new List<ShopPackData>();
    public List<PayPackDefinition> gemsPayPackDefList { get; private set; } = new List<PayPackDefinition>();
    public List<PayPackDefinition> coinsPayPackDefList { get; private set; } = new List<PayPackDefinition>();
    #endregion

    public DateTimeOffset lastRefreshDateTime { get; private set; }
    private bool hasLoadData = false;

    public void InitSystem(Action showUICB)
    {
        //TimeManager.Instance.TryExcuteWithServerUtc(() =>
        //{
        //    LoadData();
        //    showUICB?.Invoke();
        //});
        LoadData();
        showUICB?.Invoke();
    }

    private void LoadData()
    {

        hasLoadData = true;
        try
        {
            LoadRefreshTime();
            if (lastRefreshDateTime.Date == DateTimeOffset.UtcNow.Date)
            {
                //加载基础礼包数据
                LoadBaseShopData();
                //加载商城定制礼包数据
                LoadPersonalShopData();
                //加载副本商城数据
                LoadDunoenShopData();
                //刷新
                RefreshPayPack();
            }
            else
            {
                RefreshTime();

                RefreshTodayDailyPack();
                RefreshTodayDiscountPack();
                //RefreshSpurLineDiscountPack();
                RefreshTodayBoxPack();
                RefreshPayPack();
                RefreshDungeonShopData();

                SaveDungeonData();
                SaveDailyData();
                SaveDiscountData();
                SaveBoxPackData();
            }
        }
        catch (Exception e)
        {
            GameDebug.LogError("ShopSystem LoadData error!" + e);
        }
        RefreshPersonalShopData();

    }

    #region 云存档

    private ShopPackData CreateShopPackData(string SaveID, List<String> prefabNames, int todayBuyCount, int purchaseLimit, int cost, CurrencyID currency)
    {
        List<MergeRewardItem> items = new List<MergeRewardItem>();
        foreach (var item in prefabNames)
        {
            MergeRewardItem rewardItem = new MergeRewardItem()
            {
                name = item,
                num = 1
            };
            items.Add(rewardItem);
        }

        ShopPackData shopPackData = new ShopPackData(SaveID, items, purchaseLimit, cost, currency);
        shopPackData.todayBuyCount = todayBuyCount;
        return shopPackData;
    }

    private List<ShopPackData> ListToShopPackDatas(List<Dictionary<string, object>> keyValuePairs)
    {
        List<ShopPackData> shopPackDatas = new List<ShopPackData>();
        foreach (var item in keyValuePairs)
        {
            string saveID = "";
            List<String> prefabNames = new List<String>();
            int todayBuyCount = 0;
            int purchaseLimit = 0;
            int cost = 0;
            CurrencyID currencyID = CurrencyID.NONE;
            if (item.TryGetValue("SaveID", out object saveIDObj))
            {
                saveID = saveIDObj.ToString();
            }
            if (item.TryGetValue("prefabNames", out object prefabNamesObj))
            {
                prefabNames = JsonConvert.DeserializeObject<List<String>>(prefabNamesObj.ToString());
            }
            if (item.TryGetValue("todayBuyCount", out object todayBuyCountObj))
            {
                todayBuyCount = int.Parse(todayBuyCountObj.ToString());
            }
            if (item.TryGetValue("purchaseLimit", out object purchaseLimitObj))
            {
                purchaseLimit = int.Parse(purchaseLimitObj.ToString());
            }
            if (item.TryGetValue("cost", out object costObj))
            {
                cost = int.Parse(costObj.ToString());
            }
            if (item.TryGetValue("currency", out object currencyIDObj))
            {
                Enum.TryParse<CurrencyID>(currencyIDObj.ToString(), out currencyID);
            }
            shopPackDatas.Add(CreateShopPackData(saveID, prefabNames, todayBuyCount, purchaseLimit, cost, currencyID));
        }
        return shopPackDatas;
    }

    private List<Dictionary<string, object>> ShopPackDatasToList(List<ShopPackData> shopPackDatas)
    {
        List<Dictionary<string, object>> dailyPackDataList = new List<Dictionary<string, object>>();
        foreach (var item in shopPackDatas)
        {
            Dictionary<string, object> ShopPackDict = new Dictionary<string, object>();
            List<String> prefabNames = new List<String>();
            foreach (var v in item.rewardItem)
                prefabNames.Add(v.name);
            ShopPackDict.Add("SaveID", item.saveID);
            ShopPackDict.Add("prefabNames", prefabNames);
            ShopPackDict.Add("todayBuyCount", item.todayBuyCount);
            ShopPackDict.Add("purchaseLimit", item.countLimit);
            ShopPackDict.Add("cost", item.cost);
            ShopPackDict.Add("currency", item.currencyID.ToString());
            dailyPackDataList.Add(ShopPackDict);
        }
        return dailyPackDataList;
    }

    public Dictionary<string, object> GetSaveDataToFirestore()
    {
        try
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("lastRefreshDateTime", lastRefreshDateTime.ToString());
            List<Dictionary<string, object>> dailyPackDataList = ShopPackDatasToList(this.dailyPackDataList);
            List<Dictionary<string, object>> discountPackDataList = ShopPackDatasToList(this.discountPackDataList);
            List<Dictionary<string, object>> discountSpurLinePackDataList = ShopPackDatasToList(this.discountSpurLinePackDataList);
            List<Dictionary<string, object>> boxPackDataList = ShopPackDatasToList(this.boxPackDataList);

            dict.Add("dailyPackDataList", dailyPackDataList);
            dict.Add("discountPackDataList", discountPackDataList);
            dict.Add("discountSpurLinePackDataList", discountSpurLinePackDataList);
            dict.Add("boxPackDataList", boxPackDataList);
            return dict;
        }
        catch (Exception e)
        {
            GameDebug.LogError(e);
            return new Dictionary<string, object>();
        }
    }


    public void SetSaveDataFromFirestore(Dictionary<string, object> dict)
    {
        try
        {
            if (dict == null)
            {
                GameDebug.LogError("shopSystem为空");
                return;
            }
            if (dict.TryGetValue("lastRefreshDateTime", out object lastRefreshDateTimeObj))
            {
                if (DateTimeOffset.TryParse(lastRefreshDateTimeObj.ToString(), out DateTimeOffset lastRefreshDateTime))
                {
                    this.lastRefreshDateTime = lastRefreshDateTime;
                }
            }
            if (dict.TryGetValue("dailyPackDataList", out object dailyPackDataListObj))
            {
                List<Dictionary<string, object>> dailyPackDataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(dailyPackDataListObj.ToString());
                this.dailyPackDataList = ListToShopPackDatas(dailyPackDataList);
            }
            if (dict.TryGetValue("discountPackDataList", out object discountPackDataListObj))
            {
                List<Dictionary<string, object>> discountPackDataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(discountPackDataListObj.ToString());
                this.discountPackDataList = ListToShopPackDatas(discountPackDataList);
            }

            if (dict.TryGetValue("discountSpurLinePackDataList", out object discountSpurLinePackDataListObj))
            {
                List<Dictionary<string, object>> discountSpurLinePackDataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(discountSpurLinePackDataListObj.ToString());
                this.discountSpurLinePackDataList = ListToShopPackDatas(discountSpurLinePackDataList);
            }
            if (dict.TryGetValue("boxPackDataList", out object boxPackDataListObj))
            {
                List<Dictionary<string, object>> boxPackDataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(boxPackDataListObj.ToString());
                this.discountSpurLinePackDataList = ListToShopPackDatas(boxPackDataList);
            }
            SaveDungeonData();
            SaveDailyData();
            SaveDiscountData();
            SaveBoxPackData();
        }
        catch (Exception e)
        {
            GameDebug.LogError(e);
        }
    }
    #endregion


    #region 刷新
    private void LoadRefreshTime()
    {
        if (SaveUtils.HasKey(Consts.SaveKey_Shop_LastRefreshDate))
        {
            long lastRefeshDateTicks = SaveUtils.GetLong(Consts.SaveKey_Shop_LastRefreshDate);
            lastRefreshDateTime = new DateTimeOffset(lastRefeshDateTicks, TimeSpan.Zero);
        }
        else
        {
            lastRefreshDateTime = DateTimeOffset.MinValue;
            SaveLastRefreshDateTime();
        }
    }

    private void SaveLastRefreshDateTime()
    {
        SaveUtils.SetLong(Consts.SaveKey_Shop_LastRefreshDate, lastRefreshDateTime.Ticks);
    }

    private void RefreshTime()
    {
        //刷新
        if (TimeManager.IsGetServerUtcSuccess)
        {
            lastRefreshDateTime = TimeManager.ServerUtcNow();
            SaveLastRefreshDateTime();
        }
        else
        {
            lastRefreshDateTime = TimeManager.Instance.UtcNow();
            SaveLastRefreshDateTime();
            TimeManager.Instance.TryExcuteWithServerUtc(() =>
            {
                lastRefreshDateTime = TimeManager.ServerUtcNow();
                SaveLastRefreshDateTime();
            });
        }
    }
    public bool TryRefreshAllData()
    {
        if (!hasLoadData)
        {
            return false;
        }

        if (!TimeManager.IsGetServerUtcSuccess)
        {
            return false;
        }

        try
        {
            if (ExtensionTool.IsDateBeforeToday(lastRefreshDateTime, TimeManager.ServerUtcNow()))
            {
                //刷新
                lastRefreshDateTime = TimeManager.ServerUtcNow();
                SaveUtils.SetLong(Consts.SaveKey_Shop_LastRefreshDate, lastRefreshDateTime.Ticks);

                RefreshTodayDailyPack();
                RefreshTodayDiscountPack();
                RefreshTodayBoxPack();
                RefreshPayPack();

                SaveDailyData();
                SaveDiscountData();
                SaveBoxPackData();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            GameDebug.LogError("ShopSystem LoadData error!" + e);
            return false;
        }

    }

    #endregion

    #region 商品购买

    public void RefreshPayPack()
    {
        gemsPayPackDefList.Clear();
        coinsPayPackDefList.Clear();
        foreach (var item in PayPackDefinition.DefinitionMap)
        {
            if (item.Value.RewardItems.Count == 0)
            {

            }
            else if (item.Value.RewardItems.Count == 1 && item.Value.RewardItems[0].IsRewardCoins)
            {
                coinsPayPackDefList.Add(item.Value);
            }
            else if (item.Value.RewardItems.Count == 1 && item.Value.RewardItems[0].IsRewardGems)
            {
                gemsPayPackDefList.Add(item.Value);
            }
        }
        gemsPayPackDefList.Sort((p1, p2) =>
        {
            return p1.Cost.CompareTo(p2.Cost);
        });
        coinsPayPackDefList.Sort((p1, p2) =>
        {
            return p1.Cost.CompareTo(p2.Cost);
        });

    }



    public void BuyShopItem(ShopPackDefinition.ShopTag shopTag, ShopPackData shopPackData, Vector3 screenPos, Action successCB, Action failedCB)
    {
        if (shopPackData.currencyID == CurrencyID.AD || (shopPackData.currencyID == CurrencyID.Free))
        {
            GameDebug.Log("ad or free");
        }
        else if (Currencies.CanAfford(shopPackData.currencyID, shopPackData.cost))
        {
            GameDebug.Log("Buy");
            Currencies.Spend(shopPackData.currencyID, shopPackData.cost, "shop_buy");
        }
        else
        {
            GameDebug.Log("BuyShopItem failed! currencyID:" + shopPackData.currencyID);
            failedCB?.Invoke();
            return;
        }
        MergeLevelType mergeLevelType;
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.none)
            mergeLevelType = MergeLevelType.mainLine;
        else
            mergeLevelType = MergeLevelManager.Instance.CurrentLevelType;
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Shop_Buy);
        if (shopPackData.currencyID == CurrencyID.AD)
            GameManager.Instance.GiveRewardItem(shopPackData.rewardItem, "shop_buy", screenPos, mergeLevelType);
        else
            GameManager.Instance.GiveRewardItem(shopPackData.rewardItem, "shop_buy", screenPos, mergeLevelType);

        //修改今日数据
        switch (shopTag)
        {
            case ShopPackDefinition.ShopTag.personal:
                ShopRandomBagList.Remove(shopPackData);
                SavePersonalShopList();
                break;
            case ShopPackDefinition.ShopTag.daily:
                shopPackData.todayBuyCount++;
                SaveDailyData();
                break;
            case ShopPackDefinition.ShopTag.discount:
                shopPackData.todayBuyCount++;
                SaveDiscountData();
                break;
            case ShopPackDefinition.ShopTag.boxpack:
                shopPackData.todayBuyCount++;
                SaveBoxPackData();
                break;
            default:
                shopPackData.todayBuyCount++;
                break;
        }
        successCB?.Invoke();
        //DailyTaskSystem.Instance.DailyTaskEvent_ShopBuy.Invoke();
    }



    #endregion


    #region 每日礼包数据
    private void LoadBaseShopData()
    {
        LoadDailyData();

        LoadDiscountData();

        LoadBoxData();
    }

    private void LoadDailyData()
    {
        //读取存档
        if (SaveUtils.HasKey(Consts.SaveKey_Shop_Daily))
        {
            string json = SaveUtils.GetString(Consts.SaveKey_Shop_Daily);
            var list = JsonConvert.DeserializeObject<List<ShopPackData>>(json);
            if (list != null)
            {
                dailyPackDataList = list;
            }
        }
        else
        {
            RefreshTodayDailyPack();
            SaveDailyData();
        }

    }


    private void LoadBoxData()
    {
        if (SaveUtils.HasKey(Consts.SaveKey_Shop_BoxPack))
        {
            string json3 = SaveUtils.GetString(Consts.SaveKey_Shop_BoxPack);
            var list3 = JsonConvert.DeserializeObject<List<ShopPackData>>(json3);
            if (list3 != null)
            {
                boxPackDataList = list3;
            }
        }
        else
        {
            RefreshTodayBoxPack();
            SaveBoxPackData();
        }
        if (SaveUtils.HasKey(Consts.SaveKey_Shop_BoxPack1))
        {
            string json3 = SaveUtils.GetString(Consts.SaveKey_Shop_BoxPack1);
            var list3 = JsonConvert.DeserializeObject<List<ShopPackData>>(json3);
            if (list3 != null)
            {
                boxPackDungeon1DataList = list3;
            }
        }
        else
        {
            RefreshTodayBoxPack();
            SaveBoxPackData();
        }
        if (SaveUtils.HasKey(Consts.SaveKey_Shop_BoxPack2))
        {
            string json3 = SaveUtils.GetString(Consts.SaveKey_Shop_BoxPack2);
            var list3 = JsonConvert.DeserializeObject<List<ShopPackData>>(json3);
            if (list3 != null)
            {
                boxPackDungeon2DataList = list3;
            }

        }
        else
        {
            RefreshTodayBoxPack();
            SaveBoxPackData();
        }
        if (SaveUtils.HasKey(Consts.SaveKey_Shop_BoxPack3))
        {
            string json3 = SaveUtils.GetString(Consts.SaveKey_Shop_BoxPack3);
            var list3 = JsonConvert.DeserializeObject<List<ShopPackData>>(json3);
            if (list3 != null)
            {
                boxPackDungeon3DataList = list3;
            }

        }
        else
        {
            RefreshTodayBoxPack();
            SaveBoxPackData();
        }
    }

    public void SaveDailyData()
    {
        try
        {
            string json = JsonConvert.SerializeObject(dailyPackDataList);
            SaveUtils.SetString(Consts.SaveKey_Shop_Daily, json);
        }
        catch (Exception e)
        {
            GameDebug.LogError("SaveDailyData error!" + e);
        }
    }
    public void RefreshTodayDailyPack()
    {
        dailyPackDataList.Clear();
        if (ShopPackDefinition.ShopDefinitionDict.TryGetValue(ShopPackDefinition.ShopTag.daily, out var dailyList))
        {
            foreach (var item in dailyList)
            {
                CurrencyID currency = item.currencyID;
                if (currency == CurrencyID.GemsOrCoins)
                {
                    int index = UnityEngine.Random.Range(0, 2);
                    if (index > 0)
                    {
                        currency = CurrencyID.Gems;
                    }
                    else
                    {
                        currency = CurrencyID.Coins;
                    }
                }
                List<MergeRewardItem> items = new List<MergeRewardItem>();
                MergeRewardItem rewardItem;
                if (item.IsRandomPool)
                {
                    rewardItem = item.itemPool.GetRandomRewardItem();
                    if (string.IsNullOrEmpty(rewardItem.name) || rewardItem.num <= 0)
                    {
                        GameDebug.LogError("MergeRewardItem error!");
                        continue;
                    }
                }
                else
                {

                    rewardItem = new MergeRewardItem()
                    {
                        name = item.PrefabName,
                        num = item.PrefabNum
                    };
                    items.Add(rewardItem);
                }
                ShopPackData packData = new ShopPackData(item.SaveID, items, item.purchaseLimit, item.Cost, currency);
                dailyPackDataList.Add(packData);
            }
        }
    }
    public void SaveBoxPackData()
    {
        try
        {
            string json = JsonConvert.SerializeObject(boxPackDataList);
            SaveUtils.SetString(Consts.SaveKey_Shop_BoxPack, json);
        }
        catch (Exception e)
        {
            GameDebug.LogError("SaveBoxPackData error!" + e);
        }
        try
        {
            string json = JsonConvert.SerializeObject(boxPackDungeon1DataList);
            SaveUtils.SetString(Consts.SaveKey_Shop_BoxPack, json);
        }
        catch (Exception e)
        {
            GameDebug.LogError("SaveBoxPackData error!" + e);
        }
        try
        {
            string json = JsonConvert.SerializeObject(boxPackDungeon2DataList);
            SaveUtils.SetString(Consts.SaveKey_Shop_BoxPack, json);
        }
        catch (Exception e)
        {
            GameDebug.LogError("SaveBoxPackData error!" + e);
        }

        try
        {
            string json = JsonConvert.SerializeObject(boxPackDungeon3DataList);
            SaveUtils.SetString(Consts.SaveKey_Shop_BoxPack, json);
        }
        catch (Exception e)
        {
            GameDebug.LogError("SaveBoxPackData error!" + e);
        }
    }
    public void RefreshTodayBoxPack()
    {
        boxPackDataList.Clear();
        boxPackDungeon1DataList.Clear();
        boxPackDungeon2DataList.Clear();
        boxPackDungeon3DataList.Clear();
        if (ShopPackDefinition.ShopDefinitionDict.TryGetValue(ShopPackDefinition.ShopTag.boxpack, out var boxList))
        {
            foreach (var item in boxList)
            {
                if (!item.Isunlock || item.IsClose)
                    continue;
                CurrencyID currency = item.currencyID;
                if (currency == CurrencyID.GemsOrCoins)
                {
                    int index = UnityEngine.Random.Range(0, 2);
                    if (index > 0)
                    {
                        currency = CurrencyID.Gems;
                    }
                    else
                    {
                        currency = CurrencyID.Coins;
                    }
                }
                MergeRewardItem rewardItem;
                if (item.IsRandomPool)
                {
                    rewardItem = item.itemPool.GetRandomRewardItem();
                    if (string.IsNullOrEmpty(rewardItem.name) || rewardItem.num <= 0)
                    {
                        GameDebug.LogError("MergeRewardItem error!");
                        continue;
                    }
                }
                else
                {
                    rewardItem = new MergeRewardItem()
                    {
                        name = item.PrefabName,
                        num = item.PrefabNum
                    };
                }
                List<MergeRewardItem> items = new List<MergeRewardItem>();
                items.Add(rewardItem);
                ShopPackData packData = new ShopPackData(item.SaveID, items, item.purchaseLimit, item.Cost, currency);
                if (item.ShopType == "mainLine")
                    boxPackDataList.Add(packData);
                else if (item.ShopType == "Dungeon1")
                    boxPackDungeon1DataList.Add(packData);
                else if (item.ShopType == "Dungeon2")
                    boxPackDungeon2DataList.Add(packData);
                else if (item.ShopType == "Dungeon3")
                    boxPackDungeon3DataList.Add(packData);
            }
        }
    }
    //激励视频回来观看后扣除对应奖励次数
    public void RefreshDailyDataByReduceCount(List<MergeRewardItem> rewardList) 
    {
        for (int i = 0; i < dailyPackDataList.Count; i++)
        {
            if (dailyPackDataList[i].rewardItem.Count > 0 && rewardList.Count > 0 && dailyPackDataList[i].rewardItem[0].name == rewardList[0].name) 
            {
                dailyPackDataList[i].countLimit--;
                SaveDailyData();
                refreshShopAction?.Invoke();
                return;
            }
        }
    }

    #endregion


    #region 折扣礼包数据
    private void LoadDiscountData()
    {
        if (SaveUtils.HasKey(Consts.SaveKey_Shop_Discount))
        {
            string json2 = SaveUtils.GetString(Consts.SaveKey_Shop_Discount);
            var list2 = JsonConvert.DeserializeObject<List<ShopPackData>>(json2);
            if (list2 != null)
            {
                discountPackDataList = list2;
            }
        }
        else
        {
            RefreshMainLineDiscountPack();
            SaveDiscountData();
        }
        if (SaveUtils.HasKey(Consts.SaveKey_Shop_SpurLineDiscount))
        {
            string json2 = SaveUtils.GetString(Consts.SaveKey_Shop_SpurLineDiscount);
            var list2 = JsonConvert.DeserializeObject<List<ShopPackData>>(json2);
            if (list2 != null)
            {
                discountSpurLinePackDataList = list2;
            }
        }
        if (SaveUtils.HasKey(Consts.SaveKey_Shop_Discount2))
        {
            string json2 = SaveUtils.GetString(Consts.SaveKey_Shop_Discount2);
            var list2 = JsonConvert.DeserializeObject<List<ShopPackData>>(json2);
            if (list2 != null)
            {
                discountDungeon2PackDataList = list2;
            }

        }
        else
        {
            RefreshDungeon2DiscountPack();
            SaveDiscountData();
        }
        if (SaveUtils.HasKey(Consts.SaveKey_Shop_Discount3))
        {
            string json2 = SaveUtils.GetString(Consts.SaveKey_Shop_Discount3);
            var list2 = JsonConvert.DeserializeObject<List<ShopPackData>>(json2);
            if (list2 != null)
            {
                discountDungeon3PackDataList = list2;
            }

        }
        else
        {
            RefreshDungeon3DiscountPack();
            SaveDiscountData();
        }
    }

    public void SaveDiscountData()
    {
        try
        {
            string json = JsonConvert.SerializeObject(discountPackDataList);
            SaveUtils.SetString(Consts.SaveKey_Shop_Discount, json);
        }
        catch (Exception e)
        {
            GameDebug.LogError("SaveDiscountData error!" + e);
        }
        try
        {
            string json = JsonConvert.SerializeObject(discountSpurLinePackDataList);
            SaveUtils.SetString(Consts.SaveKey_Shop_SpurLineDiscount, json);
        }
        catch (Exception e)
        {
            GameDebug.LogError("SaveDiscountData error!" + e);
        }
        try
        {
            string json = JsonConvert.SerializeObject(discountDungeon2PackDataList);
            SaveUtils.SetString(Consts.SaveKey_Shop_Discount2, json);
        }
        catch (Exception e)
        {
            GameDebug.LogError("SaveDiscountData error!" + e);
        }
        try
        {
            string json = JsonConvert.SerializeObject(discountDungeon3PackDataList);
            SaveUtils.SetString(Consts.SaveKey_Shop_Discount3, json);
        }
        catch (Exception e)
        {
            GameDebug.LogError("SaveDiscountData error!" + e);
        }
    }
    private ShopPackData CreateShopPackData(ShopPackDefinition item)
    {
        CurrencyID currency = item.currencyID;
        if (currency == CurrencyID.GemsOrCoins)
        {
            int index = UnityEngine.Random.Range(0, 2);
            if (index > 0)
            {
                currency = CurrencyID.Gems;
            }
            else
            {
                currency = CurrencyID.Coins;
            }
        }
        MergeRewardItem rewardItem;
        if (item.IsRandomPool)
        {
            if (item == null || item.itemPool == null)
                return null;
            rewardItem = item.itemPool.GetRandomRewardItem();
            if (string.IsNullOrEmpty(rewardItem.name) || rewardItem.num <= 0)
            {
                GameDebug.LogError("MergeRewardItem error!");
                return null;
            }
        }
        else
        {
            rewardItem = new MergeRewardItem()
            {
                name = item.PrefabName,
                num = item.PrefabNum
            };
        }
        if (string.IsNullOrEmpty(rewardItem.name) || rewardItem.num <= 0)
        {
            GameDebug.LogError("MergeRewardItem error!");
            return null;
        }

        //如果第一次进游戏
        if (!SaveUtils.HasKey(Consts.SaveKey_Shop_Discount) && item.Initialization != "")
        {
            rewardItem = new MergeRewardItem()
            {
                name = item.Initialization,
                num = 1
            };
        }

        int cost = 1;
        switch (currency)
        {
            case CurrencyID.Coins:
                {
                    if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(rewardItem.name, out var def))
                    {
                        cost = def.PriceCoins;
                    }
                }
                break;
            case CurrencyID.Gems:
                {
                    if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(rewardItem.name, out var def))
                    {
                        cost = def.PriceGems;
                    }
                }
                break;
        }
        if (cost <= 0)
        {
            //GameDebug.LogError("cannot found priceCost! prefab:" + rewardItem.name);
        }
        List<MergeRewardItem> items = new List<MergeRewardItem>();
        items.Add(rewardItem);
        return new ShopPackData(item.SaveID, items, item.purchaseLimit, cost, currency);
    }


    public void RefreshMainLineDiscountPack()
    {
        discountPackDataList.Clear();
        if (ShopPackDefinition.ShopDefinitionDict.TryGetValue(ShopPackDefinition.ShopTag.discount, out var discountList))
        {
            foreach (var item in discountList)
            {
                if (!item.Isunlock || item.IsClose)
                    continue;
                ShopPackData shopPackData = CreateShopPackData(item);
                if (shopPackData == null)
                    continue;
                if (item.ShopType == "mainLine")
                    discountPackDataList.Add(shopPackData);
            }
        }
    }
    public void RefreshSpurLineDiscountPack(MergeLevelType mergeLevel)
    {
        discountSpurLinePackDataList.Clear();
        if (ShopPackDefinition.ShopDefinitionDict.TryGetValue(ShopPackDefinition.ShopTag.discount, out var discountList))
        {
            foreach (var item in discountList)
            {
                ShopPackData shopPackData = CreateShopPackData(item);
                if (shopPackData == null)
                    continue;
                if (mergeLevel== MergeLevelType.branch1 && item.ShopType == "spurLine_coffee"
                    || mergeLevel == MergeLevelType.branch_christmas && item.ShopType == "spurLine_christmas"
                    || mergeLevel == MergeLevelType.branch_halloween && item.ShopType == "spurLine_halloween"
                    || mergeLevel == MergeLevelType.branch_SpurLine4 && item.ShopType == "spurLine_halloween") 
                {
                    discountSpurLinePackDataList.Add(shopPackData);
                    shopPackData.MergeLevel = mergeLevel;
                } 
            }
        }
    }

    public void RefreshDungeon2DiscountPack()
    {
        discountDungeon2PackDataList.Clear();
        if (ShopPackDefinition.ShopDefinitionDict.TryGetValue(ShopPackDefinition.ShopTag.discount, out var discountList))
        {
            foreach (var item in discountList)
            {
                ShopPackData shopPackData = CreateShopPackData(item);
                if (shopPackData == null)
                    continue;
                if (item.ShopType == "Dungeon2")
                    discountDungeon2PackDataList.Add(shopPackData);
            }
        }
    }

    public void RefreshDungeon3DiscountPack()
    {
        discountDungeon3PackDataList.Clear();
        if (ShopPackDefinition.ShopDefinitionDict.TryGetValue(ShopPackDefinition.ShopTag.discount, out var discountList))
        {
            foreach (var item in discountList)
            {
                ShopPackData shopPackData = CreateShopPackData(item);
                if (shopPackData == null)
                    continue;
                if (item.ShopType == "Dungeon3")
                    discountDungeon3PackDataList.Add(shopPackData);
            }
        }
    }

    public void RefreshSingeDiscountPack()
    {
        if (MergeLevelManager.Instance.IsBranch(MergeLevelManager.Instance.CurrentLevelType))
            RefreshSpurLineDiscountPack(MergeLevelManager.Instance.CurrentLevelType);
        else if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.dungeon1)
        {

        }
        else if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.dungeon2)
            RefreshDungeon2DiscountPack();
        else if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.dungeon3)
            RefreshDungeon3DiscountPack();
        else
            RefreshMainLineDiscountPack();
    }

    private bool CheckDataError(List<ShopPackData> discountList)
    {
        bool isError = false;
        if (discountList.Count == 0)
            return true;
        foreach (var item in discountList)
        {
            if (item.cost <= 0)
                isError = true;
        }
        //检测支线 ToDo
        if (MergeLevelManager.Instance.IsBranch(MergeLevelManager.Instance.CurrentLevelType)) 
        {
            foreach (var item in discountList)
            {
                if (item.MergeLevel != MergeLevelManager.Instance.CurrentLevelType)
                    isError = true;
            }
        }
        return isError;
    }

    public List<ShopPackData> GetDiscountList()
    {
        List<ShopPackData> discountList;
        if (MergeLevelManager.Instance.IsBranch(MergeLevelManager.Instance.CurrentLevelType))
            discountList = discountSpurLinePackDataList;
        else
            discountList = discountPackDataList;
        if (CheckDataError(discountList))
        {
            RefreshSingeDiscountPack();
            SaveDiscountData();
        }
        if (MergeLevelManager.Instance.IsBranch(MergeLevelManager.Instance.CurrentLevelType))
            return discountSpurLinePackDataList;
        else
            return discountPackDataList;
    }

    public void RefreshTodayDiscountPack()
    {
        RefreshMainLineDiscountPack();
        RefreshDungeon2DiscountPack();
        RefreshDungeon3DiscountPack();
    }


    #endregion


    #region 定制私人礼包数据
    //返回普通商店数据
    private ShopPackData CreateNormalShopData(int poolId, MergeItemDefinition mergeItemDefinition, CurrencyID currencyID)
    {
        List<MergeRewardItem> rewardItems = new List<MergeRewardItem>();
        if (RandomBagConfig.randomBagDict.TryGetValue(poolId, out RandomBagItem randomBagItem))
        {
            //randomBagItem.
            if (RandomItemPondConfig.randomItemDict.TryGetValue(randomBagItem.RandomReward1, out RandomItemPool pool))
            {
                rewardItems.Add(randomBagItem.RandomReward2);
                rewardItems.Add(randomBagItem.FixedReward);
                foreach (var item in pool.rewardItemsList)
                {
                    //遍历对应的池子 找到对应物品
                    if (item.SameKind == mergeItemDefinition.SameKind && currencyID == CurrencyID.Gems)
                    {
                        rewardItems.Insert(0, item.rewardItem);
                        long time = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + randomBagItem.refreshTime;
                        return new ShopPackData(mergeItemDefinition.ItemId.ToString(),
                            rewardItems,
                            1,
                            randomBagItem.SellDiamond,
                            CurrencyID.Gems,
                            randomBagItem.OriginalPrice,
                            time);
                    }
                }
            }
        }
        return null;
    }

    private List<MergeRewardItem> needItemList = new List<MergeRewardItem>();
    private void GetAllItem()
    {
        List<string> tasks = TaskGoalsManager.Instance.ReturnSortTask();
        needItemList.Clear();
        for (int i = 0; i < tasks.Count; i++)
        {
            if (TaskGoalsDefinition.TaskDefinitionsDict.TryGetValue(tasks[i], out TaskData taskData))
            {
                List<MergeRewardItem> taskItemList = taskData.taskDefinition.needItemList;
                for (int j = 0; j < taskItemList.Count; j++)
                {
                    int count = 0;
                    for (int t = 0; t < needItemList.Count; t++)
                    {
                        if (taskData.taskDefinition.needItemList[j].name == needItemList[t].name)
                            count++;
                    }
                    //2022/7/5 改为只能有一个相同类型的礼包
                    if (count >= 1)
                        continue;
                    needItemList.Add(taskData.taskDefinition.needItemList[j]);
                }
            }
        }
    }

    public List<ShopPackData> ShopRandomBagList { get; private set; } = new List<ShopPackData>();
    public List<PayPackDefinition> GemsRandomBagList { get; private set; } = new List<PayPackDefinition>();
    public List<PayPackDefinition> CoinsRandomBagList { get; private set; } = new List<PayPackDefinition>();
    public List<PayPackDefinition> Dungeon1BagList { get; private set; } = new List<PayPackDefinition>();
    public List<PayPackDefinition> Dungeon2BagList { get; private set; } = new List<PayPackDefinition>();
    public List<PayPackDefinition> Dungeon3BagList { get; private set; } = new List<PayPackDefinition>();

    public void RemoveShopRandomBagList(ShopPackData shopPackData)
    {
        ShopRandomBagList.Remove(shopPackData);
        SaveShopList();
    }
    public void RemoveGemsRandomBagList(PayPackDefinition payPackDefinition)
    {
        GemsRandomBagList.Remove(payPackDefinition);
        SaveGemsShopList();
    }
    public void RemoveCoinsRandomBagList(PayPackDefinition payPackDefinition)
    {
        CoinsRandomBagList.Remove(payPackDefinition);
        UIPanel_TopBanner.refreshBanner?.Invoke();
        SaveCoinsShopList();
        refreshShopAction?.Invoke();
    }

    private void SaveShopList()
    {
        try
        {
            string json = JsonConvert.SerializeObject(ShopRandomBagList);
            SaveUtils.SetString(Consts.SaveKey_ShopRandomBagList, json);
        }
        catch (Exception e)
        {
            GameDebug.LogError("SaveDailyData error!" + e);
        }
    }

    private void SaveGemsShopList()
    {
        try
        {
            string json = JsonConvert.SerializeObject(GemsRandomBagList);
            SaveUtils.SetString(Consts.SaveKey_GemsRandomBagList, json);
        }
        catch (Exception e)
        {
            GameDebug.LogError("SaveDailyData error!" + e);
        }
    }

    private void SaveCoinsShopList()
    {
        try
        {
            string json = JsonConvert.SerializeObject(CoinsRandomBagList);
            SaveUtils.SetString(Consts.SaveKey_CoinsRandomBagList, json);
        }
        catch (Exception e)
        {
            GameDebug.LogError("SaveDailyData error!" + e);
        }
    }

    private void SaveDungeon1ShopList()
    {
        try
        {
            string json = JsonConvert.SerializeObject(Dungeon1BagList);
            SaveUtils.SetString(Consts.SaveKey_Dungeon1List, json);
        }
        catch (Exception e)
        {
            GameDebug.LogError("SaveDailyData error!" + e);
        }
    }

    private void SaveDungeon2ShopList()
    {
        try
        {
            string json = JsonConvert.SerializeObject(Dungeon2BagList);
            SaveUtils.SetString(Consts.SaveKey_Dungeon2List, json);
        }
        catch (Exception e)
        {
            GameDebug.LogError("SaveDailyData error!" + e);
        }
    }

    private void SaveDungeon3ShopList()
    {
        try
        {
            string json = JsonConvert.SerializeObject(Dungeon3BagList);
            SaveUtils.SetString(Consts.SaveKey_Dungeon3List, json);
        }
        catch (Exception e)
        {
            GameDebug.LogError("SaveDailyData error!" + e);
        }
    }

    private void ClearPayPackDefinitionTimeoutData(List<PayPackDefinition> payPackDefinitionList)
    {
        List<PayPackDefinition> payPackDefinitions = new List<PayPackDefinition>();
        foreach (var item in payPackDefinitionList)
        {
            TimeSpan timeSpan = DateTimeOffset.FromUnixTimeSeconds(item.refreshTime) - DateTimeOffset.UtcNow;
            if (timeSpan.TotalSeconds < 0)
            {
                payPackDefinitions.Add(item);
            }
        }
        foreach (var item in payPackDefinitions)
        {
            payPackDefinitionList.Remove(item);
        }
    }

    private void ClearShopPackDataTimeoutData(List<ShopPackData> shopPackDataList)
    {

        List<ShopPackData> shopPackDatas = new List<ShopPackData>();
        foreach (var item in shopPackDataList)
        {
            TimeSpan timeSpan = DateTimeOffset.FromUnixTimeSeconds(item.refreshTime) - DateTimeOffset.UtcNow;
            if (timeSpan.TotalSeconds < 0)
            {
                shopPackDatas.Add(item);
            }
        }
        foreach (var item in shopPackDatas)
        {
            shopPackDataList.Remove(item);
        }
    }

    public void ClearTimeoutData()
    {
        ClearShopPackDataTimeoutData(ShopRandomBagList);
        ClearPayPackDefinitionTimeoutData(GemsRandomBagList);
        ClearPayPackDefinitionTimeoutData(CoinsRandomBagList);
        SavePersonalShopList();
    }

    private bool NormalIsCanAdd(String nameStr)
    {
        foreach (var item in ShopRandomBagList)
        {
            if (item.rewardItem[0].name == nameStr)
                return false;
        }
        return true;
    }

    private void RefreshNormalShopData()
    {
        //获取所有激活任务所有需要的道具
        for (int i = 0; i < needItemList.Count; i++)
        {
            if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(needItemList[i].name, out MergeItemDefinition mergeItemDefinition))
            {
                if (mergeItemDefinition.ShopRandomBagID != 0)
                {
                    ShopPackData shopPackData = CreateNormalShopData(mergeItemDefinition.ShopRandomBagID, mergeItemDefinition, CurrencyID.Gems);

                    if (shopPackData != null)
                    {
                        if (ShopRandomBagList.Count >= 3)
                            return;
                        if (NormalIsCanAdd(shopPackData.rewardItem[0].name))
                            ShopRandomBagList.Add(shopPackData);
                    }
                }
            }
        }
    }

    private bool GemsIsCanAdd(String nameStr)
    {
        foreach (var item in GemsRandomBagList)
        {
            if (item.RewardItems[0].name == nameStr)
                return false;
        }
        return true;
    }


    private void RefreshGemsShopData()
    {
        for (int i = 0; i < needItemList.Count; i++)
        {
            if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(needItemList[i].name, out MergeItemDefinition mergeItemDefinition))
            {
                if (mergeItemDefinition.GemsRandomBagID != 0)
                {

                    PayPackDefinition shopPackData = CreateSpendShopData(mergeItemDefinition.GemsRandomBagID, mergeItemDefinition.SameKind, CurrencyID.Pay);
                    if (shopPackData != null)
                    {
                        if (GemsRandomBagList.Count >= 3)
                            return;
                        if (GemsIsCanAdd(shopPackData.RewardItems[0].name))
                            GemsRandomBagList.Add(shopPackData);
                    }
                }
            }
        }
    }


    private bool CoinsIsCanAdd(String nameStr)
    {
        foreach (var item in CoinsRandomBagList)
        {
            if (item.RewardItems[0].name == nameStr)
                return false;
        }
        return true;
    }

    private void RefreshCoinsShopData()
    {
        for (int i = 0; i < needItemList.Count; i++)
        {
            if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(needItemList[i].name, out MergeItemDefinition mergeItemDefinition))
            {
                if (mergeItemDefinition.ShopRandomBagID != 0)
                {
                    PayPackDefinition shopPackData = CreateSpendShopData(mergeItemDefinition.ShopRandomBagID, mergeItemDefinition.SameKind, CurrencyID.Pay);
                    if (shopPackData != null)
                    {
                        if (CoinsRandomBagList.Count >= 3)
                            return;
                        if (CoinsIsCanAdd(shopPackData.RewardItems[0].name))
                            CoinsRandomBagList.Add(shopPackData);
                    }
                }
            }
        }
    }


    public void SavePersonalShopList()
    {
        SaveShopList();
        SaveGemsShopList();
        SaveCoinsShopList();
    }

    private void RefreshPersonalShopList()
    {
        RefreshNormalShopData();
        RefreshGemsShopData();
        RefreshCoinsShopData();
    }


    public Action refreshShopAction;
    public void RefreshPersonalShopData()
    {
        ClearTimeoutData();
        GetAllItem();
        RefreshPersonalShopList();
        SavePersonalShopList();
        refreshShopAction?.Invoke();
    }

    public void SetNeedRefresh(string str)
    {
        SaveUtils.SetString(Consts.SaveKey_NeedRefresh, str);
    }

    public bool GetNeedRefresh()
    {
        return SaveUtils.GetString(Consts.SaveKey_NeedRefresh) == "true";
    }



    #endregion

    #region Offer 礼包数据
    public List<PayPackDefinition> OfferList { get; private set; } = new List<PayPackDefinition>();

    public void InitOfferList() 
    {
        OfferList.Clear();
        string str1 = string.Empty;
        string str2 = string.Empty;
        string str3 = string.Empty;
        string str4 = string.Empty;
        if (TaskGoalsManager.Instance.curLevelIndex <= 15)
        {
            str1 = "GardeningToolBox";
            str2 = "CoffeeCup";
            str3 = "Drawer";
            str4 = "Blender";
        }
        else if (TaskGoalsManager.Instance.curLevelIndex <= 30)
        {
            str1 = "ToolBox";
            str2 = "GardeningToolBox";
            str3 = "CoffeeCup";
            str4 = "Drawer";
        }
        else if (TaskGoalsManager.Instance.curLevelIndex <= 45)
        {
            str1 = "FlowerPot";
            str2 = "ToolBox";
            str3 = "GardeningToolBox";
            str4 = "CoffeeCup";
        }
        else if (TaskGoalsManager.Instance.curLevelIndex <= 60)
        {
            str1 = "Tree";
            str2 = "FlowerPot";
            str3 = "ToolBox";
            str4 = "GardeningToolBox";
        }
        else if (TaskGoalsManager.Instance.curLevelIndex <= 75)
        {
            str1 = "Tree";
            str2 = "FlowerPot";
            str3 = "ToolBox";
            str4 = "GardeningToolBox";
        }
        else if (TaskGoalsManager.Instance.curLevelIndex <= 90)
        {
            str1 = "BroomCabinet";
            str2 = "Tree";
            str3 = "FlowerPot";
            str4 = "ToolBox";
        }
        else if (TaskGoalsManager.Instance.curLevelIndex <= 105)
        {
            str1 = "Fishing";
            str2 = "BroomCabinet";
            str3 = "Tree";
            str4 = "FlowerPot";
        }
        else if (TaskGoalsManager.Instance.curLevelIndex <= 120)
        {
            str1 = "Cupboard";
            str2 = "Fishing";
            str3 = "BroomCabinet";
            str4 = "Tree";
        }
        else if (TaskGoalsManager.Instance.curLevelIndex <= 135)
        {
            str1 = "Cupboard";
            str2 = "Fishing";
            str3 = "BroomCabinet";
            str4 = "Tree";
        }
        else
        {
            str1 = "Cupboard";
            str2 = "Fishing";
            str3 = "BroomCabinet";
            str4 = "Tree";
        }
        bool is_buy_limit= SaveUtils.GetBool(Consts.SaveKey_BuyLimitGift, false);
        if (!is_buy_limit) 
        {
            OfferList.Add(FIRSTTIMEOFFER());
            OfferList.Add(ELEGANTOFFER(str1, str2, str3, str4));
            OfferList.Add(SIMPLEOFFER());
        }
        else
        {
            OfferList.Add(SIMPLEOFFER());
            OfferList.Add(ELEGANTOFFER(str1, str2, str3, str4));
        }
        
        OfferList.Add(SILVEROFFER(str1, str2, str3, str4));
        OfferList.Add(GOLDENOFFER(str1, str2, str3, str4));
        OfferList.Add(HIGHROLLEROFFER(str1, str2, str3, str4));
        OfferList.Add(LAVISHOFFER(str1, str2, str3, str4));
    }

    private PayPackDefinition FIRSTTIMEOFFER()
    {
        List<MergeRewardItem> rewardItems = new List<MergeRewardItem>();
        rewardItems.Add(new MergeRewardItem 
        {
            name= "gems",
            num= 140,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "EnergyChest",
            num = 2,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "FancyChest_1",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "BrownChest_1",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "PiggyBank_1",
            num = 2,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "RewardChest_1",
            num = 2,
        });
        
        PayPackDefinition payPack = new PayPackDefinition(
            "shopPackage_0",
            "32",
            rewardItems,
            0.99f
            );
        payPack.payType = "HOT";
        payPack.sameKind = "Obj/Shop/Packper/Name1";
        payPack.tag = "img_tag7";
        payPack.LimitToOne = true;
        return payPack;
    }
    private PayPackDefinition SIMPLEOFFER()
    {
        List<MergeRewardItem> rewardItems = new List<MergeRewardItem>();
        rewardItems.Add(new MergeRewardItem
        {
            name = "gems",
            num = 150,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "EnergyChest",
            num = 3,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "FancyChest_1",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "BrownChest_1",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "PiggyBank_1",
            num = 3,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "RewardChest_1",
            num = 2,
        });
        PayPackDefinition payPack = new PayPackDefinition(
           "shopPackage_1",
           "34",
           rewardItems,
           1.99f
           );
        payPack.sameKind = "Obj/Shop/Packper/Name2";
        payPack.tag = "img_tag2";
        return payPack;
    }
    private PayPackDefinition ELEGANTOFFER(string str1, string str2, string str3, string str4)
    {
        List<MergeRewardItem> rewardItems = new List<MergeRewardItem>();
        rewardItems.Add(new MergeRewardItem
        {
            name = "gems",
            num = 570,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "Energy",
            num = 200,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "FancyChest_2",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "BrownChest_2",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str1+"_5",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str2 + "_5",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str3 + "_5",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str4 + "_6",
            num = 1,
        });
        PayPackDefinition payPack = new PayPackDefinition(
        "shopPackage_2",
        "33",
        rewardItems,
        9.99f
        );
        payPack.sameKind = "Obj/Shop/Packper/Name3";
        payPack.tag = "img_tag3";
        return payPack;
    }
    private PayPackDefinition SILVEROFFER(string str1, string str2, string str3, string str4)
    {
        List<MergeRewardItem> rewardItems = new List<MergeRewardItem>();
        rewardItems.Add(new MergeRewardItem
        {
            name = "gems",
            num = 1250,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "Energy",
            num = 400,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "FancyChest_2",
            num = 2,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "BrownChest_2",
            num = 2,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str1 + "_6",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str2 + "_6",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str3 + "_6",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str4 + "_7",
            num = 1,
        });
        PayPackDefinition payPack = new PayPackDefinition(
        "shopPackage_3",
        "35",
        rewardItems,
        19.99f
        );
        payPack.payType = "POP";
        payPack.sameKind = "Obj/Shop/Packper/Name4";
        payPack.tag = "img_tag4";
        return payPack;
    }
    private PayPackDefinition GOLDENOFFER(string str1, string str2, string str3, string str4)
    {
        List<MergeRewardItem> rewardItems = new List<MergeRewardItem>();
        rewardItems.Add(new MergeRewardItem
        {
            name = "gems",
            num = 1900,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "Energy",
            num = 600,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "FancyChest_2",
            num = 3,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "BrownChest_2",
            num = 3,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str1 + "_7",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str2 + "_6",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str3 + "_6",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str4 + "_7",
            num = 1,
        });
        PayPackDefinition payPack = new PayPackDefinition(
        "shopPackage_4",
        "36",
        rewardItems,
        29.99f
        );
        payPack.sameKind = "Obj/Shop/Packper/Name5";
        payPack.tag = "img_tag5";
        return payPack;
    }
    private PayPackDefinition HIGHROLLEROFFER(string str1, string str2, string str3, string str4)
    {
        List<MergeRewardItem> rewardItems = new List<MergeRewardItem>();
        rewardItems.Add(new MergeRewardItem
        {
            name = "gems",
            num = 4000,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "Energy",
            num = 900,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "FancyChest_3",
            num = 2,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "BrownChest_3",
            num = 2,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "Coins",
            num = 3000,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str1 + "_8",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str2 + "_7",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str3 + "_7",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str4 + "_8",
            num = 1,
        });
        PayPackDefinition payPack = new PayPackDefinition(
        "shopPackage_5",
        "37",
        rewardItems,
        69.99f
        );
        payPack.sameKind = "Obj/Shop/Packper/Name6";
        payPack.tag = "img_tag6";
        return payPack;
    }
    private PayPackDefinition LAVISHOFFER(string str1, string str2, string str3, string str4)
    {
        List<MergeRewardItem> rewardItems = new List<MergeRewardItem>();
        rewardItems.Add(new MergeRewardItem
        {
            name = "gems",
            num = 7500,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "Energy",
            num = 1250,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "FancyChest_3",
            num = 3,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "BrownChest_3",
            num = 3,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = "Coins",
            num = 9900,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str1 + "_8",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str2 + "_8",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str3 + "_8",
            num = 1,
        });
        rewardItems.Add(new MergeRewardItem
        {
            name = str4 + "_9",
            num = 1,
        });
        PayPackDefinition payPack = new PayPackDefinition(
        "shopPackage_6",
        "38",
        rewardItems,
        99.99f
        );
        payPack.sameKind = "Obj/Shop/Packper/Name7";
        payPack.payType = "BEST";
        payPack.tag = "img_tag1";
        return payPack;
    }
    public void RemoveOfferList(PayPackDefinition payPackDefinition)
    {
        OfferList.Remove(payPackDefinition);
        SaveCoinsShopList();
        refreshShopAction?.Invoke();
    }

    #endregion

    #region 副本商城数据
    private void ClearDungeonData()
    {
        ClearPayPackDefinitionTimeoutData(Dungeon1BagList);
        ClearPayPackDefinitionTimeoutData(Dungeon2BagList);
        ClearPayPackDefinitionTimeoutData(Dungeon3BagList);
    }

    public void RemoveDungeon1BagList(PayPackDefinition payPackDefinition)
    {
        Dungeon1BagList.Remove(payPackDefinition);
        SaveDungeon1ShopList();
    }
    public void RemoveDungeon2BagList(PayPackDefinition payPackDefinition)
    {
        Dungeon2BagList.Remove(payPackDefinition);
        SaveDungeon2ShopList();
    }
    public void RemoveDungeon3BagList(PayPackDefinition payPackDefinition)
    {
        Dungeon3BagList.Remove(payPackDefinition);
        SaveDungeon3ShopList();
    }

    private void SaveDungeonData()
    {
        SaveDungeon1ShopList();
        SaveDungeon2ShopList();
        SaveDungeon3ShopList();
    }

    private void RefreshDungeonShopData()
    {
        foreach (var item in GiftPackageDefinition.GiftDefinitionsDict)
        {
            long time = TimeManager.Instance.TomorrowUtc().ToUnixTimeSeconds();
            PayPackDefinition payPack = new PayPackDefinition(item.Value.SaveID,
                            item.Value.UnityID,
                            item.Value.ItemRewardList,
                            item.Value.Cost,
                            item.Value.OriginalPrice.ToString(),
                            time);
            if (item.Value.GiftType == GiftPackageType.Dungeon1)
                Dungeon1BagList.Add(payPack);
            else if (item.Value.GiftType == GiftPackageType.Dungeon2)
                Dungeon2BagList.Add(payPack);
            else if (item.Value.GiftType == GiftPackageType.Dungeon3)
                Dungeon3BagList.Add(payPack);
        }
    }

    private void LoadPersonalShopData()
    {
        //读取存档
        if (SaveUtils.HasKey(Consts.SaveKey_ShopRandomBagList))
        {
            string json = SaveUtils.GetString(Consts.SaveKey_ShopRandomBagList);
            var list = JsonConvert.DeserializeObject<List<ShopPackData>>(json);
            if (list != null)
            {
                ShopRandomBagList = list;
            }
        }
        else
        {
            GetAllItem();
            RefreshNormalShopData();
            SaveShopList();
        }
        if (SaveUtils.HasKey(Consts.SaveKey_GemsRandomBagList))
        {
            string json = SaveUtils.GetString(Consts.SaveKey_GemsRandomBagList);
            var list = JsonConvert.DeserializeObject<List<PayPackDefinition>>(json);
            if (list != null)
            {
                GemsRandomBagList = list;
            }
        }
        else
        {
            GetAllItem();
            RefreshGemsShopData();
            SaveGemsShopList();

        }
        if (SaveUtils.HasKey(Consts.SaveKey_CoinsRandomBagList))
        {
            string json = SaveUtils.GetString(Consts.SaveKey_CoinsRandomBagList);
            var list = JsonConvert.DeserializeObject<List<PayPackDefinition>>(json);
            if (list != null)
            {
                CoinsRandomBagList = list;
            }
        }
        else
        {
            GetAllItem();
            RefreshCoinsShopData();
            SaveCoinsShopList();
        }
    }

    //返回金币、砖石商店数据
    private PayPackDefinition CreateSpendShopData(int poolId, string sameKind, CurrencyID currencyID)
    {
        List<MergeRewardItem> rewardItems = new List<MergeRewardItem>();
        if (RandomBagConfig.randomBagDict.TryGetValue(poolId, out RandomBagItem randomBagItem))
        {
            //randomBagItem.
            if (RandomItemPondConfig.randomItemDict.TryGetValue(randomBagItem.RandomReward1, out RandomItemPool pool))
            {
                rewardItems.Add(randomBagItem.RandomReward2);
                rewardItems.Add(randomBagItem.FixedReward);
                foreach (var item in pool.rewardItemsList)
                {
                    //遍历对应的池子 找到对应物品
                    if (item.SameKind == sameKind && currencyID == CurrencyID.Pay)
                    {
                        rewardItems.Insert(0, item.rewardItem);
                        long time = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + randomBagItem.refreshTime;
                        return new PayPackDefinition(randomBagItem.Id.ToString(),
                            randomBagItem.UnityId
                            ,
                            rewardItems,
                            randomBagItem.SellDollar,
                            randomBagItem.OriginalPrice,
                            time,
                            item.SameKind);
                    }
                }
            }
        }
        return null;
    }
    private void LoadDunoenShopData()
    {
        if (SaveUtils.HasKey(Consts.SaveKey_Dungeon1List))
        {
            string json = SaveUtils.GetString(Consts.SaveKey_Dungeon1List);
            var list = JsonConvert.DeserializeObject<List<PayPackDefinition>>(json);
            if (list != null)
            {
                Dungeon1BagList = list;
            }
        }
        else
        {
            RefreshDungeonShopData();
            SaveDungeon1ShopList();
        }

        if (SaveUtils.HasKey(Consts.SaveKey_Dungeon1List))
        {
            string json = SaveUtils.GetString(Consts.SaveKey_Dungeon1List);
            var list = JsonConvert.DeserializeObject<List<PayPackDefinition>>(json);
            if (list != null)
            {
                Dungeon2BagList = list;
            }
        }
        else
        {
            RefreshDungeonShopData();
            SaveDungeon2ShopList();
        }

        if (SaveUtils.HasKey(Consts.SaveKey_Dungeon1List))
        {
            string json = SaveUtils.GetString(Consts.SaveKey_Dungeon1List);
            var list = JsonConvert.DeserializeObject<List<PayPackDefinition>>(json);
            if (list != null)
            {
                Dungeon3BagList = list;
            }
        }
        else
        {
            RefreshDungeonShopData();
            SaveDungeon3ShopList();
        }

    }
    #endregion

}

