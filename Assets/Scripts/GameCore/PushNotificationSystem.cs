using Ivy;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 手机推送消息通知系统
/// </summary>
public class PushNotificationSystem
{
    private const string key_EnergyFull = "key_EnergyFull"; //能量已满
    private const string key_ChargeOver = "key_ChargeOver";//充能完毕
    private const string key_OpenBox = "key_OpenBox";//宝箱打开倒计时
    private const string key_BranchStart = "key_BranchStart";//支线开启推送
    private const string key_BranchEnd = "key_BranchEnd";//支线结束推送
    private const string key_BranchEnd1 = "key_BranchEnd1";//支线结束推送
    private const string key_Daily1Open = "key_Daily1Open"; //每日活动1
    private const string key_Daily2Open = "key_Daily2Open"; //每日活动2
    private const string key_Daily3Open = "key_Daily3Open"; //每日活动3
    private const string key_Daily4Open = "key_Daily4Open"; //每日活动4
    private const string key_Daily5Open = "key_Daily5Open"; //每日活动5
    private const string key_Daily6Open = "key_Daily6Open"; //每日活动6
    private const string key_DailyEnd = "key_DailyEnd"; //活动结束
    public static PushNotificationSystem Instance { get; private set; } = new PushNotificationSystem();
    private const int maxPushCount = 2;

    private int pushCountToday = 0;
    private DateTimeOffset curTime;
    private Dictionary<string, DateTimeOffset> pushDataDic = new Dictionary<string, DateTimeOffset>();//存储功能类提示<key,time>
    public void InitSystem()
    {
        LoadData();
        //判断下线期间推送的数量
        foreach (var item in pushDataDic)
        {
            if ((item.Value - TimeManager.Instance.UtcNow()).TotalSeconds <= 0) 
            {
                pushCountToday++;
            }
            //取消推送
            RiseSdk.Instance.CancelLocalNotification(item.Key);
        }
        pushDataDic.Clear();
    }

