using ivy.game;
using Ivy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager
{
    public static AchievementManager Instance { get; private set; } = new AchievementManager();
    private bool InitSuccess = false;

    public int getCoins { get; private set; }//成就--获取金币
    public int getDiamonds { get; private set; }//成就--获取砖石
    public int costCoins { get; private set; }//成就--消耗金币
    public int costDiamonds { get; private set; }//成就--消耗砖石
    public int costEnergy { get; private set; }//成就--消耗能量
    public int unlockBubble { get; private set; }//成就--解锁泡泡
    public int speedCharge { get; private set; }//成就--充能

    private string SaveKey_Achievement = "SaveKey_Achievement";
    private Dictionary<AchievementType, DateTimeOffset> AchievementTimer =new Dictionary<AchievementType, DateTimeOffset>();

    public void UpdateAchievement(AchievementType type, int num)
    {
        if (!InitSuccess)
            return;

        if (num < 0)
            return;

        try
        {
            int current = 0;
            switch (type)
            {
                case AchievementType.getCoins:
                    getCoins += num;
                    current = getCoins;
                    break;
                case AchievementType.getDiamonds:
                    getDiamonds += num;
                    current = getDiamonds;
                    break;
                case AchievementType.costCoins:
                    costCoins += num;
                    current = costCoins;
                    break;
                case AchievementType.costDiamonds:
                    costDiamonds += num;
                    current = costDiamonds;
                    break;
                case AchievementType.costEnergy:
                    costEnergy += num;
                    current = costEnergy;
                    break;
                case AchievementType.unlockBubble:
                    unlockBubble += num;
                    current = unlockBubble;
                    break;
                case AchievementType.speedCharge:
                    speedCharge += num;
                    current = speedCharge;
                    break;
                case AchievementType.levels:
                case AchievementType.chapter:
                case AchievementType.warehouse:
                    current = num;
                    break;
                default:
                    return;
            }

            SaveData();

     
            if (AchievementConfig.ConfigDict.TryGetValue(type, out var list))
            {
                foreach (var item in list)
                {
                    if (current <= 0 || item.Rate <= 0)
                    {
                        LogUtils.Log($"[UpdateAchievement] type:{type};current:{current};rate:{item.Rate}");
                        RiseSdk.Instance.UpdateArchievement("s" + item.ID, 0);
                    }
                    else if (current < item.Rate)
                    {
                        if (!AchievementTimer.TryGetValue(type, out DateTimeOffset myTimer) || (TimeManager.ServerUtcNow() - myTimer).TotalSeconds > 600)
                        {
                            LogUtils.Log($"[UpdateAchievement] type:{type};current:{current};rate:{item.Rate}");
                            RiseSdk.Instance.UpdateArchievement("s" + item.ID, Mathf.FloorToInt(current * 1f / item.Rate * 100));
                            AchievementTimer[type] = TimeManager.ServerUtcNow();
                        }
                    }
                    else
                    {
                        LogUtils.Log($"[UpdateAchievement] type:{type};current:{current};rate:{item.Rate}");
                        RiseSdk.Instance.UpdateArchievement("s" + item.ID, 100);
                    }
                }
            }
        }
        catch (Exception e)
        {
            DebugSetting.LogError("ArchievementManager TryUpdateAllArea error!!!" + e);
        }
    }

    #region Save And Load
    public void InitSystem()
    {
        try
        {
            if (SaveUtils.HasKey(SaveKey_Achievement))
            {
                string json = SaveUtils.GetString(SaveKey_Achievement);
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                TryInitData(dic);
            }
            InitSuccess = true;
        }
        catch (Exception e)
        {
            InitSuccess = true;
            LogUtils.LogError("ArchievementManager LoadData error!" + e);
        }
    }

    private void SaveData()
    {
        try
        {
            string json = JsonConvert.SerializeObject(TryGetSaveData());
            SaveUtils.SetString(SaveKey_Achievement, json);
        }
        catch (Exception e)
        {
            Debug.LogError("SaveAchievementData error!" + e);
        }
    }
    #endregion

    #region 云存档

    private Dictionary<string, object> TryGetSaveData()
    {
        if (!InitSuccess)
            InitSystem();

        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict.Add("getCoins", getCoins);
        dict.Add("getDiamonds", getDiamonds);
        dict.Add("costCoins", costCoins);
        dict.Add("costDiamonds", costDiamonds);
        dict.Add("costEnergy", costEnergy);
        dict.Add("unlockBubble", unlockBubble);
        dict.Add("speedCharge", speedCharge);
        return dict;
    }

    private void TryInitData(Dictionary<string, object> dic)
    {
        if (dic == null)
        {
            GameDebug.LogError("数据为空");
            return;
        }
        if (dic.TryGetValue("getCoins", out object getCoinsStr))
        {
            if (int.TryParse(getCoinsStr.ToString(), out int getCoinsInt))
            {
                getCoins = getCoinsInt;
            }
        }

        if (dic.TryGetValue("getDiamonds", out object getDiamondsStr))
        {
            if (int.TryParse(getDiamondsStr.ToString(), out int getDiamondsInt))
            {
                getDiamonds = getDiamondsInt;
            }
        }

        if (dic.TryGetValue("costCoins", out object costCoinsStr))
        {
            if (int.TryParse(costCoinsStr.ToString(), out int costCoinsStrInt))
            {
                costCoins = costCoinsStrInt;
            }
        }

        if (dic.TryGetValue("costDiamonds", out object costDiamondsStr))
        {
            if (int.TryParse(costDiamondsStr.ToString(), out int costDiamondsInt))
            {
                costDiamonds = costDiamondsInt;
            }
        }

        if (dic.TryGetValue("costEnergy", out object costEnergyStr))
        {
            if (int.TryParse(costEnergyStr.ToString(), out int costEnergyInt))
            {
                costEnergy = costEnergyInt;
            }
        }

        if (dic.TryGetValue("unlockBubble", out object unlockBubbleStr))
        {
            if (int.TryParse(unlockBubbleStr.ToString(), out int unlockBubbleStrInt))
            {
                unlockBubble = unlockBubbleStrInt;
            }
        }

        if (dic.TryGetValue("speedCharge", out object speedChargeStr))
        {
            if (int.TryParse(speedChargeStr.ToString(), out int speedChargeInt))
            {
                speedCharge = speedChargeInt;
            }
        }

    }

    public Dictionary<string, object> GetSaveDataToFirestore()
    {
        try
        {
            return TryGetSaveData();
        }
        catch (Exception e)
        {
            GameDebug.LogError(e);
            return new Dictionary<string, object>();
        }
    }

    public void SetSaveDataFromFirestore(Dictionary<string, object> dic)
    {
        if (dic == null) 
        {
            GameDebug.Log("achievementDic 数据为空");
        }
        TryInitData(dic);
        SaveData();
    }
    #endregion
}
