using BDUnity.Utils;
using ivy.game;
using System.Collections.Generic;

public class MergeChapterRewardDefinition
{
    private static string dataResourceFileName = "ChapterReward";

    public static Dictionary<int, MergeChapterRewardDefinition> chapterRewards = new Dictionary<int, MergeChapterRewardDefinition>();

    public int Id { get; private set; }
    public int Chapter { get; private set; }


    public List<MergeRewardItem> rewardItemList = new List<MergeRewardItem>();

    public List<int> idList { get; private set; }

    public static List<int> chapterIDList = new List<int>();


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
                MergeChapterRewardDefinition rewardDefinition = new MergeChapterRewardDefinition();
                rewardDefinition.LoadBaseData(dict3);
            }
        }
    }

    private void LoadBaseData(Dictionary<string, CustomJSONObject> dataDic)
    {
        if (dataDic.TryGetValue("ID", out CustomJSONObject saveId_Json))
        {
            if (int.TryParse(saveId_Json.ToString(), out int saveid))
                Id = saveid;
            else
                return;
        }
        else
            return;

        if (dataDic.TryGetValue("ChapterID", out CustomJSONObject chapterId_Json))
        {
            var strChapter = chapterId_Json.ToString();
            string[] array = strChapter.Split('_');
            string index = array[0];
            if (int.TryParse(index, out var temp))
                Chapter = temp;
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
                        MergeRewardItem chapterRewardItem = new MergeRewardItem
                        {
                            name = array[0],
                            num = num
                        };
                        rewardItemList.Add(chapterRewardItem);
                    }
                }
            }
        }

        chapterRewards.Add(this.Id, this);
    }
}
