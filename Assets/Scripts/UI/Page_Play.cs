using IvyCore;
using Spine.Unity;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using Ivy.Activity.CrossPromotion;
using Ivy.Consts;

public class Page_Play : UIPanelBase
{
    [SerializeField] private Transform trans_Spine;
    [SerializeField] TextMeshProUGUI text_startGame;
    [Header("关卡宝箱")]
    [SerializeField] GameObject chapterBoxReadyObj;
    [SerializeField] TextMeshProUGUI text_chapterBoxOpen;
    [SerializeField] Slider slider_chapter;
    [SerializeField] TextMeshProUGUI text_curLevelCount;
    [SerializeField] TextMeshProUGUI text_goalLevelCount;
    [SerializeField] GameObject chapterBoxClosedObj;
    [SerializeField] TextMeshProUGUI text_chapterBoxName;
    [SerializeField] TextMeshProUGUI text_chapterReachLevel;
    //[SerializeField] TextMeshProUGUI text_chapterLevel;
    [SerializeField] Animator ani_chapterBoxIcon;

    [Header("星级宝箱")]
    [SerializeField] GameObject starBoxReadyObj;
    [SerializeField] TextMeshProUGUI text_starBoxOpen;

    [SerializeField] GameObject starBoxClosedObj;
    [SerializeField] TextMeshProUGUI text_starBoxName;
    [SerializeField] Slider slider_star;
    [SerializeField] TextMeshProUGUI text_curStarCount;
    [SerializeField] TextMeshProUGUI text_goalStarCount;
    [SerializeField] Animator ani_starBoxIcon;



    [Header("大转盘")]
    [SerializeField] GameObject spinWheelObj;
    [SerializeField] GameObject spinWheelNotConnect;
    [SerializeField] TextMeshProUGUI text_spinWheelFreeTime;
    [SerializeField] GameObject spinWheelMarkHint;

    [Header("Buttons")]
    [SerializeField] private Button Btn_Start;
    [SerializeField] private Button Btn_ChapterChect;
    [SerializeField] private Button Btn_StarChest;
    [SerializeField] private Button Btn_SpinWheel;
    [SerializeField] private TextMeshProUGUI t_SpinWheel;
    [SerializeField] private Button Btn_DailyTask;
    [SerializeField] private TextMeshProUGUI t_DailyTask;
    [SerializeField] private GameObject dailyTaskRedPoint;
    [SerializeField] private Button Btn_GoldReward;
    [SerializeField] private TextMeshProUGUI t_GoldReward;
    [SerializeField] private GameObject battlepassRedPoint;
    [SerializeField] private Button btn_ChooseSkin;
    [SerializeField] private Button btn_Telescope;
    [SerializeField] private GameObject arrow;

    [Header("礼包")]
    [SerializeField] private Button Btn_Novice;
    [SerializeField] private TextMeshProUGUI t_Novice;
    [SerializeField] private TextMeshProUGUI t_NoviceTime;
    [SerializeField] private Button Btn_DressUp;
    [SerializeField] private TextMeshProUGUI t_DressUp;
    [SerializeField] private TextMeshProUGUI t_DressUpTime;
    [SerializeField] private Button Btn_Gourmet;
    [SerializeField] private TextMeshProUGUI t_Gourmet;
    [SerializeField] private TextMeshProUGUI t_GourmetTime;

    [SerializeField] private Button Btn_InAppMessage;
    [SerializeField] private TextMeshProUGUI t_InAppMessage;
    [SerializeField] private TextMeshProUGUI t_InAppMessageTime;
    [SerializeField] private Button Btn_IAMCrossPromotion;
    [SerializeField] private TextMeshProUGUI t_IAMCrossPromotionTime;
    [SerializeField] private GameObject icon_IAMCrossPromotion_elves;
    [SerializeField] private GameObject icon_IAMCrossPromotion_farmtown;
    [Header("支线活动")]
    [SerializeField] private Button Btn_Branch;
    [SerializeField] private TextMeshProUGUI t_Branch;
    [SerializeField] private TextMeshProUGUI t_BranchTime;
    [Header("万圣节")]
    [SerializeField] private Button Btn_Halloween;
    [SerializeField] private Image img_Halloween;
    [SerializeField] private TextMeshProUGUI t_Halloween;
    [SerializeField] private TextMeshProUGUI t_HalloweenTime;
    [Header("圣诞节")]
    [SerializeField] private Button btn_Christmas;
    [SerializeField] private Image img_Christmas;
    [SerializeField] private TextMeshProUGUI t_Christmas;
    [SerializeField] private TextMeshProUGUI t_ChristmasTime;
    [Header("情人节")]
    [SerializeField] private Button btn_Lover;
    [SerializeField] private Image img_Lover;
    [SerializeField] private TextMeshProUGUI t_Lover;
    [SerializeField] private TextMeshProUGUI t_LoverTime;
    [Header("活动进度条")]
    [SerializeField] private UI_PagePlay_Slider uI_PagePlay_Slider;
    [SerializeField] private RectTransform actityTrans;
    [SerializeField] private RectTransform boxTrans;

