using BDUnity.Utils;
using ivy.game;
using Ivy.Addressable;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品配置信息
/// </summary>
public class MergeItemDefinition
{
    private static string k_DataResourceFileName = "MergeObjectConfig";

    public static Dictionary<string, MergeItemDefinition> TotalDefinitionsDict { get; private set; } = new Dictionary<string, MergeItemDefinition>();
    public static Dictionary<int, string> ObjCodeDict { get; private set; } = new Dictionary<int, string>();
    public static Dictionary<MergeItemCategoryType, List<MergeItemDefinition>> TotalCategoryObjects { get; private set; } = new Dictionary<MergeItemCategoryType, List<MergeItemDefinition>>();

    public static Dictionary<string, List<string>> saveAllSpawn { get; private set; } = new Dictionary<string, List<string>>();


    #region 字段
    public int ItemId { get; private set; }
    public string PondId { get; private set; }
    public string PrefabName { get; private set; }// 预制体名称
    public int ContainerChainLevel { get; private set; }//容器链加入的等级
    public int OutputChainLevel { get; private set; }//产出链加入的等级
    public int Level { get; private set; }//等级，匹配链位置 
    public ItemRarityType RarityType { get; private set; }// 物品属性
    public MergeRewardItem BookUnlockReward { get; private set; }// 收藏解锁奖励
    public int PrefabSpecialDisplay { get; private set; } = 0;// 收藏界面图鉴解锁显示（1表示1个＋；2表示++；3表示+++；4表示最佳
    public MergeItemCategoryType CategoryType { get; private set; }//就是以前的itemtype
    public MergeItemWindowType WindowType { get; private set; }//物品窗口类型

    public int Hierarchy { get; private set; }  //物品层级
    public bool CanSelect { get; private set; }
    public bool CanMove { get; private set; }
    public bool CanMerge { get; private set; }
    public bool NeedEnergy { get; private set; }

    public int PriceCoins { get; private set; }//在商店中的金币价格
    public int PriceGems { get; private set; }//在商店中的钻石价格

    public int SellValue { get; private set; }
    public int BubbleUnlockCost { get; private set; }//泡泡解锁价格
    public int UnLockCost { get; private set; }//冰冻解锁价格

    public string MergeOutputPrefab { get; private set; }// 默认合成物品名称
    public float BubbleEqul { get; private set; }//产生泡泡概率
    public int DieTime { get; private set; }//泡泡破碎时间
    public string BubbleDieOutputPrefab { get; private set; }// 泡泡倒计时结束未花钱解锁产生物体预制体

    /// <summary>
    /// -1 为空值，即表里没有此值
    /// </summary>
    public int BoxOpenTime { get; private set; }//宝箱打开延迟时间

    public List<MergeRewardItem> fixedSpawnList { get; private set; }// 固定产出池

    public int FinishChargeCost { get; private set; }// 充能花费
    public int Rareness { get; private set; }// 获取难度
    public int CanSpawnCountByOneCharge { get; private set; }// 1次充能产出数量
    public int TotalChargeCount { get; private set; } //累计充能次数
    public int ChargeLoopCDSecond
    {
        get
        {
            var loopSec = _chargeLoopCDSecond;
            if (BattlePassSystem.Instance.IsPay)
                loopSec /= 2;
            return loopSec;
        }
    } //每次充能时间间隔
    private int _chargeLoopCDSecond = 0;

    public int CanSpawnCountByOneCharge_Auto { get; private set; }// 1次充能自动产出数量
    public int TotalChargeCount_Auto { get; private set; } //自动产出道具累计充能次数
    public int ChargeLoopCDSecond_Auto
    {
        get
        {
            var loopSec = _chargeLoopCDSecond_Auto;
            if (BattlePassSystem.Instance.IsPay)
                loopSec /= 2;
            return loopSec;
        }
    } //自动产出道具每次充能时间间隔
    private int _chargeLoopCDSecond_Auto = 0;
    public string autoSpawnPrefab { get; private set; } //自动产出道具产出物品

    public bool NeedShowChargeIcon { get; private set; } //是否显示充能图标
    public bool NeedShowParticle { get; private set; }   //是否显示星星特效
    public bool NeedShowSpawnEffect { get; private set; }   //是否显示产出特效

    public bool NeedJoinCollection { get; private set; }   //是否加入图鉴

    public string DieCreatePrefab { get; private set; } //死亡变成什么
    public bool IsSpawnPrefabWithDie => !string.IsNullOrEmpty(DieCreatePrefab);

    public List<string> swallowPondIdList { get; private set; } = new List<string>();//可被吞噬道具池ID
    public List<int> swallowCountList { get; private set; } = new List<int>();//对应池子物品所需吞噬数量
    public bool NeedShowSwallowIcon { get; private set; } //是否显示吞噬图标

    public List<string> originList { get; private set; } = new List<string>(); //由哪些物品产出 
    public MergeRewardItem Strength { get; private set; }
    public string OnTapCustomScript { get; private set; }

    public string SameKind { get; private set; }

    public string locKey_Name { get; private set; }
    public string locKey_Description { get; private set; }
    public string locKey_Chain { get; private set; }
    public string locKey_Output { get; private set; }
    public int SpeedUpSenconds { get; private set; } //加速道具参数

    //状态可转换的有限产出道具
    public int ModelSwitchSpawnCount1 { get; private set; }
    public int ModelSwitchSpawnCount2 { get; private set; }
    public string ModelSwitchPondID1 { get; private set; }
    public string ModelSwitchPondID2 { get; private set; }
    public int ModelSwitchCDTime { get; private set; }
    //battlepass经验
    public int BatterPassExp { get; private set; }
    //taskBox产出池子，key-关卡
    public Dictionary<int, string> taskBoxPondDict = new Dictionary<int, string>();
    #endregion


    // 物体名称
    public string ItemName => !string.IsNullOrEmpty(locKey_Name) ? locKey_Name : string.Empty;
    public string ItemDescription => !string.IsNullOrEmpty(locKey_Description) ? I2.Loc.ScriptLocalization.Get(locKey_Description) : string.Empty;

