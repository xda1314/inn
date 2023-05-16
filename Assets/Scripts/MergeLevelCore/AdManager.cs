using System;
using UnityEngine;
using System.Collections.Generic;
using Ivy;
using Newtonsoft.Json;

public class AdManager
{
    public class ADData
    {
        public string adKey;
        public DateTimeOffset lastWatchDate;
        public int todayWatchTimes;//数据刷新可能不及时，不能直接取用
        public int totalWatchTimes;

        public ADData(string key)
        {
            this.adKey = key;
            lastWatchDate = DateTimeOffset.MinValue;
            todayWatchTimes = 0;
            totalWatchTimes = 0;
        }
    }


    public enum ADTag
    {
        None,
        ChestRoom,
        Energy,
        RefreshDiscount,
        ShopDaily,
        TaskDoubleReward,
        DoubleStarReward,
        DoubleLevelReward,
        ClearItemCD,

        app_open,
        complete_orders,
        enter_level,
        exit_level,
        finish_level,

        dailydeals_EnergyChest,
        dailydeals_coin,
        dailydeals_gem,
        dailydeals_needle,
        InAppMessage,
    }


    //仅限内部使用 adKey
    private static Dictionary<string, ADData> adDataDict = new Dictionary<string, ADData>();


    private static Action<RiseSdk.AdEventType, int, string, int> completedCB = null;
    public static event Action playAdEvent;
    /// <summary>
    /// 播放普通广告
    /// </summary>
    /// <param name="adTag">广告tag，（供打点使用）</param>
    /// <param name="completedCB">广告播放完成回调（广告完整播放结束）</param>
    /// <param name="saveAdKey">广告ID</param>
    ///  <param name="offsetX">btnWorldPos偏移量</param>
    public static void PlayAd(Vector3 btnWorldPos, ADTag adTag, Action completedCB, string saveAdKey = "", Action failCB = null)
    {
        if (adTag == ADTag.None)
        {
            GameDebug.LogError("请选择一个广告tag或者添加一个新的");
        }

        GameDebug.Log("PlayAd");
        if (!CanShowAD_Normal(adTag))
        {
            TextTipSystem.Instance.ShowTip(btnWorldPos, I2.Loc.ScriptLocalization.Get("Obj/Chain/WatchVideo_Text5"), TextTipColorType.Yellow);
            GameDebug.Log("TestAd-------------- AD Times Exhausted!");
            return;
        }

        if (PlayerData.BuyADTime != DateTimeOffset.MinValue &&
        PlayerData.BuyADTime.AddDays(30).ToUnixTimeMilliseconds() > TimeManager.Instance.UtcNow().ToUnixTimeMilliseconds())
        {
            if (GameManager.Instance != null && GameManager.Instance.playerData != null)
            {
                GameManager.Instance.playerData.Total_Reward_Ads++;
                RiseSdk.Instance.SetUserProperty("total_reward_ads", GameManager.Instance.playerData.Total_Reward_Ads.ToString());
            }

            MinusAdTimes(adTag);
            playAdEvent?.Invoke();
            SaveADData(saveAdKey);
            completedCB?.Invoke();
            return;
        }

        if (!RiseSdk.Instance.HasRewardAd())
        {
            UISystem.Instance.ShowUI(new UIPanelData_LoadingAD(UIShowLayer.TopPopup, () =>
            {
                PlayAd(btnWorldPos, adTag, completedCB, saveAdKey, failCB);
            }, () =>
            {
                TextTipSystem.Instance.ShowTip(btnWorldPos, I2.Loc.ScriptLocalization.Get("Obj/Chain/WatchVideo_Text8"), TextTipColorType.Yellow);
                failCB?.Invoke();
                GameDebug.Log("TestAd-------------- Sdk Has No AD!");
            }));
            return;
        }

        GameManager.Instance.SavePlayerData();
        if (AdManager.completedCB != null)
        {
            RiseSdkListener.OnAdEvent -= AdManager.completedCB;
        }
        AdManager.completedCB = (RiseSdk.AdEventType adEventType, int a, string b, int c) =>
        {
            AudioManager.Instance.UnPauseBGM();
            if (adEventType == RiseSdk.AdEventType.RewardAdShowFinished)
            {
                if (GameManager.Instance != null && GameManager.Instance.playerData != null)
                {
                    GameManager.Instance.playerData.Total_Reward_Ads++;
                    RiseSdk.Instance.SetUserProperty("total_reward_ads", GameManager.Instance.playerData.Total_Reward_Ads.ToString());
                }

                MinusAdTimes(adTag);
                playAdEvent?.Invoke();
                SaveADData(saveAdKey);
                completedCB?.Invoke();
            }
        };
        RiseSdkListener.OnAdEvent += AdManager.completedCB;

        GameDebug.Log(Enum.GetName(typeof(ADTag), adTag));
        RiseSdk.Instance.ShowRewardAd(Enum.GetName(typeof(ADTag), adTag), 1);
        SaveUtils.SetInt("showADCount", SaveUtils.GetInt("showADCount", 0)+1);

        AudioManager.Instance.PauseBGM();
    }


