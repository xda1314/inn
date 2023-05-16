using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IvyCore
{
    public enum CnfPropType
    {
        None,
        Stripe,
        Square,
        ColorBomb,
        Spoon,
        Count
    }

    public class IvyCnfData : ScriptableObject
    {
        [Serializable]
        public class ShopCnf
        {
            [LabelText("金币数量")]
            public int count;
            [LabelText("金币价格")]
            public float cost;
            [LabelText("折扣")]
            [Range(0, 100)]
            public int off;
        }
        [LabelText("商店配置信息")]
        public List<ShopCnf> cnfShop;


        [Serializable]
        public class LevelAward
        {
            [LabelText("关卡")]
            public int level;
            [LabelText("道具类型")]
            public CnfPropType type;
            [LabelText("道具数量")]
            public int propCount;
            [LabelText("金币数量")]
            public int coinCount;
        }
        [LabelText("关卡奖励信息")]
        public List<LevelAward> cnfLevelAwards;

        [LabelText("关卡分段配置")]
        public List<int> cnfLevelChapter;
    }
}