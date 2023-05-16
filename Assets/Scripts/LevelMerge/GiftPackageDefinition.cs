// ILSpyBased#2
using BDUnity;
using BDUnity.Utils;
using ivy.game;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GiftPackageDefinition
{
    private static string configName = "MergeGiftBagConfig";
    public static Dictionary<int, GiftPackageDefinition> GiftDefinitionsDict { get; private set; } = new Dictionary<int, GiftPackageDefinition>();
    public int GiftId { get; set; }
    public GiftPackageType GiftType { get; set; }
    public string GiftName { get; set; }
    public string GiftViewIcon { get; set; }//礼包界面的人物icon
    public string MainViewIcon { get; set; }//主界面礼包入口的icon
    public int Unlock { get; set; }  //第几关任务结束之后弹礼包
    public List<MergeRewardItem> CoinOrGemRewardList { get; set; } = new List<MergeRewardItem>();//金币钻石奖励
    public List<MergeRewardItem> ItemRewardList { get; set; } = new List<MergeRewardItem>();//物品奖励
    public string SaveID { get; set; }
    public string UnityID { get; set; }
    public string GooglePlayID { get; set; }
    public string AppleID { get; set; }
    public float OriginalPrice { get; set; }//原价
    public float Cost { get; set; }
    public int DisappearTime { get; set; }
    private void Reset()
    {

    }
    public static void Init()
    {
        Dictionary<string, CustomJSONObject> dict = SingletonMono<GameConfig>.Instance.GetConfig(configName).dict;
        if (dict != null && dict.Count == 1)
        {
            Dictionary<string, CustomJSONObject>.Enumerator enumerator = dict.GetEnumerator();
            enumerator.MoveNext();
            Dictionary<string, CustomJSONObject> dict2 = enumerator.Current.Value.dict;
            enumerator = dict2.GetEnumerator();
            while (enumerator.MoveNext())
            {
                GiftPackageDefinition giftPackageDefinition = new GiftPackageDefinition();
                Dictionary<string, CustomJSONObject> dict3 = enumerator.Current.Value.dict;
                if (dict3.TryGetValue("ID", out CustomJSONObject id_Json))
                {
                    if (int.TryParse(id_Json.ToString(), out int id)) 
                    {
                        giftPackageDefinition.GiftId = id;
                        giftPackageDefinition.LoadBaseData(dict3);
                        GiftDefinitionsDict.Add(id, giftPackageDefinition);
                    }
                }
                else
                {
                    GameDebug.LogError("this data without key: 'ID'");
                }
            }
        }
        else
        {
            GameDebug.LogError("MergeGiftBagConfig::Init: Config is null.");
        }
    }
    private void LoadBaseData(Dictionary<string, CustomJSONObject> dataDic)
    {
        if (dataDic.TryGetValue("ID", out CustomJSONObject id_Ison))
        {
            if (int.TryParse(id_Ison.ToString(), out int giftId))
            {
                GiftId = giftId;
            }
        }
        if (dataDic.TryGetValue("BagType", out CustomJSONObject bagType_Json))
        {
            switch (bagType_Json.ToString())
            {
                case "MonthlyCard":
                    GiftType = GiftPackageType.MonthlyCard;
                    break;
                case "novice":
                    GiftType = GiftPackageType.NovicePackage;
                    break;
                case "novice2":
                    GiftType = GiftPackageType.NovicePackage;
                    break;
                case "dressUp":
                    GiftType = GiftPackageType.DressUpPackage;
                    break;
                case "gourmet":
                    GiftType = GiftPackageType.GourmetPackage;
                    break;
                case "general_1":
                    GiftType = GiftPackageType.General1Package;
                    break;
                case "general_2":
                    GiftType = GiftPackageType.General2Package;
                    break;
                case "general_3":
                    GiftType = GiftPackageType.General3Package;
                    break;
                case "general_4":
                    GiftType = GiftPackageType.General4Package;
                    break;
                case "usual":
                    GiftType = GiftPackageType.CommomPackage;
                    break;
                case "supreme":
                    GiftType = GiftPackageType.SupremePackage;
                    break;
                case "daily":
                    GiftType = GiftPackageType.Daily;
                    break;
                case "Dundaily":
                    GiftType = GiftPackageType.Daily;
                    break;
                case "dungeon1":
                    GiftType = GiftPackageType.Dungeon1;
                    break;
                case "dungeon2":
                    GiftType = GiftPackageType.Dungeon2;
                    break;
                case "dungeon3":
                    GiftType = GiftPackageType.Dungeon3;
                    break;
                case "weeklyCard":
                    GiftType = GiftPackageType.WeeklyCard;
                    break;
                case "push1":
                    GiftType = GiftPackageType.Push1Package;
                    break;
                default:
                    //GameDebug.LogError("[GiftPackageDefinition] 没有该礼包的类型:" + bagType_Json.ToString());
                    break;
            }
        }
        if (dataDic.TryGetValue("PagName", out CustomJSONObject pagName_Json))
        {
            GiftName = pagName_Json.ToString();
        }
        if (dataDic.TryGetValue("PagPic", out CustomJSONObject pagPic_Json))
        {
            GiftViewIcon = pagPic_Json.ToString();
        }
        if (dataDic.TryGetValue("PagEntrancePic", out CustomJSONObject pagEntranceOic_Json))
        {
            MainViewIcon = pagEntranceOic_Json.ToString();
        }
        if (dataDic.TryGetValue("Unlock", out CustomJSONObject unlock_Json))
        {
            if (int.TryParse(unlock_Json.ToString(), out int unlock))
            {
                this.Unlock = unlock;
            }
        }
        if (dataDic.TryGetValue("Save ID", out CustomJSONObject saveId_Json))
        {
            SaveID = saveId_Json.ToString();
        }
        if (dataDic.TryGetValue("Unity ID", out CustomJSONObject unityId_Json))
        {
            UnityID = unityId_Json.ToString();
        }
        if (dataDic.TryGetValue("Reward1", out CustomJSONObject Reward1_Json))
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
                    CoinOrGemRewardList.Add(data);
                }
            }
        }
        if (dataDic.TryGetValue("Reward2", out CustomJSONObject Reward2_Json))
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
                    ItemRewardList.Add(data);
                }
            }
        }
        if (dataDic.TryGetValue("Reward3", out CustomJSONObject Reward3_Json))
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
                    ItemRewardList.Add(data);
                }
            }
        }
        if (dataDic.TryGetValue("Reward4", out CustomJSONObject Reward4_Json))
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
                    ItemRewardList.Add(data);
                }
            }
        }
        if (dataDic.TryGetValue("Reward5", out CustomJSONObject Reward5_Json))
        {
            string reward = Reward5_Json.ToString();
            string[] strs = reward.Split(';');
            for (int i = 0; i < strs.Length; i++)
            {
                string[] array = strs[i].Split(',');
                if (array.Length == 2 && array[0] != null && int.TryParse(array[1], out int num))
                {
                    MergeRewardItem data = new MergeRewardItem();
                    data.name = array[0];
                    data.num = num;
                    ItemRewardList.Add(data);
                }
            }
        }
        if (dataDic.TryGetValue("Reward6", out CustomJSONObject Reward6_Json))
        {
            string reward = Reward6_Json.ToString();
            string[] strs = reward.Split(';');
            for (int i = 0; i < strs.Length; i++)
            {
                string[] array = strs[i].Split(',');
                if (array.Length == 2 && array[0] != null && int.TryParse(array[1], out int num))
                {
                    MergeRewardItem data = new MergeRewardItem();
                    data.name = array[0];
                    data.num = num;
                    ItemRewardList.Add(data);
                }
            }
        }
        if (dataDic.TryGetValue("Reward7", out CustomJSONObject Reward7_Json))
        {
            string reward = Reward7_Json.ToString();
            string[] strs = reward.Split(';');
            for (int i = 0; i < strs.Length; i++)
            {
                string[] array = strs[i].Split(',');
                if (array.Length == 2 && array[0] != null && int.TryParse(array[1], out int num))
                {
                    MergeRewardItem data = new MergeRewardItem();
                    data.name = array[0];
                    data.num = num;
                    ItemRewardList.Add(data);
                }
            }
        }
        if (dataDic.TryGetValue("GooglePlayID", out CustomJSONObject googlePlayId_Json))
        {
            GooglePlayID = googlePlayId_Json.ToString();
        }
        if (dataDic.TryGetValue("AppleID", out CustomJSONObject appleId_Json))
        {
            AppleID = appleId_Json.ToString();
        }
        if (dataDic.TryGetValue("OriginalPrice", out CustomJSONObject originalPrice_Json))
        {
            if (float.TryParse(originalPrice_Json.ToString(), out float originalPrice))
            {
                OriginalPrice = originalPrice;
            }
        }
        if (dataDic.TryGetValue("Cost", out CustomJSONObject cost_Json))
        {
            if (float.TryParse(cost_Json.ToString(), out float cost))
            {
                Cost = cost;
            }
        }
        if (dataDic.TryGetValue("Continued", out CustomJSONObject continued_Json))
        {
            if (int.TryParse(continued_Json.ToString(), out int continued))
            {
                DisappearTime = continued;
            }
        }
    }
}

public enum GiftPackageType
{
    WeeklyCard,
    MonthlyCard,//月卡
    NovicePackage,//新手礼包
    DressUpPackage,//装扮礼包
    GourmetPackage,
    General1Package,
    General2Package,
    General3Package,
    General4Package,
    Push1Package,
    CommomPackage,//普通礼包
    SupremePackage,//至尊礼包
    Daily,//日常礼包
    Dundaily,//副本礼包
    Dungeon1,
    Dungeon2,
    Dungeon3,
}
