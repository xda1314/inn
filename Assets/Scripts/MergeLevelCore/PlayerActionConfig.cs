using BDUnity;
using BDUnity.Utils;
using ivy.game;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色动作表
/// </summary>
public class PlayerActionConfig
{
    private static string configName = "sActivePond";
    public static Dictionary<string, List<PlayerActionItem>> playerActionDic = new Dictionary<string, List<PlayerActionItem>>();
    public static List<PlayerActionItem> actionList = new List<PlayerActionItem>();
 
    public string actionId { get; private set; }

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
                    PlayerActionConfig playerActionConfig = new PlayerActionConfig();
                    playerActionConfig.actionId = id_Json.ToString();
                    if (!playerActionDic.ContainsKey(playerActionConfig.actionId)) 
                    {
                        actionList = new List<PlayerActionItem>();
                        playerActionDic.Add(playerActionConfig.actionId, actionList);
                    }

                    PlayerActionItem item = new PlayerActionItem();
                    if (dict3.TryGetValue("ActiveName", out CustomJSONObject actionName_Json)) 
                    {
                        item.playerActionName = actionName_Json.ToString();
                    }
                    if (dict3.TryGetValue("ActiveTextPond", out CustomJSONObject languagePondId_Json)) 
                    {
                        item.languagePondId = languagePondId_Json.ToString();
                    }
                    actionList.Add(item);
                }           
            }       
        }
        else
        {
            GameDebug.LogError("sActivePond:Init: Config is null.");
        }
    }
}

public class PlayerActionItem 
{
    public string playerActionName;
    public string languagePondId;
}
