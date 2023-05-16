using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ivy;

public class UI_BannerItem : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private TextMeshProUGUI t_Desc;
    [SerializeField] private Button btn_Goto;
    [SerializeField] private TextMeshProUGUI t_Goto;

    [Header("hot sale专用")]
    [SerializeField] Transform[] ItemParents;
    [SerializeField] TextMeshProUGUI[] t_ItemNum;
    [SerializeField] TextMeshProUGUI t_OriginalPrice;
    [SerializeField] TextMeshProUGUI t_DiscountPrice;
    [SerializeField] TextMeshProUGUI t_DiscountOff;
    public void Init()
    {
        switch (index)
        {
            case 0://邮箱
                btn_Goto.onClick.AddListener(() => { UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_Mail)); });
                break;
            case 1://转盘
                btn_Goto.onClick.AddListener(() => { UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_Spin)); });
                break;
            case 2://购买新手礼包
                btn_Goto.onClick.AddListener(() => { UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_NovicePackage)); });
                break;
            case 3://订单
                btn_Goto.onClick.AddListener(() => { UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_Task)); });
                break;
            case 4://游戏广告
                //btn_Goto.onClick.AddListener(() => { });
                break;
            case 5://elves
                btn_Goto.onClick.AddListener(() =>
                {
#if UNITY_ANDROID
                    AnalyticsUtil.TrackEvent("click_promote_link", new Dictionary<string, string>()
                    {
                        { "label","banner"},
                        {"promoteapp","com.merge.elves" }
                    });
                    RiseSdk.Instance.GetApp("com.merge.elves");
#elif UNITY_IOS
                    AnalyticsUtil.TrackEvent("click_promote_link", new Dictionary<string, string>()
                    {
                        { "label","banner"},
                        {"promoteapp","1558066074" }
                    });
            RiseSdk.Instance.GetApp("1558066074");
#endif
                });
                break;
            case 6://town
                btn_Goto.onClick.AddListener(() =>
                {
#if UNITY_ANDROID
                    AnalyticsUtil.TrackEvent("click_promote_link", new Dictionary<string, string>()
                    {
                        { "label","banner"},
                        {"promoteapp","com.merge.farmtown" }
                    });
                    RiseSdk.Instance.GetApp("com.merge.farmtown");
#elif UNITY_IOS
                    AnalyticsUtil.TrackEvent("click_promote_link", new Dictionary<string, string>()
                    {
                        { "label","banner"},
                        {"promoteapp","1569751630" }
                    });
            RiseSdk.Instance.GetApp("1569751630");
#endif
                });
                break;
            case 7://经验提升礼包
                btn_Goto.onClick.AddListener(() => { UISystem.Instance.ShowUI(new TopPopupData(TopResourceType.Exp)); });
                break;
            case 8://主界面等级礼包
                btn_Goto.onClick.AddListener(() => { UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_LevelRewards)); });
                break;
            case 9://主界面星星礼包
                btn_Goto.onClick.AddListener(() => { UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_StarRewards)); });
                break;
            case 10://Hot Sale礼包1
                btn_Goto.onClick.AddListener(()=> 
                {
                    if (ShopSystem.Instance.CoinsRandomBagList.Count > 0 && ShopSystem.Instance.CoinsRandomBagList[0] != null)
                    {
                        PayPackDefinition payPackDefinition = ShopSystem.Instance.CoinsRandomBagList[0];
                        Billing.Instance.TryMakePurchase(payPackDefinition, Vector3.zero, _ =>
                        {
                            ShopSystem.Instance.RemoveCoinsRandomBagList(payPackDefinition);
                        });
                    }
                });           
                break;
            case 11://Hot Sale礼包2
                btn_Goto.onClick.AddListener(() =>
                {
                    if (ShopSystem.Instance.CoinsRandomBagList.Count > 1 && ShopSystem.Instance.CoinsRandomBagList[1] != null)
                    {
                        PayPackDefinition payPackDefinition = ShopSystem.Instance.CoinsRandomBagList[1];
                        Billing.Instance.TryMakePurchase(payPackDefinition, Vector3.zero, _ =>
                        {
                            ShopSystem.Instance.RemoveCoinsRandomBagList(payPackDefinition);
                        });
                    }
                });               
                break;
            case 12://Hot Sale礼包3
                btn_Goto.onClick.AddListener(() =>
                {
                    if (ShopSystem.Instance.CoinsRandomBagList.Count > 2 && ShopSystem.Instance.CoinsRandomBagList[2] != null)
                    {
                        PayPackDefinition payPackDefinition = ShopSystem.Instance.CoinsRandomBagList[2];
                        Billing.Instance.TryMakePurchase(payPackDefinition, Vector3.zero, _ =>
                        {
                            ShopSystem.Instance.RemoveCoinsRandomBagList(payPackDefinition);                         
                        });
                    }
                });            
                break;
            default:
                break;
        }
    }
    public void Refresh()
    {
        switch (index)
        {
            case 0:
                t_Desc.text = I2.Loc.ScriptLocalization.Get("Obj/Banner/Text1");
                t_Goto.text = I2.Loc.ScriptLocalization.Get("Obj/Banner/Btn/Text");
                break;
            case 1:
                t_Desc.text = I2.Loc.ScriptLocalization.Get("Obj/Banner/Text2");
                t_Goto.text = I2.Loc.ScriptLocalization.Get("Obj/Banner/Btn/Text");
                break;
            case 2:
                t_Goto.text = I2.Loc.ScriptLocalization.Get("Obj/Banner/Btn/Text");
                break;
            case 3:
                t_Desc.text = I2.Loc.ScriptLocalization.Get("Obj/Banner/Text3");
                t_Goto.text = I2.Loc.ScriptLocalization.Get("Obj/Banner/Btn/Text");
                break;
            case 4:
                break;
            case 5:
                t_Goto.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/QuitGameDescribe1");
                break;
            case 6:
                t_Goto.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/QuitGameDescribe1");
                break;
            case 7:
                t_Desc.text = I2.Loc.ScriptLocalization.Get("Obj/Banner/Text6");
                t_Goto.text = I2.Loc.ScriptLocalization.Get("Obj/Banner/Btn/Text");
                break;
            case 8:
                t_Desc.text = I2.Loc.ScriptLocalization.Get("Obj/Banner/Text4");
                t_Goto.text = I2.Loc.ScriptLocalization.Get("Obj/Banner/Btn/Text");
                break;
            case 9:
                t_Desc.text = I2.Loc.ScriptLocalization.Get("Obj/Banner/Text5");
                t_Goto.text = I2.Loc.ScriptLocalization.Get("Obj/Banner/Btn/Text");
                break;
            case 10:
                if (ShopSystem.Instance.CoinsRandomBagList.Count > 0 && ShopSystem.Instance.CoinsRandomBagList[0] != null) 
                {
                    ShowHotSaleInfo(ShopSystem.Instance.CoinsRandomBagList[0]);                   
                }
                break;
            case 11:
                if (ShopSystem.Instance.CoinsRandomBagList.Count > 1 && ShopSystem.Instance.CoinsRandomBagList[1] != null)
                {
                    ShowHotSaleInfo(ShopSystem.Instance.CoinsRandomBagList[1]);
                }
                break;
            case 12:
                if (ShopSystem.Instance.CoinsRandomBagList.Count > 2 && ShopSystem.Instance.CoinsRandomBagList[2] != null)
                {
                    ShowHotSaleInfo(ShopSystem.Instance.CoinsRandomBagList[1]);
                }
                break;
            default:
                break;
        }
    }

    private void ShowHotSaleInfo(PayPackDefinition payPackDefinition) 
    {
        for (int i = 0; i < payPackDefinition.RewardItems.Count; i++)
        {
            ItemParents[i].DestoryAllChildren();
            AssetSystem.Instance.Instantiate(payPackDefinition.RewardItems[i].ShowRewardPrefabName, ItemParents[i], Vector3.zero, Vector3.zero, Vector3.one * 0.6f);
            t_ItemNum[i].text = "x" + payPackDefinition.RewardItems[i].num.ToString();
        }
        t_OriginalPrice.text = payPackDefinition.prePrice;
        t_DiscountPrice.text = payPackDefinition.Cost.ToString();
        t_DiscountOff.text = GameManager.Instance.GetDiscountStrWhith_N((float)payPackDefinition.Cost / float.Parse(payPackDefinition.prePrice));
    }
}
