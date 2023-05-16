using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ivy.game;
using IvyCore;
using System.IO;
using Ivy;
using Ivy.Firebase;
using Ivy.Activity;

public class CloudSystem : SingletonMono<CloudSystem>
{
    private static bool isStartResetData = false;
    /// <summary>
    /// 下载firestore存档,覆盖本地
    /// </summary>
    public void TryChangeLocalDataFromFirestore(PlayerFirestoreData info)
    {
        if (info == null)
        {
            return;
        }

        if (isStartResetData)
        {
            return;
        }
        isStartResetData = true;
        StartCoroutine(Coroutine_ChangeFirestoreDataToLocal(info));
    }


    private IEnumerator Coroutine_ChangeFirestoreDataToLocal(PlayerFirestoreData info)
    {
        UISystem.Instance.HideAllUI();
        //重新加载游戏
        UISystem.Instance.ShowUI(new UIPanelDataBase("UIPanel_Loading", UIShowLayer.Loading));
        yield return new WaitUntil(() => UISystem.Instance.TryGetUI<UIPanel_Loading>("UIPanel_Loading", out _));
        if (UISystem.Instance.TryGetUI<UIPanel_Loading>("UIPanel_Loading", out var loadingPanel))
        {
            loadingPanel.SetLoadingProcess(0, 0);
            loadingPanel.SetLoadingProcess(0.5f, 0.3f);
        }
        yield return null;

        // 下载活动数据
        bool downloadActivityFinish = false;
        bool downloadActivityError = false;
        ActivitySystem.Instance.DownloadCloudDataToLocalAsync(() =>
        {
            downloadActivityFinish = true;
        }, () =>
        {
            downloadActivityFinish = true;
            downloadActivityError = true;
        });
        yield return new WaitUntil(() => downloadActivityFinish);
        if (downloadActivityError)
        {
            UISystem.Instance.HideAllUI();
            yield break;
        }

        try
        {
            //解析线上存档，并处理
            info.FirestoreDataToLocalData();
            FirebaseSystem.Instance.SetDownloadCloudDataFinish(true);
            FirebaseSystem.Instance.SaveFirebaseID();

            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("needDownload", false);
            string json = JsonConvert.SerializeObject(dict);
            //PlayerFirestoreData.Instance.UpdateFireStore(json, "needDownload");
            FirebaseSystem.Instance.UpdateFirestoreAsync(PlayerFirestoreData.CollectionName, "needDownload", json, (string str) =>
            {

            }, null);
        }
        catch (Exception e)
        {
            FirebaseSystem.Instance.SetForbidUploadCloudData(true);
            //canUploadData = false;
            DebugSetting.LogError(e, "[DownloadFirestoreData error]");
        }
        if (UISystem.Instance.TryGetUI<UIPanel_Loading>("UIPanel_Loading", out var loadingPanel2))
        {
            loadingPanel2.SetLoadingProcess(1f, 0.1f);
        }
        yield return null;
        UISystem.Instance.uiMainMenu.MergeUIReturnMainmenu();
        yield return GameManager.Instance.LoadGameData();
        UI_TutorManager.Instance.CloseTutor();
        ShopSystem.Instance.refreshShopAction?.Invoke();
        DungeonSystem.Instance.CheckDungeonEventByComplete?.Invoke();
        DungeonSystem.Instance.CheckDungeonEventByOpen?.Invoke();
        ActivitySystem.Instance.InitSystemAsync(new ActivitySystemOpenConfig());
        UI_Manager.Instance.InvokeRefreshEvent("", "RefreshEvent_LanguageChanged");
        UISystem.Instance.HideUI("UIPanel_Loading");
        FestivalSystem.Instance.refreshFestival?.Invoke();
        isStartResetData = false;

        GameLoader.Instance.IsCheckingAccount = false;
    }

    private void TryChangeLocalDataFromFirestore_WithoutLoading(PlayerFirestoreData info)
    {
        if (info == null)
        {
            return;
        }

        if (isStartResetData)
        {
            return;
        }
        isStartResetData = true;
        StartCoroutine(Coroutine_ChangeFirestoreDataToLocal_WithoutLoading(info));
    }