    [Header("每日活动")]
    [SerializeField] private Button btn_Daily;
    [SerializeField] private Image img_Daily;
    [SerializeField] private TextMeshProUGUI t_DailyCountDown;

    public static Action refreshDailyTaskRedPoint;
    public static Action refreshBattlepassRedPoint;
    public static Action<bool> checkIsHasSlider;

    public override void OnInitUI()
    {

        RectTransform rect = transform.GetComponent<RectTransform>();
        actityTrans.sizeDelta = new Vector2(rect.sizeDelta.x, actityTrans.sizeDelta.y);
        boxTrans.sizeDelta = new Vector2(rect.sizeDelta.x, boxTrans.sizeDelta.y);
        base.OnInitUI();
        RegisterBtnListner();
        RegisterRefreshEvent();
        RefrehView();
        RefreshChapterProgress();
        refreshDailyTaskRedPoint += () => { RefreshDailyTaskRedPoint(); };
        refreshBattlepassRedPoint += () => { RefreshBattlepassRedPoint(); };
        //UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_Evaluate));
        startPosX = btn_Telescope.transform.parent.localPosition.x;
    }


    public void RefrehView()
    {
        RefreshBackground();
        RefreshLanguageText();
        RefreshDailyTaskRedPoint();
        RefreshBattlepassRedPoint();
        uI_PagePlay_Slider.CheckRefreshUI();
    }
    private void RefreshChapterProgress()
    {
        var lastGotRewardIndex = SaveUtils.GetInt(Consts.ChapterRewardLastGotId);
        var curId = lastGotRewardIndex + 1;
        if (MergeChapterRewardDefinition.chapterRewards.TryGetValue(curId, out MergeChapterRewardDefinition chestDef))
        {
            text_curLevelCount.text = TaskGoalsManager.Instance.curLevelIndex.ToString();
            text_goalLevelCount.text = (chestDef.Chapter + 1).ToString();
            float cur_value = TaskGoalsManager.Instance.curLevelIndex * 1.0f / (chestDef.Chapter + 1);
            slider_chapter.value = cur_value;
        }
    }


    private void RegisterBtnListner()
    {
        Btn_Start.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            if (DebugSetting.CanUseDebugMap(out var debugMap) && debugMap.Debug_EnterDebugMap)
            {
                UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_TestMap, UIShowLayer.TopPopup));
                return;
            }
