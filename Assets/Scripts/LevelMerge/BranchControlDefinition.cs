using System;
using System.Collections.Generic;

public class BranchControlDefinition
{
    private static Dictionary<string, string> configDic = new Dictionary<string, string>()
    {
        { "ID","1001"},
        { "Version","1"},
        { "ActivityType","SpurLine1"},
        { "Title","Obj/SpurLine/SpurLine1Title"},
        { "Des","Obj/SpurLine/SpurLine1Describe"}
    };
    private static Dictionary<string, string> configDic1 = new Dictionary<string, string>()
    {
        { "ID","1002"},
        { "Version","2"},
        { "ActivityType","branch_halloween"},
        { "Title","Obj/SpurLine/SpurLine2Title"},
        { "Des","Obj/SpurLine/SpurLine2Describe"}
    };
    private static Dictionary<string, string> configDic2 = new Dictionary<string, string>()
    {
        { "ID","1003"},
        { "Version","3"},
        { "ActivityType","branch_christmas"},
        { "Title","Obj/SpurLine/SpurLine3Title"},
        { "Des","Obj/SpurLine/SpurLine3Describe"}
    };
    private static Dictionary<string, string> configDic3 = new Dictionary<string, string>()
    {
        { "ID","1004"},
        { "Version","4"},
        { "ActivityType","SpurLine4"},
        { "Title","Obj/SpurLine/SpurLine4Title"},
        { "Des","Obj/SpurLine/SpurLine4Describe"}
    };
    private static Dictionary<string, string> configDic4 = new Dictionary<string, string>()
    {
        { "ID","1005"},
        { "Version","5"},
        { "ActivityType","SpurLine5"},
        { "Title","Obj/SpurLine/SpurLine5Title"},
        { "Des","Obj/SpurLine/SpurLine5Describe"}
    };
    private static Dictionary<string, string> configDic5 = new Dictionary<string, string>()
    {
        { "ID","1006"},
        { "Version","6"},
        { "ActivityType","SpurLine6"},
        { "Title","Obj/SpurLine/SpurLine6Title"},
        { "Des","Obj/SpurLine/SpurLine6Describe"}
    };
    public string BranchId { get; private set; }
    public int BranchVersion { get; private set; }
    public MergeLevelType BranchType { get; private set; }
    public String Title { get; private set; }
    public String Des { get; private set; }

    /// <summary>
    /// 所有支线活动
    /// </summary>
    public static Dictionary<int, BranchControlDefinition> TotalBranchCnfDict = new Dictionary<int, BranchControlDefinition>();

    /// <summary>
    /// 开启的支线活动
    /// </summary>
    public static List<int> OpenBranchList = new List<int>() {4,5,6};

    public static void LoadDefinition()
    {
        BranchControlDefinition def5 = new BranchControlDefinition();
        def5.LoadBaseData(configDic5);
        BranchControlDefinition def4 = new BranchControlDefinition();
        def4.LoadBaseData(configDic4);
        BranchControlDefinition def3 = new BranchControlDefinition();
        def3.LoadBaseData(configDic3);
        BranchControlDefinition def2 = new BranchControlDefinition();
        def2.LoadBaseData(configDic2);
        BranchControlDefinition def1 = new BranchControlDefinition();
        def1.LoadBaseData(configDic1);
        BranchControlDefinition def = new BranchControlDefinition();
        def.LoadBaseData(configDic);
    }

    private void LoadBaseData(Dictionary<string, string> dataDic)
    {
        if (dataDic.TryGetValue("ID", out string id_Json))
        {
            BranchId = id_Json;
        }
        if (dataDic.TryGetValue("Version", out string version_Json))
        {
            if (!string.IsNullOrEmpty(version_Json) && int.TryParse(version_Json, out var tempNum))
            {
                BranchVersion = tempNum;
            }
        }
        if (dataDic.TryGetValue("ActivityType", out string type_Json))
        {
            if (!string.IsNullOrEmpty(type_Json))
            {
                if (type_Json.Equals("SpurLine1", StringComparison.OrdinalIgnoreCase))
                    BranchType = MergeLevelType.branch1;
                else if (type_Json.Equals("branch_halloween", StringComparison.OrdinalIgnoreCase))
                    BranchType = MergeLevelType.branch_halloween;
                else if (type_Json.Equals("branch_christmas", StringComparison.OrdinalIgnoreCase))
                    BranchType = MergeLevelType.branch_christmas;
                else if (type_Json.Equals("SpurLine4", StringComparison.OrdinalIgnoreCase))
                    BranchType = MergeLevelType.branch_SpurLine4;
                else if (type_Json.Equals("SpurLine5", StringComparison.OrdinalIgnoreCase))
                    BranchType = MergeLevelType.branch_SpurLine5;
                else if (type_Json.Equals("SpurLine6", StringComparison.OrdinalIgnoreCase))
                    BranchType = MergeLevelType.branch_SpurLine6;
                else
                {
                    GameDebug.LogError("支线活动类型错误!");
                    return;
                }
            }
            else
            {
                GameDebug.LogError("支线活动类型错误!");
                return;
            }
        }
        if (dataDic.TryGetValue("Title", out string Title_Json))
        {
            Title = Title_Json;
        }
        if (dataDic.TryGetValue("Des", out string Des_Json))
        {
            Des = Des_Json;
        }
        TotalBranchCnfDict[BranchVersion] = this;
    }
}
