using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using IvyCore;
using System;

public class UIPanel_ActiveRewardData : UIPanelDataBase
{
    public MergeLevelType _mergeLevel;
    public UIPanel_ActiveRewardData(MergeLevelType mergeLevel) : base(Consts.UIPanel_ActiveReward)
    {
        _mergeLevel = mergeLevel;
    }
}

public class UIPanel_ActiveReward : UIPanelBase
{
    #region 组件
    [SerializeField] private Item_GiftBag item_Show;
    [SerializeField] private Transform transform_Content;
    [SerializeField] private Transform recyclingTrans;
    [SerializeField] private Button btn_close;
    [SerializeField] private Button btn_GetReward;
    [SerializeField] private Button btn_EnterGame;
    [SerializeField] private GameObject btn_NotGetReward;
    [SerializeField] private TextMeshProUGUI text_Title;
    [SerializeField] private TextMeshProUGUI text_Des;
    [SerializeField] private TextMeshProUGUI text_BtnReward;
    [SerializeField] private TextMeshProUGUI text_EnterGame;
    [SerializeField] private TextMeshProUGUI text_NoBtnReward;
    #endregion

    #region 变量
    private List<MergeRewardItem> mergeRewards;
    private bool canReward;
    private MergeLevelType mergeLevel;
    private List<Item_GiftBag> item_Shows=new List<Item_GiftBag>();
    private Action clickCB = null;
    #endregion

    public override void OnInitUI()
    {
        base.OnInitUI();
        item_Show.gameObject.SetActive(false);
        btn_close.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
        btn_GetReward.onClick.AddListener(() =>
        {
            btn_GetReward.gameObject.SetActive(false);
            btn_NotGetReward.gameObject.SetActive(true);
            clickCB?.Invoke();
            FestivalSystem.Instance.refreshFestival?.Invoke();
        });

        btn_EnterGame.onClick.AddListener(() =>
        {
            if (MergeLevelManager.Instance.CurrentLevelType != mergeLevel)
            {
                BranchSystem.Instance.EnterGame();
                UISystem.Instance.HideUI(this);
            }
        });

    }

    /// <summary>
    /// 归一化对象
    /// </summary>
    /// <param name="uIDungeon1FuncItem"></param>
    private void Normalization(Item_GiftBag item_Show)
    {
        item_Show.transform.SetParent(transform_Content);
        item_Show.transform.localScale = Vector3.one;
        item_Show.gameObject.SetActive(true);
    }

    /// <summary>
    /// 刷新子物体UI
    /// </summary>
    /// <param name="exchangeData"></param>
    /// <param name="uIDungeon1FuncItem"></param>
    private void RefreshItem(MergeRewardItem rewardItem, Item_GiftBag item_Show)
    {
        Normalization(item_Show);
        item_Show.SetData(rewardItem);
    }

    /// <summary>
    /// 获取奖励列表
    /// </summary>
    /// <returns></returns>
    private BranchRewardDefinition GetRewardDef() 
    {
        BranchRewardDefinition branchReward = null;
        int current_point = 0;
        int goalPoint = 0;
        switch (mergeLevel)
        {
            case MergeLevelType.branch_christmas:
            case MergeLevelType.branch_halloween:
            case MergeLevelType.branch_SpurLine4:
            case MergeLevelType.branch1:
                branchReward = BranchSystem.Instance.GetCurrentBranchDef();
                current_point = BranchSystem.Instance.branchPoint;
                goalPoint = branchReward.goalPoint;
                canReward = current_point >= goalPoint;
                clickCB = () =>
                {
                    BranchSystem.Instance.ClaimReward(goalPoint);
                    GameManager.Instance.GiveRewardItem(mergeRewards, mergeLevel.ToString(), MergeLevelType.mainLine, false, () =>
                    {
                        UI_PagePlay_Slider.refreshAction?.Invoke();
                        UISystem.Instance.HideUI(this);
                    }, true);
                };
                break;
            default:
                break;
        }
        return branchReward;
    }


    /// <summary>
    /// 初始化奖励内容
    /// </summary>
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        UIPanel_ActiveRewardData data = UIPanelData as UIPanel_ActiveRewardData;
        if (data == null)
            yield break;
        mergeLevel = data._mergeLevel;
        //获取奖励
        BranchRewardDefinition branchReward = GetRewardDef();
        if(branchReward==null)
            yield break;
        mergeRewards = branchReward.rewardDataList;
        for (int i = 0; i < mergeRewards.Count; i++)
        {
            //实例化对象
            if (item_Shows.Count <= i)
                item_Shows.Add(Instantiate(item_Show));
            //刷新数据
            RefreshItem(mergeRewards[i], item_Shows[i]);
        }
        int index = mergeRewards.Count;
        for (int i = index; i < item_Shows.Count; i++)
        {
            item_Shows[i].gameObject.SetActive(false);
            item_Shows[i].transform.SetParent(recyclingTrans);
        }
        btn_GetReward.gameObject.SetActive(canReward);
        btn_EnterGame.gameObject.SetActive(!canReward);
        btn_NotGetReward.gameObject.SetActive(false);
        text_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Main/Tasks/Text1");
        text_BtnReward.text= I2.Loc.ScriptLocalization.Get("Obj/Chain/Level_Button");
        text_NoBtnReward.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Level_Button");
        text_EnterGame.text= I2.Loc.ScriptLocalization.Get("Obj/Dungeon/ButtonText2");
    }

    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
    }

}
