using System;
using System.Collections.Generic;
using UnityEngine;

public static class Currencies
{
    public static bool hasInit { get; private set; } = false;

    /// <summary>
    /// 货币容量
    /// </summary>
    private static Dictionary<CurrencyID, int> CurrencyAmounts = new Dictionary<CurrencyID, int>();
    public static DateTimeOffset energyStartCountDate { get; private set; } = DateTimeOffset.MaxValue;
    public static DateTimeOffset dungeonEnergyStartCountDate { get; private set; } = DateTimeOffset.MaxValue;

    private const int startCount_Coins = 100;
    private const int startCount_Gems = 100;
    private const int startCount_Energy = 100;
    private const int startCount_Needle = 0;
    //private const int startCount_DungeonEnergy = 100;


    public static int Coins
    {
        get
        {
            if (CurrencyAmounts.TryGetValue(CurrencyID.Coins, out int amount))
            {
                return amount;
            }
            return 0;
        }
    }

    public static int Gems
    {
        get
        {
            if (CurrencyAmounts.TryGetValue(CurrencyID.Gems, out int amount))
            {
                return amount;
            }
            return 0;
        }
    }

    public static int Energy
    {
        get
        {
            if (CurrencyAmounts.TryGetValue(CurrencyID.Energy, out int amount))
            {
                return amount;
            }
            return 0;
        }
    }
    public static int Needle
    {
        get
        {
            if (CurrencyAmounts.TryGetValue(CurrencyID.Needle, out int amount))
            {
                return amount;
            }
            return 0;
        }
    }

    public static void Init()
    {
        hasInit = true;
        CurrencyAmounts.Clear();
        CurrencyAmounts.Add(CurrencyID.Coins, 0);
        CurrencyAmounts.Add(CurrencyID.Gems, 0);
        CurrencyAmounts.Add(CurrencyID.Energy, 0);
        CurrencyAmounts.Add(CurrencyID.Needle, 0);
        LoadData();
    }

