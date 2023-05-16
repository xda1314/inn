using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Page_Shop : UIPanelBase
{
    [SerializeField] public ScrollRect scrollView;
    [SerializeField] public UI_ShopContainer uI_ShopContainer;
    public TopResourceType openUIType;
    private float scrollViewHeight;


    public override void OnInitUI()
    {
        base.OnInitUI();
        base.uiType = UIType.Popup;
        AdManager.playAdEvent += () => StartCoroutine(OnShowShop());
        ShopSystem.Instance.refreshShopAction += () => StartCoroutine(OnShowShop());
        StartCoroutine(OnShowShop());
    }

    public void RefreshLanguageText()
    {
        StartCoroutine(uI_ShopContainer.RefreshShopUI());
    }
    public IEnumerator OnShowShop()
    {
        scrollView.inertia = false;
        yield return uI_ShopContainer.RefreshShopUI();
        yield return uI_ShopContainer.CalculateCoinsOrGemsPosition();
        Invoke("Resume", 1.0f);
    }
    void Resume()
    {
        scrollView.inertia = true;
    }

    //根据消费类型进行定位
    public IEnumerator LocalPosition(TopResourceType topResource, bool isWait)
    {
        if (isWait)
            yield return new WaitForSeconds(0.5f);
        float screenHeight = (UnityEngine.Screen.height / 2160f) > (UnityEngine.Screen.width / 1080f) ? Screen.height * 1f * 1080 / Screen.width : 2160;
        scrollViewHeight = screenHeight + scrollView.GetComponent<RectTransform>().sizeDelta.y;
        switch (topResource)
        {
            case TopResourceType.Coin:
                float yPos1 = uI_ShopContainer.CoinsPosition.y + uI_ShopContainer.CoinsHeight / 2 - scrollViewHeight + uI_ShopContainer.SpaceHeight + 60;
                yPos1 = yPos1 > 0 ? yPos1 : 0;
                uI_ShopContainer.CalculatePosition(yPos1);
                break;
            case TopResourceType.Gem:
                float yPos2 = uI_ShopContainer.GemsPosition.y + uI_ShopContainer.GemsHeight / 2 - scrollViewHeight + uI_ShopContainer.SpaceHeight + 60;
                yPos2 = yPos2 > 0 ? yPos2 : 0;
                uI_ShopContainer.CalculatePosition(yPos2);
                break;
            default:
                uI_ShopContainer.CalculatePosition(0);
                break;
        }
        yield return null;
    }

}


/// <summary>
/// 顶部资源条类型
/// </summary>
public enum TopResourceType
{
    NONE,
    Exp,
    Energy,
    Coin,
    Gem,
    DungeonEnergy,
    Needle
}
