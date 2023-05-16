using DG.Tweening;
using Ivy;
using IvyCore;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MergeController))]
public class UIPanel_Merge : UIMergeBase
{
    [Header("Tips")]
    [SerializeField] private Transform trans_ShowCompleteTask;
    [SerializeField] private Transform trans_ShowClearCD;
    [SerializeField] private CanvasGroup group_ShowCommonInfo;
    [SerializeField] public TextMeshProUGUI t_LeftNeedleNum;
    //任务弹窗
    [SerializeField] private Image taskHeadIcon;
    [SerializeField] private TextMeshProUGUI t_ShowTaskDesc;
    [SerializeField] private HorizontalLayoutGroup group_TaskReward;
    [SerializeField] private Button btn_CompleteTask;
    [SerializeField] private TextMeshProUGUI t_CompleteTask;
    //普通弹窗
    [SerializeField] private TextMeshProUGUI showTip_ItemName;
    [SerializeField] private TextMeshProUGUI showTip_ItemDescribe;
    [SerializeField] private Button btn_ItemTips;
    [SerializeField] private GameObject btnLayout;
    [SerializeField] private GameObject SwallowParent;
    [SerializeField] private GameObject SwallowLayout;
    [SerializeField] private Transform select_Prefab;
    [SerializeField] private TextMeshProUGUI text_put;
    [SerializeField] private TextMeshProUGUI text_produce;
    [SerializeField] private ShowItemInfo showItem;
    [SerializeField] private GameObject[] showItemStar;
    [SerializeField] private GameObject[] showItemBox;
    [SerializeField] private GameObject stars;
    [SerializeField] private GameObject NormatStarBg;
    [SerializeField] private Button btn_MergeTutorial;
    [Header("Top Task")]
    //主线
    [SerializeField] private GameObject taskViewRoot;
    [SerializeField] private GameObject completeAllTaskRoot;//完成某一关全部任务
    [SerializeField] private GameObject completeAllChapterRoot;//完成全部章节
    [SerializeField] private Button btn_OpenTask;
    [SerializeField] private TextMeshProUGUI t_CompleteAllTask;
    [SerializeField] private TextMeshProUGUI t_CompleteAllChapter;
    [SerializeField] private HorizontalLayoutGroup grid_Task;
    [SerializeField] private TextMeshProUGUI t_LeftTaskNum;
    //支线
    [SerializeField] private GameObject completeAllBranchTask;
    [SerializeField] private Button btn_OpenBranchClaimView;
    [SerializeField] private TextMeshProUGUI t_CompleteAllBranchTask;
    //副本
    [SerializeField] private Transform targetItemParent;
    [Header("TipsOperateButton")]
    //解锁按钮（泡泡、冰块）
    [SerializeField] private Button btn_Unlock;
    [SerializeField] private TextMeshProUGUI text_Des_Unlock;
    [SerializeField] private TextMeshProUGUI text_Cost_Unlock;
    //针按钮
    [SerializeField] private Button btn_Needle;
    [SerializeField] private GameObject needleEnough;
    [SerializeField] private TextMeshProUGUI text_NeedleNum;
    [SerializeField] private GameObject hasNeedleAd;
    [SerializeField] private TextMeshProUGUI t_HasNeedleAd;
    [SerializeField] private GameObject noNeedleAd;
    [SerializeField] private TextMeshProUGUI t_NoNeedleAd;
    //出售按钮
    [SerializeField] private Button btn_Sell;
    [SerializeField] private TextMeshProUGUI text_Des_Sell;
    [SerializeField] private TextMeshProUGUI text_Cost_Sell;
    //撤销出售按钮
    [SerializeField] private Button btn_RevokeSell;
    [SerializeField] private TextMeshProUGUI text_Des_RevokeSell;
    [SerializeField] private TextMeshProUGUI text_Cost_RevokeSell;
    //开箱按钮
    [SerializeField] private Button btn_OpenBox;
    [SerializeField] private TextMeshProUGUI text_Des_OpenBox;
    //清除CD按钮
    [SerializeField] private TextMeshProUGUI t_ShowClearCDDesc;
    [SerializeField] private Button btn_ClearCDbyGem;//钻石加速   
    [SerializeField] private TextMeshProUGUI t_ClearCDbyGem;
    [SerializeField] private TextMeshProUGUI t_ClearCDCost;
    [SerializeField] private GameObject go_countDown;
    [SerializeField] private TextMeshProUGUI text_count;
    [SerializeField] private TextMeshProUGUI text_Free_ClearCD;
    [SerializeField] private Button btn_ClearCDbyAD;//广告加速
    [SerializeField] private TextMeshProUGUI t_ClearCDbyAD;
    [SerializeField] private Button btn_ClearCDbyAD_Not;
    [SerializeField] private TextMeshProUGUI t_ClearCDbyAD_Not;

    [Header("BubbleRoot")]
    [SerializeField] private Transform bubbleZoneTop;
    [SerializeField] private Transform bubbleZoneBottom;

    [Header("BottomButton")]
    [SerializeField] private Button btn_Close;
    [SerializeField] private Button btn_OpenStore;
    [SerializeField] private HorizontalLayoutGroup grid_Store;
    [SerializeField] private Button btn_openShop;
    [SerializeField] private CanvasGroup selectBox;//选中框物体
    [SerializeField] public Button btn_spawn;
    [SerializeField] private Image bgImage;
    [SerializeField] private GameObject storeBg;
    [SerializeField] private GameObject CountDown;
    [SerializeField] private TextMeshProUGUI txtDailyTime;
    [SerializeField] private TextMeshProUGUI txtDes;
    [LabelText("桌子背景列表"), BoxGroup("UI关联"), SerializeField]
    private List<GameObject> TableBgObjList;
    [LabelText("任务背景列表"), BoxGroup("UI关联"), SerializeField]
    private List<GameObject> TaskBgObjList;
    [LabelText("地图背景列表"), BoxGroup("UI关联"), SerializeField]
    private List<GameObject> MapBgObjList;

    public Transform DungeonLoveTrans { get; private set; } = null;
    public Transform BranchPointTrans { get; private set; } = null;

    public static event Action onEnterAndRefreshRedPointState; //刷新图鉴红点
    private int adReduceTime = 900;//广告加速减少的时间
    private bool isTutorialFreeClearCD = false;
    public static int BubbleZoneMaxY = 1000;
    public static int BubbleZoneMinY = -1000;
    public static Action refreshTopAction;
    private int ClearCDGemCost = 0;//2022-09-19hxc,钻石清除CD优化，根据时间长短决定消耗宝石的数量

    [SerializeField] private RectTransform TopTaskRoot;
    [SerializeField] private RectTransform TableBgs;
    [SerializeField] private Transform taskBGs;
    [SerializeField] private RectTransform vertical_layout;
    [SerializeField] private HorizontalLayoutGroup horizontal;
//#if UNITY_EDITOR
//    [Range(1, 2)] public float scale = 1;
//    [Range(-50, 300)] public float taskRootOffset = 0;
//    private void FixedUpdate()
//    {
//        taskRootOffset = GameManager.Instance.MNotchAdaptY_ / 2;
//        if (taskRootOffset <= 300 && taskRootOffset > -50 && taskRootOffset != 0)
//        {
//            TopTaskRoot.transform.localPosition = new Vector2(0, 773.4844F - taskRootOffset);
//        }
//        if (scale > 1 && scale < 2 && scale != 1)
//        {
//            float currentScale = taskBGs.localScale.x;
//            TableBgs.offsetMax = new Vector2(0, -(445 + (scale - 1) * 330));
//            vertical_layout.offsetMax = new Vector2(0, -(445 + (scale - 1) * 330));
//            taskBGs.localScale = new Vector2(scale, scale);
//        }
//    }
//#endif

    private void Start()
    {
        if (UISystem.Instance.uiMainMenu == null)
            return;
        RectTransform rect = UISystem.Instance.uiMainMenu.GetComponent<RectTransform>();

        float offset_y = TopTaskRoot.transform.localPosition.y;
        float taskRootOffset = GameManager.Instance.MNotchAdaptY_/2;
        float extendScale = (rect.rect.height - 2160) / 2160F;
        if (rect != null && extendScale!=0 || taskRootOffset!=0)
        {
            TableBgs.offsetMax = new Vector2(0, -(445 + extendScale * 330+ taskRootOffset));
            vertical_layout.offsetMax = new Vector2(0, -(445 + extendScale * 330 + taskRootOffset*1.5f));
            float verticalScale = 1 + extendScale - (taskRootOffset * 1.5f) / 2160;
            verticalScale = verticalScale > 1 ? 1 : verticalScale;
            vertical_layout.localScale = new Vector2(verticalScale, verticalScale);
            taskBGs.localScale = new Vector2(1 + extendScale, 1 + extendScale);
            TopTaskRoot.transform.localPosition = new Vector2(0, offset_y - 300 * extendScale- taskRootOffset);
        }
        refreshTopAction += DoAllTaskRootAni;
    }

    private void OnDestroy()
    {
        refreshTopAction -= DoAllTaskRootAni;
    }

