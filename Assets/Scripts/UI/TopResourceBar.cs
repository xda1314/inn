using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TopResourceBar : MonoBehaviour
{
    public TopResourceType openUIType;

    public static event Action<TopResourceType> onOpenShopPage;

    private IEnumerator ResetButtonEvent()
    {
        gameObject.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Button>().interactable = true;
    }

    void Start()
    {
        if(openUIType == TopResourceType.Coin)
            Currencies.notEnoughCoinsEvent += () => OpenTopPopupPanel(TopResourceType.Coin);
        else if (openUIType == TopResourceType.Gem)
            Currencies.notEnoughGemsEvent += () => OpenTopPopupPanel(TopResourceType.Gem);
        gameObject.GetComponent<Button>().onClick.AddListener(() =>OpenTopPopupPanel(openUIType));
    }
     

    private void RefreshOrCreateShopPanel(TopResourceType topResourceType)
    {
        if (UISystem.Instance.TryGetUI(Consts.UI_InternalShop, out UI_InternalShop popup))
        {
            if (popup)
                StartCoroutine(popup.RefreshShopUI(topResourceType));
            else
                UISystem.Instance.ShowUI(new InternalShopData(topResourceType));
        }
        else
            UISystem.Instance.ShowUI(new InternalShopData(topResourceType));
    }

    private void OpenTopPopupPanel(TopResourceType openUIType)
    {
        ////AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
        if (UISystem.Instance.TryGetUI(Consts.UI_TopPopup, out TopPopup popup))
        {
            if (popup != null)
                return;
        }
        bool isInMergePanel = false;
        if (UISystem.Instance.TryGetUI(Consts.UIPanel_Merge, out UIPanel_Merge uIPanel_Merge))
        {
            isInMergePanel = uIPanel_Merge.gameObject.activeInHierarchy;
        }
        switch (openUIType)
        {
            case TopResourceType.Energy:
                try
                {
                    UISystem.Instance.ShowUI(new TopPopupData(TopResourceType.Energy));
                }
                catch (Exception e)
                {
                    GameDebug.LogError("弹出体力界面错误:" + e);
                }
                break;
            //case TopResourceType.DungeonEnergy:
            //    try
            //    {
            //        UISystem.Instance.ShowUI(new TopPopupData(TopResourceType.Energy));
            //    }
            //    catch (Exception e)
            //    {
            //        GameDebug.LogError("弹出体力界面错误:" + e);
            //    }
            //    break;
            case TopResourceType.Exp:
                try
                {
                    UISystem.Instance.ShowUI(new TopPopupData(TopResourceType.Exp));
                }
                catch (Exception e)
                {
                    GameDebug.LogError("弹出经验界面错误:" + e);
                }
                break;
            case TopResourceType.Coin:
                try
                {
                    if (isInMergePanel)
                        RefreshOrCreateShopPanel(TopResourceType.Coin);
                    else if (!UISystem.Instance.HasUIInShow)
                    {
                        StartCoroutine(ResetButtonEvent());
                        onOpenShopPage?.Invoke(TopResourceType.Coin);
                    }
                }
                catch (Exception e)
                {
                    GameDebug.LogError("Open Shop Panel Error!" + e);
                }
                break;
            case TopResourceType.Gem:
                try
                {
                    if (isInMergePanel)
                        RefreshOrCreateShopPanel(TopResourceType.Gem);
                    else if (!UISystem.Instance.HasUIInShow)
                    {
                        StartCoroutine(ResetButtonEvent());
                        onOpenShopPage?.Invoke(TopResourceType.Gem);
                    }
                }
                catch (Exception e)
                {
                    GameDebug.LogError("Open Shop Panel Error!" + e);
                }
                break;
            default:
                break;
        }
    }
}
