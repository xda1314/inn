using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MergeItemData : System.ICloneable
{
    [JsonIgnore]
    public MergeItem ItemGO { get; private set; }
    [JsonIgnore]
    public BubbleItem BubbleGO { get; private set; }
    [JsonIgnore]
    public MergeItemDefinition Definition { get; private set; }



    #region 需要储存的数据
    public string PrefabName { get; private set; } //预制体名称，以这里为准（防止Definition为空）
    public Vector2Int GridPos { get; private set; }
    public bool IsLocked { get; private set; } = false;
    public bool IsInBox { get; private set; } = false;
    public bool IsInBubble { get; private set; } = false;
    public DateTimeOffset bubbleDieTime { get; private set; } = DateTimeOffset.MaxValue;

    public int chargeRemainUseTimes { get; private set; } //充能道具当前剩余使用次数
    public int totalChargedCount { get; private set; } //已充能次数
    private DateTimeOffset chargeFinishDate;

    //充能倒计时完成时间
    public DateTimeOffset ChargeFinishDate
    {
        get
        {
            return chargeFinishDate;
        }
        set
        {
            chargeFinishDate = value;
        }
    }

    public int chargeRemainUseTimes_Auto { get; private set; } //自产道具当前剩余使用次数
    public int totalChargedCount_Auto { get; private set; } //自产道具已充能次数
    public DateTimeOffset ChargeFinishDate_Auto { get; private set; } = DateTimeOffset.MaxValue; //自产充能倒计时完成时间

    public bool boxOpenDelayStart { get; private set; }//宝箱打开倒计时是否开启
    public DateTimeOffset boxOpenDelayEndTime { get; private set; }//宝箱打开延迟时间

    public int spawnCount { get; private set; }//产出次数
    public int autoSpawnCount { get; private set; }//自动产出次数
    public int poolSpawnCount { get; private set; }//通过池子产出的次数

    public bool canSwallow { get; private set; }//是否可进行吞噬
    public int swallowCount { get; private set; }//吞噬次数
    public Dictionary<string, int> curSwallowItemDic { get; private set; } = new Dictionary<string, int>(); //当前所需吞噬的物品名称及数量
    public Dictionary<string, int> swallowedItemDic { get; private set; } = new Dictionary<string, int>(); //当前已吞噬的物品名称及数量

    public DateTimeOffset modelSwitchStartTime { get; private set; } = DateTimeOffset.MinValue;//状态转换道具生效时间
    public bool modelSwitchChanged { get; private set; }//转换道具状态是否改变

    public DateTimeOffset boosterDieTime { get; private set; } = DateTimeOffset.MaxValue;//九宫格道具死亡时间
    public DateTimeOffset boosterAffectEndTime { get; private set; } = DateTimeOffset.MinValue;//受九宫格道具影响结束时间
    public DateTimeOffset boosterAffectLastCheckTime { get; private set; } = DateTimeOffset.MinValue;//受九宫格道具影响时最后一次计算CD的时间

    public string firstOpenTaskBoxPond { get; private set; }//第一次打开taskBox时对应关卡的pond，后续不再受关卡影响，以第一次打开时的pond为准
    #endregion

    [JsonIgnore]
    public bool IsInOpenDelay => boxOpenDelayStart && boxOpenDelayEndTime > DateTimeOffset.MinValue;//宝箱正在打开延迟中


    public MergeItemData(
        string prefabName,
        Vector2Int gridPos,
        MergeItemDefinition definition,
        bool isInBubble,
        bool isLocked,
        bool isInBox,
        DateTimeOffset bubbleDieTime,
        int chargeRemainUseTimes,
        int totalChargedCount,
        DateTimeOffset ChargeFinishDate,
        int chargeRemainUseTimes_auto,
        int totalChargedCount_auto,
        DateTimeOffset ChargeFinishDate_auto,
        bool boxOpenDelayStartCountDown,
        DateTimeOffset boxOpenDelayTime,
        int spawnCount,
        int autoSpawnCount,
        int poolSpawnCount,
        bool canSwallow,
        int swallowCount,
        DateTimeOffset modelSwitchStartTime,
        bool modelSwitchChanged,
        DateTimeOffset boosterDieTime,
        DateTimeOffset boosterAffectEndTime,
        DateTimeOffset boosterAffectLastCheckTime,
        string firstOpenTaskBoxPond)
    {
        this.PrefabName = prefabName;
        this.GridPos = gridPos;
        this.Definition = definition;
        this.IsInBubble = isInBubble;
        this.IsLocked = isLocked;
        this.IsInBox = isInBox;
        this.bubbleDieTime = bubbleDieTime;
        this.chargeRemainUseTimes = chargeRemainUseTimes;
        this.totalChargedCount = totalChargedCount;

        this.ChargeFinishDate = ChargeFinishDate;
        this.chargeRemainUseTimes_Auto = chargeRemainUseTimes_auto;
        this.totalChargedCount_Auto = totalChargedCount_auto;
        this.ChargeFinishDate_Auto = ChargeFinishDate_auto;
        this.boxOpenDelayStart = boxOpenDelayStartCountDown;
        this.boxOpenDelayEndTime = boxOpenDelayTime;
        this.spawnCount = spawnCount;
        this.autoSpawnCount = autoSpawnCount;
        this.poolSpawnCount = poolSpawnCount;
        this.canSwallow = canSwallow;
        this.swallowCount = swallowCount;
        this.modelSwitchStartTime = modelSwitchStartTime;
        this.modelSwitchChanged = modelSwitchChanged;
        this.boosterDieTime = boosterDieTime;
        this.boosterAffectEndTime = boosterAffectEndTime;
        this.boosterAffectLastCheckTime = boosterAffectLastCheckTime;
        this.firstOpenTaskBoxPond = firstOpenTaskBoxPond;
        ResetSwallowItem();
    }

    public void SetSwallowDic(Dictionary<string, int> swallowDic, Dictionary<string, int> swallowedDic)
    {
        if (swallowDic != null)
        {
            curSwallowItemDic = swallowDic;
            if (swallowedDic != null)
                swallowedItemDic = swallowedDic;
        }
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public void SetData_GridPos(Vector2Int gridPos)
    {
        GridPos = gridPos;
    }

    public void SetData_ItemGO(MergeItem itemGO)
    {
        ItemGO = itemGO;
    }

    public void SetData_BubbleGO(BubbleItem go)
    {
        BubbleGO = go;
    }

    public void SetData_OpenBubble()
    {
        //IsInBubble = false;
        //bubbleDieTime = DateTimeOffset.MaxValue;
        //ItemGO?.ShowBubbleBrokeEffect(false);
        //MergeActionSystem.OnMergeActionEvent(MergeActionType.MapDataChange);
        if (BubbleGO != null)
            BubbleGO.UnlockBubble();
    }

    public void SetData_OpenPack()
    {
        IsLocked = false;
        ItemGO?.ShowLockedOpenEffect();
        MergeActionSystem.OnMergeActionEvent(MergeActionType.MapDataChange);
    }

    public void SetData_OpenBox()
    {
        IsInBox = false;
        if (ItemGO != null)
            ItemGO.ShowBoxBrokeEffect();
        MergeActionSystem.OnMergeActionEvent(MergeActionType.MapDataChange);
    }

    public void AddPoolSpawnCount()
    {
        poolSpawnCount++;
    }
    public void ResetPoolSpawnCount()
    {
        poolSpawnCount = 0;
    }

    public void ResetSwallowCount()
    {
        swallowCount = 0;
    }

    /// <summary>
    /// 开启打开倒计时
    /// </summary>
    public void StartOpenDelay()
    {
        if (Definition != null && !boxOpenDelayStart)
        {
            if (Definition.CategoryType == MergeItemCategoryType.taskBox)
                CheckFirstOpenTaskBoxPond();

            boxOpenDelayStart = true;
            boxOpenDelayEndTime = TimeManager.Instance.UtcNow().AddSeconds(Definition.BoxOpenTime);
            if (ItemGO != null)
            {
                ItemGO.KillPlayTween_BoxJump();
                ItemGO.RefreshChargeAndClockState();
            }

            if (MergeLevelManager.Instance.CurrentMapData != null)
            {
                MergeLevelManager.Instance.CurrentMapData.SaveLevelData();
                MergeLevelManager.Instance.CurrentMapData.SaveLevelStorePackData();
            }
        }
    }

    /// <summary>
    /// 可充能道具是否处于CD状态
    /// </summary>
    /// <returns></returns>
    public bool IsInCD()
    {
        bool isCd = false;
        switch (Definition.CategoryType)
        {
            case MergeItemCategoryType.container:
            case MergeItemCategoryType.dualSkills:
                isCd = chargeRemainUseTimes <= 0 && totalChargedCount <= 0;
                break;
            case MergeItemCategoryType.finiteContainer:
            case MergeItemCategoryType.taskBox:
                isCd = IsInOpenDelay;
                break;
            case MergeItemCategoryType.produced:
                isCd = chargeRemainUseTimes_Auto <= 0 && totalChargedCount_Auto <= 0;
                break;
            case MergeItemCategoryType.wake:
                isCd = ChargeFinishDate > TimeManager.Instance.UtcNow();
                break;
            case MergeItemCategoryType.swallowZ:
                isCd = !canSwallow && chargeRemainUseTimes_Auto <= 0 && totalChargedCount_Auto <= 0;
                break;
            case MergeItemCategoryType.boxed:
                isCd = Definition.ChargeLoopCDSecond > 0 && chargeRemainUseTimes <= 0 && totalChargedCount <= 0;
                break;
        }
        return isCd;
    }

    /// <summary>
    /// 清除CD
    /// </summary>
    public void ClearCD()
    {
        if (!IsInCD())
            return;
        switch (Definition.CategoryType)
        {
            case MergeItemCategoryType.container:
            case MergeItemCategoryType.dualSkills:
            case MergeItemCategoryType.boxed:
                totalChargedCount++;
                ChargeFinishDate = TimeManager.Instance.UtcNow();
                break;
            case MergeItemCategoryType.finiteContainer:
            case MergeItemCategoryType.taskBox:
                if (IsInOpenDelay)
                    boxOpenDelayEndTime = DateTimeOffset.MinValue;
                break;
            case MergeItemCategoryType.produced:
            case MergeItemCategoryType.swallowZ:
                totalChargedCount_Auto++;
                break;
            case MergeItemCategoryType.wake:
                ChargeFinishDate = TimeManager.Instance.UtcNow();
                break;
        }

        if (ItemGO != null)
        {

            ItemGO.PlayClearCDEffect();
            ItemGO.RefreshAllStates();
        }

        if (MergeLevelManager.Instance.CurrentMapData != null)
        {
            MergeLevelManager.Instance.CurrentMapData.SaveLevelData();
            MergeLevelManager.Instance.CurrentMapData.SaveLevelStorePackData();
        }
    }

    public void ClearCD(double seconds, bool save = false, bool refresh = true)
    {
        if (!IsInCD())
            return;
        switch (Definition.CategoryType)
        {
            case MergeItemCategoryType.container:
            case MergeItemCategoryType.dualSkills:
            case MergeItemCategoryType.boxed:
            case MergeItemCategoryType.wake:
                {
                    ChargeFinishDate = ChargeFinishDate.AddSeconds(-seconds);
                    if (ChargeFinishDate < TimeManager.Instance.UtcNow())
                        ChargeFinishDate = TimeManager.Instance.UtcNow();
                }
                break;
            case MergeItemCategoryType.finiteContainer:
            case MergeItemCategoryType.taskBox:
                if (IsInOpenDelay)
                {
                    boxOpenDelayEndTime = boxOpenDelayEndTime.AddSeconds(-seconds);
                    if (boxOpenDelayEndTime <= TimeManager.Instance.UtcNow())
                        boxOpenDelayEndTime = DateTimeOffset.MinValue;
                }
                break;
            case MergeItemCategoryType.produced:
            case MergeItemCategoryType.swallowZ:
                {
                    ChargeFinishDate_Auto = ChargeFinishDate_Auto.AddSeconds(-seconds);
                    if (ChargeFinishDate_Auto < TimeManager.Instance.UtcNow())
                        ChargeFinishDate_Auto = TimeManager.Instance.UtcNow();
                }
                break;
            default:
                break;
        }

        if (refresh && ItemGO != null)
            ItemGO.RefreshAllStates();

        if (save && MergeLevelManager.Instance.CurrentMapData != null)
        {
            MergeLevelManager.Instance.CurrentMapData.SaveLevelData();
            MergeLevelManager.Instance.CurrentMapData.SaveLevelStorePackData();
        }
    }

    public int GetClearCDCost()
    {
        int cost = 0;
        var now = TimeManager.Instance.UtcNow();
        switch (Definition.CategoryType)
        {
            case MergeItemCategoryType.container:
            case MergeItemCategoryType.dualSkills:
            case MergeItemCategoryType.boxed:
            case MergeItemCategoryType.wake:
                {
                    var leftSeconds = (int)(ChargeFinishDate - now).TotalSeconds;
                    if (leftSeconds * 1.0f > Definition.ChargeLoopCDSecond)
                    {
                        ChargeFinishDate = now.AddSeconds(Definition.ChargeLoopCDSecond);
                        leftSeconds = (int)(ChargeFinishDate - now).TotalSeconds;
                    }
                    if (leftSeconds > 0 && ChargeFinishDate != DateTimeOffset.MinValue && ChargeFinishDate != DateTimeOffset.MaxValue)
                        cost = Mathf.CeilToInt(Definition.FinishChargeCost * leftSeconds * 1.0f / Definition.ChargeLoopCDSecond);
                }
                break;
            case MergeItemCategoryType.finiteContainer:
            case MergeItemCategoryType.taskBox:
                if (IsInOpenDelay)
                {
                    var leftSeconds = (int)(boxOpenDelayEndTime - now).TotalSeconds;
                    if (leftSeconds * 1.0f > Definition.BoxOpenTime)
                    {
                        boxOpenDelayEndTime= now.AddSeconds(Definition.BoxOpenTime);
                        leftSeconds = (int)(boxOpenDelayEndTime - now).TotalSeconds;
                    }
                    if (leftSeconds > 0 && boxOpenDelayEndTime != DateTimeOffset.MinValue && boxOpenDelayEndTime != DateTimeOffset.MaxValue)
                        cost = Mathf.CeilToInt(Definition.FinishChargeCost * leftSeconds * 1.0f / Definition.BoxOpenTime);
                }
                break;
            case MergeItemCategoryType.produced:
            case MergeItemCategoryType.swallowZ:
                {
                    var leftSeconds = (int)(ChargeFinishDate_Auto - now).TotalSeconds;
                    if(leftSeconds * 1.0f> Definition.ChargeLoopCDSecond_Auto) 
                    {
                        ChargeFinishDate_Auto = now.AddSeconds(Definition.ChargeLoopCDSecond_Auto);
                        leftSeconds = (int)(ChargeFinishDate_Auto - now).TotalSeconds;
                    }
                    if (leftSeconds > 0 && ChargeFinishDate_Auto != DateTimeOffset.MinValue && ChargeFinishDate_Auto != DateTimeOffset.MaxValue)
                        cost = Mathf.CeilToInt(Definition.FinishChargeCost * leftSeconds * 1.0f / Definition.ChargeLoopCDSecond_Auto);
                }
                break;
            default:
                break;
        }
        return cost;
    }

    public bool CanSpawnItem_ByContainer()
    {
        if (Definition == null)
        {
            return false;
        }

        bool canSpawn = false;
        switch (Definition.CategoryType)
        {
            case MergeItemCategoryType.container:
            case MergeItemCategoryType.boxed:
                canSpawn = chargeRemainUseTimes > 0 || totalChargedCount > 0;
                break;
            case MergeItemCategoryType.finiteContainer:
            case MergeItemCategoryType.taskBox:
                {
                    if (!boxOpenDelayStart && Definition.BoxOpenTime > 0 || IsInOpenDelay)//延迟宝箱还未开始计时或计时未结束，点击无触发事件
                        canSpawn = false;
                    else
                        canSpawn = chargeRemainUseTimes > 0 || totalChargedCount > 0;
                }
                break;
            case MergeItemCategoryType.modelSwitch:
                {
                    if (modelSwitchChanged)
                        canSpawn = spawnCount < Definition.ModelSwitchSpawnCount2;
                    else
                        canSpawn = spawnCount < Definition.ModelSwitchSpawnCount1;
                }
                break;
            case MergeItemCategoryType.wake:
                {
                    if (ChargeFinishDate > TimeManager.Instance.UtcNow())
                        canSpawn = false;
                    else
                        canSpawn = chargeRemainUseTimes > 0 || totalChargedCount > 0;
                }
                break;
            case MergeItemCategoryType.produced:
                canSpawn = chargeRemainUseTimes_Auto > 0 || totalChargedCount_Auto > 0;
                break;
            case MergeItemCategoryType.dualSkills:
                canSpawn = chargeRemainUseTimes > 0 || totalChargedCount > 0 || chargeRemainUseTimes_Auto > 0 || totalChargedCount_Auto > 0;
                break;
            case MergeItemCategoryType.swallowC:
                canSpawn = !canSwallow;
                break;
            case MergeItemCategoryType.swallowZ:
                canSpawn = !canSwallow && (chargeRemainUseTimes_Auto > 0 || totalChargedCount_Auto > 0);
                break;
            case MergeItemCategoryType.swallowF:
                canSpawn = !canSwallow && (chargeRemainUseTimes > 0 || totalChargedCount > 0);
                break;
        }
        return canSpawn;
    }

    //无限容器生成item
    public void SpawnOneItem_ByInfiniteContainer()
    {
        if (Definition == null)
        {
            return;
        }

        TryRefreshChargeDate_ByInfiniteContainer();
        if (chargeRemainUseTimes <= 0)
        {
            if (totalChargedCount > 0)
            {

                if (totalChargedCount == Definition.TotalChargeCount && Definition.ChargeLoopCDSecond > 0)//从满充能状态消耗一次次数，重置充能时间
                    ChargeFinishDate = TimeManager.Instance.UtcNow().AddSeconds(Definition.ChargeLoopCDSecond);

                totalChargedCount--;
                chargeRemainUseTimes += Definition.CanSpawnCountByOneCharge - 1;
            }
        }
        else
        {
            chargeRemainUseTimes -= 1;
            spawnCount++;

            if (ChargeFinishDate == DateTimeOffset.MaxValue && totalChargedCount < Definition.TotalChargeCount)
            {
                //Debug.LogError("充能倒计时，未正常开始");
                ChargeFinishDate = TimeManager.Instance.UtcNow().AddSeconds(Definition.ChargeLoopCDSecond);

            }
        }
        ItemGO?.RefreshAllStates();
    }

    public void TryRefreshChargeDate_ByInfiniteContainer()
    {
        if (Definition == null || Definition.ChargeLoopCDSecond <= 0)
        {
            return;
        }

        if (totalChargedCount >= Definition.TotalChargeCount)
        {
            return;
        }

        if (Definition.CategoryType == MergeItemCategoryType.wake)
        {
            return;
        }

        if (ChargeFinishDate != DateTimeOffset.MaxValue && ChargeFinishDate <= TimeManager.Instance.UtcNow())
        {
            while (ChargeFinishDate != DateTimeOffset.MaxValue && ChargeFinishDate <= TimeManager.Instance.UtcNow())
            {
                totalChargedCount++;
                if (totalChargedCount >= Definition.TotalChargeCount)
                {
                    chargeRemainUseTimes = 0;

                    ChargeFinishDate = DateTimeOffset.MaxValue;

                }
                else
                {
                    ChargeFinishDate = ChargeFinishDate.AddSeconds(Definition.ChargeLoopCDSecond);

                }
            }

            if (MergeLevelManager.Instance.CurrentMapData != null)
            {
                MergeLevelManager.Instance.CurrentMapData.SaveLevelData();
                MergeLevelManager.Instance.CurrentMapData.SaveLevelStorePackData();
            }
        }
    }

    //有限容器生成item
    public void SpawnOneItem_ByFiniteContainer()
    {
        if (Definition == null)
        {
            Debug.LogError("Definition == null");
            return;
        }
        if (chargeRemainUseTimes <= 0)
        {
            if (totalChargedCount > 0)
            {
                totalChargedCount--;
                chargeRemainUseTimes += Definition.CanSpawnCountByOneCharge - 1;
            }
        }
        else
        {
            chargeRemainUseTimes -= 1;
        }
    }

    public bool IsSpawnFinish_ByFiniteContainer()
    {
        return chargeRemainUseTimes <= 0 && totalChargedCount <= 0;
    }

    //自动容器生成item
    public void SpawnOneItem_ByAutoContainer()
    {
        if (Definition == null)
        {
            return;
        }

        TryRefreshChargeDate_ByAutoContainer();
        if (chargeRemainUseTimes_Auto <= 0)
        {
            if (totalChargedCount_Auto > 0)
            {
                if (totalChargedCount_Auto == Definition.TotalChargeCount_Auto && Definition.ChargeLoopCDSecond_Auto > 0)//从满充能状态消耗一次次数，重置充能时间
                    ChargeFinishDate_Auto = TimeManager.Instance.UtcNow().AddSeconds(Definition.ChargeLoopCDSecond_Auto);
                totalChargedCount_Auto--;
                chargeRemainUseTimes_Auto += Definition.CanSpawnCountByOneCharge_Auto - 1;
                autoSpawnCount += 1;
            }
        }
        else
        {
            chargeRemainUseTimes_Auto -= 1;
            autoSpawnCount += 1;
            if (ChargeFinishDate_Auto == DateTimeOffset.MaxValue && totalChargedCount_Auto < Definition.TotalChargeCount_Auto)
            {
                Debug.LogError("充能倒计时，未正常开始");
                ChargeFinishDate_Auto = TimeManager.Instance.UtcNow().AddSeconds(Definition.ChargeLoopCDSecond_Auto);
            }
        }
        ItemGO?.RefreshAllStates();
    }
    //自动生成容器刷新时间
    public void TryRefreshChargeDate_ByAutoContainer()
    {
        if (Definition == null || Definition.ChargeLoopCDSecond_Auto <= 0)
        {
            return;
        }

        if (totalChargedCount_Auto >= Definition.TotalChargeCount_Auto)
        {
            return;
        }

        if (totalChargedCount_Auto > 0 && totalChargedCount_Auto < Definition.TotalChargeCount_Auto
            || totalChargedCount_Auto == 0 && chargeRemainUseTimes_Auto > 0)
        {
            if (ChargeFinishDate_Auto == DateTimeOffset.MaxValue)
                ChargeFinishDate_Auto = TimeManager.Instance.UtcNow().AddSeconds(Definition.ChargeLoopCDSecond_Auto);
        }

        if (ChargeFinishDate_Auto != DateTimeOffset.MaxValue && ChargeFinishDate_Auto <= TimeManager.Instance.UtcNow())
        {
            while (ChargeFinishDate_Auto != DateTimeOffset.MaxValue && ChargeFinishDate_Auto <= TimeManager.Instance.UtcNow())
            {
                totalChargedCount_Auto++;
                if (totalChargedCount_Auto >= Definition.TotalChargeCount_Auto)
                {
                    chargeRemainUseTimes_Auto = 0;
                    ChargeFinishDate_Auto = DateTimeOffset.MaxValue;
                }
                else
                {
                    ChargeFinishDate_Auto = ChargeFinishDate_Auto.AddSeconds(Definition.ChargeLoopCDSecond_Auto);
                }
            }

            if (MergeLevelManager.Instance.CurrentMapData != null)
            {
                MergeLevelManager.Instance.CurrentMapData.SaveLevelData();
                MergeLevelManager.Instance.CurrentMapData.SaveLevelStorePackData();
            }
        }

    }

    //吞噬道具SwallowC生成item
    public void SpawnOneItem_BySwallowC()
    {
        if (chargeRemainUseTimes <= 0)
        {
            if (totalChargedCount > 0)
            {
                totalChargedCount--;
                chargeRemainUseTimes = Definition.CanSpawnCountByOneCharge - 1;
            }
        }
        else
        {
            chargeRemainUseTimes -= 1;
            spawnCount++;
        }

        if (chargeRemainUseTimes == 0 && totalChargedCount == 0)
            canSwallow = true;
    }

    //吞噬道具SwallowF生成item
    public void SpawnOneItem_BySwallowF()
    {
        if (Definition == null)
        {
            Debug.LogError("Definition == null");
            return;
        }
        if (chargeRemainUseTimes <= 0)
        {
            if (totalChargedCount > 0)
            {
                totalChargedCount--;
                chargeRemainUseTimes += Definition.CanSpawnCountByOneCharge - 1;
            }
        }
        else
        {
            chargeRemainUseTimes -= 1;
        }
    }

    public bool IsSpawnFinish_BySwallowF()
    {
        return chargeRemainUseTimes <= 0 && totalChargedCount <= 0;
    }

    //
    public bool IsSpawnFinish_BySwallowZ()
    {
        return autoSpawnCount >= Definition.TotalChargeCount_Auto * Definition.CanSpawnCountByOneCharge_Auto;
    }

    //有限容器生成item
    public void SpawnOneItem_ByBoxed()
    {
        if (Definition.ChargeLoopCDSecond > 0)
            SpawnOneItem_ByInfiniteContainer();
        else
            SpawnOneItem_ByFiniteContainer();
    }

    public bool IsSpawnFinish_ByBoxed()
    {
        return Definition.ChargeLoopCDSecond <= 0 && chargeRemainUseTimes <= 0 && totalChargedCount <= 0;
    }

    //检测modelSwitch类型道具状态
    public void CheckModelSwitch()
    {
        if (IsInBox || IsLocked || IsInBubble || Definition.CategoryType != MergeItemCategoryType.modelSwitch)
            return;
        if (modelSwitchStartTime == DateTimeOffset.MinValue)
            modelSwitchStartTime = TimeManager.Instance.UtcNow();
        if (!modelSwitchChanged && modelSwitchStartTime.AddSeconds(Definition.ModelSwitchCDTime) <= TimeManager.Instance.UtcNow())
        {
            modelSwitchChanged = true;
            modelSwitchStartTime = DateTimeOffset.MaxValue;
            spawnCount = 0;
        }
    }

    public void SpawnOneItem_ByModelSwitch()
    {
        if (Definition == null)
        {
            Debug.LogError("Definition == null");
            return;
        }
        spawnCount += 1;
    }

    public bool IsSpawnFinish_ByModelSwitch()
    {
        bool finish = false;
        if (modelSwitchChanged)
            finish = spawnCount >= Definition.ModelSwitchSpawnCount2;
        else
            finish = spawnCount >= Definition.ModelSwitchSpawnCount1;
        return finish;
    }

    //启动九宫格加速
    public void StartUpBooster()
    {
        boosterDieTime = TimeManager.Instance.UtcNow().AddSeconds(Definition.SpeedUpSenconds);
    }
    public bool IsBoosterStart()
    {
        return boosterDieTime != DateTimeOffset.MaxValue;
    }

    //检测九宫格道具死亡时间
    public bool CheckBoosterDie()
    {
        if (IsInBox || IsInBubble || IsLocked || Definition.CategoryType != MergeItemCategoryType.booster)
            return false;
        bool die = false;
        if (boosterDieTime != DateTimeOffset.MaxValue && ExtensionTool.IsDateSmallerThanNow(boosterDieTime))
            die = true;
        return die;
    }

    //检测是否收到九宫格加速道具影响,并加速CD
    private double tempCdSpeedUp = 0d;
    public bool CheckAffectByBooster()
    {
        if (IsInBox || IsInBubble || IsLocked || !IsInCD())
            return false;

        //计算加速CD
        if (boosterAffectEndTime != DateTimeOffset.MinValue
            && boosterAffectLastCheckTime != DateTimeOffset.MinValue
            && boosterAffectLastCheckTime < TimeManager.Instance.UtcNow())
        {
            var cdSec = -1d;
            if (boosterAffectEndTime > TimeManager.Instance.UtcNow())
                cdSec = (TimeManager.Instance.UtcNow() - boosterAffectLastCheckTime).TotalSeconds;
            else if (boosterAffectEndTime > boosterAffectLastCheckTime)
                cdSec = (boosterAffectEndTime - boosterAffectLastCheckTime).TotalSeconds;

            if (cdSec > 0d)
                tempCdSpeedUp += cdSec / 2;
            if (tempCdSpeedUp > 1d)
            {
                ClearCD(tempCdSpeedUp, true, false);
                tempCdSpeedUp = 0d;
            }
        }

        //重新check
        boosterAffectEndTime = DateTimeOffset.MinValue;
        boosterAffectLastCheckTime = DateTimeOffset.MinValue;
        CalculateAroundGridItem(out var aroundList);
        if (aroundList != null)
        {
            foreach (var data in aroundList)
            {
                if (data.Definition.CategoryType == MergeItemCategoryType.booster
                    && data.boosterDieTime != DateTimeOffset.MaxValue
                    && data.boosterDieTime > TimeManager.Instance.UtcNow()
                    && boosterAffectEndTime < data.boosterDieTime)
                {//暂不考虑叠加效果，取受影响的最长时间
                    boosterAffectEndTime = data.boosterDieTime;
                    boosterAffectLastCheckTime = TimeManager.Instance.UtcNow();
                }
            }
        }
        return boosterAffectEndTime != DateTimeOffset.MinValue;
    }

    /// <summary>
    /// 计算周围九宫格内所有Item信息（包括未解锁以及在箱子里）
    /// </summary>
    /// <param name="dataList">返回值</param>
    public void CalculateAroundGridItem(out List<MergeItemData> dataList)
    {
        dataList = new List<MergeItemData>();

        CalculateAroundAllGrid(out List<Vector2Int> posList);
        foreach (var pos in posList)
        {
            if (MergeLevelManager.Instance.CurrentMapData.itemDataDict.TryGetValue(pos, out var itemData))
            {
                dataList.Add(itemData);
            }
        }
    }

    /// <summary>
    /// 计算周围九宫格内所有格子信息
    /// </summary>
    /// <param name="dataList">返回坐标位置</param>
    public void CalculateAroundAllGrid(out List<Vector2Int> dataList)
    {
        dataList = new List<Vector2Int>();
        for (int YOffset = GridPos.y - 1; YOffset <= GridPos.y + 1; YOffset++)
        {
            for (int XOffset = GridPos.x - 1; XOffset <= GridPos.x + 1; XOffset++)
            {
                Vector2Int temp = new Vector2Int(XOffset, YOffset);
                if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(temp, out var itemData))
                {
                    if (temp == GridPos)
                        continue;

                    dataList.Add(temp);
                }
            }
        }
    }

    /// <summary>
    /// 计算自身为中心九宫格内空位置
    /// </summary>
    private List<Vector2Int> CalculateEmptyGrid()
    {
        CalculateAroundAllGrid(out List<Vector2Int> emptyList);
        CalculateAroundGridItem(out List<MergeItemData> dataList);
        if (dataList.Count <= 0)
        {
            return null;
        }

        foreach (var item in dataList)
        {
            if (emptyList.Contains(item.GridPos))
            {
                emptyList.Remove(item.GridPos);
            }
        }
        return emptyList;
    }

    /// <summary>
    /// 获取空位置坐标
    /// </summary>
    /// <returns></returns>
    public bool TryGetEmptyGridPosition(out List<Vector2Int> emptyList)
    {
        emptyList = CalculateEmptyGrid();
        return emptyList.Count > 0;
    }

    /// <summary>
    /// 是否可以参与合成
    /// </summary>
    /// <returns></returns>
    public bool CanMerge()
    {
        if (!Definition.CanMerge)
            return false;
        if (IsInOpenDelay)
            return false;
        if (IsInBubble || IsInBox)
            return false;
        return true;
    }

    //重置吞噬对象
    private void ResetSwallowItem()
    {
        if (Definition.CategoryType == MergeItemCategoryType.swallowC || Definition.CategoryType == MergeItemCategoryType.swallowZ
             || Definition.CategoryType == MergeItemCategoryType.swallowF)
        {
            curSwallowItemDic.Clear();
            swallowedItemDic.Clear();
            for (int i = 0; i < Definition.swallowPondIdList.Count; i++)
            {
                var swallowName = MergeRewardItemPool.FindSwallowPrefabFromPool(Definition.swallowPondIdList[i], this);
                if (!string.IsNullOrEmpty(swallowName))
                {
                    curSwallowItemDic[swallowName] = Definition.swallowCountList[i];
                    swallowedItemDic[swallowName] = 0;
                }
            }
        }
    }

    //吞噬物品
    public bool SwallowMergeItem(string name)
    {
        bool swallow = false;
        if (curSwallowItemDic != null && swallowedItemDic != null && curSwallowItemDic.ContainsKey(name))
        {
            if (swallowedItemDic.ContainsKey(name))
            {
                if (swallowedItemDic[name] < curSwallowItemDic[name])
                {
                    swallowedItemDic[name] += 1;
                    swallow = true;
                }
            }
            else
            {
                swallowedItemDic[name] = 1;
                swallow = true;
            }
            if (swallow)
            {
                swallowCount += 1;
                if (IsFullSwallow())
                {
                    canSwallow = false;
                    if (Definition.CategoryType == MergeItemCategoryType.swallowC)
                    {
                        ResetSwallowItem();
                        chargeRemainUseTimes = 0;
                        totalChargedCount = Definition.TotalChargeCount;
                    }
                    else if (Definition.CategoryType == MergeItemCategoryType.swallowZ)
                    {
                        ResetSwallowItem();
                        autoSpawnCount = 0;
                        chargeRemainUseTimes_Auto = 0;
                        totalChargedCount_Auto = 1;
                        ChargeFinishDate_Auto = TimeManager.Instance.UtcNow().AddSeconds(Definition.ChargeLoopCDSecond_Auto);
                    }
                }
            }
        }
        return swallow;
    }

    private bool IsFullSwallow()
    {
        bool full = true;
        foreach (var need in curSwallowItemDic)
        {
            if (!swallowedItemDic.ContainsKey(need.Key) || swallowedItemDic[need.Key] < need.Value)
            {
                full = false;
                break;
            }
        }
        return full;
    }

    //
    public void CheckFirstOpenTaskBoxPond()
    {
        var curLevel = TaskGoalsManager.Instance.curLevelIndex;
        foreach (var data in Definition.taskBoxPondDict)
        {
            if (curLevel <= data.Key)
            {
                firstOpenTaskBoxPond = data.Value;
                break;
            }
        }
    }

    #region 存储工具
    public static Dictionary<string, object> DataToJson(MergeItemData data)
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        if (data == null)
        {
            GameDebug.LogError("data");
            return dict;
        }
        try
        {
            if (string.IsNullOrEmpty(data.PrefabName))
            {
                GameDebug.LogError("要存储的数据prefabName为空");
                return dict;
            }
            dict.Add("name", data.PrefabName);
            dict.Add("x", data.GridPos.x);
            dict.Add("y", data.GridPos.y);
            if (data.IsLocked)
            {
                dict.Add("lock", data.IsLocked);
            }
            if (data.IsInBox)
            {
                dict.Add("box", data.IsInBox);
            }
            if (data.IsInBubble)
            {
                dict.Add("bubble", data.IsInBubble);
                if (data.bubbleDieTime != DateTimeOffset.MaxValue)
                {
                    dict.Add("dieDate", data.bubbleDieTime.ToString());
                }
            }

            if (data.chargeRemainUseTimes > 0)
            {
                dict.Add("remainUse", data.chargeRemainUseTimes);
            }
            if (data.totalChargedCount > 0)
            {
                dict.Add("remainTotal", data.totalChargedCount);
            }
            if (data.ChargeFinishDate != DateTimeOffset.MaxValue)
            {
                dict.Add("chargeFinish", data.ChargeFinishDate.ToString());
            }

            if (data.chargeRemainUseTimes_Auto > 0)
            {
                dict.Add("remainUse_auto", data.chargeRemainUseTimes_Auto);
            }
            if (data.totalChargedCount_Auto > 0)
            {
                dict.Add("remainTotal_auto", data.totalChargedCount_Auto);
            }
            if (data.ChargeFinishDate_Auto != DateTimeOffset.MaxValue)
            {
                dict.Add("chargeFinish_auto", data.ChargeFinishDate_Auto.ToString());
            }

            if (data.boxOpenDelayStart)
            {
                dict.Add("boxOpen", data.boxOpenDelayStart);
            }
            if (data.boxOpenDelayEndTime != DateTimeOffset.MinValue)
            {
                dict.Add("boxDelay", data.boxOpenDelayEndTime.ToString());
            }

            if (data.spawnCount != 0)
                dict.Add("spawnCount", data.spawnCount);
            if (data.autoSpawnCount != 0)
                dict.Add("autoSpawnCount", data.autoSpawnCount);
            if (data.poolSpawnCount != 0)
                dict.Add("poolSpawnCount", data.poolSpawnCount);
            if (data.canSwallow)
                dict.Add("canSwallow", data.canSwallow);
            if (data.swallowCount != 0)
                dict.Add("swallowCount", data.swallowCount);

            if (data.modelSwitchStartTime != DateTimeOffset.MinValue)
                dict.Add("modelSwitchStartTime", data.modelSwitchStartTime.ToString());

            if (data.modelSwitchChanged)
                dict.Add("modelSwitchChanged", data.modelSwitchChanged);

            if (data.boosterDieTime != DateTimeOffset.MaxValue)
                dict.Add("boosterDieTime", data.boosterDieTime.ToString());
            if (data.boosterAffectEndTime != DateTimeOffset.MinValue)
                dict.Add("boosterAffectEndTime", data.boosterAffectEndTime.ToString());
            if (data.boosterAffectLastCheckTime != DateTimeOffset.MinValue)
                dict.Add("boosterAffectLastCheckTime", data.boosterAffectLastCheckTime.ToString());

            if (data.curSwallowItemDic != null && data.curSwallowItemDic.Count > 0)
                dict.Add("curSwallowItemDic", JsonConvert.SerializeObject(data.curSwallowItemDic));
            if (data.swallowedItemDic != null && data.swallowedItemDic.Count > 0)
                dict.Add("swallowedItemDic", JsonConvert.SerializeObject(data.swallowedItemDic));

            if (!string.IsNullOrEmpty(data.firstOpenTaskBoxPond))
                dict.Add("firstOpenTaskBoxPond", data.firstOpenTaskBoxPond);
            return dict;
        }
        catch (Exception e)
        {
            Debug.LogError("DataToJson error!" + e);
            return new Dictionary<string, object>();
        }

    }


    public static MergeItemData JsonToData(Dictionary<string, object> item)
    {
        try
        {
            if (item == null)
            {
                GameDebug.LogError("item is empty！");
                return null;
            }
            string prefabName;
            Vector2Int GridPos;
            bool IsLocked;
            bool IsInBox;
            bool IsInBubble;
            DateTimeOffset bubbleDieTime;
            int chargeRemainUseTimes;
            int RemainTotalChargedCount;
            DateTimeOffset ChargeFinishTime;//充能倒计时
            int chargeRemainUseTimes_auto;
            int RemainTotalChargedCount_auto;
            DateTimeOffset ChargeFinishTime_auto;
            bool IsBoxOpenDelay;
            DateTimeOffset boxDelayTime;
            int spawnCount;
            int autoSpawnCount;
            int poolSpawnCount;
            bool canSwallow;
            int swallowCount;
            DateTimeOffset modelSwitchStartTime;
            bool modelSwitchChanged;
            DateTimeOffset boosterDieTime;
            DateTimeOffset boosterAffectEndTime;
            DateTimeOffset boosterAffectLastCheckTime;
            string firstOpenTaskBoxPond;
            #region JsonToData
            if (item.TryGetValue("name", out object prefabNameObj) && !string.IsNullOrEmpty(prefabNameObj.ToString()))
            {
                prefabName = prefabNameObj.ToString();
            }
            else
            {
                Debug.LogError("存储的数据没有prefabName！");
                return null;
            }
            if (item.TryGetValue("x", out object xObj) && item.TryGetValue("y", out object yObj))
            {
                if (int.TryParse(xObj.ToString(), out int x) && int.TryParse(yObj.ToString(), out int y))
                {
                    GridPos = new Vector2Int(x, y);
                }
                else
                {
                    Debug.LogError("存储的数据没有格子坐标错误！x:" + xObj.ToString() + ",y:" + yObj.ToString());
                    return null;
                }
            }
            else
            {
                Debug.LogError("存储的数据没有格子坐标！");
                return null;
            }
            if (item.TryGetValue("lock", out object isLockObj)
                && bool.TryParse(isLockObj.ToString(), out bool isLock))
            {
                IsLocked = isLock;
            }
            else
            {
                IsLocked = false;
            }
            if (item.TryGetValue("box", out object IsInBoxObj)
                && bool.TryParse(IsInBoxObj.ToString(), out bool isInBox))
            {
                IsInBox = isInBox;
            }
            else
            {
                IsInBox = false;
            }
            if (item.TryGetValue("bubble", out object IsInBubbleObj)
                && bool.TryParse(IsInBubbleObj.ToString(), out bool isInBubble))
            {
                IsInBubble = isInBubble;
            }
            else
            {
                IsInBubble = false;
            }
            if (item.TryGetValue("dieDate", out object DieTimeObj)
                && DateTimeOffset.TryParse(DieTimeObj.ToString(), out DateTimeOffset bubbleticks))
            {
                bubbleDieTime = bubbleticks;
            }
            else
            {
                bubbleDieTime = DateTimeOffset.MaxValue;
            }

            if (item.TryGetValue("remainUse", out object RemainUseTimesObj)
                && int.TryParse(RemainUseTimesObj.ToString(), out int RemainUsetimes))
            {
                chargeRemainUseTimes = RemainUsetimes;
            }
            else
            {
                chargeRemainUseTimes = 0;
            }
            if (item.TryGetValue("remainTotal", out object RemainTotalChargedCountObj)
                && int.TryParse(RemainTotalChargedCountObj.ToString(), out int RemainTotalChargedtimes))
            {
                RemainTotalChargedCount = RemainTotalChargedtimes;
            }
            else
            {
                RemainTotalChargedCount = 0;
            }
            if (item.TryGetValue("chargeFinish", out object ChargeFinishDateObj)
                && DateTimeOffset.TryParse(ChargeFinishDateObj.ToString(), out DateTimeOffset ticks))
            {
                ChargeFinishTime = ticks;
            }
            else
            {
                ChargeFinishTime = DateTimeOffset.MaxValue;
            }

            if (item.TryGetValue("remainUse_auto", out object AutoRemainUseTimesObj)
                && int.TryParse(AutoRemainUseTimesObj.ToString(), out int AutoRemainUsetimes))
            {
                chargeRemainUseTimes_auto = AutoRemainUsetimes;
            }
            else
            {
                chargeRemainUseTimes_auto = 0;
            }
            if (item.TryGetValue("remainTotal_auto", out object AutoRemainTotalChargedCountObj)
                && int.TryParse(AutoRemainTotalChargedCountObj.ToString(), out int AutoRemainTotalChargedtimes))
            {
                RemainTotalChargedCount_auto = AutoRemainTotalChargedtimes;
            }
            else
            {
                RemainTotalChargedCount_auto = 0;
            }
            if (item.TryGetValue("chargeFinish_auto", out object AutoChargeFinishDateObj)
                && DateTimeOffset.TryParse(AutoChargeFinishDateObj.ToString(), out DateTimeOffset Autoticks))
            {
                ChargeFinishTime_auto = Autoticks;
            }
            else
            {
                ChargeFinishTime_auto = DateTimeOffset.MaxValue;
            }

            if (item.TryGetValue("boxOpen", out object isOpenDelayObj)
                && bool.TryParse(isOpenDelayObj.ToString(), out bool isBoxOpenTemp))
            {
                IsBoxOpenDelay = isBoxOpenTemp;
            }
            else
            {
                IsBoxOpenDelay = false;
            }

            if (item.TryGetValue("boxDelay", out object boxDelayObj)
                && DateTimeOffset.TryParse(boxDelayObj.ToString(), out DateTimeOffset boxDelayTimeTemp))
            {
                boxDelayTime = boxDelayTimeTemp;
            }
            else
            {
                boxDelayTime = DateTimeOffset.MinValue;
            }

            if (item.TryGetValue("spawnCount", out object spawnCountObj) && int.TryParse(spawnCountObj.ToString(), out int spawnCountTemp))
            {
                spawnCount = spawnCountTemp;
            }
            else
            {
                spawnCount = 0;
            }
            if (item.TryGetValue("autoSpawnCount", out object autoSpawnCountObj) && int.TryParse(autoSpawnCountObj.ToString(), out int autoSpawnCountTemp))
                autoSpawnCount = autoSpawnCountTemp;
            else
                autoSpawnCount = 0;
            if (item.TryGetValue("poolSpawnCount", out object poolSpawnCountObj) && int.TryParse(poolSpawnCountObj.ToString(), out int poolSpawnCountTemp))
                poolSpawnCount = poolSpawnCountTemp;
            else
                poolSpawnCount = 0;

            if (item.TryGetValue("canSwallow", out object canSwallowObj)
                && bool.TryParse(canSwallowObj.ToString(), out bool canSwallowTemp))
                canSwallow = canSwallowTemp;
            else
                canSwallow = false;

            if (item.TryGetValue("swallowCount", out object swallowCountObj) && int.TryParse(swallowCountObj.ToString(), out int swallowCountTemp))
                swallowCount = swallowCountTemp;
            else
                swallowCount = 0;

            if (item.TryGetValue("modelSwitchStartTime", out object modelSwitchStartTimeObj)
                && DateTimeOffset.TryParse(modelSwitchStartTimeObj.ToString(), out DateTimeOffset modelSwitchStartTimeTemp))
                modelSwitchStartTime = modelSwitchStartTimeTemp;
            else
                modelSwitchStartTime = DateTimeOffset.MinValue;

            if (item.TryGetValue("modelSwitchChanged", out object modelSwitchChangedObj)
                && bool.TryParse(modelSwitchChangedObj.ToString(), out bool modelSwitchChangedTemp))
                modelSwitchChanged = modelSwitchChangedTemp;
            else
                modelSwitchChanged = false;

            if (item.TryGetValue("boosterDieTime", out object boosterDieTimeObj)
                && DateTimeOffset.TryParse(boosterDieTimeObj.ToString(), out DateTimeOffset boosterDieTimeTemp))
                boosterDieTime = boosterDieTimeTemp;
            else
                boosterDieTime = DateTimeOffset.MaxValue;

            if (item.TryGetValue("boosterAffectEndTime", out object boosterAffectEndTimeObj)
                && DateTimeOffset.TryParse(boosterAffectEndTimeObj.ToString(), out DateTimeOffset boosterAffectEndTimeTemp))
                boosterAffectEndTime = boosterAffectEndTimeTemp;
            else
                boosterAffectEndTime = DateTimeOffset.MinValue;

            if (item.TryGetValue("boosterAffectLastCheckTime", out object boosterAffectLastCheckTimeObj)
                && DateTimeOffset.TryParse(boosterAffectLastCheckTimeObj.ToString(), out DateTimeOffset boosterAffectLastCheckTimeTemp))
                boosterAffectLastCheckTime = boosterAffectLastCheckTimeTemp;
            else
                boosterAffectLastCheckTime = DateTimeOffset.MinValue;

            Dictionary<string, int> curSwallowItemDic = null;
            if (item.TryGetValue("curSwallowItemDic", out object curSwallowObj))
                curSwallowItemDic = JsonConvert.DeserializeObject<Dictionary<string, int>>(curSwallowObj.ToString());
            Dictionary<string, int> swallowedItemDic = null;
            if (item.TryGetValue("swallowedItemDic", out object swallowedObj))
                swallowedItemDic = JsonConvert.DeserializeObject<Dictionary<string, int>>(swallowedObj.ToString());

            if (item.TryGetValue("firstOpenTaskBoxPond", out object taskBoxPondObj))
                firstOpenTaskBoxPond = taskBoxPondObj.ToString();
            else
                firstOpenTaskBoxPond = string.Empty;

            if (!MergeItemDefinition.TotalDefinitionsDict.TryGetValue(prefabName, out var def))
            {
                GameDebug.LogError("Objects表中没找到prefabName！" + prefabName);
                def = null;
            }
            #endregion

            MergeItemData itemData = new MergeItemData(
                prefabName,
                GridPos,
                def,
                IsInBubble,
                IsLocked,
                IsInBox,
                bubbleDieTime,
                chargeRemainUseTimes,
                RemainTotalChargedCount,
                ChargeFinishTime,
                chargeRemainUseTimes_auto,
                RemainTotalChargedCount_auto,
                ChargeFinishTime_auto,
                IsBoxOpenDelay,
                boxDelayTime,
                spawnCount,
                autoSpawnCount,
                poolSpawnCount,
                canSwallow,
                swallowCount,
                modelSwitchStartTime,
                modelSwitchChanged,
                boosterDieTime,
                boosterAffectEndTime,
                boosterAffectLastCheckTime,
                firstOpenTaskBoxPond);
            if (curSwallowItemDic != null)
                itemData.SetSwallowDic(curSwallowItemDic, swallowedItemDic);
            return itemData;
        }
        catch (Exception e)
        {
            Debug.LogError("JsonToData error!" + "----------" + e);
            return null;
        }

    }
    #endregion

}
