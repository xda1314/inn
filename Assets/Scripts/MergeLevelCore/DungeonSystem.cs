using IvyCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum DungeonState
{
    none = 0,
    locked = 1,
    unlock = 2,
    finished = 3
}

public class DungeonSystem
{
    private Dictionary<MergeLevelType, DungeonState> DungeonStateDic = new Dictionary<MergeLevelType, DungeonState>();
    public static DungeonSystem Instance { get; private set; } = new DungeonSystem();
    private List<MergeLevelType> completeDungeonList = new List<MergeLevelType>();//完成的副本
    private Dictionary<MergeLevelType, int> dungeonScoreDic = new Dictionary<MergeLevelType, int>();//爱心副本积分

    public Action CheckDungeonEventByComplete;
    public Action CheckDungeonEventByOpen;
    public void InitDungeonSystem()
    {
        //读取存档
        string dataStr = SaveUtils.GetString(Consts.SaveKey_CompletedDungeon);
        if (!string.IsNullOrEmpty(dataStr))
        {
            List<string> dataList = JsonConvert.DeserializeObject<List<string>>(dataStr);
            List <MergeLevelType> l =new List<MergeLevelType>();
            if (dataList != null && dataList.Count > 0)
            {
                for (int i = 0; i < dataList.Count; i++)
                {
                    if (Enum.TryParse(dataList[i], out MergeLevelType type))
                    {
                        l.Add(type);
                    }
                }
                completeDungeonList = l;
            }
        }
        if (SaveUtils.HasKey(Consts.SaveKey_DungeonScore))
        {
            string json = SaveUtils.GetString(Consts.SaveKey_DungeonScore);
            var dic = JsonConvert.DeserializeObject<Dictionary<MergeLevelType, int>>(json);
            if (dic != null)
            {
                dungeonScoreDic = dic;
            }
        }
    }

    public List<MergeLevelType> GetFinishedDungeon()
    {
        return completeDungeonList;
    }

    public void CheckDungeon()
    {
        CheckDungeonEventByOpen?.Invoke();
    }

    public Dictionary<MergeLevelType, DungeonState> GetCurrentDungeonState()
    {
        CheckDungeonsState();
        return DungeonStateDic;
    }

    public MergeLevelType completeType { get; private set; } = MergeLevelType.none;
    public void DungeonComplete(MergeLevelType mergeLevelType)
    {
        if (!completeDungeonList.Contains(mergeLevelType))
            completeDungeonList.Add(mergeLevelType);
        completeType = mergeLevelType;
        CheckDungeonsState();
        CheckDungeonEventByComplete?.Invoke();
        //存储
        List<string> saveList = new List<string>();
        for (int i = 0; i < completeDungeonList.Count; i++)
        {
            saveList.Add(completeDungeonList[i].ToString());
        }
        SaveUtils.SetString(Consts.SaveKey_CompletedDungeon, JsonConvert.SerializeObject(saveList));
    }

    public void AddDungeonScore(MergeLevelType mergeLevelType, int score)
    {
        if (!dungeonScoreDic.ContainsKey(mergeLevelType))
            dungeonScoreDic[mergeLevelType] = score;
        else
            dungeonScoreDic[mergeLevelType] += score;
        string json = JsonConvert.SerializeObject(dungeonScoreDic);
        SaveUtils.SetString(Consts.SaveKey_DungeonScore, json);
    }

    public int GetDungeonScore(MergeLevelType mergeLevelType)
    {
        if (dungeonScoreDic.ContainsKey(mergeLevelType))
            return dungeonScoreDic[mergeLevelType];
        return 0;
    }

    #region 云存档
    public Dictionary<string, object> GetSaveDataToFirestore()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        List<string> saveList = new List<string>();
        for (int i = 0; i < completeDungeonList.Count; i++)
        {
            GameDebug.Log("lyj测试副本存储云存档单个"+ completeDungeonList[i].ToString());
            saveList.Add(completeDungeonList[i].ToString());
        }
        GameDebug.Log("lyj测试副本存储云存档");
        dic.Add("completeDungeonList", saveList);
        return dic;
    }
    public void SetSaveDataFromFirestore(Dictionary<string, object> dic)
    {
        if (dic == null)
        {
            return;
        }
        List<MergeLevelType> l = new List<MergeLevelType>();
        if (dic.TryGetValue("completeDungeonList",out object completeDungeonListObject)) 
        {
            GameDebug.Log("lyj测试副本获取云存档成功"+ completeDungeonListObject.ToString());
            List<string> dataList = JsonConvert.DeserializeObject<List<string>>(completeDungeonListObject.ToString());
            if (dataList != null && dataList.Count > 0)
            {
                GameDebug.Log("lyj测试副本完成数量" + dataList.Count);
                for (int i = 0; i < dataList.Count; i++)
                {
                    GameDebug.Log("lyj测试副y本获取单个云存档成功" + dataList[i]);
                    if (Enum.TryParse(dataList[i], out MergeLevelType type))
                    {
                        GameDebug.Log("lyj测试副本转换单个云存档成功" + type.ToString());
                        l.Add(type);
                    }
                }
            }
            completeDungeonList = l;
        }
        //存储
        List<string> saveList = new List<string>();
        for (int i = 0; i < completeDungeonList.Count; i++)
        {
            saveList.Add(completeDungeonList[i].ToString());
        }
        SaveUtils.SetString(Consts.SaveKey_CompletedDungeon, JsonConvert.SerializeObject(saveList));
    }
    #endregion

    private void CheckDungeonsState()
    {
        int curLevelIndex = TaskGoalsManager.Instance.curLevelIndex;
        //DungeonStateDic.Clear();
        int index = 0;
        foreach (var dun in DungeonDefinition.DungeonDefDic)
        {
            //判定活动开启
            int unLock = dun.Value.unlockChapter;
            if (completeDungeonList.Contains(dun.Key))
                DungeonStateDic[dun.Key] = DungeonState.finished;
            else if (curLevelIndex >= unLock)
                DungeonStateDic[dun.Key] = DungeonState.unlock;
            else if (index < 1) 
            {
                index++;
                DungeonStateDic[dun.Key] = DungeonState.locked;
            }  
        }
    }
}