    Dictionary<string, DateTimeOffset> allDic = new Dictionary<string, DateTimeOffset>();
    int leftPushCount;// //今天剩余推送次数
    public void PushNotification()
    {
        //关闭或暂停游戏时添加推送
        allDic.Clear();
        FindAllCanPush();
        leftPushCount = ReturnLeftPushCount();
        //Debug.LogError("leftPushCount" + leftPushCount);
        RefreshNeedPushData();
        TryPush();
        SaveData();
    }
    private void FindAllCanPush()
    {
        //体力到达上限时间
        int energyFullTime = Currencies.ReturnRecoveryEnergyTime();
        if (energyFullTime > 0)
        {
            allDic[key_EnergyFull] = TimeManager.Instance.UtcNow().AddSeconds(energyFullTime);
            //Debug.LogError(key_EnergyFull);
        }
        //所有容器充能完毕时间
        int chargeOverTime = MergeLevelManager.Instance.ReturnCurrentChargeOverTime();
        if (chargeOverTime > 0)
        {
            allDic[key_ChargeOver] = TimeManager.Instance.UtcNow().AddSeconds(chargeOverTime);
            //Debug.LogError(key_ChargeOver);
        }
        //宝箱打开时间
        int openBoxTime = MergeLevelManager.Instance.ReturnCurrentBoxOpenTime();
        if (openBoxTime > 0)
        {
            allDic[key_OpenBox] = TimeManager.Instance.UtcNow().AddSeconds(openBoxTime);
            //Debug.LogError(openBoxTime);
        }

        //支线活动推送
        if (BranchSystem.Instance.GetIsOpen()) 
        {
            allDic[key_BranchStart] = BranchSystem.Instance.Branch_EndTime.AddDays(1);
            int endtime = (int)(BranchSystem.Instance.Branch_EndTime - TimeManager.Instance.UtcNow()).TotalHours;
            if (endtime < 12 && endtime - 4 > 0)
            {
                allDic[key_BranchEnd] = TimeManager.Instance.UtcNow().AddHours(4);
            }
            if (endtime < 12 && endtime - 8 > 0)
            {
                allDic[key_BranchEnd1] = TimeManager.Instance.UtcNow().AddHours(8);
            }
        }

       

        //每日任务推送
        if (DailyActiveSystem.Instance.GetNoComplete()) 
        {
            int newDailyTime = (int)TimeManager.Instance.GetTomorrowRefreshTimeSpan().TotalHours;
           
            switch (DailyActiveSystem.Instance.Daily_Type)
            {
                case MergeLevelType.none:
                case MergeLevelType.daily6:
                    allDic[key_Daily1Open] = TimeManager.Instance.UtcNow().AddHours(newDailyTime);
                    break;
                case MergeLevelType.daily1:
                    allDic[key_Daily2Open] = TimeManager.Instance.UtcNow().AddHours(newDailyTime);
                    break;
                case MergeLevelType.daily2:
                    allDic[key_Daily3Open] = TimeManager.Instance.UtcNow().AddHours(newDailyTime);
                    break;
                case MergeLevelType.daily3:
                    allDic[key_Daily4Open] = TimeManager.Instance.UtcNow().AddHours(newDailyTime);
                    break;
                case MergeLevelType.daily4:
                        allDic[key_Daily5Open] = TimeManager.Instance.UtcNow().AddHours(newDailyTime);
                    break;
                case MergeLevelType.daily5:
                    allDic[key_Daily6Open] = TimeManager.Instance.UtcNow().AddHours(newDailyTime);
                    break;
                default:
                    break;
            }
            if (newDailyTime > 2) 
            {
                int delay_Time = newDailyTime - 2;
                allDic[key_DailyEnd] = TimeManager.Instance.UtcNow().AddHours(delay_Time);
            }     
        }
    }
    private int ReturnLeftPushCount()
    {
        if (ExtensionTool.IsDateToday(curTime, TimeManager.Instance.UtcNow()))
        {
            return maxPushCount - pushCountToday;
        }
        else
        {
            return maxPushCount;
        }
    }
    private void RefreshNeedPushData() 
    {
        //推送相关
        if (leftPushCount == 0)
        {
            //已达到最大推送次数,不再推送
        }
        else if (allDic.Count == leftPushCount)
        {
            //数量正好，全部推送
            foreach (var item in allDic)
            {
                pushDataDic[item.Key] = item.Value;
            }
        }
        else if (allDic.Count < leftPushCount)
        {
            //数量不够,需要推送时补齐
            foreach (var item in allDic)
            {
                pushDataDic[item.Key] = item.Value;
            }
        }
        else if (allDic.Count > leftPushCount)
        {
            //随机两个推送
            List<string> randomKeys = new List<string>();//需要推送的key值
            List<string> allKeyList = new List<string>();
            foreach (var item in allDic.Keys)
            {
                allKeyList.Add(item);
            }
            for (int i = 0; i < leftPushCount; i++)
            {
                int seed = UnityEngine.Random.Range(0, allKeyList.Count);
                randomKeys.Add(allKeyList[seed]);
                allKeyList.RemoveAt(seed);
            }
            for (int i = 0; i < randomKeys.Count; i++)
            {
                if (allDic.ContainsKey(randomKeys[i]))
                {
                    pushDataDic[randomKeys[i]] = allDic[randomKeys[i]];
                }
            }
        }
        //Debug.LogError("pushDataDic.count" + pushDataDic.Count);
    }

