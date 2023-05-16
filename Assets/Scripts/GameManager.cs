using DG.Tweening;
using Ivy;
using Ivy.Timer;
using IvyCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.NiceVibrations;
using UnityEngine;
using Ivy.Activity;

public class GameManager : SingletonMono<GameManager>
{
    public int PlayerMaxLevel { get; private set; } = 0;


    private float iosNotchAdaptY_ = 0; //苹果刘海屏适应高度 
    private float androidNotchAdaptY_ = 0; //苹果刘海屏适应高度 

    public float MNotchAdaptY_
    {
        get
        {
#if UNITY_ANDROID
            return androidNotchAdaptY_;
#else
            return iosNotchAdaptY_;
#endif
        }
        set
        {
#if UNITY_ANDROID
            androidNotchAdaptY_ = value;
#else
            iosNotchAdaptY_=value;
#endif
        }
    }

    private void Awake()
    {
        gameObject.AddComponent<CloudSystem>();
    }

    /// <summary>
    /// unity关闭时保存数据
    /// </summary>
    public override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
        SavePlayerData();
        //SavePlayerDataAndUploadCloud();
        if (MergeController.CurrentController != null)
            MergeController.CurrentController.CommitByQuitGame();
        PushNotificationSystem.Instance.PushNotification();
        Currencies.TrickEnergy();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SavePlayerData();
            //SavePlayerDataAndUploadCloud();
            if (MergeController.CurrentController != null)
                MergeController.CurrentController.CommitByQuitGame();
            PushNotificationSystem.Instance.PushNotification();
            Currencies.TrickEnergy();
        }
        else
        {
            PushNotificationSystem.Instance.InitSystem();
        }
    }

    public void SavePlayerData()
    {
        if (MergeLevelManager.Instance != null && MergeLevelManager.Instance.CurrentMapData != null)
        {
            MergeLevelManager.Instance.CurrentMapData.SaveLevelData();
            MergeLevelManager.Instance.CurrentMapData.SaveLevelStorePackData();
            MergeLevelManager.Instance.CurrentMapData.SaveBubbleData();
        }
    }

    public void SavePlayerDataAndUploadCloud()
    {
        SavePlayerData();
        CloudSystem.Instance.TryUploadToFirestoreData();
        ActivitySystem.Instance.UploadLocalDataToCloudAsync();
    }

    public IEnumerator Coroutine_LoadingToMapScene()
    {
        //AudioManager.Instance.PlayInMergeBGM(false);
        AssetSystem.Instance.PreloadMergeItems();
        GameLoader.Instance.SetLoadingProgress(0.8f, 0f);
        yield return null;
        AudioManager.Instance.PlayBGM();
        UISystem.Instance.uiMainMenu.CheckActivityOpen();
        GameLoader.Instance.SetLoadingProgress(0.9f, 0f);
        yield return null;
    }

    [CompilerGenerated]
    private sealed class _003COnUnloadSceneAsync_003Ec__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal AsyncOperation async;

        internal object _0024current;

        internal bool _0024disposing;

        internal int _0024PC;

        object IEnumerator<object>.Current
        {
            [DebuggerHidden]
            get { return this._0024current; }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get { return this._0024current; }
        }

        [DebuggerHidden]
        public _003COnUnloadSceneAsync_003Ec__Iterator1()
        {
        }

        public bool MoveNext()
        {
            uint num = (uint)this._0024PC;
            this._0024PC = -1;
            switch (num)
            {
                case 0u:
                case 1u:
                    if (!this.async.isDone)
                    {
                        this._0024current = null;
                        if (!this._0024disposing)
                        {
                            this._0024PC = 1;
                        }

                        break;
                    }

                    Resources.UnloadUnusedAssets();
                    this._0024PC = -1;
                    goto default;
                default:
                    return false;
            }

            return true;
        }

        [DebuggerHidden]
        public void Dispose()
        {
            this._0024disposing = true;
            this._0024PC = -1;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }
    }

    public PlayerData playerData { get; private set; }

    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadGameData()
    {
        // Currencies
        try
        {
            Currencies.Init();
        }
        catch (Exception e)
        {
            DebugSetting.LogError(e, "[InitializeNextScript error] Currencies!");
        }

        try
        {
            LoadPlayerData();
        }
        catch (Exception e)
        {
            DebugSetting.LogError(e, "[GameManager.InitManager error]!");
        }

        try
        {
            AssetSystem.Instance.LoadAsset<PlayerMaxLevelSO>("PlayerMaxLevelSO",
                so => { PlayerMaxLevel = so.playeMaxLevel; });
        }
        catch (Exception e)
        {
            DebugSetting.LogError(e, "[PlayerMaxlevelSO error]!");
        }

        RewardBoxManager.Instance.LoadData();
        BookSaveSystem.Instance.LoadData();

        StoreManager.Instance.LoadData();

        TaskGoalsManager.Instance.LoadData();
        //礼包初始化
        GiftPackageManager.Instance.ManagerInit();
        //副本初始化

        DungeonSystem.Instance.InitDungeonSystem();
        //支线活动初始化
        BranchSystem.Instance.InitBranchSystem();
        ChooseSkinSystem.Instance.InitSystem();
        //远程礼包初始化
        RemoteGiftSystem.Instance.InitRemoteGiftSystem();
        FestivalSystem.Instance.InitSystem();

        BattlePassSystem.Instance.InitSystem(() => { });
        //DailyTaskSystem.Instance.InitSystem(() => { });
        ShopSystem.Instance.InitSystem(() => { });

        if (UISystem.Instance != null && UISystem.Instance.topResourceManager != null)
        {
            UISystem.Instance.topResourceManager.SetExperienceValue();
        }

        AchievementManager.Instance.InitSystem();
        DailyActiveSystem.Instance.TryInitDailyActiveSystem();
        yield return null;
    }

    public void LoadPlayerData()
    {
        int exp = SaveUtils.GetInt(Consts.SaveKey_PlayerData_Exp);
        int currentExpLevel = 1;
        if (SaveUtils.HasKey(Consts.SaveKey_PlayerData_CurrentExpLevel))
        {
            currentExpLevel = SaveUtils.GetInt(Consts.SaveKey_PlayerData_CurrentExpLevel);
        }

        playerData = new PlayerData(exp, currentExpLevel);
        if (SaveUtils.HasKey(Consts.SaveKey_PlayerData_Music))
        {
            playerData.IsMusicOn = SaveUtils.GetBool(Consts.SaveKey_PlayerData_Music);
        }

        if (SaveUtils.HasKey(Consts.SaveKey_PlayerData_Haptic))
        {
            playerData.IsHapticOn = SaveUtils.GetBool(Consts.SaveKey_PlayerData_Haptic);
            VibrateSystem.SetHapticsActive(playerData.IsHapticOn);
        }

        if (SaveUtils.HasKey(Consts.SaveKey_PlayerData_Effect))
        {
            playerData.IsEffectOn = SaveUtils.GetBool(Consts.SaveKey_PlayerData_Effect);
        }

        if (SaveUtils.HasKey(Consts.SaveKey_firstPlayDate) &&
            long.TryParse(SaveUtils.GetString(Consts.SaveKey_firstPlayDate), out long ticks))
        {
            playerData.firstPlayDate = new DateTimeOffset(ticks, TimeSpan.Zero);
        }
        else
        {
            playerData.firstPlayDate = TimeManager.Instance.UtcNow();
            SavePlayerData_FirstSession();
        }

        if (SaveUtils.HasKey(Consts.SaveKey_PlayerSata_FirstEnterLevel))
        {
            playerData.isFirstEnterLevel = SaveUtils.GetBool(Consts.SaveKey_PlayerSata_FirstEnterLevel);
        }

        if (SaveUtils.HasKey(Consts.SaveKey_PlayerData_BuyMonthlyCardTime))
        {
            DateTimeOffset.TryParse(SaveUtils.GetString(Consts.SaveKey_PlayerData_BuyMonthlyCardTime),
                out DateTimeOffset dateTime);
            if (dateTime != null)
            {
                PlayerData.BuyMonthlyCardTime = dateTime;
            }
        }

        if (SaveUtils.HasKey(Consts.SaveKey_PlayerData_ReceiveRewardTime))
        {
            DateTimeOffset.TryParse(SaveUtils.GetString(Consts.SaveKey_PlayerData_ReceiveRewardTime),
                out DateTimeOffset dateTime);
            if (dateTime != null)
            {
                PlayerData.ReceiveRewardsTime = dateTime;
            }
        }

        if (DateTimeOffset.TryParse(SaveUtils.GetString(Consts.SaveKey_PlayerData_BuyDailyEneryTime),
                out DateTimeOffset dateTimeOffset))
        {
            PlayerData.BuyDailyEnergyTime = dateTimeOffset;
        }

        if (DateTimeOffset.TryParse(SaveUtils.GetString(Consts.SaveKey_PlayerData_SpinWheelFreeTime),
                out DateTimeOffset spinTime))
        {
            PlayerData.SpinWheelFreeTime = spinTime;
        }

        PlayerData.EnergyCost = SaveUtils.GetInt(Consts.SaveKey_PlayerData_EnergyCost);
        if (DateTimeOffset.TryParse(SaveUtils.GetString(Consts.SaveKey_PlayerData_BuyEnergyTime),
                out DateTimeOffset dateTime1))
        {
            PlayerData.BuyEnergyTime = dateTime1;
        }
    }

    public void ResetPlayerData_ByFirestore(int experience, int collectedExpLevel, bool isMusicOn, bool isEffectOn,
        bool IsHapticOn, string firstSession, string lastSession)
    {
        playerData.ResetData(experience, collectedExpLevel, isMusicOn, isEffectOn, IsHapticOn, firstSession,
            lastSession);
        SavePlayerData_Exp();
        SavePlayerData_Music();
        SavePlayerData_Effect();
        SavePlayerData_Haptic();
        SavePlayerData_FirstSession();
    }

    public void ResetPlayerData_Cost(int energyCost, string buyEnergyTime, string buyDailyEnergyTime,
        string spinFreeTime, Dictionary<int, object> SpinWheelItemDic, string buyMonthlyTime, string receiveMonthlyTime,
        string buyADTime)
    {
        PlayerData.EnergyCost = energyCost;
        if (!string.IsNullOrEmpty(buyEnergyTime) &&
            DateTimeOffset.TryParse(buyEnergyTime, out DateTimeOffset BuyEnergyTime))
            PlayerData.BuyEnergyTime = BuyEnergyTime;
        if (!string.IsNullOrEmpty(buyDailyEnergyTime) &&
            DateTimeOffset.TryParse(buyDailyEnergyTime, out DateTimeOffset BuyDailEnergyTime))
            PlayerData.BuyDailyEnergyTime = BuyDailEnergyTime;
        if (!string.IsNullOrEmpty(spinFreeTime) &&
            DateTimeOffset.TryParse(spinFreeTime, out DateTimeOffset SpinFreeTime))
            PlayerData.SpinWheelFreeTime = SpinFreeTime;
        if (SpinWheelItemDic != null)
            PlayerData.SpinWheelServerToLocal(SpinWheelItemDic);
        if (!string.IsNullOrEmpty(buyMonthlyTime) &&
            DateTimeOffset.TryParse(buyMonthlyTime, out DateTimeOffset buyMonthlyTimeDate))
            PlayerData.BuyMonthlyCardTime = buyMonthlyTimeDate;
        if (!string.IsNullOrEmpty(receiveMonthlyTime) &&
            DateTimeOffset.TryParse(receiveMonthlyTime, out DateTimeOffset receiveMonthlyTimeDate))
            PlayerData.ReceiveRewardsTime = receiveMonthlyTimeDate;
        if (!string.IsNullOrEmpty(buyADTime) &&
            DateTimeOffset.TryParse(buyADTime, out DateTimeOffset buyADTimeDate))
            PlayerData.BuyADTime = buyADTimeDate;

        SavePlayerData_EnergyCost();
        SavePlayerData_BuyEnergyTime();
        SavePlayerData_BuyDailyEnergyTime();
        SavePlayerData_SpinWheelFreeTime();
        SavePlayerData_BuyMonthlyCardTime();
        SavePlayerData_ReceiveMonthlyCardRewardTime();
        SavePlayerData_ReceivebuyADTime();
    }

    public void SavePlayerData_Exp()
    {
        SaveUtils.SetInt(Consts.SaveKey_PlayerData_Exp, playerData.Experience);
        SaveUtils.SetInt(Consts.SaveKey_PlayerData_CurrentExpLevel, playerData.CurrentExpLevel);
    }

    public void SavePlayerData_Music()
    {
        SaveUtils.SetBool(Consts.SaveKey_PlayerData_Music, playerData.IsMusicOn);
    }

    public void SavePlayerData_Haptic()
    {
        SaveUtils.SetBool(Consts.SaveKey_PlayerData_Haptic, playerData.IsHapticOn);
    }

    public void SavePlayerData_Effect()
    {
        SaveUtils.SetBool(Consts.SaveKey_PlayerData_Effect, playerData.IsEffectOn);
    }

    public void SavePlayerData_EnergyCost()
    {
        SaveUtils.SetInt(Consts.SaveKey_PlayerData_EnergyCost, PlayerData.EnergyCost);
    }

    public void SavePlayerData_BuyEnergyTime()
    {
        SaveUtils.SetString(Consts.SaveKey_PlayerData_BuyEnergyTime, PlayerData.BuyEnergyTime.ToString());
    }

    public void SavePlayerData_BuyDailyEnergyTime()
    {
        SaveUtils.SetString(Consts.SaveKey_PlayerData_BuyDailyEneryTime, PlayerData.BuyDailyEnergyTime.ToString());
    }

    public void SavePlayerData_FirstSession()
    {
        SaveUtils.SetString(Consts.SaveKey_firstPlayDate, playerData.firstPlayDate.Ticks.ToString());
    }

    public void SavePlayerData_SpinWheelFreeTime()
    {
        SaveUtils.SetString(Consts.SaveKey_PlayerData_SpinWheelFreeTime, PlayerData.SpinWheelFreeTime.ToString());
    }

    public void SavePlayerData_BuyMonthlyCardTime()
    {
        SaveUtils.SetString(Consts.SaveKey_PlayerData_BuyMonthlyCardTime, PlayerData.BuyMonthlyCardTime.ToString());
    }

    public void SavePlayerData_ReceiveMonthlyCardRewardTime()
    {
        SaveUtils.SetString(Consts.SaveKey_PlayerData_ReceiveRewardTime, PlayerData.ReceiveRewardsTime.ToString());
    }

    public void SavePlayerData_ReceivebuyADTime()
    {
        SaveUtils.SetString(Consts.SaveKey_PlayerData_BuyADTime, PlayerData.BuyADTime.ToString());
    }

    public static event Action UpdateEnergyCountDownEvent;
    private float timer = 0;
    private float countDown = 600;

    private void LateUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer -= 1;
            Currencies.RefreshEnergyValue();
            UpdateEnergyCountDownEvent?.Invoke();
        }

        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            countDown += 600;
            SavePlayerDataAndUploadCloud();
        }
    }

    private bool isInitRank = false;

    /// <summary>
    /// 开启活动
    /// </summary>
    public void TryStartEvent()
    {
        if (!FirstoreConfigData_EventDate.hasGetEventDateSuccess || !TimeManager.IsGetServerUtcSuccess)
        {
            return;
        }

        if (!isInitRank)
        {
            isInitRank = true;

            //rank活动
            if (FirstoreConfigData_EventDate.rank_CurrentOrNextOpenDate.IsCurrentEvent)
            {
                //处于活动中
                //DebugTool.Log("处于活动中");
                //RankSystem.Instance.InitData();
            }
            else
            {
                //尝试领取很久之前的奖励
                //RankSystem.Instance.TryGetSeasonReward();
            }
        }
    }


    /// <summary>
    /// 同步服务器数据（玩家名字和头像）
    /// </summary>
    public void UploadNameOrProfileToCloud()
    {
        //#if UNITY_EDITOR
        //        return;//unity模式下不走这里
        //#endif
    }

    /// <summary>
    /// 尝试发现新物品
    /// </summary>
    /// <param name="mergeRewardItem"></param>
    public bool TryNewDiscoveryItem(string prefabName)
    {
        if (string.IsNullOrEmpty(prefabName))
        {
            return false;
        }

        if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(prefabName,
                out MergeItemDefinition mergeItemDefinition))
        {
            if (mergeItemDefinition == null)
            {
                return false;
            }

            if (mergeItemDefinition.m_discoveryState == MergeItemDiscoveryState.Undiscovered
                || mergeItemDefinition.m_discoveryState == MergeItemDiscoveryState.NONE)
            {
                BookSaveSystem.Instance.SaveData(prefabName, MergeItemDiscoveryState.Unlock);
                return true;
            }
        }

        return false;
    }

    public bool IsDiscovered(string prefabName)
    {
        if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(prefabName,
                out MergeItemDefinition mergeItemDefinition))
        {
            if (MergeItemChain.TotalChainsDict.TryGetValue(mergeItemDefinition.ChainID, out MergeItemChain chain))
            {
                for (int i = 0; i < chain.Chain.Length; i++)
                {
                    if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(chain.Chain[i].PrefabName,
                            out MergeItemDefinition definition))
                    {
                        if (definition.m_discoveryState == MergeItemDiscoveryState.Unlock ||
                            definition.m_discoveryState == MergeItemDiscoveryState.Discovered)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            return false;
        }

        return false;
    }

    public void RefreshCurrency_Exp()
    {
        UI_Manager.Instance.InvokeRefreshEvent("", "RefreshCurrency_Exp");
    }

    public void RefreshCurrency_Coins()
    {
        UI_Manager.Instance.InvokeRefreshEvent("", "RefreshCurrency_Coins");
    }

    public void RefreshCurrency_Gems()
    {
        UI_Manager.Instance.InvokeRefreshEvent("", "RefreshCurrency_Gems");
    }

    public void RefreshCurrency_Energy(bool needTween = false)
    {
        string str = needTween ? "needTween" : "";
        UI_Manager.Instance.InvokeRefreshEvent(str, "RefreshCurrency_Energy");
    }

    public void RefreshCurrency_Needle()
    {
        UI_Manager.Instance.InvokeRefreshEvent("", "RefreshCurrency_Needle");
    }

    #region 弹窗发放奖励

    private IEnumerator PlayTweenRewardFly_Goods(GameObject go_Prefab, Vector3 targetWorldPos,
        Action oneFinishCB, float _delay)
    {
        bool showTween = false;
        Vector3 startScale = go_Prefab.transform.localScale;
        if (go_Prefab == null)
        {
            yield break;
        }

        go_Prefab.SetActive(true);
        AssetSystem.Instance.PrewarmUI("Effect_Boom", 1);
        Transform tweenRootTran = go_Prefab.transform;
        Sequence sequence = DOTween.Sequence();
        Vector3 pos = tweenRootTran.position;
        Vector2 random = UnityEngine.Random.insideUnitCircle;
        Vector2 p0 = new Vector2(pos.x, pos.y);
        Vector2 p2 = new Vector2(targetWorldPos.x, targetWorldPos.y);
        Vector2 p1 = new Vector2(p0.x, (p0.y + p2.y) * 0.5f);
        float scale_time = 0.4f;
        //变大
        //sequence.Append(tweenRootTran.DOScale(startScale.x+0.1f, scale_time).SetEase(Ease.OutQuad).SetLoops(1, LoopType.Yoyo));
        //飞行
        float duration = 0.35f + _delay;
        sequence.Append(DOTween.To(setter: value =>
        {
            Vector2 vector = DoAnimTools.Bezier(p0, p1, p2, value);
            tweenRootTran.position = new Vector3(vector.x, vector.y, 0);
        }, startValue: 0, endValue: 1, duration: duration).SetEase(Ease.InBack));
        sequence.InsertCallback(duration + 0.015f, () => { tweenRootTran.gameObject.SetActive(false); });
        sequence.InsertCallback(duration - 0.07f, () =>
        {
            // 爆炸动画
            GameObject gO = AssetSystem.Instance.SpawnUI("Effect_Boom", UISystem.Instance.topRootTran);
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Get_Good);
            gO.transform.SetAsLastSibling();
            gO.transform.position = new Vector3(targetWorldPos.x, targetWorldPos.y, 0);
            gO.GetComponent<ParticleSystem>().Play();
            TimerSystem.Instance.AddTimer(DateTimeOffset.UtcNow.AddSeconds(2),
                () => { AssetSystem.Instance.UnspawnUI("Effect_Boom", gO); });
        });
        //缩放
        sequence.Insert(_delay, tweenRootTran.DOScale(0.7f, duration - 0.05f).SetEase(Ease.InQuart));

        //回调
        sequence.InsertCallback(scale_time + _delay + duration - 0.05f, () =>
        {
            if (showTween)
            {
                return;
            }

            showTween = true;
            oneFinishCB?.Invoke();
        });

        sequence.onComplete = () =>
        {
            tweenRootTran.position = new Vector3(targetWorldPos.x, targetWorldPos.y, 0);
            tweenRootTran.gameObject.SetActive(false);
            go_Prefab.transform.localScale = startScale;
        };
        sequence.Play();
    }

    private CurrencyID GetRewardCurrencyID(MergeRewardItem rewardItem)
    {
        CurrencyID currencyID = CurrencyID.Exp;
        if (rewardItem.IsRewardCoins)
            currencyID = CurrencyID.Coins;
        else if (rewardItem.IsRewardGems)
            currencyID = CurrencyID.Gems;
        else if (rewardItem.IsRewardEnergy)
            currencyID = CurrencyID.Energy;
        return currencyID;
    }

    private int GetSplitNum(MergeRewardItem rewardItem)
    {
        int showCount = 1;
        if (rewardItem.num >= 10000)
        {
            showCount = 11;
        }
        else if (rewardItem.num >= 5000)
        {
            showCount = 9;
        }
        else if (rewardItem.num >= 300)
        {
            showCount = 7;
        }
        else if (rewardItem.num >= 5)
        {
            showCount = 5;
        }
        else
        {
            showCount = rewardItem.num;
        }

        return showCount;
    }


    private IEnumerator PlayTweenRewardFly_Currency(MergeRewardItem rewardItem, GameObject go_Prefab,
        Vector3 targetWorldPos,
        Action oneFinishCB, float delay_time)
    {
        yield return new WaitForSeconds(delay_time);
        bool showTween = false;
        Vector3 startScale;
        if (rewardItem.IsRewardCoins || rewardItem.IsRewardGems || rewardItem.IsRewardEnergy)
            startScale = new Vector3(0.8f, 0.8f, 1f);
        else
            startScale = Vector3.one;
        int showCount = GetSplitNum(rewardItem);
        Vector3 startWorldPos = go_Prefab.transform.position;
        go_Prefab.gameObject.SetActive(false);
        List<GameObject> gOList = new List<GameObject>();
        for (int i = 0; i < showCount; i++)
        {
            GameObject gO =
                AssetSystem.Instance.Instantiate(rewardItem.ShowRewardPrefabName, UISystem.Instance.topRootTran);
            if (gO == null)
            {
                yield break;
            }

            gO.transform.position = new Vector3(startWorldPos.x, startWorldPos.y, 0);
            gO.SetActive(false);
            gOList.Add(gO);
        }

        yield return null;
        CurrencyID currencyID = GetRewardCurrencyID(rewardItem);
        int all_rewardNum = rewardItem.num;
        bool isHaptic = false;
        for (int i = 0; i < showCount; i++)
        {
            int gOIndex = i;
            if (gOList[gOIndex] == null)
            {
                continue;
            }

            int single_num = rewardItem.num / showCount;
            if (i == showCount - 1)
            {
                single_num = all_rewardNum - single_num * (showCount - 1);
            }

            AssetSystem.Instance.PrewarmUI("Effect_Boom_Small", 1);
            gOList[gOIndex].SetActive(true);
            Transform tweenRootTran = gOList[gOIndex].transform;
            Sequence sequence = DOTween.Sequence();
            Vector2 random = UnityEngine.Random.insideUnitCircle;
            Vector2 p0 = new Vector2(startWorldPos.x, startWorldPos.y) + random * 0.5f;
            Vector2 p2 = new Vector2(targetWorldPos.x, targetWorldPos.y);
            Vector2 p1 = new Vector2(p0.x, (p0.y + p2.y) * 0.5f);
            tweenRootTran.localScale = startScale;
            sequence.Append(tweenRootTran.DOMove(p0, 0.7f)).SetEase(Ease.OutQuad);
            float duration = 0.35f + gOIndex * 0.15f;
            CanvasGroup animatedAlpha = tweenRootTran.gameObject.AddComponent<CanvasGroup>();
            sequence.Append(DOTween.To(setter: value =>
            {
                Vector2 vector = DoAnimTools.Bezier(p0, p1, p2, value);
                tweenRootTran.position = new Vector3(vector.x, vector.y, 0);
            }, startValue: 0, endValue: 1, duration: duration).SetEase(Ease.InQuad));
            sequence.InsertCallback(0.7f + duration - 0.02f, () =>
            {
                // 爆炸动画
                GameObject gO = AssetSystem.Instance.SpawnUI("Effect_Boom_Small", UISystem.Instance.loadingRootTran);
                gO.transform.position = new Vector3(targetWorldPos.x, targetWorldPos.y, 0);
                gO.transform.localScale = Vector3.one * 0.5f;
                gO.GetComponent<ParticleSystem>().Play();
                TimerSystem.Instance.AddTimer(DateTimeOffset.UtcNow.AddSeconds(2),
                    () => { AssetSystem.Instance.UnspawnUI("Effect_Boom_Small", gO); });
            });
            sequence.Insert(0.7f, tweenRootTran.DOScale(0.8f, duration - 0.05f).SetEase(Ease.InQuart));
            sequence.Insert(0.7f + duration - 0.1f,
                DOTween.To(setter: value => { animatedAlpha.alpha = value; }, startValue: 1, endValue: 0,
                    duration: 0.1f).SetEase(Ease.InQuad));
            sequence.InsertCallback(0.7f + duration - 0.05f, () =>
            {
                if (showTween)
                {
                    return;
                }

                showTween = true;
                oneFinishCB?.Invoke();
            });
            sequence.onComplete = () =>
            {
                tweenRootTran.position = new Vector3(targetWorldPos.x, targetWorldPos.y, 0);
                tweenRootTran.gameObject.SetActive(false);
                animatedAlpha.alpha = 1;
                if (!isHaptic)
                    VibrateSystem.Haptic(MoreMountains.NiceVibrations.HapticTypes.Selection);
                isHaptic = true;
                var alpha = tweenRootTran.gameObject.GetComponent<CanvasGroup>();
                if (alpha != null)
                {
                    Destroy(alpha);
                }

                Currencies.Add(currencyID, single_num, "", true);
                AssetSystem.Instance.DestoryGameObject(rewardItem.ShowRewardPrefabName, tweenRootTran.gameObject);
            };
            sequence.Play();
        }
    }

    public void GiveRewardByPopup(Dictionary<RewardType, Dictionary<string, MergeRewardItem>> RewardsShowDics,
        string season, List<Item_Reward> _Rewards, MergeLevelType levelType = MergeLevelType.mainLine,
        Action action = null, bool is_noOpenUI = false)
    {
        StartCoroutine(GameManager.Instance.GiveRewardDelaySingleAni(RewardsShowDics, season, _Rewards, levelType,
            action, is_noOpenUI));
    }

    private IEnumerator GiveRewardDelaySingleAni(
        Dictionary<RewardType, Dictionary<string, MergeRewardItem>> RewardsShowDics, string season,
        List<Item_Reward> _Rewards, MergeLevelType levelType = MergeLevelType.mainLine, Action action = null,
        bool is_noOpenUI = false)
    {
        int i = 0;
        foreach (var rewardsDic in RewardsShowDics)
        {
            //rewardsDic 对应每一种type类型的奖励 
            //yield return new WaitForSeconds(0.5f);
            float _delay = 0;
            foreach (var rewardItem in rewardsDic.Value.Values)
            {
                _delay += 0.2f;
                //yield return new WaitForSeconds(0.2f);
                //rewardItem 对应到单个奖励
                GameObject go_Prefab;
                if (i < _Rewards.Count)
                    go_Prefab = _Rewards[i].gameObject;
                else
                    go_Prefab = UISystem.Instance.topRootTran.GetChild(i).gameObject;
                Vector3 screenPos = go_Prefab.transform.position;

                if (rewardsDic.Key == RewardType.Goods)
                {
                    UIPanel_Merge.refreshTopAction?.Invoke();
                    StartCoroutine(PlayTweenRewardFly_Goods(go_Prefab, GetGoodsTagetPos(),
                        () => { GiveRewardItem(rewardItem, season, screenPos, false, levelType); }, _delay));
                }
                else
                {
                    //yield return new WaitForSeconds(_delay);
                    CurrencyID currencyID = GetRewardCurrencyID(rewardItem);
                    if (GetTrans(rewardItem.IsRewardExp, currencyID, out Transform currencyTran,
                            UISystem.Instance.topResourceManager))
                    {
                        StartCoroutine(PlayTweenRewardFly_Currency(rewardItem, go_Prefab, currencyTran.position,
                            null, _delay));
                    }
                }

                i++;
            }
        }

        float delay_time = 0.5f;
        if (is_noOpenUI)
            delay_time = 0.5f;
        if (RewardsShowDics.ContainsKey(RewardType.Currency) && RewardsShowDics[RewardType.Currency].Count > 0)
            yield return new WaitForSeconds(delay_time + 0.2f + i * 0.2f);
        else
            yield return new WaitForSeconds(delay_time + i * 0.2f);
        action?.Invoke();
        yield return null;
    }

    #endregion

    private Dictionary<string, Action> actionDic = new Dictionary<string, Action>();

    //弹窗发放奖励
    public void GiveRewardItem(List<MergeRewardItem> rewardItem, string season,
        MergeLevelType levelType = MergeLevelType.mainLine, bool isDouble = false, Action cb = null,
        bool is_noOpenUI = false)
    {
        if (rewardItem == null || rewardItem.Count == 0)
            return;
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.btn_Receive);
        ShowRewardsData showRewardsData = new ShowRewardsData(rewardItem, season, levelType, isDouble, cb, is_noOpenUI);
        UISystem.Instance.ShowUI(showRewardsData);
    }


    //普通发放奖励
    public void GiveRewardItem(List<MergeRewardItem> rewardItem, string season, Vector3 screenPos,
        MergeLevelType levelType = MergeLevelType.mainLine)
    {
        if (rewardItem != null)
        {
            foreach (var item in rewardItem)
            {
                GiveRewardItem(item, season, screenPos, true, levelType);
            }
        }
    }

    //普通发放奖励
    public void GiveRewardItemNoAni(List<MergeRewardItem> rewardItem, string season,
        MergeLevelType levelType = MergeLevelType.mainLine)
    {
        if (rewardItem != null)
        {
            foreach (var item in rewardItem)
            {
                GiveRewardItem(item, season, Vector3.zero, false, levelType);
            }
        }
    }


    /// <summary>
    /// 发放奖励 (只发放金币、体力、钻石、经验，和发放到关卡红色礼盒)
    /// </summary>
    /// <param name="mergeRewardItem"></param>
    /// <param name="season">奖励路径</param>
    /// <param name="screenPos">屏幕坐标</param>
    /// <param name="needFlyCurrency">货币奖励是否需要飞向顶部动画</param>
    /// <param name="levelType">发放到对应副本类型的临时背包</param>
    public void GiveRewardItem(MergeRewardItem mergeRewardItem, string season, Vector3 screenPos,
        bool needFlyCurrency = true, MergeLevelType levelType = MergeLevelType.mainLine)
    {
        GameDebug.Log("发放奖励");
        if (string.IsNullOrEmpty(mergeRewardItem.name) || mergeRewardItem.num <= 0)
        {
            GameDebug.LogError($"发放的奖励预制体名称为空或数量小于等于0！！！name:[{mergeRewardItem.name}], num:[{mergeRewardItem.num}]");
            return;
        }


        Vector3 startWorldPos = screenPos; // useScreenPos ? Camera.main.ScreenToWorldPoint(screenPos) : Vector3.zero;
        if (mergeRewardItem.IsRewardExp)
        {
            playerData.AddExperience(mergeRewardItem.num);

            //播放动画
            if (needFlyCurrency && GetCurrenciesTran(true, CurrencyID.NONE, out Transform currenciesTran))
            {
                StartCoroutine(PlayTweenRewardFly(mergeRewardItem, currenciesTran.position, startWorldPos,
                    () =>
                    {
                        currenciesTran.DOKill(true);
                        currenciesTran.DOScale(1.2f, 0.2f).SetEase(Ease.InQuad).onComplete = () =>
                        {
                            RefreshCurrency_Exp();
                            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Get_EXP);
                            VibrateSystem.Haptic(HapticTypes.Selection);
                            currenciesTran.transform.DOScale(1f, 0.2f).SetEase(Ease.OutQuad).SetDelay(0.1f);
                        };
                    }));
            }
            else
            {
                RefreshCurrency_Exp();
            }

            TryShowTopTopNoticeBar(TopNoticeType.PlayerLevelRewardReady);
        }
        else if (mergeRewardItem.IsRewardCoins)
        {
            Currencies.Add(CurrencyID.Coins, mergeRewardItem.num, season, false);
            //播放动画
            if (needFlyCurrency && GetCurrenciesTran(false, CurrencyID.Coins, out Transform currenciesTran))
            {
                StartCoroutine(PlayTweenRewardFly(mergeRewardItem, currenciesTran.position, startWorldPos,
                    () =>
                    {
                        currenciesTran.DOKill(true);
                        currenciesTran.transform.DOScale(1.2f, 0.2f).SetEase(Ease.InQuad).onComplete = () =>
                        {
                            RefreshCurrency_Coins();
                            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Get_Coins);
                            VibrateSystem.Haptic(HapticTypes.Selection);
                            currenciesTran.transform.DOScale(1f, 0.2f).SetEase(Ease.OutQuad).SetDelay(0.1f);
                        };
                    }));
            }
            else
            {
                RefreshCurrency_Coins();
            }
        }
        else if (mergeRewardItem.IsRewardEnergy)
        {
            var cId = CurrencyID.Energy;
            Currencies.Add(cId, mergeRewardItem.num, season, false);
            //播放动画
            if (needFlyCurrency && GetCurrenciesTran(false, cId, out Transform currenciesTran))
            {
                StartCoroutine(PlayTweenRewardFly(mergeRewardItem, currenciesTran.position, startWorldPos,
                    () =>
                    {
                        currenciesTran.DOKill(true);
                        currenciesTran.transform.DOScale(1.2f, 0.2f).SetEase(Ease.InQuad).onComplete = () =>
                        {
                            RefreshCurrency_Energy();
                            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Get_Energy);
                            VibrateSystem.Haptic(HapticTypes.Selection);
                            currenciesTran.transform.DOScale(1f, 0.2f).SetEase(Ease.OutQuad).SetDelay(0.1f);
                        };
                    }));
            }
            else
            {
                RefreshCurrency_Energy();
            }
        }
        else if (mergeRewardItem.IsRewardGems)
        {
            Currencies.Add(CurrencyID.Gems, mergeRewardItem.num, season, false);
            //播放动画
            if (needFlyCurrency && GetCurrenciesTran(false, CurrencyID.Gems, out Transform currenciesTran))
            {
                StartCoroutine(PlayTweenRewardFly(mergeRewardItem, currenciesTran.position, startWorldPos,
                    () =>
                    {
                        currenciesTran.DOKill(true);
                        currenciesTran.DOScale(1.2f, 0.2f).SetEase(Ease.InQuad).onComplete = () =>
                        {
                            RefreshCurrency_Gems();
                            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Get_Gems);
                            VibrateSystem.Haptic(HapticTypes.Selection);
                            currenciesTran.transform.DOScale(1.0f, 0.2f).SetEase(Ease.OutQuad).SetDelay(0.1f);
                        };
                    }));
            }
            else
            {
                RefreshCurrency_Gems();
            }
        }
        else if (mergeRewardItem.IsNeedle)
        {
            Vector3 tagetPos;
            Transform icon_play = null;
            GameObject uipanel_Merge = GameObject.Find(Consts.UIPanel_Merge);
            Transform needleNumTrans = null;
            if (uipanel_Merge != null && uipanel_Merge.activeSelf)
            {
                UIPanel_Merge uIPanel = uipanel_Merge.GetComponent<UIPanel_Merge>();
                if (uIPanel != null)
                {
                    tagetPos = uIPanel.t_LeftNeedleNum.transform.position;
                    needleNumTrans = uIPanel.t_LeftNeedleNum.transform;
                }
                else
                {
                    tagetPos = new Vector3(-1.5f, 4.2f, -50f);
                }
            }
            else
            {
                if (icon_play == null)
                    icon_play = UISystem.Instance.uiMainMenu.HVPageView.Icons[2].transform.Find("IconPlayBg");
                if (icon_play != null)
                    tagetPos = icon_play.position;
                else
                {
                    GameDebug.LogError("Not Find Icon_Play");
                    tagetPos = Vector3.zero;
                }

                GameDebug.Log(tagetPos);
            }

            StartCoroutine(PlayTweenRewardFly(mergeRewardItem, tagetPos, startWorldPos, () =>
            {
                if (needleNumTrans != null)
                {
                    needleNumTrans.transform.DOScale(1.6f, 0.2f).SetEase(Ease.InQuad).onComplete = () =>
                    {
                        Currencies.Add(CurrencyID.Needle, mergeRewardItem.num, season, false);
                        RefreshCurrency_Needle();
                        if (uipanel_Merge != null && uipanel_Merge.activeSelf)
                            MergeController.CurrentController.RefreshSelectItemInfo();
                        needleNumTrans.transform.DOScale(1f, 0.2f).SetEase(Ease.OutQuad).SetDelay(0.1f);
                    };
                }
                else
                {
                    Currencies.Add(CurrencyID.Needle, mergeRewardItem.num, season, false);
                    RefreshCurrency_Needle();
                }
            }));
        }
        else if (mergeRewardItem.IsRewardLove)
        {
            MergeLevelManager mergeLevel = MergeLevelManager.Instance;
            if (mergeLevel.IsDailyActive(mergeLevel.CurrentLevelType))
            {
                DailyActiveSystem.Instance.AddDailyLoveScore(mergeRewardItem.num);
            }
            else
            {
                DungeonSystem.Instance.AddDungeonScore(mergeLevel.CurrentLevelType, mergeRewardItem.num);
            }

            //播放动画
            if (UISystem.Instance.TryGetUI<UIPanel_Merge>(Consts.UIPanel_Merge, out UIPanel_Merge mergeUI) &&
                mergeUI.isActiveAndEnabled == true)
            {
                if (needFlyCurrency)
                {
                    StartCoroutine(PlayTweenRewardFly(mergeRewardItem, mergeUI.DungeonLoveTrans.position,
                        startWorldPos, () =>
                        {
                            mergeUI.DungeonLoveTrans.DOKill(true);
                            mergeUI.DungeonLoveTrans.DOScale(1.2f, 0.2f).SetEase(Ease.InQuad).onComplete = () =>
                            {
                                mergeUI.RefreshTaskItem();
                                mergeUI.DungeonLoveTrans.transform.DOScale(1.0f, 0.2f).SetEase(Ease.OutQuad)
                                    .SetDelay(0.1f);
                            };
                        }));
                }
                else
                {
                    mergeUI.RefreshTaskItem();
                }
            }
        }
        else if (mergeRewardItem.IsRewardSubEXP)
        {
            if(MergeLevelManager.Instance.CurrentLevelType != MergeLevelType.branch_SpurLine4
                && MergeLevelManager.Instance.CurrentLevelType != MergeLevelType.branch_SpurLine5
                && MergeLevelManager.Instance.CurrentLevelType != MergeLevelType.branch_SpurLine6
                ) 
            {
                return;
            }   
            //播放动画
            if (UISystem.Instance.TryGetUI(Consts.UIPanel_Merge, out UIPanel_Merge mergeUI) &&
                mergeUI.isActiveAndEnabled == true)
            {
                BranchSystem.Instance.AddBranchPoint(mergeRewardItem.num);
                if (needFlyCurrency)
                {
                    StartCoroutine(PlayTweenRewardFly(mergeRewardItem, mergeUI.BranchPointTrans.position,
                        startWorldPos, () =>
                        {
                            mergeUI.BranchPointTrans.DOKill(true);
                            mergeUI.BranchPointTrans.DOScale(1.2f, 0.2f).SetEase(Ease.InQuad).OnComplete(() =>
                           {
                               mergeUI.RefreshTaskItem();
                               mergeUI.BranchPointTrans.transform.DOScale(1.0f, 0.2f).SetEase(Ease.OutQuad)
                                   .SetDelay(0.1f);
                           });
                        }));
                }
                else
                {
                    mergeUI.RefreshTaskItem();
                }
            }
        }
        else if (mergeRewardItem.IsRewardBranchPoint)
        {
            if (UISystem.Instance.TryGetUI<UIPanel_BranchTask>("UIPanel_BranchTask", out var uiPanel_BranchTask)
                && uiPanel_BranchTask.isActiveAndEnabled)
            {
                return;
            }

            StartCoroutine(PlayTweenRewardFly(mergeRewardItem, UISystem.Instance.branch_Slider.transform.position,
                startWorldPos, () => { UISystem.Instance.branch_Slider.DoAniOut(); }));
            //do nothing;
        }
        else if (mergeRewardItem.name == "mail_weeklyCard")
        {
            // 月卡补单
            GameDebug.LogWarning("月卡补单");
            if (GiftPackageDefinition.GiftDefinitionsDict.TryGetValue(1006, out GiftPackageDefinition m_GiftDefinition))
            {
                List<MergeRewardItem> rewardItems = new List<MergeRewardItem>();
                List<MergeRewardItem> coinOrGemList = m_GiftDefinition.CoinOrGemRewardList;
                for (int i = 0; i < coinOrGemList.Count; i++)
                {
                    rewardItems.Add(coinOrGemList[i]);
                }

                foreach (var item in m_GiftDefinition.ItemRewardList)
                {
                    rewardItems.Add(item);
                }

                GameManager.Instance.GiveRewardItem(rewardItems, "Billing", Vector3.zero);

                DateTimeOffset timeDate = TimeManager.ServerUtcNow();
                PlayerData.BuyMonthlyCardTime = timeDate;
                PlayerData.ReceiveRewardsTime = timeDate;
                GameManager.Instance.SavePlayerData_BuyMonthlyCardTime();
                GameManager.Instance.SavePlayerData_ReceiveMonthlyCardRewardTime();

                UISystem.Instance?.uiMainMenu?.pageShop?.uI_ShopContainer?.shop_MonthlyCard?.RefreshBtnActive();
            }
        }
        //发邮件补单ad广告
        else if (mergeRewardItem.name == "mail_skipAd")
        {
            PlayerData.BuyADTime = TimeManager.Instance.UtcNow();
            SavePlayerData_ReceivebuyADTime();
        }
        else if (mergeRewardItem.name == "mail_battlepass")
        {
            //bp补单
            GameDebug.LogWarning("battlepass补单");
            BattlePassSystem.Instance.SuccessBuyBattlePassCB();
        }
        else
        {
            //是物品，放入礼包中
            if (AssetSystem.Instance.ContainsKey(mergeRewardItem.name))
            {
                RewardBoxManager.Instance.AddRewardItem(levelType, mergeRewardItem);
            }
            else
            {
                GameDebug.LogError("add item name error! :" + mergeRewardItem.name);
            }

            //播放动画
            if (needFlyCurrency)
            {
                Vector3 tagetPos = GetGoodsTagetPos();
                StartCoroutine(PlayTweenRewardFly(mergeRewardItem, tagetPos, startWorldPos, () => { }));
            }
        }
    }

    private Vector3 GetGoodsTagetPos()
    {
        Vector3 tagetPos;
        Transform icon_play = null;
        GameObject mergePanel = GameObject.Find(Consts.UIPanel_Merge);
        if (mergePanel != null && mergePanel.activeSelf)
        {
            UIPanel_Merge uIPanel = mergePanel.GetComponent<UIPanel_Merge>();
            if (uIPanel != null)
            {
                tagetPos = uIPanel.btn_spawn.transform.position;
            }
            else
            {
                GameDebug.LogError(0);
                tagetPos = new Vector3(-1.9f, 3.7f, -50f);
            }
        }
        else
        {
            if (icon_play == null)
                icon_play = UISystem.Instance.uiMainMenu.HVPageView.Icons[2].transform.Find("IconPlayBg");
            if (icon_play != null)
                tagetPos = icon_play.position;
            else
            {
                GameDebug.LogError("Not Find Icon_Play");
                tagetPos = Vector3.zero;
            }

            GameDebug.Log(tagetPos);
        }

        return tagetPos;
    }

    private IEnumerator PlayTweenRewardFly(MergeRewardItem rewardItem, Vector3 targetWorldPos,
        Vector3 startWorldPos, Action oneFinishCB)
    {
        bool showTween = false;
        int showCount = 1;
        Vector3 startScale;
        if (rewardItem.IsRewardBranchPoint)
        {
            UISystem.Instance.branch_Slider.RefreshUI(false);
            yield return new WaitForSeconds(1.5f);
            UISystem.Instance.branch_Slider.DoAniIN();
            yield return new WaitForSeconds(1f);
            targetWorldPos = UISystem.Instance.branch_Slider.transform.position;
        }

        if (rewardItem.IsRewardCoins || rewardItem.IsRewardGems || rewardItem.IsRewardEnergy)
        {
            startScale = new Vector3(0.8f, 0.8f, 1f);
        }
        else
        {
            startScale = Vector3.one;
        }

        if (rewardItem.num >= 10000)
        {
            showCount = 20;
        }
        else if (rewardItem.num >= 5000)
        {
            showCount = 15;
        }
        else if (rewardItem.num >= 300)
        {
            showCount = 10;
        }
        else if (rewardItem.num >= 5)
        {
            showCount = 5;
        }
        else
        {
            showCount = rewardItem.num;
        }


        List<GameObject> gOList = new List<GameObject>();
        for (int i = 0; i < showCount; i++)
        {
            GameObject gO =
                AssetSystem.Instance.Instantiate(rewardItem.ShowRewardPrefabName, UISystem.Instance.topRootTran);
            if (gO == null)
            {
                yield break;
            }

            gO.transform.position = new Vector3(startWorldPos.x, startWorldPos.y, 0);
            gO.SetActive(false);
            gOList.Add(gO);
        }

        yield return null;
        bool isHaptic = false;
        for (int i = 0; i < showCount; i++)
        {
            int gOIndex = i;
            if (gOList[gOIndex] == null)
            {
                continue;
            }

            gOList[gOIndex].SetActive(true);
            Transform tweenRootTran = gOList[gOIndex].transform;
            CanvasGroup animatedAlpha = tweenRootTran.gameObject.AddComponent<CanvasGroup>();
            Sequence sequence = DOTween.Sequence();

            float dis = Vector2.Distance(targetWorldPos, tweenRootTran.position);

            Vector2 random = UnityEngine.Random.insideUnitCircle;
            Vector2 p0 = new Vector2(startWorldPos.x, startWorldPos.y) + random * 0.5f;
            Vector2 p2 = new Vector2(targetWorldPos.x, targetWorldPos.y);
            Vector2 p1 = new Vector2(p0.x, (p0.y + p2.y) * 0.5f);

            tweenRootTran.localScale = startScale;

            sequence.Append(tweenRootTran.DOMove(p0, 0.7f)).SetEase(Ease.OutQuad);

            float duration = 0.1f + dis * 0.1f;
            sequence.Append(DOTween.To(setter: value =>
            {
                Vector2 vector = DoAnimTools.Bezier(p0, p1, p2, value);
                tweenRootTran.position = new Vector3(vector.x, vector.y, 0);
            }, startValue: 0, endValue: 1, duration: duration).SetEase(Ease.InQuad));

            sequence.Insert(0.7f, tweenRootTran.DOScale(0.6f, duration - 0.05f).SetEase(Ease.InQuart));
            sequence.Insert(0.7f + duration - 0.1f,
                DOTween.To(setter: value => { animatedAlpha.alpha = value; }, startValue: 1, endValue: 0,
                    duration: 0.1f).SetEase(Ease.InQuad));
            sequence.InsertCallback(0.7f + duration - 0.05f, () =>
            {
                if (showTween)
                {
                    return;
                }

                showTween = true;
                oneFinishCB?.Invoke();
            });
            sequence.onComplete = () =>
            {
                tweenRootTran.position = new Vector3(targetWorldPos.x, targetWorldPos.y, 0);
                tweenRootTran.gameObject.SetActive(false);
                animatedAlpha.alpha = 1;
                if (rewardItem.IsRewardBranchPoint)
                    UISystem.Instance.branch_Slider.RefreshUI(true);
                if (!isHaptic)
                    VibrateSystem.Haptic(MoreMountains.NiceVibrations.HapticTypes.Selection);
                isHaptic = true;
                var alpha = tweenRootTran.gameObject.GetComponent<CanvasGroup>();
                if (alpha != null)
                {
                    Destroy(alpha);
                }

                AssetSystem.Instance.DestoryGameObject(rewardItem.ShowRewardPrefabName, tweenRootTran.gameObject);
            };
            sequence.Play();
        }
    }


    private bool GetTrans(bool isExp, CurrencyID currencyID, out Transform currencyTran,
        TopResourceManager topResourceManager)
    {
        if (topResourceManager == null
            || topResourceManager.gO_Exp == null
            || topResourceManager.gO_Coin == null
            || topResourceManager.gO_Gem == null
            || topResourceManager.gO_Energy == null)
        {
            currencyTran = null;
            return false;
        }

        if (isExp)
        {
            currencyTran = topResourceManager.gO_Exp.transform;
            return true;
        }

        switch (currencyID)
        {
            case CurrencyID.Coins:
                currencyTran = topResourceManager.gO_Coin.transform;
                return true;
            case CurrencyID.Gems:
                currencyTran = topResourceManager.gO_Gem.transform;
                return true;
            case CurrencyID.Energy:
                currencyTran = topResourceManager.gO_Energy.transform;
                return true;
        }

        currencyTran = null;
        return false;
    }


    private bool GetCurrenciesTran(bool isExp, CurrencyID currencyID, out Transform currencyTran)
    {
        if (UISystem.Instance.topResourceManager != null)
            return GetTrans(isExp, currencyID, out currencyTran, UISystem.Instance.topResourceManager);
        //if (UISystem.Instance.HasUI && UISystem.Instance.TryGetUI<UI_InternalShop>(Consts.UI_InternalShop, out UI_InternalShop shopUI))
        //    return GetTrans(isExp, currencyID, out currencyTran, shopUI.topResourceManager);
        //else if (UISystem.Instance.HasUI && UISystem.Instance.TryGetUI<TopPopup>(Consts.UI_TopPopup, out TopPopup topPopUI) && topPopUI.isActiveAndEnabled == true)
        //    return GetTrans(isExp, currencyID, out currencyTran, topPopUI.topResourceManager);
        //else if (UISystem.Instance.HasUI && UISystem.Instance.TryGetUI<UIPanel_Collection>(Consts.UIPanel_Collection, out UIPanel_Collection uIPanel_Collection))
        //    return GetTrans(isExp, currencyID, out currencyTran, uIPanel_Collection.topResourceManager);
        //else if (UISystem.Instance.HasUI && UISystem.Instance.TryGetUI<UIPanel_Merge>(Consts.UIPanel_Merge, out UIPanel_Merge mergeUI) && mergeUI.isActiveAndEnabled == true)
        //    return GetTrans(isExp, currencyID, out currencyTran, mergeUI.topResourceManager);
        else
        {
            currencyTran = null;
            return false; //GetTrans(isExp, currencyID, out currencyTran, uIPanel_LevelSelect.topResourceManager);
        }
    }

    public string GetDiscountTxt(double f)
    {
        f = Math.Round(f, 1);
        string discountStr = "";
        switch (f)
        {
            case 0.1:
                discountStr = I2.Loc.ScriptLocalization.Get("Obj/Shop/OFF/Convert1");
                break;
            case 0.2:
                discountStr = I2.Loc.ScriptLocalization.Get("Obj/Shop/OFF/Convert2");
                break;
            case 0.3:
                discountStr = I2.Loc.ScriptLocalization.Get("Obj/Shop/OFF/Convert3");
                break;
            case 0.4:
                discountStr = I2.Loc.ScriptLocalization.Get("Obj/Shop/OFF/Convert4");
                break;
            case 0.5:
                discountStr = I2.Loc.ScriptLocalization.Get("Obj/Shop/OFF/Convert5");
                break;
            case 0.6:
                discountStr = I2.Loc.ScriptLocalization.Get("Obj/Shop/OFF/Convert6");
                break;
            case 0.7:
                discountStr = I2.Loc.ScriptLocalization.Get("Obj/Shop/OFF/Convert7");
                break;
            case 0.8:
                discountStr = I2.Loc.ScriptLocalization.Get("Obj/Shop/OFF/Convert8");
                break;
            case 0.9:
                discountStr = I2.Loc.ScriptLocalization.Get("Obj/Shop/OFF/Convert9");
                break;
            default:
                discountStr = I2.Loc.ScriptLocalization.Get("Obj/Shop/OFF/Convert10");
                break;
        }

        return discountStr;
    }

    public string GetDiscountStr(double f)
    {
        return GetDiscountTxt(f) + I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part2Text5");
    }

    public string GetDiscountStrWhith_N(double f)
    {
        return GetDiscountTxt(f) + "\n" + I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part2Text5");
    }

    //public void OpenTaskView(string taskId)
    //{
    //    StartCoroutine(DelayOpenTaskView(taskId));
    //}
    //private IEnumerator DelayOpenTaskView(string taskId)
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    UISystem.Instance.ShowUI(new UIPanelData_MainLineTaskTip(taskId));
    //}

    bool lastSecontCanWatchAD = true; //上一秒是否可以观看广告
    bool hasShowDailyTask = false;
    bool hasShowPlayerLevelReward = false; //经验等级
    bool hasShowStarReward = false;
    bool hasShowLevelReward = false;

    public void TryShowTopTopNoticeBar(TopNoticeType noticeType = TopNoticeType.None)
    {
        if (noticeType == TopNoticeType.None)
        {
        }

        switch (noticeType)
        {
            case TopNoticeType.ADReady:
                if (AdManager.CanShowAD_Normal(AdManager.ADTag.Energy))
                {
                    UISystem.Instance.ShowUI(new UIPanelData_TopNoticeBar(TopNoticeType.ADReady));
                }

                break;
            //case TopNoticeType.CompleteDailyTask:
            //    if (DailyTaskSystem.Instance.HasRewerdCanClaim())
            //    {
            //        if (!hasShowDailyTask)
            //        {
            //            UISystem.Instance.ShowUI(new UIPanelData_TopNoticeBar(TopNoticeType.CompleteDailyTask));
            //            hasShowDailyTask = true;
            //        }                 
            //    }
            //    else 
            //    {
            //        hasShowDailyTask = false;
            //    }
            //    break;
            //case TopNoticeType.PlayerLevelRewardReady:
            //    int needExp = playerData.NextExpLevelNeedExp;
            //    int curExp = playerData.UnCollectedExp >= playerData.NextExpLevelNeedExp
            //        ? playerData.NextExpLevelNeedExp : playerData.UnCollectedExp;
            //    if (curExp >= needExp)
            //    {
            //        if (!hasShowPlayerLevelReward) 
            //        {
            //            UISystem.Instance.ShowUI(new UIPanelData_TopNoticeBar(TopNoticeType.PlayerLevelRewardReady));
            //            hasShowPlayerLevelReward = true;
            //        }
            //    }
            //    else
            //    {
            //        hasShowPlayerLevelReward = false;
            //    }
            //    break;
            //case TopNoticeType.StarRewardReady:
            //    if (MergeStarRewardDefinition.starRewards.TryGetValue(SaveUtils.GetInt(Consts.StarRewardLastGotID) + 1, out MergeStarRewardDefinition mergeStarRewardDefinition))
            //    {
            //        if (TaskGoalsManager.Instance.YellowStarNum >= mergeStarRewardDefinition.StarCount)
            //        {
            //            if (!hasShowStarReward)
            //            {
            //                UISystem.Instance.ShowUI(new UIPanelData_TopNoticeBar(TopNoticeType.StarRewardReady));
            //                hasShowStarReward = true;
            //            }
            //        }
            //        else 
            //        {
            //            hasShowStarReward = false;
            //        }
            //    }
            //    break;
            //case TopNoticeType.LevelRewardReady:
            //    if (MergeChapterRewardDefinition.chapterRewards.TryGetValue(SaveUtils.GetInt(Consts.ChapterRewardLastGotId) + 1, out MergeChapterRewardDefinition mergeChapterRewardDefinition))
            //    {
            //        if (TaskGoalsManager.Instance.curLevelIndex > mergeChapterRewardDefinition.Chapter)
            //        {
            //            if (!hasShowLevelReward)
            //            {
            //                UISystem.Instance.ShowUI(new UIPanelData_TopNoticeBar(TopNoticeType.LevelRewardReady));
            //                hasShowLevelReward = true;
            //            }
            //        }
            //        else 
            //        {
            //            hasShowLevelReward = false;
            //        }
            //    }
            //    break;
            default:
                break;
        }
    }

    public event Action<string> playAdFail;

    public void AddPlayADFailEvent()
    {
        playAdFail += (string callback) =>
        {
            if (callback == "2")
            {
                TryShowTopTopNoticeBar(TopNoticeType.ADReady);
            }
        };
    }

    public bool success = false;
    bool hasAddEvent = false;
    public AdManager.ADTag adTag { get; private set; } = AdManager.ADTag.None;
    public List<MergeRewardItem> rewardList { get; private set; } = new List<MergeRewardItem>();

    public void PlayAdFail(List<MergeRewardItem> _rewardList, AdManager.ADTag adTag)
    {
        rewardList.Clear();
        for (int i = 0; i < _rewardList.Count; i++)
        {
            rewardList.Add(_rewardList[i]);
        }

        this.adTag = adTag;
        if (!hasAddEvent)
        {
            RiseSdkListener.OnAdLoaded += playAdFail;
            hasAddEvent = true;
        }
    }

    public void RemovePlayAdFailEvent()
    {
        if (hasAddEvent)
        {
            RiseSdkListener.OnAdLoaded -= playAdFail;
            hasAddEvent = false;
        }
    }


    float timeCount = 0; //每秒刷新

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!UISystem.Instance.HasInit)
                return;
            if (UI_TutorManager.Instance.IsTutoring())
                return;
            if (UISystem.Instance.HasUIInShow)
                return;
            if (UISystem.Instance.TryGetUI(Consts.UIPanel_Merge, out UIPanel_Merge uIPanel_Merge) &&
                uIPanel_Merge.gameObject.activeInHierarchy)
                return;

            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_ExitGame, UIShowLayer.Top));
        }

        timeCount += Time.deltaTime;
        if (timeCount >= 1)
        {
            timeCount--;
            TryShowTopTopNoticeBar(TopNoticeType.CompleteDailyTask);
        }
    }

    public void SkipLevelByEditor()
    {
#if UNITY_EDITOR
        if (DebugSetting.CanUseDebugConfig(out SO_DebugConfig sO_Debug))
        {
            if (sO_Debug.needClearMapByShipLevel)
            {
                if (MergeLevelManager.Instance.totalMapDataDict.TryGetValue(MergeLevelManager.MapName_Main,
                        out MergeMapData mapData))
                {
                    foreach (var item in mapData.gridDataDict)
                    {
                        mapData.ChangeItemData(item.Key, null);
                    }
                }
            }

            if (sO_Debug.SkipToLevel > 0 && sO_Debug.SkipToLevel <= PlayerMaxLevel)
            {
                TaskGoalsManager.Instance.SkipLevelByEditor(sO_Debug.SkipToLevel);
            }
            else if (sO_Debug.SkipToLevel == 0)
            {
            }
            else
            {
                GameDebug.LogError(string.Format("关卡必须在1到{0}之间", PlayerMaxLevel));
            }
        }
#endif
    }
}