    private IEnumerator Coroutine_ChangeFirestoreDataToLocal_WithoutLoading(PlayerFirestoreData info)
    {
        //UISystem.Instance.HideAllUI();
        ////重新加载游戏
        //UISystem.Instance.ShowUI(new UIPanelDataBase("UIPanel_Loading", UIShowLayer.Loading));
        //if (UISystem.Instance.TryGetUI<UIPanel_Loading>("UIPanel_Loading", out var loadingPanel))
        //{
        //    loadingPanel.SetLoadingProcess(0.5f, 0.3f);
        //}
        //yield return null;


        // 下载活动数据
        bool downloadActivityFinish = false;
        bool downloadActivityError = false;
        ActivitySystem.Instance.DownloadCloudDataToLocalAsync(() =>
        {
            downloadActivityFinish = true;
        }, () =>
        {
            downloadActivityFinish = true;
            downloadActivityError = true;
        });
        yield return new WaitUntil(() => downloadActivityFinish);
        if (downloadActivityError)
        {
            yield break;
        }


        try
        {
            //解析线上存档，并处理
            info.FirestoreDataToLocalData();
            FirebaseSystem.Instance.SetDownloadCloudDataFinish(true);
            FirebaseSystem.Instance.SaveFirebaseID();

            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("needDownload", false);
            string json = JsonConvert.SerializeObject(dict);
            //PlayerFirestoreData.Instance.UpdateFireStore(json, "needDownload");
            FirebaseSystem.Instance.UpdateFirestoreAsync(PlayerFirestoreData.CollectionName, "needDownload", json, (string str) =>
            {
            }, null);
        }
        catch (Exception e)
        {
            FirebaseSystem.Instance.SetForbidUploadCloudData(true);
            //canUploadData = false;
            DebugSetting.LogError(e, "[DownloadFirestoreData error]");
        }
        //if (UISystem.Instance.TryGetUI<UIPanel_Loading>("UIPanel_Loading", out var loadingPanel2))
        //{
        //    loadingPanel2.SetLoadingProcess(1f, 0.1f);
        //}
        yield return null;
        UISystem.Instance.uiMainMenu.MergeUIReturnMainmenu();
        yield return GameManager.Instance.LoadGameData();
        UI_TutorManager.Instance.CloseTutor();
        ShopSystem.Instance.refreshShopAction?.Invoke();
        DungeonSystem.Instance.CheckDungeonEventByComplete?.Invoke();
        ActivitySystem.Instance.InitSystemAsync(new ActivitySystemOpenConfig());
        UI_Manager.Instance.InvokeRefreshEvent("", "RefreshEvent_LanguageChanged");
        //UISystem.Instance.HideUI("UIPanel_Loading");
        FestivalSystem.Instance.refreshFestival?.Invoke();
        DungeonSystem.Instance.CheckDungeonEventByOpen?.Invoke();
        isStartResetData = false;

        GameLoader.Instance.IsCheckingAccount = false;
    }

    /// <summary>
    /// 上传firestore存档
    /// </summary>
    public void TryUploadToFirestoreData()
    {
        GameDebug.Log("TryUploadToFirestoreData-----------");
#if UNITY_EDITOR
        if (DebugSetting.CanUseDebugConfig(out SO_DebugConfig sO_Debug))
        {
            if (sO_Debug.textRetreated)
            {
                //PlayerFirestoreData.Instance.SetFireStore(PlayerFirestoreData.Instance.LocalToFirestoreJson(), null);
                FirebaseSystem.Instance.SetFirestoreAsync(PlayerFirestoreData.CollectionName, PlayerFirestoreData.Instance.LocalToFirestoreJson(), null, null);
            }
        }
#endif

        //判断是否可以上传存档
        if (!JustifyCanUploadNow())
        {
            GameDebug.Log("TryUploadToFirestoreData return");
            return;
        }

        GameDebug.Log(" PlayerFirestoreData.Instance.SetFireStore");
        //if (successCB != null)
        //    PlayerFirestoreData.Instance.SetPlayerDataSuccessCB(successCB);
        //PlayerFirestoreData.Instance.SetFireStore(PlayerFirestoreData.Instance.LocalToFirestoreJson(), null);
        FirebaseSystem.Instance.SetFirestoreAsync(PlayerFirestoreData.CollectionName, PlayerFirestoreData.Instance.LocalToFirestoreJson(), PlayerFirestoreData.Instance.OnSetFirestoreSuccess, null);
    }

