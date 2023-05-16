using ivy.game;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop_Offer : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI lbl_Title;
    [SerializeField] private TextMeshProUGUI text_more;
    [SerializeField] private Transform Container;
    [SerializeField] private List<UI_Shop_OfferItem> offerItemList;
    [SerializeField] private Transform itemList;
    [SerializeField] private Button btn_ShowMore;
    List<PayPackDefinition> packDataList;
    public Action action;
    private bool is_showMore=false;
    public void Start()
    {
        btn_ShowMore.gameObject.SetActive(false);
        btn_ShowMore.onClick.AddListener(delegate
        {
            is_showMore = true;
            btn_ShowMore.gameObject.SetActive(false);
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            action?.Invoke();
        });
    }

    public bool GetIsShowMore()
    {
        return is_showMore;
    }

    public void ResetBTN()
    {
        is_showMore = false;
        btn_ShowMore.gameObject.SetActive(true);
    }
    private void RefreshText()
    {
        lbl_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Packper/Title");
        text_more.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Packper/More");
    }

    public void HideUI()
    {
        for (int i = 0; i < offerItemList.Count; i++)
        {
            offerItemList[i].ClearIcon();
        }
    }

    #region 付费购买

    private void RemoveData(PayPackDefinition payPackDefinition)
    {
        ShopSystem.Instance.RemoveOfferList(payPackDefinition);
        this.gameObject.SetActive(ShopSystem.Instance.CoinsRandomBagList.Count != 0);
    }

    private void TimeOrBuyCB(UI_Shop_OfferItem ui_Shop_OfferItem, PayPackDefinition shopPackData)
    {
        //ui_Shop_OfferItem.gameObject.SetActive(false);
        //Destroy(ui_Shop_OfferItem);
        //RemoveData(shopPackData);
        //ShopSystem.Instance.SetNeedRefresh("true");
    }

    private void Normalization(int i)
    {
        offerItemList[i].gameObject.SetActive(true);
        offerItemList[i].transform.SetParent(Container);
        offerItemList[i].transform.localPosition = Vector3.zero;
        offerItemList[i].transform.localScale = Vector3.one;
    }

    private void RefreshShopItem(List<PayPackDefinition> packDataList, int i)
    {
        UI_Shop_OfferItem uI_Shop_OfferItem = null;
        PayPackDefinition shopPackData = packDataList[i];
        if (offerItemList != null && offerItemList.Count > i)
        {
            uI_Shop_OfferItem = offerItemList[i];
        }
        else
        {
            GameObject go = AssetSystem.Instance.Instantiate(Consts.UI_Shop_OfferItem, Container);
            offerItemList.Add(go.GetComponent<UI_Shop_OfferItem>());
        }
        Action<PayPackDefinition, Vector3> buyPayPersonalCB = (coinsDef, pos) =>
        {
            //Action action = () =>
            //{
            //    TimeOrBuyCB(uI_Shop_OfferItem, shopPackData);
            //    uI_Shop_OfferItem.BuyCallBack?.Invoke();
            //};  
            Billing.Instance.TryMakePurchase(coinsDef, pos, _ =>
            {
                if (this != null && uI_Shop_OfferItem != null && shopPackData != null)
                    uI_Shop_OfferItem.BuyCallBack?.Invoke();
            });

        };
        Normalization(i);
        offerItemList[i].SetItemInfo(shopPackData, buyPayPersonalCB);
    }

    public void RefreshUIData(List<PayPackDefinition> packDataList)
    {
        this.packDataList = packDataList;
        RefreshText();
        for (int i = 0; i < packDataList.Count; i++)
        {
            RefreshShopItem(packDataList, i);
            offerItemList[i].gameObject.SetActive(true);
        }
        if (packDataList.Count <= 0 || offerItemList.Count <= 0)
            return;
        for (int i = packDataList.Count; i < offerItemList.Count; i++)
        {
            offerItemList[i].gameObject.SetActive(false);
            offerItemList[i].transform.SetParent(itemList, false);
        }
        btn_ShowMore.transform.SetAsLastSibling(); 
    }     
}
#endregion
