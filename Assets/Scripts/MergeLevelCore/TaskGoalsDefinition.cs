using BDUnity.Utils;
using ivy.game;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 订单表
/// </summary>
public class TaskGoalsDefinition
{
    private const string k_DataResourceFileName = "Goals_Main";
    public static Dictionary<string, TaskData> TaskDefinitionsDict { get; private set; } = new Dictionary<string, TaskData>();//每一个订单信息
    public static Dictionary<int, TaskLevelData> taskLevelDataDic { get; private set; } = new Dictionary<int, TaskLevelData>();//每一关卡对应的所有订单信息
    public static Dictionary<string, ChapterData> ChapterDataDic { get; private set; } = new Dictionary<string, ChapterData>();//章节信息,key是章节图片名CurrentPic
    public static Dictionary<string, int> UnlockSkinDic { get; private set; } = new Dictionary<string, int>();//完成某一关之后解锁新皮肤

    public string taskId { get; private set; }
    public List<MergeRewardItem> needItemList { get; private set; } = new List<MergeRewardItem>();
    public List<MergeRewardItem> rewardItemList { get; private set; } = new List<MergeRewardItem>();
    public string PictureName { get; private set; }//顶部spine名
    public string CharacterPic { get; private set; }//任务界面人物图标名
    public string Text { get; private set; }
    public List<int> UnLockAreaIndexList { get; private set; } = new List<int>();
    public List<MergeRewardItem> ChapterRewardList { get; private set; } = new List<MergeRewardItem>();//章节奖励
    public string AtlasPic { get; private set; }
    public string curChapterPic { get; private set; }
    public string previewChapterPic { get; private set; }
    public int levelIndex { get; private set; }//第几关
    public string downloadLabel { get; private set; }//下载资源的adressable的label


