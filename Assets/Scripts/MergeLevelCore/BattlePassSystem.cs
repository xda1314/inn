using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using System.Linq;
using ivy.game;

public enum BattlePassRewardState
{
    NoReward,//没有奖励
    Lock,//未解锁,或未付费
    UnlockButNotClaimed,//已解锁，但未领取
    Claimed//已领取，
}

public class BattlePassSystem
{
    public static BattlePassSystem Instance { get; private set; } = new BattlePassSystem();

    private bool hasInitBattlePassSuccess;
    private bool hasLoadPayData;

    /// <summary>
    /// 当前月份索引，（0-11）
    /// </summary>
    public int MonthIndex { get; private set; }
    private DateTimeOffset lastRefreshDate;

    private bool _isPay;

    public bool HasInitBattlePassSuccess()
    {
        return hasInitBattlePassSuccess;
    }
    /// <summary>
    /// 是否付费battlepass
    /// </summary>
    public bool IsPay
    {
        get
        {
            if (hasInitBattlePassSuccess)
            {
                return _isPay;
            }

            if (!hasLoadPayData)
            {
                hasLoadPayData = true;
                if (SaveUtils.HasKey(Consts.SaveKey_BattlePass_IsPay))
                {
                    _isPay = SaveUtils.GetBool(Consts.SaveKey_BattlePass_IsPay);
                }
            }
            return _isPay;
        }
    }
    public static Dictionary<int, bool> freeClaimedData = new Dictionary<int, bool>();
    public static Dictionary<int, bool> payClaimedData = new Dictionary<int, bool>();

    public int TotalExp { get; private set; }// 当前总积分

    public int curExpLevel
    {
        get
        {
            var list = BattlePassDefinition.DefinitionsDict.Values.ToList();
            if (list != null && list.Count > 0)
            {
                list.Sort((p1, p2) =>
                {
                    return p1.stage.CompareTo(p2.stage);
                });


                if (TotalExp < list[0].allExp)
                {
                    return 0;
                }
                else if (TotalExp >= list[list.Count - 1].allExp)
                {
                    return list[list.Count - 1].stage;
                }

                foreach (var item in list)
                {
                    if (TotalExp < item.allExp)
                    {
                        return item.stage - 1;
                    }
                }
            }
            return 0;
        }
    }

    public bool needShowBuySpeedUpBag
    {
        get 
        {
            return TotalExp < BattlePassDefinition.maxExperience;
        }
    }
    /// <summary>
    /// 初始化系统
    /// </summary>
    /// <param name="showUIBtnCB">在UIPanel_Main的OnAfterInitOrReloadGame里面调用，初始化成功后，展示主界面battlepass按钮</param>
    public void InitSystem(Action showUIBtnCB)
    {
        //TimeManager.Instance.TryExcuteWithServerUtc(() =>
        //{
        //    hasLoadPayData = true;
        //    hasInitBattlePassSuccess = true;
        //    LoadData();
        //    TryRefreshMonthlyDate();
        //    showUIBtnCB?.Invoke();
        //});
    }


    private void LoadData()
    {
        try
        {
            if (SaveUtils.HasKey(Consts.SaveKey_BattlePass_MonthIndex))
            {
                MonthIndex = SaveUtils.GetInt(Consts.SaveKey_BattlePass_MonthIndex);
            }
            else
            {
                MonthIndex = -1;
            }

            if (SaveUtils.HasKey(Consts.SaveKey_BattlePass_LastRefreshDate))
            {
                lastRefreshDate = new DateTimeOffset(SaveUtils.GetLong(Consts.SaveKey_BattlePass_LastRefreshDate), TimeSpan.Zero);
            }
            if (SaveUtils.HasKey(Consts.SaveKey_BattlePass_IsPay))
            {
                _isPay = SaveUtils.GetBool(Consts.SaveKey_BattlePass_IsPay);
            }
            if (SaveUtils.HasKey(Consts.SaveKey_BattlePass_FreeClaimedData))
            {
                freeClaimedData = JsonConvert.DeserializeObject<Dictionary<int, bool>>(SaveUtils.GetString(Consts.SaveKey_BattlePass_FreeClaimedData));
            }
            if (SaveUtils.HasKey(Consts.SaveKey_BattlePass_PayClaimedData))
            {
                payClaimedData = JsonConvert.DeserializeObject<Dictionary<int, bool>>(SaveUtils.GetString(Consts.SaveKey_BattlePass_PayClaimedData));
            }
            if (SaveUtils.HasKey(Consts.SaveKey_BattlePass_TotalExp))
            {
                TotalExp = SaveUtils.GetInt(Consts.SaveKey_BattlePass_TotalExp);
            }
            else
            {
                TotalExp = 0;
            }
        }
        catch (Exception e)
        {
            GameDebug.LogError(e);
        }

    }

