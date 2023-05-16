using Ivy;
using Ivy.Firebase;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel_AccountLogin : UIPanelBase
{
    [SerializeField] private Button btn_Close;
    [SerializeField] private Button btn_Copy;
    [SerializeField] private Text lbl_Title;
    [SerializeField] private Text lbl_Account;
    [SerializeField] private Text lbl_LoginState;

    [SerializeField] private VerticalLayoutGroup btnGrid;
    [SerializeField] private Button btn_Google;
    [SerializeField] private Text lbl_Google;
    [SerializeField] private Button btn_Apple;
    [SerializeField] private Text lbl_Apple;
    [SerializeField] private Button btn_gameCenter;
    [SerializeField] private Text lbl_gameCenter;
    [SerializeField] private Button btn_Facebook;
    [SerializeField] private Text lbl_Facebook;
    [SerializeField] private Button btn_Email;
    [SerializeField] private Text lbl_Email;

    [SerializeField] private Button btn_Save;
    [SerializeField] private Text lbl_Save;
    [SerializeField] private Button btn_Recovery;
    [SerializeField] private Text lbl_Recovery;

    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_Close.onClick.AddListener(() => { UISystem.Instance.HideUI(this); });

        btn_Copy.onClick.AddListener(() =>
        {
            if (FirebaseSystem.Instance.IsFirebaseLinked && !FirebaseSystem.Instance.IsAnonymous)
                RiseSdk.Instance.CopyText(FirebaseSystem.Instance.FirebaseID);
            else if (!FirebaseSystem.Instance.IsFirebaseLinked && !string.IsNullOrEmpty(FirebaseSystem.Instance.LastCache_FirebaseID))
                RiseSdk.Instance.CopyText(FirebaseSystem.Instance.LastCache_FirebaseID);
            TextTipSystem.Instance.ShowTip(btn_Copy.transform.position + Vector3.up * 0.1f, I2.Loc.ScriptLocalization.Get("Obj/Set/copyid/Title"), TextTipColorType.Yellow);
        });

        btn_Google.onClick.AddListener(() =>
        {
            if (FirebaseSystem.Instance.IsGooglePlayGamesLinked)
            {
                if (FirebaseSystem.Instance.CanUnlinkGooglePlayGames)
                    FirebaseSystem.Instance.SignOut_WithGooglePlayGames(SignOutSuccessCallBack, SignOutFailCallBack);
            }
            else
            {
                FirebaseSystem.Instance.SignIn_WithGooglePlayGames(SuccessCallBack, FailCallBack);
            }
        });
        btn_Apple.onClick.AddListener(() =>
        {
            if (FirebaseSystem.Instance.IsAppleLinked)
            {
                if (FirebaseSystem.Instance.CanUnlinkApple)
                    FirebaseSystem.Instance.SignOut_WithApple(SignOutSuccessCallBack, SignOutFailCallBack);
            }
            else
            {
                FirebaseSystem.Instance.SignIn_WithApple(SuccessCallBack, FailCallBack);
            }
        });
        btn_gameCenter.onClick.AddListener(() =>
        {
            if (FirebaseSystem.Instance.IsGameCenterLinked)
            {
                if (FirebaseSystem.Instance.CanUnlinkGameCenter)
                    FirebaseSystem.Instance.SignOut_WithGameCenter(SignOutSuccessCallBack, SignOutFailCallBack);
            }
            else
            {
                FirebaseSystem.Instance.SignIn_WithGameCenter(SuccessCallBack, FailCallBack);
            }
        });
        btn_Facebook.onClick.AddListener(() =>
        {
            if (FirebaseSystem.Instance.IsFacebookLinked)
            {
                if (FirebaseSystem.Instance.CanUnlinkFacebook)
                    FirebaseSystem.Instance.SignOut_WithFackbook(SignOutSuccessCallBack, SignOutFailCallBack);
            }
            else
            {
                FirebaseSystem.Instance.SignIn_WithFackbook(SuccessCallBack, FailCallBack);
            }
        });
        btn_Email.onClick.AddListener(() =>
        {
            if (FirebaseSystem.Instance.IsEmailLinked)
            {
                if (FirebaseSystem.Instance.CanUnlinkEmail)
                    FirebaseSystem.Instance.SignOut_WithEmail(SignOutSuccessCallBack, SignOutFailCallBack);
            }
            else
            {
                UISystem.Instance.ShowUI(new UIPanelData_EmailLogin((string str1) =>
                {
                    SuccessCallBack(str1);
                    UISystem.Instance.HideUI(Consts.UIPanel_EmailLogin);
                    if (this != null)
                        OnRefresh();
                }, (string str2, string str3) =>
                {
                    FailCallBack(str2, str3);
                }));
            }
        });

    }

    public override IEnumerator OnShowUI()
    {
        OnRefresh();
        yield return base.OnShowUI();
    }

    public void OnRefresh()
    {
        //lbl_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Account/Title");
        if (FirebaseSystem.Instance.IsFirebaseLinked
            && !FirebaseSystem.Instance.IsAnonymous)
        {
            lbl_Account.text = "ID:" + FirebaseSystem.Instance.FirebaseID;
            btn_Copy.gameObject.SetActive(true);
            if (PlayerFirestoreData.FirestoreDataDate != DateTimeOffset.MinValue)
            {
                string str = "";
                TimeSpan timeSpan = DateTimeOffset.UtcNow - PlayerFirestoreData.FirestoreDataDate;
                if (timeSpan.TotalDays >= 1)
                {
                    str = string.Format(I2.Loc.ScriptLocalization.Get("Obj/Cloud/Text_d"), timeSpan.Days.ToString());
                }
                else if (timeSpan.TotalHours >= 1)
                {
                    str = string.Format(I2.Loc.ScriptLocalization.Get("Obj/Cloud/Text_h"), timeSpan.Hours.ToString());
                }
                else if (timeSpan.TotalMinutes >= 1)
                {
                    str = string.Format(I2.Loc.ScriptLocalization.Get("Obj/Cloud/Text_m"), timeSpan.Minutes.ToString());
                }
                else
                {
                    str = I2.Loc.ScriptLocalization.Get("Obj/Account/JustNow");
                }
                lbl_LoginState.text = "( " + I2.Loc.ScriptLocalization.Get("Obj/Account/CloudSaveTitle") + str + " )";
                lbl_LoginState.color = new Color(0.5843f, 0.6549f, 0.7137f, 1);
            }
            else
            {
                lbl_LoginState.text = "";
            }
        }
        else if (!FirebaseSystem.Instance.IsFirebaseLinked && !string.IsNullOrEmpty(FirebaseSystem.Instance.LastCache_FirebaseID))
        {
            btn_Copy.gameObject.SetActive(true);
            lbl_Account.text = "ID:" + FirebaseSystem.Instance.LastCache_FirebaseID;
            lbl_LoginState.text = "( " + I2.Loc.ScriptLocalization.Get("Obj/Chain/WatchVideo_Text6") + " )";
            lbl_LoginState.color = new Color(0.6902f, 0.0078f, 0.2118f, 1);
        }
        else
        {
            btn_Copy.gameObject.SetActive(false);
            lbl_Account.text = I2.Loc.ScriptLocalization.Get("Obj/Account/Unbound");
            lbl_LoginState.text = "";
        }

        string linked = "(" + I2.Loc.ScriptLocalization.Get("Obj/Chain/SignSuccessButton") + ")";

#if UNITY_ANDROID
        btn_Google.gameObject.SetActive(true);
        string googleStr = I2.Loc.ScriptLocalization.Get("Obj/Account/Google");
        if (FirebaseSystem.Instance.IsGooglePlayGamesLinked)
        {
            string googleStr2 = FirebaseSystem.Instance.GetProviderLinkedDisplayName(FirebaseSystem.AccountProvider_GooglePlayGames);
            if (!string.IsNullOrEmpty(googleStr2))
                googleStr = googleStr2;
            googleStr += linked;
        }
        lbl_Google.text = googleStr;

        btn_Apple.gameObject.SetActive(false);
        btn_gameCenter.gameObject.SetActive(false);
#elif UNITY_IOS
        btn_Google.gameObject.SetActive(false);
        btn_Apple.gameObject.SetActive(true);
        btn_gameCenter.gameObject.SetActive(true);
        string appleStr = I2.Loc.ScriptLocalization.Get("Obj/Account/Apple");
        if (FirebaseSystem.Instance.IsAppleLinked)
        {
            string appleStr2 = FirebaseSystem.Instance.GetProviderLinkedDisplayName(FirebaseSystem.AccountProvider_Apple);
            if (!string.IsNullOrEmpty(appleStr2))
                appleStr = appleStr2;
            appleStr += linked;
        }
        lbl_Apple.text = appleStr;

        string gameCenterStr = I2.Loc.ScriptLocalization.Get("Obj/Account/Gc");
        if (FirebaseSystem.Instance.IsGameCenterLinked)
        {
            string gameCenterStr2 = FirebaseSystem.Instance.GetProviderLinkedDisplayName(FirebaseSystem.AccountProvider_GameCenter);
            if (!string.IsNullOrEmpty(gameCenterStr2))
                gameCenterStr = gameCenterStr2;
            gameCenterStr += linked;
        }
        lbl_gameCenter.text = gameCenterStr;
#endif

        string facebookStr = I2.Loc.ScriptLocalization.Get("Obj/Account/Fb");
        if (FirebaseSystem.Instance.IsFacebookLinked)
        {
            string facebookStr2 = FirebaseSystem.Instance.GetProviderLinkedDisplayName(FirebaseSystem.AccountProvider_Facebook);
            if (!string.IsNullOrEmpty(facebookStr2))
                facebookStr = facebookStr2;
            facebookStr += linked;
        }
        lbl_Facebook.text = facebookStr;

        string emailStr = I2.Loc.ScriptLocalization.Get("Obj/Account/Email");
        if (FirebaseSystem.Instance.IsEmailLinked)
        {
            string emailStr2 = FirebaseSystem.Instance.GetProviderLinkedDisplayName(FirebaseSystem.AccountProvider_Email);
            if (!string.IsNullOrEmpty(emailStr2))
                emailStr = emailStr2;
            emailStr += linked;
        }
        lbl_Email.text = emailStr;

        LayoutRebuilder.ForceRebuildLayoutImmediate(btnGrid.GetComponent<RectTransform>());

        lbl_Save.text = I2.Loc.ScriptLocalization.Get("Obj/Account/Save");
        btn_Save.onClick.RemoveAllListeners();
        btn_Save.onClick.AddListener(() =>
        {
            if (FirebaseSystem.Instance.CanUploadCloudData)
            {
                GameManager.Instance.SavePlayerDataAndUploadCloud();
                if (this != null)
                    OnRefresh();
            }
        });

        lbl_Recovery.text = I2.Loc.ScriptLocalization.Get("Obj/Account/Eecovery");
        btn_Recovery.onClick.AddListener(() =>
        {
            if (FirebaseSystem.Instance.IsFirebaseLinked && !FirebaseSystem.Instance.IsAnonymous)
                CloudSystem.Instance.RecoveryFirestorePlayerData();
            else
                TextTipSystem.Instance.ShowTip(btn_Recovery.transform.position + Vector3.up * 0.1f, I2.Loc.ScriptLocalization.Get("Obj/Account/Unbound"), TextTipColorType.Yellow);
        });

    }

    private void SuccessCallBack(string result)
    {
        if (result.Equals(FirebaseSystem.SignInOperationResult.DownloadCloudDataForce.ToString()))
        {
            // 强制下载云端数据
            CloudSystem.Instance.ForceDownloadCloudData();
        }
        else if (result.Equals(FirebaseSystem.SignInOperationResult.NeedCheckCloudData.ToString()))
        {
            // 需要检查云端存档
            CloudSystem.Instance.JustifyFirestorePlayerData();
        }
        else
        {
            CloudSystem.Instance.JustifyFirestorePlayerData();
            if (this != null)
                OnRefresh();
        }
    }

    private void FailCallBack(string result, string errorMessage)
    {
        if (result.Equals(FirebaseSystem.SignInOperationResult.ChangeAccountManual.ToString()))
        {
            // 提示切换账号
            if (this != null)
            {
                UISystem.Instance.ShowUI(new UIPanelData_LoginTip(I2.Loc.ScriptLocalization.Get("Obj/AccountError/Tips2"), I2.Loc.ScriptLocalization.Get("Obj/AccountError/Tips4"), I2.Loc.ScriptLocalization.Get("Obj/Chain/CunDangButton"), () =>
                {
                    FirebaseSystem.Instance.RelinkLastChooseProvider(SuccessCallBack, FailCallBack);
                }));
            }
        }
        else
        {
            // 弹出错误提示
            if (this != null)
            {
                UISystem.Instance.ShowUI(new UIPanelData_LoginTip(I2.Loc.ScriptLocalization.Get("Obj/AccountError/Tips1"), $"{errorMessage} ({result})", I2.Loc.ScriptLocalization.Get("Obj/Chain/CunDangButton"), null));
            }
        }
    }

    private void SignOutSuccessCallBack(string result)
    {
        if (this != null)
            OnRefresh();
        TextTipSystem.Instance.ShowTip(lbl_LoginState.transform.position + Vector3.up * 0.1f, I2.Loc.ScriptLocalization.Get("Obj/Account/Unboundsuccess"), TextTipColorType.Green);
    }

    private void SignOutFailCallBack(string result, string errorMessage)
    {
        if (this != null)
            OnRefresh();
        TextTipSystem.Instance.ShowTip(lbl_LoginState.transform.position + Vector3.up * 0.1f, I2.Loc.ScriptLocalization.Get("Obj/Account/Unboundfailed"), TextTipColorType.Red);
    }

}
