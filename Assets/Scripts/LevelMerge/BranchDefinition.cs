using BDUnity.Utils;
using ivy.game;
using System.Collections.Generic;

public class BranchDefinition
{
    private static string[] branchConfigs = new string[2]
    {
        "MergeSpurLine",
        "MergeFestival"
    };
    public static Dictionary<MergeLevelType, Dictionary<string, BranchDefinition>> allBranchdefDic = new Dictionary<MergeLevelType, Dictionary<string, BranchDefinition>>();

    public string id { get; private set; }
    public List<string> nextIdList { get; private set; } = new List<string>();
    public MergeLevelType branchType { get; private set; }
    public List<MergeRewardItem> needItemList { get; private set; } = new List<MergeRewardItem>();
    public List<MergeRewardItem> rewardItemList { get; private set; } = new List<MergeRewardItem>();
    public string pictureName { get; private set; }
    public string characterPic { get; private set; }
    public string Text { get; private set; }
    


    public static void Init()
    {
        for (int i = 0; i < branchConfigs.Length; i++)
        {
            Dictionary<string, CustomJSONObject> dict = GameConfig.Instance.GetConfig(branchConfigs[i]).dict;
            if (dict != null && dict.Count == 1)
            {
                Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
                enumerator.MoveNext();
                Dictionary<string, CustomJSONObject> dict2 = enumerator.Current.Value.dict;
                enumerator = dict2.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    CustomJSONObject value = enumerator.Current.Value;
                    BranchDefinition def = new BranchDefinition();
                    def.id = value.GetString("ID", string.Empty);
                    string spurLineTpye = value.GetString("ActivityType", string.Empty);
                    if (spurLineTpye.Equals("SpurLine1", System.StringComparison.OrdinalIgnoreCase))
                    {
                        def.branchType = MergeLevelType.branch1;
                    }
                    else if (spurLineTpye.Equals("SpurLine2", System.StringComparison.OrdinalIgnoreCase))
                    {
                        def.branchType = MergeLevelType.branch_halloween;
                    }
                    else if (spurLineTpye.Equals("SpurLine3", System.StringComparison.OrdinalIgnoreCase))
                    {
                        def.branchType = MergeLevelType.branch_christmas;
                    }
                    else if (spurLineTpye.Equals("SpurLine4", System.StringComparison.OrdinalIgnoreCase))
                    {
                        def.branchType = MergeLevelType.branch_SpurLine4;
                    }
                    else if (spurLineTpye.Equals("SpurLine5", System.StringComparison.OrdinalIgnoreCase))
                    {
                        def.branchType = MergeLevelType.branch_SpurLine5;
                    }
                    else if (spurLineTpye.Equals("SpurLine6", System.StringComparison.OrdinalIgnoreCase))
                    {
                        def.branchType = MergeLevelType.branch_SpurLine6;
                    }
                    else if (spurLineTpye.Equals("Festival1", System.StringComparison.OrdinalIgnoreCase))
                    {
                        def.branchType = MergeLevelType.halloween;
                    }
                    else if (spurLineTpye.Equals("Festival2", System.StringComparison.OrdinalIgnoreCase))
                    {
                        def.branchType = MergeLevelType.christmas;
                    }
                    else if (spurLineTpye.Equals("Festival3", System.StringComparison.OrdinalIgnoreCase))
                    {
                        def.branchType = MergeLevelType.lover;
                    }
                    else
                    {
                        GameDebug.LogError("支线类型未解析，请检查配表，错误类型为:" + spurLineTpye);
                    }
                    string needItem1 = value.GetString("Aim1", string.Empty);
                    if (ExtensionTool.TryParseMergeRewardItem(needItem1, out MergeRewardItem needItem_1))
                    {
                        def.needItemList.Add(needItem_1);
                    }
                    string needItem2 = value.GetString("Aim2", string.Empty);
                    if (ExtensionTool.TryParseMergeRewardItem(needItem2, out MergeRewardItem needItem_2))
                    {
                        def.needItemList.Add(needItem_2);
                    }
                    string reward1 = value.GetString("Reward1", string.Empty);
                    if (ExtensionTool.TryParseMergeRewardItem(reward1, out MergeRewardItem reward_1))
                    {
                        def.rewardItemList.Add(reward_1);
                    }
                    string reward2 = value.GetString("Reward2", string.Empty);
                    if (ExtensionTool.TryParseMergeRewardItem(reward2, out MergeRewardItem reward_2))
                    {
                        def.rewardItemList.Add(reward_2);
                    }
                    var strNext = value.GetString("Next", string.Empty);
                    if (!string.IsNullOrEmpty(strNext))
                    {
                        string[] array = strNext.Split(';');
                        if (array != null)
                        {
                            foreach (var id in array)
                            {
                                def.nextIdList.Add(id);
                            }
                        }
                    }
                    def.pictureName = value.GetString("Picture Name", string.Empty);
                    def.characterPic = value.GetString("CharacterPic", string.Empty);
                    def.Text = value.GetString("Text", string.Empty);


                    if (!allBranchdefDic.ContainsKey(def.branchType)) 
                    {
                        allBranchdefDic.Add(def.branchType, new Dictionary<string, BranchDefinition>());
                    }
                    if (allBranchdefDic.TryGetValue(def.branchType, out Dictionary<string, BranchDefinition> dic))
                    {
                        dic[def.id] = def;
                    }
                }
            }
            else
            {
                GameDebug.LogError("BranchDefinition::Init: Config is null.");
            }
        }      
    }
}
