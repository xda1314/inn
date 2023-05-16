using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 红色礼包管理
/// </summary>
public class RewardBoxManager
{
    public static RewardBoxManager Instance { get; private set; } = new RewardBoxManager();

    public Dictionary<MergeLevelType, List<string>> rewardItemDic { get; private set; } = new Dictionary<MergeLevelType, List<string>>();

    /// <summary>
    /// 添加固定优先产出
    /// </summary>
    /// <param name="itemName"></param>
    private void AddFixeItem(Queue<string> itemName)
    {
        if (itemName == null || itemName.Count <= 0)
        {
            return;
        }
        if (rewardItemDic.TryGetValue(MergeLevelType.mainLine, out List<string> rewardList))
        {
            while (itemName.Count > 0)
            {
                rewardList.Add(itemName.Dequeue());
            }
        }
        else
        {
            List<string> rewardList2 = new List<string>();
            while (itemName.Count > 0)
            {
                rewardList2.Add(itemName.Dequeue());
            }
            rewardItemDic.Add(MergeLevelType.mainLine, rewardList2);
        }



        SaveData();
    }

    /// <summary>
    /// 添加奖励物品
    /// </summary>
    /// <param name="levelType">副本类型</param>
    /// <param name="rewardItem"></param>
    public void AddRewardItem(MergeLevelType levelType, MergeRewardItem rewardItem, bool refresh = true)
    {
        if (string.IsNullOrEmpty(rewardItem.name) || rewardItem.num <= 0)
        {
            return;
        }

        if (rewardItemDic.TryGetValue(levelType, out List<string> rewardList))
        {
            for (int i = 0; i < rewardItem.num; i++)
            {
                rewardList.Add(rewardItem.name);
            }
        }
        else
        {
            List<string> rewardList2 = new List<string>();
            for (int i = 0; i < rewardItem.num; i++)
            {
                rewardList2.Add(rewardItem.name);
            }
            rewardItemDic.Add(levelType, rewardList2);
        }

        if (refresh && MergeController.CurrentController != null)
        {
            MergeController.CurrentController.RefreshRewardBox();
        }
        SaveData();
    }



    public bool TrySpawnItem(MergeLevelType levelType, out string prefabName)
    {
        if (rewardItemDic.TryGetValue(levelType, out List<string> rewardList))
        {
            if (rewardList.Count != 0)
            {
                string itemName = rewardList[0];
                prefabName = itemName;
                return true;
            }

            prefabName = string.Empty;
            return false;
        }
        else
        {
            prefabName = string.Empty;
            return false;
        }
    }
    public void TryDestroyItemAndSaveData(MergeLevelType levelType, string itemName)
    {
        if (rewardItemDic.TryGetValue(levelType, out List<string> rewardList))
        {
            if (rewardList.Contains(itemName))
            {
                rewardList.Remove(itemName);
            }
        }
        else
        {
            GameDebug.LogError("该副本对应的临时背包未包含该数据");
        }

        SaveData();
    }

    public void ClearData()
    {
        SaveUtils.DeleteKey(Consts.SaveKey_RewardBox_Reward);
        SaveUtils.DeleteKey(Consts.SaveKey_RewardBox_FixedTutorial);
    }

    public void ResetDataFromFirestore(List<string> normalReward)
    {
        //if (normalReward != null)
        //    rewardItemList = normalReward;
        SaveData();
    }

    public void SaveData()
    {
        try
        {
            if (rewardItemDic.Count <= 0)
            {
                if (SaveUtils.HasKey(Consts.SaveKey_RewardBox_Reward))
                {
                    SaveUtils.DeleteKey(Consts.SaveKey_RewardBox_Reward);
                }
            }
            else
            {
                string rewardStr = JsonConvert.SerializeObject(rewardItemDic);
                if (!string.IsNullOrEmpty(rewardStr))
                {
                    SaveUtils.SetString(Consts.SaveKey_RewardBox_Reward, rewardStr);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("[RewardBoxManager]SaveData error!" + e);
        }
    }


    public void LoadData()
    {
        try
        {
            if (SaveUtils.HasKey(Consts.SaveKey_RewardBox_Reward))
            {
                string rewardStr = SaveUtils.GetString(Consts.SaveKey_RewardBox_Reward);
                if (!string.IsNullOrEmpty(rewardStr))
                {
                    var rewardTemp = JsonConvert.DeserializeObject<Dictionary<MergeLevelType, List<string>>>(rewardStr);
                    if (rewardTemp != null)
                    {
                        rewardItemDic = rewardTemp;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("[RewardBoxManager]LoadData error!" + e);
        }
        TryAddEditorItem();
        //TryAddTutorialItem();
    }
    #region 云存档
    public void SetSaveDataFromFirestore(Dictionary<MergeLevelType, List<string>> dic) 
    {
        if (dic == null) 
        {
            GameDebug.LogError("rewardItemDic为空");
            return;         
        }
        rewardItemDic = dic;
        SaveData();
    }
    public Dictionary<MergeLevelType, List<string>> GetSaveDataToFirestore() 
    {
        return rewardItemDic;
    }
    #endregion
    private void TryAddEditorItem()
    {
#if UNITY_EDITOR
        if (DebugSetting.CanUseDebugConfig(out var debugSO)
            && debugSO.DebugAddToRewardBoxActive
            && debugSO.DebugAddToRewardBox != null)
        {
            Queue<string> queue0 = new Queue<string>();
            foreach (var item in debugSO.DebugAddToRewardBox)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    queue0.Enqueue(item);
                }
            }
            AddFixeItem(queue0);
        }
#endif
    }
    /// <summary>
    /// 尝试向临时背包中增加引导所需物品
    /// </summary>
    private void TryAddTutorialItem()
    {
        if (!SaveUtils.HasKey(Consts.SaveKey_RewardBox_FixedTutorial))
        {
            Queue<string> queue1 = new Queue<string>();
            queue1.Enqueue("AuxiliaryTool_1");
            queue1.Enqueue("AuxiliaryTool_1");
            queue1.Enqueue("GardenTool_2");
            queue1.Enqueue("GardenTool_1");
            queue1.Enqueue("AuxiliaryTool_1");
            AddFixeItem(queue1);
            SaveUtils.SetBool(Consts.SaveKey_RewardBox_FixedTutorial, true);
        }

    }
}
