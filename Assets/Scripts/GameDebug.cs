using UnityEngine;
using System;
using System.Diagnostics;
using Ivy;

public class GameDebug
{
    [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
    public static void Log(object message)
    {
        LogUtils.Log(message);
    }

    [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
    public static void LogFormat(string format, params object[] args)
    {
        LogUtils.LogFormat(format, args);
    }

    [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
    public static void LogWarning(object message)
    {
        LogUtils.LogWarning(message);
    }

    [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
    public static void LogError(object message)
    {
        LogUtils.LogError(message);
    }

    [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
    public static void LogErrorFormat(string format, params object[] args)
    {
        LogUtils.LogErrorFormat(format, args);
    }

}

