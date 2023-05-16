using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ivy.game;
using System.IO;
using Ivy;
using Ivy.Firebase;

public class PlayerFirestoreData
{
    [NonSerialized, JsonIgnore]
    public static DateTimeOffset FirestoreDataDate = DateTimeOffset.MinValue;//服务器数据的上传时间

    //基础信息
    public string dataDate;
    public bool needDownload = false;//在服务器端修改，以供玩家下次登录游戏时回档
    public string localid = string.Empty;
    public string playerName = string.Empty;
    public string profile = string.Empty;
    public string app_version = string.Empty;
    public string firebaseID = string.Empty;
    //public string googleId = string.Empty;
    //public string facebookId = string.Empty;
    //public string appleId = string.Empty;
    public string push_token = string.Empty;
    public string language;

    public string first_session;
    public string last_session;
    public int total_active_days;
    public int user_level;
    public int total_full_ads;
    public int total_rewards_ads;
    public int total_app_open;
    public string pay_orders;//玩家总共付费次数
    public string pay_total;//玩家总共付费金额

    public int currentPlayLevel;//当前正在进行的关卡

    // 货币
    public Dictionary<string, object> currencies;

    public string lastShopRefreshData;

    public int Experience;
    public int CurrentExpLevel;
    public bool IsEffectOn;
    public bool IsMusicOn;
    public bool IsHapticOn;
    // 地图
    public Dictionary<string, object> totalMapDataDict;
    public Dictionary<string, object> totalMapDataDict2;

    public Dictionary<MergeLevelType, List<string>> rewardItemDic;//临时背包宝盒
    public Dictionary<string, MergeItemDiscoveryState> bookUnlockState; //图鉴解锁情况
    public Dictionary<string, object> storeDic;//背包数据

    //教学
    public bool Tutorial_InnGuide;
    public bool Tutorial_OpenBox;
    public bool Tutorial_InnTask;
    public bool Tutorial_InnGetReward;
    public bool Tutorial_InnBackPack;
    public bool Tutorial_InnEndLevel;
    public bool Tutorial_InnLevelReward1;
    public bool Tutorial_InnLevelReward2;
    public bool Tutorial_InnLevelChest1;
    public bool Tutorial_InnLevelChest2;
    public bool Tutorial_InnStarChest1;
    public bool Tutorial_GetEnergy;
    public bool Tutorial_InnStarChest2;
    public bool Tutorial_OpenDungeon;

    public Dictionary<string, object> mainLineTask;//主线
    public Dictionary<string, object> branchTask;//支线
    public Dictionary<string, object> dungeonSystem;//副本
    public Dictionary<string, object> giftPackage;//礼包
    //转盘
    public string SpinTime;
    public Dictionary<int, object> SpinWheelItemDic;
    //购买体力
    public int BuyEnergyCost = 10;
    public string BuyEnergyTime;
    public string BuyDailyEnergyTime;
    public string BuyMonthlyTime;
    public string ReceiveMonthlyTime;
    public string BuyADTime;

    //购买限购礼包
    public bool BuyLimitGift;

    public int levelReward;//关卡宝箱
    public int starReward;//星星宝箱

    public Dictionary<string, object> battlePass;//battlepass
    public Dictionary<string, object> shopSystem;
    public Dictionary<string, object> dailyTask;//每日任务
    public Dictionary<string, Dictionary<string, object>> festivalDic;//节日活动
    public Dictionary<string, object> chooseSkinDic;//换装系统
    public Dictionary<string, object> achievementDic;//换装系统



    [JsonIgnore]
    public const string CollectionName = "player_data";

    public static PlayerFirestoreData Instance { get; } = new PlayerFirestoreData();


