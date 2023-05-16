using Ivy;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AnalyticsUtil
{
    //Dictionary转化为string
    private static string ConvertKeyValueData(Dictionary<string, string> dictionary)
    {
        if (dictionary == null || dictionary.Count == 0)
        {
            return string.Empty;
        }
        StringBuilder str = new StringBuilder();
        foreach (string key in dictionary.Keys)
        {
            string keyValue = dictionary[key];
            if (keyValue.IndexOf(',') != -1)
            {
                keyValue = keyValue.Replace(',', '-');
            }
            str.Append(key);
            str.Append(",");
            str.Append(keyValue);
            str.Append(",");
        }
        return str.ToString();
    }

    /// <summary>
    /// 特殊事件打点
    /// </summary>
    /// <param name="type"></param>
    /// <param name="dictionary"></param>
    public static void TrackEvent(string type, Dictionary<string, string> dictionary)
    {
        if (string.IsNullOrEmpty(type))
        {
            return;
        }
        string keyValue = string.Empty;
        if (dictionary != null)
        {
            keyValue = ConvertKeyValueData(dictionary);
        }
        type = type.Trim();
        RiseSdk.Instance.TrackEvent(type, keyValue);
    }
    public static void TrackEvent(string type, string str = "")
    {
        if (string.IsNullOrEmpty(type))
        {
            return;
        }
        RiseSdk.Instance.TrackEvent(type, str);
    }

    /// <summary>
    /// 统计数据。
    /// </summary>
    /// <param name="category">需要统计的数据分类名称</param>
    /// <param name="action">需要统计的数据属性名称</param>
    /// <param name="label">数据的属性值</param>
    /// <param name="value">一般传0</param>
    public static void TrackEvent(string category, string action, string label, int value)
    {
        RiseSdk.Instance.TrackEvent(category, action, label, value);
    }

    /// <summary>
    /// 主线流程关键点
    /// </summary>
    /// <param name="label"></param>
    /// <param name="value"></param>
    public static void trackMainLine(string label, int value)
    {
        try
        {
            GameDebug.Log($"[trackMainLine] lable:{label},value:{value}");
            RiseSdk.Instance.TrackMainLine(label, value);
        }
        catch
        {
        }
    }

    /// <summary>
    /// 记录主线任务进度
    /// </summary>
    public static void recordCoreAction(string label, int inc)
    {
        try
        {
            GameDebug.Log($"[recordCoreAction] lable:{label},inc:{inc}");
            RiseSdk.Instance.RecordCoreAction(label, inc);
        }
        catch
        {
        }
    }

    /// <summary>
    /// 提交主线任务进度
    /// </summary>
    public static void commitCoreAction(string label)
    {
        try
        {
            GameDebug.Log($"[commitCoreAction] lable:{label}");
            RiseSdk.Instance.CommitCoreAction(label);
        }
        catch
        {
        }
    }
    /// <summary>
    /// 活动开启
    /// </summary>
    public static void trackActivityStart(string label, string catalog)
    {
        try
        {
            GameDebug.Log($"[trackActivityStart] lable:{label},catalog:{catalog}");
            RiseSdk.Instance.TrackActivityStart(label, catalog);
        }
        catch
        {
        }
    }

    /// <summary>
    /// 活动完成进度
    /// </summary>
    public static void trackActivityStep(string label, int step)
    {
        try
        {
            GameDebug.Log($"[trackActivityStep] lable:{label},step:{step}");
            RiseSdk.Instance.TrackActivityStep(label, step);
        }
        catch
        {
        }
    }

    /// <summary>
    /// 活动全部完成
    /// </summary>
    /// <param name="label">活动id</param>
    public static void trackActivityEnd(string label)
    {
        try
        {
            GameDebug.Log($"[trackActivityEnd] lable:{label}");
            RiseSdk.Instance.TrackActivityEnd(label);
        }
        catch
        {
        }
    }

    /// <summary>
    /// 活动内的打点
    /// </summary>
    /// <param name="label">活动id</param>
    /// <param name="catalog">打点位置名称</param>
    /// <param name="value">数量或计费点金额</param>
    /// <param name="iap">是否是计费点</param>
    public static void trackActivityEvent(string label, string catalog, float value, bool iap)
    {
        try
        {
            GameDebug.Log($"[trackActivityEvent] lable:{label},catalog:{catalog},value:{value},iap:{iap}");
            if (iap && value <= 0)
            {
                GameDebug.LogError($"[trackActivityEvent] cost <= 0");
                return;
            }
            RiseSdk.Instance.TrackActivityEvent(label, catalog, value, iap);
        }
        catch
        {
        }
    }

    /// <summary>
    /// 消耗货币
    /// </summary>
    /// <param name="virtualCurrencyName">货币名称</param>
    /// <param name="itemid">消耗点</param>
    /// <param name="value"></param>
    public static void spendVirtualCurrency(string virtualCurrencyName, string itemid, int value, int currentValue)
    {
        try
        {
            GameDebug.Log($"[spendVirtualCurrency] virtualCurrencyName:{virtualCurrencyName},itemid:{itemid},value:{value}");
            RiseSdk.Instance.SpendVirtualCurrency(virtualCurrencyName, itemid, value, currentValue);
        }
        catch
        {
        }
    }

    /// <summary>
    /// 增加货币
    /// </summary>
    /// <param name="virtualCurrencyName"></param>
    /// <param name="itemid">来源</param>
    /// <param name="value"></param>
    public static void earnVirtualCurrency(string virtualCurrencyName, string itemid, int value, int currentValue)
    {
        try
        {
            GameDebug.Log($"[earnVirtualCurrency] virtualCurrencyName:{virtualCurrencyName},itemid:{itemid},value:{value}");
            RiseSdk.Instance.EarnVirtualCurrency(virtualCurrencyName, itemid, value, currentValue);
        }
        catch
        {
        }
    }

    /// <summary>
    /// 进入关卡
    /// </summary>
    /// <param name="levelName">关卡名称</param>
    public static void trackScreenStart(string levelName)
    {
        try
        {
            GameDebug.Log($"[trackScreenStart] levelName:{levelName}");
            RiseSdk.Instance.TrackScreenStart(levelName);
        }
        catch
        {
        }
    }

    /// <summary>
    /// 退出关卡
    /// </summary>
    /// <param name="levelName"></param>
    public static void trackScreenEnd(string levelName)
    {
        try
        {
            GameDebug.Log($"[trackScreenEnd] levelName:{levelName}");
            RiseSdk.Instance.TrackScreenEnd(levelName);
        }
        catch
        {
        }
    }

    /// <summary>
    /// 教程打点
    /// </summary>
    /// <param name="stepId"></param>
    /// <param name="stepName"></param>
    public static void TrackRetentionStep(int stepId, string stepName)
    {
        RiseSdk.Instance.TrackRetentionStep(stepId, stepName);
    }

}
