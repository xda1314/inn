using BDUnity.Utils;
using ivy.game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTaskDefinition
{
    private static string k_DataResourceFileName = "DailyTask";
    public static Dictionary<string, DailyTaskDefinition> dailyTaskDefinitionDic { get; private set; } = new Dictionary<string, DailyTaskDefinition>();//[dailyTaskId,definition]
    public static Dictionary<string, DailyTaskData> locationDataDic { get; private set; } = new Dictionary<string, DailyTaskData>();//[LocationID,data]

    public string dailyTaskId;
    public string LocationID;
    public string Icon;
    public DailyTaskLocationType LocationType;
    public string Text_Key;
    public DailyTaskType taskType;
    public int maxRateProgress;
    public string targetPrefab;
    public int continueTask;
    public List<MergeRewardItem> rewardItemList = new List<MergeRewardItem>();
    public static void Init()
    {
        Dictionary<string, CustomJSONObject> dict = GameConfig.Instance.GetConfig(k_DataResourceFileName).dict;
        if (dict != null && dict.Count == 1)
        {
            Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
            enumerator.MoveNext();
            Dictionary<string, CustomJSONObject> dict2 = enumerator.Current.Value.dict;
            enumerator = dict2.GetEnumerator();
            //解析字段
            while (enumerator.MoveNext())
            {
                CustomJSONObject value = enumerator.Current.Value;
                DailyTaskDefinition def = new DailyTaskDefinition();
                def.dailyTaskId = value.GetString("ID", string.Empty);
                def.LocationID = value.GetString("LocationID", string.Empty);
                def.Icon = value.GetString("Icon", string.Empty);
                string locationType = value.GetString("LocationType", string.Empty);
                if (string.IsNullOrEmpty(locationType)) 
                {
                    def.LocationType = DailyTaskLocationType.fix;
                }
                else if (locationType == "common")
                {
                    def.LocationType = DailyTaskLocationType.common;
                }
                else if (locationType == "random")
                {
                    def.LocationType = DailyTaskLocationType.random;
                }
                else 
                {
                    GameDebug.LogError("daily task LocationType error :" + locationType);
                }
                def.Text_Key = value.GetString("Text_Key", string.Empty);
                string dailyTaskType = value.GetString("Type", string.Empty);
                switch (dailyTaskType)
                {
                    case "TaskComplete":
                        def.taskType = DailyTaskType.TaskComplete;
                        break;
                    case "ShopBuy":
                        def.taskType = DailyTaskType.ShopBuy;
                        break;
                    case "AddEnergy":
                        def.taskType = DailyTaskType.AddEnergy;
                        break;
                    case "SpendEnergy":
                        def.taskType = DailyTaskType.SpendEnergy;
                        break;
                    case "Collect":
                        def.taskType = DailyTaskType.Collect;
                        break;
                    default:
                        Debug.LogError("taskType解析错误:" + dailyTaskType);
                        break;
                }
                def.maxRateProgress = value.GetInt("Rate", 0);
                def.targetPrefab = value.GetString("Aim", string.Empty);
                string continueTask = value.GetString("ContinueTask", string.Empty);
                if (string.IsNullOrEmpty(continueTask))
                {
                    def.continueTask = -1;
                }
                else 
                {
                    if (int.TryParse(continueTask, out int num)) 
                    {
                        def.continueTask = num;
                    }
                }
                if (ExtensionTool.TryParseMergeRewardItem(value.GetString("Reward1", string.Empty), out MergeRewardItem reward1)) 
                {
                    def.rewardItemList.Add(reward1);
                }
                if (ExtensionTool.TryParseMergeRewardItem(value.GetString("Reward2", string.Empty), out MergeRewardItem reward2))
                {
                    def.rewardItemList.Add(reward2);
                }
                if (ExtensionTool.TryParseMergeRewardItem(value.GetString("Reward3", string.Empty), out MergeRewardItem reward3))
                {
                    def.rewardItemList.Add(reward3);
                }
                dailyTaskDefinitionDic.Add(def.dailyTaskId, def);

                //存储数据
                if (!locationDataDic.ContainsKey(def.LocationID)) 
                {
                    DailyTaskData taskData = new DailyTaskData();
                    locationDataDic.Add(def.LocationID, taskData);
                }
                if (locationDataDic.TryGetValue(def.LocationID, out DailyTaskData data)) 
                {
                    if (def.LocationType == DailyTaskLocationType.random)
                    {
                        data.randomTaskIdList.Add(def.dailyTaskId);
                    }
                    else if (def.LocationType == DailyTaskLocationType.common)
                    {
                        data.commonTaskId = def.dailyTaskId;
                        data.taskLevel = def.continueTask;
                    }
                    else if (def.LocationType == DailyTaskLocationType.fix) 
                    {
                        data.fixTaskId = def.dailyTaskId;
                    }
                    data.dailyTaskLocationType = def.LocationType;
                }
            }
        }
        else
        {
            GameDebug.LogError("DailyTaskDefinition::Init: Config is null.");
        }
    }
   
}
public class DailyTaskData 
{
    public DailyTaskLocationType dailyTaskLocationType;
    public int taskLevel;
    public string commonTaskId;
    public List<string> randomTaskIdList = new List<string>();
    public string fixTaskId;
}
public enum DailyTaskType
{
    None,
    TaskComplete,
    ShopBuy,
    AddEnergy,
    SpendEnergy,
    Collect
}
public enum DailyTaskLocationType 
{
    fix,  //固定任务
    common,   //低等级固定任务
    random    //随机任务
}
