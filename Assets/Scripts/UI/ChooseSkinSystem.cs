using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using ivy.game;
using IvyCore;

public class ChooseSkinSystem
{
    public enum getSkinType
    {
        None,
        completeChapter,
        HalloweenParty,
        ChristmasParty,
        LoverParty,
    }
    private const string halloweenSpine = "halloween_Spine";
    private const string christmasSpine = "christmas_Spine";
    private const string loverSpine = "cjqrj_SkeletonData";
    public static ChooseSkinSystem Instance { get; private set; } = new ChooseSkinSystem();
    //存储数据
    public string curSkinName { get; private set; }
    public List<string> saveFestivalSkin = new List<string>();
    //--------
    private Dictionary<string, getSkinType> getSkinTypeDic = new Dictionary<string, getSkinType>();//皮肤获取途径
    public void InitSystem()
    {
        LoadData();
    }
    private void LoadData()
    {
        try
        {
            if (SaveUtils.HasKey(Consts.SaveKey_ChooseSkin))
            {
                curSkinName = SaveUtils.GetString(Consts.SaveKey_ChooseSkin);
            }
            else 
            {
                curSkinName = "cj00_SkeletonData";
            }
            if (SaveUtils.HasKey(Consts.SaveKey_ChooseFestivalSkin)) 
            {
                List<string> list = JsonConvert.DeserializeObject<List<string>>(SaveUtils.GetString(Consts.SaveKey_ChooseFestivalSkin));
                if (list != null && list.Count > 0) 
                {
                    saveFestivalSkin = list;
                }
            }
        }
        catch (Exception e) 
        {
            GameDebug.LogError(e);
        }
    }
    public void SaveData()
    {
        try
        {
            SaveUtils.SetString(Consts.SaveKey_ChooseSkin, curSkinName);
            if (saveFestivalSkin != null && saveFestivalSkin.Count > 0) 
            {
                SaveUtils.SetString(Consts.SaveKey_ChooseFestivalSkin, JsonConvert.SerializeObject(saveFestivalSkin));
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
    #region 云存档
    public Dictionary<string, object> GetSaveDataToFirestore() 
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("curName", curSkinName);
        dic.Add("festivalSkin", JsonConvert.SerializeObject(saveFestivalSkin));
        return dic;
    }
    public void SetSaveDataFromFirestore(Dictionary<string, object> dic)
    {
        if (dic == null) 
        {
            GameDebug.LogError("chooseSkinDic 数据为空");
        }
        if (dic != null && dic.Count > 0) 
        {
            if (dic.TryGetValue("curName", out object obj_Nmae)) 
            {
                curSkinName = obj_Nmae.ToString();
            }
            if (dic.TryGetValue("festivalSkin", out object obj_FestivalSkin))
            {
                List<string> list = JsonConvert.DeserializeObject<List<string>>(obj_FestivalSkin.ToString());
                if (list != null && list.Count > 0) 
                {
                    saveFestivalSkin.Clear();
                    saveFestivalSkin = list;
                }
            }
            SaveData();
        }
    }
    #endregion

    public void SetSkinByName(string skinName) 
    {
        if (ReturnAllSkinList().Contains(skinName)) 
        {
            curSkinName = skinName;
            UI_Manager.Instance.InvokeRefreshEvent("", "page_Play_RefreshBackground");
            SaveData();
        }
        else 
        {
            GameDebug.LogError("skin name error!" + skinName);
        }
    }
    public List<string> ReturnAllSkinList() 
    {
        getSkinTypeDic.Clear();
        //关卡解锁的背景
        List<string> skinNameList = new List<string>();
        foreach (var item in TaskGoalsDefinition.UnlockSkinDic) 
        {
            skinNameList.Add(item.Key);
            getSkinTypeDic.Add(item.Key, getSkinType.completeChapter);
        }
        //万圣节解锁的背景
        if (saveFestivalSkin.Contains(halloweenSpine) || FestivalSystem.Instance.IsFestivalOpen(MergeLevelType.halloween)) 
        {
            skinNameList.Add(halloweenSpine);
            getSkinTypeDic[halloweenSpine] = getSkinType.HalloweenParty;
        }
        //圣诞节解锁的背景
        if (saveFestivalSkin.Contains(christmasSpine) || FestivalSystem.Instance.IsFestivalOpen(MergeLevelType.christmas)) 
        {
            skinNameList.Add(christmasSpine);
            getSkinTypeDic[christmasSpine] = getSkinType.ChristmasParty;
        }
        //情人节解锁的背景
        if (saveFestivalSkin.Contains(loverSpine) || FestivalSystem.Instance.IsFestivalOpen(MergeLevelType.lover))
        {
            skinNameList.Add(loverSpine);
            getSkinTypeDic[loverSpine] = getSkinType.LoverParty;
        }

        return skinNameList;
    }
    public List<string> ReturnUnLockSkinList()
    {
        List<string> skinNameList = new List<string>();
        foreach (var item in TaskGoalsDefinition.UnlockSkinDic)
        {
            int curLevel = TaskGoalsManager.Instance.IsCompleteAllChapter ? TaskGoalsManager.Instance.curLevelIndex + 1 : TaskGoalsManager.Instance.curLevelIndex;
            if (curLevel > item.Value)//已切关
            {
                skinNameList.Add(item.Key);
            }
            else
            {
                break;
            }
        }
        if (saveFestivalSkin.Contains(halloweenSpine) && !skinNameList.Contains(halloweenSpine)) 
        {
            skinNameList.Add(halloweenSpine);
        }
        if (saveFestivalSkin.Contains(christmasSpine) && !skinNameList.Contains(christmasSpine)) 
        {
            skinNameList.Add(christmasSpine);
        }
        if (saveFestivalSkin.Contains(loverSpine) && !skinNameList.Contains(loverSpine))
        {
            skinNameList.Add(loverSpine);
        }
        return skinNameList;
    }
    public getSkinType ReturnGetSkinTypeByName(string skinName)
    {
        if (getSkinTypeDic.TryGetValue(skinName, out getSkinType type))
        {
            return type;
        }
        else 
        {
            return getSkinType.None;
        }
    }
    public void SetFestivalSkin(MergeLevelType levelType) 
    {
        switch (levelType)
        {
            case MergeLevelType.halloween:
                if (!saveFestivalSkin.Contains(halloweenSpine)) 
                {
                    saveFestivalSkin.Add(halloweenSpine);
                    SetSkinByName(halloweenSpine);
                }              
                break;
            case MergeLevelType.christmas:
                if (!saveFestivalSkin.Contains(christmasSpine))
                {
                    saveFestivalSkin.Add(christmasSpine);
                    SetSkinByName(christmasSpine);
                }
                break;
            case MergeLevelType.lover:
                if (!saveFestivalSkin.Contains(loverSpine))
                {
                    saveFestivalSkin.Add(loverSpine);
                    SetSkinByName(loverSpine);
                }
                break;
            default:
                break;
        }
        SaveData();
    }
}
