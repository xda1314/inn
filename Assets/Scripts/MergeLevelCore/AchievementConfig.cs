using BDUnity.Utils;
using ivy.game;
using System.Collections.Generic;

public enum AchievementType
{
    none,
    levels,
    getCoins,
    getDiamonds,
    costCoins,
    costDiamonds,
    costEnergy,
    unlockBubble,
    speedCharge,
    chapter,
    warehouse
}


/// <summary>
/// 子产出池配表
/// </summary>
public class AchievementConfig
{
    private const string configName = "Achievements";

    public static Dictionary<AchievementType, List<AchievementConfig>> ConfigDict = new Dictionary<AchievementType, List<AchievementConfig>>();

    public string ID { get; private set; }

    public AchievementType achievementType { get; private set; }

    public int Rate { get; private set; }


    public static void Init()
    {
        Dictionary<string, CustomJSONObject> dict = SingletonMono<GameConfig>.Instance.GetConfig(configName).dict;
        if (dict != null && dict.Count == 1)
        {
            ConfigDict.Clear();
            Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
            enumerator.MoveNext();
            Dictionary<string, CustomJSONObject> dict2 = enumerator.Current.Value.dict;
            enumerator = dict2.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Dictionary<string, CustomJSONObject> dict3 = enumerator.Current.Value.dict;
                AchievementConfig achievementConfig = new AchievementConfig();
                if (dict3.TryGetValue("ID", out CustomJSONObject id_Json))
                {
                    achievementConfig.ID = id_Json.ToString();
                }
                else
                {
                    continue;
                }

                if (dict3.TryGetValue("Rate", out CustomJSONObject Rate_Json))
                {
                    if (int.TryParse(Rate_Json.ToString(), out int temp))
                    {
                        achievementConfig.Rate = temp;
                    }
                }

                if (dict3.TryGetValue("Type", out CustomJSONObject Type_Json))
                {
                    switch (Type_Json.ToString())
                    {
                        case "levels":
                            achievementConfig.achievementType = AchievementType.levels;
                            break;
                        case "getCoins":
                            achievementConfig.achievementType = AchievementType.getCoins;
                            break;
                        case "getDiamonds":
                            achievementConfig.achievementType = AchievementType.getDiamonds;
                            break;
                        case "costCoins":
                            achievementConfig.achievementType = AchievementType.costCoins;
                            break;
                        case "costDiamonds":
                            achievementConfig.achievementType = AchievementType.costDiamonds;
                            break;
                        case "costEnergy":
                            achievementConfig.achievementType = AchievementType.costEnergy;
                            break;
                        case "unlockBubble":
                            achievementConfig.achievementType = AchievementType.unlockBubble;
                            break;
                        case "speedCharge":
                            achievementConfig.achievementType = AchievementType.speedCharge;
                            break;
                        case "chapter":
                            achievementConfig.achievementType = AchievementType.chapter;
                            break;
                        case "warehouse":
                            achievementConfig.achievementType = AchievementType.warehouse;
                            break;
                        default:
                            continue;
                    }
                }

                if (ConfigDict.TryGetValue(achievementConfig.achievementType, out var list))
                {
                    list.Add(achievementConfig);
                }
                else
                {
                    list = new List<AchievementConfig>();
                    list.Add(achievementConfig);
                    ConfigDict[achievementConfig.achievementType] = list;
                }
            }
        }
        else
        {
            GameDebug.LogError("MonthPond::Init: Config is null.");
        }
    }
}