using BDUnity.Utils;
using EnhancedUI;
using ivy.game;
using System.Collections.Generic;

public class BranchRewardDefinition
{
    private static string[] configArray = 
    {
        "MergeSpurLineReward",
        "MergeFestivalReward"
    };
    public static Dictionary<MergeLevelType, SmallList<BranchRewardDefinition>> BranchRewardDict = new Dictionary<MergeLevelType, SmallList<BranchRewardDefinition>>();

    private MergeLevelType levelType;
    public int goalPoint { get; private set; }
    public List<MergeRewardItem> rewardDataList { get; private set; } = new List<MergeRewardItem>();
    public List<int> rewardRareNumList { get; private set; } = new List<int>();

    public static void LoadDefinition()
    {
        for (int i = 0; i < configArray.Length; i++)
        {
            Dictionary<string, CustomJSONObject> dict = SingletonMono<GameConfig>.Instance.GetConfig(configArray[i]).dict;
            if (dict != null && dict.Count == 1)
            {
                Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
                enumerator.MoveNext();
                Dictionary<string, CustomJSONObject> dict2 = enumerator.Current.Value.dict;
                enumerator = dict2.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Dictionary<string, CustomJSONObject> dict3 = enumerator.Current.Value.dict;
                    BranchRewardDefinition def = new BranchRewardDefinition();
                    def.LoadBaseData(dict3);

                    if (!BranchRewardDict.ContainsKey(def.levelType))
                    {
                        BranchRewardDict.Add(def.levelType, new SmallList<BranchRewardDefinition>());
                    }
                    if (BranchRewardDict.TryGetValue(def.levelType, out SmallList<BranchRewardDefinition> smallList))
                    {
                        smallList.Add(def);
                    }
                }
            }
        }        
    }

    private void LoadBaseData(Dictionary<string, CustomJSONObject> dataDic)
    {
        MergeLevelType type = MergeLevelType.none;
        if (dataDic.TryGetValue("ActivityType", out CustomJSONObject type_Json))
        {
            var strType = type_Json.ToString();
            if (strType.Equals("SpurLine1", System.StringComparison.OrdinalIgnoreCase))
                type = MergeLevelType.branch1;
            if (strType.Equals("SpurLine2", System.StringComparison.OrdinalIgnoreCase))
                type = MergeLevelType.branch_halloween;
            if (strType.Equals("SpurLine3", System.StringComparison.OrdinalIgnoreCase))
                type = MergeLevelType.branch_christmas;
            if (strType.Equals("SpurLine4", System.StringComparison.OrdinalIgnoreCase))
                type = MergeLevelType.branch_SpurLine4;
            if (strType.Equals("SpurLine5", System.StringComparison.OrdinalIgnoreCase))
                type = MergeLevelType.branch_SpurLine5;
            if (strType.Equals("SpurLine6", System.StringComparison.OrdinalIgnoreCase))
                type = MergeLevelType.branch_SpurLine6;
            else if (strType.Equals("Festival1", System.StringComparison.OrdinalIgnoreCase))
                type = MergeLevelType.halloween;
            else if (strType.Equals("Festival2", System.StringComparison.OrdinalIgnoreCase))
                type = MergeLevelType.christmas;
            else if (strType.Equals("Festival3", System.StringComparison.OrdinalIgnoreCase))
                type = MergeLevelType.lover;
        }

        if (type == MergeLevelType.none)
        {
            GameDebug.LogError("支线类型解析错误!");
            return;
        }

        if (dataDic.TryGetValue("CountPoints", out CustomJSONObject point_Json))
        {
            var strPoint = point_Json.ToString();
            if (!string.IsNullOrEmpty(strPoint) && int.TryParse(strPoint, out var tempNum))
            {
                this.goalPoint = tempNum;
            }
        }

        rewardDataList.Clear();
        rewardRareNumList.Clear();
        for (int i = 1; i <= 2; i++)
        {
            if (dataDic.TryGetValue($"Reward{i}", out CustomJSONObject reward_Json))
            {
                var reward = reward_Json.ToString();
                if (!string.IsNullOrEmpty(reward))
                {
                    string[] array = reward.Split(',');
                    if (array != null && array.Length == 3 && int.TryParse(array[1], out int num))
                    {
                        MergeRewardItem rewardItem = new MergeRewardItem
                        {
                            name = array[0],
                            num = num
                        };
                        rewardDataList.Add(rewardItem);
                        int rare = 0;
                        if (!string.IsNullOrEmpty(array[2]) && array[2].Equals("rare", System.StringComparison.OrdinalIgnoreCase))
                            rare = 1;
                        rewardRareNumList.Add(rare);
                    }
                }
            }
        }
        levelType = type;

    }
}

