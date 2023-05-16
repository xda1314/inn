using DG.Tweening;
using Ivy.Purchase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShopContainer : UIPanelBase
{
    [SerializeField] private RectTransform content;
    [SerializeField] private HVPageView hvPageView;
    public TopResourceType openUIType;
    //计算需要定位位置
    private bool is_first = true;
    public void CalculatePosition(float YPosition)
    {
        Vector3 targetPos = new Vector3(content.localPosition.x, YPosition, content.localPosition.z);
        if (!is_first)
            DOTween.To(() => this.content.localPosition, x => content.localPosition = x, targetPos, 0.3f);
        else
            this.content.localPosition = targetPos;
        is_first = false;
    }

    public UI_Shop_MonthlyCard shop_MonthlyCard;
    private UI_Shop_Daily shop_Daily;
    private UI_Shop_Discount shop_Discount;
    private UI_Shop_Packages shop_BoxPackage;
    private UI_Shop_GemsOrCoins shop_Gems;
    private UI_Shop_GemsOrCoins shop_Coins;
    private UI_Shop_Personal shop_Personal;
    private UI_Shop_Offer uI_Shop_Offer;
    private UI_Shop_Offer uI_Shop_MoreOffer;
    private UI_Shop_Dungeon shop_Dungeon;
    private UI_Shop_AD ui_Shop_AD;
    private Action<ShopPackData, Vector3> buyDailyCB;
    private Action refreshDiscountCB;
    private Action<ShopPackData, Vector3> buyDiscountCB;
    private Action<ShopPackData, Vector3> buyBoxPackageCB;
    private Action<PayPackDefinition, Vector3> buyGemsCB;
    private Action<PayPackDefinition, Vector3> buyCoinCB;
    public static event Action<BillingPriceInfo> shopGetBillingPriceInfoCB;
    private bool canUpdateDate = false;

    #region 商城刷新

    #region 私人定制礼包
    private List<PayPackDefinition> GetShopPersonalData(TopResourceType openUIType)
    {
        return ShopSystem.Instance.CoinsRandomBagList;
    }

    private bool IsMainLine()
    {
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine || MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.none)
            return true;
        return false;
    }
    private bool IsBranch()
    {
        if (MergeLevelManager.Instance.IsBranch(MergeLevelManager.Instance.CurrentLevelType))
            return true;
        return false;
    }

    public void RefreshSpendShopUI()
    {
        List<PayPackDefinition> shopPackDatas = GetShopPersonalData(this.openUIType);
        if (shop_Personal == null)
        {
            GameObject PersonalGO = AssetSystem.Instance.Instantiate(Consts.UI_Shop_Personal, content.transform);
            shop_Personal = PersonalGO.GetComponent<UI_Shop_Personal>();
            //向HVPageView添加 不需要滑动区域
            //if (hvPageView != null)
            //    hvPageView.DragScollVPageArea.Add(shop_Personal.transform as RectTransform);
        }
        shop_Personal.transform.SetAsLastSibling();
        shop_Personal.RefreshUIData(shopPackDatas);
        shop_Personal.gameObject.SetActive(false);
    }
    #endregion

    #region AD礼包
    private void RefreshADUI()
    {
        if (ui_Shop_AD == null)
        {
            GameObject cardGo = AssetSystem.Instance.Instantiate(Consts.UI_Shop_AD, content.transform);
            ui_Shop_AD = cardGo.GetComponent<UI_Shop_AD>();

        }
        ui_Shop_AD.RefreshUIData();
        ui_Shop_AD.transform.SetAsLastSibling();
    }

    #endregion

    #region Offer礼包
    private List<PayPackDefinition> GetShopOfferData(bool less)
    {
        ShopSystem.Instance.InitOfferList();
        List<PayPackDefinition> payPacks = new List<PayPackDefinition>();
        if (less) 
        {
            payPacks.Add(ShopSystem.Instance.OfferList[0]);
            payPacks.Add(ShopSystem.Instance.OfferList[1]);
        }
        else 
        {
            for (int i = 0; i < ShopSystem.Instance.OfferList.Count; i++)
            {
                if (i < 2) { continue; }
                else
                {
                    payPacks.Add(ShopSystem.Instance.OfferList[i]);
                }
            }
        }
        return payPacks;
    }

    public void RefreshOfferShopUI()
    {
        List<PayPackDefinition> shopPackDatas = GetShopOfferData(true);
        if (uI_Shop_Offer == null)
        {
            GameObject OfferGO = AssetSystem.Instance.Instantiate(Consts.UI_Shop_Offer, content.transform);
            uI_Shop_Offer = OfferGO.GetComponent<UI_Shop_Offer>();
        }
        uI_Shop_Offer.transform.SetAsLastSibling();
        uI_Shop_Offer.RefreshUIData(shopPackDatas);
        uI_Shop_Offer.gameObject.SetActive(shopPackDatas.Count > 0 && IsMainLine());
        //uI_Shop_Offer.ResetBTN();
        
        uI_Shop_Offer.action = () =>
        {
            if(uI_Shop_MoreOffer != null) 
            {
                uI_Shop_MoreOffer.gameObject.SetActive(true); 
            }
            StartCoroutine(CalculateCoinsOrGemsPosition());
        };
                
    }

    public void RefreshMoreOfferShopUI()
    {
        List<PayPackDefinition> shopPackDatas = GetShopOfferData(false);
        if (uI_Shop_MoreOffer == null)
        {
            GameObject OfferGO = AssetSystem.Instance.Instantiate(Consts.UI_Shop_MoreOffer, content.transform);
            uI_Shop_MoreOffer = OfferGO.GetComponent<UI_Shop_Offer>();
            uI_Shop_MoreOffer.gameObject.SetActive(true);
        }
        //if (uI_Shop_Offer.GetIsShowMore())
        //{
        //    uI_Shop_MoreOffer.gameObject.SetActive(true);
        //}
        uI_Shop_MoreOffer.transform.SetAsLastSibling();
        uI_Shop_MoreOffer.RefreshUIData(shopPackDatas);
        uI_Shop_MoreOffer.gameObject.SetActive(shopPackDatas.Count > 0 && IsMainLine());
    }
    #endregion

    private void RefreshMonthCardUI()
    {
        if (shop_MonthlyCard == null)
        {
            GameObject cardGo = AssetSystem.Instance.Instantiate(Consts.UI_Shop_MonthlyCard, content.transform);
            shop_MonthlyCard = cardGo.GetComponent<UI_Shop_MonthlyCard>();
        }
        if (shop_MonthlyCard != null)
        {
            shop_MonthlyCard.InitUIInfo();
        }
        shop_MonthlyCard.transform.SetAsLastSibling();
    }

    #region 副本定制礼包
    private List<PayPackDefinition> GetShopDungeonData()
    {
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.dungeon1)
            return ShopSystem.Instance.Dungeon1BagList;
        else if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.dungeon2)
            return ShopSystem.Instance.Dungeon2BagList;
        else if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.dungeon3)
            return ShopSystem.Instance.Dungeon3BagList;
        return null;
    }


    private void RefreshDungeonShopUI()
    {
        List<PayPackDefinition> shopPackDatas = GetShopDungeonData();
        if (shop_Dungeon == null)
        {
            GameObject DungeonGO = AssetSystem.Instance.Instantiate(Consts.UI_Shop_Dungeon, content.transform);
            shop_Dungeon = DungeonGO.GetComponent<UI_Shop_Dungeon>();
        }
        shop_Dungeon.transform.SetAsFirstSibling();
        shop_Dungeon.RefreshUIData(shopPackDatas);
        shop_Dungeon.gameObject.SetActive(shopPackDatas != null && shopPackDatas.Count > 0 && !IsMainLine() && openUIType == TopResourceType.NONE);
    }
    #endregion

    #region 每日礼包
    private void RefreshDailyShopUI()
    {
        if (shop_Daily == null)
        {
            GameObject dailyGO = AssetSystem.Instance.Instantiate(Consts.UI_Shop_Daily, content.transform);
            shop_Daily = dailyGO.GetComponent<UI_Shop_Daily>();
        }
        shop_Daily.transform.SetAsLastSibling();
        buyDailyCB = (dailyData, pos) =>
        {
            ShopSystem.Instance.BuyShopItem(ShopPackDefinition.ShopTag.daily, dailyData, pos, () =>
            {
                shop_Daily.RefreshUIData(ShopSystem.Instance.dailyPackDataList, buyDailyCB);

            }, () =>
            {
                Currencies.NotEnoughCoinsOrGems(dailyData.currencyID);
            });
        };
        shop_Daily.RefreshUIData(ShopSystem.Instance.dailyPackDataList, buyDailyCB);
        //shop_Daily.gameObject.SetActive(IsMainLine() && !IsBranch());
        if (!shop_Daily.gameObject.activeSelf) 
        {
            shop_Daily.gameObject.SetActive(true);//因为每日礼包增加了针元素，故改为所有副本均显示
        }
    }
    #endregion

    #region 折扣礼包
    private void RefreshDiscountShopUI()
    {
        List<ShopPackData> discountList = ShopSystem.Instance.GetDiscountList();
        if (shop_Discount == null)
        {
            GameObject discountGO = AssetSystem.Instance.Instantiate(Consts.UI_Shop_Discount, content.transform);
            shop_Discount = discountGO.GetComponent<UI_Shop_Discount>();
        }
        shop_Discount.transform.SetAsLastSibling();
        buyDiscountCB = (discountData, pos) =>
        {
            ShopSystem.Instance.BuyShopItem(ShopPackDefinition.ShopTag.discount, discountData, pos, () =>
            {
                shop_Discount.RefreshUIData(discountList, refreshDiscountCB, buyDiscountCB);
            }, () =>
            {
                Currencies.NotEnoughCoinsOrGems(discountData.currencyID);
            });
        };
        refreshDiscountCB = () =>
        {
            ShopSystem.Instance.RefreshSingeDiscountPack();
            ShopSystem.Instance.SaveDiscountData();
            shop_Discount.RefreshUIData(discountList, refreshDiscountCB, buyDiscountCB);
        };
        shop_Discount.RefreshUIData(discountList, refreshDiscountCB, buyDiscountCB);
        shop_Discount.gameObject.SetActive(discountList.Count != 0 && IsMainLine());
    }
    #endregion

    #region 盒子礼包
    private void RefreshBoxShopUI()
    {
        List<ShopPackData> boxPackList = ShopSystem.Instance.boxPackDataList;
        if (shop_BoxPackage == null)
        {
            GameObject boxPackGO = AssetSystem.Instance.Instantiate(Consts.UI_Shop_Packages, content.transform);
            shop_BoxPackage = boxPackGO.GetComponent<UI_Shop_Packages>();
        }
        shop_BoxPackage.transform.SetAsLastSibling();
        buyBoxPackageCB = (boxData, pos) =>
        {
            ShopSystem.Instance.BuyShopItem(ShopPackDefinition.ShopTag.discount, boxData, pos, () =>
            {
                shop_BoxPackage.RefreshUIData(boxPackList, buyBoxPackageCB);
            }, () =>
            {
                Currencies.NotEnoughCoinsOrGems(boxData.currencyID);
            });
        };
        shop_BoxPackage.RefreshUIData(boxPackList, buyBoxPackageCB);
        shop_BoxPackage.gameObject.SetActive(boxPackList.Count != 0 && !IsBranch() && IsMainLine());
    }

    #endregion

    #region 砖石礼包
    private void RefreshGemsShopUI()
    {
        if (shop_Gems == null)
        {
            GameObject gemsGO = AssetSystem.Instance.Instantiate(Consts.UI_Shop_GemsOrCoins, content.transform);
            shop_Gems = gemsGO.GetComponent<UI_Shop_GemsOrCoins>();
        }
        shop_Gems.transform.SetAsLastSibling();
        buyGemsCB = (gemsDef, pos) =>
        {
            Billing.Instance.TryMakePurchase(gemsDef, pos);
        };
        shop_Gems.RefreshUIData(true, ShopSystem.Instance.gemsPayPackDefList, buyGemsCB);
    }

    #endregion

    #region 金币礼包
    private void RefreshCoinsShopUI()
    {
        if (shop_Coins == null)
        {
            GameObject coinGO = AssetSystem.Instance.Instantiate(Consts.UI_Shop_GemsOrCoins, content.transform);
            shop_Coins = coinGO.GetComponent<UI_Shop_GemsOrCoins>();

        }
        shop_Coins.transform.SetAsLastSibling();
        buyCoinCB = (coinsDef, pos) =>
        {
            Billing.Instance.TryMakePurchase(coinsDef, pos);
        };
        shop_Coins.RefreshUIData(false, ShopSystem.Instance.coinsPayPackDefList, buyCoinCB);
    }


    //刷新商城
    public IEnumerator RefreshShopUI(TopResourceType openUIType = TopResourceType.NONE)
    {
        ShopSystem.Instance.ClearTimeoutData();
        this.openUIType = openUIType;
        canUpdateDate = true;

        shopGetBillingPriceInfoCB = null;

        //RefreshMonthCardUI();
        RefreshADUI();
        RefreshOfferShopUI();
        RefreshMoreOfferShopUI();

        RefreshDailyShopUI();
        RefreshSpendShopUI();

        RefreshDiscountShopUI();
        RefreshBoxShopUI();
        RefreshGemsShopUI();
        RefreshCoinsShopUI();
        yield return null;

        //获取价格后刷新
        List<(BillingDataBase, Action<BillingPriceInfo>)> pays = new List<(BillingDataBase, Action<BillingPriceInfo>)>();
        foreach (var item in PayPackDefinition.Definitions)
        {
            (PayPackDefinition, Action<BillingPriceInfo>) tuple = (item, shopGetBillingPriceInfoCB);
            pays.Add(tuple);
        }
        Billing.SearchPriceInfoAsync_List(pays);
        LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        yield return null;
        SetPrefabStaus();
    }

    private void SetPrefabStaus() 
    {
        //uI_Shop_MoreOffer.gameObject.SetActive(false);
    }
    #endregion

    private float timer = 0;
    private void Update()
    {
        if (!canUpdateDate)
        {
            return;
        }
        if (!TimeManager.IsGetServerUtcSuccess)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer < 1)
        {
            return;
        }
        timer -= 1;

        if (ExtensionTool.IsDateBeforeToday(ShopSystem.Instance.lastRefreshDateTime, TimeManager.ServerUtcNow()))
        {
            timer += 60;
            if (ShopSystem.Instance.TryRefreshAllData())
            {
                RefreshShopUI();
            }
        }
    }
    [NonSerialized] public Vector3 DailyPosition;
    [NonSerialized] public Vector3 CoinsPosition;
    [NonSerialized] public Vector3 GemsPosition;
    [NonSerialized] public Vector3 DefaultPosition;
    [NonSerialized] public float DailyHeight;
    [NonSerialized] public float CoinsHeight;
    [NonSerialized] public float GemsHeight;
    [NonSerialized] public float DefaultHeight;
    [NonSerialized] public float SpaceHeight;
    public IEnumerator CalculateCoinsOrGemsPosition()
    {
        yield return null;//延时一针等待商店物品创建完毕，否则位置初始化不准确
        LayoutRebuilder.ForceRebuildLayoutImmediate(content);      
        DailyPosition = -shop_Daily.transform.localPosition;
        CoinsPosition = -shop_Coins.transform.localPosition;
        GemsPosition = -shop_Gems.transform.localPosition;
        DefaultPosition = -shop_Discount.transform.localPosition;
        DailyHeight = shop_Daily.GetComponent<RectTransform>().sizeDelta.y;
        CoinsHeight = shop_Coins.GetComponent<RectTransform>().sizeDelta.y;
        GemsHeight = shop_Gems.GetComponent<RectTransform>().sizeDelta.y;
        DefaultHeight = shop_Discount.isActiveAndEnabled == true ? shop_Discount.GetComponent<RectTransform>().sizeDelta.y : 0;
        SpaceHeight = content.GetComponent<VerticalLayoutGroup>().spacing;
        yield return null;
    }

    #endregion 
}