using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class UI_Currency_Item : MonoBehaviour
{
    public CurrencyID currentCurrencyID;

    public TextMeshProUGUI lbl_Count;
    public TextMeshProUGUI lbl_EnergyRefreshCountDown;
    public GameObject imgCountDown;
    [SerializeField] private GameObject go_wife;

    public void SetCurrencyInfo(int value, bool needTween = false)
    {
        if (currentCurrencyID == CurrencyID.Energy)
        {
            go_wife.SetActive(!TimeManager.IsGetServerUtcSuccess);
        }

        if (value < 0) 
        {
            GameDebug.Log("体力异常");
            value = 1;
        }
        if (needTween)
        {
            lbl_Count.transform.DOScale(1.2f, 0.2f).SetEase(Ease.InQuad).onComplete = () =>
            {
                lbl_Count.text = value.ToString();
                lbl_Count.transform.DOScale(1f, 0.2f).SetEase(Ease.OutQuad).SetDelay(0.1f);
            };
        }
        else 
        {
            lbl_Count.text = value.ToString();
        }
    }

    private void OnEnable()
    {
        
        if (currentCurrencyID == CurrencyID.Energy)
        {
            go_wife.SetActive(!TimeManager.IsGetServerUtcSuccess);
            if (!TimeManager.IsGetServerUtcSuccess) 
            {
                TimeManager.Instance.TryExcuteWithServerUtc(() =>
                {
                    if(this!=null && go_wife!=null)
                        go_wife.SetActive(true);
                });
            }
            GameManager.UpdateEnergyCountDownEvent += UpdateCountDown;
        }
    }

    private void OnDisable()
    {
        if (currentCurrencyID == CurrencyID.Energy)
        {
            GameManager.UpdateEnergyCountDownEvent -= UpdateCountDown;
        }
    }

    private TimeSpan timeSpan;
    private void UpdateCountDown()
    {
        if (!Currencies.hasInit)
        {
            return;
        }

        if (currentCurrencyID != CurrencyID.Energy || lbl_EnergyRefreshCountDown == null)
        {
            return;
        }

        if (currentCurrencyID == CurrencyID.Energy && Currencies.StartCountDown && TimeManager.IsGetServerUtcSuccess) 
        {
            if (!imgCountDown.activeSelf)
                imgCountDown.SetActive(true);

            timeSpan = Currencies.NextAddEnergyDate();
            lbl_EnergyRefreshCountDown.text = $"{timeSpan.Minutes.ToString().PadLeft(2, '0')}:{timeSpan.Seconds.ToString().PadLeft(2, '0')}";
        }
        else if (imgCountDown.activeSelf)
            imgCountDown.SetActive(false);
        if (currentCurrencyID == CurrencyID.Energy) 
        {
            go_wife.SetActive(!TimeManager.IsGetServerUtcSuccess);
        }
        
    }
}