    public static void PlayFullAd(ADTag adTag)
    {
        RiseSdk.Instance.ShowAd(Enum.GetName(typeof(ADTag), adTag));
    }


    /// <summary>
    /// 是否可以展示普通广告
    /// </summary>
    public static bool CanShowAD_Normal(ADTag aDTag = ADTag.None)
    {
        if (!HasAdTimes(aDTag))
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 是否还有剩余播放广告的次数
    /// </summary>
    /// <returns></returns>
    private static bool HasAdTimes(ADTag aDTag)
    {
        RefreshCurrentDiscountADTimes();
        if (adWatchCountDic != null && adWatchCountDic.TryGetValue(aDTag.ToString(), out int count))
        {
            return count > 0 && remainDiscountADTimes > 0;
        }
        return remainDiscountADTimes > 0;
    }

    private static void MinusAdTimes(ADTag aDTag)
    {
        remainDiscountADTimes--;
        if (refreshLastDateTime == DateTimeOffset.MinValue)
        {
            refreshLastDateTime = TimeManager.Instance.UtcNow();
        }
        if (adWatchCountDic != null && adWatchCountDic.TryGetValue(aDTag.ToString(), out int count))
        {
            adWatchCountDic[aDTag.ToString()] = count - 1;
            if (adWatchCountDic[aDTag.ToString()] <= 0 && aDTag == ADTag.Energy)
                refreshLastDateTime = TimeManager.Instance.UtcNow();
        }
        SaveDiscountADData();
    }

    public static bool ManualEnterMergePanel = false;
    public static bool ManualExitMergePanel = false;

    private static DateTimeOffset lastPlayInterstitialADTime = DateTimeOffset.MinValue;
    /// <summary>
    /// 尝试播放Interstitial广告
    /// </summary>
    public static void TryPlayInterstitialAD(ADTag adTag, string remoteKey, Action completedCB = null)
    {
#if UNITY_EDITOR
        return;
#endif

        int open = RiseSdk.Instance.GetRemoteConfigInt(remoteKey);
        if (open == 1)
        {
            //插屏广告最小间隔时间
            int remoteTime = RemoteConfigSystem.Instance.GetRemoteConfig_Int(RemoteConfigSystem.remoteKey_full_min_dur);
            int minIntervalTime = remoteTime > 0 ? remoteTime : 30;
            if (lastPlayInterstitialADTime == DateTimeOffset.MinValue
                || (int)(TimeManager.Instance.UtcNow() - lastPlayInterstitialADTime).TotalSeconds >= minIntervalTime)
            {
                lastPlayInterstitialADTime = TimeManager.Instance.UtcNow();
                PlayInterstitial(adTag, completedCB);
            }
        }
    }

    /// <summary>
    /// 是否可以展示Interstitial广告
    /// </summary>
    public static bool CanShowAD_RewardedInterstitial()
    {
        return RiseSdk.Instance.HasRewardedInterstitial();
    }


    /// <summary>
    /// 播放Interstitial广告
    /// </summary>
    public static void PlayInterstitial(ADTag adTag, Action completedCB)
    {
        GameManager.Instance.SavePlayerData();
        if (AdManager.completedCB != null)
        {
            RiseSdkListener.OnAdEvent -= AdManager.completedCB;
        }

        AdManager.completedCB = (RiseSdk.AdEventType adEventType, int a, string b, int c) =>
        {
            if (adEventType == RiseSdk.AdEventType.FullAdShown)
            {
                if (GameManager.Instance != null && GameManager.Instance.playerData != null)
                {
                    GameManager.Instance.playerData.Total_Full_Ads++;
                    RiseSdk.Instance.SetUserProperty("total_full_ads", GameManager.Instance.playerData.Total_Full_Ads.ToString());
                }

                completedCB?.Invoke();
            }
        };
        RiseSdkListener.OnAdEvent += AdManager.completedCB;
        RiseSdk.Instance.ShowAd(Enum.GetName(typeof(ADTag), adTag));
    }






    /// <summary>
    /// 获取上一次观看广告时间
    /// </summary>
    public static bool TryGetADtWatchData(string adKey, out DateTimeOffset lastDate, out int todayTimes, out int totalTimes)
    {
        if (!adDataDict.ContainsKey(adKey))
        {
            LoadADData(adKey);
        }

        if (adDataDict.TryGetValue(adKey, out ADData data))
        {
            lastDate = data.lastWatchDate;
            if (ExtensionTool.IsDateBeforeToday(lastDate, TimeManager.Instance.UtcNow()))
            {
                todayTimes = 0;
            }
            else
            {
                todayTimes = data.todayWatchTimes;
            }
            totalTimes = data.totalWatchTimes;
            return true;
        }

        lastDate = DateTimeOffset.MinValue;
        todayTimes = 0;
        totalTimes = 0;
        return false;
    }

    /// <summary>
    /// 剩余观看广告时间
    /// </summary>
    /// <returns></returns>
    public static int LeftWatchADTime()
    {
        int leftTime = (int)(refreshLastDateTime.AddHours(1) - TimeManager.Instance.UtcNow()).TotalSeconds;
        return leftTime > 0 ? leftTime : 0;
    }

    public static bool JustifyCanWatchADNow(string adKey, TimeSpan interval, int dayMaxTimes = -1)
    {
        if (TryGetADtWatchData(adKey, out DateTimeOffset lastTime, out int todayTimes, out int totalTimes))
        {
            TimeSpan span = TimeManager.Instance.UtcNow() - lastTime;
            if (span.TotalSeconds < interval.TotalSeconds)
            {
                return false;
            }

            if (dayMaxTimes >= todayTimes)
            {
                return false;
            }
        }
        return true;
    }

    private static void LoadADData(string adKey)
    {
        if (string.IsNullOrEmpty(adKey))
        {
            GameDebug.LogError($"adKey is empty. adKey:{adKey} ");
            return;
        }

        if (SaveUtils.HasKey(adKey))
        {
            ADData aDData = new ADData(adKey);
            long ticks = SaveUtils.GetLong(adKey, aDData.lastWatchDate.Ticks);
            aDData.lastWatchDate = new DateTimeOffset(ticks, TimeSpan.Zero);
            if (ExtensionTool.IsDateBeforeToday(aDData.lastWatchDate, TimeManager.Instance.UtcNow()))
            {
                aDData.todayWatchTimes = 0;
            }
            else
            {
                aDData.todayWatchTimes = SaveUtils.GetInt(Consts.AD_TodayTimes + adKey);
            }

            aDData.totalWatchTimes = SaveUtils.GetInt(Consts.AD_TotalTimes + adKey);
            adDataDict[adKey] = aDData;
        }
        else
        {
            ADData aDData = new ADData(adKey);
            adDataDict[adKey] = aDData;
        }

    }

    private static void SaveADData(string adKey)
    {
        if (string.IsNullOrEmpty(adKey))
        {
            GameDebug.Log($"adKey is empty. adKey:{adKey} ");
            return;
        }

        if (!adDataDict.TryGetValue(adKey, out ADData aDData))
        {
            aDData = new ADData(adKey);
            adDataDict[adKey] = aDData;
        }

        DateTimeOffset currentTime = TimeManager.Instance.UtcNow();
        if (ExtensionTool.IsDateBeforeToday(aDData.lastWatchDate, currentTime))
        {
            aDData.todayWatchTimes = 1;
        }
        else
        {
            aDData.todayWatchTimes++;
        }
        aDData.totalWatchTimes++;
        aDData.lastWatchDate = currentTime;

        SaveUtils.SetLong(adKey, aDData.lastWatchDate.Ticks);
        string todayTimes = Consts.AD_TodayTimes + adKey;
        SaveUtils.SetInt(todayTimes, aDData.todayWatchTimes);
        string totalTimes = Consts.AD_TotalTimes + adKey;
        SaveUtils.SetInt(totalTimes, aDData.totalWatchTimes);

    }







    #region 广告刷新时间存储
    private static bool hasLoadData = false;
    public static DateTimeOffset refreshLastDateTime;
    public static int remainDiscountADTimes;
    private static Dictionary<string, int> adWatchCountDic = new Dictionary<string, int>();

    public static void RefreshCurrentDiscountADTimes()
    {
        if (!hasLoadData)
        {
            try
            {
                hasLoadData = true;
                if (SaveUtils.HasKey(Consts.SaveKey_DiscountRefreshLastDate))
                {
                    long tick = SaveUtils.GetLong(Consts.SaveKey_DiscountRefreshLastDate);
                    if (tick > 0)
                    {
                        refreshLastDateTime = new DateTimeOffset(tick, TimeSpan.Zero);
                    }
                    else
                    {
                        refreshLastDateTime = DateTimeOffset.MinValue;
                    }
                }
                else
                {
                    refreshLastDateTime = DateTimeOffset.MinValue;
                }

                if (SaveUtils.HasKey(Consts.SaveKey_DiscountRemainADTimes))
                {
                    remainDiscountADTimes = SaveUtils.GetInt(Consts.SaveKey_DiscountRemainADTimes);
                }
                else
                {
                    remainDiscountADTimes = 25;
                }

                string adwatch = SaveUtils.GetString(Consts.SaveKey_ADTagTimes, "");
                if (string.IsNullOrEmpty(adwatch))
                {
                    adWatchCountDic = new Dictionary<string, int>();

                }
                else
                {
                    var dic = JsonConvert.DeserializeObject<Dictionary<string, int>>(adwatch);
                    if (dic != null && dic.Count != 0)
                    {
                        adWatchCountDic = dic;
                    }
                    else
                    {
                        adWatchCountDic = new Dictionary<string, int>();

                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("[LoadDiscountADData]" + e);
            }
        }

        if (remainDiscountADTimes == 0 && refreshLastDateTime == DateTimeOffset.MinValue
            || refreshLastDateTime.AddHours(1) <= TimeManager.Instance.UtcNow())
        {
            refreshLastDateTime = DateTimeOffset.MinValue;
            remainDiscountADTimes = RemoteConfigSystem.Instance.GetRemoteConfig_Int(RemoteConfigSystem.remoteKey_reward_ads_times);
            if (remainDiscountADTimes <= 0)
            {
                remainDiscountADTimes = 25;
            }


            adWatchCountDic["ClearItemCD"] = 2;
            adWatchCountDic["Energy"] = 4;

#if !UNITY_EDITOR
            string str = RemoteConfigSystem.Instance.GetRemoteConfig_String(RemoteConfigSystem.remoteKey_ads_limit);
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    var dic = JsonConvert.DeserializeObject<Dictionary<string, int>>(str);
                    if (dic != null && dic.Count > 0)
                    {
                        foreach (var item in dic)
                        {
                            adWatchCountDic[item.Key] = item.Value;
                        }
                    }
                }
                catch (Exception e)
                {
                    LogUtils.LogError("ads_limit json error:" + e);
                }
            }
#endif

            // 本地限制只有一次
            SaveDiscountADData();
        }
    }



    private static void SaveDiscountADData()
    {
        try
        {
            if (refreshLastDateTime == DateTimeOffset.MinValue)
            {
                SaveUtils.DeleteKey(Consts.SaveKey_DiscountRefreshLastDate);
            }
            else
            {
                SaveUtils.SetLong(Consts.SaveKey_DiscountRefreshLastDate, refreshLastDateTime.Ticks);
            }
            SaveUtils.SetInt(Consts.SaveKey_DiscountRemainADTimes, remainDiscountADTimes);

            if (adWatchCountDic == null || adWatchCountDic.Count == 0)
            {
                SaveUtils.SetString(Consts.SaveKey_ADTagTimes, "");
            }
            else
            {
                SaveUtils.SetString(Consts.SaveKey_ADTagTimes, JsonConvert.SerializeObject(adWatchCountDic));
            }
        }
        catch (Exception e)
        {
            LogUtils.LogError("[SaveDiscountADData]" + e);
        }
    }

#endregion


}
