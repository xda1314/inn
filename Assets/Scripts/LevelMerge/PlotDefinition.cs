using BDUnity.Utils;
using ivy.game;
using System.Collections.Generic;

public class PlotDefinition
{
    private const string k_DataResourceFileName = "MergeDungeonDrama";
    public static Dictionary<MergeLevelType, List<PlotDefinition>> plotDefinitionDic = new Dictionary<MergeLevelType, List<PlotDefinition>>();
    private static List<PlotDefinition> plotList = new List<PlotDefinition>();

    public MergeLevelType dungeonType { get; private set; }
    public string plotId { get; private set; }
    public string plotTitle { get; private set; }
    public string plotName { get; private set; }
    public string plotDesc { get; private set; }
    public string unlockPrefab { get; private set; }


    public static void Init() 
    {
        Dictionary<string, CustomJSONObject> dict = GameConfig.Instance.GetConfig(k_DataResourceFileName).dict;
        if (dict != null && dict.Count == 1)
        {
            Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
            enumerator.MoveNext();
            Dictionary<string, CustomJSONObject> dict2 = enumerator.Current.Value.dict;
            enumerator = dict2.GetEnumerator();
            while (enumerator.MoveNext())
            {
                CustomJSONObject value = enumerator.Current.Value;
                PlotDefinition def = new PlotDefinition();
                string dungeonType = value.GetString("DungeonType", string.Empty);
                switch (dungeonType) 
                {
                    case "dungeon1":
                        def.dungeonType = MergeLevelType.dungeon1;
                        break;
                    case "dungeon2":
                        def.dungeonType = MergeLevelType.dungeon2;
                        break;
                    case "dungeon3":
                        def.dungeonType = MergeLevelType.dungeon3;
                        break;
                    default:
                        GameDebug.LogError("副本类型错误，错误类型：" + dungeonType);
                        break;
                }
                def.plotId = value.GetString("ID", string.Empty);
                def.plotTitle = value.GetString("Drama", string.Empty);
                def.plotName = value.GetString("DramaTitle", string.Empty);
                def.plotDesc = value.GetString("DramaDescribe", string.Empty);
                def.unlockPrefab = value.GetString("UnlockPrefab", string.Empty);
                plotList.Add(def);
            }
        }
        else
        {
            GameDebug.LogError("PlotDefinition::Init: Config is null.");
        }
        for (int i = 0; i < plotList.Count; i++)
        {
            if (!plotDefinitionDic.ContainsKey(plotList[i].dungeonType)) 
            {
                List<PlotDefinition> list = new List<PlotDefinition>();
                plotDefinitionDic.Add(plotList[i].dungeonType, list);
            }
            if (plotDefinitionDic.TryGetValue(plotList[i].dungeonType, out List<PlotDefinition> curList)) 
            {
                curList.Add(plotList[i]);
            }
        }

    }

}