    public static void Init()
    {
        Dictionary<string, CustomJSONObject> dict = GameConfig.Instance.GetConfig(k_DataResourceFileName).dict;
        if (dict != null && dict.Count == 1)
        {
            Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
            enumerator.MoveNext();
            Dictionary<string, CustomJSONObject> dict2 = enumerator.Current.Value.dict;
            enumerator = dict2.GetEnumerator();
            UnlockSkinDic.Add("cj00_SkeletonData", 0);//添加默认壁纸
            while (enumerator.MoveNext())
            {
                //单个订单信息
                CustomJSONObject value = enumerator.Current.Value;
                TaskGoalsDefinition taskGoalsDefinition = new TaskGoalsDefinition();
                taskGoalsDefinition.taskId = value.GetString("Goal_ID", string.Empty);
                string[] taskArray = taskGoalsDefinition.taskId.Split('_');
                if (int.TryParse(taskArray[0], out int levelIndex))
                {
                    taskGoalsDefinition.levelIndex = levelIndex;
                }
                string needItem1 = value.GetString("TargetPrefab_1", string.Empty);
                if (!string.IsNullOrEmpty(needItem1))
                {
                    string[] array1 = needItem1.Split(',');
                    if (array1.Length == 2 && array1[0] != null && int.TryParse(array1[1], out int count1))
                    {
                        MergeRewardItem item = new MergeRewardItem();
                        item.name = array1[0];
                        item.num = count1;
                        taskGoalsDefinition.needItemList.Add(item);

                        GameConfig.CheckMergeRewardItemValid(k_DataResourceFileName, item);
                    }
                    else
                    {
                        GameDebug.LogError("data error!" + needItem1);
                    }
                }
                string needItem2 = value.GetString("TargetPrefab_2", string.Empty);
                if (!string.IsNullOrEmpty(needItem2))
                {
                    string[] array2 = needItem2.Split(',');
                    if (array2.Length == 2 && array2[0] != null && int.TryParse(array2[1], out int count2))
                    {
                        MergeRewardItem item = new MergeRewardItem();
                        item.name = array2[0];
                        item.num = count2;
                        taskGoalsDefinition.needItemList.Add(item);

                        GameConfig.CheckMergeRewardItemValid(k_DataResourceFileName, item);
                    }
                    else
                    {
                        Debug.LogError("data error!" + needItem2);
                    }
                }
                List<string> rewardItemList = new List<string>();
                string rewardItem1 = value.GetString("Rewards_1", string.Empty);
                string rewardItem2 = value.GetString("Rewards_2", string.Empty);
                string rewardItem3 = value.GetString("Rewards_3", string.Empty);
                string rewardItem4 = value.GetString("Rewards_4", string.Empty);
                if (!string.IsNullOrEmpty(rewardItem1)) rewardItemList.Add(rewardItem1);
                if (!string.IsNullOrEmpty(rewardItem2)) rewardItemList.Add(rewardItem2);
                if (!string.IsNullOrEmpty(rewardItem3)) rewardItemList.Add(rewardItem3);
                if (!string.IsNullOrEmpty(rewardItem4)) rewardItemList.Add(rewardItem4);
                for (int i = 0; i < rewardItemList.Count; i++)
                {
                    string[] array = rewardItemList[i].Split(',');
                    if (array.Length == 2 && array[0] != null && int.TryParse(array[1], out int count))
                    {
                        MergeRewardItem item = new MergeRewardItem();
                        item.name = array[0];
                        item.num = count;
                        taskGoalsDefinition.rewardItemList.Add(item);

                        GameConfig.CheckMergeRewardItemValid(k_DataResourceFileName, item);
                    }
                    else
                    {
                        GameDebug.LogError("reward item data error :" + rewardItemList[i]);
                    }
                }
                taskGoalsDefinition.PictureName = value.GetString("PictureName", string.Empty);
                taskGoalsDefinition.CharacterPic = value.GetString("CharacterPic", string.Empty);
                taskGoalsDefinition.Text = value.GetString("Text", string.Empty);
                string[] unlockIndex = value.GetString("Index", string.Empty).Split(';');
                for (int i = 0; i < unlockIndex.Length; i++)
                {
                    if (int.TryParse(unlockIndex[i], out int index))
                    {
                        taskGoalsDefinition.UnLockAreaIndexList.Add(index);
                    }
                }
                string[] chapterRewards = value.GetString("ChapterRewards", string.Empty).Split(';');
                if (chapterRewards.Length > 0)
                {
                    for (int i = 0; i < chapterRewards.Length; i++)
                    {
                        string[] array = chapterRewards[i].Split(',');
                        if (array.Length == 2 && array[0] != null && int.TryParse(array[1], out int count))
                        {
                            MergeRewardItem item = new MergeRewardItem();
                            item.name = array[0];
                            item.num = count;
                            taskGoalsDefinition.ChapterRewardList.Add(item);

                            GameConfig.CheckMergeRewardItemValid(k_DataResourceFileName, item);
                        }
                    }
                }
                taskGoalsDefinition.AtlasPic = value.GetString("AtlasPic", string.Empty);
                taskGoalsDefinition.curChapterPic = value.GetString("CurrentPic", string.Empty);
                taskGoalsDefinition.previewChapterPic = value.GetString("PreviewPic", string.Empty);
                taskGoalsDefinition.downloadLabel = value.GetString("DownloadLabel", string.Empty);

                TaskData taskData = new TaskData();
                taskData.taskDefinition = taskGoalsDefinition;
                TaskDefinitionsDict.Add(taskGoalsDefinition.taskId, taskData);

                //每个等级信息
                if (!taskLevelDataDic.ContainsKey(taskGoalsDefinition.levelIndex))
                {
                    TaskLevelData checkpointData = new TaskLevelData();
                    taskLevelDataDic.Add(taskGoalsDefinition.levelIndex, checkpointData);
                }
                if (taskLevelDataDic.TryGetValue(taskGoalsDefinition.levelIndex, out TaskLevelData data))
                {
                    if (taskArray.Length == 2 && !string.IsNullOrEmpty(taskArray[1]))
                    {
                        if (taskArray[1] == "final")
                        {
                            data.finalTaskId = taskGoalsDefinition.taskId;
                            data.curChapterPic = taskGoalsDefinition.curChapterPic;
                            data.previewPic = taskGoalsDefinition.previewChapterPic;
                            UnlockSkinDic[data.previewPic] = taskGoalsDefinition.levelIndex;

                            if (!ChapterDataDic.ContainsKey(taskGoalsDefinition.curChapterPic))
                            {
                                ChapterData chapterData = new ChapterData();
                                ChapterDataDic.Add(taskGoalsDefinition.curChapterPic, chapterData);
                            }
                            if (ChapterDataDic.TryGetValue(taskGoalsDefinition.curChapterPic, out ChapterData chapter))
                            {
                                chapter.taskIdList.Add(taskGoalsDefinition.taskId);
                                if (string.IsNullOrEmpty(chapter.minLevel))
                                {
                                    chapter.minLevel = taskGoalsDefinition.levelIndex.ToString();
                                }
                                chapter.maxLevel = taskGoalsDefinition.levelIndex.ToString();
                            }
                        }
                        else
                        {
                            data.taskIdList.Add(taskGoalsDefinition.taskId);
                            data.maxTaskNum++;
                        }
                        if (!string.IsNullOrEmpty(taskGoalsDefinition.downloadLabel))
                        {
                            data.downloadLabel = taskGoalsDefinition.downloadLabel;
                        }
                    }
                }
            }

            LoadRemoteConfig();
        }
        else
        {
            GameDebug.LogError("TaskGoalsDefinition::Init: Config is null.");
        }
    }


