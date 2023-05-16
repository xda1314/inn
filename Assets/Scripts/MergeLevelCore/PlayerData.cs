using Ivy.Firebase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

/// <summary>
/// 玩家基础数据
/// </summary>
public class PlayerData
{
    public int Experience { get; private set; }//总经验
    public int CurrentExpLevel { get; private set; } = 1;//当前的经验等级
    public bool IsEffectOn { get; set; } = true;//默认开启声音
    public bool IsMusicOn { get; set; } = true;//默认开启音乐
    public bool IsHapticOn { get; set; } = true;//默认开启震动
    public DateTimeOffset firstPlayDate;//第一次进入游戏时间
    public bool isFirstEnterLevel { get; set; } = true;//是否是第一次进入合成界面

    private static string LocalplayerID;
    private static string LocalPlayerName;
    private static string LocalPlayerHeadIcon;

    public static int EnergyCost { get; set; } = 5;//购买体力消费
    public static DateTimeOffset BuyEnergyTime { get; set; } = DateTimeOffset.MinValue;    //购买体力的时间
    public static DateTimeOffset BuyDailyEnergyTime { get; set; } = DateTimeOffset.MinValue;   //购买每日体力礼包的时间
    public static DateTimeOffset SpinWheelFreeTime { get; set; } = DateTimeOffset.MinValue;   //每日转盘时间
    public static DateTimeOffset BuyMonthlyCardTime { get; set; } = DateTimeOffset.MinValue;    //购买月卡时间
    public static DateTimeOffset ReceiveRewardsTime { get; set; } = DateTimeOffset.MinValue;    //领取月卡奖励时间(用于判断每日是否领取过月卡奖励)
    public static DateTimeOffset BattlePass_BuyAllItem { get; set; } = DateTimeOffset.MinValue; //BattlePass是否购买一键购买物品

    private static DateTimeOffset buyADTime = DateTimeOffset.MinValue; //购买广告时间
    public static DateTimeOffset BuyADTime
    {
        get { return buyADTime; }
        set { buyADTime = value; }
    }

    //已经领取过的经验
    public int CollectedExp
    {
        get
        {
            if (CurrentExpLevel <= 1)
            {
                return 0;
            }

            if (MergeLevelDefinition.LevelDefinitionDict.TryGetValue((CurrentExpLevel - 1).ToString(), out var def))
            {
                return def.AllExp;
            }
            return 0;
        }
    }

    //下一个等级需要的总经验
    public int NextExpLevelNeedExp
    {
        get
        {
            if (MergeLevelDefinition.LevelDefinitionDict.TryGetValue(CurrentExpLevel.ToString(), out var def))
            {
                return def.CountExp;
            }
            return 0;
        }
    }

    //未领取过的经验
    public int UnCollectedExp => Experience > CollectedExp ? (Experience - CollectedExp) : 0;

    /// <summary>
    /// 是否可以领取奖励
    /// </summary>
    /// <param name="rewardItem"></param>
    /// <returns></returns>
    public bool CanCollectedExp(out List<MergeRewardItem> levelRewardList)
    {
        levelRewardList = new List<MergeRewardItem>();
        if (MergeLevelDefinition.LevelDefinitionDict.TryGetValue((CurrentExpLevel).ToString(), out var def))
        {
            levelRewardList = def.levelRewardList;
            if (Experience >= def.CountExp)
            {
                return true;
            }
        }
        return false;
    }

    //领取一级奖励
    public bool CollectedExpOneLevel()
    {
        if (CanCollectedExp(out _))
        {
            CurrentExpLevel++;
            GameManager.Instance.SavePlayerData_Exp();
            GameManager.Instance.RefreshCurrency_Exp();
            AnalyticsUtil.trackMainLine("PlayerLv", CurrentExpLevel);
            //MapManager.Current.UpdateViews();

            AchievementManager.Instance.UpdateAchievement(AchievementType.levels, CurrentExpLevel);
            return true;
        }
        return false;
    }

    public bool IsShowADGift()
    {
        return BuyADTime == DateTimeOffset.MinValue
        && SaveUtils.GetInt("showADCount", 0) % 50 == 1;
    }

    /// <summary>
    /// 是否已经够买ad礼包
    /// </summary>
    /// <returns></returns>
    public bool IsBuyADGift()
    {
        return PlayerData.BuyADTime != DateTimeOffset.MinValue &&
        PlayerData.BuyADTime.AddDays(30).ToUnixTimeMilliseconds() > TimeManager.Instance.UtcNow().ToUnixTimeMilliseconds();
    }

