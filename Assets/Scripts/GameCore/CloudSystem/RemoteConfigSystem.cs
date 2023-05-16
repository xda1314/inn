using UnityEngine;

/// <summary>
/// 远程配置
/// </summary>
public class RemoteConfigSystem
{
    public static RemoteConfigSystem Instance { get; private set; } = new RemoteConfigSystem();


    public const string remoteKey_ObjectConfig = "object_config";
    public const string remoteKey_goals_main_Config = "goals_main_config";
    public const string remoteKey_battlepassConfig = "battlepass_config";

    public const string remoteKey_display_all_ios_offers = "display_all_ios_offers";
    public const string remoteKey_branchConfig = "activity_open_level";
    public const string remoteKey_pushGift = "push_gift";

    public const string remoteKey_reward_ads_times = "reward_ads_times";
    public const string remoteKey_ads_limit = "ads_limit";

    public const string remoteKey_si_app_open = "si_app_open";
    public const string remoteKey_si_complete_orders = "si_complete_orders";
    public const string remoteKey_si_enter_level = "si_enter_level";
    public const string remoteKey_si_exit_level = "si_exit_level";
    public const string remoteKey_si_finish_level = "si_finish_level";

    public const string remoteKey_LoopCD_ReduceTime = "loopcd_reducetime";
    public const string remoteKey_StarterPackage = "starter_package";
    public const string remoteKey_full_min_dur = "full_min_dur";//插屏广告最低间隔时间
    public const string remoteKey_First_daily_event = "first_daily_event";

    public int GetRemoteConfig_Int(string remoteKey)
    {
        return Ivy.RemoteConfig.RemoteConfigSystem.Instance.GetRemoteConfig_Int(remoteKey);
    }

    public string GetRemoteConfig_String(string remoteKey)
    {
#if UNITY_EDITOR
        string str = string.Empty;
        if (DebugSetting.CanUseDebugConfig(out var debugSO) && debugSO.DebugRemoteConfig)
        {
            string path = $"Assets/ConfigSO/EditorConfig/{remoteKey}.json";
            if (System.IO.File.Exists(System.IO.Path.Combine(Application.dataPath, $"ConfigSO/EditorConfig/{remoteKey}.json")))
            {
                TextAsset txt = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                str = txt != null ? txt.text : string.Empty;
                return str;
            }
        }
#endif
        return Ivy.RemoteConfig.RemoteConfigSystem.Instance.GetRemoteConfig_String(remoteKey);

    }

}