    private static void LoadData()
    {
        try
        {
            //金币
            if (SaveUtils.HasKey(Consts.SaveKey_Currency_Coins))
            {
                int coinsTemp = SaveUtils.GetInt(Consts.SaveKey_Currency_Coins);
                SetCurrencyAmount(CurrencyID.Coins, coinsTemp);
            }
            else
            {
                SetCurrencyAmount(CurrencyID.Coins, startCount_Coins);
                SaveData(false, CurrencyID.Coins);
            }
            //钻石
            if (SaveUtils.HasKey(Consts.SaveKey_Currency_Gems))
            {
                int gemsTemp = SaveUtils.GetInt(Consts.SaveKey_Currency_Gems);
                SetCurrencyAmount(CurrencyID.Gems, gemsTemp);
            }
            else
            {
                SetCurrencyAmount(CurrencyID.Gems, startCount_Gems);
                SaveData(false, CurrencyID.Gems);
            }
            //能量
            if (SaveUtils.HasKey(Consts.SaveKey_Currency_Energy))
            {
                int energyTemp = SaveUtils.GetInt(Consts.SaveKey_Currency_Energy);
                SetCurrencyAmount(CurrencyID.Energy, energyTemp);
            }
            else
            {
                SetCurrencyAmount(CurrencyID.Energy, startCount_Energy);
                SaveData(false, CurrencyID.Energy);
            }
            //针
            if (SaveUtils.HasKey(Consts.SaveKey_Currency_Needle))
            {
                int needleTemp = SaveUtils.GetInt(Consts.SaveKey_Currency_Needle);
                SetCurrencyAmount(CurrencyID.Needle, needleTemp);
            }
            else
            {
                SetCurrencyAmount(CurrencyID.Needle, startCount_Needle);
                SaveData(false, CurrencyID.Needle);
            }

            if (SaveUtils.HasKey(Consts.SaveKey_Currency_EnergyDate))
            {
                long ticks = SaveUtils.GetLong(Consts.SaveKey_Currency_EnergyDate);
                energyStartCountDate = new DateTimeOffset(ticks, TimeSpan.Zero);

                RefreshEnergyValue();
            }
            else
            {
                energyStartCountDate = DateTimeOffset.MaxValue;
            }

            if (UISystem.Instance != null && UISystem.Instance.topResourceManager != null)
            {
                UISystem.Instance.topResourceManager.SetCoinValue(Coins);
                UISystem.Instance.topResourceManager.SetGemValue(Gems);
                UISystem.Instance.topResourceManager.SetEnergyValue(Energy);
                UISystem.Instance.topResourceManager.SetNeedleValue(Needle);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("currency load Data error" + e);
        }

    }

    private static void SaveData(bool saveAll, CurrencyID currencyID = CurrencyID.NONE)
    {
        try
        {
            if (saveAll)
            {
                SaveUtils.SetInt(Consts.SaveKey_Currency_Coins, Coins);
                SaveUtils.SetInt(Consts.SaveKey_Currency_Gems, Gems);
                SaveUtils.SetInt(Consts.SaveKey_Currency_Energy, Energy);
                SaveUtils.SetInt(Consts.SaveKey_Currency_Needle, Needle);

                if (StartCountDown)
                    SaveUtils.SetLong(Consts.SaveKey_Currency_EnergyDate, energyStartCountDate.Ticks);
                else
                    SaveUtils.DeleteKey(Consts.SaveKey_Currency_EnergyDate);
            }
            else
            {
                switch (currencyID)
                {
                    case CurrencyID.Coins:
                        SaveUtils.SetInt(Consts.SaveKey_Currency_Coins, Coins);
                        break;
                    case CurrencyID.Gems:
                        SaveUtils.SetInt(Consts.SaveKey_Currency_Gems, Gems);
                        break;
                    case CurrencyID.Energy:
                        {
                            SaveUtils.SetInt(Consts.SaveKey_Currency_Energy, Energy);
                            if (StartCountDown)
                                SaveUtils.SetLong(Consts.SaveKey_Currency_EnergyDate, energyStartCountDate.Ticks);
                            else
                                SaveUtils.DeleteKey(Consts.SaveKey_Currency_EnergyDate);
                        }
                        break;
                    case CurrencyID.Needle:
                        SaveUtils.SetInt(Consts.SaveKey_Currency_Needle, Needle);
                        break;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("currency saveData error!" + e);
        }

    }
    #region 云存档
    public static Dictionary<string, object> GetData_ToFirestore()
    {
        Dictionary<string, object> currencies = new Dictionary<string, object>();
        currencies.Add("Coins", Currencies.Coins);
        currencies.Add("Gems", Currencies.Gems);
        currencies.Add("Energy", Currencies.Energy);
        if (Currencies.StartCountDown)
        {
            currencies.Add("EnergyStartDate", Currencies.energyStartCountDate.ToString());
        }

        return currencies;
    }

    public static void ResetData_ByFirestore(Dictionary<string, object> dict)
    {
        if (dict == null)
        {
            return;
        }

        if (dict.TryGetValue("Coins", out object coinObj))
        {
            if (int.TryParse(coinObj.ToString(), out int coins))
            {
                SaveUtils.SetInt(Consts.SaveKey_Currency_Coins, coins);
            }
        }
        if (dict.TryGetValue("Gems", out object gemsObj))
        {
            if (int.TryParse(gemsObj.ToString(), out int gems))
            {
                SaveUtils.SetInt(Consts.SaveKey_Currency_Gems, gems);
            }
        }
        if (dict.TryGetValue("Energy", out object energyObj))
        {
            if (int.TryParse(energyObj.ToString(), out int energy))
            {
                SaveUtils.SetInt(Consts.SaveKey_Currency_Energy, energy);
            }
        }

        if (dict.TryGetValue("EnergyStartDate", out object EnergyStartDateObj))
        {
            if (DateTimeOffset.TryParse(EnergyStartDateObj.ToString(), out DateTimeOffset date))
            {
                SaveUtils.SetLong(Consts.SaveKey_Currency_EnergyDate, date.Ticks);
            }
            else
            {
                SaveUtils.DeleteKey(Consts.SaveKey_Currency_EnergyDate);
            }
        }
        else
        {
            SaveUtils.DeleteKey(Consts.SaveKey_Currency_EnergyDate);
        }

    }

    #endregion



    public static bool CanAfford(CurrencyID currencyType, int cost)
    {
        if (CurrencyAmounts.TryGetValue(currencyType, out int value))
        {
            return value >= cost;
        }
        else
        {
            Debug.LogError("Cannot found CurrencyID in CurrencyAmounts!" + currencyType);
            return false;
        }
    }

    //检查是否可以支付 如果不可以进行提示
    public static bool CanAffordOrTip(CurrencyID currencyType, int cost)
    {
        if (CurrencyAmounts.TryGetValue(currencyType, out int value))
        {
            if (value >= cost)
                return true;
        }
        else
        {
            Debug.LogError("Cannot found CurrencyID in CurrencyAmounts!" + currencyType);
        }
        NotEnoughCoinsOrGems(currencyType);
        return false;
    }


    public static void AddTest()
    {
        CurrencyAmounts[CurrencyID.Gems] = 0;
        CurrencyAmounts[CurrencyID.Energy] = 0;
        CurrencyAmounts[CurrencyID.Coins] = 0;
        SaveData(false, CurrencyID.Gems);
        SaveData(false, CurrencyID.Energy);
        SaveData(false, CurrencyID.Coins);
    }

    /// <summary>
    /// 花费
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="cost"></param>
    /// <param name="reason">消耗途径</param>
    public static bool Spend(CurrencyID currency, int cost, string reason)
    {
        if (cost <= 0)
        {
            if (cost < 0) 
            {
                DebugSetting.LogError("Charge error ,delta <0 ,delta:" + cost + " CurrencyID:" + currency.ToString());
            }
            return false;
        }

        if (CurrencyAmounts.TryGetValue(currency, out int num))
        {
            if (num <= 0 || num < cost)
            {
                NotEnoughCoinsOrGems(currency);
                return false;
            }

            num -= cost;
            CurrencyAmounts[currency] = num;
            SaveData(false, currency);

            switch (currency)
            {
                case CurrencyID.Coins:
                    if (GameManager.Instance != null)
                        GameManager.Instance.RefreshCurrency_Coins();
                    AnalyticsUtil.spendVirtualCurrency(currency.ToString(), reason, cost, num);
                    AchievementManager.Instance.UpdateAchievement(AchievementType.costCoins, cost);
                    break;
                case CurrencyID.Gems:
                    if (GameManager.Instance != null)
                        GameManager.Instance.RefreshCurrency_Gems();
                    AnalyticsUtil.spendVirtualCurrency(currency.ToString(), reason, cost, num);
                    AchievementManager.Instance.UpdateAchievement(AchievementType.costDiamonds, cost);
                    break;
                case CurrencyID.Energy:
                    if (GameManager.Instance != null)
                        GameManager.Instance.RefreshCurrency_Energy();
                    if (energyStartCountDate == DateTimeOffset.MaxValue && !IsEnergyFull())
                        StartEnergyCountDown();
                    if (!trickEnergyDic.ContainsKey(reason))
                    {
                        trickEnergyDic[reason] = 0;
                    }
                    if (trickEnergyDic.TryGetValue(reason, out int count))
                    {
                        trickEnergyDic[reason] += cost;
                    }
                    AchievementManager.Instance.UpdateAchievement(AchievementType.costEnergy, cost);
                    break;
                case CurrencyID.Needle:
                    if (GameManager.Instance != null)
                        GameManager.Instance.RefreshCurrency_Needle();
                    AnalyticsUtil.spendVirtualCurrency(currency.ToString(), reason, cost, num);
                    break;
                default:
                    break;
            }
            return true;
        }
        else
        {
            DebugSetting.LogError("spend error ,not Found CurrencyID:" + currency.ToString());
            return false;
        }

    }
    static Dictionary<string, int> trickEnergyDic = new Dictionary<string, int>();
    public static void TrickEnergy()
    {
        if (trickEnergyDic == null || trickEnergyDic.Count <= 0)
            return;
        foreach (var item in trickEnergyDic)
        {
            AnalyticsUtil.spendVirtualCurrency(CurrencyID.Energy.ToString(), item.Key, item.Value, Currencies.Energy);
        }
        trickEnergyDic.Clear();
    }
    /// <summary>
    /// 货币不足时候定位到相应页面的商城
    /// </summary>
    /// <param name="currency"></param>
    public static event Action notEnoughGemsEvent;
    public static event Action notEnoughCoinsEvent;
    public static void NotEnoughCoinsOrGems(CurrencyID currency)
    {
        if (currency != CurrencyID.Coins && currency != CurrencyID.Gems && currency != CurrencyID.Needle)
            return;

        if (HVPageView.current.CurrentPage == 0)
        {
            //如果在商城页面，直接定位
            if (currency == CurrencyID.Gems)
            {
                UISystem.Instance.uiMainMenu.LocateShopCoinsOrGems(TopResourceType.Gem);
                AnalyticsUtil.TrackEvent("gems_lack");
            }
            else if (currency == CurrencyID.Coins)
            {
                UISystem.Instance.uiMainMenu.LocateShopCoinsOrGems(TopResourceType.Coin);
                AnalyticsUtil.TrackEvent("coins_lack");
            }
        }
        else
        {
            //如果不在商城页面 ，打开提示弹窗
            if (currency == CurrencyID.Gems)
            {
                UISystem.Instance.ShowUI(new InternalShopData(TopResourceType.Gem));
                AnalyticsUtil.TrackEvent("gems_lack");
                ShopSystem.Instance.refreshShopAction?.Invoke();
            }
            else if (currency == CurrencyID.Coins)
            {
                UISystem.Instance.ShowUI(new InternalShopData(TopResourceType.Coin));
                AnalyticsUtil.TrackEvent("coins_lack");
                ShopSystem.Instance.refreshShopAction?.Invoke();
            }
            //else if (currency == CurrencyID.Needle)
            //{
            //    UISystem.Instance.ShowUI(new InternalShopData(TopResourceType.Needle));
            //    ShopSystem.Instance.refreshShopAction?.Invoke();
            //}
        }
    }
    /// <summary>
    /// 增加
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="addNum"></param>
    /// <param name="reason"></param>
    /// <returns>是否成功</returns>
    public static bool Add(CurrencyID currency, int addNum, string reason, bool refreshUI = true)
    {
        if (addNum <= 0)
        {
            return false;
        }

        if (CurrencyAmounts.TryGetValue(currency, out int num))
        {
            if (num < 0)
            {
                num = 0;
            }
            if (num == int.MaxValue)
            {
                //溢出
                return false;
            }

            try
            {
                checked
                {
                    num += addNum;
                    AnalyticsUtil.earnVirtualCurrency(currency.ToString(), reason, addNum, num);
                }
            }
            catch (OverflowException)
            {
                num = int.MaxValue;
            }
            CurrencyAmounts[currency] = num;
            SaveData(false, currency);

            switch (currency)
            {
                case CurrencyID.Coins:
                    AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Get_Coins);
                    if (refreshUI && GameManager.Instance != null)
                        GameManager.Instance.RefreshCurrency_Coins();
                    AchievementManager.Instance.UpdateAchievement(AchievementType.getCoins, addNum);
                    break;
                case CurrencyID.Gems:
                    AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Get_Gems);
                    if (refreshUI && GameManager.Instance != null)
                        GameManager.Instance.RefreshCurrency_Gems();
                    AchievementManager.Instance.UpdateAchievement(AchievementType.getDiamonds, addNum);
                    break;
                case CurrencyID.Energy:
                    {
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Get_Energy);
                        if (refreshUI && GameManager.Instance != null)
                            GameManager.Instance.RefreshCurrency_Energy();
                        CheckEnergyFull();
                    }
                    break;
                case CurrencyID.Needle:
                    if (refreshUI && GameManager.Instance != null)
                        GameManager.Instance.RefreshCurrency_Needle();
                    break;
                default:
                    break;
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// 设置数量
    /// </summary>
    /// <param name="currencyType"></param>
    /// <param name="newAmount"></param>
    /// <param name="reason"></param>
    public static void SetCurrencyAmount(CurrencyID currencyType, int newAmount, string reason = null)
    {
        if (CurrencyAmounts.TryGetValue(currencyType, out int value))
        {
            if (newAmount < 0)
            {
                newAmount = 0;
                Debug.LogError(string.Concat("Can't have a negative amount of currency: ", currencyType, " value requested: ", newAmount));
            }

            var oldAmounts = value;
            CurrencyAmounts[currencyType] = newAmount;


            if (currencyType == CurrencyID.Gems && reason != null)
            {
                int subAmounts = newAmount - oldAmounts;
                //GGAnalyticsHelper.Update_Currency_Premium(subAmounts, reason);
            }
        }

    }
    /// <summary>
    /// 返回体力充满所需时间
    /// </summary>
    /// <returns></returns>
    public static int ReturnRecoveryEnergyTime()
    {
        if (IsEnergyFull() || !StartCountDown)
        {
            return 0;
        }
        else
        {
            int time = (MaxAutoAddCount_Energy - Energy - 1) * chargeEnergySeconds + (int)NextAddEnergyDate().TotalSeconds;
            return time;
        }
    }

    #region 体力恢复相关  
    private static int MaxAutoAddCount_Energy //体力上限
    {
        get
        {
            if (BattlePassSystem.Instance.IsPay)
            {
                return MaxAutoAddCount_Energy_Commmon + AddCount_Energy_BuyBattlePass;
            }
            else
            {
                return MaxAutoAddCount_Energy_Commmon;
            }
        }
    }
    private const int MaxAutoAddCount_Energy_Commmon = 100;//普通体力上限
    private const int AddCount_Energy_BuyBattlePass = 100;//购买BP后体力上限增加
    private const int chargeEnergySeconds = 120;//每恢复一个体力所需时间
    public static bool StartCountDown => energyStartCountDate != DateTimeOffset.MaxValue;

    private static void StartEnergyCountDown()
    {
        TimeManager.Instance.TryExcuteWithServerUtc(() =>
        {
            energyStartCountDate = TimeManager.ServerUtcNow();
        },()=> 
        {
            GameDebug.LogError("未获取到服务器时间");
        });
        SaveData(false, CurrencyID.Energy);
    }

    private static void EndEnergyCountDown()
    {
        energyStartCountDate = DateTimeOffset.MaxValue;
        SaveData(false, CurrencyID.Energy);
    }

    public static void RefreshEnergyValue()
    {
        if (!hasInit)
        {
            return;
        }

        if (StartCountDown)
        {
            if (IsEnergyFull())
            {
                EndEnergyCountDown();
                return;
            }
        }
        else
        {
            if (!IsEnergyFull())
            {
                StartEnergyCountDown();
            }
            return;
        }

        TimeManager.Instance.TryExcuteWithServerUtc(() =>
        {
            var refreshEnergyTimeSpan = TimeManager.ServerUtcNow() - energyStartCountDate;
            if (refreshEnergyTimeSpan.TotalSeconds >= 0)
            {
                if (refreshEnergyTimeSpan.TotalSeconds >= chargeEnergySeconds)
                {
                    var totalAddEnergy = (int)refreshEnergyTimeSpan.TotalSeconds / chargeEnergySeconds;
                    energyStartCountDate = energyStartCountDate.AddSeconds(chargeEnergySeconds * totalAddEnergy);
                    AddEnergyAuto(totalAddEnergy);
                }
            }
            else
            {
                StartEnergyCountDown();
                GameDebug.LogError("体力时间异常！");
            }
        },()=>
        {
            GameDebug.LogError("未获取到服务器时间");
        });
    }

    private static bool AddEnergyAuto(int addNum)
    {
        if (addNum <= 0)
        {
            return false;
        }

        if (CurrencyAmounts.TryGetValue(CurrencyID.Energy, out int num))
        {
            if (num < 0)
            {
                num = 0;
            }
            if (num == int.MaxValue)
            {
                //溢出
                return false;
            }

            try
            {
                checked
                {
                    if (num + addNum >= MaxAutoAddCount_Energy)
                        num = MaxAutoAddCount_Energy;
                    else
                        num += addNum;
                }
            }
            catch (OverflowException)
            {
                num = int.MaxValue;
            }

            if (num < 0) 
            {
                GameDebug.LogError("体力值为负，注意检查数据");
                num = 1;
            }

            CurrencyAmounts[CurrencyID.Energy] = num;
            SaveData(false, CurrencyID.Energy);

            if (GameManager.Instance != null)
                GameManager.Instance.RefreshCurrency_Energy(true);

            CheckEnergyFull();
            return true;
        }
        return false;
    }

    public static TimeSpan NextAddEnergyDate()
    {
        if (energyStartCountDate != DateTimeOffset.MaxValue)
        {
            return energyStartCountDate.AddSeconds(chargeEnergySeconds + 1) - TimeManager.Instance.UtcNow();
        }
        else
        {
            GameDebug.LogError("energyStartCountDate error! error data:" + energyStartCountDate);
            return TimeSpan.Zero;
        }
    }

    private static bool IsEnergyFull()
    {
        return Energy >= MaxAutoAddCount_Energy;
    }

    private static void CheckEnergyFull()
    {
        if (StartCountDown && IsEnergyFull())
        {
            EndEnergyCountDown();
        }
    }
    #endregion
}
