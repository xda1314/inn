using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop_NormalItem : UIPanelBase
{
    //[SerializeField] private Sprite greySprite;
    //[SerializeField] private Sprite buleSprite;
    [SerializeField] private Button btn_info;
    [SerializeField] private Transform[] iconContainerTran;
    [SerializeField] private TextMeshProUGUI[] txt_nums;
    [SerializeField] private TextMeshProUGUI lbl_Remain;

    [SerializeField] private TextMeshProUGUI lbl_GemsCount;

    [SerializeField] private GameObject btn_Buy;
    [SerializeField] private Button btn_buy;
    [SerializeField] private Button btn_noBuy;
    [SerializeField] private TextMeshProUGUI text_sellOut;
    [SerializeField] private GameObject icon_costGems;
    [SerializeField] private GameObject icon_CostCoins;
    [SerializeField] private TextMeshProUGUI lbl_NormalCost;
    [SerializeField] private TextMeshProUGUI lbl_Paycost;
    [SerializeField] private TextMeshProUGUI lbl_ADCost;
    [SerializeField] private TextMeshProUGUI txt_des;
    [SerializeField] private TextMeshProUGUI t_PreCost;

    [SerializeField] private Button btn_watchAD;
    [SerializeField] private Button btn_noWatchAD;
    [SerializeField] private TextMeshProUGUI text_noAD;
    [SerializeField] private GameObject sprite_Outline;
    [SerializeField] private GameObject RibbonGO;
    [SerializeField] private TextMeshProUGUI textCountDown;
    [SerializeField] private TextMeshProUGUI txt_discount;
    [SerializeField] private Image imgDiscount;
    [SerializeField] private TextMeshProUGUI text_discount;

    private ShopPackData shopPackData;
    private PayPackDefinition packDef;
    private Action<ShopPackData, Vector3> buyCB;
    private Action<PayPackDefinition, Vector3> payCB;
    private DateTimeOffset timeOffset;
    public Action timeOutCB;

    private CurrencyID currencyID;
    [HideInInspector] public bool sellOut;
    [HideInInspector] public GameObject redPoint;
    [HideInInspector] public Image[] sprites;

    public Action refreshText;

    private void Awake()
    {
        refreshText = new Action(RefreshText);
    }

    private void RefreshText()
    {
        if (text_discount != null) 
        {
            text_discount.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Popular");
        }
        
        if (sellOut)
            lbl_Paycost.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/Engery/Text4");
        else if (currencyID == CurrencyID.Free)
            lbl_Paycost.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/Engery/Text3");
        lbl_Remain.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part3Text").Replace("{0}", (shopPackData.countLimit - shopPackData.todayBuyCount).ToString());
        lbl_ADCost.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/SpinWheel_Describe3");
    }
    public Action GetRefreshAction()
    {
        return refreshText;
    }

    public void SetItemInfo(ShopPackData shopData, Action<ShopPackData, Vector3> buyCB)
    {
        
        if (shopData == null || string.IsNullOrEmpty(shopData.rewardItem[0].name))
        {
            Debug.LogError("reward is null");
            buyCB = null;
            return;
        }
        for (int index = 0; index < shopData.rewardItem.Count; index++)
        {
            if (iconContainerTran.Length < index)
                return;
            if (shopData.rewardItem.Count > 0)
            {
                if (shopData.rewardItem[0].IsRewardCoins || shopData.rewardItem[0].IsRewardGems)
                    btn_info.gameObject.SetActive(false);
                else
                    btn_info.gameObject.SetActive(true);
            }
            else 
            {
                btn_info.gameObject.SetActive(false);
            }
            ShowItemInfo itemInfo = btn_info.GetComponent<ShowItemInfo>();
            if (itemInfo  == null)
            {
                itemInfo = btn_info.gameObject.AddComponent<ShowItemInfo>().GetComponent<ShowItemInfo>();
            }
            itemInfo.RefreshPrefabName(shopData.rewardItem[index].name, null);
            if (iconContainerTran[index].childCount <= 0 || this.shopPackData == null || this.shopPackData.rewardItem[index].ShowRewardPrefabName != shopData.rewardItem[index].ShowRewardPrefabName)
            {
                for (int i = iconContainerTran[index].childCount - 1; i >= 0; i--)
                {
                    DestroyImmediate(iconContainerTran[index].transform.GetChild(i).gameObject);
                }
                if (txt_nums.Length > index)
                {
                    txt_nums[index].text = "x" + shopData.rewardItem[index].num.ToString();
                }
                AssetSystem.Instance.InstantiateAsync(shopData.rewardItem[index].ShowRewardPrefabName, iconContainerTran[index], gO =>
                {
                    RectTransform rect = gO.GetComponents<RectTransform>()[0];
                    rect.pivot = new Vector2(0.5f, 0);
                    rect.localPosition = Vector2.zero;
                    if (gO.name == "Icon_Reward_Coins"|| gO.name == "Icon_Reward_Gems")
                        rect.localPosition =new Vector2(rect.localPosition.x, rect.localPosition.y+20);
                });
            }
        }

        SetTimeOffset(DateTimeOffset.FromUnixTimeSeconds(shopData.refreshTime));

        this.shopPackData = shopData;
        this.buyCB = buyCB;

        if (t_PreCost != null)
        {
            t_PreCost.text = shopData.OriginalPrice;
        }
        if (txt_discount != null)
        {
            txt_discount.text = GameManager.Instance.GetDiscountStrWhith_N(shopData.cost / float.Parse(shopData.OriginalPrice));
        }
        if (txt_des != null)
        {
            if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(shopData.rewardItem[0].name, out MergeItemDefinition mergeItemDefinition))
            {
                txt_des.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part2Text2").Replace("{0}", I2.Loc.ScriptLocalization.Get(mergeItemDefinition.KeyGiftName));
            }
        }
        if (shopData.countLimit > 0)
        {
            lbl_Remain.gameObject.SetActive(true);
            lbl_Remain.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part3Text").Replace("{0}", (shopData.countLimit - shopData.todayBuyCount).ToString());
        }
        else
        {
            lbl_Remain.gameObject.SetActive(false);
        }
        if (lbl_GemsCount != null) 
        {
            lbl_GemsCount.gameObject.SetActive(false);
        }      
        currencyID = shopData.currencyID;

        if (shopData.countLimit > 0 && shopData.countLimit <= shopData.todayBuyCount)
        {
            SellOut();
        }
        else
        {
            if (shopData.countLimit > 0)
            {
                sellOut = false;
            }
            //btn_watchAD.onClick.RemoveAllListeners();
            btn_buy.onClick.RemoveAllListeners();

            RefreshItemUI(shopData);

        }
        RefreshPrefabName(shopData.rewardItem[0].name);
    }

    private void SellOut()
    {
        sellOut = true;
        //已售罄
        btn_buy.gameObject.SetActive(true);
        icon_costGems.SetActive(false);
        icon_CostCoins.SetActive(false);
        lbl_NormalCost.gameObject.SetActive(false);
        btn_watchAD.gameObject.SetActive(false);
        btn_noWatchAD.gameObject.SetActive(false);
        btn_buy.gameObject.SetActive(false);
        btn_noBuy.gameObject.SetActive(true);
        text_sellOut.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/Engery/Text4");
        lbl_Paycost.GetComponent<Shadow>().effectColor = ExtensionTool.GetUIBtnLabelEffectColor(MergeUILabelType.Grey3);
        lbl_Paycost.gameObject.SetActive(true);
        //btn_buy.onClick.RemoveAllListeners();
        //btn_watchAD.onClick.RemoveAllListeners();
    }


    private void RefreshItemUI(ShopPackData shopData)
    {
        if (shopData == null)
            return;

        switch (shopData.currencyID)
        {
            case CurrencyID.Coins:
                {
                    //btn_buy.GetComponent<Image>().sprite = buleSprite;
                    btn_buy.gameObject.SetActive(true);
                    btn_noBuy.gameObject.SetActive(false);
                    lbl_Paycost.gameObject.SetActive(false);
                    lbl_NormalCost.gameObject.SetActive(true);
                    lbl_NormalCost.text = shopData.cost.ToString();
                    lbl_NormalCost.GetComponent<Shadow>().effectColor = ExtensionTool.GetUIBtnLabelEffectColor(MergeUILabelType.Purple1);
                    btn_watchAD.gameObject.SetActive(false);
                    btn_noWatchAD.gameObject.SetActive(false);
                    btn_buy.onClick.RemoveAllListeners();
                    btn_buy.onClick.AddListener(() =>
                    {
                        this.buyCB?.Invoke(shopData, iconContainerTran[0].transform.position);
                    });
                }
                break;
            case CurrencyID.Gems:
                {
                    //btn_buy.GetComponent<Image>().sprite = buleSprite;
                    btn_buy.gameObject.SetActive(true);
                    btn_noBuy.gameObject.SetActive(false);
                    lbl_Paycost.gameObject.SetActive(false);
                    lbl_NormalCost.text = shopData.cost.ToString();
                    lbl_NormalCost.GetComponent<Shadow>().effectColor = ExtensionTool.GetUIBtnLabelEffectColor(MergeUILabelType.Purple1);
                    lbl_NormalCost.gameObject.SetActive(true);
                    btn_watchAD.gameObject.SetActive(false);
                    btn_noWatchAD.gameObject.SetActive(false);
                    btn_buy.onClick.RemoveAllListeners();
                    btn_buy.onClick.AddListener(() =>
                    {
                        this.buyCB?.Invoke(shopData, iconContainerTran[0].transform.position);
                    });
                }
                break;
            case CurrencyID.Free:
                {
                    //btn_buy.GetComponent<Image>().sprite = buleSprite;
                    btn_buy.gameObject.SetActive(true);
                    btn_noBuy.gameObject.SetActive(false);
                    lbl_Paycost.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/Engery/Text3");
                    lbl_Paycost.GetComponent<Shadow>().effectColor = ExtensionTool.GetUIBtnLabelEffectColor(MergeUILabelType.Green3);
                    lbl_Paycost.gameObject.SetActive(true);
                    lbl_NormalCost.gameObject.SetActive(false);
                    btn_watchAD.gameObject.SetActive(false);
                    btn_noWatchAD.gameObject.SetActive(false);
                    btn_buy.onClick.RemoveAllListeners();
                    btn_buy.onClick.AddListener(() =>
                    {
                        this.buyCB?.Invoke(shopData, iconContainerTran[0].transform.position);
                    });
                }
                break;
            case CurrencyID.AD:
                {
                    btn_watchAD.gameObject.SetActive(true);
                    text_noAD.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/SpinWheel_Describe3");
                    lbl_ADCost.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/SpinWheel_Describe3");

                    AdManager.ADTag tag = AdManager.ADTag.None;
                    switch (shopData.rewardItem[0].ShowRewardPrefabName)
                    {
                        case "EnergyChest":
                            tag = AdManager.ADTag.dailydeals_EnergyChest;
                            break;
                        case "Icon_Reward_Coins":
                            tag = AdManager.ADTag.dailydeals_coin;
                            break;
                        case "Icon_Reward_Gems":
                            tag = AdManager.ADTag.dailydeals_gem;
                            break;
                        case "Needle":
                            tag = AdManager.ADTag.dailydeals_needle;
                            break;
                        default:
                            break;
                    }
                    bool hasAd = TimeManager.IsGetServerUtcSuccess && AdManager.CanShowAD_Normal(tag);
                    if (!hasAd)
                    {
                        btn_watchAD.gameObject.SetActive(false);
                        btn_noWatchAD.gameObject.SetActive(true);
                        btn_noBuy.gameObject.SetActive(false);
                        btn_buy.gameObject.SetActive(false);
                        sellOut = true;
                    }
                    else
                    {
                        btn_watchAD.gameObject.SetActive(true);
                        btn_noWatchAD.gameObject.SetActive(false);
                        btn_noBuy.gameObject.SetActive(false);
                        btn_buy.gameObject.SetActive(false);
                        if (shopData.countLimit > 0 && shopData.countLimit <= shopData.todayBuyCount)
                            sellOut = true;
                        else
                            sellOut = false;
                        btn_watchAD.onClick.RemoveAllListeners();
                        btn_watchAD.onClick.AddListener(() =>
                        {
                            if (shopData.rewardItem.Count > 0) 
                            {
                                Vector3 pos = iconContainerTran[0].transform.position;
                                AdManager.PlayAd(btn_watchAD.transform.position, tag, () =>
                                {
                                    this.buyCB?.Invoke(shopData, pos);
                                }, "SaveAD_Daily", () =>
                                {
                                    GameManager.Instance.PlayAdFail(shopData.rewardItem, tag);
                                });
                            }                                  
                        });
                    }
                }
                break;
            default:
                Debug.LogError("CurrencyID error");
                break;
        }
        btn_buy.gameObject.SetActive(shopData.currencyID != CurrencyID.AD);
        icon_costGems.SetActive(shopData.currencyID == CurrencyID.Gems);
        icon_CostCoins.SetActive(shopData.currencyID == CurrencyID.Coins);
    }

    public void SetRemain()
    {
        lbl_Remain.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part2Text3");
        lbl_Remain.gameObject.SetActive(true);
    }

    public void SetItemInfo(PayPackDefinition payPackDefinition, Action<PayPackDefinition, Vector3> payCB)
    {
        if (text_discount != null)
        {
            text_discount.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Popular");
        }
        if (payPackDefinition == null || payPackDefinition.RewardItems.Count <= 0)
        {
            Debug.LogError("reward is null");
            buyCB = null;
            return;
        }
        btn_buy.onClick.RemoveAllListeners();
        for (int index = 0; index < payPackDefinition.RewardItems.Count; index++)
        {
            if (iconContainerTran.Length < index)
                return;
            if (iconContainerTran[index].childCount <= 0
            || this.packDef == null || this.packDef.RewardItems.Count!= payPackDefinition.RewardItems.Count
            || this.packDef.RewardItems.Count> index && this.packDef.RewardItems[index].ShowRewardPrefabName != payPackDefinition.RewardItems[index].ShowRewardPrefabName)
            {
                for (int i = iconContainerTran[index].childCount - 1; i >= 0; i--)
                {
                    DestroyImmediate(iconContainerTran[index].transform.GetChild(i).gameObject);
                }

                if (txt_nums.Length > index)
                {
                    txt_nums[index].text = "x" + payPackDefinition.RewardItems[index].num.ToString();
                }
                if (!string.IsNullOrEmpty(payPackDefinition.iconPrefab))
                {
                    AssetSystem.Instance.InstantiateAsync(payPackDefinition.iconPrefab, iconContainerTran[index], gO =>
                    {
                        RectTransform rect = gO.GetComponents<RectTransform>()[0];
                        rect.pivot = new Vector2(0.5f, 0);
                        rect.localPosition = Vector2.zero;
                    });
                }
                else
                {
                    AssetSystem.Instance.InstantiateAsync(payPackDefinition.RewardItems[index].ShowRewardPrefabName, iconContainerTran[index], gO =>
                    {
                        RectTransform rect = gO.GetComponents<RectTransform>()[0];
                        if (gO.name == "Icon_Reward_Energy")
                            rect.sizeDelta = new Vector2(120, 120);
                        rect.pivot = new Vector2(0.5f, 0);
                        if (gO.name == "Icon_Reward_Coins")
                            rect.pivot = new Vector2(0.5f, 0.5f);
                        if (gO.name == "Icon_Reward_Gems")
                            rect.pivot = new Vector2(0.5f, 0.5f);

                        rect.localPosition = Vector2.zero;
                    });

                }
            }
        }
        this.packDef = payPackDefinition;
        this.payCB = payCB;
        if (t_PreCost != null)
        {
            t_PreCost.text = payPackDefinition.prePrice;
        }

        if (txt_des != null)
        {
            if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(payPackDefinition.RewardItems[0].name, out MergeItemDefinition mergeItemDefinition))
            {
                txt_des.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part2Text2").Replace("{0}", I2.Loc.ScriptLocalization.Get(mergeItemDefinition.KeyGiftName));
            }
        }
        if (txt_discount != null)
        {
            txt_discount.text = GameManager.Instance.GetDiscountStrWhith_N((float)payPackDefinition.Cost / float.Parse(payPackDefinition.prePrice));
        }


        SetTimeOffset(DateTimeOffset.FromUnixTimeSeconds(payPackDefinition.refreshTime));
        //if (btn_info != null)
        //{
        //    btn_info.gameObject.SetActive(true);
        //}
        if (lbl_GemsCount != null) 
        {
            lbl_GemsCount.text = payPackDefinition.RewardItems[0].num.ToString();
            lbl_GemsCount.gameObject.SetActive(true);
        }      
        lbl_Paycost.text = payPackDefinition.Cost.ToString();

        UI_ShopContainer.shopGetBillingPriceInfoCB += (info) =>
        {
            if (this == null || lbl_Paycost == null || packDef == null || packDef.UnityID != info.UnityID)
                return;
            lbl_Paycost.text = info.Price;
        };

        lbl_Remain.gameObject.SetActive(false);
        btn_noBuy.gameObject.SetActive(false);
        btn_noWatchAD.gameObject.SetActive(false);
        btn_buy.gameObject.SetActive(true);
        icon_costGems.SetActive(false);
        icon_CostCoins.SetActive(false);
        lbl_Paycost.gameObject.SetActive(true);
        lbl_Paycost.GetComponent<Shadow>().effectColor = ExtensionTool.GetUIBtnLabelEffectColor(MergeUILabelType.Purple1);
        lbl_NormalCost.gameObject.SetActive(false);
        btn_watchAD.gameObject.SetActive(false);

        btn_buy.onClick.RemoveAllListeners();
        btn_buy.onClick.AddListener(() =>
         {
             this.payCB?.Invoke(payPackDefinition, iconContainerTran[0].transform.position);
         });

    }

    public void AdjustPosition()
    {
        iconContainerTran[0].localPosition = new Vector3(15, iconContainerTran[0].localPosition.y, 0);
    }

    /// <summary>
    /// 用于不同商品不同类之间基于同一预制体的切换
    /// </summary>
    public void ClearIcon()
    {
        this.packDef = null;
        this.shopPackData = null;

        foreach (var item in iconContainerTran)
        {
            for (int i = item.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(item.transform.GetChild(i).gameObject);
            }
        }
    }

    #region InfoButton
    private void SetTimeOffset(DateTimeOffset timeOffset)
    {
        this.timeOffset = timeOffset;
    }

    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < 1)
        {
            return;
        }
        timer -= 1;
        if (textCountDown != null)
        {
            TimeSpan timeSpan = timeOffset - DateTimeOffset.UtcNow;
            if (timeSpan.TotalSeconds < 0)
            {
                timeOutCB?.Invoke();
                return;
            }
            if (timeSpan.TotalDays > 3) 
            {
                textCountDown.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part2Text2").Replace("{0}", timeSpan.TotalDays.ToString());
            }
            else 
            {
                textCountDown.text = ExtensionTool.GetFormatTime(timeOffset - DateTimeOffset.UtcNow).ToString();
            }
        }
    }
    string prefabName;
    private void RefreshPrefabName(string prefabName) //传的是点击的物品名字，不是按钮的名字
    {
        this.prefabName = prefabName;
    }
    #endregion

}
