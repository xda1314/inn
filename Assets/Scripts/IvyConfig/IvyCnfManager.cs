using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IvyCore
{
    public class IvyCnfManager : SingletonMono<IvyCnfManager>
    {
        public const string DataFilePath = "Resources_moved/IVYData";
        public const string DataFileName = "IvyCnfData.asset";
        private IvyCnfData mConfigData = null;
        private void Awake()
        {
            var path = DataFilePath.Replace("Resources_moved/", "") + "/" + DataFileName;
            mConfigData = Resources.Load<IvyCnfData>(path);
            AssetSystem.Instance.LoadAsset<IvyCnfData>(path, (data) =>
            {
                mConfigData = data;
            });
        }

        public List<IvyCnfData.ShopCnf> getShopGoldData()
        {
            return mConfigData.cnfShop;
        }

        public List<IvyCnfData.LevelAward> getLevelAwards()
        {
            return mConfigData.cnfLevelAwards;
        }

        public List<int> getLevelChapter()
        {
            return mConfigData.cnfLevelChapter;
        }
    }
}