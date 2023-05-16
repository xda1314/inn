using BDUnity.Utils;
using EnhancedUI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 节日活动管理
/// </summary>
public class FestivalSystem
{
    public static FestivalSystem Instance { get; private set; } = new FestivalSystem();
    public Dictionary<MergeLevelType, FestivalData> totalFestivalDataDic = new Dictionary<MergeLevelType, FestivalData>();
    //存储数据
    Dictionary<MergeLevelType, FestivalSaveData> totalSaveDataDic = new Dictionary<MergeLevelType, FestivalSaveData>();
    //-------
    public Action refreshFestival;
    public void InitSystem()
    {
        InitCommonData();
        LoadData();
    }
    private void LoadData()
    {
        try
        {
            Dictionary<string, Dictionary<string, object>> totalDic = new Dictionary<string, Dictionary<string, object>>();
            if (SaveUtils.HasKey(Consts.SaveKey_FestivalData))
            {
                totalDic = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(SaveUtils.GetString(Consts.SaveKey_FestivalData));
                if (totalDic == null || totalDic.Count == 0)
                {
                    GameDebug.LogError("data error!");
                    return;
                }
                foreach (var item in totalDic)
                {
                    if (Enum.TryParse(item.Key, out MergeLevelType levelType))
                    {
                        FestivalSaveData saveData = new FestivalSaveData();
                        if (item.Value.TryGetValue("score", out object obj_Score) && int.TryParse(obj_Score.ToString(), out int score))
                        {
                            saveData.scorePoint = score;
                        }
                        if (item.Value.TryGetValue("taskIds", out object obj_TaskIds))
                        {
                            saveData.curTaskIdList = JsonConvert.DeserializeObject<List<string>>(obj_TaskIds.ToString());
                        }
                        if (item.Value.TryGetValue("claimIndexs", out object obj_ClaimIds))
                        {
                            saveData.claimList = JsonConvert.DeserializeObject<List<int>>(obj_ClaimIds.ToString());
                        }
                        totalSaveDataDic[levelType] = saveData;
                    }
                }
            }

            foreach (var item in BranchDefinition.allBranchdefDic)
            {
                if (!MergeLevelManager.Instance.IsBranch(item.Key) && !totalDic.ContainsKey(item.Key.ToString()))
                {
                    FestivalSaveData data = new FestivalSaveData();
                    if (item.Value != null && item.Value.Count > 0)
                    {
                        data.curTaskIdList.Add(item.Value.First().Key);
                    }
                    else
                    {
                        GameDebug.LogError("has no data!");
                    }
                    totalSaveDataDic.Add(item.Key, data);
                }
            }
        }
        catch (Exception e)
        {
            GameDebug.LogError(e);
        }
    }
    private void SaveData()
    {
        try
        {
            Dictionary<string, Dictionary<string, object>> totalDic = new Dictionary<string, Dictionary<string, object>>();
            foreach (var item in totalSaveDataDic)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("score", item.Value.scorePoint);
                dic.Add("taskIds", item.Value.curTaskIdList);
                dic.Add("claimIndexs", item.Value.claimList);
                totalDic.Add(item.Key.ToString(), dic);
            }
            SaveUtils.SetString(Consts.SaveKey_FestivalData, JsonConvert.SerializeObject(totalDic));
        }
        catch (Exception e)
        {
            GameDebug.LogError(e);
        }
    }
    #region 云存档 
    public Dictionary<string, Dictionary<string, object>> GetSaveDataToFirestore()
    {
        try
        {
            Dictionary<string, Dictionary<string, object>> totalDic = new Dictionary<string, Dictionary<string, object>>();
            foreach (var item in totalSaveDataDic)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("score", item.Value.scorePoint);
                dic.Add("taskIds", item.Value.curTaskIdList);
                dic.Add("claimIndexs", item.Value.claimList);
                totalDic.Add(item.Key.ToString(), dic);
            }
            return totalDic;
        }
        catch (Exception e)
        {
            GameDebug.LogError(e);
            return new Dictionary<string, Dictionary<string, object>>();
        }
    }
    public void SetSaveDataFromFirestore(Dictionary<string, Dictionary<string, object>> totalDic)
    {
        if (totalDic == null || totalDic.Count == 0)
        {
            GameDebug.LogError("festivalDic data error!");
            return;
        }
        foreach (var item in totalDic)
        {
            if (Enum.TryParse(item.Key, out MergeLevelType levelType))
            {
                FestivalSaveData saveData = new FestivalSaveData();
                if (item.Value.TryGetValue("score", out object obj_Score) && int.TryParse(obj_Score.ToString(), out int score))
                {
                    saveData.scorePoint = score;
                }
                if (item.Value.TryGetValue("taskIds", out object obj_TaskIds))
                {
                    saveData.curTaskIdList = JsonConvert.DeserializeObject<List<string>>(obj_TaskIds.ToString());
                }
                if (item.Value.TryGetValue("claimIndexs", out object obj_ClaimIds))
                {
                    saveData.claimList = JsonConvert.DeserializeObject<List<int>>(obj_ClaimIds.ToString());
                }
                totalSaveDataDic[levelType] = saveData;
            }
        }
        SaveData();
    }
    #endregion

    private void InitCommonData()
    {
        try
        {
            //万圣节
            FestivalData data1 = new FestivalData();
            if (DateTimeOffset.TryParse("2022/10/30 00:00:00 +00:00", out DateTimeOffset startTime))
            {
                data1.startTime = startTime;
                data1.endTime = startTime.AddDays(30);
            }
            totalFestivalDataDic[MergeLevelType.halloween] = data1;
            //圣诞节
            FestivalData data2 = new FestivalData();
            if (DateTimeOffset.TryParse("2022/2/14 00:00:00 +00:00", out DateTimeOffset startTime2))
            {
                data2.startTime = startTime2;
                data2.endTime = startTime2.AddDays(30);
            }
            totalFestivalDataDic[MergeLevelType.christmas] = data2;
            //情人节
            FestivalData data3 = new FestivalData();
            if (DateTimeOffset.TryParse("2023/2/14 00:00:00 +00:00", out DateTimeOffset startTime3))
            {
                    data3.startTime = startTime3;
                    data3.endTime = startTime3.AddDays(30);
            }
            totalFestivalDataDic[MergeLevelType.lover] = data3;
        }
        catch (Exception e)
        {
        }
    }
    //活动是否开启
    public bool IsFestivalOpen(MergeLevelType type)
    {
        if (totalFestivalDataDic.TryGetValue(type, out FestivalData data))
        {
            return TimeManager.Instance.UtcNow() > data.startTime && TimeManager.Instance.UtcNow() <= data.endTime;
        }
        return false;
    }
    //是否达到开启条件
    public bool IsActive(MergeLevelType type)
    {
        return TaskGoalsManager.Instance.curLevelIndex >= ReturnOpenLv(type);
    }
    public int ReturnOpenLv(MergeLevelType type)
    {
        int openLv = 0;
        switch (type)
        {
            case MergeLevelType.halloween:
                openLv = 5;
                break;
            case MergeLevelType.christmas:
                openLv = 5;
                break;
            case MergeLevelType.lover:
                openLv = 5;
                break;
            default:
                break;
        }
        return openLv;
    }
    public DateTimeOffset GetFestivalEndTime(MergeLevelType type)
    {
        if (totalFestivalDataDic.TryGetValue(type, out FestivalData data))
        {
            return data.endTime;
        }
        else return DateTimeOffset.MinValue;
    }
    public List<string> ReturnCurTaskListByType(MergeLevelType levelType)
    {
        if (totalSaveDataDic.TryGetValue(levelType, out FestivalSaveData data))
        {
            return data.curTaskIdList;
        }
        return new List<string>();
    }
    public void CompleteTaskById(MergeLevelType levelType, string taskId)
    {
        if (totalSaveDataDic.TryGetValue(levelType, out FestivalSaveData data) && BranchDefinition.allBranchdefDic.TryGetValue(levelType, out Dictionary<string, BranchDefinition> totalDef))
        {
            var completeDef = totalDef[taskId];
            foreach (var reward in completeDef.rewardItemList)
            {
                if (reward.IsRewardBranchPoint)
                    AddScorePoint(levelType, reward.num);
            }
            data.curTaskIdList.Remove(taskId);
            for (int i = 0; i < completeDef.nextIdList.Count; i++)
            {
                if (!data.curTaskIdList.Contains(completeDef.nextIdList[i]))
                {
                    data.curTaskIdList.Add(completeDef.nextIdList[i]);
                }
            }
            SaveData();
        }
        else
        {
            GameDebug.LogError("taskId orror:" + taskId);
        }
    }
    //增加支线活动积分
    public void AddScorePoint(MergeLevelType levelType, int point)
    {
        if (totalSaveDataDic.TryGetValue(levelType, out FestivalSaveData data))
        {
            if (data.scorePoint == 0)
                AnalyticsUtil.trackActivityStart(levelType.ToString(),"");
            data.scorePoint += point;
            SaveData();
        }
    }
    public int ReturnCurScore(MergeLevelType levelType)
    {
        if (totalSaveDataDic.TryGetValue(levelType, out FestivalSaveData data))
        {
            return data.scorePoint;
        }
        return 0;
    }
    public int GetCanClaimRewardCount(MergeLevelType levelType)
    {
        int count = 0;
        if (totalSaveDataDic.TryGetValue(levelType, out FestivalSaveData data))
        {
            var rewardList = GetRewardsList(levelType);
            if (rewardList != null)
            {
                for (int i = 0; i < rewardList.Count; i++)
                {
                    if (data.scorePoint >= rewardList[i].goalPoint && !data.claimList.Contains(rewardList[i].goalPoint))
                        count++;
                }
            }
        }
        return count;
    }
    //获取当前支线活动奖励列表
    public SmallList<BranchRewardDefinition> GetRewardsList(MergeLevelType levelType)
    {
        if (BranchRewardDefinition.BranchRewardDict.ContainsKey(levelType))
            return BranchRewardDefinition.BranchRewardDict[levelType];
        return null;
    }

    //获取当前支线正在进行的任务或当前可领取的任务
    public BranchRewardDefinition GetCurrentBranchDef(MergeLevelType levelType)
    {
        BranchRewardDefinition branchReward = null;
        if (BranchRewardDefinition.BranchRewardDict.ContainsKey(levelType))
        {
            SmallList<BranchRewardDefinition> branchList = BranchRewardDefinition.BranchRewardDict[levelType];
            for (int i = 0; i < branchList.Count; i++)
            {
                if (!IsRewardClaimed(levelType, branchList[i].goalPoint))
                {
                    branchReward = branchList[i];
                    break;
                }
            }
        }
        return branchReward;
    }


    //领取此分数对应的奖励
    public void ClaimReward(MergeLevelType levelType, int point)
    {
        if (totalSaveDataDic.TryGetValue(levelType, out FestivalSaveData data))
        {
            if (!data.claimList.Contains(point))
            {
                AnalyticsUtil.trackActivityStep(levelType.ToString(), point/20);
                data.claimList.Add(point);
                if (point == 300) 
                    AnalyticsUtil.trackActivityEnd(levelType.ToString());
                SaveData();
            }
        }
    }
    //分数是否达到领取奖励的标准
    public bool CanClaim(MergeLevelType levelType, int point)
    {
        return ReturnCurScore(levelType) >= point;
    }
    //此分数对应的奖励是否已领取
    public bool IsRewardClaimed(MergeLevelType levelType, int point)
    {
        if (totalSaveDataDic.TryGetValue(levelType, out FestivalSaveData data))
        {
            return data.claimList.Contains(point);
        }
        return false;
    }
    //根据积分获取进度
    public Vector2Int GetPointProgress(MergeLevelType levelType, int point)
    {
        var lastPoint = 0;
        var rewards = GetRewardsList(levelType);
        if (rewards != null)
        {
            for (int i = 0; i < rewards.Count; i++)
            {
                if (point >= lastPoint && (point < rewards[i].goalPoint || i == rewards.Count - 1))
                {
                    return new Vector2Int(point - lastPoint, rewards[i].goalPoint - lastPoint);
                }
                lastPoint = rewards[i].goalPoint;
            }
        }
        return Vector2Int.one;
    }
    //是否已领取全部奖励
    public bool HasClaimAllReward(MergeLevelType levelType)
    {
        if (totalSaveDataDic.TryGetValue(levelType, out FestivalSaveData data) && data.claimList.Count == GetRewardsList(levelType).Count)
        {
            return true;
        }
        return false;
    }

}
public class FestivalData
{
    public DateTimeOffset startTime;
    public DateTimeOffset endTime;
}
public class FestivalSaveData
{
    public int scorePoint;
    public List<string> curTaskIdList = new List<string>();
    public List<int> claimList = new List<int>();
}
