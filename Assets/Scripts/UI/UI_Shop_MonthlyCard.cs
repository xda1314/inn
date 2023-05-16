using Ivy.Purchase;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop_MonthlyCard : MonoBehaviour
{
    [SerializeField] private RectTransform titleHeight;
    [SerializeField] private RectTransform bgHeight;
    [SerializeField] private TextMeshProUGUI text_title;
    [SerializeField] private TextMeshProUGUI text_des1;
    [SerializeField] private TextMeshProUGUI text_des2;
    [SerializeField] private TextMeshProUGUI text_coin;
    [SerializeField] private TextMeshProUGUI text_gem;
    [SerializeField] private TextMeshProUGUI text_enegy;
    [SerializeField] private TextMeshProUGUI text_cost;
    [SerializeField] private TextMeshProUGUI text_receive;
    [SerializeField] private TextMeshProUGUI text_hasReceive;
    [SerializeField] private TextMeshProUGUI text_notNet;
    [SerializeField] private Button btn_buyMonthCard;
    [SerializeField] private Button btn_receiveReward;
    [SerializeField] private Button btn_hasReceiveToday;
    [SerializeField] private Button btn_notNet;
    [SerializeField] public GameObject redPoint;

    public float height;
    public Action refreshText;
    private PayPackDefinition payPackDefinition;

    private bool isInit = false;
    public void InitUIInfo()
    {
        RefreshText();
        if (isInit)
        {
            return;
        }
        isInit = true;

        refreshText = new Action(RefreshText);

        InitUI();

        UI_ShopContainer.shopGetBillingPriceInfoCB += OnPayInfo;
    }


    private void OnPayInfo(BillingPriceInfo info)
    {
        if (this == null || text_cost == null || payPackDefinition == null || payPackDefinition.UnityID != info.UnityID)
        {
            return;
        }

        text_cost.text = info.Price;
    }


    private void Awake()
    {
        height = titleHeight.sizeDelta.y + bgHeight.sizeDelta.y;
        btn_buyMonthCard.onClick.AddListener(BuyMonthlyCardBtnClick);
        btn_receiveReward.onClick.AddListener(ReceiveRewardBtnClick);
    }

    public void RefreshText()
    {
        text_title.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/MonthTitle");
        text_des2.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/MonthDescribe1");
        text_des1.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/MonthDescribe2");
        text_receive.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/PointsRewardButton1");
        //text_hasReceive.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/MonthButton1");
        text_notNet.text = I2.Loc.ScriptLocalization.Get("Obj/Name/MonthNoWIFIButton");
        text_hasReceive.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo((int)TimeManager.Instance.GetTomorrowRefreshTimeSpan().TotalSeconds);
    }
    private void InitUI()
    {
        if (GiftPackageDefinition.GiftDefinitionsDict.TryGetValue(1006, out GiftPackageDefinition m_GiftDefinition))
        {
            text_gem.text = "x" + m_GiftDefinition.CoinOrGemRewardList[0].num.ToString();
            text_coin.text = "x" + m_GiftDefinition.ItemRewardList[0].num.ToString();
            text_enegy.text = "x" + m_GiftDefinition.ItemRewardList[1].num.ToString();
            text_cost.text = "$" + m_GiftDefinition.Cost.ToString();
            btn_notNet.gameObject.SetActive(true);
        }
        List<MergeRewardItem> rewardItems = new List<MergeRewardItem>();
        List<MergeRewardItem> coinOrGemList = m_GiftDefinition.CoinOrGemRewardList;
        for (int i = 0; i < coinOrGemList.Count; i++)
        {
            rewardItems.Add(coinOrGemList[i]);
        }
        foreach (var item in m_GiftDefinition.ItemRewardList)
        {
            rewardItems.Add(item);
        }
        payPackDefinition = new PayPackDefinition(m_GiftDefinition.SaveID, m_GiftDefinition.UnityID, rewardItems, m_GiftDefinition.Cost);
        RefreshBtnActive();
    }

    public void RefreshBtnActive()
    {
        btn_notNet.gameObject.SetActive(false);
        redPoint.gameObject.SetActive(false);

        btn_buyMonthCard.gameObject.SetActive(false);
        btn_receiveReward.gameObject.SetActive(false);
        btn_hasReceiveToday.gameObject.SetActive(true);

        TimeManager.Instance.TryExcuteWithServerUtc(() =>
        {
            btn_notNet.gameObject.SetActive(false);
            redPoint.gameObject.SetActive(false);
            if (ExtensionTool.IsTheSameWeek(PlayerData.BuyMonthlyCardTime, TimeManager.ServerUtcNow()))
            {
                //已购买月卡
                btn_buyMonthCard.gameObject.SetActive(false);
                if (ExtensionTool.IsDateToday(PlayerData.ReceiveRewardsTime, TimeManager.ServerUtcNow()))
                {
                    //今天已经领取过奖励
                    btn_receiveReward.gameObject.SetActive(false);
                    btn_hasReceiveToday.gameObject.SetActive(true);
                }
                else
                {
                    btn_receiveReward.gameObject.SetActive(true);
                    btn_hasReceiveToday.gameObject.SetActive(false);
                    redPoint.gameObject.SetActive(true);
                }
            }
            else
            {
                btn_buyMonthCard.gameObject.SetActive(true);
                btn_receiveReward.gameObject.SetActive(false);
                btn_hasReceiveToday.gameObject.SetActive(false);
            }
            ////主界面红点
            //if (GameManager.Instance != null && GameManager.Instance.uiPanel_Main != null)
            //{
            //    //GameManager.Instance.uiPanel_Main.monthlyCardRedPointActive = redPoint.activeSelf;
            //    //GameManager.Instance.uiPanel_Main.RefreshTip();
            //}
        });
    }

    public void BuyMonthlyCardBtnClick()
    {
        if (!TimeManager.IsGetServerUtcSuccess)
        {
            return;
        }

        //AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
        if (ExtensionTool.IsTheSameWeek(PlayerData.BuyMonthlyCardTime, TimeManager.ServerUtcNow()))
        {
            return;
        }

        //支付      
        Billing.Instance.TryMakePurchase(payPackDefinition, btn_buyMonthCard.transform.position, _ => { RefreshBtnActive(); });
        //UIManager.Instance.CloseAllUI();
    }
    public void ReceiveRewardBtnClick()
    {
        if (!TimeManager.IsGetServerUtcSuccess)
        {
            return;
        }

        //重置领取月卡奖励的时间
        PlayerData.ReceiveRewardsTime = TimeManager.ServerUtcNow();
        GameManager.Instance.SavePlayerData_ReceiveMonthlyCardRewardTime();


        GameManager.Instance.GiveRewardItem(payPackDefinition.RewardItems, "MonthCardReward", Vector3.zero);

        btn_hasReceiveToday.gameObject.SetActive(true);
        btn_buyMonthCard.gameObject.SetActive(false);
        btn_receiveReward.gameObject.SetActive(false);
        redPoint.gameObject.SetActive(false);
        RefreshBtnActive();
        //if (GameManager.Instance.uiPanel_Main != null)
        //{
        //    //GameManager.Instance.uiPanel_Main.monthlyCardRedPointActive = false;
        //    //GameManager.Instance.uiPanel_Main.RefreshTip();
        //}
        //AnalyticsSystem.TrackEvent_ClaimMonthlyCardToday();
    }
    private void Update()
    {
        text_hasReceive.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo((int)TimeManager.Instance.GetTomorrowRefreshTimeSpan().TotalSeconds);
    }
}