    private void Push(string strKey, string pushStrContent,string contentKey,  int delayTime) 
    {
        string str_Title = I2.Loc.ScriptLocalization.Get("Title/GameName");
        if (str_Title == "Title/GameName") 
        {
            GameDebug.LogError("找不到Title/GameName对应多语言");
            return;
        }
        if(contentKey == pushStrContent) 
        {
            GameDebug.LogError("找不到"+ contentKey + "对应多语言");
            return;
        } 
        RiseSdk.Instance.PushLocalNotification(strKey, str_Title, pushStrContent, delayTime, 0, false, string.Empty, string.Empty);
    }
    private void TryPush() 
    {
#if UNITY_EDITOR
        //pushDataDic[key_EnergyFull] = TimeManager.Instance.UtcNow().AddSeconds(0);
        //pushDataDic[key_ChargeOver] = TimeManager.Instance.UtcNow().AddSeconds(0);
        //pushDataDic[key_OpenBox] = TimeManager.Instance.UtcNow().AddSeconds(0);
        //pushDataDic[key_BranchStart] = TimeManager.Instance.UtcNow().AddSeconds(0);
        //pushDataDic[key_BranchEnd] = TimeManager.Instance.UtcNow().AddSeconds(0);
        //pushDataDic[key_BranchEnd1] = TimeManager.Instance.UtcNow().AddSeconds(0);
        //pushDataDic[key_Daily1Open] = TimeManager.Instance.UtcNow().AddSeconds(0);
        //pushDataDic[key_Daily2Open] = TimeManager.Instance.UtcNow().AddSeconds(0);
        //pushDataDic[key_Daily3Open] = TimeManager.Instance.UtcNow().AddSeconds(0);
        //pushDataDic[key_Daily4Open] = TimeManager.Instance.UtcNow().AddSeconds(0);
        //pushDataDic[key_Daily5Open] = TimeManager.Instance.UtcNow().AddSeconds(0);
        //pushDataDic[key_DailyEnd] = TimeManager.Instance.UtcNow().AddSeconds(0);
        //pushDataDic[key_Daily6Open] = TimeManager.Instance.UtcNow().AddSeconds(0);
#endif
        //先推功能类提示
        foreach (var item in pushDataDic)
        {
            string pushStr;
            int delayTime = (int)(item.Value - TimeManager.Instance.UtcNow()).TotalSeconds;
            switch (item.Key) 
            {
                case key_EnergyFull:
                    int seed1 = UnityEngine.Random.Range(1, 3);
                    pushStr = I2.Loc.ScriptLocalization.Get("Obj/Notice/Energy/Text" + seed1.ToString());
                    Push(key_EnergyFull,pushStr, "Obj/Notice/Energy/Text" + seed1.ToString(), delayTime);
                    break;
                case key_ChargeOver:
                    int seed2 = UnityEngine.Random.Range(1, 3);
                    pushStr = I2.Loc.ScriptLocalization.Get("Obj/Notice/Container/Text" + seed2.ToString());
                    Push(key_ChargeOver, pushStr, "Obj/Notice/Container/Text" + seed2.ToString(), delayTime);
                    break;
                case key_OpenBox:
                    int seed3 = UnityEngine.Random.Range(1, 3);
                    pushStr = I2.Loc.ScriptLocalization.Get("Obj/Notice/BoxOpen/Text" + seed3.ToString());
                    Push(key_OpenBox,pushStr, "Obj/Notice/BoxOpen/Text" + seed3.ToString(), delayTime);
                    break;
                case key_BranchStart:
                    int seed4 = UnityEngine.Random.Range(1, 3);
                    pushStr = I2.Loc.ScriptLocalization.Get("Obj/Notice/SpurLineNew/Text" + seed4.ToString());
                    Push(key_BranchStart,pushStr, "Obj/Notice/SpurLineNew/Text" + seed4.ToString(), delayTime);
                    break;
                case key_BranchEnd:
                case key_BranchEnd1:
                    int seed5 = UnityEngine.Random.Range(1, 3);
                    pushStr = I2.Loc.ScriptLocalization.Get("Obj/Notice/SpurLineEnd/Text" + seed5.ToString());
                    Push(key_BranchEnd,pushStr, "Obj/Notice/SpurLineEnd/Text" + seed5.ToString(), delayTime);
                    break;
                case key_Daily1Open:
                    pushStr = I2.Loc.ScriptLocalization.Get("Obj/Notice/DailyEvent1/Text");
                    Push(key_Daily1Open,pushStr, "Obj/Notice/DailyEvent1/Text", delayTime);
                    break;
                case key_Daily2Open:
                    pushStr = I2.Loc.ScriptLocalization.Get("Obj/Notice/DailyEvent2/Text");
                    Push(key_Daily2Open,pushStr, "Obj/Notice/DailyEvent2/Text", delayTime);
                    break; 
                case key_Daily3Open:
                    pushStr = I2.Loc.ScriptLocalization.Get("Obj/Notice/DailyEvent3/Text");
                    Push(key_Daily3Open,pushStr, "Obj/Notice/DailyEvent3/Text", delayTime);
                    break;
                case key_Daily4Open:
                    pushStr = I2.Loc.ScriptLocalization.Get("Obj/Notice/DailyEvent4/Text");
                    Push(key_Daily4Open,pushStr, "Obj/Notice/DailyEvent4/Text", delayTime);
                    break;
                case key_Daily5Open:    
                    pushStr = I2.Loc.ScriptLocalization.Get("Obj/Notice/DailyEvent5/Text");
                    Push(key_Daily5Open,pushStr, "Obj/Notice/DailyEvent5/Text", delayTime);
                    break;
                case key_Daily6Open:
                    pushStr = I2.Loc.ScriptLocalization.Get("Obj/Notice/DailyEvent6/Text");
                    Push(key_Daily6Open, pushStr, "Obj/Notice/DailyEvent6/Text", delayTime);
                    break;
                case key_DailyEnd:
                    pushStr = I2.Loc.ScriptLocalization.Get("Obj/Notice/DailyEventEnd/Text");
                    Push(key_DailyEnd,pushStr, "Obj/Notice/DailyEventEnd/Text", delayTime);
                    break;
                default:
                    GameDebug.LogError("key error:" + item.Key);
                    break;
            }
            //Debug.LogError("key：" + item.Key + "    pushContent:" + pushStr);
            //Debug.LogError("开始推送时间：" + item.Value + "   延时:" + delayTime);
        }

    }