    /// <summary>
    /// 将本地数据转成json，以供上传服务器
    /// </summary>
    /// <returns></returns>
    public string LocalToFirestoreJson()
    {
        try
        {
            needDownload = false;
            //玩家基础数据----------------------------
            dataDate = TimeManager.IsGetServerUtcSuccess ? TimeManager.ServerUtcNow().ToString() : DateTimeOffset.UtcNow.ToString();
            localid = PlayerData.GetLocalPlayerID();
            playerName = PlayerData.GetLocalPlayerName();
            profile = PlayerData.GetLocalPlayerHeadIcon();
            Experience = GameManager.Instance.playerData.Experience;
            CurrentExpLevel = GameManager.Instance.playerData.CurrentExpLevel;
            IsMusicOn = GameManager.Instance.playerData.IsMusicOn;
            IsEffectOn = GameManager.Instance.playerData.IsEffectOn;
            IsHapticOn = GameManager.Instance.playerData.IsHapticOn;

            first_session = GameManager.Instance.playerData.firstPlayDate.ToString();
            last_session = GameManager.Instance.playerData.Last_Active_Day.ToString();
            total_active_days = GameManager.Instance.playerData.Total_Active_Days;
            user_level = TaskGoalsManager.Instance?.curLevelIndex ?? 0;
            total_full_ads = GameManager.Instance.playerData.Total_Full_Ads;
            total_rewards_ads = GameManager.Instance.playerData.Total_Reward_Ads;
            total_app_open = GameManager.Instance.playerData.Total_App_Open_Count;
            pay_orders = GameManager.Instance.playerData.Pay_Orders.ToString();
            pay_total = GameManager.Instance.playerData.Pay_Totals.ToString();

            currentPlayLevel = GameManager.Instance.playerData.CurrentPlayLevel;

            BuyEnergyCost = PlayerData.EnergyCost;
            BuyEnergyTime = PlayerData.BuyEnergyTime.ToString();
            BuyDailyEnergyTime = PlayerData.BuyDailyEnergyTime.ToString();
            SpinTime = PlayerData.SpinWheelFreeTime.ToString();
            SpinWheelItemDic = PlayerData.SpinWheelLocalToServer();
            if (PlayerData.BuyMonthlyCardTime != DateTimeOffset.MinValue)
                BuyMonthlyTime = PlayerData.BuyMonthlyCardTime.ToString();
            else
                BuyMonthlyTime = string.Empty;
            if (PlayerData.ReceiveRewardsTime != DateTimeOffset.MinValue)
                ReceiveMonthlyTime = PlayerData.ReceiveRewardsTime.ToString();
            else
                ReceiveMonthlyTime = string.Empty;
            
            if (PlayerData.BuyADTime != DateTimeOffset.MinValue)
                BuyADTime = PlayerData.BuyADTime.ToString();
            else
                BuyADTime = string.Empty;
            //----------------------------------------
            totalMapDataDict = MergeLevelManager.Instance.GetSaveDataToFirestore(1);//地图数据
            totalMapDataDict2 = MergeLevelManager.Instance.GetSaveDataToFirestore(2);
            currencies = Currencies.GetData_ToFirestore(); //货币
            language = LanguageManager.CurrentLangaugeCode;
            //----------------------------------------
            app_version = RiseSdk.Instance.GetConfig(RiseSdk.CONFIG_KEY_VERSION_NAME);
            firebaseID = FirebaseSystem.Instance.FirebaseID ?? string.Empty;
            //googleId = CloudSystem.IsGoogleConnect ? CloudSystem.GoogleID : string.Empty;
            //facebookId = CloudSystem.IsFacebookConnect ? CloudSystem.FacebookID : string.Empty;
            //appleId = CloudSystem.IsAppleConnect ? CloudSystem.AppleID : string.Empty;
            push_token = RiseSdk.Instance.GetPushToken();
            //-----------------------------------------
            mainLineTask = TaskGoalsManager.Instance.GetSaveDataToFirestore();
            branchTask = BranchSystem.Instance.GetSaveDataToFirestore();
            battlePass = BattlePassSystem.Instance.GetSaveDataToFirestore();
            //dailyTask = DailyTaskSystem.Instance.GetSaveDataToFirestore();
            shopSystem = ShopSystem.Instance.GetSaveDataToFirestore();
            bookUnlockState = BookSaveSystem.Instance.GetSaveDataToFirestore();
            giftPackage = GiftPackageManager.Instance.GetSaveDataToFirestore();
            rewardItemDic = RewardBoxManager.Instance.GetSaveDataToFirestore();
            storeDic = StoreManager.Instance.GetSaveDataToFirestore();
            festivalDic = FestivalSystem.Instance.GetSaveDataToFirestore();
            chooseSkinDic = ChooseSkinSystem.Instance.GetSaveDataToFirestore();
            achievementDic = AchievementManager.Instance.GetSaveDataToFirestore();
            dungeonSystem = DungeonSystem.Instance.GetSaveDataToFirestore();
            //教学-------------------------------
            Tutorial_InnGuide = SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnGuide);
            Tutorial_OpenBox = SaveUtils.GetBool(Consts.SaveKey_Tutorial_OpenBox);
            Tutorial_InnTask = SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnTask);
            Tutorial_InnGetReward = SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnGetReward);
            Tutorial_InnBackPack = SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnBackPack);
            Tutorial_InnEndLevel = SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnEndLevel);
            Tutorial_InnLevelReward1 = SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnLevelReward1);
            Tutorial_InnLevelReward2 = SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnLevelReward2);
            Tutorial_InnLevelChest1 = SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnLevelChest1);
            Tutorial_InnLevelChest2 = SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnLevelChest2);
            Tutorial_InnStarChest1 = SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnStarChest1);
            Tutorial_GetEnergy = SaveUtils.GetBool(Consts.SaveKey_Tutorial_GetEnergy);
            Tutorial_InnStarChest2 = SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnStarChest2);
            Tutorial_OpenDungeon = SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnAdventure1);
            BuyLimitGift= SaveUtils.GetBool(Consts.SaveKey_BuyLimitGift);
            //-----------------------------------
            levelReward = SaveUtils.GetInt(Consts.ChapterRewardLastGotId);
            starReward = SaveUtils.GetInt(Consts.StarRewardLastGotID);