    /// <summary>
    /// 玩家ID
    /// </summary>
    public static string GetLocalPlayerID()
    {
        if (string.IsNullOrEmpty(LocalplayerID))
        {
            string temp = SaveUtils.GetString(Consts.SaveKey_PlayerID);
            if (!string.IsNullOrEmpty(temp))
            {
                LocalplayerID = temp;
            }
            else
            {
                LocalplayerID = ExtensionTool.GenerateUniqueID();
                SaveUtils.SetString(Consts.SaveKey_PlayerID, LocalplayerID);
            }
        }
        return LocalplayerID;
    }
    public static void SetLocalPlayerID(string id)
    {
        LocalplayerID = id;
    }

    public static void TrySetLoginpLatformDefaultName()
    {
        string playerName = string.Empty;
        if (FirebaseSystem.Instance.IsAppleLinked)
        {
            string appleStr = FirebaseSystem.Instance.GetProviderLinkedDisplayName(FirebaseSystem.AccountProvider_Apple);
            playerName = appleStr;
        }
        else if (FirebaseSystem.Instance.IsGooglePlayGamesLinked)
        {
            string googleStr = FirebaseSystem.Instance.GetProviderLinkedDisplayName(FirebaseSystem.AccountProvider_GooglePlayGames);
            playerName = googleStr;
        }
        else if (FirebaseSystem.Instance.IsFacebookLinked)
        {
            string facebookStr = FirebaseSystem.Instance.GetProviderLinkedDisplayName(FirebaseSystem.AccountProvider_Facebook);
            playerName = facebookStr;
        }
        else if (FirebaseSystem.Instance.IsGameCenterLinked)
        {
            string appleCenterStr = FirebaseSystem.Instance.GetProviderLinkedDisplayName(FirebaseSystem.AccountProvider_GameCenter);
            playerName = appleCenterStr;
        }
        if (!string.IsNullOrEmpty(playerName))
            SetPlayerName(playerName);
    }
    public static void SetPlayerName(string playerName)
    {
        if (string.IsNullOrEmpty(playerName))
        {
            return;
        }
        LocalPlayerName = playerName;
        SaveUtils.SetString(Consts.SaveKey_PlayerName, playerName);
    }
    public static string GetLocalPlayerName()
    {
        if (string.IsNullOrEmpty(LocalPlayerName))
        {
            string temp = SaveUtils.GetString(Consts.SaveKey_PlayerName);
            if (!string.IsNullOrEmpty(temp))
            {
                LocalPlayerName = temp;
            }
            else
            {
                //LocalPlayerName = I2.Loc.ScriptLocalization.Get("Obj/Set/NameButton/Text1") + "_" + LocalplayerID.Substring(LocalplayerID.Length - 8);
                LocalPlayerName = "Player" + "_" + LocalplayerID.Substring(LocalplayerID.Length - 6);
            }
        }
        return LocalPlayerName;
    }
    public static void SetLocalPlayerHeadIcon(string playerHeadIcon)
    {
        if (string.IsNullOrEmpty(playerHeadIcon))
        {
            return;
        }
        LocalPlayerHeadIcon = playerHeadIcon;
        SaveUtils.SetString(Consts.SaveKey_PlayerHeadIcon, playerHeadIcon);
    }
    public static string GetLocalPlayerHeadIcon()
    {
        if (string.IsNullOrEmpty(LocalPlayerHeadIcon))
        {
            string temp = SaveUtils.GetString(Consts.SaveKey_PlayerHeadIcon);
            if (!string.IsNullOrEmpty(temp))
            {
                LocalPlayerHeadIcon = temp;
            }
            else
            {
                LocalPlayerHeadIcon = "Avatar6";
            }
        }
        return LocalPlayerHeadIcon;
    }
    public static string GetLocalPlayerIDDisplay()
    {
        return GetLocalPlayerID().Insert(GetLocalPlayerID().Length / 2, "\n");
    }


    public PlayerData(int experience, int currentExpLevel)
    {
        Experience = experience;
        CurrentExpLevel = currentExpLevel;
        GetLocalPlayerID();
    }

