using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using I2.Loc;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using IvyCore;


public class UIPanelData_BranchTask : UIPanelDataBase
{
    public MergeLevelType levelType;
    public UIPanelData_BranchTask(MergeLevelType levelType) : base(Consts.UIPanel_BranchTask)
    {
        this.levelType = levelType;
    }
}
/// <summary>
/// 支线任务界面
/// </summary>
public class UIPanel_BranchTask : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI t_Title;
    [SerializeField] private TextMeshProUGUI t_curScoreText;
    [SerializeField] private TextMeshProUGUI t_curScore;
    [SerializeField] private VerticalLayoutGroup itemGrid;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI t_Slider;
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button completeBtn;//前往领取界面
    [SerializeField] private GameObject redPoint;
    [SerializeField] private TextMeshProUGUI completeText;
    [SerializeField] private GameObject canClaimTag;//任务全部完成，但存在未领取

    private MergeLevelType curLevelType;
    public override void OnInitUI()
    {
        base.OnInitUI();
        completeBtn.onClick.AddListener(() =>
        {
            if (flyPointCount <= 0)
            {
                UISystem.Instance.HideUI(this);
                UI_Manager.Instance.InvokeRefreshEvent("", "page_Play_OpenBranchRedeem");
            }
        });
        closeBtn.onClick.AddListener(() =>
        {
            if (flyPointCount <= 0)
                UISystem.Instance.HideUI(this);
        });
    }
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        curLevelType = ((UIPanelData_BranchTask)UIPanelData).levelType;
        RefreshTaskItem();
    }
    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
        foreach (var item in saveTaskDic)
        {
            AssetSystem.Instance.DestoryGameObject(Consts.Item_Task, item.Value.gameObject);
        }
        saveTaskDic.Clear();
    }

    private Dictionary<string, Item_Task> saveTaskDic = new Dictionary<string, Item_Task>();
    private void RefreshTaskItem()
    {
        List<string> tasks = new List<string>();
        if (MergeLevelManager.Instance.IsBranch(curLevelType))
        {
            tasks = BranchSystem.Instance.CurTaskList;
        }
        else
        {
            tasks = FestivalSystem.Instance.ReturnCurTaskListByType(curLevelType);
        }
        for (int i = 0; i < tasks.Count; i++)
        {
            if (!saveTaskDic.ContainsKey(tasks[i]))
            {
                GameObject gO = AssetSystem.Instance.Instantiate(Consts.Item_Task, itemGrid.transform);
                if (gO && gO.TryGetComponent(out Item_Task item))
                {
                    item.InitBranchNeedItems(curLevelType, tasks[i], this);
                    saveTaskDic.Add(tasks[i], item);
                }
            }
        }
        int setIndex = 0;
        foreach (var item in saveTaskDic)
        {
            item.Value.RefreshBranchNeedItems();
            if (item.Value.IsCompleteTask())
            {
                item.Value.transform.SetSiblingIndex(setIndex);
                setIndex++;
            }
        }
        foreach (var item in saveTaskDic)
        {
            if (item.Value.IsCompletePart()) 
            {
                item.Value.transform.SetSiblingIndex(setIndex);
                setIndex++;
            }
        }
        RefreshUI();
    }
    private void RefreshUI()
    {
        t_Title.text = ScriptLocalization.Get("Obj/Main/Tasks/Title");
        t_curScoreText.text = ScriptLocalization.Get("Obj/SpurLine/SpurLine1Button1");
        canClaimTag.SetActive(saveTaskDic.Count == 0);
        if (MergeLevelManager.Instance.IsBranch(curLevelType))
        {
            if (BranchSystem.Instance.GetCanClaimRewardCount() > 0)
            {
                completeText.text = ScriptLocalization.Get("Obj/SpurLine/SpurLine1Button3");
            }
            else
            {
                completeText.text = ScriptLocalization.Get("Obj/SpurLine/SpurLine1Button4");
            }                    
            t_curScore.text = BranchSystem.Instance.branchPoint.ToString();           
            redPoint.SetActive(BranchSystem.Instance.GetCanClaimRewardCount() > 0);
            RefreshSlider();
        }
        else
        {
            int canClaimPoints = FestivalSystem.Instance.GetCanClaimRewardCount(curLevelType);
            if (canClaimPoints > 0)
            {
                completeText.text = ScriptLocalization.Get("Obj/SpurLine/SpurLine1Button3");
            }
            else
            {
                completeText.text = ScriptLocalization.Get("Obj/SpurLine/SpurLine1Button4");
            }
            t_curScore.text = FestivalSystem.Instance.ReturnCurScore(curLevelType).ToString();
            redPoint.SetActive(canClaimPoints > 0);
            RefreshSlider();
        }
       
    }

    private void RefreshSlider(bool tween = false)
    {
        int branchPoint;
        Vector2Int vec2;
        if (MergeLevelManager.Instance.IsBranch(curLevelType))
        {
            branchPoint = BranchSystem.Instance.branchPoint - flyPointCount;
            vec2 = BranchSystem.Instance.GetPointProgress(branchPoint);
        }
        else
        {
            branchPoint = FestivalSystem.Instance.ReturnCurScore(curLevelType) - flyPointCount;
            vec2 = FestivalSystem.Instance.GetPointProgress(curLevelType, branchPoint);
        }
        t_Slider.text = vec2.x.ToString() + "/" + vec2.y.ToString();
        var val = (float)vec2.x / vec2.y;
        if (tween)
            DOTween.To(() => slider.value, x => slider.value = x, val, 0.2f);
        else
            slider.value = val;
    }
    /// <summary>
    /// 支线任务完成时刷新订单界面
    /// </summary>
    /// <param name="data"></param>
    public void RefreshTaskView(MergeLevelType levelType, string taskId, Vector3 startWorldPos)
    {
        if (BranchDefinition.allBranchdefDic.TryGetValue(curLevelType, out Dictionary<string, BranchDefinition> defDic)) 
        {
            if (defDic.TryGetValue(taskId, out BranchDefinition branchDefinition))
            {
                //发放奖励
                GameManager.Instance.GiveRewardItem(branchDefinition.rewardItemList, "CompleteBranck", Vector3.zero);
                //刷新数据
                if (MergeLevelManager.Instance.IsBranch(levelType))
                {
                    BranchSystem.Instance.CompleteTask(taskId);
                }
                else 
                {
                    FestivalSystem.Instance.CompleteTaskById(levelType, taskId);
                }
                saveTaskDic.Remove(taskId);
                //删除合成界面完成任务所需要的item
#if UNITY_EDITOR
                if (DebugSetting.CanUseDebugConfig(out SO_DebugConfig sO_Debug))
                {
                    if (!sO_Debug.CompleteTaskWithoutItem)
                    {
                        MergeLevelManager.Instance.RemoveDateFromMap(MergeLevelManager.Instance.CurrentLevelType, branchDefinition.needItemList, true);
                    }
                }
#else
            MergeLevelManager.Instance.RemoveDateFromMap(MergeLevelManager.Instance.CurrentLevelType, branchDefinition.needItemList, true);
#endif
                //刷新合成界面订单ui
                MergeController.CurrentController.DestroyTaskItemById(taskId);
                MergeController.CurrentController.RefreshMergeItemByCompleteTask();
                //播放奖励积分动画
                var pointCount = 0;
                foreach (var reward in branchDefinition.rewardItemList)
                {
                    if (reward.IsRewardBranchPoint)
                        pointCount += reward.num;
                }
                if (pointCount > 0)
                    PlayTweenPointsFly(pointCount, startWorldPos);
                //刷新item显示
                RefreshTaskItem();
            }
        }       
    }


    private int flyPointCount = 0;
    /// <summary>
    /// 订单点击完成时播放积分动画
    /// </summary>
    public void PlayTweenPointsFly(int pointCount, Vector3 tweenFromWorldPos)
    {
        flyPointCount += pointCount;
        Vector3 localPosition = slider.handleRect.transform.InverseTransformPoint(tweenFromWorldPos);
        float delay = 0f;
        int i = 0;
        for (; i < pointCount; i++)
        {
            GameObject fly = AssetSystem.Instance.Instantiate(Consts.Icon_Reward_Points, slider.handleRect.transform, localPosition, Vector3.zero, Vector3.zero);

            var tempDelay = delay;
            var scalePos = localPosition + new Vector3(UnityEngine.Random.Range(-60, 60), UnityEngine.Random.Range(-60, 60), 0);
            delay += UnityEngine.Random.Range(1, 3) * 0.1f;
            DOTween.Sequence().AppendInterval(tempDelay)
                .Append(fly.transform.DOScale(Vector3.one * 0.7f, 0.3f).SetEase(Ease.OutBack))
                .Insert(tempDelay, fly.transform.DOLocalMove(scalePos, 0.3f))
                .Append(fly.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InOutSine))
                .AppendCallback(() =>
                {
                    flyPointCount -= 1;
                    RefreshSlider(true);
                    AssetSystem.Instance.DestoryGameObject("", fly);
                });
        }
    }
}
