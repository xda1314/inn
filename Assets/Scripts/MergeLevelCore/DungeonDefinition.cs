using BDUnity.Utils;
using ivy.game;
using System;
using System.Collections.Generic;


public class DungeonDefinition
{
    private const string configName = "MergeDungeonSummary";

    public static Dictionary<MergeLevelType, DungeonDefinition> DungeonDefDic = new Dictionary<MergeLevelType, DungeonDefinition>();

    #region 成员
    public MergeLevelType type { get; private set; }
    public int unlockChapter { get; private set; }

    public List<MergeRewardItem> AmiMergeItems { get; set; } = new List<MergeRewardItem>();
    public List<MergeRewardItem> Score { get; set; } = new List<MergeRewardItem>();
    public List<MergeRewardItem> ItemRewardList { get; set; } = new List<MergeRewardItem>();//物品奖励
    private Dictionary<DungeonState, string> bgDic = new Dictionary<DungeonState, string>();
    private Dictionary<DungeonState, string> photoDic = new Dictionary<DungeonState, string>();
    private Dictionary<DungeonState, string> titleDic = new Dictionary<DungeonState, string>();
    private Dictionary<DungeonState, string> desDic = new Dictionary<DungeonState, string>();
    private Dictionary<DungeonState, string> textDesDic = new Dictionary<DungeonState, string>();

    public string function { get; private set; }

    public DungeonDefinition(MergeLevelType type)
    {
        this.type = type;
    }


    public void AddBg(DungeonState state, string bg)
    {
        bgDic[state] = bg;
    }

    public void AddPhoto(DungeonState state, string photo)
    {
        photoDic[state] = photo;
    }



    public string GetBg(DungeonState state)
    {
        if (bgDic.ContainsKey(state))
            return bgDic[state];
        return null;
    }

    public string GetPhoto(DungeonState state)
    {
        if (photoDic.ContainsKey(state))
            return photoDic[state];
        return null;
    }


    public void AddTitle(DungeonState state, string title)
    {
        titleDic[state] = title;
    }

    public string GetTitle(DungeonState state)
    {
        if (titleDic.ContainsKey(state))
            return titleDic[state];
        return null;
    }

    private void AddDescription(DungeonState state, string des)
    {
        desDic[state] = des;
    }

    private void AddUnlockChapter(int unlockChapter)
    {
        this.unlockChapter = unlockChapter;
    }
    private void AddTaskDescribe_Key(DungeonState state, string des) 
    {
        textDesDic[state] = des;
    }

    public string GetDescription(DungeonState state)
    {
        if (desDic.ContainsKey(state))
            return desDic[state];
        return null;
    }
    public string GetTextDescription(DungeonState state) 
    {
        if (textDesDic.ContainsKey(state))
            return textDesDic[state];
        return null;
    }

    public void SetFunc(string func)
    {
        function = func;
    }
    #endregion

