using BDUnity.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteGiftSystem
{
    public static RemoteGiftSystem Instance { get; private set; } = new RemoteGiftSystem();

    #region 远程控制数据
    public bool Remote_IsOpen { get; private set; }
    public DateTimeOffset Remote_StartTime { get; private set; }
    public DateTimeOffset Remote_EndTime { get; private set; }
    public List<MergeRewardItem> Remote_Rewards { get; private set; } = new List<MergeRewardItem>();
    #endregion

    public void InitRemoteGiftSystem()
    {
        LoadRemoteConfig();
    }

    public bool GetIsPop()
    {
        TimeSpan timeSpan = Remote_EndTime - DateTimeOffset.UtcNow;
        if (timeSpan.TotalSeconds > 0 && Remote_IsOpen)
            return true;
        return false;
    }

    //读取远程数据
    private void LoadRemoteConfig()
    {
        try
        {
#if UNITY_EDITOR
            string remoteStr = "{\"show\": true,\"start_timeS\" : \"2022-09-12 00:00:00\",\"end_timeS\" : \"2023-9-18 23:59:59\",\"items\": [{\"name\":\"gems\",\"count\":300},{\"name\":\"Coins\",\"count\":1000},{\"name\":\"Energy\",\"count\":100}]}";
#else
            string remoteStr = RemoteConfigSystem.Instance.GetRemoteConfig_String(RemoteConfigSystem.remoteKey_pushGift);
#endif
            if (string.IsNullOrEmpty(remoteStr))
                return;
            var cJson = CustomJSON.Deserialize(remoteStr, true);
            if (cJson.isDict)
            {
                var dict = cJson.dict;
                Remote_IsOpen = false;
                if (dict.TryGetValue("show", out CustomJSONObject show_Json))
                {
                    var showStr = show_Json.ToString();
                    if (!string.IsNullOrEmpty(showStr))
                        Remote_IsOpen = showStr.Equals("true", StringComparison.OrdinalIgnoreCase);
                }
                if (!Remote_IsOpen)
                    return;

                if (dict.TryGetValue("start_timeS", out CustomJSONObject start_Json))
                {
                    if (DateTimeOffset.TryParse(start_Json.ToString(), null, System.Globalization.DateTimeStyles.AssumeUniversal, out var date))
                        Remote_StartTime = date;
                }
                if (dict.TryGetValue("end_timeS", out CustomJSONObject end_Json))
                {
                    if (DateTimeOffset.TryParse(end_Json.ToString(), null, System.Globalization.DateTimeStyles.AssumeUniversal, out var date))
                        Remote_EndTime = date;
                }

                if (dict.TryGetValue("items", out CustomJSONObject items_Json))
                {
                    if (items_Json.isList)
                    {
                        var itemList = items_Json.list;
                        foreach (var itemjson in itemList)
                        {
                            if (itemjson.isDict)
                            {
                                var iDict = itemjson.dict;
                                if (iDict.TryGetValue("name", out CustomJSONObject name_Json)
                                    && iDict.TryGetValue("count", out CustomJSONObject count_Json))
                                {
                                    var nameStr = name_Json.ToString();
                                    if (!string.IsNullOrEmpty(nameStr) && int.TryParse(count_Json.ToString(), out var count))
                                        Remote_Rewards.Add(new MergeRewardItem { name = nameStr, num = count });
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            DebugSetting.LogError(e, "[LoadRemoteGiftConfig]");
        }
    }
}
