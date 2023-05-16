using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 仓库管理
/// </summary>
public class StoreManager
{
    public static StoreManager Instance { get; private set; } = new StoreManager();

    public int maxFreeStoreGridCount { get; private set; } = 5;//初始送五个格子(金币解锁)
    public int buyGridByGemIndex { get; private set; } = 0;//钻石购买格子次数
    public int curGridNumWithoutBP => maxFreeStoreGridCount + buyGridByGemIndex * addGridNumByGemOnce;//除了bp送的两个格子外的所有格子数量
    public int buyBattlePassBouns { get; private set; } = 2;//购买battlepass临时奖励的格子 
    public readonly int addGridNumByGemOnce = 5;//钻石购买一次增加数量
    public int curMaxStoreGridCount
    {
        get
        {
            if (BattlePassSystem.Instance.IsPay)
            {
                return curGridNumWithoutBP + buyBattlePassBouns;
            }
            else
            {
                return curGridNumWithoutBP;
            }
        }
    }

    public void UnlockStoreGridByCoin()
    {
        maxFreeStoreGridCount++;
        SaveData();

        AchievementManager.Instance.UpdateAchievement(AchievementType.warehouse, curGridNumWithoutBP);
    }
    public void UnlockStoreGridByGem()
    {
        buyGridByGemIndex++;
        SaveData();

        AchievementManager.Instance.UpdateAchievement(AchievementType.warehouse, curGridNumWithoutBP);
    }


    public void LoadData()
    {
        try
        {
            string saveStr = SaveUtils.GetString(Consts.SaveKey_StoreUnlock);
            if (string.IsNullOrEmpty(saveStr))//未花钱解锁格子没有存档，默认五个格子
            {
                return;
            }
            int num = int.Parse(saveStr);
            if (num != 0)
            {
                maxFreeStoreGridCount = num;
            }
            int saveInt = SaveUtils.GetInt(Consts.SaveKey_StoreUnlockByGem);
            if (saveInt > 0)
            {
                buyGridByGemIndex = saveInt;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("[LoadSaveData error]" + e);
        }
    }
    public void SaveData()
    {
        if (maxFreeStoreGridCount < 5)
            return;

        try
        {
            int maxGridCount = maxFreeStoreGridCount;
            string endStr = JsonConvert.SerializeObject(maxGridCount);
            SaveUtils.SetString(Consts.SaveKey_StoreUnlock, endStr);
            if (buyGridByGemIndex > 0)
            {
                SaveUtils.SetInt(Consts.SaveKey_StoreUnlockByGem, buyGridByGemIndex);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("[Save Storage Data error!]" + e);
        }
    }

    #region 云存档
    public void SetSaveDataFromFirestore(Dictionary<string, object> dic)
    {
        try
        {
            if (dic == null || dic.Count == 0)
            {
                GameDebug.LogError("storeDic data error!" + dic);
                return;
            }
            if (dic.TryGetValue("count", out object count_obj) && int.TryParse(count_obj.ToString(), out int count))
            {
                maxFreeStoreGridCount = count;
                SaveUtils.SetString(Consts.SaveKey_StoreUnlock, maxFreeStoreGridCount.ToString());
            }
            if (dic.TryGetValue("index", out object index_obj) && int.TryParse(index_obj.ToString(), out int index))
            {
                if (index > 0)
                {
                    buyGridByGemIndex = index;
                    SaveUtils.SetInt(Consts.SaveKey_StoreUnlockByGem, buyGridByGemIndex);
                }
            }
        }
        catch (Exception e)
        {
            GameDebug.LogError(e);
        }
    }
    public Dictionary<string, object> GetSaveDataToFirestore()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("count", maxFreeStoreGridCount);
        dic.Add("index", buyGridByGemIndex);
        return dic;
    }
    #endregion
}

