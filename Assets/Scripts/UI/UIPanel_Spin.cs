using IvyCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel_Spin : UIPanelBase
{
    [SerializeField] private Transform wheelTran;
    [SerializeField] private Transform arrowTran;
    [SerializeField] private Button Btn_free;
    [SerializeField] private Button Btn_cost;
    [SerializeField] private Button btn_close;
    [SerializeField] private SpinWheelTimeline wheel;

    [SerializeField] private TextMeshProUGUI text_title;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI spinFreeText;
    [SerializeField] private List<UI_Spin_Item> spinItemList;

    private Dictionary<int, SpinWheelReward> curRewardDic;
    private int rewardIndex;
    int totalWeight = 0;

    private void Awake()
    {
        btn_close.onClick.AddListener(() =>
        {
            if (wheel.isFinish)
                UISystem.Instance.HideUI(this);
        });

        Btn_free.onClick.AddListener(() =>
        {
            OnBtnSpinClicked();
        });
        Btn_cost.onClick.AddListener(() =>
        {
            OnBtnSpinClicked();
        });
        CalculateTotalWeight();

        //关闭奖励页后刷新奖励UI
        UI_Manager.Instance.RegisterRefreshEvent(this.gameObject, (str) =>
        {
            RefreshRewardItems();
        }, "UIPanel_Spin_RefreshRewardItems");
    }
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        text_title.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Shop_SpinWheel");
        if (SpinWheelFree()) //免费开启
        {
            Btn_free.gameObject.SetActive(true);
            Btn_cost.gameObject.SetActive(false);
            spinFreeText.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/SpinWheel_Describe3");
        }
        else
        {
            Btn_free.gameObject.SetActive(false);
            Btn_cost.gameObject.SetActive(true);
            spinFreeText.gameObject.SetActive(false);
            //spinText.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/SpinWheel_Describe4");
            costText.text = SpinWheelDefinition.cost.ToString();
        }

        if (NewDayRefreshWheelItem())
            RefreshSpinWheelRewards();
        else
            LoadSpinWheelRewards();
        RefreshRewardItems();
    }

    public static bool SpinWheelFree()
    {
        if (ExtensionTool.IsDateToday(PlayerData.SpinWheelFreeTime, TimeManager.Instance.UtcNow()))
            return false;
        else
            return true;
    }

    private bool NewDayRefreshWheelItem()
    {
        bool refresh = false;
        if (SaveUtils.HasKey(Consts.SaveKey_PlayerData_RefreshWheelItemsTime))
        {
            var lastSpinTime = DateTimeOffset.Parse(SaveUtils.GetString(Consts.SaveKey_PlayerData_RefreshWheelItemsTime));
            if (!ExtensionTool.IsDateToday(lastSpinTime, TimeManager.Instance.UtcNow()))
                refresh = true;
        }
        else
            refresh = true;
        return refresh;
    }

    private void CalculateTotalWeight()
    {
        totalWeight = 0;
        foreach (var def in SpinWheelDefinition.DefinitionDict)
        {
            if (!def.Value.Isunlock || def.Value.IsClose)
                continue;
            totalWeight += def.Value.weight;
        }
    }

    private void LoadSpinWheelRewards()
    {
        if (SaveUtils.HasKey(Consts.SaveKey_PlayerData_SpinWheelItems))
        {
            curRewardDic = JsonConvert.DeserializeObject<Dictionary<int, SpinWheelReward>>(SaveUtils.GetString(Consts.SaveKey_PlayerData_SpinWheelItems));
            if(curRewardDic == null || curRewardDic.Count == 0)
                RefreshSpinWheelRewards();//防止存档发生错误
        }
        else
            RefreshSpinWheelRewards();
    }

    private void RefreshSpinWheelRewards()
    {
        if (curRewardDic == null)
            curRewardDic = new Dictionary<int, SpinWheelReward>();
        curRewardDic.Clear();
        foreach (var def in SpinWheelDefinition.DefinitionDict)
        {
            var reward = def.Value.itemPool.GetRandomRewardItem();
            if (!def.Value.Isunlock || def.Value.IsClose)
                continue;
            SpinWheelReward spinWheelReward = new SpinWheelReward();
            spinWheelReward.rewardName = reward.name;
            spinWheelReward.count = reward.num;
            spinWheelReward.weight = def.Value.weight;
            curRewardDic.Add(def.Key, spinWheelReward);
        }
        SaveUtils.SetString(Consts.SaveKey_PlayerData_SpinWheelItems, JsonConvert.SerializeObject(curRewardDic));
        SaveUtils.SetString(Consts.SaveKey_PlayerData_RefreshWheelItemsTime, TimeManager.Instance.UtcNow().ToString());

    }

    private void RefreshRewardItems()
    {
        if (spinItemList != null)
        {
            var index = 0;
            foreach (var reward in curRewardDic)
            {

                if (index < spinItemList.Count)
                {
                    spinItemList[index].SetData(reward.Value.rewardName, reward.Value.count);
                }
                index++;
            }
        }
    }

    private void OnBtnSpinClicked()//开始按钮
    {
        if (!wheel.isFinish)
            return;

        if (!SpinWheelFree() && !Currencies.CanAffordOrTip(CurrencyID.Gems, SpinWheelDefinition.cost))
        {
            return;
        }

        int random = UnityEngine.Random.Range(1, totalWeight + 1);
        int currentTotal = 0;
        int index = 0;
        foreach (var def in curRewardDic)
        {
            if (currentTotal + def.Value.weight >= random)
            {
                rewardIndex = def.Key - 1;
                break;
            }
            index++;
            currentTotal += def.Value.weight;
        }
        wheel.SpinWheel_ByDOTween(index, () =>
        {
            //soundEffect
            //AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Break_Box);
            //cost
            if (SpinWheelFree())
            {
                PlayerData.SpinWheelFreeTime = TimeManager.Instance.UtcNow();
                GameManager.Instance.SavePlayerData_SpinWheelFreeTime();
                AnalyticsUtil.TrackEvent("track_spin", new Dictionary<string, string>() {
                    {"value","0" }
                });
                UIPanel_TopBanner.refreshBanner?.Invoke();
            }
            else
            {
                Currencies.Spend(CurrencyID.Gems, SpinWheelDefinition.cost, "wheel");
                AnalyticsUtil.TrackEvent("track_spin", new Dictionary<string, string>() {
                    {"value",SpinWheelDefinition.cost.ToString() }
                });
            }
            //refreshText
            Btn_cost.gameObject.SetActive(true);
            Btn_free.gameObject.SetActive(false);
            spinFreeText.gameObject.SetActive(false);
            costText.text = SpinWheelDefinition.cost.ToString();
            //giveRewards
            var reward = new MergeRewardItem()
            {
                name = curRewardDic[rewardIndex + 1].rewardName,
                num = curRewardDic[rewardIndex + 1].count
            };
            GameManager.Instance.GiveRewardItem(reward, "SpinWheel", spinItemList[index].transform.position, false);
            //showRewardPanel
            //if (reward.IsRewardPrefab)
            UISystem.Instance.ShowUI(new ShowRewardsData(reward));
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.spinReward);
            //refreshRewards
            RefreshSpinWheelRewards();
            //RefreshRewardItems();
        }, () =>
        {

        });
    }
}

public class SpinWheelReward
{
    public string rewardName;
    public int count;
    public int weight;
}
