using ivy.game;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop_Dungeon : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI lbl_Title;
    [SerializeField] private UI_Shop_NormalItem[] normalItemArray;
    [SerializeField] private AutoScrollWindow Container;
    [SerializeField] private Transform itemList;
    [SerializeField] private ToggleView toggleView;

    public override void OnInitUI()
    {
        base.OnInitUI();
    }


    private void Normalization(int i)
    {
        normalItemArray[i].gameObject.SetActive(true);
        Container.AddChild(normalItemArray[i].transform as RectTransform);
        normalItemArray[i].transform.localPosition = Vector3.zero;
        normalItemArray[i].transform.localScale = Vector3.one;
    }

    public void HideUI()
    {
        for (int i = 0; i < normalItemArray.Length; i++)
        {
            normalItemArray[i].ClearIcon();
        }
    }

    #region 付费购买

    private void RemoveData(PayPackDefinition payPackDefinition)
    {
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.dungeon1)
        {
            ShopSystem.Instance.RemoveDungeon1BagList(payPackDefinition);
            this.gameObject.SetActive(ShopSystem.Instance.Dungeon1BagList.Count != 0);
            toggleView.RefreshToggles(Container, ShopSystem.Instance.Dungeon1BagList.Count);
        }
        else if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.dungeon2)
        {
            ShopSystem.Instance.RemoveDungeon2BagList(payPackDefinition);
            this.gameObject.SetActive(ShopSystem.Instance.Dungeon2BagList.Count != 0);
            toggleView.RefreshToggles(Container, ShopSystem.Instance.Dungeon2BagList.Count);
        }
        else if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.dungeon3)
        {
            ShopSystem.Instance.RemoveDungeon3BagList(payPackDefinition);
            this.gameObject.SetActive(ShopSystem.Instance.Dungeon3BagList.Count != 0);
            toggleView.RefreshToggles(Container, ShopSystem.Instance.Dungeon3BagList.Count);
        }
    }
    private void LoopScroll()
    {
        if (Container)
            Container.resizeChildren();
    }

    private void TimeOutCB(UI_Shop_NormalItem uI_Shop_NormalItem, PayPackDefinition shopPackData)
    {
        uI_Shop_NormalItem.gameObject.SetActive(false);
        uI_Shop_NormalItem.transform.parent = itemList;
        RemoveData(shopPackData);
    }

    private void RefreshShopItem(List<PayPackDefinition> packDataList, int i)
    {
        if (packDataList != null && packDataList.Count > i)
        {
            UI_Shop_NormalItem uI_Shop_NormalItem = normalItemArray[i];
            PayPackDefinition shopPackData = packDataList[i];
            Action<PayPackDefinition, Vector3> buyPayPersonalCB = (coinsDef, pos) =>
            {
                Action<PayPackDefinition> action = _ =>
                    TimeOutCB(uI_Shop_NormalItem, shopPackData);
                Billing.Instance.TryMakePurchase(coinsDef, pos, action);
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
            normalItemArray[i].transform.parent = itemList;
        }
    }


    public void RefreshUIData(List<PayPackDefinition> packDataList)
    {
        if (packDataList == null || packDataList.Count <= 0)
            return;
        lbl_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part2Text");
        for (int i = 0; i < normalItemArray.Length; i++)
        {
            RefreshShopItem(packDataList, i);
        }
        LoopScroll();
        toggleView.RefreshToggles(Container, packDataList.Count);
    }
}
#endregion
