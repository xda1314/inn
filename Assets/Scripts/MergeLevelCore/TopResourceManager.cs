using Ivy;
using IvyCore;
using System.Collections;
using UnityEngine;

public class TopResourceManager : UIPanelBase
{
    public GameObject gO_Exp;
    public GameObject gO_Energy;
    public GameObject gO_Coin;
    public GameObject gO_Gem;

    private UI_Main_Experience ui_Exp;
    private UI_Currency_Item ui_energy;
    private UI_Currency_Item ui_coin;
    private UI_Currency_Item ui_gem;

    private void Awake()
    {
        ui_Exp = gO_Exp.GetComponent<UI_Main_Experience>();
        ui_energy = gO_Energy.GetComponent<UI_Currency_Item>();
        ui_coin = gO_Coin.GetComponent<UI_Currency_Item>();
        ui_gem = gO_Gem.GetComponent<UI_Currency_Item>();

        UI_Manager.Instance.RegisterRefreshEvent(this.gameObject, (str) =>
        {
            SetExperienceValue();
        }, "RefreshCurrency_Exp");
        UI_Manager.Instance.RegisterRefreshEvent(this.gameObject, (str) =>
        {
            SetCoinValue(Currencies.Coins);
        }, "RefreshCurrency_Coins");
        UI_Manager.Instance.RegisterRefreshEvent(this.gameObject, (str) =>
        {
            SetGemValue(Currencies.Gems);
        }, "RefreshCurrency_Gems");
        UI_Manager.Instance.RegisterRefreshEvent(this.gameObject, (str) =>
        {
            SetEnergyValue(Currencies.Energy, str == "needTween");
        }, "RefreshCurrency_Energy");
        UI_Manager.Instance.RegisterRefreshEvent(this.gameObject, (str) =>
        {
            SetNeedleValue(Currencies.Needle);
        }, "RefreshCurrency_Needle");
        StartCoroutine(RefrshTopUI());
    }

    private IEnumerator RefrshTopUI()
    {
        yield return new WaitForSeconds(0.5F);
        RectTransform rect = transform.GetComponent<RectTransform>();
        if (rect != null)
        {
            float f = rect.rect.width - 300;
            ui_Exp.transform.localPosition = new Vector2(-f / 2-75, 5);
            ui_energy.transform.localPosition = new Vector2(-f / 6 - 75, 5);
            ui_coin.transform.localPosition = new Vector2(f / 6 - 75, 5);
            ui_gem.transform.localPosition = new Vector2(f / 2 - 75, 5);
        }
    }
    public void SetExperienceValue()
    {
        if (ui_Exp == null)
        {
            ui_Exp = gO_Exp.GetComponent<UI_Main_Experience>();
        }
        ui_Exp.SetSlider();
    }
    public void SetEnergyValue(int value, bool needTween = false)
    {
        if (ui_energy == null)
        {
            ui_energy = gO_Energy.GetComponent<UI_Currency_Item>();
        }
        ui_energy.SetCurrencyInfo(value, needTween);
    }

    public void SetCoinValue(int value)
    {
        if (ui_coin == null)
        {
            ui_coin = gO_Coin.GetComponent<UI_Currency_Item>();
        }
        ui_coin.SetCurrencyInfo(value);
        RiseSdk.Instance.SetUserProperty("coins", value.ToString());
    }
    public void SetGemValue(int value)
    {
        if (ui_gem == null)
        {
            ui_gem = gO_Gem.GetComponent<UI_Currency_Item>();
        }
        ui_gem.SetCurrencyInfo(value);
        RiseSdk.Instance.SetUserProperty("gems", value.ToString());
    }
    public void SetNeedleValue(int value)
    {
        if (MergeController.CurrentController != null)
        {
            MergeController.CurrentController.SetNeedleNum();
        }
    }
}
