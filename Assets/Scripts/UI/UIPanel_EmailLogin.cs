using Ivy.Firebase;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelData_EmailLogin : UIPanelDataBase
{
    public Action<string> successCallBack;
    public Action<string, string> failCallBack;

    public UIPanelData_EmailLogin(Action<string> successCallBack, Action<string, string> failCallBack) : base(Consts.UIPanel_EmailLogin)
    {
        this.successCallBack = successCallBack;
        this.failCallBack = failCallBack;
    }
}

public class UIPanel_EmailLogin : UIPanelBase
{
    [SerializeField] private Button btn_Close;
    [SerializeField] private Text lbl_Title;
    [SerializeField] private Text lbl_Email;
    [SerializeField] private InputField input_Email;
    [SerializeField] private Text lbl_Password;
    [SerializeField] private InputField input_Password;
    [SerializeField] private Button btn_login;
    [SerializeField] private Text lbl_login;

    private UIPanelData_EmailLogin panelData;

    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_Close.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
    }

    public override IEnumerator OnShowUI()
    {
        panelData = UIPanelData as UIPanelData_EmailLogin;
        btn_login.onClick.AddListener(() =>
        {
            if (panelData != null
                && !string.IsNullOrWhiteSpace(input_Email.text)
                && !string.IsNullOrWhiteSpace(input_Password.text))
            {
                SaveUtils.SetString("last_input_email_address", input_Email.text);
                FirebaseSystem.Instance.SignIn_WithEmail(input_Email.text, input_Password.text, panelData?.successCallBack, panelData?.failCallBack);
            }
        });
        OnRefresh();
        yield return base.OnShowUI();
    }

    private void OnRefresh()
    {
        lbl_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Account/Email");
        lbl_Email.text = I2.Loc.ScriptLocalization.Get("Obj/Account/Emailtitle");
        input_Email.placeholder.GetComponent<Text>().text = I2.Loc.ScriptLocalization.Get("Obj/AccountEmail/account");
        lbl_Password.text = I2.Loc.ScriptLocalization.Get("Obj/Account/Password");
        input_Password.placeholder.GetComponent<Text>().text = I2.Loc.ScriptLocalization.Get("Obj/AccountEmail/Password");
        lbl_login.text = I2.Loc.ScriptLocalization.Get("Obj/Account/Signin");

        if (SaveUtils.HasKey("last_input_email_address"))
        {
            input_Email.text = SaveUtils.GetString("last_input_email_address");
        }
    }
}
