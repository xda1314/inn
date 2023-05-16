using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Nosignal : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI textTitle;
    [SerializeField] private TextMeshProUGUI textOK;
    [SerializeField] private TextMeshProUGUI textDesc1;
    [SerializeField] private TextMeshProUGUI textDesc2;

    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnOK;
    // Start is called before the first frame update
    private void Awake()
    {
        btnClose.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
        btnOK.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
            DoConnectAgain();
        });
    }
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        textTitle.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/SetLoinFailure1");
        textOK.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/SetLoinFailure4");
        textDesc1.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/SetLoinFailure2");
        textDesc2.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/SetLoinFailure3");
    }
    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
    }

    //执行重新连接
    private void DoConnectAgain()
    {

    }
}
