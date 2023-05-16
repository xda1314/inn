using BDUnity;
using BDUnity.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using ivy.game;
using Ivy;

public class TimeManager : SingletonMono<TimeManager>
{
    public static string LocalCrossDayEvent = "TimeManagerLocalCrossDayEvent";

    public static int HourOfCossDay = 3;

    private float _lastTime;

    private long _lastLocalRefreshTime;

    private float requestServerTimeLastTime;

    private float requestServerTimeInterval = 10f;

    private bool _isConnectActive;

    private bool _serverTimeValid;

    public float checkInterval = 5f;

    private long _serverTime = -1L;

    private float _serverTimeUpdateTime;

    private float _checkTimeCount;

    public bool IsConnectActive
    {
        get
        {
            return this._isConnectActive;
        }
    }

    public bool ServerTimeValid
    {
        get
        {
            return this._serverTimeValid;
        }
    }

    public long ServerTime
    {
        get
        {
            if (this._serverTime == -1)
            {
                return -1L;
            }
            return this._serverTime + (int)(Time.realtimeSinceStartup - this._serverTimeUpdateTime);
        }
    }

    public DateTime ServerDateTime
    {
        get
        {
            return TimeUtil.FromUTCTimestamp((double)this.ServerTime).ToLocalTime();
        }
    }

    public static bool IsCrossDayLocal(long lastTime, bool couldBackCross = false)
    {
        DateTime now = DateTime.Now;
        DateTime lastDate = TimeUtil.FromUTCTimestamp((double)lastTime).ToLocalTime();
        return TimeManager.IsCrossDay(now, lastDate, couldBackCross);
    }

    public static bool IsCrossDayServer(long lastTime, bool couldBackCross = false)
    {
        if (!SingletonMono<TimeManager>.Instance.ServerTimeValid)
        {
            return false;
        }
        return TimeManager.IsCrossDay(SingletonMono<TimeManager>.Instance.ServerTime, lastTime, TimeManager.HourOfCossDay, couldBackCross);
    }

    public static bool IsCrossDay(long currStamp, long lastStamp, bool couldBackCross = false)
    {
        return TimeManager.IsCrossDay(currStamp, lastStamp, TimeManager.HourOfCossDay, couldBackCross);
    }

    public static bool IsCrossDay(DateTime currDate, DateTime lastDate, bool couldBackCross = false)
    {
        return TimeManager.IsCrossDay(currDate, lastDate, TimeManager.HourOfCossDay, couldBackCross);
    }

    public static bool IsCrossDay(long currStamp, long lastStamp, int hourOfCossDay, bool couldBackCross = false)
    {
        DateTime currDate = TimeUtil.FromUTCTimestamp((double)currStamp).ToLocalTime();
        DateTime lastDate = TimeUtil.FromUTCTimestamp((double)lastStamp).ToLocalTime();
        return TimeManager.IsCrossDay(currDate, lastDate, hourOfCossDay, couldBackCross);
    }

    public static bool IsCrossDay(DateTime currDate, DateTime lastDate, int hourOfCossDay, bool couldBackCross = false)
    {
        int num = lastDate.Year * 365 + lastDate.DayOfYear;
        if (lastDate.Hour >= hourOfCossDay)
        {
            num++;
        }
        int num2 = currDate.Year * 365 + currDate.DayOfYear;
        if (currDate.Hour >= hourOfCossDay)
        {
            num2++;
        }
        if (couldBackCross)
        {
            return num2 != num;
        }
        return num2 > num;
    }

    public void Init()
    {
        TryExcuteWithServerUtc(null);
    }

    private void Awake()
    {
        this.UpdateInfo();
        this._lastLocalRefreshTime = TimeUtil.GetUTCTimestamp(true);
    }

    public void UpdateInfo()
    {
    }

