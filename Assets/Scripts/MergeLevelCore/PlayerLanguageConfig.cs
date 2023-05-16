using BDUnity;
using BDUnity.Utils;
using ivy.game;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色语言表
/// </summary>
public class PlayerLanguageConfig
{
    private static string configName = "sActiveTextPond";

    public static Dictionary<string, List<string>> playerLanguageDic = new Dictionary<string, List<string>>();
    public static List<string> languageKeyList = new List<string>();
 
    public string languageId { get; private set; }

    public static void Init()
    {
        Dictionary<string, CustomJSONObject> dict = GameConfig.Instance.GetConfig(configName).dict;
        if (dict != null && dict.Count == 1)
        {
            Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
            enumerator.MoveNext();
            Dictionary<string, CustomJSONObject> dict2 = enumerator.Current.Value.dict;
            enumerator = dict2.GetEnumerator();           
            while (enumerator.MoveNext())
            {
                Dictionary<string, CustomJSONObject> dict3 = enumerator.Current.Value.dict;

                if (dict3.TryGetValue("ID", out CustomJSONObject id_Json))
                {
                    PlayerLanguageConfig playerLanguageConfig = new PlayerLanguageConfig();
                    playerLanguageConfig.languageId = id_Json.ToString();
                    if (!playerLanguageDic.ContainsKey(playerLanguageConfig.languageId)) 
                    {
                        languageKeyList = new List<string>();
                        playerLanguageDic.Add(playerLanguageConfig.languageId, languageKeyList);
                    }

                    if (dict3.TryGetValue("Key", out CustomJSONObject key_Json)) 
                    {
                        languageKeyList.Add(key_Json.ToString());
                    }
                }           
            }       
        }
        else
        {
            GameDebug.LogError("sActiveTextPond:Init: Config is null.");
        }
    }
}
