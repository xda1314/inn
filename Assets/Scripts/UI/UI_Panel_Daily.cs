using Ivy.Purchase;
using IvyCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DailyOpenType
{
    NORMAL,
    DAILYEND,
    BRANCHEND,
}

//定义弹窗类型
public class UIPanelData_Daily : UIPanelDataBase 
{
    public DailyOpenType dailyOpen;
    public UIPanelData_Daily(DailyOpenType activeOpen):base(Consts.UI_Panel_Daily)
    {
        dailyOpen = activeOpen;
    }
}

public class UI_Panel_Daily : UIPanelBase
{
    #region 组件
    //活动进行
    [SerializeField] private GameObject img_bg1;
    [SerializeField] private DailyItem dailyItem;
    [SerializeField] private TextMeshProUGUI text_title;
    [SerializeField] private TextMeshProUGUI text_des;
    [SerializeField] private TextMeshProUGUI text_countDown;
    [SerializeField] private Transform rewardGrid;
    [SerializeField] private Button btn_close;
    [SerializeField] private Button btn_enter;
    [SerializeField] private TextMeshProUGUI text_enter;
    [SerializeField] private UINovicePackageItem uINovicePackageItem;
    //活动结束
    [SerializeField] private GameObject img_bg2;
    [SerializeField] private Button btn_award;
    [SerializeField] private TextMeshProUGUI text_award;
    [SerializeField] private TextMeshProUGUI text_awardDes;
    [SerializeField] private Transform control;
    #endregion 组件

