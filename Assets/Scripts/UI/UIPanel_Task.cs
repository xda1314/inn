using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using I2.Loc;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using IvyCore;
using Ivy;
using System.Linq;
/// <summary>
/// 任务界面
/// </summary>
public class UIPanel_Task : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI text_Title;
    [SerializeField] private TextMeshProUGUI text_CompleteAllTask;
    [SerializeField] private VerticalLayoutGroup itemGrid;
    [SerializeField] private GameObject completeTaskTag;
    [SerializeField] private HorizontalLayoutGroup rewardGrid;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI t_Slider;
    [SerializeField] private Button closeBtn;

    [SerializeField] private Button notCompleteBtn;
    [SerializeField] private TextMeshProUGUI notCompleteText;
    [SerializeField] private Button completeBtn;
    [SerializeField] private TextMeshProUGUI completeText;
    [SerializeField] private GameObject doubleRewardRoot;
    [SerializeField] private Button doubleCompleteBtn_C;
    [SerializeField] private TextMeshProUGUI doubleCompleteText_C;
    [SerializeField] private Button doubleCompleteBtn_N;
    [SerializeField] private TextMeshProUGUI doubleCompleteText_N;
    [SerializeField] private Button singleCompleteBtn;
    [SerializeField] private TextMeshProUGUI singleCompleteText;

    List<MergeRewardItem> rewardList = new List<MergeRewardItem>();
    private float curCompletetaskNum = 0;//刷新之后当前完成订单数
    private float oldCompleteTaskNum = 0;//刷新之前已完成订单数
    private int maxTaskNum = 0;//最大订单数
    public override void OnInitUI()
    {
        base.OnInitUI();
        completeBtn.onClick.AddListener(() =>
        {
            AdManager.TryPlayInterstitialAD(AdManager.ADTag.finish_level, RemoteConfigSystem.remoteKey_si_finish_level);
            AllTaskCompleteBtnClick();
        });
        singleCompleteBtn.onClick.AddListener(() =>
        {
            AdManager.TryPlayInterstitialAD(AdManager.ADTag.finish_level, RemoteConfigSystem.remoteKey_si_finish_level);
            AllTaskCompleteBtnClick();
        });
        doubleCompleteBtn_C.onClick.AddListener(() =>
        {
            DoubleRewardBtnClick();
        });
        closeBtn.onClick.AddListener(CloseBtnClick);
        RefreshFinalReward();
    }
    public override IEnumerator OnShowUI()
    {
        SetupLanguageText();
        RefreshTaskItem();
        RefreshBtnActive();
        if (TaskGoalsDefinition.taskLevelDataDic.TryGetValue(TaskGoalsManager.Instance.curLevelIndex, out TaskLevelData checkpointData))
        {
            maxTaskNum = checkpointData.maxTaskNum;
        }
        curCompletetaskNum = oldCompleteTaskNum = maxTaskNum - TaskGoalsManager.Instance.leftTaskIdList.Count;
        slider.value = oldCompleteTaskNum / maxTaskNum;
        t_Slider.text = curCompletetaskNum.ToString() + "/" + maxTaskNum.ToString();
        //tutor
        //GameDebug.Log("完成的订单数：" + curCompletetaskNum);
        yield return base.OnShowUI();
        yield return new WaitForSeconds(0.5f);
        if (!SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnTask))
        {
            try
            {
                if (itemGrid.transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.activeSelf)
                {
                    GameDebug.Log("执行inntask教学");
                    UI_TutorManager.Instance.RunTutorWithName("InnTask");
                    SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnTask, true);
                }
            }
            catch (Exception e)
            {

            }
        }

        if (!SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnEndLevel) && TaskGoalsManager.Instance.leftTaskIdList.Count == 0)
        {
            GameDebug.Log("执行InnEndLevel教学");
            UI_TutorManager.Instance.RunTutorWithName("InnEndLevel");
            SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnEndLevel, true);
        }
    }

    public void SetupLanguageText()
    {
        text_Title.text = ScriptLocalization.Get("Obj/Chain/Game_Orders");
        text_CompleteAllTask.text = ScriptLocalization.Get("Obj/Chain/Orders_Button1");
        completeText.text = singleCompleteText.text = doubleCompleteText_C.text = notCompleteText.text = string.Format(ScriptLocalization.Get("Obj/Chain/Orders_Button2"), TaskGoalsManager.Instance.curLevelIndex);
    }
    private void Update()
    {
        if (curCompletetaskNum > oldCompleteTaskNum)
        {
            oldCompleteTaskNum += Time.deltaTime * 2;
            slider.value = oldCompleteTaskNum / maxTaskNum;
            if (oldCompleteTaskNum >= maxTaskNum)
            {
                //刷新按钮显示
                RefreshBtnActive();

            }
        }
    }
    private void RefreshBtnActive()
    {
        if (TaskGoalsManager.Instance.leftTaskIdList.Count == 0)
        {
            notCompleteBtn.gameObject.SetActive(false);
            if (TaskGoalsManager.Instance.curLevelIndex >= 3)
            {
                //第三关之后弹广告播放，双倍奖励
                completeBtn.gameObject.SetActive(false);
                doubleRewardRoot.SetActive(true);
                // 广告刷新时间
                RefreshBtnState(AdManager.CanShowAD_Normal());
            }
            else
            {
                completeBtn.gameObject.SetActive(true);
                doubleRewardRoot.SetActive(false);
            }
        }
        else
        {
            notCompleteBtn.gameObject.SetActive(true);
            completeBtn.gameObject.SetActive(false);
            doubleRewardRoot.SetActive(false);
        }
    }
    private void RefreshBtnState(bool canWatchAD)
    {
        doubleCompleteBtn_C.gameObject.SetActive(canWatchAD);
        doubleCompleteBtn_N.gameObject.SetActive(!canWatchAD);
        if (canWatchAD)
        {
            doubleCompleteText_C.text = string.Format(ScriptLocalization.Get("Obj/Chain/Orders_Button2"), TaskGoalsManager.Instance.curLevelIndex);
        }
        else
        {
            doubleCompleteText_N.text = ScriptLocalization.Get("Obj/Chain/WatchVideo_Text5");
        }
    }

    private List<GameObject> saveRewardList = new List<GameObject>();
    /// <summary>
    /// 刷新完成关卡任务总奖励
    /// </summary>
    private void RefreshFinalReward()
    {
        for (int i = 0; i < saveRewardList.Count; i++)
        {
            Destroy(saveRewardList[i]);
        }
        saveRewardList.Clear();

        if (TaskGoalsDefinition.taskLevelDataDic.TryGetValue(TaskGoalsManager.Instance.curLevelIndex, out TaskLevelData checkpointData))
        {
            if (TaskGoalsDefinition.TaskDefinitionsDict.TryGetValue(checkpointData.finalTaskId, out TaskData taskData))
            {
                rewardList = taskData.taskDefinition.rewardItemList;
                for (int i = 0; i < rewardList.Count; i++)
                {
                    GameObject go = AssetSystem.Instance.Instantiate(rewardList[i].name, rewardGrid.transform);
                    saveRewardList.Add(go);
                }
            }
        }
    }

    private Dictionary<string, Item_Task> saveTaskDic = new Dictionary<string, Item_Task>();
    private void RefreshTaskItem()
    {
        List<string> tasks = TaskGoalsManager.Instance.ReturnSortTask();
        List<string> keyList = saveTaskDic.Keys.ToList();
        for (int i = 0; i < keyList.Count; i++)
        {
            if (!tasks.Contains(keyList[i])) 
            {
                AssetSystem.Instance.DestoryGameObject(keyList[i], saveTaskDic[keyList[i]].gameObject);
                saveTaskDic.Remove(keyList[i]);
            }
        }

        for (int i = 0; i < tasks.Count; i++)
        {
            if (!saveTaskDic.ContainsKey(tasks[i]))
            {
                GameObject gO = AssetSystem.Instance.Instantiate(Consts.Item_Task, itemGrid.transform);
                if (gO && gO.TryGetComponent(out Item_Task item))
                {
                    item.InitMainLineNeedItems(tasks[i], this);
                    saveTaskDic.Add(tasks[i], item);
                }
            }
        }
        int setIndex = 0;
        foreach (var item in saveTaskDic)
        {
            item.Value.RefreshMainLineNeedItems();
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
        completeTaskTag.SetActive(tasks.Count == 0);
    }
    /// <summary>
    /// 订单完成时刷新订单界面
    /// </summary>
    /// <param name="data"></param>
    public void RefreshTaskView(string taskId, Vector3 startWorldPos)
    {
        if (TaskGoalsDefinition.TaskDefinitionsDict.TryGetValue(taskId, out TaskData taskData))
        {
            AnalyticsUtil.TrackEvent("complete_orders", new Dictionary<string, string>() {
                { "label",taskData.taskDefinition.levelIndex.ToString()},
                 { "catalog",taskId},
            });
            //发放奖励
            GameManager.Instance.GiveRewardItem(taskData.taskDefinition.rewardItemList, "CompleteTask:" + taskId, Vector3.zero);
            //删除任务数据
            TaskGoalsManager.Instance.RemoveTaskfromList(taskId);
            saveTaskDic.Remove(taskId);
            //DailyTaskSystem.Instance.DailyTaskEvent_CompleteTask?.Invoke();
            GameManager.Instance.TryShowTopTopNoticeBar(TopNoticeType.StarRewardReady);
            MergeController.CurrentController.TryTweenTaskBack();
            //删除合成界面完成任务所需要的item
#if UNITY_EDITOR
            if (DebugSetting.CanUseDebugConfig(out SO_DebugConfig sO_Debug))
            {
                if (!sO_Debug.CompleteTaskWithoutItem)
                {
                    MergeLevelManager.Instance.RemoveDateFromMap(MergeLevelManager.Instance.CurrentLevelType, taskData.taskDefinition.needItemList, true);
                }
            }
#else
            MergeLevelManager.Instance.RemoveDateFromMap(MergeLevelManager.Instance.CurrentLevelType, taskData.taskDefinition.needItemList, true);
#endif            
            //刷新合成界面订单ui
            MergeController.CurrentController.DestroyTaskItemById(taskId);
            MergeController.CurrentController.RefreshMergeItemByCompleteTask();
            //播放奖励星星动画
            PlayTweenYellowStarFly(Consts.Icon_Reward_Stars, startWorldPos, slider.handleRect.position);
            //刷新item显示
            RefreshTaskItem();
            //刷新经验条
            GameManager.Instance.RefreshCurrency_Exp();
            UIPanel_TopBanner.refreshBanner?.Invoke();

            if (!SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnEndLevel) && TaskGoalsManager.Instance.leftTaskIdList.Count == 0)
            {
                GameDebug.Log("执行InnEndLevel教学");
                UI_TutorManager.Instance.RunTutorWithName("InnEndLevel");
                SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnEndLevel, true);
            }
        }
    }
    /// <summary>
    /// 完成关卡所有任务,发放奖励
    /// </summary>
    private void AllTaskCompleteBtnClick()
    {
        //AudioManager.Instance.StopBGM();
        //AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.normalBtnClick);
        //发放关卡总奖励
        AnalyticsUtil.TrackEvent("finish_level", new Dictionary<string, string>() {
            {"label",TaskGoalsManager.Instance.curLevelIndex.ToString() }
        });
        GameManager.Instance.GiveRewardItem(rewardList, "Complete:Level" + TaskGoalsManager.Instance.curLevelIndex.ToString(), MergeLevelManager.Instance.CurrentLevelType,false,()=> 
        {
            CheckGiftOrActivity();
        });
         UISystem.Instance.HideUI(this);
    }

    private void CheckGiftOrActivity() 
    {
        //如果完成一个章节，发放章节奖励
        TaskGoalsDefinition.TaskDefinitionsDict.TryGetValue(TaskGoalsManager.Instance.finalTaskId, out TaskData taskData);
        if (taskData != null && taskData.taskDefinition.ChapterRewardList.Count != 0)
        {
            GameManager.Instance.GiveRewardItem(taskData.taskDefinition.ChapterRewardList, "CompleteChapter", Vector3.zero);
            int curChapter = taskData.taskDefinition.levelIndex / 15;
            AnalyticsUtil.TrackEvent("phase_unlock_" + (curChapter + 1).ToString());
        }
        //刷新要推送的礼包数据
        GiftPackageManager.Instance.CompleteTaskGift(TaskGoalsManager.Instance.curLevelIndex);
        //切关
        TaskGoalsManager.Instance.ChangeToNextLevel();

        RefreshTaskItem();
        RefreshFinalReward();
        //2022/7/1 每次切换关卡更新任务的时候刷新一次
        ShopSystem.Instance.RefreshPersonalShopData();
        DailyActiveSystem.Instance.TryInitDailyActiveSystem();
        UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_CompleteLevel, UIShowLayer.TopPopup));
        //检测副本是否开启
        DungeonSystem.Instance.CheckDungeon();
        FestivalSystem.Instance.refreshFestival?.Invoke();
    }

    /// <summary>
    /// 双倍奖励
    /// </summary>
    public void DoubleRewardBtnClick()
    {
        //AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.normalBtnClick);

        Vector3 btnWorldPos = doubleCompleteBtn_C.transform.position;
        AdManager.PlayAd(btnWorldPos, AdManager.ADTag.TaskDoubleReward, () =>
         {
             GameManager.Instance.GiveRewardItemNoAni(rewardList, "Complete:Level" + TaskGoalsManager.Instance.curLevelIndex.ToString(), MergeLevelManager.Instance.CurrentLevelType);
             GameManager.Instance.GiveRewardItem(rewardList, "TaskDoubleReward", MergeLevelManager.Instance.CurrentLevelType, true,()=> 
             {
                 CheckGiftOrActivity();
             });
         }, "", () =>
         {
             //GameManager.Instance.ResetRewardList(rewardList);
             //GameManager.Instance.PlayAdFail();
         });
        UISystem.Instance.HideUI(this);
    }
    private void CloseBtnClick()
    {
        UISystem.Instance.HideUI(this);
        if (!SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnGetReward))
        {
            Invoke("DoTutorInnGetReward", 0.5f);
        }
    }

    private void DoTutorInnGetReward()
    {
        if (SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnTask))
        {
            if (!SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnGetReward))
            {
                GameDebug.Log("执行InnGetReward教学");
                UI_TutorManager.Instance.RunTutorWithName("InnGetReward");
                SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnGetReward, true);
            }
        }
    }
    /// <summary>
    /// 订单点击完成时播放黄色星星动画
    /// </summary>
    public void PlayTweenYellowStarFly(string prefabName, Vector3 tweenFromWorldPos, Vector3 tweenToWorldPos)
    {
        Vector3 localPosition = transform.InverseTransformPoint(tweenFromWorldPos);
        GameObject go = AssetSystem.Instance.Instantiate(prefabName, transform, localPosition, Vector3.zero, Vector3.one);

        Sequence sequence = DOTween.Sequence();
        float duration = 0.5f;
        Vector2 p0 = tweenFromWorldPos;
        Vector2 p2 = tweenToWorldPos; ;
        Vector2 p1 = new Vector2(Vector2.Lerp(p0, p2, 0.3f).x, p0.y + 0.5f);

        sequence.Append(DOTween.To(setter: value =>
        {
            Vector2 vector = DoAnimTools.Bezier(p0, p1, p2, value);
            go.transform.position = new Vector3(vector.x, vector.y, 0);
        }, startValue: 0, endValue: 1, duration: duration).SetEase(Ease.InQuad));

        sequence.onComplete = () =>
        {
            AssetSystem.Instance.DestoryGameObject(prefabName, go);
            curCompletetaskNum++;
            t_Slider.text = curCompletetaskNum.ToString() + "/" + maxTaskNum.ToString();
        };
    }
}
