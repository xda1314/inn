using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// 每日任务
/// </summary>
public class DailyTaskSystem
{
    public static DailyTaskSystem Instance { get; private set; } = new DailyTaskSystem();
    public bool hasInitDailyTaskSuccess { get; private set; } = false;
    public DateTimeOffset curSaveDateTime { get; private set; } = DateTimeOffset.MinValue;
    public Dictionary<string, SaveDailytaskItemData> dailyTaskLocationData { get; private set; } = new Dictionary<string, SaveDailytaskItemData>();//所有每日任务位置和对应数据（每日只随机一次任务）
    public Dictionary<DailyTaskType, int> dailyTaskProgressDic { get; private set; } = new Dictionary<DailyTaskType, int>();//任务完成进度(收集任务除外)

    public void InitSystem(Action showUICB)
    {
        TimeManager.Instance.TryExcuteWithServerUtc(() =>
        {
            hasInitDailyTaskSuccess = true;
            InitDailyTaskEvent();
            LoadData();
            showUICB?.Invoke();
        });
    }
    private void ResertDailyTaskData()
    {
        dailyTaskLocationData.Clear();
        foreach (var item in DailyTaskDefinition.locationDataDic)
        {
            SaveDailytaskItemData saveDailytaskItemData = new SaveDailytaskItemData();
            if (item.Value.dailyTaskLocationType == DailyTaskLocationType.fix)
            {
                saveDailytaskItemData.dailyTaskId = item.Value.fixTaskId;
            }
            else
            {
                if (TaskGoalsManager.Instance.curLevelIndex < item.Value.taskLevel)
                {
                    saveDailytaskItemData.dailyTaskId = item.Value.commonTaskId;
                }
                else
                {
                    int seed = UnityEngine.Random.Range(0, item.Value.randomTaskIdList.Count);
                    saveDailytaskItemData.dailyTaskId = item.Value.randomTaskIdList[seed];
                }
            }
            saveDailytaskItemData.hasClaim = false;
            dailyTaskLocationData.Add(item.Key, saveDailytaskItemData);
        }

        curSaveDateTime = TimeManager.ServerUtcNow();

        dailyTaskProgressDic.Clear();
        dailyTaskProgressDic.Add(DailyTaskType.AddEnergy, 0);
        dailyTaskProgressDic.Add(DailyTaskType.Collect, 0);
        dailyTaskProgressDic.Add(DailyTaskType.ShopBuy, 0);
        dailyTaskProgressDic.Add(DailyTaskType.SpendEnergy, 0);
        dailyTaskProgressDic.Add(DailyTaskType.TaskComplete, 0);
        SaveData();
    }
    public void TryCompleteDailyTask(string taskId)
    {
        if (dailyTaskLocationData.TryGetValue(taskId, out SaveDailytaskItemData dailytaskItemData))
        {
            if (dailytaskItemData.hasClaim)
                GameDebug.LogError("该每日任务奖励已领取，检查数据，错误任务id：" + taskId);
            else
            {
                dailyTaskLocationData[taskId].hasClaim = true;
                TryAnalytics();
                SaveData();
            }
        }
    }
    /// <summary>
    /// 是否有奖励可领取
    /// </summary>
    /// <returns></returns>
    public bool HasRewerdCanClaim()
    {
        foreach (var item in dailyTaskLocationData.Values)
        {
            if (!item.hasClaim) 
            {
                if (DailyTaskDefinition.dailyTaskDefinitionDic.TryGetValue(item.dailyTaskId, out DailyTaskDefinition definition)) 
                {
                    if (definition.taskType == DailyTaskType.Collect)
                    {
                        int hasItemNum = MergeLevelManager.Instance.ReturnItemNumByPrefabName(definition.targetPrefab, MergeLevelType.mainLine);
                        if (hasItemNum >= definition.maxRateProgress)
                        {
                            return true;
                        }
                    }
                    else if (dailyTaskProgressDic.TryGetValue(definition.taskType, out int progress))
                    {
                        if (progress >= definition.maxRateProgress) 
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    #region 事件系统
    public Action DailyTaskEvent_AddEnergy;
    public Action DailyTaskEvent_ShopBuy;
    public Action DailyTaskEvent_SpendEnergy;
    public Action DailyTaskEvent_CompleteTask;
    bool hasInitDailyTaskEvent = false;
    private void InitDailyTaskEvent()
    {
        if (hasInitDailyTaskEvent)
            return;
        hasInitDailyTaskEvent = true;

        DailyTaskEvent_AddEnergy += () =>
        {
            AddCompleteProgress(DailyTaskType.AddEnergy, 1);
        };
        DailyTaskEvent_ShopBuy += () =>
        {
            AddCompleteProgress(DailyTaskType.ShopBuy, 1);
        };
        DailyTaskEvent_SpendEnergy += () =>
        {
            AddCompleteProgress(DailyTaskType.SpendEnergy, 1);
        };
        DailyTaskEvent_CompleteTask += () =>
        {
            AddCompleteProgress(DailyTaskType.TaskComplete, 1);
        };

    }
    private void AddCompleteProgress(DailyTaskType dailyTaskType, int addProgress)
    {
        if (!hasInitDailyTaskSuccess)
            return;
        if (addProgress <= 0)
            return;

        if (dailyTaskProgressDic.ContainsKey(dailyTaskType))
        {
            dailyTaskProgressDic[dailyTaskType] += addProgress;
            GameDebug.Log($"增加DailyTask完成！类型：{dailyTaskType}, 增加完成数:{addProgress}");
            SaveData();
        }
    }
    #endregion
    #region 数据存储加载
    private void LoadData()
    {
        try
        {
            string saveStr = SaveUtils.GetString(Consts.SaveKey_DailyTask);
            if (string.IsNullOrEmpty(saveStr))
            {
                //没有存档，初始化每日任务
                ResertDailyTaskData();
                return;
            }

            var saveData = JsonConvert.DeserializeObject<SaveDailyTaskData>(saveStr);
            if (saveData != null)
            {
                curSaveDateTime = saveData.curSaveDateTime;
                dailyTaskLocationData = saveData.dailyTaskLocationData;
                dailyTaskProgressDic = saveData.dailyTaskProgressDic;
                if (curSaveDateTime != DateTimeOffset.MinValue && !ExtensionTool.IsDateToday(curSaveDateTime, TimeManager.ServerUtcNow()))
                {
                    //不是同一天，重置每日任务
                    ResertDailyTaskData();
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
            SaveDailyTaskData saveDta = new SaveDailyTaskData();
            saveDta.curSaveDateTime = curSaveDateTime;
            saveDta.dailyTaskLocationData = dailyTaskLocationData;
            saveDta.dailyTaskProgressDic = dailyTaskProgressDic;
            string endStr = JsonConvert.SerializeObject(saveDta);
            SaveUtils.SetString(Consts.SaveKey_DailyTask, endStr);
        }
        catch (Exception e)
        {
            Debug.LogError("[Save Data error!]" + e);
        }
    }
    #endregion
    #region 云存档
    public Dictionary<string,object> GetSaveDataToFirestore()
    {
        try
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("time", curSaveDateTime.ToString());
            Dictionary<string, Dictionary<string, object>> allClaimStateSic = new Dictionary<string, Dictionary<string, object>>();
            foreach (var item in dailyTaskLocationData)
            {
                Dictionary<string, object> claimStateSic = new Dictionary<string, object>();
                claimStateSic.Add("hasClaim", item.Value.hasClaim);
                claimStateSic.Add("id", item.Value.dailyTaskId);
                allClaimStateSic.Add(item.Key, claimStateSic);
            }
            dic.Add("state", JsonConvert.SerializeObject(allClaimStateSic));
            dic.Add("progress", JsonConvert.SerializeObject(dailyTaskProgressDic));
            return dic;
        }
        catch (Exception e) 
        {
            GameDebug.LogError(e);
            return new Dictionary<string, object>();
        }
    }
    public void SetSaveDataFromFirestore(Dictionary<string,object> dic)
    {
        try
        {
            if (dic == null) 
            {
                GameDebug.LogError("data error!");
                return;
            }
            if (dic.TryGetValue("time", out object timeStr)) 
            {
                if (DateTimeOffset.TryParse(timeStr.ToString(), out DateTimeOffset time)) 
                {
                    curSaveDateTime = time;
                }
            }
            if (dic.TryGetValue("state", out object stateStr)) 
            {
                Dictionary<string, Dictionary<string, object>> stateDic = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(stateStr.ToString());
                foreach (var item in stateDic)
                {
                    SaveDailytaskItemData data = new SaveDailytaskItemData();
                    if (item.Value.TryGetValue("hasClaim", out object hasClaimStr)) 
                    {
                        if (bool.TryParse(hasClaimStr.ToString(), out bool hasClaim)) 
                        {
                            data.hasClaim = hasClaim;
                        }              
                    }
                    if (item.Value.TryGetValue("id", out object idStr)) 
                    {
                        data.dailyTaskId = idStr.ToString();
                    }
                    dailyTaskLocationData[item.Key] = data;
                }
            }
            if (dic.TryGetValue("progress", out object progressStr)) 
            {
                Dictionary<object, object> progressDic = JsonConvert.DeserializeObject<Dictionary<object, object>>(progressStr.ToString());
                foreach (var item in progressDic)
                {
                    if (DailyTaskType.TryParse(item.Key.ToString(), out DailyTaskType type) && int.TryParse(item.Value.ToString(), out int progress)) 
                    {
                        dailyTaskProgressDic[type] = progress;
                    }
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
    private void TryAnalytics()
    {
        int hasClaimNum = 0;
        foreach (var item in dailyTaskLocationData.Values)
        {
            if (item.hasClaim)
                hasClaimNum++;
        }
        AnalyticsUtil.trackActivityStep("dailyTask", hasClaimNum);
        if (hasClaimNum == 1)
        {
            AnalyticsUtil.trackActivityStart("dailyTask", "");
        }
        else if (hasClaimNum == dailyTaskLocationData.Count)
        {
            AnalyticsUtil.trackActivityEnd("dailyTask");
        }
    }
}
public class SaveDailyTaskData
{
    public DateTimeOffset curSaveDateTime;
    public Dictionary<string, SaveDailytaskItemData> dailyTaskLocationData = new Dictionary<string, SaveDailytaskItemData>();
    public Dictionary<DailyTaskType, int> dailyTaskProgressDic = new Dictionary<DailyTaskType, int>();
}
public class SaveDailytaskItemData
{
    public bool hasClaim;
    public string dailyTaskId;
}


