using I2.Loc;
using Ivy;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LanguageManager
{
    public const string k_defaultLanguageShownInDropdown = "...";

    private static int EnglishIndex = -1;

    private static bool EnglishIndexSet = false;

    // 语言名称和code对照
    public readonly static Dictionary<string, string> LocalizedLanguageNameToCode = new Dictionary<string, string>
    {
        { "English","en" },
        { "Español", "es" },
        { "Deutsch", "de" },
        { "Français", "fr" },
        { "Italiano", "it" },
        { "Português", "pt-BR" },

        { "한국어", "ko" },
        { "日本語", "ja" },
        { "Русский", "ru" },
        { "Indonesian", "id" },

        { "繁體中文", "zh-TW" },
        { "中文", "zh-CN" },

    };

    /// <summary>
    /// 当前语言的key值
    /// </summary>
    public static string CurrentLangaugeCode
    {
        get
        {
            return LocalizationManager.CurrentLanguageCode;
        }
        set
        {
            LocalizationManager.CurrentLanguageCode = value;
        }
    }

    public static bool CurrentLanguageIsEnglish => LocalizationManager.CurrentLanguageCode == "en";
    public static string GetCurrentLanguageName()
    {
        foreach (KeyValuePair<string, string> item in LocalizedLanguageNameToCode)
        {
            if (item.Value == CurrentLangaugeCode)
            {
                return item.Key;
            }
        }
        Debug.LogError("Could not find current language in LocalizedLanguageNameToCode dictionary for language code: " + CurrentLangaugeCode);
        return CurrentLangaugeCode;
    }

    public static int GetLanguageIndex(string language)
    {
        return LocalizationManager.Sources[0].GetLanguageIndex(language);
    }

    public static void SelectStartupLanguage()
    {
        string lastChooseLanguage = PlayerPrefs.GetString("I2 Language", string.Empty);
        Debug.Log("lastChooseLanguage:" + lastChooseLanguage);
        if (lastChooseLanguage != string.Empty)
        {
            string tempLanguageCode2 = LocalizationManager.GetLanguageCode(lastChooseLanguage);
            CheckDownloadOrSetDefault(tempLanguageCode2);
            return;
        }

        string systemLan = Application.systemLanguage.ToString();
        if (systemLan == "ChineseSimplified")
        {
            systemLan = "Chinese (Simplified)";
        }
        if (systemLan == "ChineseTraditional")
        {
            systemLan = "Chinese (Traditional)";
        }
        Debug.Log("系统语言:" + systemLan);
        string supportedLanguage = LocalizationManager.GetSupportedLanguage(systemLan);
        string tempLanguageCode = LocalizationManager.GetLanguageCode(supportedLanguage);
        Debug.Log("supportedLanguage:" + supportedLanguage);
        Debug.Log("系统支持的code:" + tempLanguageCode);
        CheckDownloadOrSetDefault(tempLanguageCode);
    }

    /// <summary>
    /// 判断是否是需要下载的字体
    /// </summary>
    /// <param name="tempLanguageCode"></param>
    private static void CheckDownloadOrSetDefault(string tempLanguageCode)
    {
        LogUtils.Log("tempLanguageCode:" + tempLanguageCode);
        GameDebug.Log("所选的LanguageCode不属于需要检查下载的文件");
        if (!string.IsNullOrEmpty(tempLanguageCode))
        {
            LocalizationManager.CurrentLanguageCode = tempLanguageCode;
        }
        else
        {
            LocalizationManager.CurrentLanguageCode = "en";
        }
    }


    public static string GetEnglish(string localizationKey)
    {
        TrySetEnglishIndex();
        if (localizationKey == string.Empty || localizationKey == null)
        {
            return string.Empty;
        }
        TermData termData = LocalizationManager.GetTermData(localizationKey);
        if (termData == null)
        {
            return localizationKey;
        }
        return termData.Languages[EnglishIndex];
    }

    private static void TrySetEnglishIndex()
    {
        if (!EnglishIndexSet)
        {
            EnglishIndex = LocalizationManager.Sources[0].GetLanguageIndex("English");
            EnglishIndexSet = true;
        }
    }

    /// <summary>
    /// 获取需要展示的语言列表
    /// </summary>
    /// <returns></returns>
    public static List<string> GetLanguageToDisplay()
    {
        return LocalizedLanguageNameToCode.Keys.ToList();
    }


    public static event Action OnLanguageChangeFinishEvent;

    /// <summary>
    /// 设置游戏语言
    /// </summary>
    public static void SetNewLanguage(string chosenLanguageWithLocalizedName)
    {
        GameDebug.Log("Language changed to: " + chosenLanguageWithLocalizedName);
        if (chosenLanguageWithLocalizedName == "...")
        {
            return;
        }
        chosenLanguageWithLocalizedName = chosenLanguageWithLocalizedName.Replace("\r", string.Empty);
        if (!LocalizedLanguageNameToCode.ContainsKey(chosenLanguageWithLocalizedName))
        {
            GameDebug.LogError("Player was able to choose language: " + chosenLanguageWithLocalizedName + " but it isn't in the LocalizedLanguageNameToCode dictionary in LanguageManager.cs.");
            return;
        }
        string text = LocalizedLanguageNameToCode[chosenLanguageWithLocalizedName];
        if (CurrentLangaugeCode != text)
        {
            CurrentLangaugeCode = text;
            GameDebug.Log("Language changed to: " + chosenLanguageWithLocalizedName + " (" + text + ")");
        }


        OnLanguageChangeFinishEvent?.Invoke();

    }
}