    public void ResetData(int value, int currentExpLevel, bool iseffectOn, bool isMusicOn, bool IsHapticOn, string firstSession, string lastSession)
    {
        Experience = value;
        CurrentExpLevel = currentExpLevel;
        IsEffectOn = iseffectOn;
        IsMusicOn = isMusicOn;
        this.IsHapticOn = IsHapticOn;
        VibrateSystem.SetHapticsActive(IsHapticOn);
        if (DateTimeOffset.TryParse(firstSession, null, System.Globalization.DateTimeStyles.AssumeUniversal, out firstPlayDate))
        {

        }
        if (DateTimeOffset.TryParse(lastSession, null, System.Globalization.DateTimeStyles.AssumeUniversal, out var tempDate))
        {
            Last_Active_Day = tempDate;
        }
    }

    public void AddExperience(int delta)
    {
        if (delta <= 0)
        {
            return;
        }
        Experience += delta;
        GameManager.Instance.SavePlayerData_Exp();

        GameDebug.Log($"增加经验：{delta}, 总经验：{Experience}, 经验等级：{CurrentExpLevel}");
        GameDebug.Log($"未领取的经验：{UnCollectedExp}, 下一经验等级需要的经验:{NextExpLevelNeedExp}");
    }

    /// <summary>
    /// 刷新购买体力数据
    /// </summary>
    /// <returns></returns>
    public void TryRefreshBuyEnergyData()
    {
        if (!ExtensionTool.IsDateToday(BuyEnergyTime, TimeManager.ServerUtcNow()))
        {
            EnergyCost = 5;
            BuyEnergyTime = TimeManager.ServerUtcNow();
            GameManager.Instance.SavePlayerData_EnergyCost();
            GameManager.Instance.SavePlayerData_BuyEnergyTime();
        }
    }

    public void TryRefreshBuyADTimeData()
    {
        string buyADTime = SaveUtils.GetString(Consts.SaveKey_PlayerData_BuyADTime);
        if (!string.IsNullOrEmpty(buyADTime) &&
           DateTimeOffset.TryParse(buyADTime, out DateTimeOffset buyADTimeDate))
        {
            BuyADTime = buyADTimeDate;
        }
    }

    /// <summary>
    /// 当天是否购买体力礼包
    /// </summary>
    /// <returns>true为未购买过，false为未购买</returns>
    public bool BuyEnergyDailyCurrentDay()
    {
        if (BuyDailyEnergyTime == DateTimeOffset.MinValue)
            return false;
        else if (!ExtensionTool.IsDateToday(BuyDailyEnergyTime, TimeManager.ServerUtcNow()))
        {
            return false;
        }
        else
            return true;
    }


    #region 转盘云存档
    public static Dictionary<int, object> SpinWheelLocalToServer()
    {
        try
        {
            Dictionary<int, object> dic = new Dictionary<int, object>();
            Dictionary<int, SpinWheelReward> curRewardDic = JsonConvert.DeserializeObject<Dictionary<int, SpinWheelReward>>(SaveUtils.GetString(Consts.SaveKey_PlayerData_SpinWheelItems));
            foreach (var item in curRewardDic)
            {
                Dictionary<string, object> dataDic = new Dictionary<string, object>();
                dataDic.Add("count", item.Value.count);
                dataDic.Add("name", item.Value.rewardName);
                dataDic.Add("weight", item.Value.weight);
                dic.Add(item.Key, dataDic);
            }
            return dic;
        }
        catch (Exception e)
        {
            return new Dictionary<int, object>();
        }
    }
    public static void SpinWheelServerToLocal(Dictionary<int, object> dic)
    {
        if (dic == null || dic.Count == 0)
        {
            return;
        }
        Dictionary<int, SpinWheelReward> dataDic = new Dictionary<int, SpinWheelReward>();
        foreach (var item in dic)
        {
            SpinWheelReward spinWheelReward = new SpinWheelReward();
            Dictionary<string, object> resultDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(item.Value.ToString());
            if (resultDic.TryGetValue("name", out object nameStr))
            {
                spinWheelReward.rewardName = nameStr.ToString();
            }
            if (resultDic.TryGetValue("count", out object countStr) && int.TryParse(countStr.ToString(), out int count))
            {
                spinWheelReward.count = count;
            }
            if (resultDic.TryGetValue("weight", out object weightStr) && int.TryParse(weightStr.ToString(), out int weight))
            {
                spinWheelReward.weight = weight;
            }
            dataDic[item.Key] = spinWheelReward;
        }
        SaveUtils.SetString(Consts.SaveKey_PlayerData_SpinWheelItems, JsonConvert.SerializeObject(dataDic));
    }
    #endregion



