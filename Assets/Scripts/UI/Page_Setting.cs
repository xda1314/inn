using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;
using System.Collections.Generic;
using Newtonsoft.Json;
using IvyCore;
using Ivy;
using Ivy.Firebase;
using Ivy.Mail;

public class Page_Setting : UIPanelBase
{
    [SerializeField] Button button_Sound;//声音开关
    [SerializeField] Button button_Music;//音乐开关
    [SerializeField] Button button_Haptic;//震动开关

    [SerializeField] GameObject soundOnGO;
    [SerializeField] GameObject soundOffGO;
    [SerializeField] GameObject musicOnGO;
    [SerializeField] GameObject musicOffGO;
    [SerializeField] GameObject hapticOnGO;
    [SerializeField] GameObject hapticOffGO;

    [Header("外部链接按钮")]
    [SerializeField] Button button_TermsService;//服务条款
    [SerializeField] Button button_PrivacyPolicy;//隐私政策
    [SerializeField] Button button_Help;//帮助
    [SerializeField] Button button_Mail;//邮件
    [SerializeField] GameObject mailRedPoint;
    [SerializeField] Button button_RewardCode;//兑换码
    [Header("文本")]
    [SerializeField] TextMeshProUGUI termsOfServiceText;
    [SerializeField] TextMeshProUGUI privacyPolicyText;
    [SerializeField] TextMeshProUGUI luangeText;
    [SerializeField] TextMeshProUGUI helpText;
    [SerializeField] TextMeshProUGUI Text_shop;
    [SerializeField] TextMeshProUGUI Text_collection;
    [SerializeField] TextMeshProUGUI Text_play;
    [SerializeField] TextMeshProUGUI Text_dungeon;
    [SerializeField] TextMeshProUGUI Text_setting;
    [SerializeField] TextMeshProUGUI Text_GamePlayerID;

    [SerializeField] Button button_Language;
    [SerializeField] Button button_Copy;//复制ID
    [SerializeField] TextMeshProUGUI lbl_playerID;
    [SerializeField] TextMeshProUGUI lbl_versionInfo;

    [SerializeField] private Button btn_MoreGame;
    [SerializeField] private TextMeshProUGUI tmp_MoreGame;

    [SerializeField] private Button btn_RateUs;
    [SerializeField] private TextMeshProUGUI tmp_RateUs;


    [SerializeField] private Button btn_Rank;
    [SerializeField] private TextMeshProUGUI tmp_Rank;

    [SerializeField] private Button btn_Achievement;
    [SerializeField] private TextMeshProUGUI tmp_Achievement;

    [Header("账号登录")]
    [SerializeField] private Button btn_Login;
    [SerializeField] private TextMeshProUGUI lbl_Login;

    [SerializeField] private TextMeshProUGUI lbl_LoginDesc;
    [SerializeField] private Button btn_deleteAcount;
    [SerializeField] private TextMeshProUGUI tmp_deleteAcount;

    [Header("用户按钮")]
    [SerializeField] private Button button_ChangeName;
    //[SerializeField] private Button button_ChangeIcon;
    [SerializeField] private Text lbl_PlayerName;
    [SerializeField] private TextMeshProUGUI lbl_AvatarEnabled;
    [SerializeField] private TextMeshProUGUI lbl_Confirm;
    [SerializeField] private TextMeshProUGUI lbl_ModifyName;
    [SerializeField] private TextMeshProUGUI lbl_ModifyAvatar;
    [SerializeField] private Image playerHeadIcon;
    [SerializeField] private Image[] allHeadIcon;
    [SerializeField] private GameObject[] Bg;
    [SerializeField] private ScrollRect IconScrollView;
    [SerializeField] private RectTransform gridRect;

    public static Action refreshMailRedPoint;

