using ivy.game;
using Ivy.Purchase;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Energy_AD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text_active;
    [SerializeField] private Button btn_buyAD;
    [SerializeField] private TextMeshProUGUI text_price;
    [SerializeField] private GameObject go_noBuy;
    [SerializeField] private GameObject go_Buy;
    private PayPackDefinition packDef;
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
    }

    private void OnEnable()
    {
        RefreshUIData();
    }

    public void RefreshUIData()
    {
        GameManager.Instance.playerData.TryRefreshBuyADTimeData();
        if (GameManager.Instance != null && GameManager.Instance.playerData != null)
        {
            GameManager.Instance.playerData.TryRefreshBuyADTimeData();
        }
        packDef = new PayPackDefinition("skipAd", "46", null, 14.99f);
        text_active.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/PaidButton");

        if (PlayerData.BuyADTime != DateTimeOffset.MinValue &&
        PlayerData.BuyADTime.AddDays(30).ToUnixTimeMilliseconds() > TimeManager.Instance.UtcNow().ToUnixTimeMilliseconds())
        {
            btn_buyAD.gameObject.SetActive(false);
            go_noBuy.SetActive(false);
            go_Buy.SetActive(true);
            int countDownTime = (int)(PlayerData.BuyADTime.AddDays(30) - TimeManager.Instance.UtcNow()).TotalSeconds;
            text_active.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(countDownTime);
            return;
        }
        else
        {
            go_noBuy.SetActive(true);
            go_Buy.SetActive(false);
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