    public void SaveData()
    {
        if (!hasInitBattlePassSuccess)
        {
            return;
        }

        try
        {
            SaveUtils.SetInt(Consts.SaveKey_BattlePass_MonthIndex, MonthIndex);
            SaveUtils.SetLong(Consts.SaveKey_BattlePass_LastRefreshDate, lastRefreshDate.Ticks);
            SaveUtils.SetBool(Consts.SaveKey_BattlePass_IsPay, IsPay);
            SaveData_Claimed();
            SaveData_TotalExp();
        }
        catch (Exception e)
        {
            GameDebug.LogError(e);
        }

    }
    private void SaveData_Claimed()
    {
        SaveUtils.SetString(Consts.SaveKey_BattlePass_FreeClaimedData, JsonConvert.SerializeObject(freeClaimedData));
        SaveUtils.SetString(Consts.SaveKey_BattlePass_PayClaimedData, JsonConvert.SerializeObject(payClaimedData));
    }
    private void SaveData_TotalExp()
    {
        SaveUtils.SetInt(Consts.SaveKey_BattlePass_TotalExp, TotalExp);
    }

    #region 云存档
    public Dictionary<string, object> GetSaveDataToFirestore()
    {
        try
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("month", MonthIndex);
            dict.Add("lastRefresh", lastRefreshDate.ToString());
            dict.Add("isPay", IsPay);
            dict.Add("freeClaimed", JsonConvert.SerializeObject(freeClaimedData));
            dict.Add("payClaimed", JsonConvert.SerializeObject(payClaimedData));
            dict.Add("totalExp", TotalExp);
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
                GameDebug.LogError("battlePass为空");
                return;
            }
            if (dict.TryGetValue("month", out object monthIndexObj))
            {
                if (int.TryParse(monthIndexObj.ToString(), out int temp))
                {
                    MonthIndex = temp;
                }
            }
            if (dict.TryGetValue("lastRefresh", out object lastRefreshObj))
            {
                if (DateTimeOffset.TryParse(lastRefreshObj.ToString(), out DateTimeOffset temp))
                {
                    lastRefreshDate = temp;
                }
            }
            if (dict.TryGetValue("isPay", out object isPayObj))
            {
                if (bool.TryParse(isPayObj.ToString(), out var temp))
                {
                    _isPay = temp;
                }
            }
            if (dict.TryGetValue("freeClaimed", out object freeClaimedObj))
            {
                Dictionary<int, bool> dic = JsonConvert.DeserializeObject<Dictionary<int, bool>>(freeClaimedObj.ToString());
                if (dic != null && dic.Count > 0) 
                {
                    freeClaimedData = dic;
                }
            }
            if (dict.TryGetValue("payClaimed", out object payClaimedObj))
            {
                Dictionary<int, bool> dic = JsonConvert.DeserializeObject<Dictionary<int, bool>>(payClaimedObj.ToString());
                if (dic != null && dic.Count > 0)
                {
                    payClaimedData = dic;
                }
            }
            if (dict.TryGetValue("totalExp", out object totalExpObj))
            {
                if (int.TryParse(totalExpObj.ToString(), out int temp))
                {
                    TotalExp = temp;
                }
            }
            SaveData();
        }
        catch (Exception e)
        {
            GameDebug.LogError(e);
        }
    }
    #endregion

    public bool TryRefreshMonthlyDate()
    {
        //if (!TimeManager.IsGetServerUtcSuccess)
        //{
        //    GameDebug.LogError("TimeManager.IsGetServerUtcSuccess false");
        //    return false;
        //}

        DateTimeOffset serverNow = TimeManager.ServerUtcNow();
        if (lastRefreshDate.Year == serverNow.Year && lastRefreshDate.Month == serverNow.Month)
        {
            return false;
        }

        TotalExp = 0;
        MonthIndex = serverNow.Month - 1;
        lastRefreshDate = serverNow;
        _isPay = false;
        freeClaimedData.Clear();
        payClaimedData.Clear();     
        SaveData();
        return true;
    }

    /// <summary>
    /// 获取battlepass结束时间
    /// </summary>
    /// <returns></returns>
    public DateTimeOffset TryGetCurrentMonthFinishDate()
    {
        int year = lastRefreshDate.Year;
        int month = lastRefreshDate.Month;
        if (lastRefreshDate.Month == 12)
        {
            year++;
            month = 1;
        }
        else
        {
            month++;
        }
        return new DateTimeOffset(year, month, 1, 0, 0, 0, TimeSpan.Zero);
    }

    /// <summary>
    /// 判断某一位置奖励领取状态
    /// </summary>
    /// <param name="isFree">是否免费奖励</param>
    /// <param name="pointIndex">奖励位置，（从0开始）</param>
    /// <returns>在该位置的奖励领取状态</returns>
    public BattlePassRewardState GetRewardStateInPoint(bool isFree, int pointIndex, out MergeRewardItem mergeRewardItem)
    {
        pointIndex++;
        if (BattlePassDefinition.DefinitionsDict.TryGetValue(pointIndex, out BattlePassDefinition def) && def != null)
        {
            if (isFree)
            {
                mergeRewardItem = def.freeReward;
                if (!def.freeReward.IsValidReward())
                {
                    return BattlePassRewardState.NoReward;
                }
                else if (freeClaimedData.TryGetValue(pointIndex - 1, out bool hasClaimed) && hasClaimed == true)
                {
                    return BattlePassRewardState.Claimed;
                }
                else
                {
                    //等级足够
                    return TotalExp >= def.allExp
                        ? BattlePassRewardState.UnlockButNotClaimed
                        : BattlePassRewardState.Lock;
                }
            }
            else
            {
                mergeRewardItem = def.vipReward;
                if (!def.vipReward.IsValidReward())
                {
                    return BattlePassRewardState.NoReward;
                }
                else if (payClaimedData.TryGetValue(pointIndex - 1, out bool hasClaimed) && hasClaimed == true)
                {
                    return BattlePassRewardState.Claimed;
                }
                else
                {
                    if (!IsPay)
                    {
                        return BattlePassRewardState.Lock;
                    }
                    //等级足够
                    return TotalExp >= def.allExp
                        ? BattlePassRewardState.UnlockButNotClaimed
                        : BattlePassRewardState.Lock;
                }
            }
        }
        else
        {
            mergeRewardItem = new MergeRewardItem();
            return BattlePassRewardState.NoReward;
        }
    }


    /// <summary>
    /// 领取奖励
    /// </summary>
    /// <param name="isFree">是否免费奖励</param>
    /// <param name="pointIndex">奖励位置，（从0开始）</param>
    /// <param name="screenPos"></param>
    /// <param name="mergeReward">随机的奖励</param>
    /// <returns>是否领取成功</returns>
    public bool TryClaimReward(bool isFree, int pointIndex, Vector3 screenPos, out MergeRewardItem mergeReward)
    {
        if (GetRewardStateInPoint(isFree, pointIndex, out MergeRewardItem rewardItem) != BattlePassRewardState.UnlockButNotClaimed)
        {
            mergeReward = new MergeRewardItem();
            return false;
        }

        if (isFree)
        {
            if (freeClaimedData.TryGetValue(pointIndex, out bool hasClaimed) && hasClaimed == true)
            {
                mergeReward = new MergeRewardItem();
                return false;
            }
            else
            {
                freeClaimedData[pointIndex] = true;
                //发放奖励
                mergeReward = rewardItem;
                List<MergeRewardItem> rewardList = new List<MergeRewardItem>();
                rewardList.Add(mergeReward);
                GameManager.Instance.GiveRewardItem(rewardList, "battlepassFreeReward", Vector3.zero);
                TryAnalytics();
                SaveData_Claimed();
                return true;
            }
        }
        else
        {
            if (payClaimedData.TryGetValue(pointIndex, out bool hasClaimed) && hasClaimed == true)
            {
                mergeReward = new MergeRewardItem();
                return false;
            }
            else
            {
                payClaimedData[pointIndex] = true;
                //发放奖励
                mergeReward = rewardItem;
                List<MergeRewardItem> rewardList = new List<MergeRewardItem>();
                rewardList.Add(mergeReward);
                GameManager.Instance.GiveRewardItem(rewardList, "battlepassGoldReward", Vector3.zero);
                SaveData_Claimed();
                return true;
            }
        }
    }

    // billing脚本中调用
    public void SuccessBuyBattlePassCB()
    {
        _isPay = true;
        UIPanel_BattlePass.RefreshBattlepassScore?.Invoke();
        SaveData();
    }

    public void AddExpByCompleteDailyTask(int addExp) 
    {
        if (!hasInitBattlePassSuccess)
        {
            return;
        }
        TotalExp += addExp;
        TotalExp = TotalExp <= BattlePassDefinition.maxExperience ? TotalExp : BattlePassDefinition.maxExperience;
        GameDebug.Log($"增加BattlePass积分：{addExp}; 现在的总积分是：{TotalExp}");
        UIPanel_BattlePass.RefreshBattlepassScore?.Invoke();
        SaveData_TotalExp();
    }

    public void BuySpeedUpPackage() 
    {
        TotalExp = BattlePassDefinition.maxExperience;
        GameDebug.Log("购买battlepass加速礼包");
        SaveData_TotalExp();
    }

    public bool HasRewerdCanClaim() 
    {
        MergeRewardItem rewardItem;
        foreach (var item in BattlePassDefinition.DefinitionsDict)
        {
            if (item.Key <= curExpLevel) //已解锁的battlepass中有没有未领取的
            {
                BattlePassRewardState freeState = GetRewardStateInPoint(true, item.Key - 1, out rewardItem);
                if (freeState == BattlePassRewardState.UnlockButNotClaimed) 
                {
                    return true;
                }
                if (IsPay) 
                {
                    BattlePassRewardState vipState = GetRewardStateInPoint(false, item.Key - 1, out rewardItem);
                    if (vipState == BattlePassRewardState.UnlockButNotClaimed) 
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void TryAnalytics() 
    {
        AnalyticsUtil.trackActivityStep("battlepass", freeClaimedData.Count);
        if (freeClaimedData.Count == 1)
        {
            AnalyticsUtil.trackActivityStart("battlepass", "");
        }
        else if (freeClaimedData.Count == BattlePassDefinition.DefinitionsDict.Count) 
        {
            AnalyticsUtil.trackActivityEnd("battlepass");
        }       
    }
}
