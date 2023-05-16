using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelData_NewFiling : UIPanelDataBase
{
    public Action DownloadDataCB;

    public UIPanelData_NewFiling(UIShowLayer UIShowLayer, Action DownloadDataCB) : base(Consts.UI_NewFiling, UIShowLayer)
    {
        this.DownloadDataCB = DownloadDataCB;
    }
}

public class UI_NewFiling : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI textTitle;
    [SerializeField] private TextMeshProUGUI textOK;
    [SerializeField] private TextMeshProUGUI textCancel;
    [SerializeField] private TextMeshProUGUI textDesc;

    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnOK;
    [SerializeField] private Button btnCancel;

    private Action DownloadDataCB;

    public override void OnInitUI()
    {
        base.OnInitUI();

        btnClose.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
        btnOK.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
            OnBtnOKClicked();
        });
        btnCancel.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
    }

    public override IEnumerator OnShowUI()
    {
        DownloadDataCB = ((UIPanelData_NewFiling)UIPanelData).DownloadDataCB;
        yield return base.OnShowUI();
        textTitle.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/CunDangTitle");
        textDesc.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/CunDangDescribe");
        textOK.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/CunDangButton");
        textCancel.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/CodeButton1");
    }
    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
    }

    private void OnBtnOKClicked()
    {
        UISystem.Instance.HideUI(this);
        DownloadDataCB?.Invoke();
    }

}