    public override void OnInitUI()
    {
        base.OnInitUI();
        ButtonAddListener();
        MergeActionSystem.GameActionEvent_MapDataChange += () =>
        {
            Invoke("MBCI", 0.2f);
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.put_down);
        };
        MergeActionSystem.GameActionEvent_AddOrRemoveItem += () =>
        {
            RefreshTaskItem();
            RefreshCompleteTask();
        };
        InitTweenPos();
        CheckBubbleZone();
    }
    void MBCI() //用于延时调用
    {
        mergeController.RefreshMergeItemByChangeItem();
    }

    private void ButtonAddListener()
    {
        btn_Close.onClick.AddListener(() => OnBtnCloseClick());
        btn_openShop.onClick.AddListener(() => OpenInternalShop());
        btn_ItemTips.onClick.AddListener(ShowInfoByBtnClick);
        btn_OpenStore.onClick.AddListener(() => F_OpenStoreViewClick());
        btn_spawn.onClick.AddListener(TryMoveItemToMapByClick);
        btn_OpenTask.onClick.AddListener(OpenTaskBtnClick);
        btn_CompleteTask.onClick.AddListener(CompleteBtnClick);
        btn_OpenBranchClaimView.onClick.AddListener(() =>
        {
            if (UISystem.Instance.TryGetUI<UIPanel_Merge>(Consts.UIPanel_Merge, out var uiMerge))
                UISystem.Instance.HideUI(uiMerge);
            //UI_Manager.Instance.InvokeRefreshEvent("", "page_Play_OpenBranchRedeem");
        });

        //tips操作按钮
        btn_Unlock.onClick.AddListener(() =>
        {
            if (mergeController.currentSelectItemData == null || mergeController.currentSelectItemData.Definition == null)
                return;

            //花费钻石解锁泡泡和被蜘蛛网覆盖的物体
            if (mergeController.currentSelectItemData.IsInBubble || mergeController.currentSelectItemData.IsLocked)
            {
                int cost = 0;
                if (mergeController.currentSelectItemData.IsInBubble)
                    cost = mergeController.currentSelectItemData.Definition.BubbleUnlockCost;
                else
                    cost = mergeController.currentSelectItemData.Definition.UnLockCost;
                if (!Currencies.CanAffordOrTip(CurrencyID.Gems, cost))
                {
                    Currencies.NotEnoughCoinsOrGems(CurrencyID.Gems);
                    AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
                    return;
                }
                Currencies.Spend(CurrencyID.Gems, cost, "unlock");

                if (mergeController.currentSelectItemData.IsInBubble)
                {
                    mergeController.currentSelectItemData.SetData_OpenBubble();
                    AchievementManager.Instance.UpdateAchievement(AchievementType.unlockBubble, 1);
                }
                else if (mergeController.currentSelectItemData.IsLocked)
                {
                    AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Break_Cobweb);
                    mergeController.currentSelectItemData.SetData_OpenPack();
                    ShowNewDiscoveryView(mergeController.currentSelectItemData.PrefabName);
                    //蛛网解锁时解锁周围箱子
                    mergeController.OpenNearBox(mergeController.currentSelectItemData.GridPos);
                    AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Break_Box);
                }
                MergeLevelManager.Instance.CurrentMapData.AddOrRemoveItemData();
                //刷新
                RefreshSelectItemInfo();
            }
        });
        btn_Needle.onClick.AddListener(() =>
        {
            if (mergeController == null || mergeController.currentSelectItemData == null || !mergeController.currentSelectItemData.IsInBubble)
                return;

            int cost = (int)Math.Ceiling((float)mergeController.currentSelectItemData.Definition.BubbleUnlockCost / 5);
            cost = cost > 0 ? cost : 1;

            if (!Currencies.CanAfford(CurrencyID.Needle, cost))
            {

                if (needleState == NeedleState.hasNeedleAd)
                {
                    AdManager.PlayAd(btn_Needle.transform.position, AdManager.ADTag.dailydeals_needle, () =>
                    {
                        List<MergeRewardItem> rewardList = new List<MergeRewardItem>();
                        MergeRewardItem item = new MergeRewardItem();
                        item.name = "Needle";
                        item.num = 1;
                        rewardList.Add(item);
                        GameManager.Instance.GiveRewardItem(rewardList, "watchAd", btn_Needle.transform.position, MergeLevelManager.Instance.CurrentLevelType);
                    }, "SaveAD_Daily", () =>
                    {
                        List<MergeRewardItem> rewardList = new List<MergeRewardItem>();
                        MergeRewardItem item = new MergeRewardItem();
                        item.name = "Needle";
                        item.num = 1;
                        rewardList.Add(item);
                        GameManager.Instance.PlayAdFail(rewardList, AdManager.ADTag.dailydeals_needle);
                    });
                }
                else if (needleState == NeedleState.noNeedleAd)
                {

                }
                return;
            }
            if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine)
            {
                Currencies.Spend(CurrencyID.Needle, cost, "Lv" + TaskGoalsManager.Instance.curLevelIndex);
            }
            else
            {
                Currencies.Spend(CurrencyID.Needle, cost, MergeLevelManager.Instance.CurrentLevelType.ToString());
            }
            if (mergeController.currentSelectItemData.IsInBubble)
            {
                mergeController.currentSelectItemData.SetData_OpenBubble();
                AchievementManager.Instance.UpdateAchievement(AchievementType.unlockBubble, 1);
            }
            MergeLevelManager.Instance.CurrentMapData.AddOrRemoveItemData();
            RefreshSelectItemInfo();
            SetNeedleNum();
        });
        btn_Sell.onClick.AddListener(() =>
        {
            if (mergeController == null || mergeController.currentSelectItemData == null)
                return;

            if (mergeController.currentSelectItemData.Definition == null)
            {
                MergeLevelManager.Instance.CurrentMapData.ChangeItemData(mergeController.currentSelectItemData.GridPos, null);
                MergeLevelManager.Instance.CurrentMapData.AddOrRemoveItemData();
                return;
            }

            if (mergeController.currentSelectItemData.ItemGO.NeedFixSellView()) //任务或每日任务目标道具
            {
                AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
                UISystem.Instance.ShowUI(new UIPanelData_FixSell(mergeController.currentSelectItemData.Definition.PrefabName));
            }
            else if (mergeController.currentSelectItemData.Definition.RarityType == ItemRarityType.Simple)
            {
                AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.unlockSellReturnBtn);
                FixSellItem();
            }
            else
            {
                AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
                UISystem.Instance.ShowUI(new UIPanelData_FixSell(mergeController.currentSelectItemData.Definition.PrefabName));
            }
        });

        btn_RevokeSell.onClick.AddListener(() =>
        {
            //返回钱
            if (lastSellItemData == null)
                return;

            if (lastSellItemData.Definition == null)
            {
                lastSellItemData = null;
                return;
            }

            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.unlockSellReturnBtn);
            int cost = lastSellItemData.Definition.SellValue;
            Currencies.Spend(CurrencyID.Coins, cost, "revoke");
            MergeItem temp = mergeController.CreateItem_WithData(lastSellItemData);
            MergeLevelManager.Instance.CurrentMapData.ChangeItemData(lastSellItemData.GridPos, lastSellItemData);
            MergeLevelManager.Instance.CurrentMapData.AddOrRemoveItemData();

            mergeController.SetCurrentSelectData(lastSellItemData);
        });

        btn_ClearCDbyGem.onClick.AddListener(() =>
        {
            if (mergeController.currentSelectItemData == null || mergeController.currentSelectItemData.Definition == null)
                return;

            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            int cost = ClearCDGemCost;
            if (isTutorialFreeClearCD)
            {
                isTutorialFreeClearCD = false;
                cost = 0;
            }
            if (cost <= Currencies.Gems)
            {
                if (cost > 0)
                    Currencies.Spend(CurrencyID.Gems, cost, "unlock");
                mergeController.currentSelectItemData.ClearCD();
                AchievementManager.Instance.UpdateAchievement(AchievementType.speedCharge, 1);
                RefreshSelectItemInfo();
            }
            else
                Currencies.NotEnoughCoinsOrGems(CurrencyID.Gems);
        });
        btn_ClearCDbyAD.onClick.AddListener(() =>
        {
            if (mergeController.currentSelectItemData == null || mergeController.currentSelectItemData.Definition == null)
                return;
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            AdManager.PlayAd(btn_ClearCDbyAD.transform.position, AdManager.ADTag.ClearItemCD, () =>
            {
#if !UNITY_EDITOR
                adReduceTime = RemoteConfigSystem.Instance.GetRemoteConfig_Int(RemoteConfigSystem.remoteKey_LoopCD_ReduceTime);
#endif
                if (adReduceTime <= 0)
                    adReduceTime = 900;
                mergeController.currentSelectItemData.ClearCD(adReduceTime, true, true);
                AchievementManager.Instance.UpdateAchievement(AchievementType.speedCharge, 1);
                RefreshSelectItemInfo();
            }, "", () =>
            {

            });
        });

        btn_OpenBox.onClick.AddListener(() =>
        {
            if (mergeController.currentSelectItemData == null || mergeController.currentSelectItemData.Definition == null)
                return;
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Break_Box);

            mergeController.currentSelectItemData.StartOpenDelay();
            RefreshSelectItemInfo();
            btn_OpenBox.gameObject.SetActive(false);
        });
        btn_MergeTutorial.onClick.AddListener(() =>
        {
            UISystem.Instance.ShowUI(new UIPanelData_Tutorial(TutorialType.merge));
        });
    }

    public override IEnumerator OnShowUI()
    {
        yield return UISystem.Instance.uiPanel_TransitionAnimation.TransitionAnimationDown();
        if (AdManager.ManualEnterMergePanel)
        {
            AdManager.ManualEnterMergePanel = false;
            AdManager.TryPlayInterstitialAD(AdManager.ADTag.enter_level, RemoteConfigSystem.remoteKey_si_enter_level);
        }
        yield return base.OnShowUI();

        SetMergeBg();
        int index = (int)MergeLevelManager.Instance.CurrentLevelType - 1;
        mergeController.InitMapInfo(this, TableBgObjList[index], TaskBgObjList[index], MapBgObjList[index]);
        lastSellItemData = null;
        mergeController.SetCurrentSelectData();

        RefreshUI();
        RefreshRewardBox();
        InitCreateTaskItem();
        SetNeedleNum();
        RefreshBottomStore();
        yield return UISystem.Instance.uiPanel_TransitionAnimation.TransitionAnimationUp();
        UIPanel_TransitionAnimation.needTween = true;
        btn_MergeTutorial.gameObject.SetActive(TaskGoalsManager.Instance.curLevelIndex < 5);
        //AudioManager.Instance.PlayInMergeBGM(true);

        yield return null;

        MergeLevelType mergeLevel = MergeLevelManager.Instance.CurrentLevelType;
        MergeLevelManager.Instance.TryNewDiscoveryItemByEnterNewDungeon();
        storeBg.gameObject.SetActive(mergeLevel == MergeLevelType.mainLine);
        if (MergeLevelManager.Instance.IsDailyActive(mergeLevel)) 
        {
            CountDown.gameObject.SetActive(true);
            var leftSeconds = (int)(TimeManager.Instance.GetTomorrowRefreshTimeSpan()).TotalSeconds;
            txtDailyTime.text =MyTimer.ReturnTextUntilSecond_MaxShowTwo(leftSeconds);
            txtDes.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/BatterpassText2");
        }
        else 
        {
            CountDown.gameObject.SetActive(false);
        }
    }

    private void SetMergeBg()
    {
        var levelType = MergeLevelManager.Instance.CurrentLevelType;
        if (TableBgObjList != null && TaskBgObjList != null && MapBgObjList != null
            && TableBgObjList.Count == TaskBgObjList.Count && TableBgObjList.Count == MapBgObjList.Count)
        {
            int _type = (int)levelType;
            for (int index = 0; index < TableBgObjList.Count; index++)
            {
                TableBgObjList[index].SetActive(index + 1 == _type);
                TaskBgObjList[index].SetActive(index + 1 == _type);
                MapBgObjList[index].SetActive(index + 1 == _type);
            }
        }
        else
            GameDebug.LogError("MergeUI背景配置错误!");
    }

    private void RefreshUI()
    {
        btn_OpenStore.gameObject.SetActive(MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine);
        for (int i = 0; i < showItemBox.Length; i++)
        {
            if (showItemBox[i].activeSelf)
            {
                showItemBox[i].SetActive(false);
            }
        }
        showItemBox[0].SetActive(true);
        stars.SetActive(false);
        NormatStarBg.SetActive(false);
    }

    public override IEnumerator OnHideUI()
    {
        yield return UISystem.Instance.uiPanel_TransitionAnimation.TransitionAnimationDown();
        if (AdManager.ManualExitMergePanel)
        {
            AdManager.ManualExitMergePanel = false;
            AdManager.TryPlayInterstitialAD(AdManager.ADTag.exit_level, RemoteConfigSystem.remoteKey_si_exit_level);
        }
        yield return base.OnHideUI();
        //InitTweenPos();
        RefreshShowCD("0", false);
        mergeController.CleanMap();
        MergeLevelManager.Instance.CloseMapData();
        // 上传排行榜
        if (GameManager.Instance.playerData.Experience > 0)
            RiseSdk.Instance.UpdateLeaderboard("world_rank", GameManager.Instance.playerData.Experience);

        onEnterAndRefreshRedPointState?.Invoke();
        lastSellItemData = null;
        mergeController.StopTween_BoxSelected();
        //AudioManager.Instance.PlayInMergeBGM(false);
        DestroyAllTaskItem();
        yield return UISystem.Instance.uiPanel_TransitionAnimation.TransitionAnimationUp();
        UISystem.Instance.uiMainMenu.MergeUIReturnMainmenu();
        ClearBottomStore();       
    }

    private DateTimeOffset BubbleDieTime = DateTimeOffset.MinValue;//泡泡死亡时间
    private DateTimeOffset ChargeOverTime = DateTimeOffset.MinValue;//充能结束时间
    private float bubbleDieIndexTime = 0;
    private float chargeIndexTime = 0;
    private float updateTimeInterval = 0;

    private bool is_show_text = false;
    private void RefreshCountDown()
    {
        var operateData = mergeController.currentSelectItemData;
        if (operateData != null)
        {
            if (MergeItemCategoryType.finiteContainer == operateData.Definition.CategoryType || MergeItemCategoryType.taskBox == operateData.Definition.CategoryType)
            {
                if (operateData.boxOpenDelayEndTime == DateTimeOffset.MaxValue)
                {
                    go_countDown.SetActive(false);
                    is_show_text = false;
                    if (is_show_text)
                        RefreshSelectItemInfo();
                }
                else
                {
                    int countDown = (int)ExtensionTool.RemainTime(operateData.boxOpenDelayEndTime).TotalSeconds;
                    if (countDown > 0 && operateData.IsInCD())
                    {
                        go_countDown.SetActive(true);
                        if(adReduceTime> countDown) 
                        {
                            text_count.text = I2.Loc.ScriptLocalization.Get("Obj/merge/UnlockInstantly");
                        }
                        else 
                        {
                            text_count.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(countDown);
                        }
                        is_show_text = true;
                        go_countDown.SetActive(AdManager.CanShowAD_Normal(AdManager.ADTag.ClearItemCD));
                    }
                    else
                    {
                        go_countDown.SetActive(false);
                        if (is_show_text && !operateData.IsInCD())
                        {
                            RefreshSelectItemInfo();
                            is_show_text = false;
                        }

                    }

                }
            }
            else
            {
                int countDown = (int)ExtensionTool.RemainTime(operateData.ChargeFinishDate).TotalSeconds;
                if (countDown > 0 && operateData.IsInCD())
                {
                    go_countDown.SetActive(true);
                    if (adReduceTime > countDown)
                    {
                        text_count.text = I2.Loc.ScriptLocalization.Get("Obj/merge/UnlockInstantly");
                    }
                    else
                    {
                        text_count.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(countDown);
                    }
                    is_show_text = true;
                    go_countDown.SetActive(AdManager.CanShowAD_Normal(AdManager.ADTag.ClearItemCD));
                }
                else
                {
                    go_countDown.SetActive(false);
                    if (is_show_text && !operateData.IsInCD())
                    {
                        RefreshSelectItemInfo();
                        is_show_text = false;
                    }
                }
            }
        }

        if (btn_Unlock.gameObject.activeSelf && BubbleDieTime != DateTimeOffset.MinValue)
        {
            var leftScends = (int)ExtensionTool.RemainTime(BubbleDieTime).TotalSeconds;
            text_Des_Unlock.text =
                MyTimer.ReturnTextUntilSecond_MaxShowTwo((int)ExtensionTool.RemainTime(BubbleDieTime).TotalSeconds);
        }

        if (btn_ClearCDbyGem.gameObject.activeSelf && ChargeOverTime != DateTimeOffset.MinValue)
        {
            if (ChargeOverTime <= TimeManager.Instance.UtcNow())
                RefreshSelectItemInfo();
        }

        if (MergeLevelManager.Instance.IsDailyActive(MergeLevelManager.Instance.CurrentLevelType))
        {
            if (!CountDown.gameObject.activeSelf)
                CountDown.gameObject.SetActive(true);
            var leftSeconds = (int)(TimeManager.Instance.GetTomorrowRefreshTimeSpan()).TotalSeconds;
            txtDailyTime.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(leftSeconds);
        }
        else
        {
            if (CountDown.gameObject.activeSelf)
                CountDown.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        RefreshCountDown();
    }

    private List<SwallowItem> _swallowItemList = new List<SwallowItem>();
    public override void RefreshSelectItemInfo()
    {
        if (lastSellItemData != null)
            lastSellItemData = null;

        if (mergeController == null || mergeController.currentSelectItemData == null || mergeController.currentSelectItemData.Definition == null)
        {
            selectBox.gameObject.SetActive(false);
            btn_ItemTips.gameObject.SetActive(false);

            btnLayout.SetActive(false);
            SwallowParent.SetActive(false);

            showTip_ItemName.gameObject.SetActive(false);
            showTip_ItemDescribe.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/Mainl/Text");
            return;
        }

        showTip_ItemName.gameObject.SetActive(true);
        btn_ItemTips.gameObject.SetActive(true);


        var operateData = mergeController.currentSelectItemData;
        RefreshItemQuality((int)operateData.Definition.RarityType);
        //TryShowClrerCDBtn(false);
        bool needShowCD = false;
        //是否为吞噬道具
        if (!operateData.IsInBubble && !operateData.IsLocked &&
            (operateData.Definition.CategoryType == MergeItemCategoryType.swallowC
            || operateData.Definition.CategoryType == MergeItemCategoryType.swallowZ
            || operateData.Definition.CategoryType == MergeItemCategoryType.swallowF))
        {
            btnLayout.SetActive(false);
            if (operateData.Definition.NeedJoinCollection)
            {
                showTip_ItemName.text = I2.Loc.ScriptLocalization.Get(operateData.Definition.locKey_Name)
                + string.Format(I2.Loc.ScriptLocalization.Get("Obj/Item/Info/Level"), operateData.Definition.Level.ToString())
                +" <color=#0000ffff>" + "(" + I2.Loc.ScriptLocalization.Get("Obj/Merge/Special")+")" + "</color>";
            }
            else 
            {
                showTip_ItemName.text = I2.Loc.ScriptLocalization.Get(operateData.Definition.locKey_Name)
                + " <color=#0000ffff>" + "(" + I2.Loc.ScriptLocalization.Get("Obj/Merge/Special") + ")" + "</color>";
            }
            

            string strLevel = I2.Loc.ScriptLocalization.Get("Obj/Desc/ItemLevel_Text");
            strLevel = string.Format(strLevel, operateData.Definition.Level.ToString());
            string strDes = I2.Loc.ScriptLocalization.Get(operateData.Definition.ItemDescription);
            showTip_ItemDescribe.text = string.Format(strDes, strLevel);
            if (operateData.canSwallow)
            {
                SwallowParent.SetActive(true);
                int index = 0;
                foreach (var data in operateData.curSwallowItemDic)
                {
                    if (_swallowItemList.Count > index)
                    {
                        _swallowItemList[index].gameObject.SetActive(true);
                        _swallowItemList[index].InitWithData(operateData, data.Key);
                    }
                    else
                    {
                        var prefab = AssetSystem.Instance.Instantiate("SwallowItem", SwallowLayout.transform);
                        prefab.transform.SetAsFirstSibling();
                        if (prefab != null && prefab.TryGetComponent<SwallowItem>(out var swallow))
                        {
                            prefab.layer = LayerMask.NameToLayer("UI");
                            swallow.transform.localPosition = Vector3.zero;
                            swallow.transform.localScale = Vector3.one;
                            swallow.InitWithData(operateData, data.Key);
                            _swallowItemList.Add(swallow);
                        }
                        else
                        {
                            Debug.LogError("SwallowItem_Instantiate_Error!");
                            if (prefab != null)
                                AssetSystem.Instance.DestoryGameObject("", prefab);
                        }
                    }
                    index++;
                }
                for (int i = select_Prefab.transform.childCount; i > 0; --i)
                {
                    DestroyImmediate(select_Prefab.transform.GetChild(0).gameObject);
                }
                AssetSystem.Instance.Instantiate(operateData.Definition.PrefabName, select_Prefab);
                showItem.RefreshPrefabName(operateData.Definition.PrefabName,null);
                for (; index < _swallowItemList.Count; index++)
                    _swallowItemList[index].gameObject.SetActive(false);
                showTip_ItemDescribe.gameObject.SetActive(false);
                text_put.text= I2.Loc.ScriptLocalization.Get("Obj/Merge/PutIn");
                text_produce.text = I2.Loc.ScriptLocalization.Get("Obj/Merge/Produce");
                LayoutRebuilder.ForceRebuildLayoutImmediate(SwallowParent.GetComponent<RectTransform>());
                
            }
            else 
            {
                SwallowParent.SetActive(false);
                showTip_ItemDescribe.gameObject.SetActive(true);
            }
                
        }
        else
        {
            showTip_ItemDescribe.gameObject.SetActive(true);
            btnLayout.SetActive(true);
            SwallowParent.SetActive(false);
            btn_RevokeSell.gameObject.SetActive(false);
            //优先判断是否需要解锁
            if (operateData.IsInBubble || operateData.IsLocked)
            {
                btn_Unlock.gameObject.SetActive(true);
                btn_Sell.gameObject.SetActive(false);
                btn_RevokeSell.gameObject.SetActive(false);
                btn_ClearCDbyGem.gameObject.SetActive(false);
                btn_OpenBox.gameObject.SetActive(false);

                if (operateData.IsInBubble)
                {
                    TryShowNeedleBtn(true);
                    showTip_ItemName.text = I2.Loc.ScriptLocalization.Get(operateData.Definition.locKey_Name) + string.Format(I2.Loc.ScriptLocalization.Get("Obj/Item/Info/Level"), operateData.Definition.Level.ToString()) + I2.Loc.ScriptLocalization.Get("Obj/Eliminate/InfoBar/Button10");
                    showTip_ItemDescribe.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/InfoBar/Button8");

                    BubbleDieTime = operateData.bubbleDieTime;
                    text_Des_Unlock.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo((int)ExtensionTool.RemainTime(BubbleDieTime).TotalSeconds);
                    text_Cost_Unlock.text = operateData.Definition.BubbleUnlockCost.ToString();
                    int needleNum = (int)Math.Ceiling((float)operateData.Definition.BubbleUnlockCost / 5);
                    needleNum = needleNum > 0 ? needleNum : 1;
                    text_NeedleNum.text = needleNum.ToString();
                    t_HasNeedleAd.text = Currencies.Needle.ToString() + "/" + needleNum.ToString();
                    t_NoNeedleAd.text = Currencies.Needle.ToString() + "/" + needleNum.ToString();
                }
                else
                {
                    TryShowNeedleBtn(false);
                    BubbleDieTime = DateTimeOffset.MinValue;
                    showTip_ItemName.text = /*I2.Loc.ScriptLocalization.Get("Obj/Eliminate/InfoBar/Button9") +*/ I2.Loc.ScriptLocalization.Get(operateData.Definition.locKey_Name) + string.Format(I2.Loc.ScriptLocalization.Get("Obj/Item/Info/Level"), operateData.Definition.Level.ToString());
                    showTip_ItemDescribe.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/InfoBar/Button7");
                    text_Des_Unlock.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/InfoBar/Button1");
                    text_Cost_Unlock.text = operateData.Definition.UnLockCost.ToString();
                }
            }
            else
            {
                TryShowNeedleBtn(false);
                btn_Unlock.gameObject.SetActive(false);
                showTip_ItemName.text = I2.Loc.ScriptLocalization.Get(operateData.Definition.locKey_Name) + string.Format(I2.Loc.ScriptLocalization.Get("Obj/Item/Info/Level"), operateData.Definition.Level.ToString());
                string strLevel = I2.Loc.ScriptLocalization.Get("Obj/Desc/ItemLevel_Text");
                strLevel = string.Format(strLevel, operateData.Definition.Level.ToString());
                string strDes = I2.Loc.ScriptLocalization.Get(operateData.Definition.ItemDescription);
                showTip_ItemDescribe.text = string.Format(strDes, strLevel);/*.Replace(" ", "\u3000");*/

                //可出售
                if (operateData.Definition.SellValue > 0)
                {
                    btn_Sell.gameObject.SetActive(true);
                    text_Cost_Sell.text = operateData.Definition.SellValue.ToString();
                    text_Des_Sell.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/InfoBar/Button2");
                }
                else
                    btn_Sell.gameObject.SetActive(false);

                //可清除CD
                if (operateData.Definition.FinishChargeCost > 0 && operateData.IsInCD())
                {
                    needShowCD = true;
                    btn_ClearCDbyGem.gameObject.SetActive(true);
                    //ClearCDGemCost = operateData.Definition.FinishChargeCost;
                    ClearCDGemCost = operateData.GetClearCDCost();
                    t_ClearCDCost.text = ClearCDGemCost.ToString();
                    //教学
                    if (!SaveUtils.GetBool(Consts.SaveKey_Tutorial_ClearCD) || isInOpenBoxGuide)
                    {
                        isInOpenBoxGuide = false;
                        isTutorialFreeClearCD = true;
                        t_ClearCDbyGem.gameObject.SetActive(false);
                        t_ClearCDCost.gameObject.SetActive(false);
                        text_Free_ClearCD.gameObject.SetActive(true);
                        text_Free_ClearCD.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/Engery/Text3");
                        SaveUtils.SetBool(Consts.SaveKey_Tutorial_ClearCD, true);
                        UI_TutorManager.Instance.RunTutorWithName("InnFreeClearCd");
                    }
                    else
                    {
                        t_ClearCDbyGem.gameObject.SetActive(true);
                        t_ClearCDCost.gameObject.SetActive(true);
                        text_Free_ClearCD.gameObject.SetActive(false);
                    }
                    if (operateData.Definition.SellValue > 0)
                    {
                        btn_Sell.gameObject.SetActive(true);
                    }
                }

                //可开箱
                if ((operateData.Definition.CategoryType == MergeItemCategoryType.finiteContainer
                    || operateData.Definition.CategoryType == MergeItemCategoryType.taskBox)
                    && operateData.Definition.BoxOpenTime > 0
                    && operateData.boxOpenDelayEndTime == DateTimeOffset.MinValue && !operateData.boxOpenDelayStart)
                {
                    btn_OpenBox.gameObject.SetActive(true);
                    text_Des_OpenBox.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/InfoBar/Button5");
                    //教学
                    if (!SaveUtils.GetBool(Consts.SaveKey_Tutorial_OpenBox))
                    {
                        isInOpenBoxGuide = true;
                        SaveUtils.SetBool(Consts.SaveKey_Tutorial_OpenBox, true);
                        UI_TutorManager.Instance.RunTutorWithName("MergeOpenBox");
                    }
                }
                else
                    btn_OpenBox.gameObject.SetActive(false);
            }
        }
        RefreshShowCD(operateData.Definition.FinishChargeCost.ToString(), needShowCD);
    }

    private bool isInOpenBoxGuide = false;

    private void TryShowNeedleBtn(bool show)
    {
        btn_Needle.gameObject.SetActive(show);
        if (mergeController == null || mergeController.currentSelectItemData == null || !mergeController.currentSelectItemData.IsInBubble)
            return;

        int cost = (int)Math.Ceiling((float)mergeController.currentSelectItemData.Definition.BubbleUnlockCost / 5);
        cost = cost > 0 ? cost : 1;
        if (Currencies.CanAffordOrTip(CurrencyID.Needle, cost))
        {
            needleState = NeedleState.needleEnough;
        }
        else
        {
            if (AdManager.CanShowAD_Normal())
            {
                needleState = NeedleState.hasNeedleAd;
            }
            else
            {
                needleState = NeedleState.noNeedleAd;
            }
        }
        switch (needleState)
        {
            case NeedleState.needleEnough:
                needleEnough.SetActive(true);
                hasNeedleAd.SetActive(false);
                noNeedleAd.SetActive(false);
                break;
            case NeedleState.hasNeedleAd:
                needleEnough.SetActive(false);
                hasNeedleAd.SetActive(true);
                noNeedleAd.SetActive(false);
                break;
            case NeedleState.noNeedleAd:
                needleEnough.SetActive(false);
                hasNeedleAd.SetActive(false);
                noNeedleAd.SetActive(true);
                break;
            default:
                btn_Needle.gameObject.SetActive(false);
                break;
        }
    }
    NeedleState needleState = NeedleState.none;
    private enum NeedleState
    {
        none,
        needleEnough,
        hasNeedleAd,
        noNeedleAd
    }
    //private void TryShowClrerCDBtn(bool show)
    //{
    //    if (show)
    //    {
    //        int seed = UnityEngine.Random.Range(0, 2);
    //        if (!AdManager.CanShowAD_Normal() || seed == 0)
    //        {
    //            btn_ClearCDbyGem.gameObject.SetActive(true);
    //            btn_ClearCDbyAD.gameObject.SetActive(false);
    //            t_ClearCDbyGem.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/InfoBar/Button4");
    //        }
    //        else
    //        {
    //            btn_ClearCDbyGem.gameObject.SetActive(false);
    //            btn_ClearCDbyAD.gameObject.SetActive(true);
    //            t_ClearCDbyAD.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/PointsRewardText1");
    //        }
    //    }
    //    else
    //    {
    //        if (btn_ClearCDbyGem.gameObject.activeSelf)
    //            btn_ClearCDbyGem.gameObject.SetActive(false);
    //        if (btn_ClearCDbyAD.gameObject.activeSelf)
    //            btn_ClearCDbyAD.gameObject.SetActive(false);
    //    }
    //}

    private void RefreshItemQuality(int starNum)
    {
        if (starNum == 0)
        {
            NormatStarBg.SetActive(false);
            stars.SetActive(false);
        }
        else
        {
            NormatStarBg.SetActive(true);
            stars.SetActive(true);
            for (int i = 0; i < showItemStar.Length; i++)
            {
                showItemStar[i].SetActive(i < starNum);
            }
        }
        for (int i = 0; i < showItemBox.Length; i++)
        {
            if (showItemBox[i].activeSelf)
            {
                showItemBox[i].SetActive(false);
            }
        }
        int itemQuality = starNum - 1 < 0 ? 0 : starNum - 1;
        showItemBox[itemQuality].SetActive(true);
    }



    //确认出售
    public override void FixSellItem()
    {
        lastSellItemData = mergeController.currentSelectItemData;
        mergeController.SellSelectedItem();
        Currencies.Add(CurrencyID.Coins, mergeController.currentSelectItemData.Definition.SellValue, "sell");
        btn_Sell.gameObject.SetActive(false);
        btn_RevokeSell.gameObject.SetActive(true);
        btn_ClearCDbyGem.gameObject.SetActive(false);
        btn_OpenBox.gameObject.SetActive(false);
        text_Cost_RevokeSell.text = mergeController.currentSelectItemData.Definition.SellValue.ToString();
        text_Des_RevokeSell.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/DoubleConfirmation/Text2");
        showTip_ItemDescribe.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/DoubleConfirmation/Text1");
    }
    public override void RefreshStoreEffect(bool show)
    {
        //if (show)
        //    effect_Store.Play();
        //else
        //{
        //    effect_Store.Stop();
        //    effect_Store.Clear();
        //}

    }
   

    /// <summary>
    /// 展示新发现物品界面
    /// </summary>
    /// <param name="prefabName"></param>
    public override void ShowNewDiscoveryView(string prefabName)
    {
        if (GameManager.Instance.TryNewDiscoveryItem(prefabName))
        {
            if (!UI_TutorManager.Instance.IsTutoring() && MergeItemDefinition.TotalDefinitionsDict.TryGetValue(prefabName, out MergeItemDefinition mergeItemDefinition))
            {
                if (mergeItemDefinition.RarityType > ItemRarityType.Simple)
                {
                    try
                    {
                        UISystem.Instance.ShowUI(new NewContainerItemData(prefabName));
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.getNewMergeItem);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("弹出新发现界面错误:" + e);
                    }
                }
            }
        }
    }
    public override void SetNeedleNum()
    {
        t_LeftNeedleNum.text = Currencies.Needle.ToString();
    }

    #region 按钮事件
    private MergeItemData lastSellItemData;//刚刚卖掉的物体

    private void OnBtnCloseClick()
    {
        AdManager.ManualExitMergePanel = true;
        //UI_TutorManager.Instance.RunTutorWithName("TestMergeDrag");
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
        VibrateSystem.Haptic(HapticTypes.Selection);
        TryTweenTaskBack();

        if (MergeController.CurrentController != null)
            MergeController.CurrentController.HideMergeUI();
        else
            UISystem.Instance.HideUI(this);
    }

    private void ShowInfoByBtnClick()
    {
        if (mergeController.currentSelectItemData == null || mergeController.currentSelectItemData.Definition == null)
        {
            return;
        }
        if (mergeController.currentSelectItemData.Definition.WindowType == MergeItemWindowType.universal)
            UISystem.Instance.ShowUI(new UIPanelData_ShowTip(mergeController.currentSelectItemData.Definition.PrefabName));
        else if (mergeController.currentSelectItemData.Definition.WindowType == MergeItemWindowType.count)
            UISystem.Instance.ShowUI(new UIPanelData_ShowCountTip(mergeController.currentSelectItemData.Definition.PrefabName));
        else if (mergeController.currentSelectItemData.Definition.WindowType == MergeItemWindowType.output)
            UISystem.Instance.ShowUI(new UIPanelData_ShowOutputTip(mergeController.currentSelectItemData.Definition.PrefabName));
        else if (mergeController.currentSelectItemData.Definition.WindowType == MergeItemWindowType.special)
            UISystem.Instance.ShowUI(new UIPanelData_ShowSpecialTip(mergeController.currentSelectItemData.Definition.PrefabName));
        else if (mergeController.currentSelectItemData.Definition.WindowType == MergeItemWindowType.rare)
            UISystem.Instance.ShowUI(new UIPanelData_ShowRareTip(mergeController.currentSelectItemData.Definition.PrefabName));

        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
    }

    public void F_OpenStoreViewClick()
    {
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
        UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_Store));
    }

    //打开内置商城
    private void OpenInternalShop()
    {
        //以防为获取组件打不开商城
        lastSellItemData = null;
        mergeController.SetCurrentSelectData(null);
        try
        {
            UISystem.Instance.ShowUI(new InternalShopData(TopResourceType.NONE));
        }
        catch (Exception e)
        {
            GameDebug.LogError("Open Shop Error!" + e);
        }
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
    }

    private void OpenTaskViewClick()
    {
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine)
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_AllTask));
        else
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_DungeonTask));
    }
    #endregion

    #region 顶部临时背包
    private string RewardBoxName = string.Empty;
    private GameObject saveRewardBoxItem = null;
    private MergeLevelType lastLevelType = MergeLevelType.none;
    /// <summary>
    /// 刷新顶部临时背包
    /// </summary>
    public override void RefreshRewardBox()
    {
        if (lastLevelType != MergeLevelManager.Instance.CurrentLevelType)
        {
            lastLevelType = MergeLevelManager.Instance.CurrentLevelType;
            if (!string.IsNullOrEmpty(RewardBoxName) && saveRewardBoxItem != null)
            {
                AssetSystem.Instance.DestoryGameObject(RewardBoxName, saveRewardBoxItem);
                RewardBoxName = string.Empty;
                saveRewardBoxItem = null;
            }
        }

        if (RewardBoxManager.Instance.TrySpawnItem(MergeLevelManager.Instance.CurrentLevelType, out string prefabName) && saveRewardBoxItem == null)
        {
            RewardBoxName = prefabName;
            saveRewardBoxItem = AssetSystem.Instance.Instantiate(prefabName, btn_spawn.transform);
        }
        refreshTopAction?.Invoke();
        if (saveRewardBoxItem == null) 
            btn_spawn.transform.Find("Beibao").gameObject.SetActive(false);
        else 
            btn_spawn.transform.Find("Beibao").gameObject.SetActive(true);
        btn_spawn.gameObject.SetActive(MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine);
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine) 
        {
            horizontal.padding.left = 250;
        }
        else
        {
            horizontal.padding.left = 25;
        }
    }

    private void TryMoveItemToMapByClick()
    {
        if (!string.IsNullOrEmpty(RewardBoxName) && mergeController.TryGetNearEmptyGrid(Vector2Int.one, out var gridPos, btn_spawn.transform.position))
        {
            mergeController.CreateItem_FlyToGrid(RewardBoxName, saveRewardBoxItem.transform.position, gridPos, 0, false, true, true);
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Get_Reward);
            RewardBoxManager.Instance.TryDestroyItemAndSaveData(MergeLevelManager.Instance.CurrentLevelType, RewardBoxName);
            AssetSystem.Instance.DestoryGameObject(RewardBoxName, saveRewardBoxItem);
            RewardBoxName = string.Empty;
            saveRewardBoxItem = null;
            RefreshRewardBox();
        }
        if (!SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnBackPack))
        {
            Invoke("DoTutorInnBackPack", 0.4f);
        }
    }
    private void DoTutorInnBackPack()
    {
        GameDebug.Log("执行InnBackPack教学");
        UI_TutorManager.Instance.RunTutorWithName("InnBackPack");
        SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnBackPack, true);
    }
    #endregion

    #region 顶部任务
    private Dictionary<string, UI_TaskItem> saveTaskItemDic = new Dictionary<string, UI_TaskItem>();//主线
    private UI_TaskItem saveDungeonItem = null;
    private UI_TaskItem uI_TaskItemDaily = null;
    private UI_TaskItem uI_PointBranch = null;
    private int curCompleteTaskCount = 0;
    private void InitCreateTaskItem()
    {
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine)//主线
        {
            t_LeftTaskNum.transform.parent.gameObject.SetActive(true);
            List<string> tasks = TaskGoalsManager.Instance.ReturnSortTask();
            for (int i = 0; i < tasks.Count; i++)
            {
                GameObject gO = AssetSystem.Instance.Instantiate(Consts.UI_TaskItem, grid_Task.transform);
                if (gO && gO.TryGetComponent(out UI_TaskItem item))
                {
                    item.InitMainLineItem(tasks[i]);
                    saveTaskItemDic.Add(tasks[i], item);
                }
            }
        }
        else if (MergeLevelManager.Instance.IsBranch(MergeLevelManager.Instance.CurrentLevelType)) //支线
        {
            //无任务活动
            if(MergeLevelManager.Instance.CurrentLevelType== MergeLevelType.branch_SpurLine4
                || MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.branch_SpurLine5
                || MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.branch_SpurLine6) 
            {
                GameObject gO = AssetSystem.Instance.Instantiate(Consts.UI_TaskItem, targetItemParent);
                if (!(gO && gO.TryGetComponent(out uI_PointBranch)))
                {
                    return;
                }
                uI_PointBranch.InitPiontBranchItem();
                BranchPointTrans = uI_PointBranch.ReturnPointTrans();
            }
            else
            {
                List<string> tasks = BranchSystem.Instance.CurTaskList;
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (!saveTaskItemDic.ContainsKey(tasks[i]))
                    {
                        GameObject gO = AssetSystem.Instance.Instantiate(Consts.UI_TaskItem, grid_Task.transform);
                        if (gO && gO.TryGetComponent(out UI_TaskItem item))
                        {
                            item.InitBranchItem(tasks[i]);
                            saveTaskItemDic.Add(tasks[i], item);
                        }
                    }
                }
            }
        }
        else if (MergeLevelManager.Instance.IsFestivalBranch(MergeLevelManager.Instance.CurrentLevelType))
        {
            List<string> tasks = FestivalSystem.Instance.ReturnCurTaskListByType(MergeLevelManager.Instance.CurrentLevelType);
            for (int i = 0; i < tasks.Count; i++)
            {
                if (!saveTaskItemDic.ContainsKey(tasks[i]))
                {
                    GameObject gO = AssetSystem.Instance.Instantiate(Consts.UI_TaskItem, grid_Task.transform);
                    if (gO && gO.TryGetComponent(out UI_TaskItem item))
                    {
                        item.InitBranchItem(tasks[i]);
                        saveTaskItemDic.Add(tasks[i], item);
                    }
                }
            }
        }
        //简单任务
        else if (MergeLevelManager.Instance.IsDailyActive(MergeLevelManager.Instance.CurrentLevelType)) 
        {
            if(DailyDefinition.DailyDefDic.TryGetValue(MergeLevelManager.Instance.CurrentLevelType,out DailyDefinition dailyDefinition))
            {
                GameObject gO = AssetSystem.Instance.Instantiate(Consts.UI_TaskItem, targetItemParent);
                if (!(gO && gO.TryGetComponent(out uI_TaskItemDaily)))
                {
                    return;
                }
                //爱心活动
                if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.daily4) 
                {
                    uI_TaskItemDaily.InitLoveDailyTaskItem(dailyDefinition.Score);
                    DungeonLoveTrans = uI_TaskItemDaily.ReturnLoveTrans();
                }
                else 
                {
                    uI_TaskItemDaily.InitNormalDailyTaskItem(dailyDefinition.AmiMergeItems);
                }
            }
        }
        else //副本
        {
            t_LeftTaskNum.transform.parent.gameObject.SetActive(false);
            if (DungeonDefinition.DungeonDefDic.TryGetValue(MergeLevelManager.Instance.CurrentLevelType, out DungeonDefinition definition))
            {
                if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.dungeon2 && definition.Score.Count == 1)//爱心副本
                {
                    MergeRewardItem needItem = definition.Score[0];
                    GameObject gO = AssetSystem.Instance.Instantiate(Consts.UI_TaskItem, targetItemParent);
                    if (gO && gO.TryGetComponent(out saveDungeonItem))
                    {
                        saveDungeonItem.InitLoveDungeonTaskItem(needItem);
                        DungeonLoveTrans = saveDungeonItem.ReturnLoveTrans();
                    }
                }
                else//普通副本
                {
                    MergeRewardItem needItem = definition.AmiMergeItems[0];
                    GameObject gO = AssetSystem.Instance.Instantiate(Consts.UI_TaskItem, targetItemParent);
                    if (gO && gO.TryGetComponent(out saveDungeonItem))
                    {
                        saveDungeonItem.InitCommonDungeonTaskItem(needItem);
                    }
                }
            }
        }
        RefreshTopTaskUI();
        RefreshTaskItem();
    }
    public void RefreshTaskItem()
    {
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine)//主线
        {
            curCompleteTaskCount = 0;
            foreach (var item in saveTaskItemDic)
            {
                item.Value.RefreshMainLineItem();
                if (item.Value.IsCompleteTask())
                {
                    item.Value.transform.SetSiblingIndex(curCompleteTaskCount);
                    curCompleteTaskCount++;
                }
            }
            foreach (var item in saveTaskItemDic)
            {
                if (item.Value.IsCompletePart())
                {
                    item.Value.transform.SetSiblingIndex(curCompleteTaskCount);
                    curCompleteTaskCount++;
                }
            }
        }
        else if (MergeLevelManager.Instance.IsBranch(MergeLevelManager.Instance.CurrentLevelType))
        {
            if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.branch_SpurLine4 
                || MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.branch_SpurLine5
                || MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.branch_SpurLine6)
            {
                if (uI_PointBranch == null)
                    return;
                completeAllBranchTask.SetActive(BranchSystem.Instance.ClaimedAllTask());
                uI_PointBranch.gameObject.SetActive(!BranchSystem.Instance.ClaimedAllTask());
                uI_PointBranch.RefreshPiontBranchItem();
            }
            else 
            {
                //支线完成任务时需要新增任务，此处处理
                List<string> tasks = BranchSystem.Instance.CurTaskList;
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (!saveTaskItemDic.ContainsKey(tasks[i]))
                    {
                        GameObject gO = AssetSystem.Instance.Instantiate(Consts.UI_TaskItem, grid_Task.transform);
                        if (gO && gO.TryGetComponent(out UI_TaskItem item))
                        {
                            item.InitBranchItem(tasks[i]);
                            saveTaskItemDic.Add(tasks[i], item);
                        }
                    }
                }
                curCompleteTaskCount = 0;
                foreach (var item in saveTaskItemDic)
                {
                    item.Value.RefreshBranchItem();
                    if (item.Value.IsCompleteTask())
                    {
                        item.Value.transform.SetSiblingIndex(curCompleteTaskCount);
                        curCompleteTaskCount++;
                    }
                }
                foreach (var item in saveTaskItemDic)
                {
                    if (item.Value.IsCompletePart())
                    {
                        item.Value.transform.SetSiblingIndex(curCompleteTaskCount);
                        curCompleteTaskCount++;
                    }
                }
            }
        }
        else if (MergeLevelManager.Instance.IsFestivalBranch(MergeLevelManager.Instance.CurrentLevelType))
        {
            List<string> tasks = FestivalSystem.Instance.ReturnCurTaskListByType(MergeLevelManager.Instance.CurrentLevelType);
            for (int i = 0; i < tasks.Count; i++)
            {
                if (!saveTaskItemDic.ContainsKey(tasks[i]))
                {
                    GameObject gO = AssetSystem.Instance.Instantiate(Consts.UI_TaskItem, grid_Task.transform);
                    if (gO && gO.TryGetComponent(out UI_TaskItem item))
                    {
                        item.InitBranchItem(tasks[i]);
                        saveTaskItemDic.Add(tasks[i], item);
                    }
                }
            }
            curCompleteTaskCount = 0;
            foreach (var item in saveTaskItemDic)
            {
                item.Value.RefreshBranchItem();
                if (item.Value.IsCompleteTask())
                {
                    item.Value.transform.SetSiblingIndex(curCompleteTaskCount);
                    curCompleteTaskCount++;
                }
            }
            foreach (var item in saveTaskItemDic)
            {
                if (item.Value.IsCompletePart())
                {
                    item.Value.transform.SetSiblingIndex(curCompleteTaskCount);
                    curCompleteTaskCount++;
                }
            }
        }
        else if (MergeLevelManager.Instance.IsDailyActive(MergeLevelManager.Instance.CurrentLevelType)) 
        {
            if (uI_TaskItemDaily != null)
            {
                if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.daily4)//爱心副本
                {
                    uI_TaskItemDaily.RefreshLoveDailyItem();
                }
                else
                {
                    uI_TaskItemDaily.RefreshCommonDailyItem();
                }
            }
        }
        else  //副本
        {
            if (saveDungeonItem != null)
            {
                if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.dungeon2)//爱心副本
                {
                    saveDungeonItem.RefreshLoveDungeonItem();
                }
                else
                {
                    saveDungeonItem.RefreshCommonDungeonItem();
                }
            }
        }
    }
    private void DestroyAllTaskItem()
    {
        foreach (var item in saveTaskItemDic)
        {
            AssetSystem.Instance.DestoryGameObject(Consts.UI_TaskItem, item.Value.gameObject);
        }
        saveTaskItemDic.Clear();

        if (saveDungeonItem != null)
        {
            AssetSystem.Instance.DestoryGameObject(Consts.UI_TaskItem, saveDungeonItem.gameObject);
        }
        saveDungeonItem = null;

        if (uI_TaskItemDaily != null) 
        {
            AssetSystem.Instance.DestoryGameObject(Consts.UI_TaskItem, uI_TaskItemDaily.gameObject);
        }
        uI_TaskItemDaily = null;
        if (uI_PointBranch != null)
        {
            AssetSystem.Instance.DestoryGameObject(Consts.UI_TaskItem, uI_PointBranch.gameObject);
        }
        uI_PointBranch = null;
    }



    public override void DestroyTaskItemById(string taskId)
    {
        if (saveTaskItemDic.TryGetValue(taskId, out UI_TaskItem uI_TaskItem))
        {
            AssetSystem.Instance.DestoryGameObject(Consts.UI_TaskItem, uI_TaskItem.gameObject);
            saveTaskItemDic.Remove(taskId);
            RefreshTopTaskUI();
        }
        RefreshTaskItem();
    }

    private void DoAllTaskRootAni() 
    {
        completeAllTaskRoot.transform.DOLocalMoveX(0, 0.3f).SetEase(Ease.InOutQuad);
    }

    private void RefreshTopTaskUI()
    {
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine)
        {
            t_LeftTaskNum.text = TaskGoalsManager.Instance.leftTaskIdList.Count.ToString();
            bool completeAllTask = TaskGoalsManager.Instance.leftTaskIdList.Count == 0;
            t_CompleteAllTask.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Orders_Button4");
            t_CompleteAllChapter.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/PassTask/Text");
            bool isCompleteAllChapter = TaskGoalsManager.Instance.IsCompleteAllChapter;
            taskViewRoot.SetActive(!isCompleteAllChapter);
            completeAllTaskRoot.SetActive(completeAllTask && !isCompleteAllChapter);
            completeAllChapterRoot.SetActive(isCompleteAllChapter);
            completeAllBranchTask.SetActive(false);
            completeAllTaskRoot.transform.localPosition = new Vector3(0, -92, 0);
        }
        else if (MergeLevelManager.Instance.IsBranch(MergeLevelManager.Instance.CurrentLevelType)
            || MergeLevelManager.Instance.IsFestivalBranch(MergeLevelManager.Instance.CurrentLevelType))
        {
            if (MergeLevelManager.Instance.IsBranch(MergeLevelManager.Instance.CurrentLevelType))
            {
                if(MergeLevelManager.Instance.CurrentLevelType== MergeLevelType.branch_SpurLine4) 
                {
                    completeAllBranchTask.SetActive(BranchSystem.Instance.ClaimedAllTask());
                }
                else 
                {
                    completeAllBranchTask.SetActive(BranchSystem.Instance.CurTaskList.Count == 0);
                    t_LeftTaskNum.text = BranchSystem.Instance.CurTaskList.Count.ToString();
                }
            }
            else
            {
                t_LeftTaskNum.text = FestivalSystem.Instance.ReturnCurTaskListByType(MergeLevelManager.Instance.CurrentLevelType).Count.ToString();
                completeAllBranchTask.SetActive(FestivalSystem.Instance.ReturnCurTaskListByType(MergeLevelManager.Instance.CurrentLevelType).Count==0);
            }
               
            taskViewRoot.SetActive(true);
            completeAllTaskRoot.SetActive(false);
            completeAllChapterRoot.SetActive(false);
            t_CompleteAllBranchTask.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Orders_Button5");
        }

        else
        {
            completeAllTaskRoot.SetActive(false);
            completeAllChapterRoot.SetActive(false);
            completeAllBranchTask.SetActive(false);
        }
    }
    private void OpenTaskBtnClick()
    {
        UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_Task));
    }
    #endregion

    #region 底部滑动背包
    private List<StoreItem> saveStoreItemList = new List<StoreItem>();
    public override void RefreshBottomStore(int index = -1)
    {
        if (MergeLevelManager.Instance == null || MergeLevelManager.Instance.CurrentLevelType != MergeLevelType.mainLine)
        {
            return;
        }

        List<MergeItemData> itemDataList = MergeLevelManager.Instance.CurrentMapData.storePackList;
        if (saveStoreItemList.Count == 0)
        {
            //初始化背包
            for (int i = 0; i < itemDataList.Count; i++)
            {
                GameObject itemRoot = AssetSystem.Instance.Instantiate(Consts.StoreItem, grid_Store.transform);
                //ShowStoreItemTween(itemRoot.transform);
                GameObject go = AssetSystem.Instance.Instantiate(itemDataList[i].PrefabName, itemRoot.transform);
                go.transform.localScale *= 0.8f;
                StoreItem item = itemRoot.GetComponent<StoreItem>();
                if (item != null)
                {
                    saveStoreItemList.Add(item);
                }
            }
            StoreRootTween();
        }
        else if (itemDataList.Count > saveStoreItemList.Count)
        {
            //向背包中增加item
            for (int i = 0; i < itemDataList.Count; i++)
            {
                bool isNewItem = true;
                for (int j = 0; j < saveStoreItemList.Count; j++)
                {
                    if (saveStoreItemList[j].mergeItemData == itemDataList[i])
                    {
                        //不是新增的数据
                        isNewItem = false;
                        break;
                    }
                }
                if (isNewItem)
                {
                    GameObject itemRoot = AssetSystem.Instance.Instantiate(Consts.StoreItem, grid_Store.transform);
                    if (index == -1)
                    {
                        itemRoot.transform.SetAsLastSibling();
                    }
                    else
                    {
                        itemRoot.transform.SetSiblingIndex(index);
                    }

                    ShowStoreItemTween(itemRoot.transform);
                    GameObject go = AssetSystem.Instance.Instantiate(itemDataList[i].PrefabName, itemRoot.transform);
                    go.transform.localScale *= 0.8f;
                    StoreItem item = itemRoot.GetComponent<StoreItem>();
                    if (item != null)
                    {
                        if (index == -1)
                        {
                            saveStoreItemList.Add(item);
                        }
                        else
                        {
                            saveStoreItemList.Insert(index, item);
                        }
                    }
                }
            }
            StoreRootTween();
        }
        else if (itemDataList.Count < saveStoreItemList.Count)
        {
            //从背包移除item
            for (int i = 0; i < saveStoreItemList.Count; i++)
            {
                if (!itemDataList.Contains(saveStoreItemList[i].mergeItemData))
                {
                    AssetSystem.Instance.DestoryGameObject(Consts.StoreItem, saveStoreItemList[i].gameObject);
                    saveStoreItemList.Remove(saveStoreItemList[i]);
                }
            }
        }
        else if (itemDataList.Count == saveStoreItemList.Count)
        {
            //背包和棋盘数据交换  
            for (int i = 0; i < saveStoreItemList.Count; i++)
            {
                if (itemDataList[i] != saveStoreItemList[i].mergeItemData)
                {
                    AssetSystem.Instance.DestoryGameObject(Consts.StoreItem, saveStoreItemList[i].gameObject);
                    saveStoreItemList.Remove(saveStoreItemList[i]);

                    GameObject itemRoot = AssetSystem.Instance.Instantiate(Consts.StoreItem, grid_Store.transform);
                    ShowStoreItemTween(itemRoot.transform);
                    itemRoot.transform.SetSiblingIndex(i);
                    GameObject go = AssetSystem.Instance.Instantiate(itemDataList[i].PrefabName, itemRoot.transform);
                    go.transform.localScale *= 0.8f;
                    StoreItem item = itemRoot.GetComponent<StoreItem>();
                    if (item != null)
                    {
                        saveStoreItemList.Insert(i, item);
                    }
                }
            }
        }
        //刷新StoreItem数据
        for (int i = 0; i < saveStoreItemList.Count; i++)
        {
            saveStoreItemList[i].InitItem(i, itemDataList[i]);
        }
    }
    private void ClearBottomStore() 
    {
        for (int i = 0; i < saveStoreItemList.Count; i++)
        {
            Destroy(saveStoreItemList[i].gameObject);
        }
        saveStoreItemList.Clear();
    }
    private void StoreRootTween()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(mergeController.StoreTrans.DOScale(1.15f, 0.2f).SetEase(Ease.OutQuad));
        sequence.Insert(0.2f, mergeController.StoreTrans.DOScale(1f, 0.2f).SetEase(Ease.OutQuad));
        sequence.Play();
    }
    private void ShowStoreItemTween(Transform transform)
    {
        RectTransform rect = transform.GetComponent<RectTransform>();
        transform.transform.localScale = Vector3.zero;
        transform.localScale = Vector3.zero;
        rect.sizeDelta = new Vector2(0, 150);

        var s = DOTween.Sequence();
        s.Append(transform.DOScale(1f, 0.4f));
        s.Insert(0, rect.DOSizeDelta(new Vector2(120, 150), 0.4f));
        s.onComplete += () =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(grid_Store.GetComponent<RectTransform>());
        };
    }

    #endregion

    #region 顶部提示条相关
    float moveDistance = 0;
    Sequence tweenTask;
    bool isShowCompleteTask = false;
    Sequence tweenCD;
    bool isShowClearCD = false;
    private void InitTweenPos()
    {
        moveDistance = 1600;
        if (trans_ShowCompleteTask != null)
        {
            trans_ShowCompleteTask.gameObject.SetActive(true);
            trans_ShowCompleteTask.transform.localPosition = new Vector2(moveDistance, trans_ShowCompleteTask.transform.localPosition.y);
        }
        if (trans_ShowClearCD != null)
        {
            trans_ShowClearCD.gameObject.SetActive(true);
            trans_ShowClearCD.transform.localPosition = new Vector2(moveDistance, trans_ShowClearCD.transform.localPosition.y);
        }
        RefreshCompleteTask();
    }
    private string conpleteTaskId = string.Empty;
    private Dictionary<GameObject, string> saveTaskRewardDic = new Dictionary<GameObject, string>();
    private void RefreshCompleteTask()
    {
        foreach (var item in saveTaskItemDic)
        {
            if (!item.Value.IsCompleteTaskBeforeRefresh() && item.Value.IsCompleteTask())
            {
                conpleteTaskId = item.Value.taskId;
                TryKillTweenTask();
                TryKillTweenCD();
                //动画
                float timer = 0f;
                DOTween.To(() => group_ShowCommonInfo.alpha, (alpha) => { group_ShowCommonInfo.alpha = alpha; }, 0, 0.5f);
                tweenTask = DOTween.Sequence();
                tweenTask.Append(trans_ShowCompleteTask.transform.DOLocalMoveX(0, 0.5f).SetEase(Ease.OutBack));
                AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.taskFinishFlyNear);
                isShowCompleteTask = true;
                //ui
                taskHeadIcon.sprite = item.Value.ReturnHeadIcon();
                t_ShowTaskDesc.text = I2.Loc.ScriptLocalization.Get(item.Value.ReturndDalogueText());

                t_CompleteTask.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Orders_Button3");
                foreach (var rewardItem in saveTaskRewardDic)
                {
                    AssetSystem.Instance.DestoryGameObject(rewardItem.Value, rewardItem.Key);
                }
                saveTaskRewardDic.Clear();
                List<MergeRewardItem> rewardList = item.Value.ReturnRewardItem();
                for (int i = 0; i < rewardList.Count; i++)
                {
                    GameObject gO = AssetSystem.Instance.Instantiate(rewardList[i].ShowRewardPrefabName, group_TaskReward.transform);
                    gO.transform.localScale = Vector3.one * 0.55f;
                    if (gO)
                    {
                        saveTaskRewardDic.Add(gO, rewardList[i].ShowRewardPrefabName);
                    }
                }
                //List<Sprite> spriteList = item.Value.ReturnNeedItemIcons();
                //for (int i = 0; i < spriteList.Count; i++)
                //{
                //    needTaskTtemImage[i].transform.parent.gameObject.SetActive(true);
                //    needTaskTtemImage[i].sprite = spriteList[i];
                //    needItemNums[i].text = item.Value.t_ItemNum[i].text;
                //}
                //if (spriteList.Count == 1)
                //{
                //    needTaskTtemImage[1].transform.parent.gameObject.SetActive(false);
                //}
                UIPanel_TopBanner.refreshBanner?.Invoke();
                break;//防止同时完成两个任务
            }
        }
    }
    public override void TryTweenTaskBack()
    {
        if (isShowCompleteTask)
        {
            tweenTask.Kill();
            tweenTask = DOTween.Sequence();
            tweenTask.Append(trans_ShowCompleteTask.transform.DOLocalMoveX(moveDistance, 0.5f).SetEase(Ease.OutBack));
            tweenTask.OnComplete(() =>
            {
                isShowCompleteTask = false;
                DOTween.To(() => group_ShowCommonInfo.alpha, (alpha) => { group_ShowCommonInfo.alpha = alpha; }, 1, 0.5f);
            });
        }

    }
    private void CompleteBtnClick()
    {
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine
            && TaskGoalsDefinition.TaskDefinitionsDict.TryGetValue(conpleteTaskId, out TaskData taskData))
        {
            AnalyticsUtil.TrackEvent("complete_orders", new Dictionary<string, string>() {
                { "label",taskData.taskDefinition.levelIndex.ToString()},
                 { "catalog",conpleteTaskId},
            });
            //发放奖励
            GameManager.Instance.GiveRewardItem(taskData.taskDefinition.rewardItemList, "CompleteTask:" + conpleteTaskId, Vector3.zero);
            //删除任务数据
            TaskGoalsManager.Instance.RemoveTaskfromList(conpleteTaskId);
            //删除合成界面完成任务所需要的item
            MergeLevelManager.Instance.RemoveDateFromMap(MergeLevelManager.Instance.CurrentLevelType, taskData.taskDefinition.needItemList, true);
            //刷新合成界面订单ui
            MergeController.CurrentController.DestroyTaskItemById(conpleteTaskId);
            MergeController.CurrentController.RefreshMergeItemByCompleteTask();
            if (TaskGoalsManager.Instance.GetLeftTaskCount() == 0) 
            {
                OpenTaskBtnClick();
            }
        }
        else if ((MergeLevelManager.Instance.IsBranch(MergeLevelManager.Instance.CurrentLevelType) || MergeLevelManager.Instance.IsFestivalBranch(MergeLevelManager.Instance.CurrentLevelType))
            && BranchDefinition.allBranchdefDic.TryGetValue(MergeLevelManager.Instance.CurrentLevelType, out Dictionary<string, BranchDefinition> defDic)
            && defDic.TryGetValue(conpleteTaskId, out BranchDefinition branchDefinition))
        {
            //发放奖励
            GameManager.Instance.GiveRewardItem(branchDefinition.rewardItemList, "CompleteBranck", Vector3.zero);
            //刷新数据
            if (MergeLevelManager.Instance.IsBranch(MergeLevelManager.Instance.CurrentLevelType))
            {
                BranchSystem.Instance.CompleteTask(conpleteTaskId);
            }
            else
            {
                FestivalSystem.Instance.CompleteTaskById(MergeLevelManager.Instance.CurrentLevelType, conpleteTaskId);
            }
            MergeLevelManager.Instance.RemoveDateFromMap(MergeLevelManager.Instance.CurrentLevelType, branchDefinition.needItemList, true);
            //刷新合成界面订单ui
            MergeController.CurrentController.DestroyTaskItemById(conpleteTaskId);
            MergeController.CurrentController.RefreshMergeItemByCompleteTask();
        }
        TryTweenTaskBack();
    }
    private void RefreshShowCD(string cost, bool needShowCD)
    {
        if (needShowCD)
        {
            //if (isShowClearCD)
            //    return;
            //动画
            TryKillTweenTask();
            TryKillTweenCD();
            DOTween.To(() => group_ShowCommonInfo.alpha, (alpha) => { group_ShowCommonInfo.alpha = alpha; }, 0, 0.5f);
            tweenCD = DOTween.Sequence();
            tweenCD.Append(trans_ShowClearCD.transform.DOLocalMoveX(0, 0.5f).SetEase(Ease.OutBack));
            isShowClearCD = true;
            //ui
            t_ShowClearCDDesc.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/WatchVideo_Text2");
            t_ClearCDbyGem.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/WatchVideo_Text3");
#if !UNITY_EDITOR
                adReduceTime = RemoteConfigSystem.Instance.GetRemoteConfig_Int(RemoteConfigSystem.remoteKey_LoopCD_ReduceTime);
#endif
            if (adReduceTime <= 0)
                adReduceTime = 900;
            t_ClearCDbyAD.text = string.Format(I2.Loc.ScriptLocalization.Get("Obj/Time/Show5"), (adReduceTime / 60).ToString());
            t_ClearCDbyAD_Not.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/WatchVideo_Text1");
            RefreshWatchADBtn();
        }
        else
        {
            if (!isShowCompleteTask)
            {
                isShowClearCD = false;
                DOTween.To(() => group_ShowCommonInfo.alpha, (alpha) => { group_ShowCommonInfo.alpha = alpha; }, 1, 0.5f);
                tweenCD = DOTween.Sequence();
                tweenCD.Append(trans_ShowClearCD.transform.DOLocalMoveX(moveDistance, 0.5f).SetEase(Ease.OutBack));
            }
        }
    }
    private void RefreshWatchADBtn()
    {
        bool canWatchAD = AdManager.CanShowAD_Normal(AdManager.ADTag.ClearItemCD);
        btn_ClearCDbyAD.gameObject.SetActive(canWatchAD);
        btn_ClearCDbyAD_Not.gameObject.SetActive(!canWatchAD);
    }
    private void TryKillTweenTask()
    {
        if (isShowCompleteTask)
        {
            tweenTask.Kill();
            trans_ShowCompleteTask.transform.localPosition = new Vector2(moveDistance, trans_ShowCompleteTask.transform.localPosition.y);
            isShowCompleteTask = false;
        }
    }
    private void TryKillTweenCD()
    {
        if (isShowClearCD)
        {
            tweenCD.Kill();
            trans_ShowClearCD.transform.localPosition = new Vector2(moveDistance, trans_ShowClearCD.transform.localPosition.y);
            isShowClearCD = false;
        }
    }

    #endregion

    #region 泡泡区域
    private void CheckBubbleZone()
    {
        BubbleZoneMaxY = (int)bubbleZoneTop.localPosition.y - 80;
        BubbleZoneMinY = (int)bubbleZoneBottom.localPosition.y + 80;
    }
    #endregion
}
