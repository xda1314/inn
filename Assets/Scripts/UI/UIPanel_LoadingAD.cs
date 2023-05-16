using Ivy;
using Ivy.Timer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIPanelData_LoadingAD : UIPanelDataBase
{
    public Action playADCB;
    public Action failCB;

    public UIPanelData_LoadingAD(UIShowLayer UIShowLayer, Action playADCB, Action failCB) : base(Consts.UIPanel_LoadingAD, UIShowLayer)
    {
        this.playADCB = playADCB;
        this.failCB = failCB;
    }
}

public class UIPanel_LoadingAD : UIPanelBase
{
    [SerializeField] Transform loadingIcon;
    [SerializeField] TextMeshProUGUI loadingText;

    Timer_DeadTime timer_DeadTime;
    UIPanelData_LoadingAD panelData;

    public override IEnumerator OnShowUI()
    {
        panelData = UIPanelData as UIPanelData_LoadingAD;
        loadingText.text = I2.Loc.ScriptLocalization.Get("Obj/Name/LoadingText0");
        loadingIcon.DOKill();
        loadingIcon.DOLocalRotate(Vector3.forward * -1000, 10, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
        yield return base.OnShowUI();

        RiseSdkListener.OnAdLoaded += OnADLoaded;
        timer_DeadTime = TimerSystem.Instance.AddTimer(DateTimeOffset.UtcNow.AddSeconds(5), () =>
        {
            panelData?.failCB();
            UISystem.Instance.HideUI(this);
        });
    }

    public override IEnumerator OnHideUI()
    {
        if (!timer_DeadTime.IsFinish)
            TimerSystem.Instance.RemoveTimer(timer_DeadTime);
        RiseSdkListener.OnAdLoaded -= OnADLoaded;
        yield return base.OnHideUI();
    }

    private void OnADLoaded(string str)
    {
        if (str == 2.ToString() && RiseSdk.Instance.HasRewardAd())
        {
            if (!timer_DeadTime.IsFinish)
                TimerSystem.Instance.RemoveTimer(timer_DeadTime);
            RiseSdkListener.OnAdLoaded -= OnADLoaded;
            panelData?.playADCB?.Invoke();
            UISystem.Instance.HideUI(this);
        }
    }

}