    private static void LoadRemoteConfig()
    {
        try
        {
            string str = RemoteConfigSystem.Instance.GetRemoteConfig_String(RemoteConfigSystem.remoteKey_goals_main_Config);
            if (string.IsNullOrEmpty(str))
            {
                return;
            }


            var largeDict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, object>>>>(str);
            if (largeDict == null)
            {
                return;
            }

            foreach (var dict in largeDict)
            {
                if (dict.Key != "all")
                {
                    string[] verstr = dict.Key.Split('_');
                    if (verstr == null || verstr.Length != 2)
                    {
                        continue;
                    }

                    string currentVersion = Application.isEditor && DebugSetting.CanUseDebugConfig(out var debugSO)
                        ? debugSO.DebugCurrentVersion
                        : Ivy.RiseSdk.Instance.GetConfig(Ivy.RiseSdk.CONFIG_KEY_VERSION_NAME);
                    if (ExtensionTool.TryCompareAppVersion(currentVersion, verstr[1], out int result))
                    {
                        switch (verstr[0])
                        {
                            case "equal":
                                if (result != 0)
                                {
                                    continue;
                                }
                                break;
                            case "less":
                                if (result != -1)
                                {
                                    continue;
                                }
                                break;
                            case "greater":
                                if (result != 1)
                                {
                                    continue;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                foreach (var goalsDict in dict.Value)
                {
                    if (string.IsNullOrEmpty(goalsDict.Key))
                    {
                        continue;
                    }

                    if (TaskDefinitionsDict.TryGetValue(goalsDict.Key, out var taskData) && taskData.taskDefinition != null)//每个任务的目标道具
                    {
                        //替换每个任务的目标道具
                        var needItemList = taskData.taskDefinition.needItemList;
                        List<int> needRemoveNeedList = new List<int>();
                        for (int i = 1; i <= 2; i++)
                        {
                            if (goalsDict.Value.TryGetValue("TargetPrefab_" + i, out object targettemp))
                            {
                                if (needItemList != null && needItemList.Count > 0 && !string.IsNullOrEmpty(targettemp.ToString()))
                                {
                                    if (targettemp.ToString() == "null")
                                    {
                                        needRemoveNeedList.Add(i - 1);
                                    }
                                    else
                                    {
                                        string[] array1 = targettemp.ToString().Split(',');
                                        if (array1.Length == 2 && array1[0] != null && int.TryParse(array1[1], out int count1))
                                        {
                                            MergeRewardItem item = new MergeRewardItem();
                                            item.name = array1[0];
                                            item.num = count1;
                                            if (needItemList.Count > i - 1)
                                            {
                                                needItemList[i - 1] = item;
                                            }
                                            else
                                            {
                                                needItemList.Add(item);
                                            }
                                        }
                                        else
                                        {
                                            GameDebug.LogError("data error targettemp!" + targettemp.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        for (int i = needRemoveNeedList.Count - 1; i >= 0; i--)
                        {
                            if (needItemList.Count > needRemoveNeedList[i])
                            {
                                needItemList.RemoveAt(needRemoveNeedList[i]);
                            }
                            else
                            {
                                GameDebug.LogError("out off array");
                            }
                        }
                        //替换任务和关卡奖励
                        var rewardItemList = taskData.taskDefinition.rewardItemList;
                        List<int> needRemoveRewardList = new List<int>();
                        string[] strs = goalsDict.Key.Split('_');
                        if (strs.Length != 2)
                        {
                            GameDebug.LogError("remote Goal_ID error! error:" + goalsDict.Key);
                        }
                        else
                        {
                            for (int i = 1; i <= 4; i++)
                            {
                                if (goalsDict.Value.TryGetValue("Rewards_" + i, out object rewardtemp))
                                {
                                    if (rewardItemList != null && rewardItemList.Count > 0 && !string.IsNullOrEmpty(rewardtemp.ToString()))
                                    {
                                        if (rewardtemp.ToString() == "null")
                                        {
                                            needRemoveRewardList.Add(i - 1);
                                        }
                                        else
                                        {
                                            string[] array1 = rewardtemp.ToString().Split(',');
                                            if (array1.Length == 2 && array1[0] != null && int.TryParse(array1[1], out int count1))
                                            {
                                                MergeRewardItem item = new MergeRewardItem();
                                                item.name = array1[0];
                                                item.num = count1;
                                                if (rewardItemList.Count > i - 1)
                                                {
                                                    rewardItemList[i - 1] = item;
                                                }
                                                else
                                                {
                                                    rewardItemList.Add(item);
                                                }
                                            }
                                            else
                                            {
                                                GameDebug.LogError("data error rewardtemp!" + rewardtemp.ToString());
                                            }
                                        }
                                    }
                                }
                            }
                            for (int i = needRemoveRewardList.Count - 1; i >= 0; i--)
                            {
                                if (rewardItemList.Count > needRemoveRewardList[i])
                                {
                                    rewardItemList.RemoveAt(needRemoveRewardList[i]);
                                }
                                else
                                {
                                    GameDebug.LogError("out off array");
                                }
                            }
                        }
                        //替换章节奖励
                        if (goalsDict.Value.TryGetValue("ChapterRewards", out object rewardStr))
                        {
                            if (taskData.taskDefinition.ChapterRewardList.Count == 0)
                            {
                                GameDebug.LogError("the task has no chapter reward! taskId:" + goalsDict.Key);
                            }
                            var chapterRewardList = taskData.taskDefinition.ChapterRewardList;
                            string[] chapterRewards = rewardStr.ToString().Split(';');
                            if (chapterRewards.Length > 0)
                            {
                                for (int i = 0; i < chapterRewards.Length; i++)
                                {
                                    string[] array = chapterRewards[i].Split(',');
                                    if (array.Length == 2 && array[0] != null && int.TryParse(array[1], out int count))
                                    {
                                        MergeRewardItem item = new MergeRewardItem();
                                        item.name = array[0];
                                        item.num = count;
                                        if (chapterRewardList.Count > i)
                                        {
                                            chapterRewardList[i] = item;
                                        }
                                        else
                                        {
                                            chapterRewardList.Add(item);
                                        }
                                    }
                                }
                            }
                            if (chapterRewards.Length < chapterRewardList.Count)
                            {
                                for (int i = chapterRewardList.Count - 1; i > chapterRewards.Length - 1; i--)
                                {
                                    chapterRewardList.Remove(chapterRewardList[i]);
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (System.Exception e)
        {

        }

    }

}


/// <summary>
/// 订单信息
/// </summary>
public class TaskData
{
    public TaskGoalsDefinition taskDefinition;
    public bool isComplete = false;//订单完成
    public bool isCompletePart = false;//订单完成部分
    public string taskStory;//订单完成时显示的文本
}
/// <summary>
/// 关卡数据
/// </summary>
public class TaskLevelData
{
    public List<string> taskIdList = new List<string>();
    public string finalTaskId;
    public int maxTaskNum = 0;
    public string curChapterPic;
    public string previewPic;
    public string downloadLabel;
}
public class ChapterData
{
    public List<string> taskIdList = new List<string>();
    public string minLevel;
    public string maxLevel;
}