    #region 存档
    private void LoadData() 
    {
        try
        {
            pushCountToday = SaveUtils.GetInt(Consts.SaveKey_Push_Count);
            string curTimeStr = SaveUtils.GetString(Consts.SaveKey_Push_CurTime);
            if (!string.IsNullOrEmpty(curTimeStr) && DateTimeOffset.TryParse(curTimeStr, out DateTimeOffset timeOffset))  
            {
                curTime = timeOffset;
            }
            string keysStr = SaveUtils.GetString(Consts.SaveKey_Push_KeyList);
            if (!string.IsNullOrEmpty(keysStr)) 
            {
                Dictionary<string, object> saveDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(keysStr);
                foreach (var item in saveDic)
                {
                    if (DateTimeOffset.TryParse(item.Value.ToString(), out DateTimeOffset dateTimeOffset)) 
                    {
                        pushDataDic[item.Key] = dateTimeOffset;
                    }
                }
            }
        }
        catch (Exception e) 
        {
            GameDebug.LogError(e);
        }
    }
    private void SaveData() 
    {
        try
        {
            SaveUtils.SetInt(Consts.SaveKey_Push_Count, pushCountToday);
            if (!SaveUtils.HasKey(Consts.SaveKey_Push_CurTime))
            {
                SaveUtils.SetString(Consts.SaveKey_Push_CurTime, TimeManager.Instance.UtcNow().ToString());
            }
            else 
            {
                SaveUtils.SetString(Consts.SaveKey_Push_CurTime, curTime.ToString());
            }
            Dictionary<string, object> saveDic = new Dictionary<string, object>();
            foreach (var item in pushDataDic)
            {
                saveDic.Add(item.Key, item.Value.ToString());
            }
            string keysStr = JsonConvert.SerializeObject(saveDic);
            SaveUtils.SetString(Consts.SaveKey_Push_KeyList, keysStr);
        }
        catch(Exception e) 
        {
            GameDebug.LogError(e);
        }
    }
    #endregion
}
