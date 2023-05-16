// ILSpyBased#2
using BDUnity.Utils;
using ivy.game;
using System.Collections.Generic;

public class SpinWheelDefinition
{
    private const string k_DataResourceFileName = "MergeSpinWheelConfig";

    public static Dictionary<int, SpinWheelDefinition> DefinitionDict = new Dictionary<int, SpinWheelDefinition>();

    public int id;

    public static int cost;
    public int saveID;
    public string pondID;
    public int weight;
    public MergeRewardItemPool itemPool;

    private int unlock = -1;
    private int close = 99;
    public bool Isunlock => TaskGoalsManager.Instance.curLevelIndex >= unlock;
    public bool IsClose => TaskGoalsManager.Instance.curLevelIndex >= close;

    private void Reset()
    {

    }
    public static void Init()
    {
        Dictionary<string, CustomJSONObject> dict = SingletonMono<GameConfig>.Instance.GetConfig(k_DataResourceFileName).dict;
        if (dict != null && dict.Count == 1)
        {
            Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
            enumerator.MoveNext();
            Dictionary<string, CustomJSONObject> dict2 = enumerator.Current.Value.dict;
            enumerator = dict2.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Dictionary<string, CustomJSONObject> dict3 = enumerator.Current.Value.dict;
                SpinWheelDefinition spinWheelDefinition = new SpinWheelDefinition();
                if (dict3.TryGetValue("ID", out CustomJSONObject id_Json))
                {
                    if (int.TryParse(id_Json.ToString(), out int id))
                    {
                        spinWheelDefinition.id = id;
                    }
                    spinWheelDefinition.LoadBaseData(dict3);
                    DefinitionDict.Add(spinWheelDefinition.id, spinWheelDefinition);
                }
            }
        }
        else
        {
            GameDebug.LogError("MergeSpinWheelConfig::Init: Config is null.");
        }
    }
    private void LoadBaseData(Dictionary<string, CustomJSONObject> dataDic)
    {
        if (dataDic.TryGetValue("Save ID", out CustomJSONObject saveId_Json))
        {
            if (int.TryParse(saveId_Json.ToString(), out int saveID))
            {
                this.saveID = saveID;
            }
        }
        if (dataDic.TryGetValue("SetID", out CustomJSONObject setId_Json))
        {
            pondID = setId_Json.ToString();
        }
        if (dataDic.TryGetValue("weight", out CustomJSONObject weight_Json))
        {
            if (int.TryParse(weight_Json.ToString(), out int weight))
            {
                this.weight = weight;
            }
        }
        if (dataDic.TryGetValue("Cost", out CustomJSONObject cost_Json))
        {
            string[] array = cost_Json.ToString().Split(',');
            if (array.Length == 2 && int.TryParse(array[1], out int cost))
            {
                SpinWheelDefinition.cost = cost;
            }
        }

        if (PoolDefinition.PoolDefinitionDict.TryGetValue(pondID, out var mergeRewardItemPool))
        {
            itemPool = mergeRewardItemPool;
        }

        if (dataDic.TryGetValue("Unlock", out CustomJSONObject unlock_Json))
        {
            if (int.TryParse(unlock_Json.ToString(), out int unlock))
            {
                this.unlock = unlock;
            }
        }

        if (dataDic.TryGetValue("Close", out CustomJSONObject close_Json))
        {
            if (int.TryParse(close_Json.ToString(), out int close))
            {
                this.close = close;
            }
        }
    }
}
