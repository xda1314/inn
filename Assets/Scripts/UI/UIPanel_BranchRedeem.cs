using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIPanelData_BranchRedeem : UIPanelDataBase
{
    public MergeLevelType levelType;
    public UIPanelData_BranchRedeem(MergeLevelType levelType) : base(Consts.UIPanel_BranchRedeem)
    {
        this.levelType = levelType;
    }
}
public class UIPanel_BranchRedeem : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI textTitle;
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnEnterGame;
    [SerializeField] private TextMeshProUGUI textPointdes;
    [SerializeField] private TextMeshProUGUI textPoint;
    [SerializeField] private TextMeshProUGUI textLeftTime;
    [SerializeField] private TextMeshProUGUI textRnterGame;
    [SerializeField] private TextMeshProUGUI t_CanNotEnter;

    [SerializeField] private BranchRedeemScrollController scrollController;
    private MergeLevelType curLevelType;
    public override void OnInitUI()
    {
        base.OnInitUI();
        btnClose.onClick.AddListener(() =>
        {
            CloseView();
        });
        btnEnterGame.onClick.AddListener(() =>
        {
            if (MergeLevelManager.Instance.CurrentLevelType != curLevelType)
            {
                if (MergeLevelManager.Instance.IsBranch(curLevelType))
                {
                    BranchSystem.Instance.EnterGame();
                }
                else
                {
                    if (FestivalSystem.Instance.IsFestivalOpen(curLevelType) && FestivalSystem.Instance.IsActive(curLevelType))
                        MergeLevelManager.Instance.ShowMergePanelByDungeonType(curLevelType);
                }
                this.gameObject.SetActive(false);
            }
            CloseView();
        });
        BranchSystem.Instance.refreshByReset += () => { CloseView(); };
    }

    public override IEnumerator OnShowUI()
    {
        curLevelType = ((UIPanelData_BranchRedeem)UIPanelData).levelType;

        textPointdes.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine1Button1");
        textRnterGame.text = I2.Loc.ScriptLocalization.Get("Obj/Dungeon/ButtonText2");
        t_CanNotEnter.text = string.Format(I2.Loc.ScriptLocalization.Get("Obj/Chain/EctypeDescribe2"), FestivalSystem.Instance.ReturnOpenLv(curLevelType).ToString());

        if (MergeLevelManager.Instance.IsBranch(curLevelType))
        {
            bool canEnter = TaskGoalsManager.Instance.curLevelIndex >= BranchSystem.Instance.Remote_UnlockLevel;
            btnEnterGame.gameObject.SetActive(canEnter);
            t_CanNotEnter.transform.parent.gameObject.SetActive(!canEnter);
            if(curLevelType == MergeLevelType.branch1)
                textTitle.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine1Title");
            else if(curLevelType == MergeLevelType.branch_christmas)
                textTitle.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine3Title");
            else if (curLevelType == MergeLevelType.branch_halloween)
                textTitle.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine2Title");
            else if (curLevelType == MergeLevelType.branch_SpurLine4) 
            {
                textTitle.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine4Title");
            }
            else if (curLevelType == MergeLevelType.branch_SpurLine5)
            {
                textTitle.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine5Title");
            }
            else if (curLevelType == MergeLevelType.branch_SpurLine6)
            {
                textTitle.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine6Title");
            }
            if (BranchSystem.Instance.GetIsOpen())
            {
                textPoint.text = BranchSystem.Instance.branchPoint.ToString();
                updateTag = true;
                updateInterval = 1.1f;
                var rewards = BranchSystem.Instance.GetRewardsList();
                if (rewards != null)
                    scrollController.SetData(rewards, curLevelType);

            }
            else
            {
                CloseView();
            }
        }
        else
        {
            bool canEnter = FestivalSystem.Instance.IsFestivalOpen(curLevelType) && FestivalSystem.Instance.IsActive(curLevelType);
            btnEnterGame.gameObject.SetActive(canEnter);
            t_CanNotEnter.transform.parent.gameObject.SetActive(!canEnter);
            switch (curLevelType)
            {
                case MergeLevelType.halloween:
                    textTitle.text = I2.Loc.ScriptLocalization.Get("Obj/Festival/Festival1Title");
                    break;
                case MergeLevelType.christmas:
                    textTitle.text = I2.Loc.ScriptLocalization.Get("Obj/Festival/Festival2Title");
                    break;
                case MergeLevelType.lover:
                    textTitle.text = I2.Loc.ScriptLocalization.Get("Obj/Festival/Festival3Title");
                    break;
            }
            if (FestivalSystem.Instance.IsFestivalOpen(curLevelType))
            {
                textPoint.text = FestivalSystem.Instance.ReturnCurScore(curLevelType).ToString();
                updateTag = true;
                updateInterval = 1.1f;
                var rewards = FestivalSystem.Instance.GetRewardsList(curLevelType);
                if (rewards != null)
                    scrollController.SetData(rewards, curLevelType);
            }
        }
        yield return null;
        yield return base.OnShowUI();
        Invoke("JumpToCurrentData", 0.25f);
    }

    public void JumpToCurrentData()
    {
        var currentIndex = -1;
        if (MergeLevelManager.Instance.IsBranch(curLevelType))
        {
            currentIndex = BranchSystem.Instance.GetCurrentIndex();
        }
        else if (MergeLevelManager.Instance.IsFestivalBranch(curLevelType))
        {

        }
        if (currentIndex > 0)
        {

            scrollController.JumpToCurrentData(currentIndex);
        }
    }


    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
        updateTag = false;
    }

    private bool updateTag = false;
    private float updateInterval = 1.1f;
    private void Update()
    {
        if (updateTag)
        {
            updateInterval += Time.deltaTime;
            if (updateInterval > 1.0f)
            {
                updateInterval--;
                if (MergeLevelManager.Instance.IsBranch(curLevelType))
                {
                    var leftSeconds = (int)(BranchSystem.Instance.Branch_EndTime - TimeManager.Instance.UtcNow()).TotalSeconds;//老玩家
                    if (leftSeconds > 0)
                    {
                        textLeftTime.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(leftSeconds);
                    }
                    else
                    {
                        var newLeftTime = (int)(BranchSystem.Instance.curStartTime.AddDays(BranchSystem.Instance.NewLoopTime) - TimeManager.Instance.UtcNow()).TotalSeconds;//新玩家
                        if (newLeftTime > 0)
                        {
                            textLeftTime.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(newLeftTime);
                        }
                        else
                        {
                            CloseView();
                        }
                    }
                }
                else
                {
                    var leftSeconds = (int)(FestivalSystem.Instance.GetFestivalEndTime(curLevelType) - TimeManager.Instance.UtcNow()).TotalSeconds;
                    if (leftSeconds > 0)
                    {
                        textLeftTime.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(leftSeconds);
                    }
                    else
                    {
                        CloseView();
                    }
                }
            }
        }
    }

    private void CloseView()
    {
        UISystem.Instance.HideUI(this);
    }
}