    private void Update()
    {
        if (this._checkTimeCount > 0f)
        {
            this._checkTimeCount -= Time.deltaTime;
            if (this._checkTimeCount <= 0f)
            {
                this._checkTimeCount = 0f;
                this.UpdateInfo();
            }
        }
        this.LocalCrossDayUpdate();
    }

    private void LocalCrossDayUpdate()
    {
        if (Time.deltaTime > 0f)
        {
            this._lastTime += Time.deltaTime;
        }
        if (this._lastTime > this.checkInterval)
        {
            if (TimeManager.IsCrossDayLocal(this._lastLocalRefreshTime, false))
            {
                EventManager.ExecuteEvent(TimeManager.LocalCrossDayEvent);
            }
            this._lastTime = 0f;
            this._lastLocalRefreshTime = TimeUtil.GetUTCTimestamp(true);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            this.UpdateInfo();
        }
    }







    /// <summary>
    /// 是否已经获取服务器时间
    /// </summary>
    public static bool IsGetServerUtcSuccess { get; private set; }
    private static DateTimeOffset serverUtc_WhenGetServer;
    private static float realtimeSinceStart_WhenGetServer;
    private static DateTimeOffset tempDateTimeOffsetNow;

    /// <summary>
    /// 现在的服务器Utc时间
    /// </summary>
    public static DateTimeOffset ServerUtcNow()
    {
        if (IsGetServerUtcSuccess)
        {
            tempDateTimeOffsetNow = serverUtc_WhenGetServer.AddSeconds(Time.realtimeSinceStartup - realtimeSinceStart_WhenGetServer);
        }
        else
        {
            GameDebug.Log("未获取服务器时间");
            tempDateTimeOffsetNow = Instance.UtcNow();
        }
        return tempDateTimeOffsetNow;
    }

    /// <summary>
    /// 尝试以获取的服务器时间运行
    /// </summary>
    public async void TryExcuteWithServerUtc(Action successedCB, Action failedCB = null, int waitMilliSecond = 0)
    {
        if (waitMilliSecond > 0)
        {
            await Task.Delay(waitMilliSecond);
        }

        if (successedCB == null)
        {
            CloudFunction_GetServerUtc(null, null);
            return;
        }

        if (IsGetServerUtcSuccess)
        {
            successedCB?.Invoke();

            if ((ServerUtcNow() - serverUtc_WhenGetServer).TotalHours > 0.5f)
            {
                CloudFunction_GetServerUtc(null, null);
            }
            return;
        }
        CloudFunction_GetServerUtc(successedCB, failedCB);
    }

    private List<Action> onCloudFunction_GetServerUtcSuccessCBList = new List<Action>();
    private List<Action> onCloudFunction_GetServerUtcFailCBList = new List<Action>();
#if UNITY_EDITOR
    private async void CloudFunction_GetServerUtc(Action successedCB, Action failedCB)
#else
    private void CloudFunction_GetServerUtc(Action successedCB, Action failedCB)
#endif
    {
        if (successedCB != null)
            onCloudFunction_GetServerUtcSuccessCBList.Add(successedCB);
        if (failedCB != null)
            onCloudFunction_GetServerUtcFailCBList.Add(failedCB);

#if UNITY_EDITOR
        //模拟网络延迟
        DateTimeOffset date = DateTimeOffset.UtcNow;
        if (DebugSetting.CanUseDebugConfig(out var debugSO))
        {
            if (debugSO.DebugNotGetServerTime)
            {
                return;
            }

            await System.Threading.Tasks.Task.Delay(debugSO.DebugGetServerTimeDelay > 0 ? debugSO.DebugGetServerTimeDelay : 500);
            if (debugSO.DebugSetGetServer)
            {
                if (!DateTimeOffset.TryParse(debugSO.DebugSetServerDate, null, System.Globalization.DateTimeStyles.AssumeUniversal, out date))
                {
                    GameDebug.LogError("模拟设置时间格式错误! 示例：" + DateTimeOffset.UtcNow.ToString());
                    date = DateTimeOffset.UtcNow;
                }
            }
        }

        GameDebug.Log("现在模拟的服务器时间是：" + date.ToString());
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict.Add("result", "ok");
        dict.Add("value", date.ToUnixTimeMilliseconds().ToString());
        TimeManager.Instance.ParseServerUtc(JsonConvert.SerializeObject(dict));
        return;
#endif
        AmazonawsConnect.CallLambdaFunction("date", null, (str) =>
        {
            TimeManager.Instance.ParseServerUtc(str);
        });
    }



