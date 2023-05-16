using BDUnity.Utils;
using Ivy.Addressable;
using Ivy.Firebase;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace ivy.game
{
    public class GameConfig : SingletonMono<GameConfig>
    {
        private static ConfigSO configSO;

        public CustomJSONObject GetConfig(string name)
        {
            if (configSO == null)
            {
                AssetSystem.Instance.LoadAsset<ConfigSO>("ConfigSO", so =>
                {
                    configSO = so;
                });
            }
            if (configSO == null || configSO.configDict == null)
            {
                GameDebug.LogErrorFormat("[GameConfig.GetConfig] Data{0} not exist!", name);
                return null;
            }

            string curConfigName = name + "_new";
            if (configSO.configDict.TryGetValue(curConfigName, out string json1) && !string.IsNullOrEmpty(json1))
            {
                return CustomJSON.Deserialize(json1, true);
            }
            else if (configSO.configDict.TryGetValue(name, out string json2) && !string.IsNullOrEmpty(json2))
            {
                return CustomJSON.Deserialize(json2, true);
            }

            Ivy.LogUtils.LogErrorToSDK("[GameConfig.GetConfig] Data not exist!" + curConfigName);
            return null;

        }

        public IEnumerator LoadAllConfig()
        {
            PoolDefinition.LoadDefinitions(); //获取随机池
            //MonthPondConfig.Init();
            //SonPondConfig.Init();
            MergeItemDefinition.LoadDefinition();
            MapDefinition.Init();
            LootTable.Init();



            StorageDefinition.Init();
            SpinWheelDefinition.Init();
            GiftPackageDefinition.Init();
            ShopPackDefinition.Init();

            //DailyTaskDefinition.Init();
            BattlePassDefinition.Init();
            AchievementConfig.Init();

            MergeLevelDefinition.Init();
            RandomBagConfig.Init();
            RandomItemPondConfig.LoadDefinitions();

            DungeonDefinition.LoadDungeonDefinitions(); //副本信息
            DailyDefinition.LoadDailyDefinitions();//简单活动信息
            TaskGoalsDefinition.Init();
            MergeChapterRewardDefinition.LoadDefinition();

            MergeStarRewardDefinition.LoadDefinition();
            BranchControlDefinition.LoadDefinition();
            BranchRewardDefinition.LoadDefinition();
            BranchDefinition.Init();//支线

            PayPackDefinition.Init();
            Billing.Instance.InitSystem();


            CheckConfigValid();
            yield return null;
        }


        [Conditional("UNITY_EDITOR")]
        private void CheckConfigValid()
        {
            foreach (var item in PoolDefinition.PoolDefinitionDict.Values)
            {
                foreach (var item2 in item.rewardItemsList)
                {
                    CheckMergeRewardItemValid("pond", item2.rewardItem);
                }
            }

            foreach (var list in MergeItemDefinition.TotalDefinitionsDict)
            {
                if (!string.IsNullOrEmpty(list.Value.PondId) && LootTable.LootTableDic.TryGetValue(list.Value.PondId, out _))
                {
                    CheckPond("MergeObjectConfig", list.Value.PondId);
                }
                if (list.Value.taskBoxPondDict.Count > 0)
                {
                    foreach (var item in list.Value.taskBoxPondDict)
                    {
                        CheckPond("MergeObjectConfig", item.Value);
                    }
                }
            }
            foreach (var item in LootTable.LootTableDic)
            {
                CheckPond("LootTable", item.Key);
            }
        }

        [Conditional("UNITY_EDITOR")]
        public static void CheckRewardNameVaild(string configName, string rewardName)
        {
            var temp2 = new MergeRewardItem();
            temp2.name = rewardName;
            temp2.num = 1;
            CheckMergeRewardItemValid(configName, temp2);
        }

        [Conditional("UNITY_EDITOR")]
        public static void CheckMergeRewardItemValid(string configName, MergeRewardItem rewardItem)
        {
#if UNITY_EDITOR
            if (!string.IsNullOrEmpty(rewardItem.name))
            {
                if (rewardItem.IsRewardPrefab)
                {
                    if (!MergeItemDefinition.TotalDefinitionsDict.TryGetValue(rewardItem.ShowRewardPrefabName, out _))
                    {
                        Ivy.LogUtils.LogError($"{configName}配表中物品在MergeObjectConfig配表中找不到！名称：" + rewardItem.ShowRewardPrefabName);
                    }
                }
                else
                {
                    CheckPrefabNameValid(configName, rewardItem.ShowRewardPrefabName);
                }
            }
#endif
        }

        [Conditional("UNITY_EDITOR")]
        public static void CheckPond(string configName, string pond)
        {
            if (pond.IsNullOrEmpty())
                return;
            if (pond.StartsWith("Loot_"))
            {
                if (LootTable.LootTableDic.TryGetValue(pond, out var lootTable))
                {
                    foreach (var item in lootTable.pool.rewardItemsList)
                    {
                        CheckPond(configName, item.rewardItem.name);
                    }
                }
                else
                {
                    Ivy.LogUtils.LogError($"{configName}配表中池子在LootTable配表中找不到！名称：" + pond);
                }
            }
            else
            {
                CheckRewardNameVaild(configName, pond);
            }
        }

        [Conditional("UNITY_EDITOR")]
        public static void CheckPrefabNameValid(string configName, string prefabName)
        {
#if UNITY_EDITOR
            if (!string.IsNullOrEmpty(prefabName))
            {
                if (!AddressableSystem.Instance.ContainsKey(prefabName))
                {
                    Ivy.LogUtils.LogError($"{configName}中的物品找不到对应的预制体！名称：" + prefabName);
                }
            }
#endif
        }


    }
}