    [JsonIgnore]
    private int _PayOrders = -1;
    /// <summary>
    /// 付费次数
    /// </summary>
    [JsonIgnore]
    public int Pay_Orders
    {
        get
        {
            if (_PayOrders < 0)
            {
                _PayOrders = SaveUtils.GetInt("player_pay_order", 0);
            }
            return _PayOrders;
        }
        set
        {
            if (value < 0)
                return;
            _PayOrders = value;
            SaveUtils.SetInt("player_pay_order", _PayOrders);
        }
    }
    [JsonIgnore]
    private float _Pay_Totals = -1;
    /// <summary>
    /// 总付费金额
    /// </summary>
    [JsonIgnore]
    public float Pay_Totals
    {
        get
        {
            if (_Pay_Totals < 0)
            {
                _Pay_Totals = SaveUtils.GetFloat("player_pay_total", 0);
            }
            return _Pay_Totals;
        }
        set
        {
            if (value < 0)
                return;
            _Pay_Totals = value;
            SaveUtils.SetFloat("player_pay_total", _Pay_Totals);
        }
    }

    [JsonIgnore]
    private DateTimeOffset _Last_Active_Day = DateTimeOffset.MinValue;
    /// <summary>
    /// 上次活跃日期
    /// </summary>
    [JsonIgnore]
    public DateTimeOffset Last_Active_Day
    {
        get
        {
            if (_Last_Active_Day == DateTimeOffset.MinValue)
            {
                string dateStr = SaveUtils.GetString("Last_Active_Day", "");
                if (!string.IsNullOrEmpty(dateStr))
                    DateTimeOffset.TryParse(dateStr, out _Last_Active_Day);
            }
            return _Last_Active_Day;
        }
        set
        {
            _Last_Active_Day = value;
            SaveUtils.SetString("Last_Active_Day", _Last_Active_Day.ToString());
        }
    }
    [JsonIgnore]
    private int _Total_Active_Days = -1;
    /// <summary>
    /// 活跃天数
    /// </summary>
    [JsonIgnore]
    public int Total_Active_Days
    {
        get
        {
            if (_Total_Active_Days < 0)
            {
                _Total_Active_Days = SaveUtils.GetInt("total_active_days", 0);
            }
            return _Total_Active_Days;
        }
        set
        {
            if (value < 0)
                return;
            _Total_Active_Days = value;
            SaveUtils.SetInt("total_active_days", _Total_Active_Days);
        }
    }
    [JsonIgnore]
    private int _Total_App_Open_Count = -1;
    /// <summary>
    /// 游戏开启次数
    /// </summary>
    [JsonIgnore]
    public int Total_App_Open_Count
    {
        get
        {
            if (_Total_App_Open_Count < 0)
            {
                _Total_App_Open_Count = SaveUtils.GetInt("user_total_app_open", 0);
            }
            return _Total_App_Open_Count;
        }
        set
        {
            if (value < 0)
                return;
            _Total_App_Open_Count = value;
            SaveUtils.SetInt("user_total_app_open", _Total_App_Open_Count);
        }
    }


    [JsonIgnore]
    private int _Total_Full_Ads = -1;
    /// <summary>
    /// 插屏广告次数
    /// </summary>
    [JsonIgnore]
    public int Total_Full_Ads
    {
        get
        {
            if (_Total_Full_Ads < 0)
            {
                _Total_Full_Ads = SaveUtils.GetInt("total_full_ads", 0);
            }
            return _Total_Full_Ads;
        }
        set
        {
            if (value < 0)
                return;
            _Total_Full_Ads = value;
            SaveUtils.SetInt("total_full_ads", _Total_Full_Ads);
        }
    }

    [JsonIgnore]
    private int _Total_Reward_Ads = -1;
    /// <summary>
    /// 激励广告次数
    /// </summary>
    [JsonIgnore]
    public int Total_Reward_Ads
    {
        get
        {
            if (_Total_Reward_Ads < 0)
            {
                _Total_Reward_Ads = SaveUtils.GetInt("total_reward_ads", 0);
            }
            return _Total_Reward_Ads;
        }
        set
        {
            if (value < 0)
                return;
            _Total_Reward_Ads = value;
            SaveUtils.SetInt("total_reward_ads", _Total_Reward_Ads);
        }
    }



    [JsonIgnore]
    public int CurrentPlayLevel //玩家关卡等级
    {
        get
        {
            return SaveUtils.GetInt("current_play_level", 0);
        }
        set
        {
            int _CurrentPlayLevel = value;
            SaveUtils.SetInt("current_play_level", _CurrentPlayLevel);
        }
    }

}