    private static bool isTryGetPlayerBaseData = false;
    private bool JustifyCanUploadNow()
    {
        if (!FirebaseSystem.Instance.IsFirebaseLinked)
        {
            return false;
        }

        // 封禁账号禁止上传数据
        if (Black_Board.IsBlockAccount_All || Black_Board.IsBlockAccount_CanPlayLocal)
        {
            return false;
        }

        if (isTryGetPlayerBaseData)
        {
            GameDebug.Log("isGetPlayerBaseData true");
            if (FirebaseSystem.Instance.CanUploadCloudData)
            {
                GameDebug.Log("canUploadData true");
                return true;
            }
        }
        else
        {
            GameDebug.Log("isGetPlayerBaseData false");

            if (GameManager.Instance != null
                && GameManager.Instance.playerData != null)
            {
                JustifyFirestorePlayerData();
            }
        }
        return false;
    }
    private IEnumerator TextRetreated()
    {
        yield return new WaitForSeconds(5f);//延时5s，模拟下载数据

        string str = File.ReadAllText("C://MergeInn//JsonData.json");
        if (!string.IsNullOrEmpty(str))
        {
            PlayerFirestoreData info1 = JsonConvert.DeserializeObject<PlayerFirestoreData>(str);
            TryChangeLocalDataFromFirestore(info1);
            GameDebug.LogError("模拟回档成功");
        }
    }
    /// <summary>
    /// 判断firestore存档版本
    /// </summary>
    public void JustifyFirestorePlayerData(bool autoSignInCheck = false)
    {
        if (GameManager.Instance != null
            && GameManager.Instance.playerData != null)
        {
#if UNITY_EDITOR
            if (DebugSetting.CanUseDebugConfig(out SO_DebugConfig sO_Debug))
            {
                if (sO_Debug.textRetreated)
                {
                    StartCoroutine(TextRetreated());
                }
            }
#endif

            isTryGetPlayerBaseData = true;
            FirebaseSystem.Instance.ReadFirestoreAsync(PlayerFirestoreData.CollectionName, (string jsonStr) =>
            {
                if (string.IsNullOrEmpty(jsonStr) || jsonStr == "{}")
                {
                    //线上没有存档，上传本地存档
                    GameDebug.LogError("线上没有存档，上传本地存档");
                    FirebaseSystem.Instance.SetCheckCloudDataFinish();
                    FirebaseSystem.Instance.SaveFirebaseID();
                    FirebaseSystem.Instance.SetFirestoreAsync(PlayerFirestoreData.CollectionName, PlayerFirestoreData.Instance.LocalToFirestoreJson(), PlayerFirestoreData.Instance.OnSetFirestoreSuccess, null);
                    GameLoader.Instance.IsCheckingAccount = false;
                    return;
                }

                try
                {
                    PlayerFirestoreData info = JsonConvert.DeserializeObject<PlayerFirestoreData>(jsonStr);
                    if (info == null)
                    {
                        GameLoader.Instance.IsCheckingAccount = false;
                        return;
                    }

                    // 更新云端数据时间
                    if (!string.IsNullOrEmpty(info.dataDate) && DateTimeOffset.TryParse(info.dataDate, out var lastUpdateDate))
                    {
                        PlayerFirestoreData.FirestoreDataDate = lastUpdateDate;
                    }
                    else
                    {
                        PlayerFirestoreData.FirestoreDataDate = DateTimeOffset.MinValue;
                    }

#if UNITY_EDITOR
                    if (DebugSetting.CanUseDebugConfig(out var debugSO) && debugSO.DebugReloadFirestoreData)
#else
                    if (!info.needDownload
                        && info.Experience <= GameManager.Instance.playerData.Experience
                        && info.user_level <= TaskGoalsManager.Instance.curLevelIndex)
                    {
                        if (CloudIsLargerThanLocalVersion(info.app_version))
                        {
                            GameDebug.LogWarning("canUploadData false");
                            FirebaseSystem.Instance.SetCheckCloudDataFinish();
                            FirebaseSystem.Instance.SetForbidUploadCloudData(true);
                        }
                        else
                        {
                            //可以上传数据
                            GameDebug.LogWarning("canUploadData true");
                            FirebaseSystem.Instance.SetCheckCloudDataFinish();
                        }
                        GameLoader.Instance.IsCheckingAccount = false;
                    }
                    else
#endif
                    {
                        GameDebug.LogWarning("尝试下载数据");
                        //尝试下载数据
                        //canUploadData = false;
                        FirebaseSystem.Instance.SetCheckCloudDataFinish();
                        FirebaseSystem.Instance.SetDownloadCloudDataFinish(false);
                        UI_TutorManager.Instance.CloseTutor();
                        if (autoSignInCheck
                            && GameManager.Instance.playerData.Experience < 1
                            && GameManager.Instance.playerData.Experience < info.Experience)
                        {
                            GameLoader.Instance.IsOldPlayerFirstEnter = true;
                            TryChangeLocalDataFromFirestore_WithoutLoading(info);
                        }
                        else
                        {
                            GameLoader.Instance.IsCheckingAccount = false;
                            UISystem.Instance.ShowUI(new UIPanelData_NewFiling(UIShowLayer.Top, () =>
                            {
                                TryChangeLocalDataFromFirestore(info);
                            }));
                        }
                    }
                }
                catch (Exception)
                {
                    GameLoader.Instance.IsCheckingAccount = false;
                }

            }, _ =>
            {
                GameLoader.Instance.IsCheckingAccount = false;
            });
        }
    }

