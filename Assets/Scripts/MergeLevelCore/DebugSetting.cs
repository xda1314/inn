using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ivy;

public class DebugSetting : MonoBehaviour
{
    private static SO_DebugConfig debugConfigSO;
    private static SO_DebugMap debugMapSO;

    private static bool IsInit = false;
    private static void Init()
    {
        if (IsInit)
        {
            return;
        }
        IsInit = true;
#if UNITY_EDITOR 
        debugConfigSO = UnityEditor.AssetDatabase.LoadAssetAtPath<SO_DebugConfig>("Assets/ScriptObjects/SO_DebugConfig.asset");
        debugMapSO = UnityEditor.AssetDatabase.LoadAssetAtPath<SO_DebugMap>("Assets/ScriptObjects/SO_DebugMap.asset");
#elif DEVELOPMENT_BUILD
        if (AssetSystem.Instance.ContainsKey("SO_DebugConfig"))
        {
            AssetSystem.Instance.LoadAsset<SO_DebugConfig>("SO_DebugConfig", (so) =>
            {
                debugConfigSO = so;
            });
        }
        if (AssetSystem.Instance.ContainsKey("SO_DebugMap"))
        {
            AssetSystem.Instance.LoadAsset<SO_DebugMap>("SO_DebugMap", (so) =>
            {
                debugMapSO = so;
            });
        }
#endif

    }

    /// <summary>
    /// 判断当前情况下，是否可以使用DebugConfigSO
    /// </summary>
    /// <returns></returns>
    public static bool CanUseDebugConfig(out SO_DebugConfig configSO)
    {
        Init();

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (debugConfigSO != null)
        {
            configSO = debugConfigSO;
            return true;
        }
#endif
        configSO = null;
        return false;
    }

    public static bool CanUseDebugMap(out SO_DebugMap configSO)
    {
        Init();

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (debugMapSO != null)
        {
            configSO = debugMapSO;
            return true;
        }
#endif
        configSO = null;
        return false;
    }

    [Obsolete]
    public static void LogError(string message)
    {
        LogUtils.LogErrorToSDK(message);
    }

    [Obsolete]
    public static void LogError(Exception e, string message)
    {
        LogUtils.LogErrorToSDK(message, e);
    }



}
