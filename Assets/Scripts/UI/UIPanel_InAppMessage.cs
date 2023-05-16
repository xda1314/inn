using Ivy.InAppMessage;
using Ivy.Purchase;
using Ivy.Timer;
using IvyCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel_InAppMessage : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI text_title;
    [SerializeField] private TextMeshProUGUI text_des;
    [SerializeField] private Button btn_close;
    [SerializeField] private GameObject countDownGO;
    [SerializeField] private TextMeshProUGUI text_countDown;
    [SerializeField] private GameObject gemNumGO;
    [SerializeField] private TextMeshProUGUI text_gemNum;
    [SerializeField] private TextMeshProUGUI text_discount;
    [SerializeField] private TextMeshProUGUI text_discount_suffix;
    [SerializeField] private GridLayoutGroup rewardGrid;
    [SerializeField] private Button btn_Buy;
    [SerializeField] private TextMeshProUGUI text_price;//折扣价
    [SerializeField] private GameObject costGemsGO;//折扣价
    [SerializeField] private TextMeshProUGUI tmp_costGems;//折扣价
    [SerializeField] private GameObject costCoinsGO;//折扣价
    [SerializeField] private TextMeshProUGUI tmp_costCoins;//折扣价
    [SerializeField] private GameObject adGO;//折扣价
    [SerializeField] private TextMeshProUGUI tmp_ad;//折扣价
    [SerializeField] private List<UI_InAppMessageReward> uiItems = new List<UI_InAppMessageReward>();

    private InAppMessagePackData packData;


    private PayPackDefinition m_PayDefinition;

    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_close.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
    }


    public override IEnumerator OnShowUI()
    {
        packData = InAppMessageSystem.Instance.CurrentPackData;
        if (packData == null)
            yield break;
        InAppMessageSystem.Instance.SetPackShow(packData.id);

        RefreshPackageUI();
        TimerSystem.Instance.OnUpdatePerSecond += RefreshCountDown;
        yield return base.OnShowUI();
    }

    public override IEnumerator OnHideUI()
    {
        TimerSystem.Instance.OnUpdatePerSecond -= RefreshCountDown;
        yield return base.OnHideUI();

        if (packData != null)
            InAppMessageSystem.Instance.SetPackHide(packData.id);
        m_PayDefinition = null;
    }

    private void OnPayInfo(BillingPriceInfo info)
    {
        if (this == null || text_price == null || m_PayDefinition == null || m_PayDefinition.UnityID != info.UnityID)
        {
            return;
        }
        text_price.text = info.Price;
    }

    private void RefreshPackageUI()
    {
        List<MergeRewardItem> mergeRewardItems = new List<MergeRewardItem>();
        if (packData.RewardsContainer != null && packData.RewardsContainer.Count > 0)
        {
            foreach (var item in packData.RewardsContainer.Rewards)
            {
                MergeRewardItem rewardItem = new MergeRewardItem();
                rewardItem.name = item.Name;
                rewardItem.num = item.Num;
                if (rewardItem.IsValidReward())
                    mergeRewardItems.Add(rewardItem);
            }
        }

        text_title.text = packData.titleStr;
        text_des.text = packData.bodyStr;

        //text_originalPrice.text = "$" + m_GiftDefinition.OriginalPrice.ToString();
        //text_price.text = "$" + m_GiftDefinition.Cost.ToString();
        //text_discount.text = GameManager.Instance.GetDiscountTxt(m_GiftDefinition.Cost / m_GiftDefinition.OriginalPrice);
        text_discount.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Packper/Title");
        //text_discount_suffix.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part2Text5");
        List<MergeRewardItem> commomItemList = new List<MergeRewardItem>();
        int gemsCount = 0;
        for (int i = 0; i < mergeRewardItems.Count; i++)
        {
            //钻石奖励
            if (mergeRewardItems[i].IsRewardGems)
            {
                gemsCount += mergeRewardItems[i].num;
            }
            else
            {
                commomItemList.Add(mergeRewardItems[i]);
            }
        }
        if (gemsCount > 0)
        {
            text_gemNum.text = "+" + gemsCount;
            gemNumGO.SetActive(true);
        }
        else
        {
            gemNumGO.SetActive(false);
        }
        for (int i = 0; i < 6; i++)
        {
            if (commomItemList.Count - 1 >= i)
            {
                uiItems[i].SetData(commomItemList[i]);
                uiItems[i].gameObject.SetActive(true);
            }
            else
            {
                uiItems[i].gameObject.SetActive(false);
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(rewardGrid.GetComponent<RectTransform>());

        switch (packData.packType)
        {
            case InAppMessagePackType.Gems:
                {
                    text_price.gameObject.SetActive(false);
                    costCoinsGO.gameObject.SetActive(false);
                    costGemsGO.gameObject.SetActive(true);
                    adGO.gameObject.SetActive(false);

                    tmp_costGems.text = packData.costNum.ToString();
                }
                break;
            case InAppMessagePackType.Coins:
                {
                    text_price.gameObject.SetActive(false);
                    costCoinsGO.gameObject.SetActive(true);
                    costGemsGO.gameObject.SetActive(false);
                    adGO.gameObject.SetActive(false);

                    tmp_costCoins.text = packData.costNum.ToString();
                }
                break;
            case InAppMessagePackType.Pay:
                {
                    text_price.gameObject.SetActive(true);
                    costCoinsGO.gameObject.SetActive(false);
                    costGemsGO.gameObject.SetActive(false);
                    adGO.gameObject.SetActive(false);

                    m_PayDefinition = new PayPackDefinition(InAppMessagePackData.SaveID_InAppMessagePack + packData.id, packData.payID, mergeRewardItems, 0);

                    text_price.text = "???";
                    Billing.SearchPriceInfoAsync_One(m_PayDefinition, OnPayInfo);
                }
                break;
            case InAppMessagePackType.AD:
                {
                    text_price.gameObject.SetActive(false);
                    costCoinsGO.gameObject.SetActive(false);
                    costGemsGO.gameObject.SetActive(false);
                    adGO.gameObject.SetActive(true);

                    tmp_ad.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/Engery/Text3");
                }
                break;
            case InAppMessagePackType.Free:
                {
                    text_price.gameObject.SetActive(true);
                    costCoinsGO.gameObject.SetActive(false);
                    costGemsGO.gameObject.SetActive(false);
                    adGO.gameObject.SetActive(false);

                    text_price.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/Engery/Text3");
                }
                break;
            default:
                {
                    text_price.gameObject.SetActive(true);
                    costCoinsGO.gameObject.SetActive(false);
                    costGemsGO.gameObject.SetActive(false);
                    adGO.gameObject.SetActive(false);
                    text_price.text = "";

                }
                break;
        }

        btn_Buy.onClick.RemoveAllListeners();
        btn_Buy.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            //支付
            if (packData != null)
            {
                switch (packData.packType)
                {
                    case InAppMessagePackType.Gems:
                        {
                            if (Currencies.CanAffordOrTip(CurrencyID.Gems, packData.costNum))
                            {
                                GameManager.Instance.GiveRewardItem(mergeRewardItems, "iam", rewardGrid.transform.position);
                                Currencies.Spend(CurrencyID.Gems, packData.costNum, "iam");
                                InAppMessageSystem.Instance.SetPackCollect(packData.id);
                                UISystem.Instance.HideUI(this);
                            }
                        }
                        break;
                    case InAppMessagePackType.Coins:
                        {
                            if (Currencies.CanAffordOrTip(CurrencyID.Coins, packData.costNum))
                            {
                                GameManager.Instance.GiveRewardItem(mergeRewardItems, "iam", rewardGrid.transform.position);
                                Currencies.Spend(CurrencyID.Coins, packData.costNum, "iam");
                                InAppMessageSystem.Instance.SetPackCollect(packData.id);
                                UISystem.Instance.HideUI(this);
                            }
                        }
                        break;
                    case InAppMessagePackType.Pay:
                        {
                            if (m_PayDefinition != null)
                            {
                                Billing.Instance.TryMakePurchase(m_PayDefinition, rewardGrid.transform.position, def =>
                                {
                                    InAppMessageSystem.Instance.SetPackCollect(packData.id, def.Cost);
                                    UISystem.Instance.HideUI(this);
                                });
                            }
                        }
                        break;
                    case InAppMessagePackType.AD:
                        {
                            AdManager.PlayAd(btn_Buy.transform.position, AdManager.ADTag.InAppMessage, () =>
                            {
                                GameManager.Instance.GiveRewardItem(mergeRewardItems, "iam", rewardGrid.transform.position);
                                InAppMessageSystem.Instance.SetPackCollect(packData.id);
                                UISystem.Instance.HideUI(this);
                            }, "", () =>
                            {
                                GameManager.Instance.PlayAdFail(mergeRewardItems, AdManager.ADTag.InAppMessage);
                            });
                        }
                        break;
                    case InAppMessagePackType.Free:
                        {
                            GameManager.Instance.GiveRewardItem(mergeRewardItems, "iam", rewardGrid.transform.position);
                            InAppMessageSystem.Instance.SetPackCollect(packData.id);
                            UISystem.Instance.HideUI(this);
                        }
                        break;
                    default:
                        InAppMessageSystem.Instance.CleanCurrentPack();
                        UISystem.Instance.HideUI(this);
                        break;
                }
            }
        });

        RefreshCountDown();
    }

    private void RefreshCountDown()
    {
        if (packData == null)
            return;

        if (packData.lifeTime > 0)
        {
            countDownGO.SetActive(true);
            TimeSpan timeSpan = packData.ShowEndDate - DateTimeOffset.UtcNow;
            if (timeSpan.TotalSeconds >= 0)
            {
                text_countDown.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo((int)timeSpan.TotalSeconds);
            }
            else
            {
                UISystem.Instance.HideUI(this);
            }
        }
        else
        {
            countDownGO.SetActive(false);
        }
    }

}
