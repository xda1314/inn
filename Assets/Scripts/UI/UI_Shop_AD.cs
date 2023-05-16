using ivy.game;
using Ivy.Purchase;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop_AD : MonoBehaviour
{
    #region 组件
    [Header("未购买")]
    [SerializeField] private TextMeshProUGUI text_noBuyTitle;
    [SerializeField] private TextMeshProUGUI text_noBuyDes;
    [SerializeField] private Button btn_buyAD;
    [SerializeField] private TextMeshProUGUI text_price;
    [SerializeField] private GameObject go_noBuy;

    [Header("已购买")]
    [SerializeField] private GameObject go_Buy;
    [SerializeField] private TextMeshProUGUI text_BuyTitle;
    [SerializeField] private TextMeshProUGUI text_BuyDes;
    [SerializeField] private TextMeshProUGUI text_active;

    [Header("复购")]
    [SerializeField] private GameObject go_repurchase;
    [SerializeField] private TextMeshProUGUI text_tag;
    [SerializeField] private TextMeshProUGUI text_num1;
    [SerializeField] private TextMeshProUGUI text_RepurchaseDes;
    [SerializeField] private Button btn_repurchaseBuy;
    [SerializeField] private TextMeshProUGUI text_repurchasePrice;
    [SerializeField] private TextMeshProUGUI text_limitTime;
    #endregion

    #region 变量
    //未购买
    private PayPackDefinition packDef;
    //购买
    private PayPackDefinition packDef1;
    #endregion
    private void Start()
    {
        btn_buyAD.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            Billing.Instance.TryMakePurchase(packDef, Vector3.zero, _ =>
            {
                RefreshUIData();
            });
        });
        btn_repurchaseBuy.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            Billing.Instance.TryMakePurchase(packDef1, Vector3.zero, _ =>
            {
                RefreshUIData();
            });
        });
    }

    private void OnEnable()
    {
        RefreshUIData();
    }

    /// <summary>
    /// 未购买
    /// </summary>
    private void RefreshNoBuy() 
    {
        packDef = new PayPackDefinition("skipAd", "46", null, 14.99f);
        if (text_noBuyTitle != null)
            text_noBuyTitle.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/SkipAD");
        if (text_noBuyDes != null )
            text_noBuyDes.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/SkipADDesc");
        go_noBuy.SetActive(true);
        go_Buy.SetActive(false);
        go_repurchase.SetActive(false);
        text_price.text = "???";
        Billing.SearchPriceInfoAsync_One(packDef, info =>
        {
            if (this == null || text_price == null || packDef.UnityID != info.UnityID)
                return;
            text_price.text = info.Price;
        });
    }

    /// <summary>
    /// 已购买
    /// </summary>
    private void RefreshBuy()
    {
        if (text_noBuyTitle != null)
            text_BuyTitle.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/SkipAD");
        if (text_BuyDes != null)
            text_BuyDes.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/SkipADDesc");
        text_active.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/PaidButton");
        go_noBuy.SetActive(false);
        go_Buy.SetActive(true);
        go_repurchase.SetActive(false);
        int countDownTime = (int)(PlayerData.BuyADTime.AddDays(30) - TimeManager.Instance.UtcNow()).TotalSeconds;
        text_active.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(countDownTime);
    }

    private float timer = 0.0f;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < 1.0f) 
        {
            timer -= 1.0f;
            int countDownTime = (int)(PlayerData.BuyADTime.AddDays(30) - TimeManager.Instance.UtcNow()).TotalSeconds;
            text_active.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(countDownTime);
            int limitTime = (int)(PlayerData.BuyADTime.AddDays(33) - TimeManager.Instance.UtcNow()).TotalSeconds;
            text_limitTime.text = /*I2.Loc.ScriptLocalization.Get("Obj/Chain/BatterpassText2") + "\n" +*/ MyTimer.ReturnTextUntilSecond_MaxShowTwo(limitTime);
        }
    }

    /// <summary>
    /// 复购
    /// </summary>
    private void RefreshRepurchase()
    {
        List<MergeRewardItem> mergeRewards = new List<MergeRewardItem>();
        MergeRewardItem mergeReward = new MergeRewardItem
        {
            name = "gems",
            num = 100
        };
        mergeRewards.Add(mergeReward);
        packDef1 = new PayPackDefinition("skipAd", "46", mergeRewards, 14.99f);
        go_noBuy.SetActive(false);
        go_Buy.SetActive(false);
        go_repurchase.SetActive(true);
        text_tag.text= I2.Loc.ScriptLocalization.Get("Obj/GiftBag/push/Title1");
        packDef = new PayPackDefinition("skipAd", "46", null, 14.99f);
        text_repurchasePrice.text = "???";
        Billing.SearchPriceInfoAsync_One(packDef1, info =>
        {
            if (this == null || text_repurchasePrice == null || packDef.UnityID != info.UnityID)
                return;
            text_repurchasePrice.text = info.Price;
        });
        text_num1.text= "+100";
        text_RepurchaseDes.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/SkipADDesc");
        int limitTime = (int)(PlayerData.BuyADTime.AddDays(33) - TimeManager.Instance.UtcNow()).TotalSeconds;
        text_limitTime.text = /*I2.Loc.ScriptLocalization.Get("Obj/Chain/BatterpassText2") + "\n" +*/ MyTimer.ReturnTextUntilSecond_MaxShowTwo(limitTime);
    }

    /// <summary>
    /// 刷新UI
    /// </summary>
    public void RefreshUIData()
    {
        if(GameManager.Instance != null && GameManager.Instance.playerData != null) 
        {
            GameManager.Instance.playerData.TryRefreshBuyADTimeData();
        }

        if (PlayerData.BuyADTime != DateTimeOffset.MinValue &&
        PlayerData.BuyADTime.AddDays(30).ToUnixTimeMilliseconds() > TimeManager.Instance.UtcNow().ToUnixTimeMilliseconds())
            RefreshBuy();

        else if (PlayerData.BuyADTime != DateTimeOffset.MinValue
            && PlayerData.BuyADTime.AddDays(33).ToUnixTimeMilliseconds() > TimeManager.Instance.UtcNow().ToUnixTimeMilliseconds())
            RefreshRepurchase();

        else
            RefreshNoBuy();
    }


}