    public static void LoadDungeonDefinitions()
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
                    if (dict3.TryGetValue("DungeonType", out CustomJSONObject type_Json))
                    {
                        if(Enum.TryParse<MergeLevelType>(type_Json.ToString(), out MergeLevelType type))
                        {
                            if (!DungeonDefDic.ContainsKey(type))
                                DungeonDefDic[type] = new DungeonDefinition(type);
                            if (DungeonDefDic.ContainsKey(type))
                            {
                                if (dict3.TryGetValue("State", out CustomJSONObject state_Json))
                                {
                                    var state = DungeonState.none;
                                    var strState = state_Json.ToString();
                                    if (strState.Equals("unlock", StringComparison.OrdinalIgnoreCase))
                                        state = DungeonState.locked;
                                    else if (strState.Equals("conduct", StringComparison.OrdinalIgnoreCase))
                                        state = DungeonState.unlock;
                                    else if (strState.Equals("finished", StringComparison.OrdinalIgnoreCase))
                                        state = DungeonState.finished;

                                    if (state != DungeonState.none)
                                    {
                                        if (dict3.TryGetValue("Pic_Background", out CustomJSONObject pic_Json))
                                            DungeonDefDic[type].AddBg(state, pic_Json.ToString());
                                        if (dict3.TryGetValue("Title_Key", out CustomJSONObject title_Json))
                                            DungeonDefDic[type].AddTitle(state, title_Json.ToString());
                                        if (dict3.TryGetValue("Describe_Key", out CustomJSONObject des_Json))
                                            DungeonDefDic[type].AddDescription(state, des_Json.ToString());
                                        if (dict3.TryGetValue("Pic_Photo", out CustomJSONObject pic_pJson))
                                            DungeonDefDic[type].AddPhoto(state, pic_pJson.ToString());
                                    }

                                    if (state == DungeonState.unlock)
                                    {
                                        if (dict3.TryGetValue("TaskDescribe_Key", out CustomJSONObject TaskDescribe_Key_Json)) 
                                        {
                                            DungeonDefDic[type].AddTaskDescribe_Key(state, TaskDescribe_Key_Json.ToString());
                                        }
                                        if (dict3.TryGetValue("UnlockChapter", out CustomJSONObject unlockChapter_pJson))
                                        {
                                            if (int.TryParse(unlockChapter_pJson.ToString(), out int unlockChapter_int))
                                                DungeonDefDic[type].AddUnlockChapter(unlockChapter_int);
                                        }
                                        if (dict3.TryGetValue("Aim", out CustomJSONObject Aim_pJson))
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
                                                    DungeonDefDic[type].AmiMergeItems.Add(data);
                                                }
                                            }
                                        }
                                        if (dict3.TryGetValue("Score", out CustomJSONObject Score_Json))
                                        {
                                            string reward = Score_Json.ToString();
                                            string[] strs = reward.Split(';');
                                            for (int i = 0; i < strs.Length; i++)
                                            {
                                                string[] array = strs[i].Split(',');
                                                if (array.Length == 2 && array[0] != null && int.TryParse(array[1], out int num))
                                                {
                                                    MergeRewardItem data = new MergeRewardItem();
                                                    data.name = array[0];
                                                    data.num = num;
                                                    DungeonDefDic[type].Score.Add(data);
                                                }
                                            }
                                        }


                                        if (dict3.TryGetValue("Reward1", out CustomJSONObject Reward1_Json))
                                        {
                                            string reward = Reward1_Json.ToString();
                                            string[] strs = reward.Split(';');
                                            for (int i = 0; i < strs.Length; i++)
                                            {
                                                string[] array = strs[i].Split(',');
                                                if (array.Length == 2 && array[0] != null && int.TryParse(array[1], out int num))
                                                {
                                                    MergeRewardItem data = new MergeRewardItem();
                                                    data.name = array[0];
                                                    data.num = num;
                                                    DungeonDefDic[type].ItemRewardList.Add(data);
                                                }
                                            }
                                        }
                                        if (dict3.TryGetValue("Reward2", out CustomJSONObject Reward2_Json))
                                        {
                                            string reward = Reward2_Json.ToString();
                                            string[] strs = reward.Split(';');
                                            for (int i = 0; i < strs.Length; i++)
                                            {
                                                string[] array = strs[i].Split(',');
                                                if (array.Length == 2 && array[0] != null && int.TryParse(array[1], out int num))
                                                {
                                                    MergeRewardItem data = new MergeRewardItem();
                                                    data.name = array[0];
                                                    data.num = num;
                                                    DungeonDefDic[type].ItemRewardList.Add(data);
                                                }
                                            }
                                        }
                                        if (dict3.TryGetValue("Reward3", out CustomJSONObject Reward3_Json))
                                        {
                                            string reward = Reward3_Json.ToString();
                                            string[] strs = reward.Split(';');
                                            for (int i = 0; i < strs.Length; i++)
                                            {
                                                string[] array = strs[i].Split(',');
                                                if (array.Length == 2 && array[0] != null && int.TryParse(array[1], out int num))
                                                {
                                                    MergeRewardItem data = new MergeRewardItem();
                                                    data.name = array[0];
                                                    data.num = num;
                                                    DungeonDefDic[type].ItemRewardList.Add(data);
                                                }
                                            }
                                        }
                                        if (dict3.TryGetValue("Reward4", out CustomJSONObject Reward4_Json))
                                        {
                                            string reward = Reward4_Json.ToString();
                                            string[] strs = reward.Split(';');
                                            for (int i = 0; i < strs.Length; i++)
                                            {
                                                string[] array = strs[i].Split(',');
                                                if (array.Length == 2 && array[0] != null && int.TryParse(array[1], out int num))
                                                {
                                                    MergeRewardItem data = new MergeRewardItem();
                                                    data.name = array[0];
                                                    data.num = num;
                                                    DungeonDefDic[type].ItemRewardList.Add(data);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }

                    }
                }
            }
        }
    }
}
