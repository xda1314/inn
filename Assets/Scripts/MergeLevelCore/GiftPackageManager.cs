using ivy.game;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 礼包管理
/// </summary>
public class GiftPackageManager
{
    public static GiftPackageManager Instance { get; private set; } = new GiftPackageManager();

    public List<int> GiftIndexList { get; private set; } = new List<int>();//当前所有要推送的礼包id(顺序推送)
    public DateTimeOffset PushGiftEndTime { get; private set; } = DateTimeOffset.MinValue;//当前礼包结束的时间

    /// <summary>
    /// 获取推送礼包数据
    /// </summary>
    public void ManagerInit() 
    {
        LoadData();
    }

    /// <summary>
    /// 完成相应关卡 添加礼包
    /// </summary>
    public void CompleteTaskGift(int levelIndex)
    {
        foreach (var item in GiftPackageDefinition.GiftDefinitionsDict)
        {
            if (item.Value.Unlock == levelIndex)
            {
                if(!GiftIndexList.Contains(item.Key))
                    GiftIndexList.Add(item.Key);
            }
        }
        if (GiftIndexList.Count <= 0)
            return;
        //推送礼包列表中的第一个礼包
        GiftPackageDefinition.GiftDefinitionsDict.TryGetValue(GiftIndexList[0], out GiftPackageDefinition definition);
        if (PushGiftEndTime < TimeManager.Instance.UtcNow())
        {
            PushGiftEndTime = TimeManager.Instance.UtcNow().AddSeconds(definition.DisappearTime);
        }
        SaveData();
    }

    public bool GetIsPop()
    {
        if (GiftIndexList.Count > 0) 
        {
            GiftPackageDefinition.GiftDefinitionsDict.TryGetValue(GiftIndexList[0], out GiftPackageDefinition definition);
            if (PushGiftEndTime < TimeManager.Instance.UtcNow())
            {
                PushNextGiftPackage();
            }
            if (GiftIndexList.Count > 0)
            {
                return true;
            }
        }
        return false;
    }


    public GiftPackageDefinition GetGiftPackage() 
    {
        //推送礼包列表中的第一个礼包
        GiftPackageDefinition.GiftDefinitionsDict.TryGetValue(GiftIndexList[0], out GiftPackageDefinition definition);
        return definition;
    }

    /// <summary>
    /// 当前推送的礼包被购买或者倒计时结束未购买时，如果有其他礼包推送下一个礼包
    /// </summary>
    public void PushNextGiftPackage()
    {
        GiftIndexList.RemoveAt(0);
        PushGiftEndTime = DateTimeOffset.MinValue;
        if (GiftIndexList.Count > 0) 
        {
            //下一个礼包信息
            GiftPackageDefinition.GiftDefinitionsDict.TryGetValue(GiftIndexList[0], out GiftPackageDefinition definition);
            PushGiftEndTime = TimeManager.Instance.UtcNow().AddSeconds(definition.DisappearTime);
        }
        SaveData();
    }

    public bool GiftPackageExit(GiftPackageType giftPackageType) 
    {
        foreach (var item in GiftIndexList)
        {
            GiftPackageDefinition.GiftDefinitionsDict.TryGetValue(item, out GiftPackageDefinition definition);
            if (definition.GiftType== giftPackageType) 
                return true;
        }
        return false;
    }

    /// <summary>
    /// 打开不同的礼包界面
    /// </summary>
    public void OpenGiftPackageView()
    {
        GiftPackageDefinition.GiftDefinitionsDict.TryGetValue(GiftIndexList[0], out GiftPackageDefinition definition);
        switch (definition.GiftType)
        {
            case GiftPackageType.NovicePackage:
                UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_NovicePackage2));
                //if (string.IsNullOrEmpty(RemoteConfigSystem.Instance.GetRemoteConfig_String(RemoteConfigSystem.remoteKey_StarterPackage)))
                //{                    
                //    UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_NovicePackage2));
                //}
                //else 
                //{
                //    UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_NovicePackage));
                //}             
                break;
            case GiftPackageType.DressUpPackage:
                UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_DressUpPackage, UIShowLayer.TopResouce));
                break;
            case GiftPackageType.GourmetPackage:
                UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_GourmetPackage));
                break;
            case GiftPackageType.General1Package:
                UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_General1Package));
                break;
            case GiftPackageType.General2Package:
                UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_General2Package));
                break;
            case GiftPackageType.General3Package:
                UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_General3Package));
                break;
            case GiftPackageType.General4Package:
                UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_General4Package));
                break;
            default:
                Debug.LogError("该礼包还未做" + definition.GiftType);
                break;
        }
    }


    #region Save And Load
    public void SaveData()
    {
        try
        {
            SaveGiftPackageData data = new SaveGiftPackageData();
            data.GiftIndexList = GiftIndexList;
            data.PushGiftEndTime = PushGiftEndTime.ToString();

            string endStr = JsonConvert.SerializeObject(data);
            SaveUtils.SetString(Consts.SaveKey_GiftPackage, endStr);          
        }
        catch (Exception e)
        {
            Debug.LogError("SaveData error!" + e);
        }
    }

    public void LoadData()
    {
        try
        {
#if UNITY_IOS
            //if (RemoteConfigSystem.Instance.GetRemoteConfig_Int(RemoteConfigSystem.remoteKey_display_all_ios_offers) == 1)
            //{
            //    GameManager.Instance.uIPanel_LevelSelect.ShowGiftBagBtnClick();
            //}
#endif

            if (SaveUtils.HasKey(Consts.SaveKey_GiftPackage))
            {
                string saveStr = SaveUtils.GetString(Consts.SaveKey_GiftPackage);
                if (string.IsNullOrEmpty(saveStr))
                {
                    return;
                }
                var data = JsonConvert.DeserializeObject<SaveGiftPackageData>(saveStr);
                if (data == null)
                {
                    return;
                }
                GiftIndexList = data.GiftIndexList;
                DateTimeOffset.TryParse(data.PushGiftEndTime.ToString(), out DateTimeOffset endTime);
                if (endTime != null)
                    PushGiftEndTime = endTime;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("LoadData error!" + e);
        }
    }
    #endregion
    #region 云存档
    public Dictionary<string, object> GetSaveDataToFirestore()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("indexList", JsonConvert.SerializeObject(GiftIndexList));
        dic.Add("endTime", PushGiftEndTime.ToString());
        return dic;
    }
    public void SetSaveDataFromFirestore(Dictionary<string, object> dic)
    {
        if (dic == null)
        {
            GameDebug.LogError("giftPackage数据为空");
            return;
        }
        if (dic.TryGetValue("indexList", out object indexList)) 
        {
            GiftIndexList = JsonConvert.DeserializeObject<List<int>>(indexList.ToString());
        }
        if (dic.TryGetValue("endTime", out object endTimeStr)) 
        {
            if (DateTimeOffset.TryParse(endTimeStr.ToString(), out DateTimeOffset endTime)) 
            {
                PushGiftEndTime = endTime;
            }
        }
        if (GiftIndexList.Count > 0 && PushGiftEndTime != DateTimeOffset.MinValue)
            SaveData();
    }
    #endregion
}




public class SaveGiftPackageData
{
    public List<int> GiftIndexList;
    public string PushGiftEndTime;
}
