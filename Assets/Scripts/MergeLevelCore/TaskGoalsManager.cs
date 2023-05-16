using Ivy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主线订单管理
/// </summary>
public class TaskGoalsManager
{
    public static TaskGoalsManager Instance { get; private set; } = new TaskGoalsManager();
    public int curLevelIndex { get; private set; }  //当前任务等级
    public List<string> leftTaskIdList { get; private set; } = new List<string>();//当前关卡剩余订单
    public string finalTaskId { get; private set; }
    public int YellowStarNum { get; private set; } = 0;
    public bool IsCompleteAllChapter { get; private set; } = false;

    public static event Action<int> changeLevelAction;
    /// <summary>
    /// 切换下一关，刷新任务
    /// </summary>
    public void ChangeToNextLevel()
    {
        //完成配表所有任务处理
        if (curLevelIndex >= GameManager.Instance.PlayerMaxLevel)
        {
            IsCompleteAllChapter = true;
            SaveData();
            return;
        }

        AnalyticsUtil.TrackEvent("level_" + curLevelIndex + "_end");
        GameManager.Instance.playerData.CurrentPlayLevel = 0;

        AchievementManager.Instance.UpdateAchievement(AchievementType.chapter, curLevelIndex);
        curLevelIndex++;
        AnalyticsUtil.trackMainLine("taskLv", curLevelIndex);
        AnalyticsUtil.TrackEvent("level_start", curLevelIndex.ToString());
        RiseSdk.Instance.SetUserProperty("user_level", curLevelIndex.ToString());
        changeLevelAction?.Invoke(curLevelIndex);
        AddTaskToList();
        UIPanel_TopBanner.refreshBanner?.Invoke();
    }
    public void RemoveTaskfromList(string taskId)
    {
        leftTaskIdList.Remove(taskId);
        TryCommitOrderNum();
        YellowStarNum++;
        AnalyticsUtil.trackMainLine("star", YellowStarNum);
        SaveData();
        IvyCore.UI_Manager.Instance.InvokeRefreshEvent("", "page_Play_RefreshStarChest");
    }

    public int GetLeftTaskCount() 
    {
        return leftTaskIdList.Count;
    }
    private void TryCommitOrderNum()
    {
        AnalyticsUtil.recordCoreAction("order", 1);
        AnalyticsUtil.commitCoreAction("order");
    }
    /// <summary>
    /// 等级增加时添加该等级所有订单
    /// </summary>
    public void AddTaskToList()
    {
        leftTaskIdList.Clear();
        if (TaskGoalsDefinition.taskLevelDataDic.TryGetValue(curLevelIndex, out TaskLevelData taskLevelData))
        {
            for (int i = 0; i < taskLevelData.taskIdList.Count; i++)
            {
                leftTaskIdList.Add(taskLevelData.taskIdList[i]);
            }
            finalTaskId = taskLevelData.finalTaskId;
        }
        else
        {
            GameDebug.LogError("task data error ,check config 'Goals_Main',Error level:" + curLevelIndex);
        }
        SaveData();
    }

    public void DeductStar(int num)
    {
        if (YellowStarNum >= num)
        {
            YellowStarNum -= num;
            SaveData();
            IvyCore.UI_Manager.Instance.InvokeRefreshEvent("", "page_Play_RefreshStarChest");
        }
        else
        {
            Debug.LogError("star is not enough");
        }
    }

    public int ReturnStarCount()
    {
        return YellowStarNum;
    }

    public List<MergeItemData> taskItemData { get; private set; } = new List<MergeItemData>();
    /// <summary>
    /// 存储地图中不在箱子，泡泡中且未锁住的item数据
    /// </summary>
    public void CheckTaskInfo()
    {
        taskItemData.Clear();
        foreach (var item in MergeLevelManager.Instance.CurrentMapData.itemDataDict)
        {
            if (!item.Value.IsInBubble && !item.Value.IsInBox && !item.Value.IsLocked)
            {
                taskItemData.Add(item.Value);
            }
        }
        foreach (var item in MergeLevelManager.Instance.CurrentMapData.storePackList)
        {
            if (!item.IsInBubble && !item.IsInBox && !item.IsLocked)
            {
                taskItemData.Add(item);
            }
        }
    }
    public List<string> ReturnSortTask()
    {
        List<string> taskIdList = leftTaskIdList;
        if (taskIdList.Count == 0)
            return taskIdList;

        List<string> completeList = new List<string>();
        List<string> completePartList = new List<string>();
        List<string> notCompleteList = new List<string>();
        for (int i = 0; i < taskIdList.Count; i++)
        {
            if (TaskGoalsDefinition.TaskDefinitionsDict.TryGetValue(taskIdList[i], out TaskData taskData))
            {
                bool isAllComplete = true;
                bool isAllNotComplete = true;
                for (int j = 0; j < taskData.taskDefinition.needItemList.Count; j++)
                {
                    int hasItemNumInMap = MergeLevelManager.Instance.ReturnItemNumByPrefabName(taskData.taskDefinition.needItemList[j].name, MergeLevelManager.Instance.CurrentLevelType);
                    bool isComplete = hasItemNumInMap >= taskData.taskDefinition.needItemList[j].num;
                    if (isComplete)
                        isAllNotComplete = false;
                    if (!isComplete)
                        isAllComplete = false;
                }
                if (isAllComplete)
                    completeList.Add(taskIdList[i]);
                else if (isAllNotComplete)
                    notCompleteList.Add(taskIdList[i]);
                else
                    completePartList.Add(taskIdList[i]);
            }
        }
        taskIdList.Clear();
        taskIdList.AddRange(completeList);
        taskIdList.AddRange(completePartList);
        taskIdList.AddRange(notCompleteList);
        return taskIdList;
    }
    public bool HasTaskCanClaim()
    {
        List<string> taskIdList = leftTaskIdList;
        for (int i = 0; i < taskIdList.Count; i++)
        {
            if (TaskGoalsDefinition.TaskDefinitionsDict.TryGetValue(taskIdList[i], out TaskData taskData))
            {
                for (int j = 0; j < taskData.taskDefinition.needItemList.Count; j++)
                {
                    int hasItemNumInMap = MergeLevelManager.Instance.ReturnItemNumByPrefabName(taskData.taskDefinition.needItemList[j].name, MergeLevelManager.Instance.CurrentLevelType);
                    bool isComplete = hasItemNumInMap >= taskData.taskDefinition.needItemList[j].num;
                    if (isComplete)
                        return true;
                }
            }
        }
        return false;
    }
    public void SkipLevelByEditor(int level)
    {
#if UNITY_EDITOR
        curLevelIndex = level;
        AddTaskToList();
#endif
    }

