using BDUnity.Utils;
using ivy.game;
using System.Collections.Generic;

public class MergeStarRewardDefinition
{
    private static string dataResourceFileName = "StarReward";
    public static Dictionary<int, MergeStarRewardDefinition> starRewards = new Dictionary<int, MergeStarRewardDefinition>();

    public int Id { get; private set; }
    public int StarCount { get; private set; }
    public List<MergeRewardItem> rewardItemList = new List<MergeRewardItem>();

    public static void LoadDefinition()
    {
        Dictionary<string, CustomJSONObject> dict = SingletonMono<GameConfig>.Instance.GetConfig(dataResourceFileName).dict;
        if (dict != null && dict.Count == 1)
        {
            Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
            enumerator.MoveNext();
            Dictionary<string, CustomJSONObject> dict2 = enumerator.Current.Value.dict;
            enumerator = dict2.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Dictionary<string, CustomJSONObject> dict3 = enumerator.Current.Value.dict;
                MergeStarRewardDefinition rewardDefinition = new MergeStarRewardDefinition();
                rewardDefinition.LoadBaseData(dict3);
            }
        }
    }

    private void LoadBaseData(Dictionary<string, CustomJSONObject> dataDic)
    {
        if (dataDic.TryGetValue("ID", out CustomJSONObject Id_Json))
        {
            var strId = Id_Json.ToString();
            if (!string.IsNullOrEmpty(strId) && int.TryParse(strId, out var tempId))
            {
                this.Id = tempId;
            }
            else
                return;
        }
        else
            return;

        if (dataDic.TryGetValue("StarNum", out CustomJSONObject starNum_Json))
        {
            var strNum = starNum_Json.ToString();
            if (!string.IsNullOrEmpty(strNum) && int.TryParse(strNum, out var tempNum))
            {
                this.StarCount = tempNum;
            }
        }

        rewardItemList.Clear();
        for (int i = 1; i <= 6; i++)
        {
            if (dataDic.TryGetValue($"Reward{i}", out CustomJSONObject reward_Json))
            {
                var reward = reward_Json.ToString();
                if (!string.IsNullOrEmpty(reward))
                {
                    string[] array = reward.Split(',');
                    if (array != null && array.Length == 2 && int.TryParse(array[1], out int num))
                    {
                        MergeRewardItem rewardItem = new MergeRewardItem
                        {
                            name = array[0],
                            num = num
                        };
                        rewardItemList.Add(rewardItem);

                        GameConfig.CheckMergeRewardItemValid(dataResourceFileName, rewardItem);
                    }
                }
            }
        }

        starRewards.Add(this.Id, this);
    }
}
