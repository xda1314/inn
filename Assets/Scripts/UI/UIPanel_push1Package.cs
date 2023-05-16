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
public class UIPanel_push1Package : UIPanelBase
{
    #region 组件

    [SerializeField] private TextMeshProUGUI text_title;
    [SerializeField] private TextMeshProUGUI text_des;
    [SerializeField] private TextMeshProUGUI text_price;//折扣价
    [SerializeField] private TextMeshProUGUI text_countDown;
    [SerializeField] private TextMeshProUGUI text_discount;
    [SerializeField] private TextMeshProUGUI text_originalPrice;//原价
    [SerializeField] private GridLayoutGroup rewardGrid;
    [SerializeField] private Button btn_close;
    [SerializeField] private Button btn_Buy;
    [SerializeField] private List<GameObject> itemLists = new List<GameObject>();

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
    /// 刷新子物体UI
    /// </summary>
    /// <param name="exchangeData"></param>
    /// <param name="uIDungeon1FuncItem"></param>
    private void RefreshItem(MergeRewardItem rewardItem,int index)
    {
        Transform iconRoot = itemLists[index].transform.Find("IconRoot");
        TextMeshProUGUI text_Num= itemLists[index].transform.Find("text_Num").GetComponent<TextMeshProUGUI>();
        AssetSystem.Instance.InstantiateAsync(rewardItem.ShowRewardPrefabName, iconRoot, gO =>
        {
            RectTransform rect = gO.GetComponents<RectTransform>()[0];
            rect.sizeDelta = new Vector2(150, 150);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.localPosition = Vector2.zero;
        });
        text_Num.text = "X" + rewardItem.num.ToString();
    }

    /// <summary>
    /// 多语言
    /// </summary>
    private void RefreshLanguageText()
    {
        text_des.text = I2.Loc.ScriptLocalization.Get("Obj/GiftBag/Novice/Describe");
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
        text_originalPrice.text = "$" + m_GiftDefinition.OriginalPrice.ToString();
        text_price.text = "$" + m_GiftDefinition.Cost.ToString();
        text_discount.text = GameManager.Instance.GetDiscountStrWhith_N(m_GiftDefinition.Cost / m_GiftDefinition.OriginalPrice);
        List<MergeRewardItem> commomItemList = new List<MergeRewardItem>();
        List<MergeRewardItem> coinOrGemList = m_GiftDefinition.CoinOrGemRewardList;
        for (int i = 0; i < coinOrGemList.Count; i++)
            commomItemList.Add(coinOrGemList[i]);

        List<MergeRewardItem> itemList = m_GiftDefinition.ItemRewardList;
        for (int i = 0; i < itemList.Count; i++)
            commomItemList.Add(itemList[i]);

        for (int i = 0; i < itemLists.Count; i++)
            RefreshItem(commomItemList[i], i);

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
        this.timeOffset = RemoteGiftSystem.Instance.Remote_EndTime;
        //this.timeOffset = TimeManager.Instance.UtcNow().AddSeconds(172800);
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
