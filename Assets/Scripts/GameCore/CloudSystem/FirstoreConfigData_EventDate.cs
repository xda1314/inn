using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Globalization;

public static class FirstoreConfigData_EventDate
{
    public struct FirstoreEventDateConfig
    {
        public DateTimeOffset startDate;//开启时间
        public DateTimeOffset endDate;//结束时间
        public DateTimeOffset resultEndDate;//展示结束时间
        public int cycleDays;

        /// <summary>
        /// 是当前的活动
        /// </summary>
        public bool IsCurrentEvent
        {
            get
            {
                return endDate != DateTimeOffset.MinValue
                    && startDate != DateTimeOffset.MinValue
                    && resultEndDate != DateTimeOffset.MinValue
                    && startDate < endDate
                    && endDate <= resultEndDate
                    && TimeManager.ServerUtcNow() > startDate
                    && TimeManager.ServerUtcNow() < resultEndDate;
            }
        }

        /// <summary>
        /// 当前处于展示结果，给奖励阶段
        /// </summary>
        public bool IsCurrentEventShowResult
        {
            get
            {
                return endDate != DateTimeOffset.MinValue
                    && startDate != DateTimeOffset.MinValue
                    && resultEndDate != DateTimeOffset.MinValue
                    && endDate < resultEndDate
                    && TimeManager.ServerUtcNow() > endDate
                    && TimeManager.ServerUtcNow() < resultEndDate;
            }
        }

        /// <summary>
        /// 是未来将开启的活动
        /// </summary>
        public bool IsFutureEvent
        {
            get
            {
                return endDate != DateTimeOffset.MinValue
                    && startDate != DateTimeOffset.MinValue
                    && startDate < endDate
                    && TimeManager.ServerUtcNow() < startDate
                    && TimeManager.ServerUtcNow() < endDate;
            }
        }
    }

    public static bool hasGetEventDateSuccess { get; private set; } = false;

    public static FirstoreEventDateConfig rank_CurrentOrNextOpenDate { get; private set; }//排行榜当前活动时间，或是下次开启的活动时间
    private static List<FirstoreEventDateConfig> eventDateConfig_Rank = new List<FirstoreEventDateConfig>();


    public static void ParseConfigData_EventDate(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            return;
        }

        try
        {
            GameDebug.Log("[ParseConfigData_EventDate]" + json);

            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            if (dict == null)
            {
                return;
            }



            if (dict.TryGetValue("rank", out object rankDateObj))
            {
                var rankDict = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(rankDateObj.ToString());
                foreach (var rankDate in rankDict)
                {
                    int cycle = 0;
                    DateTimeOffset start = DateTimeOffset.MinValue;
                    DateTimeOffset end = DateTimeOffset.MinValue;
                    if (rankDate.TryGetValue("cycle", out object cycleObj))
                    {
                        int.TryParse(cycleObj.ToString(), out cycle);
                        if (cycle < 0)
                        {
                            cycle = 0;
                        }
                    }
                    if (rankDate.TryGetValue("start", out object startObj))
                    {
                        string str = startObj.ToString();
                        DateTimeOffset.TryParse(str, null, DateTimeStyles.AssumeUniversal, out start);
                    }
                    if (rankDate.TryGetValue("end", out object endObj))
                    {
                        string str = endObj.ToString();
                        DateTimeOffset.TryParse(str, null, DateTimeStyles.AssumeUniversal, out end);
                    }

                    if (start != DateTimeOffset.MinValue && start < end)
                    {
                        FirstoreEventDateConfig date2 = new FirstoreEventDateConfig()
                        {
                            cycleDays = cycle,
                            startDate = start,
                            endDate = end
                        };
                        eventDateConfig_Rank.Add(date2);
                    }
                }


                //计算当前或下一次的开启时间
                rank_CurrentOrNextOpenDate = GetCurrentOrNextOpenData(eventDateConfig_Rank);
            }


            hasGetEventDateSuccess = true;
        }
        catch (Exception e)
        {
            hasGetEventDateSuccess = false;
            Debug.LogError(e);
        }
    }


    private static FirstoreEventDateConfig GetCurrentOrNextOpenData(List<FirstoreEventDateConfig> list)
    {
        if (list != null)
        {
            foreach (var item in list)
            {

                DateTimeOffset serverNow = TimeManager.ServerUtcNow();
                DateTimeOffset start = item.startDate;
                DateTimeOffset end = item.endDate;
                DateTimeOffset showResult = item.endDate;
                int cycle = item.cycleDays;
                for (int i = 0; i < 1000; i++)
                {
                    if (serverNow < showResult)
                    {
                        FirstoreEventDateConfig firstoreEventDateConfig = new FirstoreEventDateConfig()
                        {
                            cycleDays = cycle,
                            startDate = start,
                            endDate = end,
                            resultEndDate = showResult
                        };
                        return firstoreEventDateConfig;
                    }

                    if (serverNow > showResult)
                    {
                        if (cycle > 0)
                        {
                            start = start.AddDays(cycle);
                            end = end.AddDays(cycle);
                            showResult = end;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        return new FirstoreEventDateConfig()
        {
            cycleDays = 0,
            startDate = DateTimeOffset.MinValue,
            endDate = DateTimeOffset.MinValue,
            resultEndDate = DateTimeOffset.MinValue
        };
    }

}