    //匹配链ID
    public string ChainID => locKey_Chain;
    public bool HasChain => !string.IsNullOrEmpty(locKey_Chain);
    //匹配链名称
    public string ChainName => !string.IsNullOrEmpty(locKey_Chain) ? locKey_Chain : string.Empty;
    //在匹配链中的位置
    public int ChainPosition { get; private set; } = -1;
    //匹配链
    public MergeItemChain ChainObj { get; private set; }

    public int ShopRandomBagID { get; private set; }

    public int GemsRandomBagID { get; private set; }
    public int CoinsRandomBagID { get; private set; }
    public string KeyGiftName { get; private set; }
    public bool IsMaxLevel => string.IsNullOrEmpty(MergeOutputPrefab);//该物品处于匹配链最高级
    //物品发现状态
    public MergeItemDiscoveryState m_discoveryState
    {
        get
        {
            if (BookSaveSystem.prefabDataDict.TryGetValue(PrefabName, out var state))
            {
                if (state != null)
                {
                    return state.state;
                }
            }
            return MergeItemDiscoveryState.Undiscovered;
        }
    }


    public static void LoadDefinition()
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
                MergeItemDefinition mergeItemDefinition = new MergeItemDefinition();
                if (dict3.TryGetValue("PrefabName", out CustomJSONObject itemId_Json))
                {
                    mergeItemDefinition.LoadBaseData(dict3);
                    TotalDefinitionsDict.Add(itemId_Json.ToString(), mergeItemDefinition);
                }
                else
                {
                    GameDebug.LogError("this data without key: 'Index'");
                }
            }
        }
        else
        {
            GameDebug.LogError("MergeStorageConfig::Init: Config is null.");
        }

        LoadRemoteConfigObject();


        foreach (var item in TotalDefinitionsDict)
        {
            if (item.Value.HasChain)
            {
                item.Value.ChainPosition = item.Value.Level;
                item.Value.ChainObj = MergeItemChain.AddToChain(item.Value);
            }

        }


    }

    public static void LoadRemoteConfigObject()
    {
        try
        {
            string str = RemoteConfigSystem.Instance.GetRemoteConfig_String(RemoteConfigSystem.remoteKey_ObjectConfig);
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            var largeDict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, object>>>>(str);
            if (largeDict == null)
            {
                return;
            }

            foreach (var dict in largeDict)
            {
                if (dict.Key != "all")
                {
                    string[] verstr = dict.Key.Split('_');
                    if (verstr == null || verstr.Length != 2)
                    {
                        continue;
                    }

                    string currentVersion = Application.isEditor && DebugSetting.CanUseDebugConfig(out var debugSO)
                        ? debugSO.DebugCurrentVersion
                        : Ivy.RiseSdk.Instance.GetConfig(Ivy.RiseSdk.CONFIG_KEY_VERSION_NAME);
                    if (ExtensionTool.TryCompareAppVersion(currentVersion, verstr[1], out int result))
                    {
                        switch (verstr[0])
                        {
                            case "equal":
                                if (result != 0)
                                {
                                    continue;
                                }
                                break;
                            case "less":
                                if (result != -1)
                                {
                                    continue;
                                }
                                break;
                            case "greater":
                                if (result != 1)
                                {
                                    continue;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                foreach (var prefabDict in dict.Value)
                {
                    if (string.IsNullOrEmpty(prefabDict.Key))
                    {
                        continue;
                    }

                    if (!TotalDefinitionsDict.TryGetValue(prefabDict.Key, out MergeItemDefinition itemDefinition))
                    {
                        itemDefinition = new MergeItemDefinition();
                        itemDefinition.PrefabName = prefabDict.Key;
                        TotalDefinitionsDict.Add(itemDefinition.PrefabName, itemDefinition);
                    }
                    if (prefabDict.Value.TryGetValue("Pond", out object pond))
                    {

                        if (itemDefinition.CategoryType == MergeItemCategoryType.modelSwitch)
                        {
                            string[] array = pond.ToString().Split(';');
                            if (array.Length >= 2)
                            {
                                itemDefinition.ModelSwitchPondID1 = array[0];
                                itemDefinition.ModelSwitchPondID2 = array[1];
                            }
                        }
                        else
                            itemDefinition.PondId = pond.ToString();
                    }
                    if (prefabDict.Value.TryGetValue("Level", out object LevelObj))
                    {
                        if (int.TryParse(LevelObj.ToString(), out int temp))
                        {
                            itemDefinition.Level = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("Rarity", out object RarityObj))
                    {
                        if (int.TryParse(RarityObj.ToString(), out int temp))
                        {
                            itemDefinition.RarityType = (ItemRarityType)temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("PrefabUnlockReward", out object PrefabUnlockRewardObj))
                    {
                        if (ExtensionTool.TryParseMergeRewardItem(PrefabUnlockRewardObj.ToString(), out MergeRewardItem temp))
                        {
                            itemDefinition.BookUnlockReward = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("PrefabSpecialDisplay", out object PrefabSpecialDisplayObj))
                    {
                        if (int.TryParse(PrefabSpecialDisplayObj.ToString(), out int temp))
                        {
                            itemDefinition.PrefabSpecialDisplay = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("Categories", out object CategoriesObj))
                    {
                        if (Enum.TryParse(CategoriesObj.ToString(), out MergeItemCategoryType type))
                        {
                            itemDefinition.CategoryType = type;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("WindowType", out object WindowTypeObj))
                    {
                        if (Enum.TryParse(WindowTypeObj.ToString(), out MergeItemWindowType type))
                        {
                            itemDefinition.WindowType = type;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("AutomaticOutput", out object AutomaticOutputObj))
                    {
                        itemDefinition.autoSpawnPrefab = AutomaticOutputObj.ToString();
                    }
                    if (prefabDict.Value.TryGetValue("CanSelect", out object CanSelectObj))
                    {
                        itemDefinition.CanSelect = CanSelectObj.ToString() == "x";
                    }
                    if (prefabDict.Value.TryGetValue("CanMove", out object CanMoveObj))
                    {
                        itemDefinition.CanMove = CanMoveObj.ToString() == "x";
                    }
                    if (prefabDict.Value.TryGetValue("CanMerge", out object CanMergeObj))
                    {
                        itemDefinition.CanMerge = CanMergeObj.ToString() == "x";
                    }
                    if (prefabDict.Value.TryGetValue("NeedEnergy", out object NeedEnergyObj))
                    {
                        itemDefinition.NeedEnergy = NeedEnergyObj.ToString() == "x";
                    }

                    if (prefabDict.Value.TryGetValue("PriceCoins", out object PriceCoinsObj))
                    {
                        if (int.TryParse(PriceCoinsObj.ToString(), out int temp))
                        {
                            itemDefinition.PriceCoins = temp;
                        }
                    }

                    if (prefabDict.Value.TryGetValue("PriceGems", out object PriceGemsObj))
                    {
                        if (int.TryParse(PriceGemsObj.ToString(), out int temp))
                        {
                            itemDefinition.PriceGems = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("SellValue", out object SellValueObj))
                    {
                        if (int.TryParse(SellValueObj.ToString(), out int temp))
                        {
                            itemDefinition.SellValue = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("DupeCost", out object DupeCostObj))
                    {
                        if (int.TryParse(DupeCostObj.ToString(), out int temp))
                        {
                            itemDefinition.BubbleUnlockCost = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("unlockcost", out object unlockcostObj))
                    {
                        if (int.TryParse(unlockcostObj.ToString(), out int temp))
                        {
                            itemDefinition.UnLockCost = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("DefaultOutput", out object DefaultOutputObj))
                    {
                        itemDefinition.MergeOutputPrefab = DefaultOutputObj.ToString();
                    }
                    if (prefabDict.Value.TryGetValue("Hierarchy", out object HierarchyObj))
                    {
                        if (int.TryParse(HierarchyObj.ToString(), out int temp))
                        {
                            itemDefinition.Hierarchy = temp;
                        }
                    }

                    if (prefabDict.Value.TryGetValue("Bubble_equl", out object Bubble_equlObj))
                    {
                        if (int.TryParse(Bubble_equlObj.ToString(), out int temp))
                        {
                            itemDefinition.BubbleEqul = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("BubbleBreakTime", out object BubbleBreakTimeObj))
                    {
                        if (int.TryParse(BubbleBreakTimeObj.ToString(), out int temp))
                        {
                            itemDefinition.DieTime = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("BubbleDieCoin", out object BubbleDieCoinObj))
                    {
                        itemDefinition.BubbleDieOutputPrefab = BubbleDieCoinObj.ToString();
                    }
                    if (prefabDict.Value.TryGetValue("BoxOpenTime", out object BoxOpenTimeObj))
                    {
                        if (int.TryParse(BoxOpenTimeObj.ToString(), out int temp))
                        {
                            itemDefinition.BoxOpenTime = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("FixdSpawn", out object FixdSpawnObj))
                    {
                        itemDefinition.fixedSpawnList = new List<MergeRewardItem>();
                        string listStr = FixdSpawnObj.ToString();
                        if (!string.IsNullOrEmpty(listStr))
                        {
                            string[] array = listStr.Split(';');
                            foreach (var item in array)
                            {
                                string[] items = item.Split(',');
                                if (items != null && items.Length == 2 && int.TryParse(items[1], out int num))
                                {
                                    MergeRewardItem itemTemp = new MergeRewardItem()
                                    {
                                        name = items[0],
                                        num = num
                                    };
                                    itemDefinition.fixedSpawnList.Add(itemTemp);
                                }
                                else
                                {
                                    UnityEngine.Debug.LogError("data error！" + listStr);
                                }
                            }
                        }
                    }
                    if (prefabDict.Value.TryGetValue("ResetCDCost", out object ResetCDCostObj))
                    {
                        if (int.TryParse(ResetCDCostObj.ToString(), out int temp))
                        {
                            itemDefinition.FinishChargeCost = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("CreateCount", out object CreateCountObj))
                    {
                        if (itemDefinition.CategoryType == MergeItemCategoryType.dualSkills
                            || itemDefinition.CategoryType == MergeItemCategoryType.modelSwitch)
                        {
                            string[] array = CreateCountObj.ToString().Split(';');
                            int idx = 0;
                            foreach (var countStr in array)
                            {
                                if (int.TryParse(countStr, out int temp))
                                {
                                    if (idx == 0)
                                    {
                                        if (itemDefinition.CategoryType == MergeItemCategoryType.dualSkills)
                                            itemDefinition.CanSpawnCountByOneCharge = temp;
                                        else
                                            itemDefinition.ModelSwitchSpawnCount1 = temp;
                                    }
                                    else if (idx == 1)
                                    {
                                        if (itemDefinition.CategoryType == MergeItemCategoryType.dualSkills)
                                            itemDefinition.CanSpawnCountByOneCharge_Auto = temp;
                                        else
                                            itemDefinition.ModelSwitchSpawnCount2 = temp;
                                    }
                                }
                                idx++;
                            }
                        }
                        else if (int.TryParse(CreateCountObj.ToString(), out int temp))
                        {
                            if (itemDefinition.CategoryType == MergeItemCategoryType.produced
                                || itemDefinition.CategoryType == MergeItemCategoryType.swallowZ)
                                itemDefinition.CanSpawnCountByOneCharge_Auto = temp;
                            else
                                itemDefinition.CanSpawnCountByOneCharge = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("Rareness", out object RarenessObj))
                    {
                        if (int.TryParse(RarenessObj.ToString(), out int temp))
                        {
                            itemDefinition.Rareness = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("ShopRandomBagID", out object ShopRandomBagIDObj))
                    {
                        if (int.TryParse(ShopRandomBagIDObj.ToString(), out int temp))
                        {
                            itemDefinition.ShopRandomBagID = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("SameKind", out object SameKindObj))
                    {
                        itemDefinition.SameKind = SameKindObj.ToString();
                    }
                    if (prefabDict.Value.TryGetValue("GemsRandomBagID", out object GemsRandomBagIDObj))
                    {
                        if (int.TryParse(GemsRandomBagIDObj.ToString(), out int temp))
                        {
                            itemDefinition.GemsRandomBagID = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("CoinsRandomBagID", out object CoinsRandomBagIDObj))
                    {
                        if (int.TryParse(CoinsRandomBagIDObj.ToString(), out int temp))
                        {
                            itemDefinition.CoinsRandomBagID = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("LoopTime", out object LoopTimeObj))
                    {
                        if (itemDefinition.CategoryType == MergeItemCategoryType.dualSkills)
                        {
                            string[] array = LoopTimeObj.ToString().Split(';');
                            int idx = 0;
                            foreach (var countStr in array)
                            {
                                if (int.TryParse(countStr, out int temp))
                                {
                                    if (idx == 0)
                                        itemDefinition.TotalChargeCount = temp;
                                    else if (idx == 1)
                                        itemDefinition.TotalChargeCount_Auto = temp;
                                }
                                idx++;
                            }
                        }
                        else if (int.TryParse(LoopTimeObj.ToString(), out int temp))
                        {
                            if (itemDefinition.CategoryType == MergeItemCategoryType.produced
                                || itemDefinition.CategoryType == MergeItemCategoryType.swallowZ)
                                itemDefinition.TotalChargeCount_Auto = temp;
                            else
                                itemDefinition.TotalChargeCount = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("LoopCD", out object LoopCDObj))
                    {
                        if (itemDefinition.CategoryType == MergeItemCategoryType.dualSkills)
                        {
                            string[] array = LoopCDObj.ToString().Split(';');
                            int idx = 0;
                            foreach (var cdStr in array)
                            {
                                if (int.TryParse(cdStr, out int temp))
                                {
                                    if (idx == 0)
                                        itemDefinition._chargeLoopCDSecond = temp;
                                    else if (idx == 1)
                                        itemDefinition._chargeLoopCDSecond_Auto = temp;
                                }
                                idx++;
                            }
                        }
                        else if (int.TryParse(LoopCDObj.ToString(), out int temp))
                        {
                            if (itemDefinition.CategoryType == MergeItemCategoryType.produced
                                || itemDefinition.CategoryType == MergeItemCategoryType.swallowZ)
                                itemDefinition._chargeLoopCDSecond_Auto = temp;
                            else
                                itemDefinition._chargeLoopCDSecond = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("DieCreate", out object DieCreateObj))
                    {
                        itemDefinition.DieCreatePrefab = DieCreateObj.ToString();
                    }
                    if (prefabDict.Value.TryGetValue("Origin", out object originObj))
                    {
                        string[] array = originObj.ToString().Split(';');
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(array[i]))
                            {
                                if (i < itemDefinition.originList.Count)
                                    itemDefinition.originList[i] = array[i];
                                else
                                    itemDefinition.originList.Add(array[i]);
                            }
                        }
                    }
                    if (prefabDict.Value.TryGetValue("Strength", out object StrengthObj))
                    {
                        if (ExtensionTool.TryParseMergeRewardItem(StrengthObj.ToString(), out MergeRewardItem temp))
                        {
                            itemDefinition.Strength = temp;
                        }
                    }
                    if (prefabDict.Value.TryGetValue("Key_Name", out object Key_NameObj))
                    {
                        itemDefinition.locKey_Name = Key_NameObj.ToString();
                    }
                    if (prefabDict.Value.TryGetValue("Key_Desc", out object Key_DescObj))
                    {
                        itemDefinition.locKey_Description = Key_DescObj.ToString();
                    }
                    if (prefabDict.Value.TryGetValue("Key - Chain", out object Key_ChainObj))
                    {
                        itemDefinition.locKey_Chain = Key_ChainObj.ToString();
                    }
                    if (prefabDict.Value.TryGetValue("Key-Output", out object Key_Output))
                    {
                        itemDefinition.locKey_Output = Key_Output.ToString();
                    }
                    if (prefabDict.Value.TryGetValue("Parameters", out object ParametersObj))
                    {
                        if (int.TryParse(ParametersObj.ToString(), out int temp))
                        {
                            if (itemDefinition.CategoryType == MergeItemCategoryType.modelSwitch)
                                itemDefinition.ModelSwitchCDTime = temp;
                            else
                                itemDefinition.SpeedUpSenconds = temp;
                        }
                    }
                }

            }


        }
        catch (Exception e)
        {
            DebugSetting.LogError(e, "[LoadRemoteConfigObject]");
        }
    }


    private void LoadBaseData(Dictionary<string, CustomJSONObject> dataDic)
    {
        if (dataDic.TryGetValue("ItemID", out CustomJSONObject itenId_Json))
        {
            if (int.TryParse(itenId_Json.ToString(), out int itemId))
            {
                ItemId = itemId;
            }
        }
        if (dataDic.TryGetValue("PrefabName", out CustomJSONObject prefabName_Json))
        {
            PrefabName = prefabName_Json.ToString();

            GameConfig.CheckPrefabNameValid(k_DataResourceFileName, PrefabName);
        }
        if (dataDic.TryGetValue("ContainerChain", out CustomJSONObject ContainerChain_Json))
        {
            if (ContainerChain_Json != null && int.TryParse(ContainerChain_Json.ToString(), out int containerChain))
            {
                ContainerChainLevel = containerChain;
            }
        }
        if (dataDic.TryGetValue("OutputChain", out CustomJSONObject OutputChainLevel_Json))
        {
            if (OutputChainLevel_Json != null && int.TryParse(OutputChainLevel_Json.ToString(), out int outputChainLevel))
            {
                OutputChainLevel = outputChainLevel;
            }
        }
        if (dataDic.TryGetValue("Level", out CustomJSONObject level_Json))
        {
            if (int.TryParse(level_Json.ToString(), out int level))
            {
                Level = level;
            }
        }
        if (dataDic.TryGetValue("Rarity", out CustomJSONObject Rarity_Json))
        {
            if (int.TryParse(Rarity_Json.ToString(), out int rarity))
                RarityType = (ItemRarityType)rarity;
        }
        if (dataDic.TryGetValue("PrefabUnlockReward", out CustomJSONObject unlockReward_Json))
        {
            string unlockReward = unlockReward_Json.ToString();
            string[] array = unlockReward.Split(',');
            if (array.Length == 2 && int.TryParse(array[1], out int num))
            {
                MergeRewardItem item = new MergeRewardItem();
                item.name = array[0];
                item.num = num;
                BookUnlockReward = item;
            }
        }
        if (dataDic.TryGetValue("PrefabSpecialDisplay", out CustomJSONObject specialDisplay_Json))
        {
            if (int.TryParse(specialDisplay_Json.ToString(), out int specialDisplay))
            {
                PrefabSpecialDisplay = specialDisplay;
            }
        }
        if (dataDic.TryGetValue("Categories", out CustomJSONObject categories_Json))
        {
            if (Enum.TryParse(categories_Json.ToString(), out MergeItemCategoryType type))
            {
                CategoryType = type;
            }
        }
        if (dataDic.TryGetValue("WindowType", out CustomJSONObject WindowTypeObj_Json))
        {
            if (Enum.TryParse(WindowTypeObj_Json.ToString(), out MergeItemWindowType type))
            {
                WindowType = type;
            }
        }
        if (dataDic.TryGetValue("AutomaticOutput", out CustomJSONObject automaticOutput_json))
        {
            autoSpawnPrefab = automaticOutput_json.ToString();
        }
        if (dataDic.TryGetValue("Can Select", out CustomJSONObject canSelect_Json))
        {
            CanSelect = canSelect_Json.ToString() == "x" ? true : false;
        }
        if (dataDic.TryGetValue("Can Move", out CustomJSONObject canMove_Json))
        {
            CanMove = canMove_Json.ToString() == "x" ? true : false;
        }
        if (dataDic.TryGetValue("Can Merge", out CustomJSONObject canMerge_Ison))
        {
            CanMerge = canMerge_Ison.ToString() == "x" ? true : false;
        }
        if (dataDic.TryGetValue("NeedEnergy", out CustomJSONObject needEnergy_Json))
        {
            NeedEnergy = needEnergy_Json.ToString() == "x" ? true : false;
        }
        if (dataDic.TryGetValue("PriceCoins", out CustomJSONObject priceCoins_Json))
        {
            if (int.TryParse(priceCoins_Json.ToString(), out int priceCoins))
            {
                PriceCoins = priceCoins;
            }
        }
        if (dataDic.TryGetValue("PriceGems", out CustomJSONObject priceGems_Json))
        {
            if (int.TryParse(priceGems_Json.ToString(), out int priceGems))
            {
                PriceGems = priceGems;
            }
        }
        if (dataDic.TryGetValue("SellValue", out CustomJSONObject sellValue_Json))
        {
            if (int.TryParse(sellValue_Json.ToString(), out int sellValue))
            {
                SellValue = sellValue;
            }
        }
        if (dataDic.TryGetValue("Dupe Cost", out CustomJSONObject dupeCost_Json))
        {
            if (int.TryParse(dupeCost_Json.ToString(), out int dupeCost))
            {
                BubbleUnlockCost = dupeCost;
            }
        }
        if (dataDic.TryGetValue("unlockcost", out CustomJSONObject unlockcost_Json))
        {
            if (int.TryParse(unlockcost_Json.ToString(), out int unlockcost))
            {
                UnLockCost = unlockcost;
            }
        }
        if (dataDic.TryGetValue("Default Output", out CustomJSONObject defaultOutput_Json))
        {
            MergeOutputPrefab = defaultOutput_Json.ToString();
        }
        if (dataDic.TryGetValue("Hierarchy", out CustomJSONObject Hierarchy_Json))
        {
            if (int.TryParse(Hierarchy_Json.ToString(), out int Hierarchy_equl))
            {
                Hierarchy = Hierarchy_equl;
            }
        }
        if (dataDic.TryGetValue("Bubble_equl", out CustomJSONObject bubble_equl_Json))
        {
            if (float.TryParse(bubble_equl_Json.ToString(), out float bubble_equl))
            {
                BubbleEqul = bubble_equl;
            }
        }
        if (dataDic.TryGetValue("BubbleBreakTime", out CustomJSONObject bubbleBreakTime_Json))
        {
            if (int.TryParse(bubbleBreakTime_Json.ToString(), out int bubbleBreakTime))
            {
                DieTime = bubbleBreakTime;
            }
        }
        if (dataDic.TryGetValue("BubbleDieCoin", out CustomJSONObject bubbleDieCoin_Json))
        {
            BubbleDieOutputPrefab = bubbleDieCoin_Json.ToString();
        }
        if (dataDic.TryGetValue("BoxOpenTime", out CustomJSONObject boxOpenTime_Json))
        {
            if (int.TryParse(boxOpenTime_Json.ToString(), out int boxOpenTime))
            {
                BoxOpenTime = boxOpenTime;
            }
        }
        if (dataDic.TryGetValue("FixdSpawn", out CustomJSONObject fixdSpawn))
        {
            fixedSpawnList = new List<MergeRewardItem>();
            string listStr = fixdSpawn.ToString();
            if (!string.IsNullOrEmpty(listStr))
            {
                string[] array = listStr.Split(';');
                foreach (var item in array)
                {
                    string[] items = item.Split(',');
                    if (items != null && items.Length == 2 && int.TryParse(items[1], out int num))
                    {
                        MergeRewardItem itemTemp = new MergeRewardItem()
                        {
                            name = items[0],
                            num = num
                        };
                        fixedSpawnList.Add(itemTemp);
                    }
                    else
                    {
                        UnityEngine.Debug.LogError("data error！" + listStr);
                    }
                }
            }
        }

        if (dataDic.TryGetValue("Pond", out CustomJSONObject pond_Json))
        {
            if (CategoryType == MergeItemCategoryType.modelSwitch)
            {
                string[] array = pond_Json.ToString().Split(';');
                if (array.Length >= 2)
                {
                    ModelSwitchPondID1 = array[0];
                    ModelSwitchPondID2 = array[1];
                }
            }
            else
                PondId = pond_Json.ToString();
        }
        if (dataDic.TryGetValue("ResetCDCost", out CustomJSONObject resetCDCost_Json))
        {
            if (int.TryParse(resetCDCost_Json.ToString(), out int resetCDCost))
            {
                FinishChargeCost = resetCDCost;
            }
        }
        if (dataDic.TryGetValue("CreateCount", out CustomJSONObject createCount_Json))
        {
            if (CategoryType == MergeItemCategoryType.dualSkills
                || CategoryType == MergeItemCategoryType.modelSwitch)
            {
                string[] array = createCount_Json.ToString().Split(';');
                int idx = 0;
                foreach (var countStr in array)
                {
                    if (int.TryParse(countStr, out int temp))
                    {
                        if (idx == 0)
                        {
                            if (CategoryType == MergeItemCategoryType.dualSkills)
                                CanSpawnCountByOneCharge = temp;
                            else
                                ModelSwitchSpawnCount1 = temp;
                        }
                        else if (idx == 1)
                        {
                            if (CategoryType == MergeItemCategoryType.dualSkills)
                                CanSpawnCountByOneCharge_Auto = temp;
                            else
                                ModelSwitchSpawnCount2 = temp;
                        }
                    }
                    idx++;
                }
            }
            else if (int.TryParse(createCount_Json.ToString(), out int temp))
            {
                if (CategoryType == MergeItemCategoryType.produced || CategoryType == MergeItemCategoryType.swallowZ)
                    CanSpawnCountByOneCharge_Auto = temp;
                else
                    CanSpawnCountByOneCharge = temp;
            }
        }
        if (dataDic.TryGetValue("SameKind", out CustomJSONObject SameKind_Json))
        {
            SameKind = SameKind_Json.ToString();
        }
        if (dataDic.TryGetValue("ShopRandomBagID", out CustomJSONObject ShopRandomBagID_Json))
        {
            if (int.TryParse(ShopRandomBagID_Json.ToString(), out int ShopRandomBagID_num))
            {
                ShopRandomBagID = ShopRandomBagID_num;
            }
        }
        if (dataDic.TryGetValue("GemsRandomBagID", out CustomJSONObject GemsRandomBagID_Json))
        {
            if (int.TryParse(GemsRandomBagID_Json.ToString(), out int GemsRandomBagID_num))
            {
                GemsRandomBagID = GemsRandomBagID_num;
            }
        }
        if (dataDic.TryGetValue("CoinsRandomBagID", out CustomJSONObject CoinsRandomBagID_Json))
        {
            if (int.TryParse(CoinsRandomBagID_Json.ToString(), out int CoinsRandomBagID_num))
            {
                CoinsRandomBagID = CoinsRandomBagID_num;
            }
        }
        if (dataDic.TryGetValue("Rareness", out CustomJSONObject Rareness_Json))
        {
            if (int.TryParse(Rareness_Json.ToString(), out int Rareness_num))
            {
                Rareness = Rareness_num;
            }
        }
        if (dataDic.TryGetValue("LoopTime", out CustomJSONObject loopTime_Json))
        {
            if (CategoryType == MergeItemCategoryType.dualSkills)
            {
                string[] array = loopTime_Json.ToString().Split(';');
                int idx = 0;
                foreach (var countStr in array)
                {
                    if (int.TryParse(countStr, out int temp))
                    {
                        if (idx == 0)
                            TotalChargeCount = temp;
                        else if (idx == 1)
                            TotalChargeCount_Auto = temp;
                    }
                    idx++;
                }
            }
            else if (int.TryParse(loopTime_Json.ToString(), out int temp))
            {
                if (CategoryType == MergeItemCategoryType.produced || CategoryType == MergeItemCategoryType.swallowZ)
                    TotalChargeCount_Auto = temp;
                else
                    TotalChargeCount = temp;
            }
        }
        if (dataDic.TryGetValue("LoopCD", out CustomJSONObject loopCD_Json))
        {
            if (CategoryType == MergeItemCategoryType.dualSkills)
            {
                string[] array = loopCD_Json.ToString().Split(';');
                int idx = 0;
                foreach (var cdStr in array)
                {
                    if (int.TryParse(cdStr, out int temp))
                    {
                        if (idx == 0)
                            _chargeLoopCDSecond = temp;
                        else if (idx == 1)
                            _chargeLoopCDSecond_Auto = temp;
                    }
                    idx++;
                }
            }
            else if (int.TryParse(loopCD_Json.ToString(), out int temp))
            {
                if (CategoryType == MergeItemCategoryType.produced
                    || CategoryType == MergeItemCategoryType.swallowZ)
                    _chargeLoopCDSecond_Auto = temp;
                else
                    _chargeLoopCDSecond = temp;
            }
        }
        if (dataDic.TryGetValue("LightningDisplay", out CustomJSONObject lightningDisplay_Json))
        {
            NeedShowChargeIcon = lightningDisplay_Json.ToString().Equals("true", StringComparison.OrdinalIgnoreCase) ? true : false;
        }
        if (dataDic.TryGetValue("StarDisplay", out CustomJSONObject starDisplay_Json))
        {
            NeedShowParticle = starDisplay_Json.ToString().Equals("true", StringComparison.OrdinalIgnoreCase) ? true : false;
        }
        if (dataDic.TryGetValue("OutputDisplay", out CustomJSONObject OutputDisplay))
        {
            NeedShowSpawnEffect = OutputDisplay.ToString().Equals("true", StringComparison.OrdinalIgnoreCase) ? true : false;
        }
        if (dataDic.TryGetValue("DieCreate", out CustomJSONObject dieCreate_Json))
        {
            DieCreatePrefab = dieCreate_Json.ToString();
        }
        if (dataDic.TryGetValue("IllustrationJoin", out CustomJSONObject IllustrationJoin_Json))
        {
            NeedJoinCollection = IllustrationJoin_Json.ToString().Equals("true", StringComparison.OrdinalIgnoreCase) ? true : false;
        }
        if (dataDic.TryGetValue("EatenProps", out CustomJSONObject EatenProps))
        {
            string[] array = EatenProps.ToString().Split(';');
            for (int i = 0; i < array.Length; i++)
            {
                string[] array2 = array[i].Split(',');
                if (array2 != null && array2.Length == 2)
                {
                    if (int.TryParse(array2[1], out var count))
                    {
                        swallowPondIdList.Add(array2[0]);
                        swallowCountList.Add(count);
                    }
                }
            }
        }
        if (dataDic.TryGetValue("EatenDisplay", out CustomJSONObject EatenDisplay))
        {
            NeedShowSwallowIcon = EatenDisplay.ToString().Equals("true", StringComparison.OrdinalIgnoreCase) ? true : false;
        }
        if (dataDic.TryGetValue("Origin", out CustomJSONObject origin_Json))
        {
            string[] array = origin_Json.ToString().Split(';');
            for (int i = 0; i < array.Length; i++)
            {
                if (!string.IsNullOrEmpty(array[i]))
                    originList.Add(array[i]);
            }
        }
        if (dataDic.TryGetValue("Strength", out CustomJSONObject strength_Json))
        {
            string strength = strength_Json.ToString();
            string[] array = strength.Split(',');
            if (array.Length == 2 && int.TryParse(array[1], out int num))
            {
                MergeRewardItem rewardItem = new MergeRewardItem();
                rewardItem.name = array[0];
                rewardItem.num = num;
                Strength = rewardItem;
            }
        }
        if (dataDic.TryGetValue("On Tap: Custom Script", out CustomJSONObject customScript_Json))
        {
            OnTapCustomScript = customScript_Json.ToString();
        }
        if (dataDic.TryGetValue("Key - Name", out CustomJSONObject keyName_Json))
        {
            locKey_Name = keyName_Json.ToString();
        }
        if (dataDic.TryGetValue("Key - Desc", out CustomJSONObject keyDesc_Json))
        {
            locKey_Description = keyDesc_Json.ToString();
        }
        if (dataDic.TryGetValue("Key - Chain", out CustomJSONObject keyChain_Json))
        {
            locKey_Chain = keyChain_Json.ToString();
        }
        if (dataDic.TryGetValue("Key-GiftName", out CustomJSONObject keyGiftName_Json))
        {
            KeyGiftName = keyGiftName_Json.ToString();
        }
        if (dataDic.TryGetValue("Key-Output", out CustomJSONObject keyOutput_Json))
        {
            locKey_Output = keyOutput_Json.ToString();
        }
        if (dataDic.TryGetValue("Parameters", out CustomJSONObject Parameters_Json))
        {
            if (int.TryParse(Parameters_Json.ToString(), out int temp))
            {
                if (CategoryType == MergeItemCategoryType.modelSwitch)
                    ModelSwitchCDTime = temp;
                else
                    SpeedUpSenconds = temp;
            }
        }
        if (dataDic.TryGetValue("BPexp", out CustomJSONObject bpExp_Json))
        {
            if (int.TryParse(bpExp_Json.ToString(), out int bpExp))
            {
                BatterPassExp = bpExp;
            }
        }

        if (dataDic.TryGetValue("UnlockChapter", out CustomJSONObject ca_Json))
        {
            string[] array = ca_Json.ToString().Split(';');
            for (int i = 0; i < array.Length; i++)
            {
                if (!string.IsNullOrEmpty(array[i]))
                {
                    string[] arr = array[i].Split(',');
                    if (arr != null && arr.Length == 2 && int.TryParse(arr[0], out var chapter) && !string.IsNullOrEmpty(arr[1]))
                    {
                        taskBoxPondDict[chapter] = arr[1];
                    }
                }
            }
        }
    }

    private void ParseCategories(string strings)
    {
        if (strings.Length == 0)
        {
            GameDebug.LogError("Expected a non-empty list of category strings if this method is called. Loading match object definition for: " + PrefabName);
            return;
        }

        if (Enum.TryParse(strings, out MergeItemCategoryType type))
        {
            CategoryType = type;
            if (TotalCategoryObjects.ContainsKey(type))
            {
                TotalCategoryObjects[type].Add(this);
            }
            else
            {
                TotalCategoryObjects[type] = new List<MergeItemDefinition> { this };
            }
        }
        else
        {
            GameDebug.LogError("无法解析该枚举类型！" + strings);
        }


    }
}


/// <summary>
/// 物品类型
/// </summary>
public enum MergeItemCategoryType
{
    simple,
    container,//无限容器，可点击产出物品
    consumable,//资源类，（金币，经验等）
    finiteContainer,//有限容器，取完物品后消失（如存钱罐）
    boxed,//容器（根据是否有CD时间判断是否为无限容器），产出锁住的物品，
    opened,//领取的时候直接打开，装有的奖励加入临时背包
    produced,//自动产出道具
    wake,//唤醒道具，CD后相当于一个有限容器
    dualSkills,//container+produced
    modelSwitch,//产出道具，根据时间段不同可产出不同物品
    booster,//九宫格加速道具
    timeBoster,//全局瞬间减CD道具
    taskBox,//有限容器，根据关卡阶段不同产出不同
    universal,//合成道具，将任一非最高等级道具的等级提升一级，不能提升存钱罐和taskbox类型道具(仅合成道具主动拖拽至目标时生效，反之不生效)
    splitUp,//拆分道具，将任一非最低等级道具拆分为两个低一级的道具，不能拆分存钱罐和taskbox类型道具(仅拆分道具主动拖拽至目标时生效，反之不生效)
    used,
    //副本道具
    clean,
    swallowC,//吞噬对应道具后进入充能状态，生成完成后变为初始状态
    swallowZ,//吞噬对应道具后进入自产状态，自产有上限，生成完成后死亡
    swallowT,//副本三用，暂未实现，吞噬池逻辑不同，其他同swallowC
    swallowF//吞噬对应道具后进入充能状态，生成完成后消失
}

/// <summary>
/// 物品被发现的状态
/// </summary>
public enum MergeItemDiscoveryState
{
    NONE = 0,
    Undiscovered = 1,
    Unlock = 2,
    Discovered = 3
}

public enum ItemRarityType
{
    Simple = 0,
    Star1 = 1,
    star2 = 2,
    Star3 = 3,
    Star4 = 4,
    Star5 = 5
}

/// <summary>
/// 显示物品窗口类型
/// </summary>
public enum MergeItemWindowType
{
    universal,  //显示自身产出链以及来自某物品
    count, //显示自身所在产出链，显示来自以及产出
    output, //显示自身所在产出链，显示产出道具
    special, //只显示自身所在产出链
    rare, //显示自身所在产出链，显示获取途径（文本）
}

[Serializable]
public struct MergeRewardItem
{
    public string name;
    public int num;
    public string source;

    [JsonIgnore]
    // 要展示的预制体名称
    public string ShowRewardPrefabName => ExtensionTool.GetShowPrefabName(this);
    [JsonIgnore]
    // 要展示的数量文本
    public string ShowRewardCountTxt => num.ToString();
    [JsonIgnore]
    public bool IsRewardGems => !string.IsNullOrEmpty(name) && name.Equals("gems", StringComparison.OrdinalIgnoreCase);
    [JsonIgnore]
    public bool IsRewardCoins => !string.IsNullOrEmpty(name) && name.Equals("coins", StringComparison.OrdinalIgnoreCase);
    [JsonIgnore]
    public bool IsRewardExp => !string.IsNullOrEmpty(name) && name.Equals("exp", StringComparison.OrdinalIgnoreCase);
    [JsonIgnore]
    public bool IsRewardEnergy => !string.IsNullOrEmpty(name) && name.Equals("energy", StringComparison.OrdinalIgnoreCase);
    [JsonIgnore]
    public bool IsNeedle => !string.IsNullOrEmpty(name) && name.Equals("needle", StringComparison.OrdinalIgnoreCase);
    [JsonIgnore]
    public bool IsRewardLove => !string.IsNullOrEmpty(name) && name.Equals("love", StringComparison.OrdinalIgnoreCase);
    [JsonIgnore]
    public bool IsRewardBranchPoint => !string.IsNullOrEmpty(name) &&name.Equals("points", StringComparison.OrdinalIgnoreCase);
    [JsonIgnore]
    public bool IsRewardSubEXP => !string.IsNullOrEmpty(name) && name.Equals("SubEXP", StringComparison.OrdinalIgnoreCase);
    [JsonIgnore]
    public bool IsRewardPrefab => !IsRewardCoins && !IsRewardEnergy && !IsRewardExp && !IsRewardGems && !IsRewardLove && !IsRewardBranchPoint;
    public bool IsCurrency => IsRewardCoins || IsRewardEnergy || IsRewardExp || IsRewardGems;

    public bool IsValidReward()
    {
        return (!string.IsNullOrEmpty(name) && num > 0);
    }

    public bool IsRandomBoxByLevel()
    {
        //return IsValidReward() && RandomBoxByLevel.DefinitionsDict.ContainsKey(name);
        return false;
    }

}

public struct RandomSpawnItem
{
    public string name;
    public int num;
}
/// <summary>
/// 物品匹配链
/// </summary>
public class MergeItemChain
{
    public static int k_MAX_CHAIN_PLANNED_FOR = 18;

    /// <summary>
    /// 所有的匹配链 < ChainID, MergeItemChain>
    /// </summary>
    public static Dictionary<string, MergeItemChain> TotalChainsDict { get; private set; } = new Dictionary<string, MergeItemChain>();

    public MergeItemDefinition[] Chain { get; private set; } = new MergeItemDefinition[k_MAX_CHAIN_PLANNED_FOR];
    public int ChainSize;

    public static MergeItemDefinition GetChainSiblingAtIndex(MergeItemDefinition def, int chainOrder1Based)
    {
        if (def == null || !TotalChainsDict.ContainsKey(def.ChainID))
        {
            return null;
        }
        MergeItemChain discoveryChain = TotalChainsDict[def.ChainID];
        int num = chainOrder1Based - 1;
        if (num < 0)
        {
            Debug.LogError("DiscoveryChain::GetChainSiblingAtIndex() was given a chainOrder1Based that was 0, which is not allowed. These are 1-based. In chain: " + def.ChainID);
            return null;
        }
        if (num >= discoveryChain.ChainSize)
        {
            Debug.LogError("DiscoveryChain::GetChainSiblingAtIndex() was given a chainOrder1Based that was greater than the size of the chain: " + chainOrder1Based + " in chain: " + def.ChainID);
            return null;
        }
        return discoveryChain.Chain[num];
    }

    public MergeItemDefinition GetNextAfter(MergeItemDefinition afterThisObject)
    {
        int num = afterThisObject.ChainPosition - 1;
        int num2 = num + 1;
        if (num2 >= ChainSize)
        {
            return null;
        }
        return Chain[num2];
    }

    public static MergeItemChain AddToChain(MergeItemDefinition def)
    {
        if (!def.HasChain)
        {
            return null;
        }
        if (!TotalChainsDict.TryGetValue(def.ChainID, out MergeItemChain itemChain))
        {
            itemChain = new MergeItemChain();
            TotalChainsDict.Add(def.ChainID, itemChain);
        }
        itemChain.AddDef(def);
        return itemChain;

    }


    private void AddDef(MergeItemDefinition def)
    {
        int index = def.ChainPosition - 1;
        if (index < 0 || index > Chain.Length)
        {
            UnityEngine.Debug.LogError("该物体的配表的匹配链位置设置错误（小于1或大于匹配量长度）: " + def.ItemName + ".请修改配表!");
            return;
        }
        //if (Chain[def.ChainPosition - 1] != null)
        //{
        //    UnityEngine.Debug.LogError("该物体的匹配链位置和匹配链中的其他物体相同：" + def.ItemName + ". 请修改配表!");
        //    return;
        //}
        Chain[def.ChainPosition - 1] = def;
        ChainSize++;
    }


    public static bool GetBeforeChainItem(MergeItemDefinition def, out MergeItemDefinition before)
    {
        before = null;
        if (def == null || !TotalChainsDict.ContainsKey(def.ChainID))
            return false;
        int beforeIndex = def.ChainPosition - 2;
        if (beforeIndex < 0)
            return false;
        MergeItemChain discoveryChain = TotalChainsDict[def.ChainID];
        if (beforeIndex < discoveryChain.Chain.Length)
        {
            before = discoveryChain.Chain[beforeIndex];
            return true;
        }
        else
            return false;
    }

    public static bool GetAfterChainItem(MergeItemDefinition def, out MergeItemDefinition after)
    {
        after = null;
        if (def == null || !TotalChainsDict.ContainsKey(def.ChainID))
            return false;
        int afterIndex = def.ChainPosition;
        if (afterIndex < 0)
            return false;
        MergeItemChain discoveryChain = TotalChainsDict[def.ChainID];
        if (afterIndex < discoveryChain.Chain.Length)
        {
            after = discoveryChain.Chain[afterIndex];
            return true;
        }
        else
            return false;
    }
}
