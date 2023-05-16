using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel_BuyBattlePassSpeedBag : UIPanelBase
{
    [SerializeField] private Text t_Desc;
    [SerializeField] private Button btn_Buy;
    [SerializeField] private Text t_BuyBtn;
    [SerializeField] private Button btn_Cancel;
    [SerializeField] private Text t_CancelBtn;

    private UIPanel_BattlePass uipanel;
    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_Buy.onClick.AddListener(() =>
        {
            BattlePassSystem.Instance.BuySpeedUpPackage();
            uipanel.RefreshView();
            UISystem.Instance.HideUI(this);
        });
        btn_Cancel.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
    }
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        uipanel = ((BuyBP_Data)UIPanelData).bp;

        t_Desc.text = "活动快到期了，你还有很多奖励没有领取,todo";
        t_BuyBtn.text = "购买";
        t_CancelBtn.text = "取消";
    }

}