#if UNITY_EDITOR
            if (DebugSetting.CanUseDebugConfig(out SO_DebugConfig sO_Debug))
            {
                if (sO_Debug.textRetreated)
                {
                    string dataStr = JsonConvert.SerializeObject(this);
                    File.WriteAllText("C://MergeInn//JsonData.json", dataStr);
                }
            }
#endif            
            return JsonConvert.SerializeObject(this);
        }
        catch (Exception e)
        {
            LogUtils.LogErrorToSDK(e + "[ConvertLocalDataToJson]");
            return string.Empty;
        }
    }
    /// <summary>
    /// 将服务器数据覆盖到本地
    /// </summary>
    public void FirestoreDataToLocalData()
    {
        GameManager.Instance.ResetPlayerData_ByFirestore(Experience, CurrentExpLevel, IsMusicOn, IsEffectOn, IsHapticOn, first_session, last_session);

        GameManager.Instance.playerData.Total_Active_Days = total_active_days;
        GameManager.Instance.playerData.Total_Full_Ads = total_full_ads;
        GameManager.Instance.playerData.Total_Reward_Ads = total_rewards_ads;
        GameManager.Instance.playerData.Total_App_Open_Count = total_app_open;
        if (int.TryParse(pay_orders, out var payOrderTemp))
        {
            GameManager.Instance.playerData.Pay_Orders = payOrderTemp;
        }
        if (float.TryParse(pay_total, out var pay_totalTemp))
        {
            GameManager.Instance.playerData.Pay_Totals = pay_totalTemp;
        }
        GameManager.Instance.playerData.CurrentPlayLevel = currentPlayLevel;
        GameManager.Instance.ResetPlayerData_Cost(BuyEnergyCost, BuyEnergyTime, BuyDailyEnergyTime, SpinTime, SpinWheelItemDic, BuyMonthlyTime, ReceiveMonthlyTime,BuyADTime);
        PlayerData.SetLocalPlayerID(localid);
        PlayerData.SetPlayerName(playerName);
        PlayerData.SetLocalPlayerHeadIcon(profile);
        if (currencies != null)
        {
            Currencies.ResetData_ByFirestore(currencies);
        }
        LanguageManager.CurrentLangaugeCode = language;

        MergeLevelManager.Instance.SetSaveDataFromFirestore(totalMapDataDict);//地图数据   
        MergeLevelManager.Instance.SetSaveDataFromFirestore(totalMapDataDict2);

        GiftPackageManager.Instance.SetSaveDataFromFirestore(giftPackage);
        BranchSystem.Instance.SetSaveDataFromFirestore(branchTask);
        BattlePassSystem.Instance.SetSaveDataFromFirestore(battlePass);
        //DailyTaskSystem.Instance.SetSaveDataFromFirestore(dailyTask);
        ShopSystem.Instance.SetSaveDataFromFirestore(shopSystem);
        BookSaveSystem.Instance.SetSaveDataFromFirestore(bookUnlockState);
        TaskGoalsManager.Instance.SetSaveDataFromFirestore(mainLineTask);
        RewardBoxManager.Instance.SetSaveDataFromFirestore(rewardItemDic);
        StoreManager.Instance.SetSaveDataFromFirestore(storeDic);
        FestivalSystem.Instance.SetSaveDataFromFirestore(festivalDic);
        ChooseSkinSystem.Instance.SetSaveDataFromFirestore(chooseSkinDic);
        AchievementManager.Instance.SetSaveDataFromFirestore(achievementDic);
        DungeonSystem.Instance.SetSaveDataFromFirestore(dungeonSystem);
        //教学--------------------
        SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnGuide, Tutorial_InnGuide);
        SaveUtils.SetBool(Consts.SaveKey_Tutorial_OpenBox, Tutorial_OpenBox);
        SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnTask, Tutorial_InnTask);
        SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnGetReward, Tutorial_InnGetReward);
        SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnBackPack, Tutorial_InnBackPack);
        SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnEndLevel, Tutorial_InnEndLevel);
        SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnLevelReward1, Tutorial_InnLevelReward1);
        SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnLevelReward2, Tutorial_InnLevelReward2);
        SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnLevelChest1, Tutorial_InnLevelChest1);
        SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnLevelChest2, Tutorial_InnLevelChest2);
        SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnStarChest1, Tutorial_InnStarChest1);
        SaveUtils.SetBool(Consts.SaveKey_Tutorial_GetEnergy, Tutorial_GetEnergy);
        SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnStarChest2, Tutorial_InnStarChest2);
        SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnAdventure1, Tutorial_OpenDungeon);
        SaveUtils.GetBool(Consts.SaveKey_BuyLimitGift, BuyLimitGift);
        //------------------------
        SaveUtils.SetInt(Consts.ChapterRewardLastGotId, levelReward);
        SaveUtils.SetInt(Consts.StarRewardLastGotID, starReward);
    }

    //public void OnReadFirestoreSuccess(string jsonStr)
    //{
    //    if (!string.IsNullOrEmpty(jsonStr) && jsonStr != "{}")
    //    {
    //        try
    //        {
    //            //读取、解析线上存档，并处理
    //            readSuccessCB?.Invoke(jsonStr);
    //        }
    //        catch (Exception e)
    //        {
    //            DebugSetting.LogError(e, "[DownloadFirestoreData error]");
    //        }
    //    }
    //    else
    //    {
    //        //线上没有存档，上传本地存档
    //        GameDebug.LogError("线上没有存档，上传本地存档");
    //        FirebaseSystem.Instance.SetFirestoreAsync(CollectionName, LocalToFirestoreJson(), PlayerFirestoreData.Instance.OnSetFirestoreSuccess, null);
    //    }
    //}

    // private List<Action> SetPlayerDataSuccessCBList = new List<Action>();

    //public void SetPlayerDataSuccessCB(Action cb)
    //{
    //    SetPlayerDataSuccessCBList.Add(cb);
    //}

    //public void OnUpdateFirestoreSuccess(string translationId)
    //{
    //    if (translationId == "needDownload")
    //    {
    //        CloudSystem.canUploadData = true;
    //    }

    //    updateSuccessCB?.Invoke(translationId);
    //}

    public void OnSetFirestoreSuccess()
    {

        if (!string.IsNullOrEmpty(dataDate) && DateTimeOffset.TryParse(dataDate, out var lastUpdateDate))
        {
            FirestoreDataDate = lastUpdateDate;
        }
        else
        {
            FirestoreDataDate = DateTimeOffset.MinValue;
        }
    }

}
