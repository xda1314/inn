using BDUnity.Utils;
using ivy.game;
using System.Collections.Generic;

/// <summary>
/// 母产出池配表
/// </summary>
public class MonthPondConfig
{
    private static string configName = "MonthPond";

    //public static Dictionary<string, List<MonthPondConfig>> ParentPondDict { get; private set; } = new Dictionary<string, List<MonthPondConfig>>();
    //private static List<MonthPondConfig> ParentPondList = new List<MonthPondConfig>();
    //private static string parentPondId;

    public string sonPondID { get; private set; }
    public int min { get; private set; }
    public int max { get; private set; }
    public string advancedID { get; private set; }

    public static void Init()
    {
        //Dictionary<string, CustomJSONObject> dict = SingletonMono<GameConfig>.Instance.GetConfig(configName).dict;
        //if (dict != null && dict.Count == 1)
        //{
        //    Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
        //    enumerator.MoveNext();
        //    Dictionary<string, CustomJSONObject> dict2 = enumerator.Current.Value.dict;
        //    enumerator = dict2.GetEnumerator();
        //    var parentPondList = new List<MonthPondConfig>();
        //    while (enumerator.MoveNext())
        //    {
        //        Dictionary<string, CustomJSONObject> dict3 = enumerator.Current.Value.dict;

        //        if (dict3.TryGetValue("Pond", out CustomJSONObject monthPond_Json))
        //        {
        //            var monthPondConfig = new MonthPondConfig();
        //            if (!string.IsNullOrEmpty(monthPond_Json.ToString()) && monthPond_Json.ToString() != "0")
        //            {
        //                var parentPondId = monthPond_Json.ToString();
        //                parentPondList = new List<MonthPondConfig>();
        //                ParentPondDict.Add(parentPondId, parentPondList);
        //            }
        //            monthPondConfig.LoadBaseData(dict3);
        //            parentPondList.Add(monthPondConfig);
        //        }
        //    }
        //}
        //else
        //{
        //    GameDebug.LogError("MonthPond::Init: Config is null.");
        //}
    }
    private void LoadBaseData(Dictionary<string, CustomJSONObject> dataDic)
    {
        if (dataDic.TryGetValue("SonPondID", out CustomJSONObject sonPoundId_Json))
        {
            sonPondID = sonPoundId_Json.ToString();
        }
        if (dataDic.TryGetValue("Min", out CustomJSONObject min_Json))
        {
            if (int.TryParse(min_Json.ToString(), out int min))
            {
                this.min = min;
            }
        }
        if (dataDic.TryGetValue("Max", out CustomJSONObject max_Json))
        {
            if (int.TryParse(max_Json.ToString(), out int max))
            {
                this.max = max;
            }
        }
        if (dataDic.TryGetValue("AdvancedID", out CustomJSONObject advancedID_Json))
        {
            advancedID = advancedID_Json.ToString();
        }
    }
}
