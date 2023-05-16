using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using ivy.game;

public class DungeonShopSystem
{
    public static DungeonShopSystem Instance { get; private set; } = new DungeonShopSystem();
    public List<ShopPackData> dungeon1FuncDataList { get; private set; } = new List<ShopPackData>();
    public void InitSystem()
    {
        LoadShopData();
    }
    private void LoadShopData()
    {
        //读取存档
        if (SaveUtils.HasKey(Consts.SaveKey_Dungeon1Func))
        {
            string json = SaveUtils.GetString(Consts.SaveKey_Dungeon1Func);
            var list = JsonConvert.DeserializeObject<List<ShopPackData>>(json);
            if (list != null)
            {
                dungeon1FuncDataList = list;
            }
            ClearTimeoutData();
            SaveShopList();
        }
        else
        {
            Refreshdungeon1FuncData();
            SaveShopList();
        }
    }

    public void RemoveDateItem(ShopPackData shopPackData) 
    {
        dungeon1FuncDataList.Remove(shopPackData);
        SaveShopList();
    }

    public void SaveShopList()
    {
        try
        {
            string json = JsonConvert.SerializeObject(dungeon1FuncDataList);
            SaveUtils.SetString(Consts.SaveKey_Dungeon1Func, json);
        }
        catch (Exception e)
        {
            Debug.LogError("SaveFuncDataList error!" + e);
        }
    }

    ////清除过期时间数据
    //public void ClearTimeoutData()
    //{
    //    List<ShopPackData> shopPackDatas = new List<ShopPackData>();
    //    foreach (var item in dungeon1FuncDataList)
    //    {
    //        TimeSpan timeSpan = DateTimeOffset.FromUnixTimeSeconds(item.refreshTime) - DateTimeOffset.UtcNow;
    //        if (timeSpan.TotalSeconds < 0)
    //        {
    //            shopPackDatas.Add(item);
    //        }
    //    }
    //    foreach (var item in shopPackDatas)
    //    {
    //        dungeon1FuncDataList.Remove(item);
    //    }
    //}

    //固定时间刷新
    public void ClearTimeoutData()
    {
        if (GetIsNRefresh())
        {
            Refreshdungeon1FuncData();
            SaveShopList();
        }
    }

    private bool GetIsNRefresh() 
    {
        List<ShopPackData> shopPackDatas = new List<ShopPackData>();
        foreach (var item in dungeon1FuncDataList)
        {
            TimeSpan timeSpan = DateTimeOffset.FromUnixTimeSeconds(item.refreshTime) - DateTimeOffset.UtcNow;
            if (timeSpan.TotalSeconds < 0)
            {
                return true;
            }
        }
        return false;
    }

    private void Refreshdungeon1FuncData()
    {
        dungeon1FuncDataList.Clear();
        foreach (var item in Dungeon1Function.Definitions)
        {
            //if (RandomItemPondConfig.randomItemDict.TryGetValue(Convert.ToInt32(item.RandomItemPondID), out var pool))
            //{
            //    List<MergeRewardItem> mergeRewardItems = new List<MergeRewardItem>();
            //    mergeRewardItems.Add(pool.GetRandomRewardItem());
            //    long time = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + item.refreshTime;
            //    //long time = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 30;
            //    ShopPackData shopPackData = new ShopPackData(item.SaveID,
            //        mergeRewardItems,
            //        item.purchaseLimit,
            //        item.Cost,
            //        item.currencyID,
            //        "",
            //        time
            //        );
            //    dungeon1FuncDataList.Add(shopPackData);
            //}
        }
    }
}
