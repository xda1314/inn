using BDUnity.Utils;
using EnhancedUI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BranchSystem
{
    public static BranchSystem Instance { get; private set; } = new BranchSystem();

    #region 存档数据
    private int BranchVersion = 0;//支线关卡版本号
    private int BranchLoopCount = 0;//活动循环次数
    public DateTimeOffset curStartTime { get; private set; } = DateTimeOffset.MinValue;//本次赛季开始时间(优化后新存档)
    public BranchControlDefinition CurBranchDef { get; private set; }
    private List<string> _crTaskList = new List<string>();
    public List<string> CurTaskList
    {
        get
        {
            return _crTaskList;
        }
        set
        {
            _crTaskList = value;
        }
    }
    public int branchPoint { get; private set; }
    private List<int> claimedRewardIndexSet = new List<int>();
    private int currentVerson = 4;   //当前支线活动
    private int lastVerson = 4;   //当前支线活动
    #endregion

    public int Remote_UnlockLevel { get; private set; }
    public readonly int NewLoopTime = 3;
    public List<MergeRewardItem> LastRewardList { get;private set; } = new List<MergeRewardItem>();  //上一个活动奖励
    public DateTimeOffset Branch_EndTime { get; private set; }
    public Action RefreshBranchEvent;
    public Action refreshByReset;
    public void InitBranchSystem()
    {
        LoadLocalData();
        int remotelevel = RemoteConfigSystem.Instance.GetRemoteConfig_Int(RemoteConfigSystem.remoteKey_branchConfig);
        GameDebug.Log("Lyj测试获取远程配置remotelevel" + remotelevel);
        Remote_UnlockLevel = remotelevel > 0 ? remotelevel : 10;
        CheckBranchCnf();
        InitLastRewardList();
    }

    //保底奖励
    public List<MergeRewardItem> GuaranteedRewards { get; set; } = new List<MergeRewardItem>()
    {
        new MergeRewardItem
        {
            name = "Energy_3",
            num = 1,
        },
            new MergeRewardItem
        {
            name = "Coins_2",
            num = 1,
        }
    };

    //检测当前支线活动是否开启
    public bool GetIsOpen()
    {
        if (CurBranchDef == null)
            GameDebug.Log("Lyj测试当前数据为空");
        GameDebug.Log("Lyj测试当前等级是否符合"+(Remote_UnlockLevel <= TaskGoalsManager.Instance.curLevelIndex));
        if (CurBranchDef != null 
            && Remote_UnlockLevel <= TaskGoalsManager.Instance.curLevelIndex)
            return true;
        return false;
    }

    public bool IsActive() 
    {
        return TimeManager.Instance.UtcNow() <= curStartTime.AddDays(NewLoopTime);
    }

    /// <summary>
    /// 领取当前所有任务完奖励
    /// </summary>
    /// <returns></returns>
    public bool ClaimedAllTask()
    {
        return GetCurrentBranchDef() == null;
    }

    /// <summary>
    /// 重置赛季
    /// </summary>
    private void TryResetNewSeason()
    {
        #region 清除数据
        branchPoint = 0;
        SaveUtils.SetInt(Consts.SaveKey_Branch_Point, branchPoint);
        claimedRewardIndexSet.Clear();
        SaveUtils.SetString(Consts.SaveKey_Branch_ClaimedRewards, string.Empty);
        CurTaskList.Clear();
        SaveUtils.DeleteKey("Save_Key_BranchGuaranteeRewardsPop");
        #endregion

        #region 开启新支线
        currentVerson ++;
        int index = 0;
        while (!BranchControlDefinition.OpenBranchList.Contains(currentVerson)) 
        {
            currentVerson ++;
            index++;
            if (currentVerson > BranchControlDefinition.TotalBranchCnfDict.Count)
            {
                currentVerson = 1;
            }
            if(index > 50)
            {
                break;
            }    
        }

        //获取当前支线活动数据
        if (BranchControlDefinition.TotalBranchCnfDict.TryGetValue(currentVerson, out var cnf))
        {
            CurBranchDef = cnf;
        }
        //获取当前支线任务数据
        if (BranchDefinition.allBranchdefDic.ContainsKey(CurBranchDef.BranchType))
        {
            var taskDic = BranchDefinition.allBranchdefDic[CurBranchDef.BranchType];
            if (taskDic != null && taskDic.Count > 0)
            {
                CurTaskList.Add(taskDic.First().Key);
            }
        }
#if UNITY_EDITOR
        if (DebugSetting.CanUseDebugConfig(out SO_DebugConfig sO_Debug))
        {
            if (sO_Debug.mergeLevel != MergeLevelType.none && CurBranchDef.BranchType != sO_Debug.mergeLevel)
            {
                SaveUtils.SetInt(Consts.SaveKey_Branch_Point, branchPoint);
                claimedRewardIndexSet.Clear();
                SaveUtils.SetString(Consts.SaveKey_Branch_ClaimedRewards, string.Empty);
                CurTaskList.Clear();
                
                if (sO_Debug.mergeLevel== MergeLevelType.branch1) 
                {
                    currentVerson = 1;
                }
                else if (sO_Debug.mergeLevel == MergeLevelType.branch_halloween)
                {
                    currentVerson = 2;
                }
                else if (sO_Debug.mergeLevel == MergeLevelType.branch_christmas)
                {
                    currentVerson = 3;
                }
                else if (sO_Debug.mergeLevel == MergeLevelType.branch_SpurLine4)
                {
                    currentVerson = 4;
                }
                else if (sO_Debug.mergeLevel == MergeLevelType.branch_SpurLine5)
                {
                    currentVerson = 5;
                }
                else if (sO_Debug.mergeLevel == MergeLevelType.branch_SpurLine6)
                {
                    currentVerson = 6;
                }

                if (BranchControlDefinition.TotalBranchCnfDict.TryGetValue(currentVerson, out var cnf1))
                {
                    CurBranchDef = cnf1;
                }
                if (BranchDefinition.allBranchdefDic.ContainsKey(sO_Debug.mergeLevel)) 
                {
                    var taskDic = BranchDefinition.allBranchdefDic[CurBranchDef.BranchType];
                    if (taskDic != null && taskDic.Count > 0)
                    {
                        CurTaskList.Add(taskDic.First().Key);
                    }
                }
            }
        }
#endif
        //存储数据
        SaveUtils.SetString(Consts.SaveKey_Branch_CurTasks, JsonConvert.SerializeObject(CurTaskList));
        MergeLevelManager.Instance.TryDeleteMapDataByDungeonType(CurBranchDef.BranchType);//重置地图
        curStartTime = TimeManager.Instance.UtcNow();
        Branch_EndTime = curStartTime.AddDays(NewLoopTime);
        SaveUtils.SetString(Consts.SaveKey_Branch_CurVersionTime, curStartTime.ToString());//重置时间存档
        SaveUtils.SetInt(Consts.SaveKey_currentVerson, currentVerson);//重置版本 
        #endregion
    }

    private void TryPushGuaranteeRewards() 
    {
        if (SaveUtils.HasKey("Save_Key_BranchGuaranteeRewardsPop"))
        {
            return;
        }
        if (claimedRewardIndexSet.Count > 0)
        {
            return;
        }
        lastVerson = currentVerson;
        SaveUtils.SetInt(Consts.SaveKey_lastVerson, lastVerson);
        SaveUtils.SetBool("Save_Key_BranchGuaranteeRewardsPop", true);
        InitLastRewardList();
    }

    //检测当前时间段的支线活动
    public void CheckBranchCnf()
    {
        if (BranchControlDefinition.TotalBranchCnfDict.TryGetValue(currentVerson, out var cnf))
        {
            CurBranchDef = cnf;
        }
#if UNITY_EDITOR
        if (DebugSetting.CanUseDebugConfig(out SO_DebugConfig sO_Debug))
        {
            if (sO_Debug.mergeLevel != MergeLevelType.none) 
            {
                if (CurBranchDef == null || CurBranchDef.BranchType != sO_Debug.mergeLevel)
                {
                    TryResetNewSeason();
                }
            }
        }
#endif

        if (!SaveUtils.HasKey(Consts.SaveKey_Branch_CurVersionTime))//没有新的时间存档
        {
            TryResetNewSeason();
        }
        else
        {
            string timeStr = SaveUtils.GetString(Consts.SaveKey_Branch_CurVersionTime);
            if (!string.IsNullOrEmpty(timeStr) && DateTimeOffset.TryParse(timeStr, out DateTimeOffset timeOffset))
            {
                curStartTime = timeOffset;
                Branch_EndTime = curStartTime.AddDays(NewLoopTime);
                if (TimeManager.Instance.UtcNow() > curStartTime.AddDays(NewLoopTime))
                {
                    TryPushGuaranteeRewards();
                }
                if (TimeManager.Instance.UtcNow() > curStartTime.AddDays(NewLoopTime + 1))
                {
                    //重置支线
                    TryResetNewSeason();
                }
            }
            else
            {
                TryResetNewSeason();
            }
        }
    }

    //进入支线游戏
    public void EnterGame()
    {
        if (GetIsOpen())
            MergeLevelManager.Instance.ShowMergePanelByDungeonType(CurBranchDef.BranchType);
    }

    //获取上一个支线信息
    public BranchControlDefinition GetLastBranchControl() 
    {
        if (BranchControlDefinition.TotalBranchCnfDict.TryGetValue(lastVerson, out var cnf))
        {
            return cnf;
        }
        return null;
    }

    private void InitLastRewardList() 
    {
        if (BranchControlDefinition.TotalBranchCnfDict.TryGetValue(lastVerson, out var cnf))
        {
            if (!BranchRewardDefinition.BranchRewardDict.ContainsKey(cnf.BranchType))
                return;
            SmallList<BranchRewardDefinition> branchList = BranchRewardDefinition.BranchRewardDict[cnf.BranchType];
            for (int i = 0; i < branchList.Count; i++)
            {
                foreach (var item in branchList[i].rewardDataList)
                {
                    LastRewardList.Add(item);
                }
            }
        }
    }


    //获取当前支线正在进行的任务或当前可领取的任务
    public BranchRewardDefinition GetCurrentBranchDef()
    {
        BranchRewardDefinition branchReward = null;
        if (CurBranchDef != null && BranchRewardDefinition.BranchRewardDict.ContainsKey(CurBranchDef.BranchType))
        {
            SmallList<BranchRewardDefinition> branchList = BranchRewardDefinition.BranchRewardDict[CurBranchDef.BranchType];
            for (int i = 0; i < branchList.Count; i++)
            {
                if (!IsRewardClaimed(branchList[i].goalPoint))
                {
                    branchReward = branchList[i];
                    break;
                }
            }
        }
        return branchReward;
    }

    public int GetCurrentIndex() 
    {
        int currentIndex = 0;
        if (CurBranchDef != null && BranchRewardDefinition.BranchRewardDict.ContainsKey(CurBranchDef.BranchType))
        {
            SmallList<BranchRewardDefinition> branchList = BranchRewardDefinition.BranchRewardDict[CurBranchDef.BranchType];
            for (int i = 0; i < branchList.Count; i++)
            {
                if (!IsRewardClaimed(branchList[i].goalPoint))
                {
                    currentIndex = i;
                    break;
                }
            }
        }
        return currentIndex;
    }

    //完成任务
    public void CompleteTask(string taskid)
    {
        if (CurTaskList.Contains(taskid))
        {
            CurTaskList.Remove(taskid);
            if (BranchDefinition.allBranchdefDic.TryGetValue(CurBranchDef.BranchType, out Dictionary<string, BranchDefinition> defDic))
            {
                if (defDic.ContainsKey(taskid))
                {
                    var completeTask = defDic[taskid];
                    foreach (var reward in completeTask.rewardItemList)
                    {
                        if (reward.IsRewardBranchPoint)
                            AddBranchPoint(reward.num);
                    }
                    for (int i = 0; i < completeTask.nextIdList.Count; i++)
                    {
                        if (!CurTaskList.Contains(completeTask.nextIdList[i]))
                        {
                            CurTaskList.Add(completeTask.nextIdList[i]);
                        }
                    }
                }
                SaveUtils.SetString(Consts.SaveKey_Branch_CurTasks, JsonConvert.SerializeObject(CurTaskList));
            }
        }
    }

    //增加支线活动积分
    public void AddBranchPoint(int point)
    {
        branchPoint += point;
        SaveUtils.SetInt(Consts.SaveKey_Branch_Point, branchPoint);
    }

    //获取当前支线活动奖励列表
    public SmallList<BranchRewardDefinition> GetRewardsList()
    {
        if (CurBranchDef != null && BranchRewardDefinition.BranchRewardDict.ContainsKey(CurBranchDef.BranchType))
            return BranchRewardDefinition.BranchRewardDict[CurBranchDef.BranchType];
        return null;
    }

    //分数是否达到领取奖励的标准
    public bool CanClaim(int point)
    {
        return branchPoint >= point;
    }

    /// <summary>
    /// 此分数对应的奖励是否领取
    /// </summary>
    /// <param name="point">分数</param>
    /// <returns></returns>
    public bool IsRewardClaimed(int point)
    {
        return claimedRewardIndexSet.Contains(point);
    }

    //领取此分数对应的奖励
    public void ClaimReward(int point)
    {
        if (!claimedRewardIndexSet.Contains(point))
        {
            if (point==20) 
            {
                AnalyticsUtil.trackActivityStart(CurBranchDef.BranchType.ToString(), "");
            }
            AnalyticsUtil.trackActivityStep(CurBranchDef.BranchType.ToString(), point / 20);
            claimedRewardIndexSet.Add(point);
            SaveUtils.SetString(Consts.SaveKey_Branch_ClaimedRewards, JsonConvert.SerializeObject(claimedRewardIndexSet));
        }
        //领取奖励时判断是否完成所有支线，完成则重置
        SmallList<BranchRewardDefinition> list = GetRewardsList();
        if (claimedRewardIndexSet.Count >= list.Count)
        {
            AnalyticsUtil.trackActivityEnd(CurBranchDef.BranchType.ToString());
            curStartTime = TimeManager.Instance.UtcNow().AddDays(-NewLoopTime);
            SaveUtils.SetString(Consts.SaveKey_Branch_CurVersionTime, curStartTime.ToString());//时间存档
            SaveUtils.SetBool("Is_Show_Branch", true);
            refreshByReset?.Invoke();
        }
        FestivalSystem.Instance.refreshFestival?.Invoke();
        UI_PagePlay_Slider.refreshAction?.Invoke();
    }

    //获取当前可领取奖励个数
    public int GetCanClaimRewardCount()
    {
        int count = 0;
        var rewardList = GetRewardsList();
        if (rewardList != null)
        {
            for (int i = 0; i < rewardList.Count; i++)
            {
                if (CanClaim(rewardList[i].goalPoint) && !IsRewardClaimed(rewardList[i].goalPoint))
                    count++;
            }
        }
        return count;
    }

    //根据积分获取进度
    public Vector2Int GetPointProgress(int point)
    {
        var lastPoint = 0;
        var rewards = GetRewardsList();
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


    //读取本地数据
    private void LoadLocalData()
    {
        BranchVersion = SaveUtils.GetInt(Consts.SaveKey_Branch_Version);
        BranchLoopCount = SaveUtils.GetInt(Consts.SaveKey_Branch_LoopCount);
        currentVerson = SaveUtils.GetInt(Consts.SaveKey_currentVerson);
        lastVerson = SaveUtils.GetInt(Consts.SaveKey_lastVerson);
        branchPoint = SaveUtils.GetInt(Consts.SaveKey_Branch_Point);
        var strSet = SaveUtils.GetString(Consts.SaveKey_Branch_ClaimedRewards);
        if (!string.IsNullOrEmpty(strSet))
            claimedRewardIndexSet = JsonConvert.DeserializeObject<List<int>>(strSet);
        var strTasks = SaveUtils.GetString(Consts.SaveKey_Branch_CurTasks);
        if (!string.IsNullOrEmpty(strTasks))
            CurTaskList = JsonConvert.DeserializeObject<List<string>>(strTasks);
    }

    #region 云存档
    public Dictionary<string, object> GetSaveDataToFirestore()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("version", BranchVersion);
        dic.Add("loopCount", BranchLoopCount);
        dic.Add("branchPoint", branchPoint);
        dic.Add("claimIndex", JsonConvert.SerializeObject(claimedRewardIndexSet));
        dic.Add("taskList", JsonConvert.SerializeObject(CurTaskList));
        dic.Add("startTime", curStartTime.ToString());
        dic.Add("currentVerson", currentVerson);
        dic.Add("lastVerson", lastVerson);
        return dic;
    }
    public void SetSaveDataFromFirestore(Dictionary<string, object> dic)
    {
        if (dic == null)
        {
            GameDebug.LogError("branchTask数据为空");
            return;
        }
        if (dic.TryGetValue("version", out object version))
        {
            if (int.TryParse(version.ToString(), out int version_Int))
            {
                BranchVersion = version_Int;
                SaveUtils.SetInt(Consts.SaveKey_Branch_Version, BranchVersion);
            }
        }
        if (dic.TryGetValue("loopCount", out object loopCount))
        {
            if (int.TryParse(loopCount.ToString(), out int loopCount_Int))
            {
                BranchLoopCount = loopCount_Int;
                SaveUtils.SetInt(Consts.SaveKey_Branch_LoopCount, BranchLoopCount);
            }
        }
        if (dic.TryGetValue("branchPoint", out object point))
        {
            if (int.TryParse(point.ToString(), out int point_Int))
            {
                branchPoint = point_Int;
                SaveUtils.SetInt(Consts.SaveKey_Branch_Point, branchPoint);
            }
        }
        if (dic.TryGetValue("claimIndex", out object claimIndex))
        {
            claimedRewardIndexSet = JsonConvert.DeserializeObject<List<int>>(claimIndex.ToString());
            SaveUtils.SetString(Consts.SaveKey_Branch_ClaimedRewards, JsonConvert.SerializeObject(claimedRewardIndexSet));
        }
        if (dic.TryGetValue("taskList", out object taskList))
        {
            CurTaskList = JsonConvert.DeserializeObject<List<string>>(taskList.ToString());
            SaveUtils.SetString(Consts.SaveKey_Branch_CurTasks, JsonConvert.SerializeObject(CurTaskList));
        }

        if (dic.TryGetValue("startTime", out object curTime))
        {
            if (DateTimeOffset.TryParse(curTime.ToString(), out DateTimeOffset timeOffset))
            {
                curStartTime = timeOffset;
                Branch_EndTime = curStartTime.AddDays(NewLoopTime);
                SaveUtils.SetString(Consts.SaveKey_Branch_CurVersionTime, curStartTime.ToString());
            }
        }
        
        if (dic.TryGetValue("lastVerson", out object objlastVerson))
        {
            if (int.TryParse(objlastVerson.ToString(), out int _lastVerson))
            {
                lastVerson = _lastVerson;
            }
        }
        if (dic.TryGetValue("currentVerson", out object objCurrentVerson))
        {
            if (int.TryParse(objCurrentVerson.ToString(), out int _currentVerson))
            {
                currentVerson = _currentVerson;
                //之前玩家没有存 默认0
                if (currentVerson <= 0)
                {
                    currentVerson = 1;
                }
                CheckBranchCnf();
                SaveUtils.SetInt(Consts.SaveKey_currentVerson, currentVerson);
            }
        }
        RefreshBranchEvent?.Invoke();
    }
    #endregion

}
