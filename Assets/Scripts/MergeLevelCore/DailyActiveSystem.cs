using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class DailyActiveSystem
{
    #region 变量
    public static DailyActiveSystem Instance { get; private set; } = new DailyActiveSystem();
    public List<MergeRewardItem> ItemRewardList { get; set; } = new List<MergeRewardItem>();  //物品奖励
    public List<MergeRewardItem> LastRewardList { get; set; } = new List<MergeRewardItem>();  //上一个活动奖励
    public MergeLevelType Daily_Type = MergeLevelType.none;  //当前每日简单活动
    public MergeLevelType LastDailyType = MergeLevelType.none;  //当前每日简单活动
    public int Score { get; private set; } = 0;
    private DateTimeOffset lastRefreshDateTime;  //上一次活动初始化时间 
    private bool IsComplete = false;
    private bool _isInit = false;
    public Action refreshDaily;

    //保底奖励
    public List<MergeRewardItem> GuaranteedRewards { get; set; } = new List<MergeRewardItem>()
    {
        new MergeRewardItem
        {
            name = "Energy_3",
            num = 1,
        },
            new MergeRewardItem
        {
            name = "Coins_2",
            num = 1,
        }
    };
    #endregion

    #region 常量
    private const int UNLOCK_Level = 4;
    #endregion

    /// <summary>
    /// 初始化系统
    /// </summary>
    public void TryInitDailyActiveSystem()
    {
        if (TaskGoalsManager.Instance.curLevelIndex > UNLOCK_Level && !_isInit)
        {
            LoadData();
            if (lastRefreshDateTime.Date != TimeManager.ServerUtcNow().Date)
            {
                PushNextDailyActive();
                SaveData();
                refreshDaily?.Invoke();
            }
            _isInit = true;
        }
    }

    /// <summary>
    /// 活动是否开启
    /// </summary>
    /// <returns></returns>
    public bool GetIsOpen()
    {
        return TaskGoalsManager.Instance.curLevelIndex > UNLOCK_Level && Daily_Type != MergeLevelType.none;
    }

    public bool GetNoComplete()
    {
        return TaskGoalsManager.Instance.curLevelIndex > UNLOCK_Level && !IsComplete && Daily_Type != MergeLevelType.none;
    }

    public void DailyComplete()
    {
        IsComplete = true;
        AnalyticsUtil.trackActivityEnd(Daily_Type.ToString());
        SaveData();
        refreshDaily?.Invoke();
    }

    public void AddDailyLoveScore(int num)
    {
        Score += num;
        SaveData();
    }

    private void PushNextDailyType()
    {
        switch (Daily_Type)
        {
            case MergeLevelType.none:
                int dailyNum = RemoteConfigSystem.Instance.GetRemoteConfig_Int(RemoteConfigSystem.remoteKey_ads_limit);
#if UNITY_EDITOR
                dailyNum = 6;
#endif
                if (0 < dailyNum && dailyNum < 7)
                {
                    string dailyTye = "daily" + dailyNum.ToString();
                    Daily_Type = (MergeLevelType)Enum.Parse(typeof(MergeLevelType), dailyTye);
                }
                else
                {
                    //远程没有的情况下 默认活动一
                    Daily_Type = MergeLevelType.daily1;
                }
                break;
            case MergeLevelType.daily6:
                Daily_Type = MergeLevelType.daily1;
                break;
            case MergeLevelType.daily1:
                Daily_Type = MergeLevelType.daily2;
                break;
            case MergeLevelType.daily2:
                Daily_Type = MergeLevelType.daily4;
                break;
            case MergeLevelType.daily3:
                Daily_Type = MergeLevelType.daily4;
                break;
            case MergeLevelType.daily4:
                Daily_Type = MergeLevelType.daily5;
                break;
            case MergeLevelType.daily5:
                Daily_Type = MergeLevelType.daily6;
                break;
            default:
                break;
        }
    }

    private void PushNextDailyRewards(string str1,string str2)
    {
        switch (Daily_Type)
        {
            case MergeLevelType.daily1:
                ItemRewardList = DAIlY1_DATA(str1, str2);
                break;
            case MergeLevelType.daily2:
                ItemRewardList = DAIlY2_DATA(str1, str2);
                break;
            case MergeLevelType.daily3:
                ItemRewardList = DAIlY3_DATA(str1, str2);
                break;
            case MergeLevelType.daily4:
                ItemRewardList = DAIlY4_DATA(str1, str2);
                break;
            case MergeLevelType.daily5:
                ItemRewardList = DAIlY5_DATA(str1, str2);
                break;
            case MergeLevelType.daily6:
                ItemRewardList = DAIlY6_DATA(str1, str2);
                break;
            default:
                break;

        }
    }

    private void PushNextDailyActive()
    {
        Random random = new Random();
        string str1 = GetRewardStr(random);
        string str2 = GetRewardStr(random);
        //存储上一份奖励  用于活动结束后展示
        LastRewardList = ItemRewardList;
        LastDailyType = Daily_Type;
        if (!IsComplete)
            AnalyticsUtil.trackActivityEnd(Daily_Type.ToString());
        if (Daily_Type != MergeLevelType.none)
            SaveUtils.SetBool("Save_Key_DailyGuaranteeRewardsPop", !IsComplete);
        PushNextDailyType();
        PushNextDailyRewards(str1, str2);
        IsComplete = false;
        Score = 0;
        AnalyticsUtil.trackActivityStart(Daily_Type.ToString(), "");
        MergeLevelManager.Instance.TryDeleteMapDataByDungeonType(Daily_Type);//重置地图
    }

    #region 奖励数据
    private string GetRewardStr(Random random)
    {
        List<MergeRewardItem> rewardItems = new List<MergeRewardItem>()
        {
            new MergeRewardItem
        {
            name = "Blender_1",
            num = 1,
        },
            new MergeRewardItem
        {
            name = "Drawer_1",
            num = 1,
        },
            new MergeRewardItem
        {
            name = "CoffeeCup_1",
            num = 1,
        },
            new MergeRewardItem
        {
            name = "GardeningToolBox_1",
            num = 1,
        }
        };
        if (TaskGoalsManager.Instance.curLevelIndex > 15)
        {
            rewardItems.Add(new MergeRewardItem
            {
                name = "ToolBox_1",
                num = 1,
            });
        }
        if (TaskGoalsManager.Instance.curLevelIndex > 30)
        {
            rewardItems.Add(new MergeRewardItem
            {
                name = "FlowerPot_1",
                num = 1,
            });
        }
        if (TaskGoalsManager.Instance.curLevelIndex > 45)
        {
            rewardItems.Add(new MergeRewardItem
            {
                name = "Tree_1",
                num = 1,
            });
        }
        if (TaskGoalsManager.Instance.curLevelIndex > 75)
        {
            rewardItems.Add(new MergeRewardItem
            {
                name = "Tree_1",
                num = 1,
            });
        }

        if (TaskGoalsManager.Instance.curLevelIndex > 90)
        {
            rewardItems.Add(new MergeRewardItem
            {
                name = "Tree_1",
                num = 1,
            });
        }
        if (TaskGoalsManager.Instance.curLevelIndex > 105)
        {
            rewardItems.Add(new MergeRewardItem
            {
                name = "Tree_1",
                num = 1,
            });
        }

        if (TaskGoalsManager.Instance.curLevelIndex > 135)
        {
            rewardItems.Add(new MergeRewardItem
            {
                name = "Tree_1",
                num = 1,
            });
        }
        if (TaskGoalsManager.Instance.curLevelIndex > 135)
        {
            rewardItems.Add(new MergeRewardItem
            {
                name = "Tree_1",
                num = 1,
            });
        }

        int lmitMax = rewardItems.Count;
        int selectIndex = random.Next(0, lmitMax - 1);
        return rewardItems[selectIndex].name;
    }

    private List<MergeRewardItem> DAIlY1_DATA(string str1, string str2)
    {
        List<MergeRewardItem> rewardItems = new List<MergeRewardItem>()
        {
            new MergeRewardItem
        {
            name = "Booster_1",
            num = 1,
        },
            new MergeRewardItem
        {
            name = "Coins",
            num = 55,
        },
            new MergeRewardItem
        {
            name = "EnergyChest",
            num = 1,
        },
            new MergeRewardItem
        {
            name = str1,
            num = 1,
        },
            new MergeRewardItem
        {
            name = str2,
            num = 1,
        }
        };
        return rewardItems;
    }

    private List<MergeRewardItem> DAIlY2_DATA(string str1, string str2)
    {
        List<MergeRewardItem> rewardItems = new List<MergeRewardItem>()
        {
            new MergeRewardItem
        {
            name = "Energy_4",
            num = 1,
        },
new MergeRewardItem
        {
            name = "Coins",
            num = 55,
        },new MergeRewardItem
        {
            name = "CoinChest",
            num = 1,
        },new MergeRewardItem
        {
            name = str1,
            num = 1,
        },new MergeRewardItem
        {
            name = str2,
            num = 1,
        }
        };

        return rewardItems;
    }

    private List<MergeRewardItem> DAIlY3_DATA(string str1, string str2)
    {
        List<MergeRewardItem> rewardItems = new List<MergeRewardItem>()
        {
        new MergeRewardItem
        {
            name = "TimeBooster_1",
            num = 1,
        },
        new MergeRewardItem
        {
            name = "Coins",
            num = 55,
        },new MergeRewardItem
        {
            name = "EnergyChest",
            num = 1,
        },new MergeRewardItem
        {
            name = str1,
            num = 1,
        },new MergeRewardItem
        {
            name = str2,
            num = 1,
        }
        };
        return rewardItems;
    }

    private List<MergeRewardItem> DAIlY4_DATA(string str1, string str2)
    {
        List<MergeRewardItem> rewardItems = new List<MergeRewardItem>()
        {
            new MergeRewardItem
        {
            name = "TimeBooster_1",
            num = 1,
        },
            new MergeRewardItem
        {
            name = "Coins",
            num = 55,
        },
            new MergeRewardItem
        {
            name = "EnergyChest",
            num = 1,
        },
new MergeRewardItem
        {
            name = str1,
            num = 1,
        }
,new MergeRewardItem
        {
            name = str2,
            num = 1,
        }

        };
        return rewardItems;
    }

    private List<MergeRewardItem> DAIlY5_DATA(string str1, string str2)
    {
        List<MergeRewardItem> rewardItems = new List<MergeRewardItem>()
        {
        new MergeRewardItem
        {
            name = "Energy_4",
            num = 1,
        },
        new MergeRewardItem
        {
            name = "Coins",
            num = 55,
        },
        new MergeRewardItem
        {
            name = "CoinChest",
            num = 1,
        },new MergeRewardItem
        {
            name = str1,
            num = 1,
        },new MergeRewardItem
        {
            name = str2,
            num = 1,
        }
        };
        return rewardItems;
    }

    private List<MergeRewardItem> DAIlY6_DATA(string str1, string str2)
    {
        List<MergeRewardItem> rewardItems = new List<MergeRewardItem>()
        {
        new MergeRewardItem
        {
            name = "TimeBooster_1",
            num = 1,
        },
        new MergeRewardItem
        {
            name = "Coins",
            num = 55,
        },
        new MergeRewardItem
        {
            name = "EnergyChest",
            num = 1,
        },new MergeRewardItem
        {
            name = str1,
            num = 1,
        },new MergeRewardItem
        {
            name = str2,
            num = 1,
        }
        };
        return rewardItems;
    }

    #endregion

    #region Save And Load
    public void SaveData()
    {
        try
        {
            SaveUtils.SetString("Save_Key_DailyActiveSystem",
                JsonConvert.SerializeObject(new DailyActiveDefinition
                { Daily_Type = Daily_Type, ItemRewardList = ItemRewardList, LastRewardList = LastRewardList, IsComplete = IsComplete, Score = Score, LastDailyType = LastDailyType}));
            SaveUtils.SetLong("SaveKey_Daily_LastRefreshDate", TimeManager.ServerUtcNow().Ticks);
        }
        catch (Exception e)
        {
            GameDebug.LogError("SaveData error!" + e);
        }
    }

    public void LoadData()
    {
        try
        {
            string saveStr = SaveUtils.GetString("Save_Key_DailyActiveSystem");
            if (string.IsNullOrEmpty(saveStr))
            {
                return;
            }
            var data = JsonConvert.DeserializeObject<DailyActiveDefinition>(saveStr);
            if (data == null)
            {
                return;
            }
            Daily_Type = data.Daily_Type;
            ItemRewardList = data.ItemRewardList;
            IsComplete = data.IsComplete;
            LastRewardList = data.LastRewardList;
            LastDailyType = data.LastDailyType;
            Score = data.Score;
            long lastRefeshDateTicks = SaveUtils.GetLong("SaveKey_Daily_LastRefreshDate");
            lastRefreshDateTime = new DateTimeOffset(lastRefeshDateTicks, TimeSpan.Zero);
        }
        catch (Exception e)
        {
            GameDebug.LogError("LoadData error!" + e);
        }
    }
    #endregion
}

public class DailyActiveDefinition
{
    public List<MergeRewardItem> ItemRewardList;
    public List<MergeRewardItem> LastRewardList;
    public MergeLevelType Daily_Type;
    public MergeLevelType LastDailyType;
    public bool IsComplete;
    public int Score;
}