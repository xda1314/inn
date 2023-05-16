using BDUnity;
using BDUnity.Utils;
using ivy.game;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家等级奖励表
/// </summary>
public class MergeLevelDefinition
{
    private static string configName = "LevelAndReward";

    public static Dictionary<string, MergeLevelDefinition> LevelDefinitionDict = new Dictionary<string, MergeLevelDefinition>();//<level,definition>

    public int level { get; private set; }
    public int CountExp { get; private set; }//上一级升级到当前等级所需经验
    public int AllExp { get; private set; }//升级到当前等级所需的经验总和

    public List<MergeRewardItem> levelRewardList { get; private set; } = new List<MergeRewardItem>();

    public static void Init()
    {
        Dictionary<string, CustomJSONObject> dict = SingletonMono<GameConfig>.Instance.GetConfig(configName).dict;
        if (dict != null && dict.Count == 1)
        {
            Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
            enumerator.MoveNext();
            Dictionary<string, CustomJSONObject> dict2 = enumerator.Current.Value.dict;
            enumerator = dict2.GetEnumerator();
            MergeLevelDefinition mergeLevelDefinition;
            while (enumerator.MoveNext())
            {
                Dictionary<string, CustomJSONObject> dict3 = enumerator.Current.Value.dict;

                if (dict3.TryGetValue("Level", out CustomJSONObject level_Json))
                {
                    mergeLevelDefinition = new MergeLevelDefinition();
                    if (int.TryParse(level_Json.ToString(), out int level))
                    {
                        mergeLevelDefinition.level = level;
                        mergeLevelDefinition.LoadBaseData(dict3);
                    }
                    LevelDefinitionDict.Add(level_Json.ToString(), mergeLevelDefinition);
                }
            }
        }
        else
        {
            GameDebug.LogError("MonthPond::Init: Config is null.");
        }
    }
    private void LoadBaseData(Dictionary<string, CustomJSONObject> dataDic)
    {
        if (dataDic.TryGetValue("IntervalEXP", out CustomJSONObject intervalEXP_Json))
        {
            if (int.TryParse(intervalEXP_Json.ToString(), out int intervalEXP))
            {
                CountExp = intervalEXP;
            }
        }
        if (dataDic.TryGetValue("SumEXP", out CustomJSONObject sumEXP_Json))
        {
            if (int.TryParse(sumEXP_Json.ToString(), out int sumEXP))
            {
                AllExp = sumEXP;
            }
        }
        for (int i = 1; i <= 3; i++)
        {
            if (dataDic.TryGetValue("Reward" + i.ToString(), out CustomJSONObject reward_Json))
            {
                string[] array = reward_Json.ToString().Split(',');
                if (array.Length == 2 && int.TryParse(array[1], out int num))
                {
                    MergeRewardItem item = new MergeRewardItem();
                    item.name = array[0];
                    item.num = num;
                    levelRewardList.Add(item);
                }
            }
        }
    }
}
