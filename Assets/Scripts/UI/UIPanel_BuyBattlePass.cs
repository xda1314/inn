using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ivy.Purchase;

public class BuyBP_Data : UIPanelDataBase
{
    public UIPanel_BattlePass bp;
    public BuyBP_Data(UIPanel_BattlePass bp, string uiPanelName) : base(uiPanelName)
    {
        this.bp = bp;
    }
}

public class UIPanel_BuyBattlePass : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI lbl_title;
    [SerializeField] private TextMeshProUGUI lbl_rewardDesc1;
    [SerializeField] private TextMeshProUGUI lbl_rewardDesc2;
    [SerializeField] private TextMeshProUGUI lbl_rewardDesc3;
    [SerializeField] private TextMeshProUGUI lbl_CD;
    [SerializeField] private TextMeshProUGUI lbl_Cost;

    [SerializeField] private Button btn_Buy;
    [SerializeField] private Button btn_Close;

    private PayPackDefinition pay;
    private float cost;

    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_Buy.onClick.AddListener(() =>
        {
            BuyBattlePass();
        });
        btn_Close.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
    }
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        btn_Buy.gameObject.SetActive(!BattlePassSystem.Instance.IsPay);
        if (PayPackDefinition.DefinitionMap.TryGetValue("batterpass", out pay))
        {
            cost = pay.Cost;
            lbl_Cost.text = cost.ToString();
            Billing.SearchPriceInfoAsync_One(pay, OnPayInfo);
        }

        lbl_title.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/BatterpassTitle");
        lbl_rewardDesc1.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/BatterpassPayText1");
        lbl_rewardDesc2.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/BatterpassPayText2");
        lbl_rewardDesc3.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/BatterpassPayText3");

        countDownTime = (int)(BattlePassSystem.Instance.TryGetCurrentMonthFinishDate() - TimeManager.ServerUtcNow()).TotalSeconds;
        lbl_CD.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(countDownTime);
    }
    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
    }



    private void OnPayInfo(BillingPriceInfo info)
    {
        if (this == null || lbl_Cost == null || pay == null || pay.UnityID != info.UnityID)
        {
            return;
        }

        lbl_Cost.text = info.Price;
    }
    /// <summary>
    /// 购买battlePass
    /// </summary>
    public void BuyBattlePass()
    {
        if (BattlePassSystem.Instance.IsPay)
        {
            return;
        }

        if (PayPackDefinition.DefinitionMap.TryGetValue("batterpass", out var def))
        {
            Billing.Instance.TryMakePurchase(def, Vector3.zero, _ =>
            {
                BattlePassSystem.Instance.SuccessBuyBattlePassCB();
                UISystem.Instance.HideUI(this);
            });
        }
    }

    int countDownTime = -1;
    float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer--;
            countDownTime--;
            lbl_CD.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(countDownTime);
        }
    }
}
