using BDUnity.Utils;
using ivy.game;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BattlePassDefinition
{
    private static string configName = "MergeBattlepass";
    public static Dictionary<int, BattlePassDefinition> DefinitionsDict { get; private set; } = new Dictionary<int, BattlePassDefinition>();
    public static int maxExperience { get; private set; }
    public static int maxStage { get; private set; }


    public int stage;
    public int indexExp;//上一等级到该等级所需经验
    public int allExp;//当前总经验
    public MergeRewardItem freeReward;
    public MergeRewardItem vipReward;
    public int stageReward;

    public static void Init()
    {
        Dictionary<string, CustomJSONObject> dict = SingletonMono<GameConfig>.Instance.GetConfig(configName).dict;
        if (dict != null && dict.Count == 1)
        {
            Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
            enumerator.MoveNext();
            Dictionary<string, CustomJSONObject> dict2 = enumerator.Current.Value.dict;
            enumerator = dict2.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Dictionary<string, CustomJSONObject> dict3 = enumerator.Current.Value.dict;
                BattlePassDefinition battlePassDefinition = new BattlePassDefinition();
                if (dict3.TryGetValue("Stage", out CustomJSONObject stage_Json))
                {
                    if (int.TryParse(stage_Json.ToString(), out int stage))
                    {
                        battlePassDefinition.stage = stage;
                        battlePassDefinition.LoadBaseData(dict3);
                        DefinitionsDict.Add(battlePassDefinition.stage, battlePassDefinition);
                    }
                }
                else
                {
                    GameDebug.LogError("this data without key: 'Stage'");
                }
            }
        }
        else
        {
            GameDebug.LogError("MergeSpinWheelConfig::Init: Config is null.");
        }
    }
    private void LoadBaseData(Dictionary<string, CustomJSONObject> dataDic)
    {
        if (dataDic.TryGetValue("IntervalExp", out CustomJSONObject IntervalExp_Json))
        {
            if (int.TryParse(IntervalExp_Json.ToString(), out int temp))
            {
                indexExp = temp;
            }
        }
        if (dataDic.TryGetValue("CountExp", out CustomJSONObject experience_Json))
        {
            if (int.TryParse(experience_Json.ToString(), out int experrence))
            {
                allExp = experrence;
                maxStage = stage;
                maxExperience = experrence;
            }
        }
        if (dataDic.TryGetValue("Reward_Free", out CustomJSONObject freeReward_Json))
        {
            string[] array = freeReward_Json.ToString().Split(',');
            if (array.Length == 2 && int.TryParse(array[1], out int num))
            {
                MergeRewardItem mergeRewardItem = new MergeRewardItem();
                mergeRewardItem.name = array[0];
                mergeRewardItem.num = num;
                freeReward = mergeRewardItem;
            }
            else
            {
                GameDebug.LogError("Reward_Free error :" + freeReward_Json);
            }
        }
        if (dataDic.TryGetValue("Reward_Unlock", out CustomJSONObject vipReward_Json))
        {
            string[] array = vipReward_Json.ToString().Split(',');
            if (array.Length == 2 && int.TryParse(array[1], out int num))
            {
                MergeRewardItem mergeRewardItem = new MergeRewardItem();
                mergeRewardItem.name = array[0];
                mergeRewardItem.num = num;
                vipReward = mergeRewardItem;
            }
            else
            {
                GameDebug.LogError("Reward_Unlock error: " + vipReward_Json);
            }
        }
        if (dataDic.TryGetValue("StageReward", out CustomJSONObject stageReward_Json))
        {
            if (int.TryParse(stageReward_Json.ToString(), out int stageReward))
            {
                this.stageReward = stageReward;
            }
        }
    }

    //private static void LoadRemoteBattlePassConfig()
    //{
    //    try
    //    {
    //        string str = RemoteConfigSystem.Instance.GetRemoteConfig_String(RemoteConfigSystem.remoteKey_battlepassConfig);
    //        if (string.IsNullOrEmpty(str))
    //        {
    //            return;
    //        }

    //        var largeDict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, object>>>>(str);
    //        if (largeDict == null)
    //        {
    //            return;
    //        }

    //        foreach (var dict in largeDict)
    //        {
    //            if (dict.Key != "all")
    //            {
    //                string[] verstr = dict.Key.Split('_');
    //                if (verstr == null || verstr.Length != 2)
    //                {
    //                    continue;
    //                }

    //                string currentVersion = Application.isEditor && DebugSetting.CanUseDebugConfig(out var debugSO)
    //                    ? debugSO.DebugCurrentVersion
    //                    : RiseSdk.Instance.GetConfig(RiseSdk.CONFIG_KEY_VERSION_NAME);
    //                if (ExtensionTool.TryCompareAppVersion(currentVersion, verstr[1], out int result))
    //                {
    //                    switch (verstr[0])
    //                    {
    //                        case "equal":
    //                            if (result != 0)
    //                            {
    //                                continue;
    //                            }
    //                            break;
    //                        case "less":
    //                            if (result != -1)
    //                            {
    //                                continue;
    //                            }
    //                            break;
    //                        case "greater":
    //                            if (result != 1)
    //                            {
    //                                continue;
    //                            }
    //                            break;
    //                        default:
    //                            continue;
    //                            break;
    //                    }
    //                }
    //                else
    //                {
    //                    continue;
    //                }
    //            }

    //            foreach (var stageDict in dict.Value)
    //            {
    //                if (!int.TryParse(stageDict.Key, out int tempStage))
    //                {
    //                    continue;
    //                }

    //                if (!DefinitionsDict.TryGetValue(tempStage, out var itemDefinition))
    //                {
    //                    itemDefinition = new BattlePassDefinition();
    //                    itemDefinition.stage = tempStage;
    //                    if (MaxStage < tempStage)
    //                    {
    //                        MaxStage = tempStage;
    //                    }
    //                    DefinitionsDict.Add(itemDefinition.stage, itemDefinition);
    //                }

    //                if (stageDict.Value.TryGetValue("IntervalExp", out object IntervalExp))
    //                {
    //                    if (int.TryParse(IntervalExp.ToString(), out int temp))
    //                    {
    //                        itemDefinition.indexExp = temp;
    //                    }
    //                }
    //                if (stageDict.Value.TryGetValue("CountExp", out object experiecneObj))
    //                {
    //                    if (int.TryParse(experiecneObj.ToString(), out int temp))
    //                    {
    //                        itemDefinition.allExp = temp;
    //                    }
    //                }
    //                if (stageDict.Value.TryGetValue("Reward_Free", out object freeObj))
    //                {
    //                    if (ExtensionTool.TryParseMergeRewardItem(freeObj.ToString(), out MergeRewardItem temp))
    //                    {
    //                        itemDefinition.freeReward = temp;
    //                    }
    //                }
    //                if (stageDict.Value.TryGetValue("Reward_Unlock", out object rewardUnlockPay))
    //                {
    //                    if (ExtensionTool.TryParseMergeRewardItem(rewardUnlockPay.ToString(), out MergeRewardItem temp))
    //                    {
    //                        itemDefinition.vipReward = temp;
    //                    }
    //                }
    //                if (stageDict.Value.TryGetValue("StageReward", out object stageRewardObj))
    //                {
    //                    if (int.TryParse(stageRewardObj.ToString(), out int temp))
    //                    {
    //                        itemDefinition.stageReward = temp;
    //                    }
    //                }


    //            }

    //        }


    //    }
    //    catch (Exception e)
    //    {
    //        DebugSetting.LogError(e, "[LoadRemoteConfigObject]");
    //    }
    //}

}
