using I2.Loc;
using System;

public class MyTimer
{
    /// <summary>
    /// 输入s，返回hh:mm:ss
    /// </summary>
    public static string ReturnTextUntilSecond_MaxShowTwo(int second)
    {
        try
        {
            if (second < 0)
                return 0.ToString();
            int _day = second / 86400;
            int _hour = second % 86400 / 3600;
            int _min = second % 3600 / 60;
            int _second = second % 60;
            if (_day > 0)
            {
                return string.Format(ScriptLocalization.Get("Obj/Time/Show1"), _day) + " " + string.Format(ScriptLocalization.Get("Obj/Time/Show2"), _hour);
            }
            else if (_hour > 0)
            {
                return string.Format(ScriptLocalization.Get("Obj/Time/Show2"), _hour) + " " + string.Format(ScriptLocalization.Get("Obj/Time/Show3"), _min);
            }
            else if (_min > 0)
            {
                return string.Format(ScriptLocalization.Get("Obj/Time/Show3"), _min) + " " + string.Format(ScriptLocalization.Get("Obj/Time/Show4"), _second);
            }
            else if (_second > 0)
            {
                return string.Format(ScriptLocalization.Get("Obj/Time/Show4"), _second);
            }
            else
            {
                return 0.ToString();
            }
        }
        catch (Exception e)
        {
            GameDebug.LogError(e);
            return string.Empty;
        }
    }
    /// <summary>
    /// 输入s，返回hh:mm
    /// </summary>
    public static string ReturnNumUntilMin(int second)
    {
        if (second < 0)
            return 0.ToString();
        string _hour = (second / 3600) >= 10 ? (second / 3600).ToString() : ("0" + (second / 3600).ToString());
        string _min = (second % 3600 / 60) >= 10 ? (second % 3600 / 60).ToString() : ("0" + (second % 3600 / 60).ToString());
        if (second / 3600 <= 0) return "00" + _min;
        else return _hour + ":" + _min;
    }
    
    /// <summary>
    /// 返回某天 某时 某分
    /// </summary>
    /// <returns></returns>
    public static string ReturnTextUntilMin(int second)
    {
        if (second < 0)
            return 0.ToString();
        int _day = second / 86400;
        int _hour = second % 86400 / 3600;
        int _min = second % 3600 / 60;
        if (_day > 0)
        {
            return string.Format(ScriptLocalization.Get("Obj/Time/Show1"), _day) + string.Format(ScriptLocalization.Get("Obj/Time/Show2"), _hour) + string.Format(ScriptLocalization.Get("Obj/Time/Show3"), _min);
        }
        else if (_hour > 0)
        {
            return string.Format(ScriptLocalization.Get("Obj/Time/Show2"), _hour) + string.Format(ScriptLocalization.Get("Obj/Time/Show3"), _min);
        }
        else if (_min > 0)
        {
            return string.Format(ScriptLocalization.Get("Obj/Time/Show3"), _min);
        }
        else
        {
            return 0.ToString();
        }
    }
    
}