    #region 变量
    [SerializeField] private List<UINovicePackageItem> uINovicePackageItems = new List<UINovicePackageItem>();
    private List<DailyItem> dailyItems = new List<DailyItem>();
    private DailyOpenType dailyOpen;
    private List<MergeRewardItem> itemRewardList = new List<MergeRewardItem>();
    #endregion

    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_enter.onClick.AddListener(() =>
        {
            if (DailyActiveSystem.Instance.GetNoComplete()) 
            {
                MergeLevelManager.Instance.ShowMergePanelByDungeonType(DailyActiveSystem.Instance.Daily_Type);
                this.gameObject.SetActive(false);
                UISystem.Instance.HideUI(this);
            }
        });
        btn_award.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
        btn_close.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
    }

    /// <summary>
    /// 归一化对象
    /// </summary>    
    /// <param name="dailyItem"></param>
    private void Normalization(DailyItem dailyItem)
    {
        dailyItem.transform.SetParent(rewardGrid.transform, false);
        dailyItem.gameObject.SetActive(true);
    }

    public override IEnumerator OnHideUI() 
    {
        if(dailyOpen!= DailyOpenType.NORMAL)
            GameManager.Instance.GiveRewardItem(itemRewardList, dailyOpen.ToString());
        yield return base.OnHideUI();
    }

    /// <summary>
    /// 刷新子物体UI
    /// </summary>
    /// <param name="exchangeData"></param>
    /// <param name="uIDungeon1FuncItem"></param>
    private void RefreshItem(MergeRewardItem rewardItem, DailyItem dailyItem)
    {
        Normalization(dailyItem);
        dailyItem.SetData(rewardItem);
    }

    public override IEnumerator OnShowUI()
    {
        foreach (Transform child in rewardGrid)
        {
            child.gameObject.SetActive(false);
        }
        yield return base.OnShowUI();
        this.dailyOpen  = ((UIPanelData_Daily)UIPanelData).dailyOpen;
        RefreshTopReward(this.dailyOpen);
        RefreshGuarantees(this.dailyOpen);
    }

    private void RefreshTopReward(DailyOpenType dailyOpen) 
    {
        img_bg1.gameObject.SetActive(dailyOpen == DailyOpenType.NORMAL);
        img_bg2.gameObject.SetActive(dailyOpen != DailyOpenType.NORMAL);
        List<MergeRewardItem> itemRewardList = null;
        text_awardDes.text = I2.Loc.ScriptLocalization.Get("Obj/DailyEvent/DailyEventParticipationRewards");
        switch (dailyOpen)
        {
            case DailyOpenType.NORMAL:
                itemRewardList = DailyActiveSystem.Instance.ItemRewardList;
                text_title.text = I2.Loc.ScriptLocalization.Get(DailyDefinition.DailyDefDic[DailyActiveSystem.Instance.Daily_Type].textTitle);
                text_des.text = I2.Loc.ScriptLocalization.Get("Obj/DailyEvent/DailyEventRewards");
                text_enter.text = I2.Loc.ScriptLocalization.Get("Obj/Dungeon/ButtonText2");
                break;
            case DailyOpenType.DAILYEND:
                itemRewardList = DailyActiveSystem.Instance.LastRewardList;
                text_title.text = I2.Loc.ScriptLocalization.Get(DailyDefinition.DailyDefDic[DailyActiveSystem.Instance.LastDailyType].textTitle);
                text_des.text = I2.Loc.ScriptLocalization.Get("Obj/DailyEvent/DailyEventRewards");
                text_award.text = I2.Loc.ScriptLocalization.Get("Obj/Dungeon/ButtonText2");
                break;
            case DailyOpenType.BRANCHEND:
                BranchSystem branch = BranchSystem.Instance;
                itemRewardList = branch.LastRewardList;
                text_title.text = I2.Loc.ScriptLocalization.Get(branch.GetLastBranchControl().Title);
                text_des.text = I2.Loc.ScriptLocalization.Get("Obj/DailyEvent/DailyEventRewards");
                text_award.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Level_Button");
                break;
        }
        for (int i = 0; i < itemRewardList.Count; i++)
        {
            if (dailyItems.Count <= i)
                dailyItems.Add(Instantiate(dailyItem));
            RefreshItem(itemRewardList[i], dailyItems[i]);
        }
        for (int i = itemRewardList.Count; i < dailyItems.Count; i++)
        {
            dailyItems[i].gameObject.SetActive(false);
        }
        SetCountDownTime();
    }

    /// <summary>
    /// 归一化对象
    /// </summary>
    /// <param name="uIDungeon1FuncItem"></param>
    private void Normalization(UINovicePackageItem uINovicePackageItem)
    {
        uINovicePackageItem.transform.SetParent(control.transform, false);
        uINovicePackageItem.gameObject.SetActive(true);
    }

    /// <summary>
    /// 刷新子物体UI
    /// </summary>
    /// <param name="exchangeData"></param>
    /// <param name="uIDungeon1FuncItem"></param>
    private void RefreshItem(MergeRewardItem rewardItem, UINovicePackageItem uINovicePackageItem)
    {
        Normalization(uINovicePackageItem);
        uINovicePackageItem.SetData(rewardItem);
    }

    private void RefreshGuarantees(DailyOpenType dailyOpen) 
    {
        if (dailyOpen == DailyOpenType.NORMAL) 
        {
            return;
        }
        switch (dailyOpen)
        {
            case DailyOpenType.DAILYEND:
                itemRewardList = DailyActiveSystem.Instance.GuaranteedRewards;
                break;
            case DailyOpenType.BRANCHEND:
                itemRewardList = BranchSystem.Instance.GuaranteedRewards;
                break;
        }
        for (int i = 0; i < itemRewardList.Count; i++)
        {
            //实例化对象
            if (uINovicePackageItems.Count <= i)
                uINovicePackageItems.Add(Instantiate(uINovicePackageItem));
            //刷新数据
            RefreshItem(itemRewardList[i], uINovicePackageItems[i]);
        }
    }


    private void SetCountDownTime() 
    {
        TimeSpan timeSpan = TimeManager.Instance.GetTomorrowRefreshTimeSpan();
        if (timeSpan.TotalSeconds < 0)
        {
            text_countDown.text = "00:00";
            return;
        }
        text_countDown.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo((int)timeSpan.TotalSeconds);
    }

    private float timer = 0f;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < 1)
        {
            return;
        }
        timer -= 1;
        if (text_countDown != null)
        {
            SetCountDownTime();
        }
    }
}
