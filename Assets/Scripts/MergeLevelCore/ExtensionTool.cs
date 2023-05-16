using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.U2D;
using ivy.game;
using Ivy;

public static class ExtensionTool
{
    public static void ForceRebuildLayoutImmediate(this LayoutGroup layout)
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout.GetComponent<RectTransform>());
    }

    public static string GenerateUniqueID()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() + Guid.NewGuid().ToString("N");
    }

    public static string GetVersionInfo()
    {
#if UNITY_EDITOR
        return Application.platform.ToString() + " v" + Application.version;
#endif
        return Application.platform.ToString() + " v" + RiseSdk.Instance.GetConfig(RiseSdk.CONFIG_KEY_VERSION_NAME);
    }

    public static Sprite Texture2DToSprite(this Texture2D texture2D)
    {
        var rect = new Rect(0, 0, texture2D.width, texture2D.height);
        var pivot = Vector2.one * 0.5f;
        var newSprite = Sprite.Create(texture2D, rect, pivot);
        return newSprite;
    }

    public static string GetString(this Dictionary<string, object> dict, string key)
    {
        try
        {
            if (dict == null || dict.Count <= 0)
            {
                return string.Empty;
            }
            if (dict.TryGetValue(key, out object value) && value != null)
            {
                return value.ToString();
            }
            return string.Empty;
        }
        catch (Exception e)
        {
            GameDebug.LogError(e);
            return string.Empty;
        }
    }

    public static bool IsNumeric(this string str)
    {
        return Regex.IsMatch(str, @"^[0-9]+$");
    }

    public static bool TryParseMergeRewardItem(string rewardStr, out MergeRewardItem rewardItem)
    {
        if (string.IsNullOrEmpty(rewardStr))
        {
            rewardItem = default;
            return false;
        }

        string[] array = rewardStr.Split(',');
        if (array != null && array.Length == 2
            && !string.IsNullOrEmpty(array[0])
            && !string.IsNullOrEmpty(array[1]))
        {
            string nameTemp = array[0];
            if (int.TryParse(array[1], out int numTemp))
            {
                rewardItem = new MergeRewardItem()
                {
                    name = nameTemp,
                    num = numTemp
                };
                return true;
            }
        }

        rewardItem = default;
        return false;
    }

    // 解析pool
    public static bool TryParseMergeRewardItemPool(string poolID, string rewardListStr, out MergeRewardItemPool rewardItemPool)
    {
        rewardItemPool = new MergeRewardItemPool(poolID);
        List<string> itemNameList = new List<string>();
        if (!string.IsNullOrEmpty(rewardListStr))
        {
            string[] array = rewardListStr.Split(';');
            foreach (var item in array)
            {
                string[] items = item.Split(',');
                if (items != null && items.Length == 2 && int.TryParse(items[1], out int num))
                {
                    MergeRewardItem itemTemp = new MergeRewardItem()
                    {
                        name = items[0],
                        num = 1
                    };
                    MergeRewardItemWithWidget rewardWithWeight = new MergeRewardItemWithWidget(itemTemp, num);
                    rewardItemPool.TryAddMergeRewardItem(rewardWithWeight);
                    itemNameList.Add(items[0]);
                }
                else
                {
                    UnityEngine.Debug.LogError("data error！" + rewardListStr);
                }
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// 是否是今天
    /// </summary>
    /// <param name="date"></param>
    /// <param name="now"></param>
    /// <returns></returns>
    public static bool IsDateToday(DateTimeOffset date, DateTimeOffset now)
    {
        return date.Year == now.Year && date.Month == now.Month && date.Day == now.Day;
    }
    /// <summary>
    /// 是否是当月
    /// </summary>
    /// <param name="date"></param>
    /// <param name="now"></param>
    /// <returns></returns>
    public static bool IsTheSameMonth(DateTimeOffset date, DateTimeOffset now)
    {
        return date.Year == now.Year && date.Month == now.Month;
    }

    /// <summary>
    /// 是否同一个星期
    /// </summary>
    /// <param name="date"></param>
    /// <param name="now"></param>
    /// <returns></returns>
    public static bool IsTheSameWeek(DateTimeOffset date, DateTimeOffset now)
    {
        DateTimeOffset dateTimeOffset;
        if (dateTimeOffset == date)
            return false;
        else
            return date.AddDays(-(int)date.DayOfWeek).Date == now.AddDays(-(int)now.DayOfWeek).Date;
    }

    /// <summary>
    /// 今天之前
    /// </summary>
    /// <param name="date"></param>
    /// <param name="now"></param>
    /// <returns></returns>
    public static bool IsDateBeforeToday(DateTimeOffset date, DateTimeOffset now)
    {
        return date.Year == now.Year && date.Month == now.Month && date.Day < now.Day;
    }

    private static DateTimeOffset timeOffsetNow;
    public static bool IsDateSmallerThanNow(DateTimeOffset date)
    {
        timeOffsetNow = TimeManager.Instance.UtcNow();
        return date <= timeOffsetNow;
    }

    public static TimeSpan RemainTime(DateTimeOffset date)
    {
        timeOffsetNow = TimeManager.Instance.UtcNow();
        return date - timeOffsetNow;
    }

    /// <summary>
    /// 获取展示的预制体名称
    /// </summary>
    /// <param name="mergeRewardItem"></param>
    /// <returns></returns>
    public static string GetShowPrefabName(MergeRewardItem rewardItem)
    {
        if (string.IsNullOrEmpty(rewardItem.name))
        {
            return "";
        }
        var name = rewardItem.name.ToLower();
        switch (name)
        {
            case "gems":
                return Consts.Icon_Reward_Gems;
            case "coins":
                return Consts.Icon_Reward_Coins;
            case "exp":
                return Consts.Icon_Reward_Exp;
            case "energy":
                return Consts.Icon_Reward_Energy;
            case "love":
                return Consts.Icon_Reward_Love;
            case "points":
                return Consts.Icon_Reward_Points;
            default:
                return rewardItem.name;
        }


    }

    private static SpriteAtlas btnSpriteAtlas;
    /// <summary>
    /// 获取按钮图片，return Sprite
    /// </summary>
    /// <param name="btnType"></param>
    /// <returns></returns>
    public static Sprite GetBtnSprite(MergeUIBtnType btnType)
    {
        if (btnSpriteAtlas == null)
        {
            AssetSystem.Instance.LoadAsset<SpriteAtlas>("ProfileBtnSprite", (so) =>
            {
                btnSpriteAtlas = so;
            });
            if (btnSpriteAtlas == null)
            {
                GameDebug.LogError("Load ProfileBtnSprite Error!");
            }
        }

        if (btnSpriteAtlas == null)
            return null;

        switch (btnType)
        {
            case MergeUIBtnType.Purple1:
                return btnSpriteAtlas.GetSprite("BtnPurple1");
            case MergeUIBtnType.Purple2:
                return btnSpriteAtlas.GetSprite("BtnPurple2");
            case MergeUIBtnType.Purple3:
                return btnSpriteAtlas.GetSprite("BtnPerple3");
            case MergeUIBtnType.Grey1:
                return btnSpriteAtlas.GetSprite("BtnGrey1");
            case MergeUIBtnType.Grey2:
                return btnSpriteAtlas.GetSprite("BtnGrey2");
            case MergeUIBtnType.Grey3:
                return btnSpriteAtlas.GetSprite("BtnGrey3");
            case MergeUIBtnType.Green1:
                return btnSpriteAtlas.GetSprite("BtnGreen1");
            case MergeUIBtnType.Green2:
                return btnSpriteAtlas.GetSprite("BtnGreen2");
            case MergeUIBtnType.Green3:
                return btnSpriteAtlas.GetSprite("BtnGreen3");
            case MergeUIBtnType.Blue0:
                return btnSpriteAtlas.GetSprite("BtnBlue0");
            case MergeUIBtnType.Blue1:
                return btnSpriteAtlas.GetSprite("BtnBlue1");
            case MergeUIBtnType.Blue2:
                return btnSpriteAtlas.GetSprite("BtnBlue2");
            case MergeUIBtnType.Blue3:
                return btnSpriteAtlas.GetSprite("BtnBlue3");
            case MergeUIBtnType.Red1:
                return btnSpriteAtlas.GetSprite("BtnRed1");
            case MergeUIBtnType.Red2:
                return btnSpriteAtlas.GetSprite("BtnRed2");
            case MergeUIBtnType.Red3:
                return btnSpriteAtlas.GetSprite("BtnRed3");
            default:
                return btnSpriteAtlas.GetSprite("BtnGreen2");
        }
    }


    /// <summary>
    /// 文本颜色
    /// </summary>
    /// <param name="textType"></param>
    /// <returns></returns>
    public static Color GetUIBtnLabelEffectColor(MergeUILabelType textType)
    {
        switch (textType)
        {
            //灰色
            case MergeUILabelType.Grey1:
            case MergeUILabelType.Grey2:
            case MergeUILabelType.Grey3:
                return new Color(0 / 255f, 0 / 255f, 0 / 255f, 1);
            //紫色
            case MergeUILabelType.Purple1:
            case MergeUILabelType.Purple2:
            case MergeUILabelType.Purple3:
                return new Color(125 / 255f, 8 / 255f, 140 / 255f, 1);
            //蓝色
            case MergeUILabelType.Blue0:
            case MergeUILabelType.Blue1:
            case MergeUILabelType.Blue2:
            case MergeUILabelType.Blue3:
                return new Color(8 / 255f, 89 / 255f, 140 / 255f, 1);
            //咖啡色
            case MergeUILabelType.Brown:
                return new Color(175 / 255f, 70 / 255f, 74 / 255f, 1);
            case MergeUILabelType.Cyan:
                return new Color(83 / 255f, 101 / 255f, 131 / 255f, 1);
            //绿色
            default:
                return new Color(10 / 255f, 99 / 255f, 5 / 255f, 1);
        }
    }


    private static int strTypeCount;
    private static StringBuilder strBuilder = new StringBuilder();
    /// <summary>
    /// 格式化时间
    /// </summary>
    public static string GetFormatTime(TimeSpan timeSpan, int strTypeCount = 0)
    {
        if (timeSpan.TotalSeconds < 0)
        {
            return string.Empty;
        }

        strBuilder.Clear();
        if (timeSpan.Days > 0)
        {
            strBuilder.Append(timeSpan.Days.ToString() + "d");
            strTypeCount++;
        }
        if (timeSpan.Hours > 0)
        {
            strBuilder.Append(timeSpan.Hours.ToString() + "h");
            strTypeCount++;
        }
        if (strTypeCount < 2 && timeSpan.Minutes > 0)
        {
            strBuilder.Append(timeSpan.Minutes.ToString() + "m");
            strTypeCount++;
        }
        if (timeSpan.Days <= 0 && strTypeCount < 2 && timeSpan.Seconds > 0)
        {
            strBuilder.Append(timeSpan.Seconds.ToString() + "s");
        }
        return strBuilder.ToString();
    }




    public static bool TryCompareAppVersion(string v1, string v2, out int result)
    {
        if (string.IsNullOrEmpty(v1) || string.IsNullOrEmpty(v2))
        {
            result = 0;
            return false;
        }

        string[] v1Array = v1.Split('.');
        string[] v2Array = v2.Split('.');

        if (v1Array == null || v2Array == null)
        {
            result = 0;
            return false;
        }

        for (int i = 0; i < 4; i++)
        {
            if (v1Array.Length - 1 < i && v2Array.Length - 1 >= i)
            {
                result = -1;
                return true;
            }
            else if (v1Array.Length - 1 >= i && v2Array.Length - 1 < i)
            {
                result = 1;
                return true;
            }
            else if (v1Array.Length - 1 >= i && v2Array.Length - 1 >= i)
            {
                if (int.TryParse(v1Array[i], out int v1Temp)
                && int.TryParse(v2Array[i], out int v2Temp))
                {
                    if (v1Temp > v2Temp)
                    {
                        result = 1;
                        return true;
                    }
                    else if (v1Temp < v2Temp)
                    {
                        result = -1;
                        return true;
                    }
                }
                else
                {
                    result = 0;
                    return false;
                }
            }
        }

        result = 0;
        return true;
    }

}

public enum MergeUIBtnType
{
    Purple1,    //紫色
    Purple2,
    Purple3,
    Grey1,      //灰色
    Grey2,
    Grey3,
    Green1,     //绿色
    Green2,
    Green3,
    Blue0,      //蓝色
    Blue1,
    Blue2,
    Blue3,
    Red1,       //红色
    Red2,
    Red3,
}

public enum MergeUILabelType
{
    Purple1,    //紫色
    Purple2,
    Purple3,
    Grey1,      //灰色
    Grey2,
    Grey3,
    Green1,     //绿色
    Green2,
    Green3,
    Blue0,      //蓝色
    Blue1,
    Blue2,
    Blue3,
    Red1,       //红色
    Red2,
    Red3,
    Brown,      //咖啡色
    Cyan,       //青蓝色
}
