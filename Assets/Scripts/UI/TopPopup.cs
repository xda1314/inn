using DG.Tweening;
using I2.Loc;
using IvyCore;
using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopPopupData : UIPanelDataBase
{
    public TopResourceType openType;
    public TopPopupData(TopResourceType openType) : base(Consts.UI_TopPopup)
    {
        this.openType = openType;
    }

}

public class TopPopup : UIPanelBase
{
    #region 组件
    [SerializeField] private Button btn_Close;
    [SerializeField] private TextMeshProUGUI text_Title;
    [SerializeField] private GameObject AD_bg;
    [Header("经验界面")]
    [SerializeField] private GameObject expGO;
    [SerializeField] private Button btn_ClaimExpReward;
    [SerializeField] private Button btn_NoClaimExpReward;
    [SerializeField] private Transform itemTF;
    [SerializeField] private TextMeshProUGUI expItemDesText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI t_Exp;
    [SerializeField] private TextMeshProUGUI greyBtnText;
    [SerializeField] private TextMeshProUGUI noclaimgreyBtnText;
    [SerializeField] private Slider scrollBar;
    [SerializeField] GameObject particle;

    [Header("体力界面")]
    [SerializeField] GameObject energyGO;
    [SerializeField] private Button btn_CostMoney;
    [SerializeField] private Button btn_CostMoney_Not;
    [SerializeField] private Button btn_CostGem;
    [SerializeField] private Button btn_AD;
    [SerializeField] private Button btn_noAD;
    [SerializeField] Transform[] dailyPackItemIcons;
    [SerializeField] TextMeshProUGUI[] dailyPackItemLabels;
    [SerializeField] TextMeshProUGUI countDown;
    //[SerializeField] TextMeshProUGUI originalPrice;    
    [SerializeField] TextMeshProUGUI discountPrice;
    [SerializeField] TextMeshProUGUI t_SellOut;
    [SerializeField] TextMeshProUGUI energyCostText;
    [SerializeField] TextMeshProUGUI energyTutorialFreeText;
    [SerializeField] TextMeshProUGUI energyFreeText;
    [SerializeField] TextMeshProUGUI energyNoFreeText;
    [SerializeField] TextMeshProUGUI NoFreeTime;
    [SerializeField] TextMeshProUGUI energyNoAdFree;
    [SerializeField] TextMeshProUGUI txtProportion;
    [SerializeField] TextMeshProUGUI txtKind;
    #endregion

    private List<MergeRewardItem> rewardItems;

    private PayPackDefinition payPack;

    public TopResourceType openType;

    private List<GameObject> saveBg = new List<GameObject>();
    private List<MergeRewardItem> spawnGO_List = new List<MergeRewardItem>();
    private List<string> prefabNameList = new List<string>();
    private List<GameObject> ui_ItemGO = new List<GameObject>();

    private int currentLevel;
    public override void OnInitUI()
    {
        base.OnInitUI();
        uiType = UIType.Popup;
        expGO.gameObject.SetActive(false);
        energyGO.gameObject.SetActive(false);
        btn_ClaimExpReward.onClick.AddListener(() => GetLevelReward());
        localPos_y = AD_bg.transform.localPosition.y;
        rewardItems = new List<MergeRewardItem>();
        btn_AD.onClick.AddListener(ClickWatchAD);
        btn_Close.onClick.AddListener(CloseButton);
        btn_CostGem.onClick.AddListener(ClickBuyEnergy);
        btn_CostMoney.onClick.AddListener(BuyEnergyByMoney);
    }

    private float localPos_y = 0.0f;
    private void ADGiftShowAni() 
    {
        AD_bg.SetActive(openType == TopResourceType.Energy);
        if(openType == TopResourceType.Energy) 
        {
            AD_bg.transform.DOLocalMoveY(-575f, 0.4f).SetEase(Ease.OutBounce);
        }
    }

