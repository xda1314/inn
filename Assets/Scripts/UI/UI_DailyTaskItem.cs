using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_DailyTaskItem : MonoBehaviour
{
    [SerializeField] private GameObject[] taskBgs; //0未完成，1完成，2已领取
    [SerializeField] private Button[] taskBtns;//0未完成，1完成，2已领取
    [SerializeField] private TextMeshProUGUI[] t_Btns;//0未完成，1完成，2已领取
    [SerializeField] private TextMeshProUGUI[] lbl_reward;//0是经验，1,2是物品数量
    [SerializeField] private GameObject hasClaimTag;
    [SerializeField] private Material material_Green;
    [SerializeField] private Material material_Brown;

    [SerializeField] private TextMeshProUGUI t_TaskDesc;
    [SerializeField] private Transform targetTrans;
    [SerializeField] private TextMeshProUGUI t_Progress;
    [SerializeField] private Transform[] rewardTran;
    

    private DailyTaskState _state = DailyTaskState.NotFinish;
    DailyTaskDefinition dailyTaskDefinition;
    UIPanel_DailyTask uIPanel_DailyTask = null;
    int hasItemNum = 0;
    public void InitItem(DailyTaskDefinition dailyTaskDefinition, UIPanel_DailyTask panel)
    {
        taskBtns[0].onClick.AddListener(GotoBtnClick);
        taskBtns[1].onClick.AddListener(ClaimBtnClick);
        
        this.dailyTaskDefinition = dailyTaskDefinition;
        uIPanel_DailyTask = panel;
        string iconName = dailyTaskDefinition.taskType == DailyTaskType.Collect ? dailyTaskDefinition.targetPrefab : dailyTaskDefinition.Icon;
        AssetSystem.Instance.Instantiate(iconName, targetTrans, Vector3.zero, Vector3.zero, Vector3.one * 0.8f).gameObject.transform.SetAsFirstSibling();

        for (int i = 0; i < dailyTaskDefinition.rewardItemList.Count; i++)
        {
            if (dailyTaskDefinition.rewardItemList[i].name != "exp_BP")
            {
                GameObject go = AssetSystem.Instance.Instantiate(dailyTaskDefinition.rewardItemList[i].name, rewardTran[i], Vector3.zero, Vector3.zero, Vector3.one * 0.7f);
                go.transform.SetAsFirstSibling();
            }
            lbl_reward[i].text = "x" + dailyTaskDefinition.rewardItemList[i].num.ToString();
        }
    }
    public void RefreshItem()
    {
        t_TaskDesc.text = I2.Loc.ScriptLocalization.Get(dailyTaskDefinition.Text_Key);      

        if (DailyTaskSystem.Instance.dailyTaskProgressDic.TryGetValue(dailyTaskDefinition.taskType, out int progress))
        {        
            if (dailyTaskDefinition.taskType == DailyTaskType.Collect)
            {
                hasItemNum = MergeLevelManager.Instance.ReturnItemNumByPrefabName(dailyTaskDefinition.targetPrefab, MergeLevelManager.Instance.CurrentLevelType);
            }
            else
            {
                hasItemNum = progress;
            }
            if (hasItemNum >= dailyTaskDefinition.maxRateProgress) 
            {
                _state = DailyTaskState.Finish;
            }
        }
        if (DailyTaskSystem.Instance.dailyTaskLocationData.TryGetValue(dailyTaskDefinition.LocationID, out SaveDailytaskItemData dailyTaskItemData)) 
        {
            if (dailyTaskItemData.hasClaim) 
            {
                _state = DailyTaskState.HasClaim;
            }
        }

        RefreshItemState();
    }
    private void RefreshItemState() 
    {
        for (int i = 0; i < taskBgs.Length; i++)
        {
            if (taskBgs[i].activeSelf)
                taskBgs[i].SetActive(false);
        }

        switch (_state) 
        {
            case DailyTaskState.NotFinish:
                taskBgs[0].SetActive(true);
                t_Btns[0].text = I2.Loc.ScriptLocalization.Get("Obj/Chain/DairyTaskButton1");
                t_Progress.text = hasItemNum.ToString() + "/" + dailyTaskDefinition.maxRateProgress.ToString();
                //t_Progress.fontMaterial = material_Brown;
                t_TaskDesc.color = new Color32(189, 109, 81, 255);
                hasClaimTag.SetActive(false);
                for (int i = 0; i < lbl_reward.Length; i++)
                {
                    lbl_reward[i].color = new Color32(169, 72, 38, 255);
                }
                break;
            case DailyTaskState.Finish:
                taskBgs[1].SetActive(true);
                t_Btns[1].text = I2.Loc.ScriptLocalization.Get("Obj/Chain/DairyTaskButton3");
                t_Progress.text = hasItemNum.ToString() + "/" + dailyTaskDefinition.maxRateProgress.ToString();
                //t_Progress.fontMaterial = material_Green;
                t_TaskDesc.color = new Color32(92, 149, 80, 255);
                hasClaimTag.SetActive(false);
                for (int i = 0; i < lbl_reward.Length; i++)
                {
                    lbl_reward[i].color = new Color32(68, 123, 56, 255);
                }
                break;
            case DailyTaskState.HasClaim:
                taskBgs[2].SetActive(true);
                t_Btns[2].text = I2.Loc.ScriptLocalization.Get("Obj/Chain/DairyTaskButton2");
                t_Progress.text = dailyTaskDefinition.maxRateProgress.ToString() + "/" + dailyTaskDefinition.maxRateProgress.ToString();
                //t_Progress.fontMaterial = material_Brown;
                t_TaskDesc.color = new Color32(180, 142, 129, 255);
                hasClaimTag.SetActive(true);
                for (int i = 0; i < lbl_reward.Length; i++)
                {
                    lbl_reward[i].color = new Color32(202, 155, 132, 255);
                }
                break;
            default:
                GameDebug.LogError("daily task state error!");
                break;
        }
    }

    
    public bool ReturnIsComplete() 
    {
        return _state == DailyTaskState.Finish;
    }
    public bool ReturnHasClaim() 
    {
        return _state == DailyTaskState.HasClaim;
    }


    private void ClaimBtnClick()
    {
        //发放奖励
        List<MergeRewardItem> rewardItemList = new List<MergeRewardItem>();
        for (int i = 0; i < dailyTaskDefinition.rewardItemList.Count; i++)
        {
            if (dailyTaskDefinition.rewardItemList[i].name == "exp_BP")
            {
                BattlePassSystem.Instance.AddExpByCompleteDailyTask(dailyTaskDefinition.rewardItemList[i].num); //增加battlepass经验
            }
            else 
            {
                rewardItemList.Add(dailyTaskDefinition.rewardItemList[i]);
            }
        }
        GameManager.Instance.GiveRewardItem(rewardItemList, "DailyTaskReward", Vector3.zero);
        //收集任务需要销毁合成界面该目标
        if (dailyTaskDefinition.taskType == DailyTaskType.Collect) 
        {
            List<MergeRewardItem> needItemList = new List<MergeRewardItem>();
            MergeRewardItem needItem = new MergeRewardItem();
            needItem.name = dailyTaskDefinition.targetPrefab;
            needItem.num = 1;
            needItemList.Add(needItem);
            MergeLevelManager.Instance.RemoveDateFromMap(MergeLevelManager.Instance.CurrentLevelType, needItemList, true);
        }
        //刷新数据和页面
        DailyTaskSystem.Instance.TryCompleteDailyTask(dailyTaskDefinition.LocationID);
        RefreshItem();
        uIPanel_DailyTask.SortRefreshItems();
        uIPanel_DailyTask.RefreshSlider(rewardTran[0].position);
    }
    private void GotoBtnClick()
    {
        uIPanel_DailyTask.OpenWhichUI(dailyTaskDefinition.taskType);       
    }


    private enum DailyTaskState 
    {
        NotFinish,
        Finish,
        HasClaim,
    }
}
