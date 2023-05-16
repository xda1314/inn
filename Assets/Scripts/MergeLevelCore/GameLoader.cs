using I2.Loc;
using ivy.game;
using Ivy;
using Ivy.Activity;
using Ivy.Firebase;
using Ivy.Mail;
using IvyCore;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameLoader : MonoBehaviour
{
    public static GameLoader Instance { get; private set; }

    public bool IsLoadingFinish { get; private set; } = false;

    [NonSerialized] public bool IsCheckingAccount = false;
    [NonSerialized] public bool IsOldPlayerFirstEnter = false;


    [SerializeField] private GameObject loadingBGGO;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        var start = RiseSdkListener.Instance;
        RiseSdk.Instance.Init();
    }

    private void Start()
    {
        StartCoroutine(StartLoading());
    }

    private IEnumerator StartLoading()
    {
        yield return new WaitForSeconds(0.3f);
        Input.multiTouchEnabled = false;
        yield return AssetSystem.Instance.InitSystem();
        gameObject.AddComponent<LowMemoryRelease>();
        var sdkhelp = SDKHelper.Instance;
        GameActionManager.InitWhenEngineStart();


        I2.Loc.LocalizationManager.UpdateSources();
        LanguageManager.SelectStartupLanguage();


        UISystem.Instance.InitSystem();
        UISystem.Instance.ShowUI(new UIPanelDataBase("UIPanel_Loading", UIShowLayer.Loading));
        yield return new WaitUntil(() =>
        {
            if (UISystem.Instance.TryGetUI<UIPanelBase>("UIPanel_Loading", out var loadingUi))
            {
                return !loadingUi.IsShowing;
            }
            return false;
        });
        SetLoadingProgress(0, 0);
        if (loadingBGGO != null)
        {
            loadingBGGO.SetActive(false);
            GameObject.Destroy(loadingBGGO);
        }
        yield return AssetSystem.Instance.CheckForCatalogUpdate();
        SetLoadingProgress(0.05f, 0.3f);

        yield return DownloadAsset();
        AssetSystem.Instance.LoadAsset<I2.Loc.LanguageSourceAsset>("I2Languages_remote", (I2.Loc.LanguageSourceAsset loc) =>
        {
            LocalizationManager.AddGlobalLanguageSourceAsset(loc);
        });
        SetLoadingProgress(0.1f, 0.3f);

        yield return LoadData();
    }

    private IEnumerator LoadData()
    {
        TimeManager.Instance.TryExcuteWithServerUtc(null, null);
        yield return GameConfig.Instance.LoadAllConfig();
        SetLoadingProgress(0.2f, 0.3f);
        yield return GameManager.Instance.LoadGameData();
        SetLoadingProgress(0.3f, 0.3f);
        TimeManager.Instance.Init();

        FirebaseSystem.Instance.InitSystem();
        FirebaseSystem.Instance.SetFirebaseConnectedCallback(() =>
        {
            //首次登录显示第三方平台昵称
            PlayerData.TrySetLoginpLatformDefaultName();

            // 检测邮件
            MailSystem.Instance.InitSystem(() =>
            {
                //展示新邮件提示
                Page_Setting.refreshMailRedPoint?.Invoke();
            });
            // 检测snapMessage
            SnapMessageSystem.Instance.InitSystem();
            SnapMessageSystem.Instance.AddListener("force_download", (object obj) =>
            {
                if (bool.TryParse(obj.ToString(), out bool forceDownload))
                {
                    FirebaseSystem.Instance.SetDownloadCloudDataFinish(false);
                    UISystem.Instance.HideAllUI();
                    CloudSystem.Instance.ForceDownloadCloudData();
                }
            });

            // 开始检测线上活动
            //TimeManager.Instance.TryExcuteWithServerUtc(() =>
            //{
            //    //ConfigFirestoreData.Instance.ReadFirestore_EventDate();
            //}, () =>
            //{
            //    //TimeManager.Instance.TryExcuteWithServerUtc(() =>
            //    //{
            //    //    ConfigFirestoreData.Instance.ReadFirestore_EventDate();
            //    //}, null, 1000);
            //});
        });

        GameManager.Instance.SkipLevelByEditor();//editor模式下跳关
        yield return null;
        SetLoadingProgress(0.4f, 0.3f);
        UISystem.Instance.uiMainMenu.gameObject.SetActive(true);
        UISystem.Instance.uiMainMenu.OnInitUI();//初始化主界面
        UISystem.Instance.AdaptTopUI();
        yield return null;
        // 播放插屏广告
        AdManager.TryPlayInterstitialAD(AdManager.ADTag.app_open, RemoteConfigSystem.remoteKey_si_app_open);

        SetLoadingProgress(0.5f, 0.3f);
        yield return null;
        bool needCheckOldPlayer = GameManager.Instance.playerData.Experience == 0;
        IsCheckingAccount = true;
        DateTimeOffset startCheckDate = DateTimeOffset.UtcNow;
        // 自动登录
        FirebaseSystem.Instance.SignInAuto((string result) =>
        {
            if (result.Equals(FirebaseSystem.SignInOperationResult.DownloadCloudDataForce.ToString()))
            {
                // 强制下载云端数据
                CloudSystem.Instance.ForceDownloadCloudData();
            }
            else if (result.Equals(FirebaseSystem.SignInOperationResult.NeedCheckCloudData.ToString()))
            {
                // 需要检查云端存档
                CloudSystem.Instance.JustifyFirestorePlayerData(true);
            }
            else
            {
                IsCheckingAccount = false;
                FirebaseSystem.Instance.SetCheckCloudDataFinish(false);
                CloudSystem.Instance.JustifyFirestorePlayerData();
            }

        }, (string error, string errorMessage) =>
        {
            IsCheckingAccount = false;
        });

        while (!Application.isEditor && needCheckOldPlayer && IsCheckingAccount)
        {
            if ((DateTimeOffset.UtcNow - startCheckDate).TotalSeconds >= 10)
                break;
            yield return null;
        }
        SetLoadingProgress(0.6f, 0.3f);
        yield return null;
        yield return GameManager.Instance.Coroutine_LoadingToMapScene();
        PushNotificationSystem.Instance.InitSystem();//初始化后台推送       
        // 检测登录天数
        if (GameManager.Instance.playerData != null)
        {
            //if (GameManager.Instance.playerData.Last_Active_Day.Date < TimeManager.Instance.UtcNow().Date)
            //{
            //    GameManager.Instance.playerData.Last_Active_Day = TimeManager.Instance.UtcNow();
            //    GameManager.Instance.playerData.Total_Active_Days++;
            //    RiseSdk.Instance.SetUserProperty("total_activity_days", GameManager.Instance.playerData.Total_Active_Days.ToString());
            //}
            if (TimeManager.Instance.UtcNow() > GameManager.Instance.playerData.firstPlayDate)
            {
                int days = (TimeManager.Instance.UtcNow() - GameManager.Instance.playerData.firstPlayDate).Days;
                RiseSdk.Instance.SetUserProperty("total_activity_days", days.ToString());
            }
            GameManager.Instance.playerData.Total_App_Open_Count++;
            RiseSdk.Instance.SetUserProperty("user_total_app_open", GameManager.Instance.playerData.Total_App_Open_Count.ToString());
        }
        UIPanel_TopBanner.refreshBanner?.Invoke();//刷新顶部banner

        var temp = UI_TutorManager.Instance;
        // 检测封禁情况
        Black_Board.Instance.CheckBlockAccountData();
        RiseSdk.Instance.SaveUserAttribute("");
        // 上传排行榜
        if (GameManager.Instance.playerData.Experience > 0)
            RiseSdk.Instance.UpdateLeaderboard("world_rank", GameManager.Instance.playerData.Experience);

        ActivitySystem.Instance.InitSystemAsync(new ActivitySystemOpenConfig());
        InAppMessageSystem.Instance.InitSystem();

        SetLoadingProgress(1f, 0);
        yield return null;
        GameManager.Instance.AddPlayADFailEvent();
        yield return EnterGame();
        IsLoadingFinish = true;

        Billing.Instance.QueryOrder();

        // 检测封禁情况
        FirebaseSystem.Instance.ReadFirestoreAsync(Black_Board.CollectionName, Black_Board.Instance.OnReadFirestoreSuccess, null);



#if UNITY_EDITOR || DEVELOPMENT_BUILD || IVY_DEBUG
        InAppMessageSystem.Instance.Debug_SendInAppMessage(@"{
            ""id"":""3333333"",
            ""title"":""aber"",
            ""body"":""brebed"",
            ""image"":""bdrb"",
            ""action"":""mergeelves://crosspromotion?package=com.merge.farmtown""
        }");
#endif

    }


    public void SetLoadingProgress(float value, float duration = 0)
    {
        if (UISystem.Instance.TryGetUI<UIPanel_Loading>("UIPanel_Loading", out var loadingPanel))
        {
            loadingPanel.SetLoadingProcess(value, duration);
        }
    }

    /// <summary>
    /// 玩家首次进入游戏直接跳合成界面
    /// </summary>
    private IEnumerator EnterGame()
    {
        string saveStr = SaveUtils.GetString(Consts.SaveKey_LevelData_Prefix + MergeLevelManager.MapName_Main);
        if (string.IsNullOrEmpty(saveStr))
        {
            //第一次进入游戏，直接进入合成界面
            AdManager.ManualEnterMergePanel = true;
            UIPanel_TransitionAnimation.needTween = false;
            MergeLevelManager.Instance.ShowMergePanelByDungeonType(MergeLevelType.mainLine);
            yield return new WaitUntil(() =>
            {
                if (UISystem.Instance.HasInShowingUI)
                    return false;

                if (UISystem.Instance.TryGetUIOnlyOpen<UIPanelBase>(Consts.UIPanel_Merge, out var ui))
                {
                    if (!ui.IsShowing)
                        return true;
                }
                return false;
            });

            UISystem.Instance.HideUI("UIPanel_Loading");
            yield return null;
            if (!SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnGuide))
            {
                UI_TutorManager.Instance.RunTutorWithName("InnGuide");
                SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnGuide, true);
            }
        }
        else
        {
            UISystem.Instance.HideUI("UIPanel_Loading");
        }
    }


    public enum DownloadState
    {
        None = 0,
        GetDownloadSize,
        Downloading,
        Success,
        fail
    }

    private static DownloadState abDownloadState;

    public IEnumerator DownloadAsset(bool tryAgain = false)
    {
        UISystem.Instance.TryGetUI<UIPanel_Loading>("UIPanel_Loading", out var loadingPanel);

        abDownloadState = DownloadState.GetDownloadSize;
        yield return AssetSystem.Instance.GetDownloadSizeAsync("preload", (long downloadSize) =>
        {
            if (downloadSize > 0)
            {
                abDownloadState = DownloadState.Downloading;
                if (loadingPanel != null)
                {
                    loadingPanel.ShowDownloadSlider(true);
                }
            }
            else
            {
                abDownloadState = DownloadState.Success;
            }
        }, () =>
        {
            abDownloadState = DownloadState.fail;
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_DownloadFail, UIShowLayer.Top), true);
        });

        if (abDownloadState == DownloadState.Downloading)
        {
            AsyncOperationHandle handle = AssetSystem.Instance.DownloadDependenciesAsync("preload", () =>
            {
                abDownloadState = DownloadState.Success;
                if (loadingPanel != null)
                {
                    loadingPanel.ShowDownloadSlider(false);
                }
            }, () =>
            {
                abDownloadState = DownloadState.fail;
                UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_DownloadFail, UIShowLayer.Top), true);
            });

            while (abDownloadState == DownloadState.Downloading)
            {
                if (loadingPanel != null)
                {
                    loadingPanel.SetDownloadSliderValue(handle);
                }
                yield return new WaitForSeconds(0.2f);
            }
        }

        if (!tryAgain)
        {
            yield return new WaitUntil(() => { return abDownloadState == DownloadState.Success; });
        }
    }

}