    #region Save And Load
    /// <summary>
    /// 首次加载游戏（无存档）
    /// </summary>
    private void InitData()
    {
        curLevelIndex = 1;
        AddTaskToList();
    }

    //清除存档
    public void ResetData()
    {
        SaveUtils.DeleteKey(Consts.SaveKey_GoalsTask);
        InitData();
    }

    public void SaveData()
    {
        try
        {
            SaveTaskData data = new SaveTaskData();
            data.curLevelIndex = curLevelIndex;
            data.finalTaskId = finalTaskId;
            data.YellowStarNum = YellowStarNum;
            data.leftTaskIdList = leftTaskIdList;
            data.IsCompleteAllChapter = IsCompleteAllChapter;
            string endStr = JsonConvert.SerializeObject(data);
            SaveUtils.SetString(Consts.SaveKey_GoalsTask, endStr);
        }
        catch (Exception e)
        {
            Debug.LogError("[TaskGoalsManager]SaveData error!" + e);
        }
    }
    public void LoadData()
    {
        try
        {
            if (SaveUtils.HasKey(Consts.SaveKey_GoalsTask))//有存档读存档
            {
                string saveStr = SaveUtils.GetString(Consts.SaveKey_GoalsTask);
                if (string.IsNullOrEmpty(saveStr))
                {
                    return;
                }
                var data = JsonConvert.DeserializeObject<SaveTaskData>(saveStr);
                if (data == null)
                {
                    return;
                }
                curLevelIndex = data.curLevelIndex;
                finalTaskId = data.finalTaskId;
                YellowStarNum = data.YellowStarNum;
                leftTaskIdList = data.leftTaskIdList;
                IsCompleteAllChapter = data.IsCompleteAllChapter;
            }
            else
            {
                InitData();
            }
            if (IsCompleteAllChapter)
            {
                //有新关卡开启(配表新增关卡，针对已完成上个版本所有任务的玩家)
                if (curLevelIndex < GameManager.Instance.PlayerMaxLevel)
                {
                    IsCompleteAllChapter = false;
                    curLevelIndex++;
                    AddTaskToList();
                }
            }
        }

        catch (Exception e)
        {
            Debug.LogError("[ TaskGoalsManager loadData error]" + e);
        }
    }
    #endregion
    #region 云存档
    public Dictionary<string, object> GetSaveDataToFirestore()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("level", curLevelIndex);
        dic.Add("finalTask", finalTaskId);
        dic.Add("starNum", YellowStarNum);
        dic.Add("idList", JsonConvert.SerializeObject(leftTaskIdList));
        dic.Add("completeAll", IsCompleteAllChapter);
        return dic;
    }
    public void SetSaveDataFromFirestore(Dictionary<string, object> dic)
    {
        if (dic == null)
        {
            GameDebug.LogError("mainLineTask为空");
            return;
        }
        if (dic.TryGetValue("level", out object levelStr))
        {
            if (int.TryParse(levelStr.ToString(), out int level))
            {
                curLevelIndex = level;
            }
        }
        if (dic.TryGetValue("finalTask", out object finalTaskStr))
        {
            finalTaskId = finalTaskStr.ToString();
        }
        if (dic.TryGetValue("starNum", out object starNumStr))
        {
            if (int.TryParse(starNumStr.ToString(), out int starNum))
            {
                YellowStarNum = starNum;
            }
        }
        if (dic.TryGetValue("idList", out object idListStr))
        {
            leftTaskIdList = JsonConvert.DeserializeObject<List<string>>(idListStr.ToString());
        }
        if (dic.TryGetValue("completeAll", out object completeAllStr))
        {
            if (bool.TryParse(completeAllStr.ToString(), out bool isCompleteAll))
            {
                IsCompleteAllChapter = isCompleteAll;
            }
        }
        SaveData();
    }
    #endregion
}

public class SaveTaskData
{
    public int curLevelIndex;
    public string finalTaskId;
    public int YellowStarNum;
    public bool IsCompleteAllChapter;
    public List<string> leftTaskIdList = new List<string>();
}
