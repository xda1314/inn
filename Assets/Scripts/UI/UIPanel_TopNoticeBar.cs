using DG.Tweening;
using IvyCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelData_TopNoticeBar :UIPanelDataBase
{
    public TopNoticeType type;
    public UIPanelData_TopNoticeBar(TopNoticeType type) : base(Consts.UIPanel_TopNoticeBar, UIShowLayer.TopPopup)
    {
        this.type = type;
    }
}
public class UIPanel_TopNoticeBar : UIPanelBase
{
    [SerializeField] private Transform tweenRoot;
    [SerializeField] private Button[] btn_Goto;
    [SerializeField] private TextMeshProUGUI[] t_Desc;
    [SerializeField] private Button btn_Close;
    [SerializeField] private Image img_Reward;
    [SerializeField] private ImageSpritesContainer container;

    private TopNoticeType noticeType = TopNoticeType.None;
    private float startPosY;
    public override void OnInitUI()
    {
        base.OnInitUI();       
        startPosY = tweenRoot.transform.localPosition.y;
        for (int i = 0; i < btn_Goto.Length; i++) 
        {
            int index = i;
            btn_Goto[index].onClick.AddListener(() => GotoBtnClick(index));
        }
        btn_Close.onClick.AddListener(() => TweenBack());
    }
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        tweenRoot.localPosition = new Vector3(tweenRoot.localPosition.x, startPosY, 0);
        noticeType = ((UIPanelData_TopNoticeBar)UIPanelData).type;
        RefreshUI();
        if (noticeType != TopNoticeType.None)
        {
            yield return TweenPosition();
        }
    }
    bool canTweenBack = false;
    private IEnumerator TweenPosition() 
    {
        yield return null;
        tweenRoot.DOLocalMoveY(startPosY - 500, 0.6f);
        yield return new WaitForSeconds(1f);
        canTweenBack = true;
    }
    private void TweenBack() 
    {
        if (!canTweenBack)
            return;
        tweenRoot.DOLocalMoveY(startPosY, 0.6f).onComplete += () =>
        {
            UISystem.Instance.HideUI(this);
        };
    }

    private void GotoBtnClick(int index) 
    {
        switch (index) 
        {
            case 0:
                AdManager.PlayAd(Vector3.zero, GameManager.Instance.adTag, () =>
                {
                    List<MergeRewardItem> rewardList = GameManager.Instance.rewardList;
                    MergeLevelType levelType = MergeLevelManager.Instance.CurrentLevelType;
                    GameManager.Instance.GiveRewardItem(rewardList, "WatchAD", Vector3.zero, levelType);
                    ShopSystem.Instance.RefreshDailyDataByReduceCount(rewardList);
                    //MergeActionSystem.OnMergeActionEvent(MergeActionType.AddEnergy);
                    GameManager.Instance.RemovePlayAdFailEvent();
                });              
                break;
            case 1:
                UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_DailyTask));
                break;
            case 2:
                UISystem.Instance.ShowUI(new TopPopupData(TopResourceType.Exp));
                break;
            case 3:
                UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_StarRewards));
                break;
            case 4:
                UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_LevelRewards));
                break;
            default:
                break;
        }
        UISystem.Instance.HideUI(this);
    }
    private void RefreshUI() 
    {
        for (int i = 0; i < btn_Goto.Length; i++)
        {
            btn_Goto[i].gameObject.SetActive(false);
        }

        switch (noticeType) 
        {
            case TopNoticeType.ADReady:
                btn_Goto[0].gameObject.SetActive(true);
                string spriteName = string.Empty;
                string itemName = string.Empty;
                switch (GameManager.Instance.adTag) 
                {
                    case AdManager.ADTag.Energy:
                        spriteName = "energy";
                        itemName = I2.Loc.ScriptLocalization.Get("Obj/Name/Energy");
                        break;
                    case AdManager.ADTag.dailydeals_EnergyChest:
                        spriteName = "EnergyChest";
                        itemName = I2.Loc.ScriptLocalization.Get("Obj/Name/EnergyChest");
                        break;
                    case AdManager.ADTag.dailydeals_coin:
                        spriteName = "coin_icon_big";
                        itemName = I2.Loc.ScriptLocalization.Get("Obj/Name/Coins_1");
                        break;
                    case AdManager.ADTag.dailydeals_gem:
                        spriteName = "gem";
                        itemName = I2.Loc.ScriptLocalization.Get("Obj/Name/Diamonds");
                        break;
                    case AdManager.ADTag.dailydeals_needle:
                        spriteName = "Needle2";
                        itemName = I2.Loc.ScriptLocalization.Get("Obj/Name/Needle");
                        break;
                    default:
                        break;
                }
                if (!string.IsNullOrEmpty(spriteName)) 
                {
                    img_Reward.sprite = container.GetSprite(spriteName);
                }
                string text = string.Format(I2.Loc.ScriptLocalization.Get("Obj/Chain/WatchVideo_Text7"), itemName);
                text = text.Replace("\\n", "\n");
                t_Desc[0].text = text;
                break;
            case TopNoticeType.CompleteDailyTask:
                btn_Goto[1].gameObject.SetActive(true);
                t_Desc[1].text = I2.Loc.ScriptLocalization.Get("Obj/Banner/Text3");
                break;
            case TopNoticeType.PlayerLevelRewardReady:
                btn_Goto[2].gameObject.SetActive(true);
                t_Desc[2].text = I2.Loc.ScriptLocalization.Get("Obj/Banner/Text4");
                break;
            case TopNoticeType.StarRewardReady:
                btn_Goto[3].gameObject.SetActive(true);
                t_Desc[3].text = I2.Loc.ScriptLocalization.Get("Obj/Banner/Text5");
                break;
            case TopNoticeType.LevelRewardReady:
                btn_Goto[4].gameObject.SetActive(true);
                t_Desc[4].text = I2.Loc.ScriptLocalization.Get("Obj/Banner/Text6");
                break;
            default:
                break;
        }
    }

}


public enum TopNoticeType 
{
    None,
    ADReady,
    CompleteDailyTask,
    PlayerLevelRewardReady,//等级奖励
    StarRewardReady,
    LevelRewardReady,//关卡奖励
}