    public override IEnumerator OnShowUI()
    {
        AD_bg.transform.localPosition = new Vector3(0, localPos_y, 0);
        InitEnergyCostMoneyRewards();
        UIPanelData.SetShowCallBack(null, _ => { ADGiftShowAni(); });
        openType = ((TopPopupData)UIPanelData).openType;
        StartCoroutine(RefreshUI());
        SetBgCloseBtnEnable(true);
        countDown.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo((int)TimeManager.Instance.GetTomorrowRefreshTimeSpan().TotalSeconds);
        RefreshAdCountDown();
        yield return base.OnShowUI();
        yield return new WaitForSeconds(0.3F);
    }
    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
        expGO.SetActive(false);
        energyGO.SetActive(false);
        if (particle.activeSelf)
            particle.SetActive(false);

        foreach (var item in dailyPackItemIcons)
        {
            for (int i = 0; i < item.childCount; i++)
            {
                Destroy(item.GetChild(i).gameObject);
            }
        }
    }



    private void InitEnergyCostMoneyRewards()
    {

        GiftPackageDefinition definition;

        GiftPackageDefinition.GiftDefinitionsDict.TryGetValue(1004, out definition);

        rewardItems.Clear();
        foreach (var item in definition.ItemRewardList)
        {
            rewardItems.Add(item);
        }
        foreach (var item in definition.CoinOrGemRewardList)
        {

            rewardItems.Add(item);
        }
        discountPrice.text = definition.Cost.ToString();
        //originalPrice.text = definition.OriginalPrice.ToString();
        t_SellOut.text = ScriptLocalization.Get("Obj/Chain/Shop_soldout");
        txtProportion.text = GameManager.Instance.GetDiscountStr(definition.Cost / definition.OriginalPrice);
        payPack = new PayPackDefinition(definition.SaveID, definition.UnityID, rewardItems, definition.Cost);
        payPack.GooglePlayProductID = definition.GooglePlayID;
        payPack.AppleProductID = definition.AppleID;
        Billing.SearchPriceInfoAsync_One(payPack, priceInfo =>
        {
            if (this == null || discountPrice == null || payPack == null || payPack.UnityID != priceInfo.UnityID)
            {
                return;
            }
            discountPrice.text = priceInfo.Price.ToString();
        });

        txtKind.text = ScriptLocalization.Get(definition.GiftName);
        if (rewardItems != null)
        {
            for (int i = 0; i < rewardItems.Count; i++)
            {
                if (rewardItems[i].IsRewardCoins)
                {
                    dailyPackItemLabels[1].text = "+" + rewardItems[i].num.ToString();
                    GameObject icon_coin = AssetSystem.Instance.Instantiate(Consts.Icon_Reward_Coins, dailyPackItemIcons[1]);
                    icon_coin.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
                }
                else if (rewardItems[i].IsRewardGems)
                {
                    dailyPackItemLabels[0].text = "+" + rewardItems[i].num.ToString();
                    GameObject icon_item = AssetSystem.Instance.Instantiate(Consts.Icon_Reward_Gems, dailyPackItemIcons[0]);
                    icon_item.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
                }
                else if (rewardItems[i].IsRewardEnergy)
                {
                    dailyPackItemLabels[2].text = "+" + rewardItems[i].num.ToString();
                    GameObject icon_item = AssetSystem.Instance.Instantiate(Consts.Icon_Reward_Energy, dailyPackItemIcons[2]);
                    icon_item.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
                }
            }
        }
    }

    //购买体力--钱
    private void BuyEnergyByMoney()
    {
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
        Billing.Instance.TryMakePurchase(payPack, dailyPackItemIcons[0].transform.position, _ =>
         {
             //DailyTaskSystem.Instance.DailyTaskEvent_AddEnergy?.Invoke();
             PlayerData.BuyDailyEnergyTime = TimeManager.Instance.UtcNow();
             GameManager.Instance.SavePlayerData_BuyDailyEnergyTime();
             SellOut();
         });
    }

    public void SellOut()
    {
        if (GameManager.Instance.playerData.BuyEnergyDailyCurrentDay())
        {
            btn_CostMoney.gameObject.SetActive(false);
            btn_CostMoney_Not.gameObject.SetActive(true);
        }
        else 
        {
            btn_CostMoney.gameObject.SetActive(true);
            btn_CostMoney_Not.gameObject.SetActive(false);
        }
    }
    int adCoolTime = -1;
    float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            if (countDown != null)
                countDown.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo((int)TimeManager.Instance.GetTomorrowRefreshTimeSpan().TotalSeconds);
            timer--;
            if (adCoolTime > 0) 
            {
                adCoolTime--;
                NoFreeTime.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(adCoolTime);
                if (adCoolTime <= 0)  
                {
                    RefreshAdCountDown();
                }
            }
        }
    }

    private IEnumerator RefreshUI()
    {
        //判断打开的是哪个页面
        if (this.openType == TopResourceType.Exp)
        {
            expGO.SetActive(true);
            yield return RefreshExp();
        }
        else if (this.openType == TopResourceType.Energy)
        {
            bool isTutorial = Currencies.Energy <= 0 && !SaveUtils.GetBool(Consts.SaveKey_Tutorial_GetEnergy);
            energyGO.SetActive(true);
            RefreshEnergyUI(isTutorial);
            //yield return new WaitForSeconds(0.3f);
            //教学
            if (isTutorial)
            {
                SaveUtils.SetBool(Consts.SaveKey_Tutorial_GetEnergy, true);
                SetBgCloseBtnEnable(false);
                UI_TutorManager.Instance.RunTutorWithName("InnUseEnergy");
            }
        }
    }

    private IEnumerator RefreshExp()
    {
        currentLevel = GameManager.Instance.playerData.CurrentExpLevel;
        SetupExpUI();

        for (int i = 0; i < prefabNameList.Count; i++)
        {
            AssetSystem.Instance.DestoryGameObject(prefabNameList[i], ui_ItemGO[i]);
            Destroy(saveBg[i]);
        }
        prefabNameList.Clear();
        ui_ItemGO.Clear();
        saveBg.Clear();

        GameManager.Instance.playerData.CanCollectedExp(out List<MergeRewardItem> levelRewardList);
        if (levelRewardList.Count > 0)
        {
            spawnGO_List.Clear();
            for (int i = 0; i < levelRewardList.Count; i++)
            {
                GameObject bg = AssetSystem.Instance.Instantiate(Consts.UI_CommomItemBg, itemTF);
                //bg.transform.localScale = Vector3.one;
                GameObject go = AssetSystem.Instance.Instantiate(levelRewardList[i].name, bg.transform.Find("bg"));
                if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(levelRewardList[i].name, out MergeItemDefinition definition))
                {
                    bg.transform.Find("NeedItemNum").GetComponent<TextMeshProUGUI>().text = ScriptLocalization.Get(definition.locKey_Name);
                }
                ShowItemInfo showItemInfo = bg.transform.Find("ShowInfoBtn").GetComponent<ShowItemInfo>();
                if (showItemInfo != null)
                {
                    showItemInfo.RefreshPrefabName(levelRewardList[i].name, null);
                }
                saveBg.Add(bg);
                if (go != null)
                {
                    ui_ItemGO.Add(go);
                    prefabNameList.Add(levelRewardList[i].name);
                    MergeRewardItem rewardItem = new MergeRewardItem
                    {
                        name = levelRewardList[i].name,
                        num = levelRewardList[i].num
                    };
                    spawnGO_List.Add(rewardItem);
                }
            }

            //yield return new WaitForSeconds(0.2f);
            //for (int i = 0; i < saveBg.Count; i++)
            //{
            //    DOTweenAnimation animation = saveBg[i].GetComponent<DOTweenAnimation>();
            //    if (animation)
            //    {
            //        animation.DOPlay();
            //    }
            //    yield return new WaitForSeconds(0.2f);
            //}
            yield return null;
        }

        //教学
        if (SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnLevelReward2))
        {
            GameDebug.Log("执行InnEndLevel教学");
            UI_TutorManager.Instance.RunTutorWithName("InnLevelReward2");
            SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnLevelReward2, false);
        }
    }

    //发放升级奖励
    private void GetLevelReward()
    {
        if (!GameManager.Instance.playerData.CollectedExpOneLevel())
        {
            return;
        }
        //AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.btn_Receive);
        GameObject UIMerge = GameObject.Find(Consts.UIPanel_Merge);
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Get_EXPReward);
        if (UIMerge != null && UIMerge.activeSelf)
        {
            for (int i = 0; i < ui_ItemGO.Count; i++)//飞
            {
                GameManager.Instance.GiveRewardItem(spawnGO_List[i], "UpgradeReward", ui_ItemGO[i].transform.position, false);
            }
        }
        else
        {
            GameManager.Instance.GiveRewardItem(spawnGO_List, "UpgradeReward", Vector3.zero);//弹
        }

        RefreshExpUI();
        spawnGO_List.Clear();
        StartCoroutine(RefreshExp());

        if (GameManager.Instance.playerData.UnCollectedExp < GameManager.Instance.playerData.NextExpLevelNeedExp)
        {
            CloseButton();
        }
        UIPanel_TopBanner.refreshBanner?.Invoke();
    }


    private void SetupExpUI()
    {
        text_Title.text = ScriptLocalization.Get("Obj/Eliminate/Level/Title");

        greyBtnText.text = ScriptLocalization.Get("Obj/Eliminate/Level/Button");
        noclaimgreyBtnText.text = ScriptLocalization.Get("Obj/Eliminate/Level/Button");
        levelText.text = currentLevel.ToString();
        RefreshExpUI();
        int needExp = GameManager.Instance.playerData.NextExpLevelNeedExp;
        int curExp = GameManager.Instance.playerData.UnCollectedExp >= GameManager.Instance.playerData.NextExpLevelNeedExp
            ? GameManager.Instance.playerData.NextExpLevelNeedExp : GameManager.Instance.playerData.UnCollectedExp;
        if (curExp < needExp)
            expItemDesText.text = ScriptLocalization.Get("Obj/Eliminate/Level/Text1");
        else
            expItemDesText.text = ScriptLocalization.Get("Obj/Eliminate/Level/Text2");
        t_Exp.text = curExp.ToString() + "/" + needExp.ToString();
        btn_ClaimExpReward.gameObject.SetActive(curExp >= needExp);
        btn_NoClaimExpReward.gameObject.SetActive(curExp < needExp);
    }

    /// <summary>
    /// 刷新声望奖励页面
    /// </summary>
    private void RefreshExpUI()
    {
        scrollBar.value = GameManager.Instance.playerData.UnCollectedExp >= GameManager.Instance.playerData.NextExpLevelNeedExp
            ? 1 : GameManager.Instance.playerData.UnCollectedExp * 1f / GameManager.Instance.playerData.NextExpLevelNeedExp;

        bool levelUp = scrollBar.value >= 1;

        //转换按钮状态
        btn_ClaimExpReward.interactable = levelUp;
        particle.SetActive(levelUp);
    }

    /// <summary>
    /// 刷新体力奖励页面
    /// </summary>
    private void RefreshEnergyUI(bool tutorialFree = false)
    {
        isTutorialBuyEnergy = tutorialFree;
        GameManager.Instance.playerData.TryRefreshBuyEnergyData();
        GameManager.Instance.playerData.BuyEnergyDailyCurrentDay();
        SellOut();

        text_Title.text = ScriptLocalization.Get("Obj/Eliminate/Engery/Title");
        if (tutorialFree)
        {
            energyTutorialFreeText.gameObject.SetActive(true);
            energyCostText.transform.parent.gameObject.SetActive(false);
            energyTutorialFreeText.text = ScriptLocalization.Get("Obj/Eliminate/Engery/Text3");
        }
        else
        {
            energyTutorialFreeText.gameObject.SetActive(false);
            energyCostText.transform.parent.gameObject.SetActive(true);
            if (PlayerData.EnergyCost < 100)
                energyCostText.text = PlayerData.EnergyCost.ToString();
            else
                energyCostText.text = "100";
        }      
        energyFreeText.text = ScriptLocalization.Get("Obj/Eliminate/Engery/Text3");
        energyNoFreeText.text = ScriptLocalization.Get("Obj/Chain/WatchVideo_Text5");
        energyNoAdFree.text = ScriptLocalization.Get("Obj/Eliminate/Engery/Text3");
    }
    /// <summary>
    /// 广告刷新相关
    /// </summary>
    private void RefreshAdCountDown()
    {
        bool hasAd = TimeManager.IsGetServerUtcSuccess && AdManager.CanShowAD_Normal(AdManager.ADTag.Energy);
        btn_AD.interactable = hasAd;
        if (hasAd)
        {
            btn_AD.gameObject.SetActive(true);
            btn_noAD.gameObject.SetActive(false);
        }
        else
        {
            btn_AD.gameObject.SetActive(false);
            btn_noAD.gameObject.SetActive(true);
            adCoolTime = AdManager.LeftWatchADTime();//是否有广告倒计时，防止时间为负数的情况
            if (adCoolTime > 0)
            {
                NoFreeTime.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(adCoolTime);
                energyNoFreeText.transform.parent.gameObject.SetActive(true);
                energyNoAdFree.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                GameDebug.LogWarning("广告倒计时为负,检查是否有广告");
                energyNoFreeText.transform.parent.gameObject.SetActive(false);
                energyNoAdFree.transform.parent.gameObject.SetActive(true);
            }
        }
    }
    private void ClickWatchAD()
    {
        VibrateSystem.Haptic(HapticTypes.Selection);
        AdManager.PlayAd(Vector3.zero, AdManager.ADTag.Energy, () =>
        {
            GetEnergy();
            RefreshAdCountDown();
        }, "", () =>
          {
              MergeRewardItem item = new MergeRewardItem
              {
                  name = "energy",
                  num = 50
              };
              List<MergeRewardItem> rewardList = new List<MergeRewardItem>();
              rewardList.Add(item);
              GameManager.Instance.PlayAdFail(rewardList, AdManager.ADTag.Energy);
          });
    }

    
    private void GetEnergy()
    {
        MergeRewardItem item;
        MergeLevelType levelType = MergeLevelManager.Instance.CurrentLevelType;

        item = new MergeRewardItem
        {
            name = "energy",
            num = 50
        };
        List<MergeRewardItem> rewardList = new List<MergeRewardItem>();
        rewardList.Add(item);
        if (GameManager.Instance.playerData.IsBuyADGift()) 
        {
            GameManager.Instance.GiveRewardItem(rewardList, "WatchAD", btn_AD.transform.position);
        }
        else
        {
            GameManager.Instance.GiveRewardItem(rewardList, "WatchAD", levelType, false, () =>
            {
                if (GameManager.Instance.playerData.IsShowADGift())
                {
                    if (UI_TutorManager.Instance.IsTutoring())
                        return;
                    UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_ADGift));
                }
            });
        }
        MergeActionSystem.OnMergeActionEvent(MergeActionType.AddEnergy);
        RefreshEnergyUI();
    }

    private bool isTutorialBuyEnergy = false;
    private void ClickBuyEnergy()
    {
        if (!isTutorialBuyEnergy)
        {
            int cost = 0;
            GameManager.Instance.playerData.TryRefreshBuyEnergyData();
            if (PlayerData.EnergyCost <= 100)
                cost = PlayerData.EnergyCost;
            else
                cost = 100;
            if (!Currencies.CanAffordOrTip(CurrencyID.Gems, cost))
                return;
            Currencies.Spend(CurrencyID.Gems, cost, "buyEnergy");
            PlayerData.BuyEnergyTime = TimeManager.ServerUtcNow();
            AnalyticsUtil.TrackEvent("claim_gem_energy", new Dictionary<string, string>() {
                {"value",cost.ToString() }
            });
            cost *= 2;
            cost = cost < 100 ? cost : 100;
            energyCostText.text = cost.ToString();

            PlayerData.EnergyCost = cost;
            GameManager.Instance.SavePlayerData_EnergyCost();
            GameManager.Instance.SavePlayerData_BuyEnergyTime();
            //DailyTaskSystem.Instance.DailyTaskEvent_AddEnergy?.Invoke();
        }
        else
        {
            isTutorialBuyEnergy = false;
            energyTutorialFreeText.gameObject.SetActive(false);
            energyCostText.transform.parent.gameObject.SetActive(true);
            if (PlayerData.EnergyCost < 100)
                energyCostText.text = PlayerData.EnergyCost.ToString();
            else
                energyCostText.text = "100";
        }


        MergeLevelType levelType = MergeLevelManager.Instance.CurrentLevelType;

        MergeRewardItem item = new MergeRewardItem
        {
            name = "energy",
            num = 100
        };


        GameManager.Instance.GiveRewardItem(item, "BuyByGem", btn_CostGem.gameObject.transform.position, true, levelType);
        MergeActionSystem.OnMergeActionEvent(MergeActionType.AddEnergy);
    }

    private void CloseButton()
    {
        //AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
        UISystem.Instance.HideUI(this);
    }

}
