using Coffee.UIExtensions;
using DG.Tweening;
using ivy.game;
using IvyCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 物品动画类型
/// </summary>
public enum MergeItemTweenType
{
    CreateByFlying,
    CreateByScale,
    Select,
    Flying,
    ChargeTween,
    BoxJump,
    CanMergeTween,
    OpenPack,
    BubbleFloat,//泡泡浮动
    BoxExpand,
    FlyingFromStore
}

public class MergeItem : Selectable, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform spriteRootTran;
    [HideInInspector] public GameObject mainIconSprite;//主预制体位置
    private Vector3 localScale = new Vector3(1.1f, 1.1f, 1f);
    private Vector3 storeScale = new Vector3(0.8f, 0.8f, 1f);

    //周围九宫格内Item信息（不包括自己）
    public List<MergeItemData> aroundItemData
    {
        get
        {
            if (ItemData == null)
                return null;
            ItemData.CalculateAroundGridItem(out List<MergeItemData> list);
            return list;
        }
    }

    private UIMergeBase uiMergeBase;
    private MergeController mergeController;

    /// <summary>
    /// 物品数据
    /// </summary>
    public MergeItemData ItemData { get; private set; }

    //Tween
    private bool IsBoxJump = false;
    private bool IsPlayCharge = false;
    private bool IsCanMove = true;

    // 自身动画
    private Dictionary<MergeItemTweenType, ItemTweenSequence> sequenceDict = new Dictionary<MergeItemTweenType, ItemTweenSequence>();

    public void InitItemInfo(UIMergeBase mergeUI, MergeController mergeSystem, MergeItemData data)
    {
        if (data == null || data.Definition == null)
        {
            GameDebug.LogError("set MergeItemData  is null");
            return;
        }
        this.uiMergeBase = mergeUI;
        this.mergeController = mergeSystem;

        enabled = true;
        ItemData = data;
        data.SetData_ItemGO(this);

        StartCoroutine(WaitToRefreshAllStates());
        transform.localScale = localScale;

    }

    private IEnumerator WaitToRefreshAllStates()
    {
        yield return null;
        RefreshAllStates();
    }

    public void RefreshAllStates()
    {
        RefreshBoxState();
        RefreshLockedState();
        RefreshTaskCheckIcon();
        RefreshChargeAndClockState();
        RefreshSpawnEffect();
        RefreshSpinEffect();
        RefreshBubbleState();
        RefreshSpeedUpEffect();
        RefreshMaxLevelState();
    }

    #region 蛛网状态
    private GameObject _LockedWidget;
    private void RefreshLockedState()
    {
        SetActiveLockedWidget(ItemData.IsLocked && !ItemData.IsInBox);
    }

    private void SetActiveLockedWidget(bool active)
    {
        if (_LockedWidget == null && active)
            _LockedWidget = AssetSystem.Instance.Instantiate(Consts.ItemState_Locked, spriteRootTran);
        if (_LockedWidget != null)
        {
            if (active)
            {
                if (!_LockedWidget.activeSelf)
                    _LockedWidget.SetActive(true);
            }
            else if (_LockedWidget.activeSelf)
                _LockedWidget.SetActive(false);
        }
    }

    // 打开蛛网
    public void ShowLockedOpenEffect(bool unspawnInFinish = false)
    {
        ////AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.iceBreak);
        StartCoroutine(Coroutine_LockedOpen(unspawnInFinish));
    }

    private IEnumerator Coroutine_LockedOpen(bool unspawnInFinish)
    {
        if (sequenceDict.TryGetValue(MergeItemTweenType.OpenPack, out var tween) && tween != null)
        {
            if (tween.sequence != null)
                tween.sequence.Kill(true);
        }
        else
            tween = new ItemTweenSequence();

        if (unspawnInFinish)
        {
            tween.sequence = DOTween.Sequence();
            sequenceDict[MergeItemTweenType.OpenPack] = tween;
            mainIconSprite.transform.localScale = Vector3.one;
            tween.sequence.Append(mainIconSprite.transform.DOScale(Vector3.one * 0.01f, 0.25f).SetEase(Ease.InQuad));
            tween.sequence.Play();
            yield return new WaitForSeconds(0.3f);
        }
        else
        {
            tween.sequence = DOTween.Sequence();
            sequenceDict[MergeItemTweenType.OpenPack] = tween;
            mainIconSprite.transform.localScale = Vector3.one;
            tween.sequence.Append(mainIconSprite.transform.DOScale(Vector3.one * 0.6f, 0.15f).SetEase(Ease.OutSine))
                .Append(mainIconSprite.transform.DOScale(Vector3.one, 0.18f).SetEase(Ease.OutBack));
            tween.sequence.Play();
            yield return new WaitForSeconds(0.15f);
        }

        if (unspawnInFinish)
        {
            if (tween != null && tween.sequence != null)
                tween.sequence.Kill(true);
            mainIconSprite.transform.localScale = Vector3.one;
            AssetSystem.Instance.UnspawnMergeItem(this);
        }
        else
            RefreshAllStates();
    }
    #endregion

    #region Item死亡特效
    private GameObject _ItemDeadEffect;
    public void ShowItemDeadEffect()
    {
        RefreshAllStates();
        if (_ItemDeadEffect == null)
            _ItemDeadEffect = AssetSystem.Instance.Instantiate(Consts.ItemDead_Effect, spriteRootTran);
        if (_ItemDeadEffect != null)
            StartCoroutine(Coroutine_ItemDeadEffect());
    }
    private IEnumerator Coroutine_ItemDeadEffect()
    {
        _ItemDeadEffect.SetActive(true);
        if (_ItemDeadEffect.TryGetComponent<Coffee.UIExtensions.UIParticle>(out var particle))
        {
            particle.Play();
            mainIconSprite.SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        mainIconSprite.SetActive(true);
        _ItemDeadEffect.SetActive(false);
        AssetSystem.Instance.UnspawnMergeItem(this);
    }
    #endregion

    #region 箱子状态
    private GameObject _BoxWidget;
    private GameObject _BoxBrokeEffect;
    private void RefreshBoxState()
    {
        SetActiveBoxWidget(ItemData.IsInBox);
        if (_BoxBrokeEffect != null)
            _BoxBrokeEffect.SetActive(false);
    }

    private void SetActiveBoxWidget(bool active)
    {
        if (_BoxWidget == null && active)
        {
            string boxPrefab = "ItemState_Box" + UnityEngine.Random.Range(1, 4);
            _BoxWidget = AssetSystem.Instance.Instantiate(boxPrefab, transform);
        }
        if (_BoxWidget != null)
        {
            if (active)
            {
                if (!_BoxWidget.activeSelf)
                    _BoxWidget.SetActive(true);
                spriteRootTran.gameObject.SetActive(false);
            }
            else
            {
                if (_BoxWidget.activeSelf)
                    _BoxWidget.SetActive(false);
                spriteRootTran.gameObject.SetActive(true);
            }
        }
    }

    public void ShowBoxBrokeEffect()
    {
        RefreshAllStates();
        if (_BoxBrokeEffect == null)
            _BoxBrokeEffect = AssetSystem.Instance.Instantiate(Consts.ItemBoxBroke_Effect, spriteRootTran);
        if (_BoxBrokeEffect != null)
            StartCoroutine(Coroutine_BoxBrokeEffect());
    }
    private IEnumerator Coroutine_BoxBrokeEffect()
    {
        ////AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.boxBreak);
        _BoxBrokeEffect.SetActive(true);
        if (_BoxBrokeEffect.TryGetComponent<Coffee.UIExtensions.UIParticle>(out var particle))
            particle.Play();
        yield return new WaitForSeconds(0.8f);
        _BoxBrokeEffect.SetActive(false);
    }
    #endregion

    #region 主线/副本任务图标
    private GameObject _TaskCheckWidget;
    private GameObject _DailyTaskCheckWidget;
    public void RefreshTaskCheckIcon()
    {
        bool showTaskCheck = false;
        bool showDailyTaskCheck = false;
        if (ItemData != null && !ItemData.IsInBox && !ItemData.IsInBubble && !ItemData.IsLocked)
        {
            if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine)
            {
                List<string> taskIdList = TaskGoalsManager.Instance.leftTaskIdList;
                for (int i = 0; i < taskIdList.Count; i++)
                {
                    if (TaskGoalsDefinition.TaskDefinitionsDict.TryGetValue(taskIdList[i], out TaskData taskData))
                    {
                        for (int j = 0; j < taskData.taskDefinition.needItemList.Count; j++)
                        {
                            if (ItemData.PrefabName == taskData.taskDefinition.needItemList[j].ShowRewardPrefabName)
                            {
                                showTaskCheck = true;
                                break;
                            }
                        }
                    }
                    if (showTaskCheck)
                        break;
                }
                if (!showTaskCheck) //优先显示task标志
                {
                    foreach (var item in DailyTaskSystem.Instance.dailyTaskLocationData.Values)
                    {
                        if (!item.hasClaim
                            && DailyTaskDefinition.dailyTaskDefinitionDic.TryGetValue(item.dailyTaskId, out DailyTaskDefinition dailyTaskDefinition)
                            && dailyTaskDefinition.taskType == DailyTaskType.Collect)
                        {
                            if (ItemData.PrefabName == dailyTaskDefinition.targetPrefab)
                            {
                                showDailyTaskCheck = true;
                                break;
                            }
                        }
                    }
                }
            }
            else if (MergeLevelManager.Instance.IsBranch(MergeLevelManager.Instance.CurrentLevelType)  || MergeLevelManager.Instance.IsFestivalBranch(MergeLevelManager.Instance.CurrentLevelType))
            {
                List<string> taskIdList = new List<string>();
                if (MergeLevelManager.Instance.IsBranch(MergeLevelManager.Instance.CurrentLevelType))
                {
                    taskIdList = BranchSystem.Instance.CurTaskList;
                }
                else
                {
                    taskIdList = FestivalSystem.Instance.ReturnCurTaskListByType(MergeLevelManager.Instance.CurrentLevelType);
                }
                if (BranchDefinition.allBranchdefDic.TryGetValue(MergeLevelManager.Instance.CurrentLevelType, out Dictionary<string, BranchDefinition> defDic))
                {
                    for (int i = 0; i < taskIdList.Count; i++)
                    {
                        if (defDic.TryGetValue(taskIdList[i], out BranchDefinition branchDefinition))
                        {
                            for (int j = 0; j < branchDefinition.needItemList.Count; j++)
                            {
                                if (ItemData.PrefabName == branchDefinition.needItemList[j].ShowRewardPrefabName)
                                {
                                    showTaskCheck = true;
                                    break;
                                }
                            }
                            if (showTaskCheck)
                                break;
                        }
                    }
                }
            }
            else if (MergeLevelManager.Instance.IsDailyActive(MergeLevelManager.Instance.CurrentLevelType)) 
            {
                if (DailyDefinition.DailyDefDic.TryGetValue(MergeLevelManager.Instance.CurrentLevelType, out DailyDefinition dailyDefinition))
                {
                    for (int i = 0; i < dailyDefinition.AmiMergeItems.Count; i++)
                    {
                        if (ItemData.PrefabName == dailyDefinition.AmiMergeItems[i].ShowRewardPrefabName)
                        {
                            showTaskCheck = true;
                            break;
                        }
                    }
                }
            }
            else //副本
            {
                if (DungeonDefinition.DungeonDefDic.TryGetValue(MergeLevelManager.Instance.CurrentLevelType, out DungeonDefinition dungeonDefinition))
                {
                    for (int i = 0; i < dungeonDefinition.AmiMergeItems.Count; i++)
                    {
                        if (ItemData.PrefabName == dungeonDefinition.AmiMergeItems[i].ShowRewardPrefabName)
                        {
                            showTaskCheck = true;
                            break;
                        }
                    }
                }
            }
        }

        SetActiveTaskWidget(showTaskCheck);
        SetActiveDailyTaskWidget(showDailyTaskCheck);
    }

    private void SetActiveTaskWidget(bool active)
    {
        if (_TaskCheckWidget == null && active)
            _TaskCheckWidget = AssetSystem.Instance.Instantiate(Consts.ItemState_Task, spriteRootTran, new Vector3(40, -40, 0), Vector3.zero, Vector3.one);
        if (_TaskCheckWidget != null)
        {
            if (active)
            {
                if (!_TaskCheckWidget.activeSelf)
                    _TaskCheckWidget.SetActive(true);
            }
            else if (_TaskCheckWidget.activeSelf)
                _TaskCheckWidget.SetActive(false);
        }
    }
    private void SetActiveDailyTaskWidget(bool active)
    {
        if (_DailyTaskCheckWidget == null && active)
            _DailyTaskCheckWidget = AssetSystem.Instance.Instantiate(Consts.ItemState_DailyTask, spriteRootTran, new Vector3(40, -40, 0), Vector3.zero, Vector3.one);
        if (_DailyTaskCheckWidget != null)
        {
            if (active)
            {
                if (!_DailyTaskCheckWidget.activeSelf)
                    _DailyTaskCheckWidget.SetActive(true);
            }
            else if (_DailyTaskCheckWidget.activeSelf)
                _DailyTaskCheckWidget.SetActive(false);
        }
    }
    public bool NeedFixSellView()
    {
        if ((_TaskCheckWidget != null && _TaskCheckWidget.activeSelf)
            || (_DailyTaskCheckWidget != null && _DailyTaskCheckWidget.activeSelf))
            return true;
        else
            return false;

    }
    #endregion

    #region 充能及倒计时效果
    private GameObject _ChargeOverWidget;
    private Slider _ClockWidget;
    public void RefreshChargeAndClockState()
    {
        if (ItemData.IsInBox || ItemData.IsLocked || ItemData.IsInBubble)
        {
            SetActiveChargeWidget(false);
            SetActiveClockWidget(false);
            return;
        }

        switch (ItemData.Definition.CategoryType)
        {
            case MergeItemCategoryType.container:
            case MergeItemCategoryType.dualSkills:
                {
                    //刷新倒计时
                    ItemData.TryRefreshChargeDate_ByInfiniteContainer();
                    if (ItemData.chargeRemainUseTimes > 0 || ItemData.totalChargedCount > 0)
                    {
                        SetActiveClockWidget(false);
                        SetActiveChargeWidget(ItemData.Definition.NeedShowChargeIcon);
                    }
                    else
                    {
                        SetActiveClockWidget(true,
                            (float)ExtensionTool.RemainTime(ItemData.ChargeFinishDate).TotalSeconds / ItemData.Definition.ChargeLoopCDSecond);
                        SetActiveChargeWidget(false);
                    }
                }
                break;
            case MergeItemCategoryType.boxed:
                {
                    if (ItemData.chargeRemainUseTimes > 0 || ItemData.totalChargedCount > 0)
                    {
                        SetActiveClockWidget(false);
                        SetActiveChargeWidget(ItemData.Definition.NeedShowChargeIcon);
                    }
                    else if (ItemData.Definition.ChargeLoopCDSecond > 0)
                    {
                        ItemData.TryRefreshChargeDate_ByInfiniteContainer();
                        SetActiveClockWidget(true,
                            (float)ExtensionTool.RemainTime(ItemData.ChargeFinishDate).TotalSeconds / ItemData.Definition.ChargeLoopCDSecond);
                        SetActiveChargeWidget(false);
                    }
                }
                break;
            case MergeItemCategoryType.finiteContainer:
            case MergeItemCategoryType.taskBox:
                {
                    if (ItemData.Definition.BoxOpenTime > 0)//延迟宝箱
                    {
                        if (!ItemData.boxOpenDelayStart)//延迟宝箱还未开始计时打开
                        {
                            SetActiveClockWidget(false);
                            SetActiveChargeWidget(false);
                        }
                        else if (ItemData.IsInOpenDelay)//延迟宝箱处于CD状态
                        {
                            if (ItemData.boxOpenDelayEndTime > TimeManager.Instance.UtcNow())
                            {
                                SetActiveClockWidget(true,
                                    (float)ExtensionTool.RemainTime(ItemData.boxOpenDelayEndTime).TotalSeconds / ItemData.Definition.BoxOpenTime);
                                SetActiveChargeWidget(false);
                            }
                            else
                                ItemData.ClearCD();
                        }
                        else if (ItemData.chargeRemainUseTimes > 0 || ItemData.totalChargedCount > 0)
                        {
                            SetActiveClockWidget(false);
                            SetActiveChargeWidget(ItemData.Definition.NeedShowChargeIcon);
                        }
                    }
                    else if (ItemData.chargeRemainUseTimes > 0 || ItemData.totalChargedCount > 0)
                    {
                        SetActiveClockWidget(false);
                        SetActiveChargeWidget(ItemData.Definition.NeedShowChargeIcon);
                    }
                }
                break;
            case MergeItemCategoryType.modelSwitch:
                {
                    ItemData.CheckModelSwitch();
                    SetActiveClockWidget(false);
                    SetActiveChargeWidget(ItemData.Definition.NeedShowChargeIcon);
                }
                break;
            case MergeItemCategoryType.produced:
                {
                    //刷新倒计时
                    if (ItemData.chargeRemainUseTimes_Auto > 0 || ItemData.totalChargedCount_Auto > 0)
                    {
                        SetActiveClockWidget(false);
                        SetActiveChargeWidget(ItemData.Definition.NeedShowChargeIcon);
                    }
                    else
                    {
                        SetActiveClockWidget(true,
                            (float)ExtensionTool.RemainTime(ItemData.ChargeFinishDate_Auto).TotalSeconds / ItemData.Definition.ChargeLoopCDSecond_Auto);
                        SetActiveChargeWidget(false);
                    }
                }
                break;
            case MergeItemCategoryType.wake:
                {
                    if (ItemData.ChargeFinishDate <= TimeManager.Instance.UtcNow())//已唤醒状态
                    {
                        SetActiveClockWidget(false);
                        SetActiveChargeWidget(ItemData.Definition.NeedShowChargeIcon);
                    }
                    else
                    {
                        SetActiveClockWidget(true,
                            (float)ExtensionTool.RemainTime(ItemData.ChargeFinishDate).TotalSeconds / ItemData.Definition.ChargeLoopCDSecond);
                        SetActiveChargeWidget(false);
                    }
                }
                break;
            case MergeItemCategoryType.swallowC:
            case MergeItemCategoryType.swallowF:
                SetActiveChargeWidget(ItemData.Definition.NeedShowChargeIcon && !ItemData.canSwallow);
                break;
            case MergeItemCategoryType.swallowZ:
                {
                    if (ItemData.canSwallow)
                    {
                        SetActiveClockWidget(false);
                        SetActiveChargeWidget(false);
                    }
                    else
                    {
                        if (ItemData.chargeRemainUseTimes_Auto > 0 || ItemData.totalChargedCount_Auto > 0)
                        {
                            SetActiveClockWidget(false);
                            SetActiveChargeWidget(ItemData.Definition.NeedShowChargeIcon);
                        }
                        else
                        {
                            SetActiveClockWidget(true,
                                (float)ExtensionTool.RemainTime(ItemData.ChargeFinishDate_Auto).TotalSeconds / ItemData.Definition.ChargeLoopCDSecond_Auto);
                            SetActiveChargeWidget(false);
                        }
                    }
                }
                break;
            default:
                {
                    SetActiveClockWidget(false);
                    SetActiveChargeWidget(false);
                }
                break;
        }
    }

    private void SetActiveChargeWidget(bool active)
    {
        if (_ChargeOverWidget == null && active)
            _ChargeOverWidget = AssetSystem.Instance.Instantiate(Consts.ItemState_Charge, spriteRootTran);
        if (_ChargeOverWidget != null)
        {
            if (active)
            {
                if (!_ChargeOverWidget.activeSelf)
                    _ChargeOverWidget.SetActive(true);
                PlayTween_Charge();
            }
            else if (_ChargeOverWidget.activeSelf)
                _ChargeOverWidget.SetActive(false);
        }
    }

    private void SetActiveClockWidget(bool active, float sliderVal = 0f)
    {
        if (_ClockWidget == null && active)
        {
            GameObject gO = AssetSystem.Instance.Instantiate(Consts.ItemState_Clock, spriteRootTran);
            gO.transform.localPosition = new Vector3(47, 38, 0);
            _ClockWidget = gO.GetComponent<Slider>();
        }

        if (_ClockWidget != null)
        {
            if (active)
            {
                if (!_ClockWidget.gameObject.activeSelf)
                    _ClockWidget.gameObject.SetActive(true);
                _ClockWidget.value = sliderVal;
            }
            else if (_ClockWidget.gameObject.activeSelf)
                _ClockWidget.gameObject.SetActive(false);
        }
    }
    #endregion

    #region 生成效果
    private GameObject _SpawnEffect;
    private void RefreshSpawnEffect()
    {
        if (ItemData.IsInBubble || ItemData.IsInBox || ItemData.IsLocked)
            SetActiveSpawnEffect(false);
        else if (ItemData.Definition.NeedShowSpawnEffect && ItemData.CanSpawnItem_ByContainer())
            SetActiveSpawnEffect(true);
        else
            SetActiveSpawnEffect(false);
    }

    public void SetActiveSpawnEffect(bool active)
    {
        if (_SpawnEffect == null && active)
            _SpawnEffect = AssetSystem.Instance.Instantiate(Consts.SpawnItem_Effect, spriteRootTran);
        if (_SpawnEffect != null)
        {
            if (active)
            {
                if (!_SpawnEffect.activeSelf)
                    _SpawnEffect.SetActive(true);
            }
            else if (_SpawnEffect.activeSelf)
                _SpawnEffect.SetActive(false);
        }
    }
    #endregion

    #region 星闪效果
    private GameObject _SpinParticle;
    private void RefreshSpinEffect()
    {
        if (ItemData.IsInBubble || ItemData.IsInBox || ItemData.IsLocked)
            SetActiveSpinParticle(false);
        else if (ItemData.Definition.NeedShowParticle && ItemData.CanSpawnItem_ByContainer())
            SetActiveSpinParticle(true);
        else
            SetActiveSpinParticle(false);
    }

    public void SetActiveSpinParticle(bool active)
    {
        if (_SpinParticle == null && active)
            _SpinParticle = AssetSystem.Instance.Instantiate(Consts.ItemSpine_BoxParticle, spriteRootTran);
        if (_SpinParticle != null)
        {
            if (active)
            {
                if (!_SpinParticle.activeSelf)
                    _SpinParticle.SetActive(true);
            }
            else if (_SpinParticle.activeSelf)
                _SpinParticle.SetActive(false);
        }
    }
    #endregion

    #region 泡泡效果
    private UIParticle _BubbleWidget;
    private GameObject _BubbleBrokeEffect;
    bool showingBorkeEffect = false;
    private void RefreshBubbleState()
    {
        if (showingBorkeEffect)
            return;
        if (!ItemData.IsInBox && ItemData.IsInBubble)
        {
            SetActiveBubbleIcon(true);
            if (ExtensionTool.IsDateSmallerThanNow(ItemData.bubbleDieTime))
            {
                //倒计时结束未花钱解锁，产出金币并破灭消失
                MergeLevelManager.Instance.CurrentMapData.ChangeItemData(ItemData.GridPos, null);
                if (!string.IsNullOrEmpty(ItemData.Definition.BubbleDieOutputPrefab))
                {
                    mergeController.CreateItem_InGridDirectly(ItemData.Definition.BubbleDieOutputPrefab, ItemData.GridPos);
                }
                else if (ReferenceEquals(mergeController.currentSelectItemData, ItemData))
                {
                    mergeController.HideWithTween_BoxSelected();
                    mergeController.SetCurrentSelectData(null);
                }
                //破灭动画
                ShowBubbleBrokeEffect(true);
            }
        }
        else
            SetActiveBubbleIcon(false);

        if (_BubbleBrokeEffect != null)
            _BubbleBrokeEffect.SetActive(false);
    }

    private void SetActiveBubbleIcon(bool active)
    {
        if (_BubbleWidget == null && active)
        {
            var obj = AssetSystem.Instance.Instantiate(Consts.Bubble_Effect, spriteRootTran);
            _BubbleWidget = obj.GetComponent<UIParticle>();
            _BubbleWidget?.Play();
        }
        if (_BubbleWidget != null)
        {
            if (active)
            {
                if (!_BubbleWidget.gameObject.activeSelf)
                {
                    _BubbleWidget.gameObject.SetActive(true);
                    _BubbleWidget.Play();
                }
                //PlayTween_BubbleFloat();
            }
            else if (_BubbleWidget.gameObject.activeSelf)
            {
                _BubbleWidget.gameObject.SetActive(false);
                //StopTween_BubbleFloat();
            }
        }
    }

    public void ShowBubbleBrokeEffect(bool unspawnInFinish)
    {
        SetActiveBubbleIcon(false);
        if (_BubbleBrokeEffect == null)
            _BubbleBrokeEffect = AssetSystem.Instance.Instantiate(Consts.BubbleBroke_Effect, spriteRootTran);
        if (_BubbleBrokeEffect != null)
            StartCoroutine(Coroutine_BubbleBrokeEffect(unspawnInFinish));
        else
        {
            if (unspawnInFinish)
                AssetSystem.Instance.UnspawnMergeItem(this);
            else
                RefreshTaskCheckIcon();
        }
    }
    private IEnumerator Coroutine_BubbleBrokeEffect(bool unspawnInFinish)
    {
        showingBorkeEffect = true;
        ////AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.removePaoPao);
        if (unspawnInFinish)
            spriteRootTran.localScale = Vector3.zero;
        _BubbleBrokeEffect.SetActive(true);
        if (_BubbleBrokeEffect.TryGetComponent<Coffee.UIExtensions.UIParticle>(out var particle))
            particle.Play();
        yield return new WaitForSeconds(0.5f);
        _BubbleBrokeEffect.SetActive(false);
        if (unspawnInFinish)
            AssetSystem.Instance.UnspawnMergeItem(this);
        else
            RefreshTaskCheckIcon();
        showingBorkeEffect = false;
    }
    #endregion

    #region 九宫格加速效果
    private UIParticle _SpeedUpParticle;
    private UIParticle _BeAffectParticle;
    private void RefreshSpeedUpEffect()
    {
        if (ItemData.Definition.CategoryType == MergeItemCategoryType.booster)
        {
            SetActiveBeAffectParticle(false);
            if (ItemData.CheckBoosterDie())//九宫格加速道具死亡
            {
                MergeLevelManager.Instance.CurrentMapData.ChangeItemData(ItemData.GridPos, null);
                MergeLevelManager.Instance.CurrentMapData.AddOrRemoveItemData();
                AssetSystem.Instance.UnspawnMergeItem(this);

                if (ItemData.Definition.IsSpawnPrefabWithDie)
                {
                    mergeController.CreateItem_InGridDirectly(ItemData.Definition.DieCreatePrefab, ItemData.GridPos);
                }
                else if (ReferenceEquals(mergeController.currentSelectItemData, ItemData))
                {
                    mergeController.HideWithTween_BoxSelected();
                    mergeController.SetCurrentSelectData(null);
                }
            }
            else if (ItemData.IsBoosterStart())
                SetActiveSpeedUpParticle(true);
        }
        else
        {
            SetActiveSpeedUpParticle(false);
            //被加速效果
            SetActiveBeAffectParticle(ItemData.CheckAffectByBooster());
        }
    }

    private void SetActiveSpeedUpParticle(bool active)
    {
        if (_SpeedUpParticle == null && active)
        {
            var obj = AssetSystem.Instance.Instantiate(Consts.SpeedUpItem_Effect, spriteRootTran);
            _SpeedUpParticle = obj.GetComponent<UIParticle>();
            _SpeedUpParticle?.Play();
        }
        if (_SpeedUpParticle != null)
        {
            if (active)
            {
                if (!_SpeedUpParticle.gameObject.activeSelf)
                {
                    _SpeedUpParticle.gameObject.SetActive(true);

                }
                _SpeedUpParticle.Play();
            }
            else if (_SpeedUpParticle.gameObject.activeSelf)
                _SpeedUpParticle.gameObject.SetActive(false);
        }
    }

    private void SetActiveBeAffectParticle(bool active)
    {
        if (_BeAffectParticle == null && active)
        {
            var obj = AssetSystem.Instance.Instantiate(Consts.SpeedUpItem_BeAffect, spriteRootTran);
            _BeAffectParticle = obj.GetComponent<UIParticle>();
            _BeAffectParticle?.Play();
        }
        if (_BeAffectParticle != null)
        {
            if (active)
            {
                if (!_BeAffectParticle.gameObject.activeSelf)
                {
                    _BeAffectParticle.gameObject.SetActive(true);
                }
                _BeAffectParticle.Play();
            }
            else if (_BeAffectParticle.gameObject.activeSelf)
                _BeAffectParticle.gameObject.SetActive(false);
        }
    }
    #endregion

    #region item拖拽到背包位置特效
    bool isShowingItemOnBagEffect = false;
    private void RefreshDragItemOnBag(Vector3 curItemPos)
    {
        if (Vector2.Distance(curItemPos, mergeController.StoreTrans.position) <= 0.5f)
        {
            if (!isShowingItemOnBagEffect)
            {
                uiMergeBase.RefreshStoreEffect(true);
                isShowingItemOnBagEffect = true;
            }
        }
        else
        {
            if (isShowingItemOnBagEffect)
            {
                uiMergeBase.RefreshStoreEffect(false);
                isShowingItemOnBagEffect = false;
            }
        }
    }
    #endregion

    #region 背包返回合成界面特效
    ParticleSystem fromBagToMapEffect = null;
    public void RefreshBagToMapEffet()
    {
        if (fromBagToMapEffect == null)
        {
            GameObject go = AssetSystem.Instance.Instantiate(Consts.FromBagToMapEffect, spriteRootTran);
            if (go == null)
            {
                return;
            }
            fromBagToMapEffect = go.GetComponent<ParticleSystem>();
        }
        fromBagToMapEffect.Play();
        fromBagToMapEffect.transform.SetAsFirstSibling();
    }
    #endregion

    #region 清除CD特效
    private UIParticle _ClearCDEffect;
    public void PlayClearCDEffect()
    {
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Recharge);
        if (_ClockWidget != null && _ClockWidget.gameObject.activeSelf)
        {
            var clock = Instantiate(_ClockWidget.gameObject);
            clock.transform.SetParent(_ClockWidget.transform.parent, false);
            clock.transform.localPosition = _ClockWidget.transform.localPosition;
            DOTween.Sequence().Append(clock.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.OutSine))
                .Insert(0, clock.transform.DOScale(1.6f, 0.5f))
                .AppendInterval(0.2f)
                .AppendCallback(() =>
                {
                    if (_ClearCDEffect == null)
                    {
                        var obj = AssetSystem.Instance.Instantiate(Consts.ItemClearCD_Effect, transform);
                        _ClearCDEffect = obj.GetComponent<UIParticle>();
                    }
                    if (_ClearCDEffect != null)
                    {
                        if (!_ClearCDEffect.gameObject.activeSelf)
                            _ClearCDEffect.gameObject.SetActive(true);
                        _ClearCDEffect?.Play();
                    }
#if UNITY_EDITOR
                    DestroyImmediate(clock);
#else
                    Destroy(clock);
#endif
                });
        }
    }
    #endregion

    #region 达到最大等级 
    GameObject saveMaxLevelTag = null;
    private void RefreshMaxLevelState()
    {
        if (ItemData.IsInBox || ItemData.IsLocked)
            return;

        if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(ItemData.PrefabName, out MergeItemDefinition itemDefinition))
        {
            if (itemDefinition.IsMaxLevel && saveMaxLevelTag == null)
            {
                saveMaxLevelTag = AssetSystem.Instance.Instantiate(Consts.ItemState_MaxLevel, spriteRootTran, new Vector3(-40, -40, 0), Vector3.zero, Vector3.one);
            }
        }
    }
    #endregion
    private float countDownTimer;
    private void Update()
    {
        if (ItemData == null || ItemData.Definition == null)
        {
            return;
        }

        countDownTimer += Time.deltaTime;
        if (countDownTimer < 1f)
        {
            return;
        }
        countDownTimer -= 1f;

        TryAutoSpawn();
        RefreshSpinEffect();
        RefreshSpawnEffect();
        RefreshBubbleState();
        RefreshChargeAndClockState();
        RefreshSpeedUpEffect();

        if (ItemData.Definition.CategoryType == MergeItemCategoryType.finiteContainer
            || ItemData.Definition.CategoryType == MergeItemCategoryType.taskBox)
        {
            if (ItemData.Definition.BoxOpenTime > 0)
            {
                if (ItemData.boxOpenDelayEndTime == DateTimeOffset.MinValue && !ItemData.boxOpenDelayStart)
                    PlayTween_BoxJump();
            }
        }
    }

    protected override void OnDestroy()
    {
        StopAllCoroutines();
        StopPlayAllTween();
        base.OnDestroy();
    }

    public void BefroeUnspawn()
    {
        StopPlayAllTween();
        if (_ClearCDEffect != null)
            _ClearCDEffect.gameObject.SetActive(false);
    }


    #region 事件系统

    public void OnPointerClick(PointerEventData eventData)
    {
        if (UI_TutorManager.Instance.IsTutoring() && ItemData.GridPos != UI_TutorManager.Instance.curMergeClickPos)
            return;
        if (eventData.dragging)
        {
            return;
        }

        if (ItemData == null || ItemData.IsInBox || ItemData.Definition == null || uiMergeBase == null)
        {
            return;
        }

        PlayTween_Selected();
        mergeController.ResetTipCountDown();

        if (ItemData.Definition.CanSelect)
        {
            mergeController.ShowWithTween_BoxSelected(transform.position, null, true);

            if (mergeController.currentSelectItemData != null
                && mergeController.currentSelectItemData.GridPos == ItemData.GridPos
                && mergeController.currentSelectItemData.PrefabName == ItemData.PrefabName)
            {
                mergeController.OnClickSelectItem(ItemData);
            }
            else
            {
                AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Tap_Eliminate);
                mergeController.SetCurrentSelectData(ItemData);
            }
            mergeController.TryTweenTaskBack();
        }
        else
        {
            mergeController.SetCurrentSelectData(null);
        }
    }

    private static Vector2Int CurDraggingPos = Vector2Int.zero;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (UI_TutorManager.Instance.IsTutoring() && ItemData.GridPos != UI_TutorManager.Instance.curMergeDragStartPos)
            return;
        if (ItemData == null || ItemData.Definition == null || ItemData.IsLocked || ItemData.IsInBox || uiMergeBase == null)
        {
            return;
        }

        if (!ItemData.Definition.CanMove)
        {
            return;
        }
        if (CurDraggingPos != Vector2Int.zero)
            return;
        if (!IsCanMove)
            return;
        CurDraggingPos = ItemData.GridPos;
        GameDebug.Log("OnBeginDrag");
        transform.SetParent(mergeController.DragPanelTran, true);

        mergeController.ResetTipCountDown();
        mergeController.hasItemDragging = true;
        mergeController.HideWithTween_BoxSelected();
        mergeController.SetCurrentSelectData(ItemData);
        mergeController.TryTweenTaskBack();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (CurDraggingPos != ItemData.GridPos)
            return;
        if (UI_TutorManager.Instance.IsTutoring() && ItemData.GridPos != UI_TutorManager.Instance.curMergeDragStartPos)
            return;
        if (ItemData == null || ItemData.Definition == null || ItemData.IsLocked || ItemData.IsInBox || uiMergeBase == null)
        {
            return;
        }
        if (!ItemData.Definition.CanMove)
        {
            return;
        }

        mergeController.hasItemDragging = true;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector3 worldPos))
        {
            transform.position = worldPos;
        }

        //RefreshDragItemOnBag(worldPos);
        mergeController.MoveAboveMergeItem(this);
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine//只有主线关有背包
            && mergeController.StoreTrans != null && !ItemData.IsInBubble)
        {
            if (Math.Abs(worldPos.x - mergeController.StoreTrans.position.x) <= 1.5f && Math.Abs(worldPos.y - mergeController.StoreTrans.position.y) <= 0.4f)
            {
                transform.localScale = storeScale;
            }
            else
            {
                transform.localScale = localScale;
            }
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (CurDraggingPos == ItemData.GridPos)
            CurDraggingPos = Vector2Int.zero;
        else
            return;
        if (UI_TutorManager.Instance.IsTutoring() && ItemData.GridPos != UI_TutorManager.Instance.curMergeDragStartPos)
            return;
        if (ItemData == null || ItemData.Definition == null || ItemData.IsLocked || ItemData.IsInBox || uiMergeBase == null)
        {
            return;
        }
        if (!ItemData.Definition.CanMove)
        {
            return;
        }
        GameDebug.Log("OnEndDrag");

        mergeController.hasItemDragging = false;
        transform.SetParent(mergeController.MapRoot.transform, true);

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector3 worldPos))
        {
            transform.position = worldPos;
        }

        //教学相关
        bool isDragToMergeTutorial = false;
        bool isDragToBagTutorial = false;
        if (UI_TutorManager.Instance.IsTutoring() && UI_TutorManager.Instance.curMergeDragStartPos != Vector2Int.zero)
        {
            if (UI_TutorManager.Instance.curMergeDragEndPos != Vector2Int.zero)
                isDragToMergeTutorial = true;
            else
                isDragToBagTutorial = true;
        }

        try
        {
            if (mergeController.ConvertLocalPositionToGrid(worldPos, out Vector2Int endgridPos))
            {
                if (isDragToBagTutorial)
                {
                    ReturnToSelfGridPos();
                    return;
                }
                if (isDragToMergeTutorial)
                {
                    if (UI_TutorManager.Instance.curMergeDragEndPos != endgridPos)
                    {
                        ReturnToSelfGridPos();
                        return;
                    }
                    else
                        UI_TutorManager.Instance.GoNextTutor();
                }

                mergeController.TryMergeItem(this, endgridPos);
                mergeController.RefreshMergeAboveEffect(false, Vector3.zero);
            }
            else if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine//只有主线关有背包
                && mergeController.StoreTrans != null && !ItemData.IsInBubble)//气泡物体不能移入背包
            {
                if (isDragToMergeTutorial)
                {
                    ReturnToSelfGridPos();
                    return;
                }

                uiMergeBase.RefreshStoreEffect(false);
                int storeGridCount = StoreManager.Instance.curMaxStoreGridCount;
                //背包数量充足
                if (Math.Abs(worldPos.x - mergeController.StoreTrans.position.x) <= 1.5f && Math.Abs(worldPos.y - mergeController.StoreTrans.position.y) <= 0.4f)
                {
                    if (storeGridCount > MergeLevelManager.Instance.CurrentMapData.storePackList.Count)
                    {
                        Transform storeRoot = mergeController.StoreTrans.Find("Scroll View/Viewport/Content");
                        int index = -1;
                        if (storeRoot != null)       //排序
                        {
                            int childNum = storeRoot.childCount;
                            for (int i = 0; i < childNum; i++)
                            {
                                Transform item = storeRoot.GetChild(i);
                                if (item.position.x > worldPos.x)
                                {
                                    index = i;
                                    break;
                                }
                            }
                        }
                        if (isDragToBagTutorial)
                            UI_TutorManager.Instance.GoNextTutor();
                        GameDebug.Log("移入背包");
                        mergeController.SetCurrentSelectData(null);
                        MergeLevelManager.Instance.CurrentMapData.ChangeItemData(ItemData.GridPos, null);
                        MergeLevelManager.Instance.CurrentMapData.AddToStorePackList(ItemData, index);
                        AssetSystem.Instance.UnspawnMergeItem(this);

                        uiMergeBase.RefreshBottomStore(index);
                        return;
                    }
                    else if (isDragToBagTutorial)
                    {
                        UI_TutorManager.Instance.ForceEndTutor();
                    }
                    else
                    {
                        TextTipSystem.Instance.ShowTip(worldPos, I2.Loc.ScriptLocalization.Get("Obj/code/Backpack/Text1"), TextTipColorType.Yellow);
                        transform.localScale = localScale;
                    }
                }
                ReturnToSelfGridPos();
            }
            else
            {
                ReturnToSelfGridPos();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Merge OnDragEnd Err!" + e);
        }
    }

    #endregion


    public void ReturnToSelfGridPos(bool showSelect = false)
    {
        if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(ItemData.GridPos, out var originGrid))
        {
            Vector3 originGridWorldPos = originGrid.GridGO.transform.position;
            PlayTween_Flying(originGridWorldPos, () =>
            {
                if (showSelect)
                {
                    mergeController.ShowWithTween_BoxSelected(originGridWorldPos);
                }
            });
        }
    }

    #region 动画
    //泡泡浮动动画
    private void PlayTween_BubbleFloat()
    {
        if (!sequenceDict.TryGetValue(MergeItemTweenType.BubbleFloat, out var tween))
        {
            tween = new ItemTweenSequence();

            tween.sequence = DOTween.Sequence();
            sequenceDict[MergeItemTweenType.BubbleFloat] = tween;
            spriteRootTran.localPosition = Vector3.zero;
            tween.sequence.Append(spriteRootTran.DOLocalMoveY(30, 1.0f).SetEase(Ease.InOutSine));
            tween.sequence.Append(spriteRootTran.DOLocalMoveY(0, 1.0f).SetEase(Ease.InOutSine));
            tween.sequence.SetLoops(-1);
            tween.sequence.Play();
        }
    }
    private void StopTween_BubbleFloat()
    {
        StopPlayTween(MergeItemTweenType.BubbleFloat);
        spriteRootTran.localPosition = Vector3.zero;
    }

    public class ItemTweenSequence
    {
        public Sequence sequence;
    }
    public void StopPlayTween(MergeItemTweenType mergeItemState)
    {
        if (sequenceDict.TryGetValue(mergeItemState, out var item))
        {
            if (item != null && item.sequence != null)
            {
                item.sequence.Kill(true);
            }
            sequenceDict.Remove(mergeItemState);
        }
    }

    public void StopPlayTweenCanMergeShader()
    {
        if (sequenceDict.TryGetValue(MergeItemTweenType.CanMergeTween, out var item))
        {
            if (item != null && item.sequence != null)
            {
                item.sequence.Rewind(true);
            }

            //UISprite sprites = mainIconSprite.GetComponentInChildren<UISprite>();
            //if (sprites != null)
            //{
            //    sprites.material = null;
            //}
        }
        SetActiveSpawnEffect(false);
    }

    public void StopPlayAllTween()
    {
        foreach (var item in sequenceDict)
        {
            if (item.Value != null && item.Value.sequence != null)
            {
                item.Value.sequence.Kill(true);
            }
        }
        sequenceDict.Clear();
    }

    public void PlayTween_CreateByFly(Vector3 targetWorldPos, bool deafaultJump = false)
    {
        if (sequenceDict.TryGetValue(MergeItemTweenType.CreateByFlying, out var tween) && tween != null)
        {
            if (tween.sequence != null)
            {
                tween.sequence.Kill();
            }
        }
        else
        {
            tween = new ItemTweenSequence();
        }

        interactable = false;

        tween.sequence = DOTween.Sequence();
        sequenceDict[MergeItemTweenType.CreateByFlying] = tween;

        transform.SetParent(mergeController.DragPanelTran);

        transform.localScale = Vector3.one;

        float dis = Vector3.Distance(targetWorldPos, transform.position);
        float durationTime = 0.4f + dis * 0.1f;
        Vector2 p0 = transform.position;
        Vector2 p2 = targetWorldPos;
        float jumpHight = 2f;
        if (!deafaultJump)
            jumpHight = dis;
        Vector2 p1 = new Vector2(Vector2.Lerp(p0, p2, 0.5f).x, transform.position.y + jumpHight);
        var posz = transform.position.z;
        tween.sequence.Append(DOTween.To(setter: value =>
        {
            Vector2 vector = DoAnimTools.Bezier(p0, p1, p2, value);
            transform.position = new Vector3(vector.x, vector.y, posz);
        }, startValue: 0, endValue: 1, duration: durationTime).SetEase(Ease.OutSine));

        tween.sequence.Insert(0, transform.DOScale(localScale, durationTime - 0.4f));

        tween.sequence.onComplete = () =>
        {
            transform.SetParent(mergeController.MapRoot.transform);
            transform.position = targetWorldPos;
            //transform.localScale = Vector3.one;
            interactable = true;
            //transform.localScale = localScale;
            mergeController.RefreshMergeItemByChangeItem();
        };
        tween.sequence.Play();

    }

    public void PlayTween_CreateByScale()
    {
        if (sequenceDict.TryGetValue(MergeItemTweenType.CreateByScale, out var tween) && tween != null)
        {
            if (tween.sequence != null)
            {
                tween.sequence.Kill();
            }
        }
        else
        {
            tween = new ItemTweenSequence();
        }

        tween.sequence = DOTween.Sequence();
        sequenceDict[MergeItemTweenType.CreateByScale] = tween;
        spriteRootTran.localScale = Vector3.one * 0.01f;
        tween.sequence.Append(spriteRootTran.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
        tween.sequence.Play();
        tween.sequence.onComplete = () =>
        {
            spriteRootTran.localScale = Vector3.one;
            transform.localScale = new Vector3(1.1f, 1.1f, 1);
        };
    }

    private UIParticle _timeBoosterParticle;
    public void PlayTween_BoxExpand(float delay)
    {
        if (ItemData == null || ItemData.Definition == null || ItemData.IsLocked || ItemData.IsInBox)
            return;

        if (sequenceDict.TryGetValue(MergeItemTweenType.BoxExpand, out var tween) && tween != null)
        {
            if (tween.sequence != null)
                tween.sequence.Kill();
        }
        else
            tween = new ItemTweenSequence();

        tween.sequence = DOTween.Sequence();
        sequenceDict[MergeItemTweenType.BoxExpand] = tween;
        tween.sequence.AppendInterval(delay);
        tween.sequence.AppendCallback(() =>
        {
            var obj = AssetSystem.Instance.Instantiate(Consts.TimeBoosterEffect, spriteRootTran);
            _timeBoosterParticle = obj.GetComponent<UIParticle>();
            _timeBoosterParticle?.Play();
        });
        tween.sequence.Append(spriteRootTran.DOScale(Vector3.one * 1.3f, 0.15f).SetEase(Ease.OutSine));
        tween.sequence.Append(spriteRootTran.DOScale(Vector3.one, 0.1f).SetEase(Ease.InSine));
        tween.sequence.Play();
        tween.sequence.onComplete = () =>
        {
            if (_timeBoosterParticle != null)
            {
                Destroy(_timeBoosterParticle.gameObject);
                _timeBoosterParticle = null;
            }
        };
    }

    /// <summary>
    /// 返回原位置
    /// </summary>
    public void PlayTween_FlyToOriginPos()
    {
        if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(ItemData.GridPos, out var originGrid))
        {
            Vector3 originGridWorldPos = originGrid.GridGO.transform.position;
            PlayTween_Flying(originGridWorldPos);
        }
        else
        {
            GameDebug.LogError("[PlayTween_FlyToOrigin] 找不到原来的格子位置");
        }
    }

    public void PlayTween_Flying(Vector3 targetWorldPos, Action completeCB = null, float durationScale = 1)
    {
        if (sequenceDict.TryGetValue(MergeItemTweenType.Flying, out var tween) && tween != null)
        {
            if (tween.sequence != null)
            {
                tween.sequence.Kill(true);
            }
        }
        else
        {
            tween = new ItemTweenSequence();
        }

        interactable = false;

        transform.SetParent(mergeController.DragPanelTran);

        float dis = Vector2.Distance(transform.position, targetWorldPos);
        float duration = Mathf.Max(0.2f, (dis / 160) * 0.2f * 0.4f * durationScale);
        tween.sequence = DOTween.Sequence();
        sequenceDict[MergeItemTweenType.Flying] = tween;

        tween.sequence.Append(transform.DOMove(targetWorldPos, duration).SetEase(Ease.OutQuad));
        tween.sequence.Play();

        tween.sequence.onComplete = () =>
        {
            transform.SetParent(mergeController.MapRoot.transform);
            transform.position = targetWorldPos;

            interactable = true;

            completeCB?.Invoke();
        };
    }

    public void PlayTween_FlyingFromStore(Vector3 startPos, Vector3 endPos, Action completeCB = null)
    {
        if (sequenceDict.TryGetValue(MergeItemTweenType.FlyingFromStore, out var tween) && tween != null)
        {
            if (tween.sequence != null)
                tween.sequence.Kill(true);
        }
        else
            tween = new ItemTweenSequence();

        interactable = false;
        transform.SetParent(mergeController.DragPanelTran);

        tween.sequence = DOTween.Sequence();
        sequenceDict[MergeItemTweenType.FlyingFromStore] = tween;

        Vector3 normal = (endPos - startPos).normalized;    //方向
        float distance = Vector3.Distance(startPos, endPos);
        Vector3 middlePos = normal * distance * 0.9f + startPos;
        tween.sequence.Append(transform.DOMove(middlePos, 0.6f).SetEase(Ease.OutQuad));
        tween.sequence.Append(transform.DOMove(endPos, 0.3f).SetEase(Ease.OutQuad));
        tween.sequence.Insert(0, transform.DOScale(1.6f, 0.4f).SetEase(Ease.OutBack));
        tween.sequence.Insert(0.4f, transform.DOScale(0.8f, 0.2f).SetEase(Ease.InQuad));
        tween.sequence.Insert(0.6f, transform.DOScale(1.1f, 0.3f).SetEase(Ease.OutBack));
        tween.sequence.Play();
        tween.sequence.onComplete = () =>
        {
            transform.SetParent(mergeController.MapRoot.transform);
            transform.position = endPos;
            interactable = true;
            completeCB?.Invoke();
        };
    }

    public void PlayTween_Selected()
    {
        if (sequenceDict.TryGetValue(MergeItemTweenType.Select, out var tween) && tween != null)
        {
            if (tween.sequence != null)
            {
                tween.sequence.Kill();
            }
        }
        else
        {
            tween = new ItemTweenSequence();
        }

        tween.sequence = DOTween.Sequence();
        sequenceDict[MergeItemTweenType.Select] = tween;
        spriteRootTran.localScale = Vector3.one;
        //tween.sequence.Append(spriteRootTran.DOScale(Vector3.one * 0.8f, 0.3f).SetEase(Ease.OutQuart));
        //tween.sequence.Append(spriteRootTran.DOScale(Vector3.one, 0.15f).SetEase(Ease.InQuart));

        tween.sequence.AppendCallback(() =>
        {
            PunchScale(spriteRootTran.gameObject, "Grabbed_ObjectAnim", 1.5f, 0.2f, 0.2f);
        }).AppendInterval(1.5f);

        tween.sequence.Play();
        tween.sequence.onComplete = () =>
        {
            spriteRootTran.localScale = Vector3.one;
        };
    }

    public static void PunchScale(GameObject drawnObject, string animationName, float time, float xGrowth, float yGrowth, bool resetToRestingScaleFirst = true)
    {
        if (!(drawnObject == null))
        {
            xGrowth *= drawnObject.transform.localScale.x;
            yGrowth *= drawnObject.transform.localScale.y;
            if (resetToRestingScaleFirst)
            {
                drawnObject.transform.localScale = Vector3.one;
            }
            iTween.PunchScale(drawnObject.gameObject, iTween.Hash("name", animationName, "islocal", true, "time", time, "x", xGrowth, "y", yGrowth));
        }
    }



    public void PlayTween_Charge()
    {
        if (IsPlayCharge)
        {
            return;
        }

        if (sequenceDict.TryGetValue(MergeItemTweenType.ChargeTween, out var tween) && tween != null)
        {
            if (tween.sequence != null)
            {
                return;
            }
        }
        else
        {
            tween = new ItemTweenSequence();
        }

        IsPlayCharge = true;

        tween.sequence = DOTween.Sequence();
        tween.sequence.SetLoops(-1);
        sequenceDict[MergeItemTweenType.ChargeTween] = tween;

        _ChargeOverWidget.transform.localPosition = Vector3.zero + Vector3.up * 20;
        tween.sequence.Append(_ChargeOverWidget.transform.DOLocalMoveY(-20, 1f).SetEase(Ease.InQuad));
        tween.sequence.Append(_ChargeOverWidget.transform.DOLocalMoveY(20, 1f).SetEase(Ease.OutQuad));

        tween.sequence.onComplete = () =>
        {
            _ChargeOverWidget.transform.localPosition = Vector3.zero + Vector3.up * 20;
            IsPlayCharge = false;
            if (mergeController != null)
                mergeController.SetCurrentSelectData(ItemData);
            //if (uiPanel_Merge != null)
            //    uiPanel_Merge.RefreshSelectItemInfo();
        };
        tween.sequence.onKill = () =>
        {
            IsPlayCharge = false;
        };
        tween.sequence.Play();
    }

    /// <summary>
    /// 有延迟时间的finiteContainer，在未开箱的情况下执行动画
    /// </summary>
    public void PlayTween_BoxJump()
    {
        if (IsBoxJump)
        {
            return;
        }
        if (sequenceDict.TryGetValue(MergeItemTweenType.BoxJump, out var tween) && tween != null)
        {
            if (tween.sequence != null)
            {
                tween.sequence.Kill();
            }
        }
        else
        {
            tween = new ItemTweenSequence();
        }


        tween.sequence = DOTween.Sequence();
        sequenceDict[MergeItemTweenType.BoxJump] = tween;

        IsBoxJump = true;

        float time = 1.5f;
        spriteRootTran.localScale = Vector3.one;
        spriteRootTran.localPosition = Vector3.zero;
        tween.sequence.Append(spriteRootTran.DOScaleX(1.15f, time / 8).SetEase(Ease.OutQuad));
        tween.sequence.Append(spriteRootTran.DOScaleX(1, time / 8).SetEase(Ease.OutQuad));
        tween.sequence.Insert(time / 8, spriteRootTran.DOLocalMoveY(30, time / 8).SetEase(Ease.OutQuad));
        tween.sequence.Append(spriteRootTran.DOLocalRotate(new Vector3(0, 0, 15), time / 8).SetEase(Ease.OutQuad));
        tween.sequence.Append(spriteRootTran.DOLocalRotate(new Vector3(0, 0, 0), time / 8).SetEase(Ease.OutQuad));
        tween.sequence.Append(spriteRootTran.DOLocalRotate(new Vector3(0, 0, 15), time / 8).SetEase(Ease.OutQuad));
        tween.sequence.Append(spriteRootTran.DOLocalRotate(new Vector3(0, 0, 0), time / 8).SetEase(Ease.OutQuad));
        tween.sequence.Append(spriteRootTran.DOScaleX(1.15f, time / 8).SetEase(Ease.OutQuad));
        tween.sequence.Append(spriteRootTran.DOScaleX(1, time / 8).SetEase(Ease.OutQuad));
        tween.sequence.Insert(time * 3 / 4, spriteRootTran.DOLocalMoveY(0, time / 8).SetEase(Ease.OutQuad));
        tween.sequence.AppendInterval(0.5f);

        tween.sequence.OnComplete(() =>
        {
            spriteRootTran.localScale = Vector3.one;
            spriteRootTran.localPosition = Vector3.zero;
            IsBoxJump = false;
        });
        tween.sequence.Play();
    }
    public void KillPlayTween_BoxJump()
    {
        if (sequenceDict.TryGetValue(MergeItemTweenType.BoxJump, out var tween) && tween != null)
        {
            if (tween.sequence != null)
            {
                tween.sequence.Kill();
                spriteRootTran.localScale = Vector3.one;
                spriteRootTran.localPosition = Vector3.zero;
                spriteRootTran.localRotation = Quaternion.Euler(Vector3.zero);
            }
        }
    }

    private static Material hsvMat;
    private Material currentMat;

    public void PlayTween_CanMerge()
    {
        transform.SetAsLastSibling();
        if (sequenceDict.TryGetValue(MergeItemTweenType.CanMergeTween, out var tween) && tween != null)
        {
            if (tween.sequence != null)
            {
                tween.sequence.Kill(true);
            }
        }
        else
        {
            tween = new ItemTweenSequence();
        }

        tween.sequence = DOTween.Sequence();
        sequenceDict[MergeItemTweenType.CanMergeTween] = tween;

        if (hsvMat == null)
        {
            hsvMat = Resources.Load<Material>("Materials/UISpriteHSVColor");
        }
        //UISprite sprites = mainIconSprite.GetComponentInChildren<UISprite>(true);
        //if (sprites != null)
        //{
        //    sprites.material = hsvMat;
        //    sprites.onRender += GetMainIconMat;
        //}

        tween.sequence.AppendInterval(0.1f);
        tween.sequence.Append(spriteRootTran.DOScale(1.15f, 0.375f).SetEase(Ease.OutQuad));
        tween.sequence.Insert(0.1f, spriteRootTran.DOLocalMoveY(30, 0.375f).SetEase(Ease.OutQuad));
        tween.sequence.Insert(0.1f, DOTween.To(value =>
       {
           if (currentMat != null)
           {
               currentMat.SetFloat("_Saturation", value);
               currentMat.SetFloat("_Value", value);
           }
       }, 1, 1.2f, 0.375f));

        tween.sequence.Insert(0.1f + 0.375f, spriteRootTran.DOScale(1, 0.375f).SetEase(Ease.InQuad));
        tween.sequence.Insert(0.1f + 0.375f, spriteRootTran.DOLocalMoveY(0, 0.375f).SetEase(Ease.InQuad));
        tween.sequence.Insert(0.1f + 0.375f, DOTween.To(value =>
        {
            if (currentMat != null)
            {
                currentMat.SetFloat("_Saturation", value);
                currentMat.SetFloat("_Value", value);
            }
        }, 1.2f, 0.9f, 0.375f));

        tween.sequence.Insert(0.1f + 0.75f, spriteRootTran.DOScale(1.15f, 0.375f).SetEase(Ease.OutQuad));
        tween.sequence.Insert(0.1f + 0.75f, spriteRootTran.DOLocalMoveY(30, 0.375f).SetEase(Ease.OutQuad));
        tween.sequence.Insert(0.1f + 0.75f, DOTween.To(value =>
        {
            if (currentMat != null)
            {
                currentMat.SetFloat("_Saturation", value);
                currentMat.SetFloat("_Value", value);
            }
        }, 0.9f, 1.2f, 0.375f));

        tween.sequence.Insert(0.1f + 1.125f, spriteRootTran.DOScale(1, 0.375f).SetEase(Ease.InQuad));
        tween.sequence.Insert(0.1f + 1.125f, spriteRootTran.DOLocalMoveY(0, 0.375f).SetEase(Ease.InQuad));
        tween.sequence.Insert(0.1f + 1.125f, DOTween.To(value =>
        {
            if (currentMat != null)
            {
                currentMat.SetFloat("_Saturation", value);
                currentMat.SetFloat("_Value", value);
            }
        }, 1.2f, 1, 0.375f));

        tween.sequence.onComplete = () =>
        {
            spriteRootTran.localScale = Vector3.one;
            spriteRootTran.localPosition = Vector3.zero;
        };
        tween.sequence.onKill = () =>
        {
            //if (sprites != null)
            //{
            //    sprites.onRender -= GetMainIconMat;
            //    sprites.material = null;
            //}
        };
        tween.sequence.Play();

    }
    public void KillPlayTween_CanMerge()
    {
        if (sequenceDict.TryGetValue(MergeItemTweenType.CanMergeTween, out var tween) && tween != null)
        {
            if (tween.sequence != null)
            {
                tween.sequence.Kill();
                spriteRootTran.localScale = Vector3.one;
                spriteRootTran.localPosition = Vector3.zero;
            }
        }
    }

    //被吞噬动作
    public void PlayTween_BeSwallow()
    {
        IsCanMove = false;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1.2f, 0.3f).SetEase(Ease.InQuad));
        sequence.Append(transform.DOScale(0.6f, 0.3f).SetEase(Ease.InQuad));
        sequence.AppendCallback(() =>
        {
            AssetSystem.Instance.UnspawnMergeItem(this);
            IsCanMove = true;
        });
    }
    //吞噬动作
    public GameObject devour; //吞噬特效
    public void PlayTween_Devour()
    {
        devour = AssetSystem.Instance.Instantiate(Consts.ItemMerge_Effect, transform);
        devour.SetActive(true);
        devour.transform.SetParent(transform);
        devour.transform.SetAsLastSibling();
        devour.transform.localPosition = Vector3.zero;
        devour.transform.localRotation = Quaternion.identity;
        devour.transform.localScale = Vector3.one;
        if (devour.transform.GetChild(0).TryGetComponent(out ParticleSystem particle))
        {
            particle.Play();
        }
    }

    private void GetMainIconMat(Material current)
    {
        currentMat = current;
    }

    #endregion

    /// <summary>
    /// 自产道具
    /// </summary>
    private void TryAutoSpawn()
    {
        if (ItemData == null || ItemData.IsInBox || ItemData.IsLocked || ItemData.IsInBubble || mergeController.IsRevoking())
            return;
        if (ItemData.Definition.CategoryType == MergeItemCategoryType.produced
            || ItemData.Definition.CategoryType == MergeItemCategoryType.dualSkills
            || ItemData.Definition.CategoryType == MergeItemCategoryType.swallowZ && !ItemData.canSwallow)
        {
            if (ItemData.chargeRemainUseTimes_Auto > 0 || ItemData.totalChargedCount_Auto > 0)
            {
                if (!string.IsNullOrEmpty(ItemData.Definition.autoSpawnPrefab))
                {
                    if (mergeController.TryGetEmptyGridAround(ItemData.GridPos, out var emptyGrid))
                    {
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Produce);
                        mergeController.CreateItem_FlyToGrid(ItemData.Definition.autoSpawnPrefab, transform.position, emptyGrid);
                        ItemData.SpawnOneItem_ByAutoContainer();
                        if (ItemData.Definition.NeedEnergy && mergeController.CanAffordEnergy(false))
                            mergeController.SpendEnergy();

                        if (ItemData.Definition.CategoryType == MergeItemCategoryType.swallowZ
                            && ItemData.IsSpawnFinish_BySwallowZ())
                        {
                            MergeLevelManager.Instance.CurrentMapData.ChangeItemData(ItemData.GridPos, null);
                            MergeLevelManager.Instance.CurrentMapData.AddOrRemoveItemData();
                            //销毁物体
                            AssetSystem.Instance.UnspawnMergeItem(this);

                            if (ItemData.Definition.IsSpawnPrefabWithDie)
                            {
                                mergeController.CreateItem_InGridDirectly(ItemData.Definition.DieCreatePrefab, ItemData.GridPos);
                            }
                            mergeController.HideWithTween_BoxSelected();
                            mergeController.SetCurrentSelectData(null);
                        }
                        else if (ReferenceEquals(mergeController.currentSelectItemData, ItemData))
                            mergeController.SetCurrentSelectData(ItemData);
                    }
                }
            }
            else
                ItemData.TryRefreshChargeDate_ByAutoContainer();
        }
    }
    #region 排序
    public void SetItemSibling(int index)
    {
        transform.SetSiblingIndex(index);
    }
    public void SetItemSibling(String s)
    {
        switch (s)
        {
            case "First": transform.SetAsFirstSibling(); break;
            case "Last": transform.SetAsLastSibling(); break;
            default:
                GameDebug.Log("输入的Sibling有误");
                break;
        }
    }
    public int GetItemSiblingIndexByPos()   //根据位置返回数值
    {
        int index = (ItemData.GridPos.x - 1) + (ItemData.GridPos.y - 1) * 7;
        return index;
    }
    #endregion
    Transform Task;
    public bool IsTaskItem()
    {
        Task = spriteRootTran.Find(Consts.ItemState_Task);
        if (Task != null && Task.gameObject.activeSelf)
        {
            return true;
        }
        return false;
    }
}
