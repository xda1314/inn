using IvyCore;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class UI_MainMenu : UIPanelBase
{
    [SerializeField]
    private ScrollRect _scrollRect;

    [SerializeField]
    private HVPageView _pageView;
    public HVPageView HVPageView
    {
        get
        {
            if (_pageView == null)
                _pageView = transform.GetComponentInChildren<HVPageView>();
            return _pageView;
        }
    }

    [SerializeField] public Page_Shop pageShop;
    [SerializeField] private UIPanel_Collection pageCollection;
    [SerializeField] public Page_Play pagePlay;
    [SerializeField] private Page_Dungeon pageDungeon;
    [SerializeField] public Page_Setting pageSetting;
    [SerializeField] private GameObject[] redPoint;

    private int curPageIndex = 2;
    //常驻ui，在uisystem里手动调用
    public override void OnInitUI()
    {
        base.OnInitUI();
        AddEvent();

        pageShop.OnInitUI();
        pageCollection.OnInitUI();
        pagePlay.OnInitUI();
        pageDungeon.OnInitUI();
        pageSetting.OnInitUI();
    }
    private void AddEvent() 
    {
        //_scrollRect.onValueChanged.AddListener((Vector2 value) =>
        //{
        //    if (value.x == 1 && !UISystem.Instance.HasUI)
        //    {
        //        TryShowLogInAppleIcon();
        //    }
        //    else
        //    {
        //        TryHideLogInAppleIcon();
        //    }
        //});
        HVPageView.OnPageChangeEvent += HVPageView_OnPageChangeEvent;
        HVPageView.OnPageEnterEvent += HVPageView_OnPageEnterEvent;
        HVPageView.OnPageExitEvent += HVPageView_OnPageExitEvent;
        UIPanel_Collection.RefreshRedPointState += () => RefreshCollectRedPointState();
        UI_Shop_Daily.RefreshRedPointState += (bool isActive) => RefreshShopRedPointState(isActive);
        TopResourceBar.onOpenShopPage += LocateShopCoinsOrGems;
        //主页多语言刷新
        UI_Manager.Instance.RegisterRefreshEvent(gameObject, (str) =>
        {
            pageShop.RefreshLanguageText();
            pageCollection.RefreshLanguageText();
            pagePlay.RefreshLanguageText();
            pageDungeon.RefreshLanguageText();
            pageSetting.SetupLanguageText();
        }, "RefreshEvent_LanguageChanged");
        UI_Manager.Instance.RegisterRefreshEvent(gameObject, (str) =>
        {
            isCheck = false;
        }, "RefreshEvent_ResetCheckActivity");
    }


    private ScrollRect HVPageView_OnPageChangeEvent(int pageIndex, bool isLocked)
    {
        if (pageIndex == 1 && pageCollection.propScrollRect.TryGetComponent<ScrollRect>(out var _scroll))
            return _scroll;
        else if (pageIndex == 0)
            return pageShop.scrollView;
        else if (pageIndex == 3)
            return pageDungeon.dungeonScroll;
        return null;
    }

    private void HVPageView_OnPageEnterEvent(int pageIndex, GameObject page)
    {
        curPageIndex = pageIndex;
        if (pageIndex == 0)
        {
            //商城
            redPoint[0].SetActive(false);
        }
        else if (pageIndex == 1)
        {
            //收藏
            redPoint[1].SetActive(false);
            pageCollection.onPageCollectionEnter();
        }
        else if (pageIndex == 2)
        {
            //主界面
        }
        else if (pageIndex == 3)
        {
            //副本
        }
        else if (pageIndex == 4)
        {
            //设置
            pageSetting.SetMainIcon();
        }
    }

    private void HVPageView_OnPageExitEvent(int pageIndex, GameObject page)
    {
        if (pageIndex == 0)
        {
            redPoint[0].SetActive(true);
            RefreshShopRedPointState(isActive);
        }
        else if (pageIndex == 1) 
        {
            redPoint[1].SetActive(true);
            RefreshCollectRedPointState();
        }
    }

    public void LocateShopCoinsOrGems(TopResourceType resourceType)
    {
        StartCoroutine(pageShop.LocalPosition(resourceType, HVPageView.CurrentPage != 0));
        HVPageView.SkipToPage(0);
    }

    bool isActive;
    private void RefreshShopRedPointState(bool isActive)
    {
        //if (!TimeManager.IsGetServerUtcSuccess)
        //    return;
        //商店页面红点提示
        this.isActive = isActive;
        if (curPageIndex == 0) 
            return;
        if (redPoint[0] != null)
        {
            redPoint[0].SetActive(isActive);
        }
    }

    private void RefreshCollectRedPointState()
    {
        if (curPageIndex == 1)
            return;
        if (redPoint[1] != null)
        {
            if (UIPanel_Collection.claimedItemDict.Count <= 0)
            {
                if (redPoint[1].activeSelf)
                {
                    redPoint[1].SetActive(false);
                }
            }
            else
            {
                if (!redPoint[1].activeSelf)
                {
                    redPoint[1].SetActive(true);
                }
            }
        }
        else
        {
            Debug.LogError("null");
        }
    }

    #region 检测活动开启
    private bool isCheck = false;
    public void CheckActivityOpen()
    {
        //发放玩家支线未完成的奖励
        if (SaveUtils.HasKey("Save_Key_BranchGuaranteeRewardsPop") && SaveUtils.GetBool("Save_Key_BranchGuaranteeRewardsPop"))
        {
            if (BranchSystem.Instance.LastRewardList.Count == 0)
                return;
            SaveUtils.SetBool("Save_Key_BranchGuaranteeRewardsPop", false);
            UISystem.Instance.ShowUI(new UIPanelData_Daily(DailyOpenType.BRANCHEND));
        }
        //发放玩家每日未完成的奖励
        else if(SaveUtils.GetBool("Save_Key_DailyGuaranteeRewardsPop", false))
        {
            if (DailyActiveSystem.Instance.LastRewardList.Count == 0)
                return;
            SaveUtils.SetBool("Save_Key_DailyGuaranteeRewardsPop", false);
            UISystem.Instance.ShowUI(new UIPanelData_Daily(DailyOpenType.DAILYEND));
        }
        //礼包弹窗
        else if (GiftPackageManager.Instance.GetIsPop() && !isCheck)
        {
            GiftPackageManager.Instance.OpenGiftPackageView();
            isCheck = true;
        }
    }
    public void ShowEvaluateView()
    {
        StartCoroutine(DelayShowEvaluate());
    }
    private IEnumerator DelayShowEvaluate() 
    {
        yield return new WaitForSeconds(0.5f);
        if (TaskGoalsManager.Instance.curLevelIndex == 4)//第三关结束，此处已切关，所以用4
        {
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_Evaluate));
        }
    }
    #endregion

    #region 从merge页返回主页面
    public int pageToIndex = 2;
    public void MergeUIReturnMainmenu()
    {
        HVPageView.SkipToPage(pageToIndex);
        pageToIndex = 2;
        pagePlay.RefrehView();
        SetMainViewSpineOnOrOff(true);
        StartCoroutine(TodoAfterReturnMainmenu());
    }

    private IEnumerator TodoAfterReturnMainmenu()
    {
        yield return new WaitForSeconds(0.3f);
        bool isInTutorial = false;
        //tutor
        if (pagePlay.ChestCanOpen) 
        {
            if (!SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnLevelChest1))
            {
                GameDebug.Log("执行InnLevelChest1教学");
                UI_TutorManager.Instance.RunTutorWithName("InnLevelChest1");
                SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnLevelChest1, true);
                SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnLevelChest2, true);
                isInTutorial = true;
            }
            else 
            {
                GameManager.Instance.TryShowTopTopNoticeBar(TopNoticeType.LevelRewardReady);
            }
        }
        if (!SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnStarChest1) && pagePlay.StarChestCanOpen)
        {
            GameDebug.Log("执行InnStarChest1教学");
            UI_TutorManager.Instance.RunTutorWithName("InnStarChest1");
            SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnStarChest1, true);
            SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnStarChest2, true);
            isInTutorial = true;
        }

        if (!isInTutorial)
        {
            bool isExpFull = GameManager.Instance.playerData.UnCollectedExp >= GameManager.Instance.playerData.NextExpLevelNeedExp;
            if (!SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnLevelReward1) && isExpFull)
            {
                GameDebug.Log("执行InnLevelReward1教学");
                UI_TutorManager.Instance.RunTutorWithName("InnLevelReward1");
                isInTutorial = true;
                SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnLevelReward1, true);
                SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnLevelReward2, true);
            }
        }
        if (!isInTutorial)
        {
            CheckActivityOpen();
        }

    }
    public void SetMainViewSpineOnOrOff(bool on)
    {
        pagePlay.SetMainViewSpineOnOrOff(on);
    }
    #endregion
    public void TryStartOpenDungeonTutor() 
    {
        if (!SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnAdventure1) && TaskGoalsManager.Instance.curLevelIndex == 5)
        {
            GameDebug.Log("执行InnAdventure1教学");
            UI_TutorManager.Instance.RunTutorWithName("InnAdventure1");
            SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnAdventure1, true);
        }      
    }

//    #region 苹果登录icon
//    /// <summary>
//    /// 尝试显示苹果登录按钮
//    /// </summary>
//    public void TryShowLogInAppleIcon()
//    {
//#if UNITY_IOS
//        if (pageSetting == null || _scrollRect == null)
//            return;

//        if (Page_Setting.IsShowSignInApple)
//        {
//            //已经显示过了
//        }
//        else if (_scrollRect.normalizedPosition.x == 1)
//        {
//            pageSetting.ShowSDKSignInApple();
//        }
//#endif
//    }

//    /// <summary>
//    /// 尝试关闭苹果登录按钮
//    /// </summary>
//    public void TryHideLogInAppleIcon()
//    {
//#if UNITY_IOS
//        if (pageSetting == null)
//            return;

//        if (Page_Setting.IsShowSignInApple)
//        {
//            pageSetting.HideSDKSignInApple();
//        }
//        else
//        {
//            //已经隐藏过了
//        }
//#endif
//    }
//    #endregion


}
