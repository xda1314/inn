using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using ivy.game;

public class BookSaveData
{
    public string prefab;
    public MergeItemDiscoveryState state;
}

/// <summary>
/// 收藏存储
/// </summary>
public class BookSaveSystem
{
    public static BookSaveSystem Instance { get; private set; } = new BookSaveSystem();

    /// <summary>
    /// 所有的匹配链 < prefabname, MergeItemChain>
    /// </summary>
    public static Dictionary<string, BookSaveData> prefabDataDict { get; private set; } = new Dictionary<string, BookSaveData>();

    public void LoadData()
    {
        try
        {
            if (MergeItemDefinition.TotalDefinitionsDict.Count <= 0)
            {
                Debug.LogError("MergeItemDefinition.TotalDefinitionsDict is empty");
                return;
            }
            prefabDataDict.Clear();
            List<string> prefabNameList = MergeItemDefinition.TotalDefinitionsDict.Keys.ToList();
            for (int i = 0; i < prefabNameList.Count; i++)
            {
                if (!string.IsNullOrEmpty(prefabNameList[i]) && SaveUtils.HasKey(Consts.SaveKey_BookMap_Prefix + prefabNameList[i]))
                {
                    string bookStr = SaveUtils.GetString(Consts.SaveKey_BookMap_Prefix + prefabNameList[i]);

                    var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(bookStr);
                    if (dict != null)
                    {
                        BookSaveData saveData = new BookSaveData();
                        if (dict.TryGetValue("prefab", out object prefabName) && !string.IsNullOrEmpty(prefabName.ToString()))
                        {
                            saveData.prefab = prefabName.ToString();
                            if (dict.TryGetValue("state", out object stateObj))
                            {
                                switch (stateObj.ToString())
                                {
                                    case "undiscovered":
                                        saveData.state = MergeItemDiscoveryState.Undiscovered;
                                        break;
                                    case "unlock":
                                        saveData.state = MergeItemDiscoveryState.Unlock;
                                        break;
                                    case "discovered":
                                        saveData.state = MergeItemDiscoveryState.Discovered;
                                        break;
                                    default:
                                        Debug.LogError("无法解析" + stateObj.ToString());
                                        break;
                                }


                                //加入字典
                                if (prefabDataDict.TryGetValue(saveData.prefab, out var bookSaveData))
                                {
                                    Debug.LogError("BookSaveSystem LoadData has same" + saveData.prefab);
                                }
                                else
                                {
                                    prefabDataDict.Add(saveData.prefab, saveData);
                                }

                            }
                        }
                    }
                }

            }

        }
        catch (System.Exception e)
        {
            GameDebug.LogError(e);
        }

    }

    /// <summary>
    /// 保存解锁信息
    /// </summary>
    /// <param name="prefabName"></param>
    /// <param name="state"></param>
    public void SaveData(string prefabName, MergeItemDiscoveryState state)
    {
        if (state == MergeItemDiscoveryState.NONE || state == MergeItemDiscoveryState.Undiscovered)
        {
            return;
        }

        BookSaveData saveData = new BookSaveData();
        saveData.prefab = prefabName;
        saveData.state = state;
        prefabDataDict[saveData.prefab] = saveData;

        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict.Add("prefab", prefabName);
        switch (state)
        {
            case MergeItemDiscoveryState.NONE:
            case MergeItemDiscoveryState.Undiscovered:
                dict.Add("state", "undiscovered");
                break;
            case MergeItemDiscoveryState.Unlock:
                dict.Add("state", "unlock");
                break;
            case MergeItemDiscoveryState.Discovered:
                dict.Add("state", "discovered");
                break;
            default:
                Debug.LogError("BookSaveSystem saveData error state not set!" + state);
                break;
        }

        try
        {
            string json = JsonConvert.SerializeObject(dict);
            SaveUtils.SetString(Consts.SaveKey_BookMap_Prefix + prefabName, json);
        }
        catch (System.Exception e)
        {
            Debug.LogError("BookSaveSystem saveData error!" + e);
        }
    }

    #region 云存档
    public Dictionary<string, MergeItemDiscoveryState> GetSaveDataToFirestore()
    {
        Dictionary<string, MergeItemDiscoveryState> dict = new Dictionary<string, MergeItemDiscoveryState>();
        foreach (var item in prefabDataDict)
        {
            dict.Add(item.Value.prefab, item.Value.state);
        }
        return dict;
    }

    public void SetSaveDataFromFirestore(Dictionary<string, MergeItemDiscoveryState> firestoreDataDict)
    {
        try
        {
            if (firestoreDataDict == null)
            {
                GameDebug.LogError("bookUnlockState为空");
                return;
            }
            if (firestoreDataDict != null)
            {
                prefabDataDict.Clear();
                List<string> prefabNameList = MergeItemDefinition.TotalDefinitionsDict.Keys.ToList();
                for (int i = 0; i < prefabNameList.Count; i++)
                {
                    if (!string.IsNullOrEmpty(prefabNameList[i]))
                    {
                        if (firestoreDataDict.TryGetValue(prefabNameList[i], out var state))
                        {
                            SaveData(prefabNameList[i], state);
                        }
                        else
                        {
                            string key = Consts.SaveKey_BookMap_Prefix + prefabNameList[i];
                            if (SaveUtils.HasKey(key))
                            {
                                SaveUtils.DeleteKey(key);
                            }
                        }
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("BookSaveSystem ResetDataFromFirestore error" + e);
        }
    }
    #endregion
}