    public void ForceDownloadCloudData()
    {
        FirebaseSystem.Instance.ReadFirestoreAsync(PlayerFirestoreData.CollectionName, (string jsonStr) =>
        {
            if (string.IsNullOrEmpty(jsonStr) || jsonStr == "{}")
            {
                //线上没有存档，上传本地存档
                GameDebug.LogError("线上没有存档，上传本地存档");
                FirebaseSystem.Instance.SetCheckCloudDataFinish();
                FirebaseSystem.Instance.SaveFirebaseID();
                FirebaseSystem.Instance.SetFirestoreAsync(PlayerFirestoreData.CollectionName, PlayerFirestoreData.Instance.LocalToFirestoreJson(), PlayerFirestoreData.Instance.OnSetFirestoreSuccess, null);
                return;
            }

            try
            {
                PlayerFirestoreData info = JsonConvert.DeserializeObject<PlayerFirestoreData>(jsonStr);
                if (info == null)
                {
                    return;
                }

                // 更新云端数据时间
                if (!string.IsNullOrEmpty(info.dataDate) && DateTimeOffset.TryParse(info.dataDate, out var lastUpdateDate))
                {
                    PlayerFirestoreData.FirestoreDataDate = lastUpdateDate;
                }
                else
                {
                    PlayerFirestoreData.FirestoreDataDate = DateTimeOffset.MinValue;
                }

                GameDebug.LogWarning("尝试下载数据");
                //尝试下载数据
                //canUploadData = false;
                FirebaseSystem.Instance.SetCheckCloudDataFinish();
                FirebaseSystem.Instance.SetDownloadCloudDataFinish(false);
                UI_TutorManager.Instance.CloseTutor();
                TryChangeLocalDataFromFirestore(info);
            }
            catch (Exception)
            {

            }

        }, null);
    }

    public void RecoveryFirestorePlayerData()
    {
        if (GameManager.Instance != null
            && GameManager.Instance.playerData != null)
        {
            FirebaseSystem.Instance.ReadFirestoreAsync(PlayerFirestoreData.CollectionName, (string jsonStr) =>
            {
                if (string.IsNullOrEmpty(jsonStr) || jsonStr == "{}")
                {
                    return;
                }

                try
                {
                    PlayerFirestoreData info = JsonConvert.DeserializeObject<PlayerFirestoreData>(jsonStr);
                    if (info == null)
                    {
                        return;
                    }

                    // 更新云端数据时间
                    if (!string.IsNullOrEmpty(info.dataDate) && DateTimeOffset.TryParse(info.dataDate, out var lastUpdateDate))
                    {
                        PlayerFirestoreData.FirestoreDataDate = lastUpdateDate;
                    }
                    else
                    {
                        PlayerFirestoreData.FirestoreDataDate = DateTimeOffset.MinValue;
                    }

                    GameDebug.LogWarning("尝试下载数据");
                    //尝试下载数据
                    //canUploadData = false;
                    FirebaseSystem.Instance.SetDownloadCloudDataFinish(false);
                    UI_TutorManager.Instance.CloseTutor();
                    UISystem.Instance.ShowUI(new UIPanelData_NewFiling(UIShowLayer.Top, () =>
                    {
                        TryChangeLocalDataFromFirestore(info);
                    }));
                }
                catch (Exception)
                {

                }

            }, null);
        }
    }

    private static int[] GetAppVersion(string content)
    {
        if (!string.IsNullOrEmpty(content))
        {
            string[] list = content.Split('.');
            if (list != null && list.Length > 0)
            {
                int[] vers = new int[list.Length];
                for (int i = 0; i < list.Length; i++)
                {
                    if (int.TryParse(list[i], out int value))
                        vers[i] = value;
                    else
                        vers[i] = 0;
                }
                return vers;
            }
        }
        return null;
    }

    /// <summary>
    /// 大于等于Version
    /// </summary>
    /// <param name="app_version"></param>
    /// <returns></returns>
    private static bool CloudIsLargerThanLocalVersion(string app_version)
    {
        try
        {
            string localVersion = RiseSdk.Instance.GetConfig(RiseSdk.CONFIG_KEY_VERSION_NAME);
            int[] versions = GetAppVersion(app_version);
            int[] local_versions = GetAppVersion(localVersion);
            if (versions == null || local_versions == null)
            {
                return false;
            }
            for (int i = 0; i < versions.Length && i < local_versions.Length; i++)
            {
                if (versions[i] > local_versions[i])
                {
                    return true;
                }
                else if (versions[i] < local_versions[i])
                {
                    return false;
                }
            }
            return versions.Length > local_versions.Length;
        }
        catch (Exception e)
        {
            DebugSetting.LogError(e, "CompareLargeLocalVersion error!");
            return false;
        }
    }


}
