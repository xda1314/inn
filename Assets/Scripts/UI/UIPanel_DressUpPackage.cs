using Ivy.Purchase;
using IvyCore;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 新手礼包
/// </summary>
public class UIPanel_DressUpPackage : UIPanelBase
{
    #region 组件

    [SerializeField] private UINovicePackageItem uINovicePackageItem;
    [SerializeField] private TextMeshProUGUI text_title;
    [SerializeField] private TextMeshProUGUI text_des;
    [SerializeField] private TextMeshProUGUI text_price;//折扣价
    [SerializeField] private TextMeshProUGUI text_countDown;
    [SerializeField] private TextMeshProUGUI text_gemNum;
    //[SerializeField] private TextMeshProUGUI text_buy;
    [SerializeField] private TextMeshProUGUI text_discount;
    [SerializeField] private TextMeshProUGUI text_discount_suffix;
    //[SerializeField] private TextMeshProUGUI text_originalPrice;//原价
    [SerializeField] private GridLayoutGroup rewardGrid;
    [SerializeField] private Button btn_close;
    [SerializeField] private Button btn_Buy;
    [SerializeField] private List<UINovicePackageItem> uINovicePackageItems = new List<UINovicePackageItem>();
    //public TopResourceManager topResourceManager;

    #endregion 组件

    #region 私有变量

    private Action<PayPackDefinition> timeOutOrBuyCB;
    private DateTimeOffset timeOffset;
    private int m_LevelIndex = -1;
    private GiftPackageDefinition m_GiftDefinition;
    private PayPackDefinition m_PayDefinition;

    #endregion 私有变量

    /// <summary>
    /// 初始化
    /// </summary>
    public override void OnInitUI()
    {
        base.OnInitUI();
        uINovicePackageItem.gameObject.SetActive(false);
        AddBtnEvent();
        timeOutOrBuyCB = _ =>
        {
            //移除当前数据数据 推送下一礼包
            GiftPackageManager.Instance.PushNextGiftPackage();
            //检测新礼包
            IvyCore.UI_Manager.Instance.InvokeRefreshEvent("", "RefreshEvent_ResetCheckActivity");
            Invoke("HideUI", 1.5f);
        };
    }

    /// <summary>
    /// 添加按钮事件
    /// </summary>
    private void AddBtnEvent()
    {
        btn_close.onClick.AddListener(() =>
        {
            HideUI();
        });
        btn_Buy.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            //支付
            Billing.Instance.TryMakePurchase(m_PayDefinition, rewardGrid.transform.position, timeOutOrBuyCB);
        });
    }

    private void HideUI()
    {
        UISystem.Instance.HideUI(this, () =>
        {
            UI_Manager.Instance.InvokeRefreshEvent("", "UnFoldControl");
        });
    }

    /// <summary>
    /// 打开UI
    /// </summary>
    /// <returns></returns>
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();

        RefreshData();
        RefreshPackageUI();
        SetTimeOffset();
        Billing.SearchPriceInfoAsync_One(m_PayDefinition, OnPayInfo);
    }

    /// <summary>
    /// 关闭UI
    /// </summary>
    /// <returns></returns>
    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
    }

    private void OnPayInfo(BillingPriceInfo info)
    {
        if (this == null || text_price == null || m_PayDefinition == null || m_PayDefinition.UnityID != info.UnityID)
        {
            return;
        }

        text_price.text = info.Price;
    }

    private void RefreshData()
    {
        m_LevelIndex = GiftPackageManager.Instance.GiftIndexList[0];//多个礼包时，推送第一个礼包
        GiftPackageDefinition.GiftDefinitionsDict.TryGetValue(m_LevelIndex, out m_GiftDefinition);
    }

    /// <summary>
    /// 归一化对象
    /// </summary>
    /// <param name="uIDungeon1FuncItem"></param>
    private void Normalization(UINovicePackageItem uINovicePackageItem)
    {
        uINovicePackageItem.transform.SetParent(rewardGrid.transform, false);
        uINovicePackageItem.gameObject.SetActive(true);
    }

    /// <summary>
    /// 刷新子物体UI
    /// </summary>
    /// <param name="exchangeData"></param>
    /// <param name="uIDungeon1FuncItem"></param>
    private void RefreshItem(MergeRewardItem rewardItem, UINovicePackageItem uINovicePackageItem)
    {
        Normalization(uINovicePackageItem);
        uINovicePackageItem.SetData(rewardItem);
    }

    /// <summary>
    /// 多语言
    /// </summary>
    private void RefreshLanguageText()
    {
        text_des.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/kuairenBagDescribe1");
        text_title.text = I2.Loc.ScriptLocalization.Get(m_GiftDefinition.GiftName);
    }

    /// <summary>
    /// 初始化礼包内容
    /// </summary>
    private void RefreshPackageUI()
    {
        if (m_GiftDefinition == null)
        {
            return;
        }
        RefreshLanguageText();
        //text_originalPrice.text = "$" + m_GiftDefinition.OriginalPrice.ToString();
        text_price.text = "$" + m_GiftDefinition.Cost.ToString();
        text_discount.text = GameManager.Instance.GetDiscountTxt(m_GiftDefinition.Cost / m_GiftDefinition.OriginalPrice);
        text_discount_suffix.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part2Text5");
        List<MergeRewardItem> commomItemList = new List<MergeRewardItem>();
        List<MergeRewardItem> coinOrGemList = m_GiftDefinition.CoinOrGemRewardList;
        for (int i = 0; i < coinOrGemList.Count; i++)
        {
            //钻石奖励
            if (coinOrGemList[i].IsRewardGems)
            {
                text_gemNum.text = "+" + coinOrGemList[i].num.ToString();
            }
            commomItemList.Add(coinOrGemList[i]);
        }
        List<MergeRewardItem> itemList = m_GiftDefinition.ItemRewardList;
        for (int i = 0; i < itemList.Count; i++)
        {
            //实例化对象
            if (uINovicePackageItems.Count <= i)
                uINovicePackageItems.Add(Instantiate(uINovicePackageItem));
            //刷新数据
            RefreshItem(itemList[i], uINovicePackageItems[i]);

            commomItemList.Add(itemList[i]);
        }
        //强刷布局
        LayoutRebuilder.ForceRebuildLayoutImmediate(rewardGrid.GetComponent<RectTransform>());
        //new支付接口
        m_PayDefinition = new PayPackDefinition(m_GiftDefinition.SaveID, m_GiftDefinition.UnityID, commomItemList, m_GiftDefinition.Cost);
        if (m_GiftDefinition != null)
        {
            m_PayDefinition.GooglePlayProductID = m_GiftDefinition.GooglePlayID;
            m_PayDefinition.AppleProductID = m_GiftDefinition.AppleID;
        }
    }

    /// <summary>
    /// 初始化推送礼包剩余倒计时时间
    /// </summary>
    private void SetTimeOffset()
    {
        this.timeOffset = GiftPackageManager.Instance.PushGiftEndTime;
        TimeSpan timeSpan = timeOffset - DateTimeOffset.UtcNow;
        text_countDown.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo((int)timeSpan.TotalSeconds);
    }

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < 1)
        {
            return;
        }
        timer -= 1;
        if (text_countDown != null)
        {
            TimeSpan timeSpan = timeOffset - DateTimeOffset.UtcNow;
            if (timeSpan.TotalSeconds < 0)
            {
                timeOutOrBuyCB?.Invoke(null);
            }
            text_countDown.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo((int)timeSpan.TotalSeconds);
        }
    }
}
