using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelData_LoginTip : UIPanelDataBase
{
    public string titleStr;
    public string bodyStr;
    public string btnStr;
    public Action OkCB;

    public UIPanelData_LoginTip(string titleStr, string bodyStr, string btnStr, Action okCB) : base(Consts.UIPanel_LoginTip)
    {
        this.titleStr = titleStr;
        this.bodyStr = bodyStr;
        this.btnStr = btnStr;
        this.OkCB = okCB;
    }
}

public class UIPanel_LoginTip : UIPanelBase
{
    [SerializeField] public Button btn_close;
    [SerializeField] public Text tmp_title;
    [SerializeField] public Text tmp_body;
    [SerializeField] public Button btn_ok;
    [SerializeField] public Text tmp_btn;

    private UIPanelData_LoginTip panelData_LoginTip;

    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_close.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
        btn_ok.onClick.AddListener(() =>
        {
            panelData_LoginTip?.OkCB?.Invoke();
            UISystem.Instance.HideUI(this);
        });
    }

    public override IEnumerator OnShowUI()
    {
        panelData_LoginTip = UIPanelData as UIPanelData_LoginTip;
        tmp_title.text = panelData_LoginTip.titleStr;
        tmp_body.text = panelData_LoginTip.bodyStr;
        tmp_btn.text = panelData_LoginTip.btnStr;
        yield return base.OnShowUI();
    }

}
