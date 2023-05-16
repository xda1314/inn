// ILSpyBased#2
using System;

namespace BDUnity.Utils
{
    public class TimeUtil
    {
        public static DateTime UTC_BASE = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static double GetUTCTimestamp()
        {
            return DateTime.UtcNow.Subtract(TimeUtil.UTC_BASE).TotalSeconds;
        }

        public static double GetUTCTimestamp(DateTime dt)
        {
            dt = dt.ToUniversalTime();
            return dt.Subtract(TimeUtil.UTC_BASE).TotalSeconds;
        }

        public static long GetUTCTimestamp(bool second)
        {
            TimeSpan timeSpan = DateTime.UtcNow.Subtract(TimeUtil.UTC_BASE);
            if (second)
            {
                return Convert.ToInt64(timeSpan.TotalSeconds);
            }
            return Convert.ToInt64(timeSpan.TotalMilliseconds);
        }

        public static DateTime FromUTCTimestamp(double timestamp)
        {
            TimeSpan value = TimeSpan.FromSeconds(timestamp);
            return TimeUtil.UTC_BASE.Add(value);
        }

        public static DateTime GetDayStart(DateTime dataTime)
        {
            return new DateTime(dataTime.Year, dataTime.Month, dataTime.Day, 0, 0, 0, dataTime.Kind);
        }

        public static DateTime GetDayEnd(DateTime dataTime)
        {
            return new DateTime(dataTime.Year, dataTime.Month, dataTime.Day, 23, 59, 59, dataTime.Kind);
        }

        public static bool IsDayEnd(DateTime dataTime)
        {
            return dataTime.Hour == 23 && dataTime.Minute == 59 && dataTime.Second == 59;
        }

        public static int GetLocalTimeZone()
        {
            return TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;
        }

        public static int GetLocalTimeZoneSeconds()
        {
            return TimeUtil.GetLocalTimeZone() * 3600;
        }

        public static long GetFixUTCTimestamp(long checkTime, bool second)
        {
            long uTCTimestamp = TimeUtil.GetUTCTimestamp(second);
            return Math.Min(uTCTimestamp, checkTime);
        }
    }
}