    public override void OnInitUI()
    {
        base.OnInitUI();
        InitUI();
        UIPanel_ChangeName.changeName += (str) =>
        {
            ChangePlayerName(str);
        };
        refreshMailRedPoint += () => { RefreshRedPoint(); };

        button_Sound.onClick.AddListener(SwitchSound);
        button_Music.onClick.AddListener(SwitchMusic);
        button_Haptic.onClick.AddListener(SwitchHaptic);
        button_TermsService.onClick.AddListener(OpenLinkOfTermsService);
        button_PrivacyPolicy.onClick.AddListener(OpenLinkOfPrivacyPolicy);
        button_Help.onClick.AddListener(OpenLinkOfHelp);
        button_Language.onClick.AddListener(SettingLanuage);
        button_Mail.onClick.AddListener(OpenLinkOfMail);
        button_RewardCode.onClick.AddListener(() => { UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_RewardCode, UIShowLayer.Popup)); });
        button_Copy.onClick.AddListener(CopyPlayerID);
        //button_ChangeIcon.onClick.AddListener(OnSetIcon);
        button_ChangeName.onClick.AddListener(() => UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_ChangeName)));

        btn_Login.onClick.AddListener(OnBtnLoginClicked);


        for (int i = 0; i < allHeadIcon.Length; i++)
        {
            if (allHeadIcon[i].TryGetComponent(out Image image))
            {
                allHeadIcon[i].GetComponent<Button>().onClick.AddListener(() => OnChooseHeadIcon(image.sprite));
            }
        }

        btn_RateUs.onClick.AddListener(() =>
        {
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_Evaluate));
            //#if UNITY_ANDROID

            //            RiseSdk.Instance.GetApp("com.merge.inn");
            //#elif UNITY_IOS
            //            RiseSdk.Instance.GetApp("1597263539");
            //#endif
        });

        btn_Rank.onClick.AddListener(() =>
        {
            RiseSdk.Instance.DisplayLeaderboards();
        });
        btn_Achievement.onClick.AddListener(() =>
        {
            RiseSdk.Instance.DisplayArchievements();
        });

        btn_deleteAcount.onClick.AddListener(() =>
        {
            GameDebug.Log("删除账号");
            string url = "https://docs.google.com/forms/d/e/1FAIpQLSd1fD-iI-n0RWO_9wM8MVEWhuakFxKvyQgfhYNoIedPoBgUZQ/viewform?usp=sf_link";
            RiseSdk.Instance.DisplayUrl("", url);
        });

        btn_MoreGame.onClick.AddListener(() =>
        {
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_MoreGame));
        });
        gridRect.sizeDelta = new Vector2(gridRect.sizeDelta.x, 0);
    }
    public void SetupLanguageText()
    {
        tmp_deleteAcount.text = I2.Loc.ScriptLocalization.Get("Obj/Other/Text2");
        tmp_MoreGame.text = I2.Loc.ScriptLocalization.Get("Obj/Other/Text3");
        tmp_RateUs.text = I2.Loc.ScriptLocalization.Get("Obj/Other/Text4");

        tmp_Rank.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/LeaderboardTitle");
        tmp_Achievement.text = I2.Loc.ScriptLocalization.Get("Obj/AchievementReward/Button");

        termsOfServiceText.text = I2.Loc.ScriptLocalization.Get("Obj/Set/Button/Text4");
        privacyPolicyText.text = I2.Loc.ScriptLocalization.Get("Obj/Set/Button/Text5");
        luangeText.text = I2.Loc.ScriptLocalization.Get("Obj/Set/Button/Text6");
        helpText.text = I2.Loc.ScriptLocalization.Get("Obj/Set/Button/Text7");

        lbl_AvatarEnabled.text = I2.Loc.ScriptLocalization.Get("Obj/Set/AvatarButton/Title");
        lbl_Confirm.text = I2.Loc.ScriptLocalization.Get("Obj/Set/NameButton/Text3");
        lbl_ModifyName.text = I2.Loc.ScriptLocalization.Get("Obj/Set/NameButton/Title");
        lbl_ModifyAvatar.text = I2.Loc.ScriptLocalization.Get("Obj/Set/AvatarButton/TapText1");

        Text_shop.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Shop_Title");
        Text_collection.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Collections_Title");
        Text_play.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Home_Title");
        Text_dungeon.text = I2.Loc.ScriptLocalization.Get("Obj/Dungeon/Title");
        Text_setting.text = I2.Loc.ScriptLocalization.Get("Obj/Set/Title/Text");
        Text_GamePlayerID.text = I2.Loc.ScriptLocalization.Get("Obj/Set/Describe/Text1");

        lbl_versionInfo.text = "v" + RiseSdk.Instance.GetConfig(RiseSdk.CONFIG_KEY_VERSION_NAME);

        RefreshPlayerID();
        ChangePlayerName(PlayerData.GetLocalPlayerName());
        RefreshSignInState();
    }

    //开启或关闭声音
    private void SwitchSound()
    {
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
        GameManager.Instance.playerData.IsEffectOn = !GameManager.Instance.playerData.IsEffectOn;
        GameManager.Instance.SavePlayerData_Effect();
        soundOnGO.SetActive(GameManager.Instance.playerData.IsEffectOn);
        soundOffGO.SetActive(!GameManager.Instance.playerData.IsEffectOn);
    }

    //开启或关闭音乐
    private void SwitchMusic()
    {
        GameManager.Instance.playerData.IsMusicOn = !GameManager.Instance.playerData.IsMusicOn;
        AudioManager.Instance.ChangeActivePlayBGM();
        GameManager.Instance.SavePlayerData_Music();
        musicOnGO.SetActive(GameManager.Instance.playerData.IsMusicOn);
        musicOffGO.SetActive(!GameManager.Instance.playerData.IsMusicOn);
    }

    private void SwitchHaptic()
    {
        GameManager.Instance.playerData.IsHapticOn = !GameManager.Instance.playerData.IsHapticOn;
        VibrateSystem.SetHapticsActive(GameManager.Instance.playerData.IsHapticOn);
        if (GameManager.Instance.playerData.IsHapticOn)
            VibrateSystem.Haptic(MoreMountains.NiceVibrations.HapticTypes.Selection);
        GameManager.Instance.SavePlayerData_Haptic();
        hapticOnGO.SetActive(GameManager.Instance.playerData.IsHapticOn);
        hapticOffGO.SetActive(!GameManager.Instance.playerData.IsHapticOn);
    }

    //初始化图标状态以及语言
    public void InitUI()
    {
        hapticOffGO.SetActive(!GameManager.Instance.playerData.IsHapticOn);
        musicOffGO.SetActive(!GameManager.Instance.playerData.IsMusicOn);
        soundOffGO.SetActive(!GameManager.Instance.playerData.IsEffectOn);
        SetMainIcon();
        SetupLanguageText();
    }

    //跳转服务条款
    private void OpenLinkOfTermsService()
    {
        //跳转服务条款链接
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
        RiseSdk.Instance.DisplayUrl("", @"https://www.lisgame.com/term-of-use.pdf");
    }

    //跳转隐私政策
    private void OpenLinkOfPrivacyPolicy()
    {
        //跳转隐私政策链接
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
        RiseSdk.Instance.DisplayUrl("", @"https://www.lilisgame.com/privacy-policy-en.pdf");
    }

    //跳转帮助页面
    private void OpenLinkOfHelp()
    {
        //跳转帮助页面链接
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
        //RiseSdk.Instance.DisplayUrl("", @"https://www.facebook.com/grandinnstory");

        try
        {
            string str = "";
            Dictionary<string, string> playerDict = new Dictionary<string, string>();
            playerDict.Add("level", TaskGoalsManager.Instance?.curLevelIndex.ToString() ?? "0");
            playerDict.Add("gems", Currencies.Gems.ToString());
            playerDict.Add("coins", Currencies.Coins.ToString());
            playerDict.Add("pay_orders", (GameManager.Instance?.playerData?.Pay_Orders.ToString()) ?? "0");
            playerDict.Add("pay_total", (GameManager.Instance?.playerData?.Pay_Totals.ToString()) ?? "0");
            str = JsonConvert.SerializeObject(playerDict);
            RiseSdk.Instance.HelpEngagement(FirebaseSystem.Instance.FirebaseID, str);
        }
        catch (Exception e)
        {
        }

    }

    //打开语言设置页面
    private void SettingLanuage()
    {
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
        UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_ChooseLanguage));
    }

    //跳转邮箱页面
    private void OpenLinkOfMail()
    {
        //跳转邮箱页面链接
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
        UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_Mail));
        //RiseSdk.Instance.DisplayUrl("", @"https://docs.google.com/forms/d/e/1FAIpQLSeE-xJ7evxhyjeJeAPZ71OzZ2_0ik-vvUU6w-pPW74fIWxz3w/viewform");
    }

    //复制玩家ID
    private void CopyPlayerID()
    {
        //复制玩家ID
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
        //RiseSdk.Instance.CopyText(FirebaseSystem.Instance.IsFirebaseLinked ? FirebaseSystem.Instance.FirebaseID : PlayerData.GetLocalPlayerID());
        RiseSdk.Instance.CopyText(FirebaseSystem.Instance.FirebaseID);
        TextTipSystem.Instance.ShowTip(button_Copy.transform.position + Vector3.up * 0.1f, I2.Loc.ScriptLocalization.Get("Obj/Set/copyid/Title"), TextTipColorType.Yellow);
    }

    private void OnBtnLoginClicked()
    {
        UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_AccountLogin));
    }

    //private void OnBtnGoogleClicked()
    //{
    //    //AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
    //    //CloudSystem.Instance.SignInOrSignOut_Google();
    //}

    //private void OnBtnFacebookClicked()
    //{
    //    //AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
    //    //CloudSystem.Instance.SignInOrSignOut_Facebook();
    //}

    public void SetMainIcon()
    {
        var localHeadName = PlayerData.GetLocalPlayerHeadIcon();
        for (int i = 0; i < allHeadIcon.Length; i++)
        {
            if (allHeadIcon[i].TryGetComponent<Image>(out var image) && image.sprite != null && image.sprite.name.Equals(localHeadName))
            {
                playerHeadIcon.sprite = image.sprite;
            }
        }
        OnChooseHeadIcon(playerHeadIcon.sprite);
        SetIconScrollView(playerHeadIcon.sprite);

        RefreshFirestoreDataDate();
    }
    private void OnChooseHeadIcon(Sprite sprite)
    {
        int i = 0;
        playerHeadIcon.sprite = sprite;
        switch (playerHeadIcon.sprite.name)
        {
            case "Avatar6":
                i = 0;
                break;
            case "Avatar9":
                i = 1;
                break;
            case "Avatar10":
                i = 2;
                break;
            case "Avatar3":
                i = 3;
                break;
            case "Avatar2":
                i = 4;
                break;
            case "Avatar7":
                i = 5;
                break;
            case "Avatar8":
                i = 6;
                break;
            case "Avatar4":
                i = 7;
                break;
            case "Avatar5":
                i = 8;
                break;
            case "Avatar1":
                i = 9;
                break;
        }
        SetIconBg(i);
        PlayerData.SetLocalPlayerHeadIcon(playerHeadIcon.sprite.name);
        GameManager.Instance.UploadNameOrProfileToCloud();
    }
    private void SetIconScrollView(Sprite sprite)
    {
        playerHeadIcon.sprite = sprite;
        if (playerHeadIcon.sprite.name == "Avatar6" || playerHeadIcon.sprite.name == "Avatar9" || playerHeadIcon.sprite.name == "Avatar10")
        {
            IconScrollView.normalizedPosition = new Vector2(0, 1);
        }
        else if (playerHeadIcon.sprite.name == "Avatar3" || playerHeadIcon.sprite.name == "Avatar2" || playerHeadIcon.sprite.name == "Avatar7")
        {
            IconScrollView.normalizedPosition = new Vector2(0, 0.6f);
        }
        else if (playerHeadIcon.sprite.name == "Avatar8" || playerHeadIcon.sprite.name == "Avatar4" || playerHeadIcon.sprite.name == "Avatar5")
        {
            IconScrollView.normalizedPosition = new Vector2(0, 0.2f);
        }
        else
        {
            IconScrollView.normalizedPosition = new Vector2(0, 0);
        }
        //PlayerData.SetLocalPlayerHeadIcon(sprite.name);
        //GameManager.Instance.UploadNameOrProfileToCloud();
    }
    //public void OnSetIcon()
    //{
    //    PlayerData.SetLocalPlayerHeadIcon(playerHeadIcon.sprite.name);
    //    GameManager.Instance.UploadNameOrProfileToCloud();
    //}
    private void SetIconBg(int index)
    {
        for (int i = 0; i < Bg.Length; i++)
        {
            if (i == index)
                Bg[i].SetActive(true);
            else
                Bg[i].SetActive(false);
        }
    }
    public void ChangePlayerName(string playerName)
    {
        lbl_PlayerName.text = playerName;
    }

    public void ShowChangeNameBtnClick()
    {
        //UIManager.Instance.OpenUIPanel(Consts.UIPanel_ChangeName, UILayer.Tip);
    }

    /// <summary>
    /// 刷新登录状态
    /// </summary>
    public void RefreshSignInState()
    {
        //#if UNITY_IOS
        //        if (CloudSystem.IsAppleConnect)
        //        {
        //            if (lbl_LoginApple != null)
        //                lbl_LoginApple.text = I2.Loc.ScriptLocalization.Get("Obj/Other/Text1");
        //        }
        //        else
        //        {
        //            if (lbl_LoginApple != null)
        //                lbl_LoginApple.text = "";
        //        }
        //#else
        //        if (CloudSystem.IsGoogleConnect)
        //        {
        //            if (lbl_LoginGoogle != null)
        //                lbl_LoginGoogle.text = I2.Loc.ScriptLocalization.Get("Obj/Other/Text1");
        //        }
        //        else
        //        {
        //            if (lbl_LoginGoogle != null)
        //                lbl_LoginGoogle.text = I2.Loc.ScriptLocalization.Get("Obj/Set/Button/Text3");
        //        }
        //#endif

        //        if (CloudSystem.IsFacebookConnect)
        //        {
        //            if (lbl_LoginFackbook != null)
        //                lbl_LoginFackbook.text = I2.Loc.ScriptLocalization.Get("Obj/Other/Text1");
        //        }
        //        else
        //        {
        //            if (lbl_LoginFackbook != null)
        //                lbl_LoginFackbook.text = I2.Loc.ScriptLocalization.Get("Obj/Set/Button/Text1");
        //        }

        if (FirebaseSystem.Instance.IsFirebaseLinked && !FirebaseSystem.Instance.IsAnonymous)
        {
            if (lbl_Login != null)
                lbl_Login.text = I2.Loc.ScriptLocalization.Get("Obj/Account/Btn");
        }
        else
        {
            if (lbl_Login != null)
                lbl_Login.text = I2.Loc.ScriptLocalization.Get("Obj/Account/Btn");
        }

        RefreshFirestoreDataDate();
    }

    public void RefreshFirestoreDataDate()
    {
        if (FirebaseSystem.Instance.IsFirebaseLinked && !FirebaseSystem.Instance.IsAnonymous)
        {
            if (lbl_LoginDesc != null)
            {
                string date = "";
                if (PlayerFirestoreData.FirestoreDataDate != DateTimeOffset.MinValue)
                {
                    TimeSpan interval = TimeManager.ServerUtcNow() - PlayerFirestoreData.FirestoreDataDate;
                    if (interval.TotalSeconds > 0)
                    {
                        if (interval.Days > 0)
                        {
                            date = String.Format(I2.Loc.ScriptLocalization.Get("Obj/Cloud/Text_d"), interval.Days);
                        }
                        else if (interval.Hours > 0)
                        {
                            date = String.Format(I2.Loc.ScriptLocalization.Get("Obj/Cloud/Text_h"), interval.Hours);
                        }
                        else if (interval.Minutes > 0)
                        {
                            date = String.Format(I2.Loc.ScriptLocalization.Get("Obj/Cloud/Text_m"), interval.Minutes);
                        }
                        else if (interval.Minutes == 0)
                        {
                            date = String.Format(I2.Loc.ScriptLocalization.Get("Obj/Cloud/Text_m"), 1);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(date))
                {
                    date = " (" + date + ")";
                }
                lbl_LoginDesc.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/SignSuccessDescribe") + date;
            }
        }
        else
        {
            if (lbl_LoginDesc != null)
                lbl_LoginDesc.text = I2.Loc.ScriptLocalization.Get("Obj/Set/Describe/Text");
        }

        RefreshPlayerID();
    }

    public void RefreshPlayerID()
    {
        lbl_playerID.text = FirebaseSystem.Instance.FirebaseID;
        //lbl_playerID.text = FirebaseSystem.Instance.IsFirebaseLinked ? FirebaseSystem.Instance.FirebaseID : PlayerData.GetLocalPlayerIDDisplay();
    }

    private void RefreshRedPoint()
    {
        MailSystem.Instance.RefreshMail(() =>
        {
            bool hasEmail = MailSystem.Instance.GetShowEmails().Count > 0;
            mailRedPoint.SetActive(hasEmail);
            UIPanel_TopBanner.refreshBanner?.Invoke();
        });
    }



    //    #region apple
    //    public static bool IsShowSignInApple { get; private set; } = false;
    //    /// <summary>
    //    /// 展示sdk的苹果登录icon
    //    /// </summary>
    //    public void ShowSDKSignInApple()
    //    {
    //#if UNITY_IOS
    //        GameDebug.Log("ShowSDKSignInApple");
    //        if (IsShowSignInApple)
    //            return;
    //        if (!CloudSystem.IsAppleConnect)
    //        {
    //            IsShowSignInApple = true;
    //            Vector3 centerScreenPos = UISystem.Instance.UICamera.WorldToScreenPoint(btn_LoginApple.transform.position);
    //            Vector3[] worldCorners = new Vector3[4];
    //            btn_LoginApple.GetComponent<RectTransform>().GetWorldCorners(worldCorners);
    //            Vector3 lbScreenPos = UISystem.Instance.UICamera.WorldToScreenPoint(worldCorners[0]);
    //            Vector3 trScreenPos = UISystem.Instance.UICamera.WorldToScreenPoint(worldCorners[2]);
    //            float pixelWidth = trScreenPos.x - lbScreenPos.x;
    //            float pixelHeight = trScreenPos.y - lbScreenPos.y;
    //            float x = centerScreenPos.x - pixelWidth * 0.5f;
    //            float y = centerScreenPos.y + pixelHeight * 0.5f;
    //            RiseSdk.Instance.ShowSignInWithApple(x, Screen.height - y, pixelWidth, pixelHeight, true);

    //        }
    //#endif
    //    }
    //    /// <summary>
    //    /// 隐藏sdk的苹果登录icon
    //    /// </summary>
    //    public void HideSDKSignInApple()
    //    {
    //#if UNITY_IOS
    //        GameDebug.Log("HideSDKSignInApple");
    //        if (!IsShowSignInApple)
    //            return;
    //        IsShowSignInApple = false;
    //        RiseSdk.Instance.HideSignInWithApple();
    //#endif
    //    }

    //    private void Awake()
    //    {
    //#if UNITY_IOS
    //        RiseSdkListener.SignInAppleSuccess += HideSignInWithApple;
    //#endif
    //    }

    //    private void OnDestroy()
    //    {
    //#if UNITY_IOS
    //        RiseSdkListener.SignInAppleSuccess -= HideSignInWithApple;
    //#endif
    //    }

    //    private void HideSignInWithApple(string str)
    //    {
    //        HideSDKSignInApple();
    //    }

    //    #endregion


}
