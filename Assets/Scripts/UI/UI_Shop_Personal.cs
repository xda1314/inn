using ivy.game;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// hot商场（跟随任务改变推送商品）
/// </summary>
public class UI_Shop_Personal : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI lbl_Title;
    [SerializeField] private UI_Shop_NormalItem[] normalItemArray;
    [SerializeField] private Transform Container;
    [SerializeField] private Transform itemList;
    //[SerializeField] private ToggleView toggleView;

    private void RefreshText()
    {
        lbl_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part1");
    }


    private void Normalization(int i)
    {
        normalItemArray[i].gameObject.SetActive(true);
        //Container.AddChild(normalItemArray[i].transform as RectTransform);
        normalItemArray[i].transform.SetParent(Container);
        normalItemArray[i].transform.localPosition = Vector3.zero;
        normalItemArray[i].transform.localScale = Vector3.one;
    }

    private void SetTimeOutCB(UI_Shop_NormalItem uI_Shop_NormalItem)
    {

    }

    public void HideUI()
    {
        for (int i = 0; i < normalItemArray.Length; i++)
        {
            normalItemArray[i].ClearIcon();
        }
    }

    #region 普通购买

    private void TimeOutCB(UI_Shop_NormalItem uI_Shop_NormalItem, ShopPackData shopPackData)
    {
        uI_Shop_NormalItem.gameObject.SetActive(false);
        uI_Shop_NormalItem.transform.SetParent(itemList);
        this.gameObject.SetActive(ShopSystem.Instance.ShopRandomBagList.Count != 0);
        //toggleView.RefreshToggles(Container, ShopSystem.Instance.ShopRandomBagList.Count);
        ShopSystem.Instance.SetNeedRefresh("true");
    }

    private void RefreshShopItem(List<ShopPackData> packDataList, int i)
    {
        if (packDataList != null && packDataList.Count > i)
        {
            UI_Shop_NormalItem uI_Shop_NormalItem = normalItemArray[i];
            ShopPackData shopPackData = packDataList[i];
            Action<ShopPackData, Vector3> buyPersonalCB = (PersonalData, pos) =>
            {
                ShopSystem.Instance.BuyShopItem(ShopPackDefinition.ShopTag.personal, PersonalData, pos, () =>
                    TimeOutCB(uI_Shop_NormalItem, shopPackData), () =>
                    Currencies.NotEnoughCoinsOrGems(PersonalData.currencyID));
            };
            uI_Shop_NormalItem.SetItemInfo(shopPackData, buyPersonalCB);
            Normalization(i);
            uI_Shop_NormalItem.timeOutCB = () =>
                {
                    ShopSystem.Instance.RemoveShopRandomBagList(shopPackData);
                    TimeOutCB(uI_Shop_NormalItem, shopPackData);
                };
        }
        else
        {
            normalItemArray[i].gameObject.SetActive(false);
            normalItemArray[i].transform.parent = itemList;
        }
    }

    public void RefreshUIData(List<ShopPackData> packDataList)
    {
        lbl_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part2");

        for (int i = 0; i < normalItemArray.Length; i++)
        {
            RefreshShopItem(packDataList, i);
        }
        LoopScroll();
        //toggleView.RefreshToggles(Container, packDataList.Count);
    }

    #endregion


    #region 付费购买

    private void RemoveData(PayPackDefinition payPackDefinition)
    {
        ShopSystem.Instance.RemoveCoinsRandomBagList(payPackDefinition);
        this.gameObject.SetActive(ShopSystem.Instance.CoinsRandomBagList.Count != 0);
        //toggleView.RefreshToggles(Container, ShopSystem.Instance.CoinsRandomBagList.Count);
    }

    private void TimeOutCB(UI_Shop_NormalItem uI_Shop_NormalItem, PayPackDefinition shopPackData)
    {
        uI_Shop_NormalItem.gameObject.SetActive(false);
        uI_Shop_NormalItem.transform.SetParent(itemList);
        RemoveData(shopPackData);
        ShopSystem.Instance.SetNeedRefresh("true");
    }

    private void RefreshShopItem(List<PayPackDefinition> packDataList, int i)
    {
        if (packDataList != null && packDataList.Count > i)
        {
            UI_Shop_NormalItem uI_Shop_NormalItem = normalItemArray[i];
            PayPackDefinition shopPackData = packDataList[i];
            Action<PayPackDefinition, Vector3> buyPayPersonalCB = (coinsDef, pos) =>
            {
                Action action = () =>
                    TimeOutCB(uI_Shop_NormalItem, shopPackData);
                Billing.Instance.TryMakePurchase(coinsDef, pos, _ =>
                {
                    action.Invoke();
                    //DailyTaskSystem.Instance.DailyTaskEvent_ShopBuy.Invoke();
                });
            };
            Normalization(i);
            normalItemArray[i].SetItemInfo(shopPackData, buyPayPersonalCB);
            normalItemArray[i].SetRemain();
            uI_Shop_NormalItem.timeOutCB = () =>
                TimeOutCB(uI_Shop_NormalItem, shopPackData);
        }
        else
        {
            normalItemArray[i].gameObject.SetActive(false);
            normalItemArray[i].transform.SetParent(itemList, false);
        }
    }

    private void LoopScroll()
    {
        //if (Container)
        //    Container.resizeChildren();
    }


    public void RefreshUIData(List<PayPackDefinition> packDataList)
    {
        lbl_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part2Text");
        for (int i = 0; i < normalItemArray.Length; i++)
        {
            RefreshShopItem(packDataList, i);
        }
        //toggleView.RefreshToggles(Container, packDataList.Count);
        LoopScroll();
    }
}
#endregion
