using IvyCore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ShowRewardsData : UIPanelDataBase
{
    public List<MergeRewardItem> rewardList = new List<MergeRewardItem>();
    public MergeRewardItem reward;
    public string season;
    public MergeLevelType levelType;
    public bool isDouble = false;
    public bool is_noOpenUI;
    public Action cb = null;
    public ShowRewardsData(MergeRewardItem reward) : base(Consts.UIPanel_ShowRewards, UIShowLayer.TopPopup)
    {
        this.reward = reward;
    }
    public ShowRewardsData(List<MergeRewardItem> rewardList, string season, MergeLevelType levelType, bool isDouble, Action cb, bool is_noOpenUI) : base(Consts.UIPanel_ShowRewards, UIShowLayer.TopPopup)
    {
        for (int i = 0; i < rewardList.Count; i++)
        {
            MergeRewardItem  x = rewardList[i];
            this.rewardList.Add(x);
        }
        this.season = season;
        this.levelType = levelType;
        this.isDouble = isDouble;
        this.cb = cb;
        this.is_noOpenUI = is_noOpenUI;
        /*this.rewardList = rewardList;*/

    }
}

public enum RewardType 
{
    None,
    Currency,
    Goods
}

public class UIPanel_ShowRewards : UIPanelBase
{
    #region 组件
    [SerializeField] private TextMeshProUGUI textTitle;
    [SerializeField] private TextMeshProUGUI textContinue;
    [SerializeField] private Transform groupTrans;
    [SerializeField] private Button btnClose;
    #endregion

    #region 变量
    private List<Item_Reward> rewardItemList = new List<Item_Reward>();
    private string season;
    private MergeLevelType levelType;
    private bool isDouble;
    private Dictionary<RewardType,Dictionary<string,MergeRewardItem>> RewardsShowDics;
    private Action cb = null;
    private bool is_noOpenUI = false;
    #endregion

    public override void OnInitUI()
    {
        base.OnInitUI();
        btnClose.onClick.AddListener(() =>
        {
            StartCoroutine(HideUI());
        });
    }

    private IEnumerator HideUI() 
    {
        btnClose.enabled = false;
        foreach (var item in rewardItemList)
        {
            item.transform.SetParent(UISystem.Instance.topRootTran);
        }
        textContinue.transform.DOScale(0, 0.3f);
        GameManager.Instance.GiveRewardByPopup(RewardsShowDics, season, rewardItemList, levelType, cb, is_noOpenUI);
        yield return new WaitForSeconds(0.5f);
        UISystem.Instance.HideUI(this);
    }

    private void OnDestroy()
    {
        rewardItemList = null;
    }

    private Dictionary<string, MergeRewardItem> GetSingleRewardTypeDic(RewardType rewardType) 
    {
        Dictionary<string, MergeRewardItem> rewardsDic;
        if (RewardsShowDics.ContainsKey(rewardType))
            rewardsDic = RewardsShowDics[rewardType];
        else
        {
            RewardsShowDics[rewardType] = new Dictionary<string, MergeRewardItem>();
            rewardsDic = RewardsShowDics[rewardType];
        }
        return rewardsDic;
    }

    /// <summary>
    /// 给奖励排序 物品在前，金币砖石在后
    /// </summary>
    private void ReWardSort()
    {
        if (!RewardsShowDics.ContainsKey(RewardType.Goods))
            RewardsShowDics[RewardType.Goods] = new Dictionary<string, MergeRewardItem>();
        else if (!RewardsShowDics.ContainsKey(RewardType.Currency))
            RewardsShowDics[RewardType.Currency] = new Dictionary<string, MergeRewardItem>();
    }

    private void RefreshRewardData(ShowRewardsData data) 
    {
        RewardsShowDics = new Dictionary<RewardType, Dictionary<string, MergeRewardItem>>();

        ReWardSort();

        for (int i = 0; i < data.rewardList.Count; i++)
        {
            Dictionary<string, MergeRewardItem> rewardsDic;
            if (data.rewardList[i].IsCurrency)
                rewardsDic = GetSingleRewardTypeDic(RewardType.Currency);
            else
                rewardsDic = GetSingleRewardTypeDic(RewardType.Goods);

            if (!rewardsDic.ContainsKey(data.rewardList[i].name))
            {
                rewardsDic.Add(data.rewardList[i].name, data.rewardList[i]);
            }
            else if (rewardsDic.TryGetValue(data.rewardList[i].name, out MergeRewardItem item))
            {
                MergeRewardItem newReward = new MergeRewardItem();
                newReward.name = item.name;
                newReward.num = item.num + data.rewardList[i].num;
                rewardsDic[data.rewardList[i].name] = newReward;
            }
        }
    }

    private void RefreshRewardPrefab() 
    {
        int index = 0;
        foreach (var rewardDic in RewardsShowDics.Values)
        {
            foreach (var rewardItem in rewardDic.Values)
            {
                if (index < rewardItemList.Count)
                {
                    if (!rewardItemList[index].gameObject.activeSelf)
                        rewardItemList[index].gameObject.SetActive(true);
                    rewardItemList[index].SetData(rewardItem,isDouble, index);
                    rewardItemList[index].transform.SetParent(groupTrans);
                }
                else
                {
                    var go = AssetSystem.Instance.Instantiate(Consts.Item_Reward, groupTrans);
                    if (go != null && go.TryGetComponent<Item_Reward>(out var reward))
                    {
                        reward.SetData(rewardItem, isDouble, index);
                        rewardItemList.Add(reward);
                    }
                }
                index++;
            }
        }
        for (; index < rewardItemList.Count; index++)
        {
            rewardItemList[index].gameObject.SetActive(false);
        }
    }


    public override IEnumerator OnShowUI()
    {
        textTitle.text = I2.Loc.ScriptLocalization.Get("Obj/Main/Tasks/Text2");
        textContinue.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/DiacoverDescribe5");
        textContinue.transform.localScale = Vector2.one;
        ShowRewardsData data = UIPanelData as ShowRewardsData;
        if (data == null)
            yield break;
        season = data.season;
        levelType = data.levelType;
        isDouble = data.isDouble;
        cb = data.cb;
        is_noOpenUI = data.is_noOpenUI;
        RefreshRewardData(data);
        RefreshRewardPrefab();
        btnClose.enabled = false;
        yield return base.OnShowUI();
        yield return new WaitForSeconds(1.0f);
        btnClose.enabled = true;
    }
}
