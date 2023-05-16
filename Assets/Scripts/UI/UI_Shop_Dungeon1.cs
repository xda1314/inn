using ivy.game;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop_Dungeon1 : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI lbl_Title;
    [SerializeField] private Button btn_Close;
    [SerializeField] private UI_Shop_NormalItem normalItemArray;
    [SerializeField] private RectTransform Container;
    private List<UI_Shop_NormalItem> uI_Shop_NormalItems = new List<UI_Shop_NormalItem>();

    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_Close.onClick.AddListener(() => UISystem.Instance.HideUI(this));
    }

    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        RefreshUIData(DungeonShopSystem.Instance.dungeon1FuncDataList);
    }

    private void Normalization(UI_Shop_NormalItem normalItemArray)
    {
        normalItemArray.transform.parent= Container;
        //normalItemArray.transform.localPosition = Vector3.zero;
        normalItemArray.transform.localScale = Vector3.one;
        normalItemArray.gameObject.SetActive(true);
    }

    #region 副本1购买

    private void TimeOutCB(UI_Shop_NormalItem uI_Shop_NormalItem, ShopPackData shopPackData)
    {
        DungeonShopSystem.Instance.RemoveDateItem(shopPackData);
        uI_Shop_NormalItem.gameObject.SetActive(false);
        if(DungeonShopSystem.Instance.dungeon1FuncDataList.Count==0)
            UISystem.Instance.HideUI(this);
    }

    private void RefreshShopItem(ShopPackData shopPackData, UI_Shop_NormalItem uI_Shop_NormalItem)
    {
        Action<ShopPackData, Vector3> buyPersonalCB = (DungeonData, pos) =>
        {
            ShopSystem.Instance.BuyShopItem(ShopPackDefinition.ShopTag.dungeon1Func, DungeonData, pos, () =>
                RefreshUIData(DungeonShopSystem.Instance.dungeon1FuncDataList), () =>
                Currencies.NotEnoughCoinsOrGems(DungeonData.currencyID));
        };
        uI_Shop_NormalItem.SetItemInfo(shopPackData, buyPersonalCB);
        uI_Shop_NormalItem.timeOutCB = () =>
            TimeOutCB(uI_Shop_NormalItem, shopPackData);
        Normalization(uI_Shop_NormalItem);
    }

    public void RefreshUIData(List<ShopPackData> packDataList)
    {
        lbl_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part1");
        for (int i = 0; i < packDataList.Count; i++)
        {
            if (uI_Shop_NormalItems.Count<=i)
                uI_Shop_NormalItems.Add(Instantiate(normalItemArray));
            RefreshShopItem(packDataList[i], uI_Shop_NormalItems[i]);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(Container);
    }

    #endregion
}