#endif

            VibrateSystem.Haptic(MoreMountains.NiceVibrations.HapticTypes.Selection);

            if (GameManager.Instance.playerData.CurrentPlayLevel != TaskGoalsManager.Instance.curLevelIndex)
            {
                GameManager.Instance.playerData.CurrentPlayLevel = TaskGoalsManager.Instance.curLevelIndex;
                AnalyticsUtil.TrackEvent("level_" + GameManager.Instance.playerData.CurrentPlayLevel + "_start");
            }

            AdManager.ManualEnterMergePanel = true;
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            MergeLevelManager.Instance.ShowMergePanelByDungeonType(MergeLevelType.mainLine);
            AchievementManager.Instance.UpdateAchievement(AchievementType.chapter, TaskGoalsManager.Instance.curLevelIndex);
        });
        Btn_SpinWheel.gameObject.SetActive(false);
        Btn_SpinWheel.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_Spin));
        });
        Btn_ChapterChect.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_LevelRewards, UIShowLayer.TopPopup));
        });

        Btn_StarChest.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_StarRewards, UIShowLayer.TopPopup));
        });
        Btn_DailyTask.gameObject.SetActive(false);
        Btn_DailyTask.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_DailyTask));
        });
        Btn_GoldReward.gameObject.SetActive(false);
        Btn_GoldReward.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            if (!BattlePassSystem.Instance.HasInitBattlePassSuccess())
                return;
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_BattlePass));
        });

        DateTimeOffset timeOffset = GiftPackageManager.Instance.PushGiftEndTime;
        TimeSpan timeSpan = timeOffset - DateTimeOffset.UtcNow;
        //礼包控制
        if (GiftPackageManager.Instance.GiftIndexList.Count > 0 && timeSpan.TotalSeconds > 0)
        {
            GiftPackageDefinition.GiftDefinitionsDict.TryGetValue(GiftPackageManager.Instance.GiftIndexList[0], out GiftPackageDefinition definition);
            Btn_Novice.transform.parent.gameObject.SetActive(definition.GiftType == GiftPackageType.NovicePackage);
            Btn_DressUp.transform.parent.gameObject.SetActive(definition.GiftType == GiftPackageType.DressUpPackage);
            Btn_Gourmet.transform.parent.gameObject.SetActive(definition.GiftType == GiftPackageType.GourmetPackage);
        }
        Btn_Novice.onClick.AddListener(() =>
        {
            //AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            //UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_NovicePackage));
            GiftPackageManager.Instance.OpenGiftPackageView();
        });

        Btn_DressUp.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_DressUpPackage, UIShowLayer.TopResouce));
        });
        Btn_Gourmet.onClick.AddListener(() =>

        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_GourmetPackage));
        });

        Btn_InAppMessage.onClick.AddListener(() =>
        {
            if (InAppMessageSystem.Instance.CurrentPackData != null)
                UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_InAppMessage));
        });

        Btn_IAMCrossPromotion.onClick.AddListener(() =>
        {
            if (Activity_CrossPromotion.Instance.InitState == Ivy.Activity.ActivityModuleState.Success)
            {
                if (Activity_CrossPromotion.Instance.HasPromotion())
                    UISystem.Instance.ShowUI(new UIPanelData_CrossPromotion(null));
            }
        });

        Btn_Branch.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            if (BranchSystem.Instance.GetIsOpen())
                UISystem.Instance.ShowUI(new UIPanelData_BranchRedeem(MergeLevelType.branch1));
        });
        btn_ChooseSkin.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_ChooseSkin));
        });
        btn_Telescope.onClick.AddListener(() =>
        {
            TwennChooseSkinBtn();
        });
        Btn_Halloween.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            if (FestivalSystem.Instance.IsFestivalOpen(MergeLevelType.halloween))
                UISystem.Instance.ShowUI(new UIPanelData_BranchRedeem(MergeLevelType.halloween));
        });
        btn_Christmas.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            if (FestivalSystem.Instance.IsFestivalOpen(MergeLevelType.christmas))
                UISystem.Instance.ShowUI(new UIPanelData_BranchRedeem(MergeLevelType.christmas));
        });
        btn_Lover.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            if (FestivalSystem.Instance.IsFestivalOpen(MergeLevelType.lover))
                UISystem.Instance.ShowUI(new UIPanelData_BranchRedeem(MergeLevelType.lover));
        });
        btn_Daily.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            UISystem.Instance.ShowUI(new UIPanelData_Daily(DailyOpenType.NORMAL));
        });
    }

    private void RegisterRefreshEvent()
    {
        //刷新主页背景
        UI_Manager.Instance.RegisterRefreshEvent(this.gameObject, (str) =>
        {
            RefreshBackground();
        }, "page_Play_RefreshBackground");

        //刷新关卡宝箱
        UI_Manager.Instance.RegisterRefreshEvent(this.gameObject, (str) =>
        {
            StartCoroutine(RefreshChapterChest());
        }, "page_Play_RefreshChapterChest");

        //刷新星星礼包
        UI_Manager.Instance.RegisterRefreshEvent(this.gameObject, (str) =>
        {
            RefreshStarChest();
        }, "page_Play_RefreshStarChest");

        //打开支线兑换界面
        UI_Manager.Instance.RegisterRefreshEvent(this.gameObject, (str) =>
        {
            StartCoroutine(WaitToOpenBranchRedeem());
        }, "page_Play_OpenBranchRedeem");

    }

    #region 功能刷新
    //多语言刷新
    public void RefreshLanguageText()
    {
        RefreshStarChest();
        //t_SpinWheel.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Shop_SpinWheel2");
        t_DailyTask.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/DairyTaskTitle");
        t_GoldReward.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/BatterpassTitle");
        t_Novice.text = I2.Loc.ScriptLocalization.Get("Obj/GiftBag/Novice/Title");
        t_DressUp.text = I2.Loc.ScriptLocalization.Get("Obj/GiftBag/dressUp/Title");
        t_Gourmet.text = I2.Loc.ScriptLocalization.Get("Obj/GiftBag/gourmet/Title");
        t_Branch.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine1Title");
        t_Halloween.text = I2.Loc.ScriptLocalization.Get("Obj/Festival/Festival1Title");
        t_Christmas.text = I2.Loc.ScriptLocalization.Get("Obj/Festival/Festival2Title");
        t_Lover.text = I2.Loc.ScriptLocalization.Get("Obj/Festival/Festival3Title");

        //t_InAppMessage.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part3");
    }

    private void RefreshInAppMessageBtn()
    {
        if (InAppMessageSystem.Instance.CurrentPackData != null
            && InAppMessageSystem.Instance.CurrentPackData.HasShow)
        {
            if (!Btn_InAppMessage.transform.parent.gameObject.activeSelf)
            {
                Btn_InAppMessage.transform.parent.gameObject.SetActive(true);
                t_InAppMessage.text = InAppMessageSystem.Instance.CurrentPackData.titleStr;
            }
        }
        else
        {
            if (Btn_InAppMessage.transform.parent.gameObject.activeSelf)
                Btn_InAppMessage.transform.parent.gameObject.SetActive(false);

        }
    }

    SkeletonGraphic mainViewSpine = null;
    //刷新主页背景
    private void RefreshBackground()
    {
        RefreshBackGroundText();

        AssetSystem.Instance.LoadAsset<SkeletonDataAsset>(ChooseSkinSystem.Instance.curSkinName, (skeletonDataAsset) =>
        {
            AssetSystem.Instance.LoadAsset<Material>("SkeletonGraphicDefault", (material) =>
            {
                if (mainViewSpine != null)
                {
                    Destroy(mainViewSpine.gameObject);
                    mainViewSpine = null;
                }
                mainViewSpine = SkeletonGraphic.NewSkeletonGraphicGameObject(skeletonDataAsset, trans_Spine, material);
                if (mainViewSpine != null)
                {
                    mainViewSpine.AnimationState.SetAnimation(0, "animation", true);
                }
            });
        });


    }

    public void RefreshBackGroundText()
    {
#if UNITY_EDITOR
        if (DebugSetting.CanUseDebugMap(out var debugMap) && debugMap.Debug_EnterDebugMap)
        {
            text_startGame.text = "编辑地图";
            return;
        }
#endif

        if (TaskGoalsManager.Instance.IsCompleteAllChapter)
        {
            text_startGame.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Update_Title");
        }
        else
        {
            text_startGame.text = string.Format(I2.Loc.ScriptLocalization.Get("Obj/Chain/LevelChestDescribe3"), TaskGoalsManager.Instance.curLevelIndex);
        }
    }

    public void SetMainViewSpineOnOrOff(bool on)
    {
        if (mainViewSpine == null)
            return;
        if (on)
            mainViewSpine.AnimationState.SetAnimation(0, "animation", true);
        else
            mainViewSpine.AnimationState.SetEmptyAnimation(0, 1);
    }

    public bool ChestCanOpen = false, StarChestCanOpen = false;

    //刷新关卡宝箱
    private IEnumerator RefreshChapterChest()
    {
        //顺带顺序开始游戏关卡
        if (TaskGoalsManager.Instance.IsCompleteAllChapter)
        {
            text_startGame.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Update_Title");
        }
        else
        {
            text_startGame.text = string.Format(I2.Loc.ScriptLocalization.Get("Obj/Chain/LevelChestDescribe3"), TaskGoalsManager.Instance.curLevelIndex);
        }


        var lastGotRewardIndex = SaveUtils.GetInt(Consts.ChapterRewardLastGotId);
        var curId = lastGotRewardIndex + 1;
        if (MergeChapterRewardDefinition.chapterRewards.TryGetValue(curId, out MergeChapterRewardDefinition chestDef))
        {
            Btn_ChapterChect.gameObject.SetActive(true);
            if (TaskGoalsManager.Instance.curLevelIndex > chestDef.Chapter)
            {
                chapterBoxReadyObj.SetActive(true);
                chapterBoxClosedObj.SetActive(false);
                ani_chapterBoxIcon.enabled = true;
                text_chapterBoxOpen.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Home_Button2");
                ChestCanOpen = true;
            }
            else
            {
                chapterBoxReadyObj.SetActive(false);
                chapterBoxClosedObj.SetActive(true);
                ani_chapterBoxIcon.enabled = false;
                ani_chapterBoxIcon.transform.localRotation = Quaternion.Euler(Vector3.zero);
                text_chapterBoxName.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/LevelChest_Title");
                float pre_value = (TaskGoalsManager.Instance.curLevelIndex - 1) * 1.0f / (chestDef.Chapter + 1);
                float cur_value = TaskGoalsManager.Instance.curLevelIndex * 1.0f / (chestDef.Chapter + 1);

                slider_chapter.value = pre_value;
                text_curLevelCount.text = (TaskGoalsManager.Instance.curLevelIndex - 1).ToString();
                text_goalLevelCount.text = (chestDef.Chapter + 1).ToString();
                MergeRewardItem rewardItem = new MergeRewardItem
                {
                    name = "Lv",
                    num = 10
                };
                Transform transform = slider_chapter.transform.Find("Star");
                yield return new WaitForSeconds(1.5f);
                StartCoroutine(PlayTweenRewardFly_Currency(rewardItem, transform.position, text_startGame.transform.position,
                    () =>
                    {
                        DOTween.To(value => { slider_chapter.value = value; }, pre_value, cur_value, 0.75f).SetEase(Ease.OutQuad).OnComplete(() =>
                        {
                            text_curLevelCount.text = TaskGoalsManager.Instance.curLevelIndex.ToString();
                            text_goalLevelCount.text = (chestDef.Chapter + 1).ToString();
                        });
                    }));
                yield return new WaitForSeconds(1.5f);

                //slider_chapter.transform.DOPunchScale(Vector3.one * 0.2f, 1f).SetAutoKill().OnComplete(() =>
                //{

                //});
                //text_chapterReachLevel.text = string.Format(I2.Loc.ScriptLocalization.Get("Obj/Chain/LevelChestDescribe1"), chestDef.Chapter + 1);
                ChestCanOpen = false;
            }
        }
        else
        {
            Btn_ChapterChect.gameObject.SetActive(false);
        }
    }

    private IEnumerator PlayTweenRewardFly_Currency(MergeRewardItem rewardItem, Vector3 targetWorldPos,
        Vector3 startWorldPos, Action oneFinishCB)
    {
        bool showTween = false;
        int showCount = 1;
        Vector3 startScale;
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

            float duration = 0.1f + dis * 0.05f;
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
    //刷新星星宝箱
    private void RefreshStarChest()
    {
        var lastGotRewardIndex = SaveUtils.GetInt(Consts.StarRewardLastGotID);
        var curId = lastGotRewardIndex + 1;
        if (MergeStarRewardDefinition.starRewards.TryGetValue(curId, out MergeStarRewardDefinition chestDef))
        {
            if (TaskGoalsManager.Instance.YellowStarNum >= chestDef.StarCount)
            {
                starBoxReadyObj.SetActive(true);
                starBoxClosedObj.SetActive(false);
                ani_starBoxIcon.enabled = true;
                text_starBoxOpen.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Home_Button2");
                StarChestCanOpen = true;
            }
            else
            {
                starBoxReadyObj.SetActive(false);
                starBoxClosedObj.SetActive(true);
                ani_starBoxIcon.enabled = false;
                ani_starBoxIcon.transform.localRotation = Quaternion.Euler(Vector3.zero);
                text_starBoxName.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/StarChest_Title");
                slider_star.value = TaskGoalsManager.Instance.YellowStarNum * 1.0f / chestDef.StarCount;
                text_curStarCount.text = TaskGoalsManager.Instance.YellowStarNum.ToString();
                text_goalStarCount.text = chestDef.StarCount.ToString();
                StarChestCanOpen = false;
            }
        }
        else
        {
            Btn_StarChest.gameObject.SetActive(false);
        }
    }
    #endregion

    #region update刷新
    private float updateInterval = 1.1f;
    private void Update()
    {
        updateInterval += Time.deltaTime;
        if (updateInterval >= 1.0f)
        {
            updateInterval--;
            DoUpdateSpinWheelLogic();

            DoUpdatePackageCountDown();
            DoUpdateInAppMessageCountDown();
            DoUpdateIAMCrossPromotionCountDown();
            RefreshDailyTaskRedPoint();
            RefreshBattlepassRedPoint();
            DoUpdateCheckFestivalLogic();
            DoUpdateCheckDailyLogic();
        }
    }

    /// <summary>
    /// 初始化推送礼包剩余倒计时时间
    /// </summary>

    private bool isEnable = false;
    private GiftPackageType giftPackageType;
    private void DoUpdatePackageCountDown()
    {
        TimeSpan timeSpan = GiftPackageManager.Instance.PushGiftEndTime - DateTimeOffset.UtcNow;
        if (timeSpan.TotalSeconds < 0 && isEnable)
        {
            Btn_Novice.transform.parent.gameObject.SetActive(false);
            Btn_DressUp.transform.parent.gameObject.SetActive(false);
            Btn_Gourmet.transform.parent.gameObject.SetActive(false);
            isEnable = false;
        }
        else
        {
            t_NoviceTime.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo((int)timeSpan.TotalSeconds);
            t_DressUpTime.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo((int)timeSpan.TotalSeconds);
            t_GourmetTime.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo((int)timeSpan.TotalSeconds);
            if (GiftPackageManager.Instance.GiftIndexList.Count == 0)
                return;
            GiftPackageDefinition.GiftDefinitionsDict.TryGetValue(GiftPackageManager.Instance.GiftIndexList[0], out GiftPackageDefinition definition);
            if (isEnable && giftPackageType == definition.GiftType)
                return;
            giftPackageType = definition.GiftType;
            Btn_Novice.transform.parent.gameObject.SetActive(giftPackageType == GiftPackageType.NovicePackage);
            Btn_DressUp.transform.parent.gameObject.SetActive(giftPackageType == GiftPackageType.DressUpPackage);
            Btn_Gourmet.transform.parent.gameObject.SetActive(giftPackageType == GiftPackageType.GourmetPackage);
            isEnable = true;
        }
    }

    private void DoUpdateInAppMessageCountDown()
    {
        RefreshInAppMessageBtn();
        if (InAppMessageSystem.Instance.CurrentPackData != null
            && InAppMessageSystem.Instance.CurrentPackData.HasShow
            && InAppMessageSystem.Instance.CurrentPackData.lifeTime > 0)
        {
            t_InAppMessageTime.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(Mathf.CeilToInt((float)(InAppMessageSystem.Instance.CurrentPackData.ShowEndDate - TimeManager.Instance.UtcNow()).TotalSeconds));
        }
    }

    private void DoUpdateIAMCrossPromotionCountDown()
    {
        if (Activity_CrossPromotion.Instance.InitState == Ivy.Activity.ActivityModuleState.Success
            && Activity_CrossPromotion.Instance.HasPromotion())
        {
            if (!Btn_IAMCrossPromotion.transform.parent.gameObject.activeSelf)
            {
                Btn_IAMCrossPromotion.transform.parent.gameObject.SetActive(true);
                string package = Activity_CrossPromotion.Instance.ActivityData.PackageID;
                if (package == LisGameConsts.AppID_Android_MergeFarmtown || package == LisGameConsts.AppID_IOS_MergeFarmtown)
                {
                    icon_IAMCrossPromotion_farmtown.gameObject.SetActive(true);
                    icon_IAMCrossPromotion_elves.gameObject.SetActive(false);
                }
                //else if (package == LisGameConsts.AppID_Android_MergeRomance || package == LisGameConsts.AppID_IOS_MergeRomance)
                //{
                //}
                else
                {
                    icon_IAMCrossPromotion_farmtown.gameObject.SetActive(false);
                    icon_IAMCrossPromotion_elves.gameObject.SetActive(true);
                }
            }

            if (Activity_CrossPromotion.Instance.ActivityData.Claimed)
            {
                t_IAMCrossPromotionTime.text = " " + I2.Loc.ScriptLocalization.Get("Obj/Chain/EctypeDescribe11");
            }
            else
            {
                t_IAMCrossPromotionTime.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(Mathf.CeilToInt((float)(Activity_CrossPromotion.Instance.ActivityData.EndDate - TimeManager.Instance.UtcNow()).TotalSeconds));
            }
        }
        else
        {
            if (Btn_IAMCrossPromotion.transform.parent.gameObject.activeSelf)
                Btn_IAMCrossPromotion.transform.parent.gameObject.SetActive(false);
        }
    }

    //update大转盘
    private void DoUpdateSpinWheelLogic()
    {
        if (ExtensionTool.IsDateToday(PlayerData.SpinWheelFreeTime, TimeManager.Instance.UtcNow()))//同一天，不免费
        {
            spinWheelMarkHint.SetActive(false);
            text_spinWheelFreeTime.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo((int)TimeManager.Instance.GetTomorrowRefreshTimeSpan().TotalSeconds);
        }
        else
        {
            spinWheelMarkHint.SetActive(true);
            text_spinWheelFreeTime.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/SpinWheel_Describe3");
        }
    }

    bool hasInitHalloweenSprite = false;
    bool hasInitChristmasSprite = false;
    bool hasInitLoverSprite = false;
    bool hasInitDailySprite = false;
    private void DoUpdateCheckFestivalLogic()
    {
        //halloween
        if (FestivalSystem.Instance.IsFestivalOpen(MergeLevelType.halloween))
        {
            if (!Btn_Halloween.gameObject.activeSelf)
                Btn_Halloween.gameObject.SetActive(true);
            var leftSeconds = (int)(FestivalSystem.Instance.GetFestivalEndTime(MergeLevelType.halloween) - TimeManager.Instance.UtcNow()).TotalSeconds;
            if (leftSeconds > 0)
            {
                if (!Btn_Halloween.gameObject.activeSelf)
                {
                    Btn_Halloween.gameObject.SetActive(true);
                }
                if (!hasInitHalloweenSprite)
                {
                    AssetSystem.Instance.LoadAsset<Sprite>("RemoteAtlas_Halloween[Icon10_Halloween]", (sprite) =>
                    {
                        img_Halloween.sprite = sprite;
                        hasInitHalloweenSprite = true;
                    });
                }
                t_HalloweenTime.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(leftSeconds);
            }
            else
            {
                Btn_Halloween.gameObject.SetActive(false);
            }
        }
        else
        {
            if (Btn_Halloween.gameObject.activeSelf)
                Btn_Halloween.gameObject.SetActive(false);
        }
        //christmas
        if (FestivalSystem.Instance.IsFestivalOpen(MergeLevelType.christmas))
        {
            if (!btn_Christmas.gameObject.activeSelf)
                btn_Christmas.gameObject.SetActive(true);
            var leftSeconds = (int)(FestivalSystem.Instance.GetFestivalEndTime(MergeLevelType.christmas) - TimeManager.Instance.UtcNow()).TotalSeconds;
            if (leftSeconds > 0)
            {
                if (!btn_Christmas.gameObject.activeSelf)
                {
                    btn_Christmas.gameObject.SetActive(true);
                }
                if (!hasInitChristmasSprite)
                {
                    AssetSystem.Instance.LoadAsset<Sprite>("RemoteAtlas_Christmas[Icon11_Christmas]", (sprite) =>
                    {
                        img_Christmas.sprite = sprite;
                        hasInitChristmasSprite = true;
                    });
                }
                t_ChristmasTime.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(leftSeconds);
            }
            else
            {
                btn_Christmas.gameObject.SetActive(false);
            }
        }
        else
        {
            if (btn_Christmas.gameObject.activeSelf)
                btn_Christmas.gameObject.SetActive(false);
        }
        //lover
        if (FestivalSystem.Instance.IsFestivalOpen(MergeLevelType.lover))
        {
            if (!btn_Lover.gameObject.activeSelf)
                btn_Lover.gameObject.SetActive(true);
            var leftSeconds = (int)(FestivalSystem.Instance.GetFestivalEndTime(MergeLevelType.lover) - TimeManager.Instance.UtcNow()).TotalSeconds;
            if (leftSeconds > 0)
            {
                if (!btn_Lover.gameObject.activeSelf)
                {
                    btn_Lover.gameObject.SetActive(true);
                }
                if (!hasInitLoverSprite)
                {
                    AssetSystem.Instance.LoadAsset<Sprite>("RemoteAtlas_Lover[Icon12]", (sprite) =>
                    {
                        img_Lover.sprite = sprite;
                        hasInitLoverSprite = true;
                    });
                }
                t_LoverTime.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(leftSeconds);
            }
            else
            {
                btn_Lover.gameObject.SetActive(false);
            }
        }
        else
        {
            if (btn_Lover.gameObject.activeSelf)
                btn_Lover.gameObject.SetActive(false);
        }
    }

    private void DoUpdateCheckDailyLogic()
    {
        //每日活动
        if (DailyActiveSystem.Instance.GetNoComplete())
        {
            if (!btn_Daily.gameObject.activeSelf)
                btn_Daily.gameObject.SetActive(true);
            var leftSeconds = (int)(TimeManager.Instance.GetTomorrowRefreshTimeSpan()).TotalSeconds;
            if (leftSeconds > 0)
            {
                if (!btn_Daily.gameObject.activeSelf)
                {
                    btn_Daily.gameObject.SetActive(true);
                }
                string str = string.Empty;
                switch (DailyActiveSystem.Instance.Daily_Type)
                {
                    case MergeLevelType.daily1:
                        str = "UISpriteAtlas_page[daily1]";
                        break;
                    case MergeLevelType.daily2:
                        str = "UISpriteAtlas_page[daily2]";
                        break;
                    case MergeLevelType.daily3:
                        str = "UISpriteAtlas_page[daily3]";
                        break;
                    case MergeLevelType.daily4:
                        str = "UISpriteAtlas_page[daily4]";
                        break;
                    case MergeLevelType.daily5:
                        str = "UISpriteAtlas_page[daily5]";
                        break;
                    case MergeLevelType.daily6:
                        str = "UISpriteAtlas_page[daily6]";
                        break;
                    default:
                        return;
                }
                if (string.IsNullOrEmpty(str))
                {
                    GameDebug.LogError("获取每日活动类型错误");
                }
                if (!hasInitDailySprite)
                {
                    AssetSystem.Instance.LoadAsset<Sprite>(str, (sprite) =>
                    {
                        img_Daily.sprite = sprite;
                        img_Daily.SetNativeSize();
                        hasInitDailySprite = true;
                    });
                }
                t_DailyCountDown.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(leftSeconds);
            }
            else
            {
                btn_Daily.gameObject.SetActive(false);
            }
        }
        else
        {
            if (btn_Daily.gameObject.activeSelf)
                btn_Daily.gameObject.SetActive(false);
        }
    }

    #endregion

    #region 红点刷新
    private void RefreshDailyTaskRedPoint()
    {
        //dailyTaskRedPoint.SetActive(DailyTaskSystem.Instance.HasRewerdCanClaim());
    }
    private void RefreshBattlepassRedPoint()
    {
        battlepassRedPoint.SetActive(BattlePassSystem.Instance.HasRewerdCanClaim());
    }
    #endregion

    private IEnumerator WaitToOpenBranchRedeem()
    {
        yield return new WaitForSeconds(0.4f);
        if (MergeLevelManager.Instance.IsBranch(MergeLevelManager.Instance.CurrentLevelType))
        {
            if (BranchSystem.Instance.GetIsOpen())
                UISystem.Instance.ShowUI(new UIPanelData_BranchRedeem(MergeLevelManager.Instance.CurrentLevelType));
        }
        else
        {
            if (FestivalSystem.Instance.IsFestivalOpen(MergeLevelManager.Instance.CurrentLevelType))
                UISystem.Instance.ShowUI(new UIPanelData_BranchRedeem(MergeLevelManager.Instance.CurrentLevelType));
        }
    }

    bool tweenOut = true;
    float startPosX = 0;
    private void TwennChooseSkinBtn()
    {
        if (tweenOut)
        {
            btn_Telescope.transform.parent.DOLocalMoveX(startPosX - 155, 0.2f).onComplete += () =>
                {
                    arrow.transform.localRotation = new Quaternion(0, 180, 0, 0);
                };
        }
        else
        {
            btn_Telescope.transform.parent.DOLocalMoveX(startPosX, 0.2f).onComplete += () =>
              {
                  arrow.transform.localRotation = new Quaternion(0, 0, 0, 0);
              };
        }
        tweenOut = !tweenOut;
    }
}
