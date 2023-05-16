using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InternalShopData : UIPanelDataBase
{
    public TopResourceType openUIType= TopResourceType.NONE;
    public InternalShopData(TopResourceType openUIType) : base(Consts.UI_InternalShop)
    {
        this.openUIType = openUIType;
    }
}

public class UI_InternalShop : UIPanelBase
{
    [SerializeField] private Button btn_Close;
    [SerializeField] private ScrollRect scrollView;
    [SerializeField] private UI_ShopContainer uI_ShopContainer;
    public TopResourceType openUIType;
    private float scrollViewHeight;

    public override void OnInitUI()
    {
        base.OnInitUI();
        base.uiType = UIType.Popup;
        AdManager.playAdEvent += () =>
        {
            if (gameObject.activeSelf)
                StartCoroutine(RefreshShopUI(openUIType));
        };          
        ShopSystem.Instance.refreshShopAction += () =>
        {
            if (gameObject.activeSelf)
                StartCoroutine(RefreshShopUI(openUIType));
        };
        if (btn_Close != null)
           btn_Close.onClick.AddListener(CloseButton);
    }

    public override IEnumerator OnShowUI()
    {
        scrollView.inertia = false;
        if ((InternalShopData)UIPanelData!=null)
        {
            openUIType = ((InternalShopData)UIPanelData).openUIType;
        }
        yield return RefreshShopUI(openUIType,true);
        yield return base.OnShowUI();
    }

    public IEnumerator RefreshShopUI(TopResourceType openUIType,bool isWait=false) 
    {
        yield return uI_ShopContainer.RefreshShopUI(openUIType);
        yield return uI_ShopContainer.CalculateCoinsOrGemsPosition();
        //if (isWait)
        //    yield return new WaitForSeconds(0.5f);
        LocalPosition(openUIType);
        Invoke("Resume", 1.0f);
        this.openUIType = openUIType;
    }
    void Resume()
    {
        scrollView.inertia = true;
    }


    private void CloseButton()
    {
        UISystem.Instance.HideUI(this, null);
    }

    //根据消费类型进行定位
    public void LocalPosition(TopResourceType topResource)
    {
        scrollViewHeight = scrollView.GetComponent<RectTransform>().sizeDelta.y;
        switch (topResource)
        {
            case TopResourceType.Coin:
                float yPos1 = uI_ShopContainer.CoinsPosition.y + uI_ShopContainer.CoinsHeight / 2 - scrollViewHeight + uI_ShopContainer.SpaceHeight;
                yPos1 = yPos1 > 0 ? yPos1 : 0;
                uI_ShopContainer.CalculatePosition(yPos1);
                break;
            case TopResourceType.Gem:
                float yPos2 = uI_ShopContainer.GemsPosition.y + uI_ShopContainer.GemsHeight / 2 - scrollViewHeight + uI_ShopContainer.SpaceHeight;
                yPos2 = yPos2 > 0 ? yPos2 : 0;
                uI_ShopContainer.CalculatePosition(yPos2);
                break;
            case TopResourceType.Needle:
                float yPos3 = uI_ShopContainer.DailyPosition.y + uI_ShopContainer.DailyHeight / 2 - scrollViewHeight + uI_ShopContainer.SpaceHeight;
                yPos3 = yPos3 > 0 ? yPos3 : 0;
                uI_ShopContainer.CalculatePosition(yPos3);
                break;
            default:
                float yPos = uI_ShopContainer.DefaultPosition.y + uI_ShopContainer.DefaultHeight / 2 - scrollViewHeight + uI_ShopContainer.SpaceHeight;
                yPos = yPos > 0 ? yPos : 0;
                uI_ShopContainer.CalculatePosition(0);
                break;
        }
    }

}


