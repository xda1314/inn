using BDUnity.Utils;
using ivy.game;
using System;
using System.Collections.Generic;


public class DailyDefinition
{
    private const string configName = "MergeDailyEvent";

    public static Dictionary<MergeLevelType, DailyDefinition> DailyDefDic = new Dictionary<MergeLevelType, DailyDefinition>();

    #region 成员
    public MergeLevelType type { get; private set; }
    public int unlockChapter { get; private set; }
    public List<MergeRewardItem> AmiMergeItems { get; set; } = new List<MergeRewardItem>();
    public MergeRewardItem Score { get; set; } = new MergeRewardItem();
    public string pictureName { get; private set; }
    public string characterPic { get; private set; }
    public string textTitle { get; private set; }
    #endregion

    public DailyDefinition(MergeLevelType type)
    {
        this.type = type;
    }

    public static void LoadDailyDefinitions()
    {
        var dict = SingletonMono<GameConfig>.Instance.GetConfig(configName).dict;
        if (dict != null && dict.Count == 1)
        {
            var enumerator = dict.GetEnumerator();
            if (enumerator.MoveNext())
            {
                var dict2 = enumerator.Current.Value.dict;
                enumerator = dict2.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var dict3 = enumerator.Current.Value.dict;
                    if (dict3.TryGetValue("ActivityType", out CustomJSONObject type_Json))
                    {
                        if (Enum.TryParse(type_Json.ToString(), out MergeLevelType type))
                        {
                            if (!DailyDefDic.ContainsKey(type))
                                DailyDefDic[type] = new DailyDefinition(type);
                            if (dict3.TryGetValue("Aims", out CustomJSONObject Aim_pJson))
                            {
                                string reward = Aim_pJson.ToString();
                                string[] strs = reward.Split(';');
                                for (int i = 0; i < strs.Length; i++)
                                {
                                    string[] array = strs[i].Split(',');
                                    if (array.Length == 2 && array[0] != null && int.TryParse(array[1], out int num))
                                    {
                                        MergeRewardItem data = new MergeRewardItem();
                                        data.name = array[0];
                                        data.num = num;
                                        DailyDefDic[type].AmiMergeItems.Add(data);
                                    }
                                }
                            }
                            if (dict3.TryGetValue("Score", out CustomJSONObject Score_Json))
                            {
                                string reward = Score_Json.ToString();
                                string[] array = reward.Split(',');
                                if (array.Length == 2 && !string.IsNullOrEmpty(array[0]) && int.TryParse(array[1], out int num))
                                {
                                    MergeRewardItem data = new MergeRewardItem();
                                    data.name = array[0];
                                    data.num = num;
                                    DailyDefDic[type].Score = data;
                                }
                            }

                            if (dict3.TryGetValue("Picture Name", out CustomJSONObject pictureNameObject)) 
                            {
                                DailyDefDic[type].pictureName = pictureNameObject.ToString();
                            }

                            if (dict3.TryGetValue("CharacterPic", out CustomJSONObject characterPicObject)) 
                            {
                                DailyDefDic[type].characterPic = characterPicObject.ToString();
                            }
                            if (dict3.TryGetValue("TextTitle", out CustomJSONObject textTitleObject))
                            {
                                DailyDefDic[type].textTitle = textTitleObject.ToString();
                            }
                        }
                    }
                }
            }
        }
    }
}
