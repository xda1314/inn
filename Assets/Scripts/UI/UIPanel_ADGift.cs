using Ivy;
using Ivy.Firebase;
using IvyCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel_ADGift : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI text_des;
    [SerializeField] private TextMeshProUGUI text_free;
    [SerializeField] private Button btn_buyAD;
    [SerializeField] private Button btn_close;
    [SerializeField] private TextMeshProUGUI text_price;
    private PayPackDefinition packDef;
    public override void OnInitUI()
    {
        btn_buyAD.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
            Billing.Instance.TryMakePurchase(packDef, Vector3.zero, _ =>
            {
                UISystem.Instance.HideUI(this);
            });
        });

        btn_close.onClick.AddListener(() => 
        {
            UISystem.Instance.HideUI(this);
        });
    }

    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        RefreshUIData();
    }
    public void RefreshUIData()
    {
        GameManager.Instance.playerData.TryRefreshBuyADTimeData();
        packDef = new PayPackDefinition("skipAd", "46", null, 14.99f);
        text_des.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/SkipAD");
        text_free.text= I2.Loc.ScriptLocalization.Get("Obj/Chain/SpinWheel_Describe3");
        if (PlayerData.BuyADTime != DateTimeOffset.MinValue &&
        PlayerData.BuyADTime.AddDays(30).ToUnixTimeMilliseconds() > TimeManager.Instance.UtcNow().ToUnixTimeMilliseconds())
        {
            btn_buyAD.gameObject.SetActive(false);
            return;
        }
        btn_buyAD.gameObject.SetActive(true);
        text_price.text = "???";
        Billing.SearchPriceInfoAsync_One(packDef, info =>
        {
            if (this == null || text_price == null || packDef.UnityID != info.UnityID)
                return;
            text_price.text = info.Price;
        });
    }
}