    private DateTimeOffset tomorrowUtc;
    public DateTimeOffset TomorrowUtc()
    {
        tomorrowUtc = UtcNow().AddDays(1);
        return new DateTimeOffset(tomorrowUtc.Year, tomorrowUtc.Month, tomorrowUtc.Day, 0, 0, 0, TimeSpan.Zero);
    }

    /// <summary>
    /// 与明天的时间差
    /// </summary>
    /// <returns></returns>
    public TimeSpan GetTomorrowRefreshTimeSpan()
    {
        return TomorrowUtc() - UtcNow();
    }

    /// <summary>
    /// 获取本地当前时间
    /// </summary>
    /// <returns></returns>
    public DateTimeOffset UtcNow()
    {
#if UNITY_EDITOR
        if (DebugSetting.CanUseDebugConfig(out var debugSO) && debugSO.DebugSetLocal)
        {
            DateTimeOffset date = DateTimeOffset.UtcNow;
            if (!DateTimeOffset.TryParse(debugSO.DebugSetLocalDate, null, System.Globalization.DateTimeStyles.AssumeUniversal, out date))
            {
                GameDebug.LogError("模拟设置时间格式错误! 示例：" + DateTimeOffset.UtcNow.ToString());
            }
            else
            {
                return date;
            }
        }
#endif
        return DateTimeOffset.UtcNow;
    }


    private void ParseServerUtc(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            return;
        }

        try
        {
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            if (dict == null || !dict.TryGetValue("value", out object valueTemp))
            {
                return;
            }

            if (long.TryParse(valueTemp.ToString(), out long timestamp))
            {
                IsGetServerUtcSuccess = true;
                realtimeSinceStart_WhenGetServer = Time.realtimeSinceStartup;
                serverUtc_WhenGetServer = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).UtcDateTime;

                for (int i = onCloudFunction_GetServerUtcSuccessCBList.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        onCloudFunction_GetServerUtcSuccessCBList[i]?.Invoke();
                        onCloudFunction_GetServerUtcSuccessCBList.RemoveAt(i);
                    }
                    catch (Exception e)
                    {
                        LogUtils.LogErrorToSDK("[ParseServerUtc] successCB error!" + e);
                    }
                }
                onCloudFunction_GetServerUtcFailCBList.Clear();
                return;
            }
            else
            {
                for (int i = onCloudFunction_GetServerUtcFailCBList.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        onCloudFunction_GetServerUtcFailCBList[i]?.Invoke();
                        onCloudFunction_GetServerUtcFailCBList.RemoveAt(i);
                    }
                    catch (Exception e)
                    {
                        LogUtils.LogErrorToSDK("[ParseServerUtc] FailCB error!" + e);
                    }
                }
                onCloudFunction_GetServerUtcSuccessCBList.Clear();
            }
        }
        catch (Exception ee)
        {
            for (int i = onCloudFunction_GetServerUtcFailCBList.Count - 1; i >= 0; i--)
            {
                try
                {
                    onCloudFunction_GetServerUtcFailCBList[i]?.Invoke();
                    onCloudFunction_GetServerUtcFailCBList.RemoveAt(i);
                }
                catch (Exception e)
                {
                    LogUtils.LogErrorToSDK("[ParseServerUtc] FailCB error!" + e);
                }
            }
            onCloudFunction_GetServerUtcSuccessCBList.Clear();
            LogUtils.LogErrorToSDK("[ParseServerUtc]  error!" + ee);
        }

    }






}
