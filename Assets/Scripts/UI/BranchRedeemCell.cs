using EnhancedUI.EnhancedScroller;
using IvyCore;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BranchRedeemCell : EnhancedScrollerCellView
{
    [SerializeField] private GameObject bgBetter;
    [SerializeField] private GameObject objJunction;
    [SerializeField] private GameObject objYellowUp;
    [SerializeField] private GameObject objYellowDown;
    [SerializeField] private TextMeshProUGUI textPoint;
    [SerializeField] private Slider sliderNormal;
    [SerializeField] private Slider sliderFirst;
    [SerializeField] private List<GameObject> tagImgNormalList;
    [SerializeField] private List<GameObject> tagImgLastList;
    [SerializeField] private Transform rewardsGroup;
    [SerializeField] private GameObject btnReceived;
    [SerializeField] private Button btnClaim;
    [SerializeField] private GameObject btnNoClaim;
    [SerializeField] private TextMeshProUGUI[] t_Btn;//0已领取，1可领取，2不可领取
    [SerializeField] private GameObject hasClaimTag;//背景虚化
    [SerializeField] private Sprite greenPoint;
    [SerializeField] private Sprite greyPoint;
    [SerializeField] private Sprite greenPointStar;
    [SerializeField] private Sprite greyPointStar;
    private BranchRewardDefinition mData;
    private BranchRewardDefinition preData;
    private BranchRewardDefinition nextData;
    private bool isStart = false;
    private List<BranchRedeemRewardItem> rewardItemList = new List<BranchRedeemRewardItem>();
    private MergeLevelType curLevelType;
    void Start()
    {
        isStart = true;
        RefreshUI();

        btnClaim.onClick.AddListener(() =>
        {
            //播放动画
            if (mData != null)
            {
                if (MergeLevelManager.Instance.IsBranch(curLevelType)) 
                {
                    BranchSystem.Instance.ClaimReward(mData.goalPoint);
                    GameManager.Instance.GiveRewardItem(mData.rewardDataList, "branchReward", Vector3.zero);
                }
                else 
                {
                    FestivalSystem.Instance.ClaimReward(curLevelType, mData.goalPoint);
                    List<MergeRewardItem> rewardList = mData.rewardDataList;
                    List<MergeRewardItem> saveReward = new List<MergeRewardItem>();
                    for (int i = 0; i < rewardList.Count; i++)
                    {
                        if (string.Equals(rewardList[i].ShowRewardPrefabName, "MainBG_F01")) 
                        {
                            ChooseSkinSystem.Instance.SetFestivalSkin(curLevelType);
                        }
                        else 
                        {
                            saveReward.Add(rewardList[i]);
                        }
                    }
                    GameManager.Instance.GiveRewardItem(saveReward, "halloween", Vector3.zero);
                    UI_Manager.Instance.InvokeRefreshEvent("", "RefreshEvent_LanguageChanged");
                }                
                UI_Manager.Instance.InvokeRefreshEvent("", "BranchRedeem_Refresh");
            }
            if (UISystem.Instance.TryGetUI(Consts.UIPanel_Merge, out UIPanel_Merge mergeUI) &&
                mergeUI.isActiveAndEnabled == true)
            {
                mergeUI.RefreshTaskItem();
            }
        });
        UI_Manager.Instance.RegisterRefreshEvent(this.gameObject, (str) =>
        {
            RefreshUI();           
        }, "BranchRedeem_Refresh");
    }

    public void SetData(BranchRewardDefinition data, BranchRewardDefinition pre, BranchRewardDefinition next, MergeLevelType levelType)
    {
        mData = data;
        preData = pre;
        nextData = next;
        curLevelType = levelType;
        t_Btn[0].text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/Reward/Btn2");
        t_Btn[1].text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/Reward/Btn1");
        t_Btn[2].text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/Reward/Btn1");
        if (isStart)
            RefreshUI();
    }

    private void RefreshPointUI(Sprite greenPoint,Sprite greyPoint) 
    {
        int dex = 0;
        foreach (var item in tagImgNormalList)
        {
            if (item.TryGetComponent(out Image image))
            {
                if (dex == 0)
                {
                    image.sprite = greyPoint;
                }
                else
                {
                    image.sprite = greenPoint;
                }
            }
            dex++;
        }
        dex = 0;
        foreach (var item in tagImgLastList)
        {
            if (item.TryGetComponent(out Image image))
            {
                if (dex == 0)
                {
                    image.sprite = greyPoint;
                }
                else
                {
                    image.sprite = greenPoint;
                }
            }
        }
    }

    private void RefreshUI()
    {
        bool achieved;
        bool claimed;
        if (curLevelType == MergeLevelType.branch_SpurLine4 
            || curLevelType == MergeLevelType.branch_SpurLine5 
            || curLevelType == MergeLevelType.branch_SpurLine6)
        {
            RefreshPointUI(greenPoint, greyPoint);
        }
        else
        {
            RefreshPointUI(greenPointStar, greyPointStar);
        }

        if (MergeLevelManager.Instance.IsBranch(curLevelType))
        {
            achieved = BranchSystem.Instance.CanClaim(mData.goalPoint);
            claimed = achieved && BranchSystem.Instance.IsRewardClaimed(mData.goalPoint);
        }
        else
        {
            achieved = FestivalSystem.Instance.CanClaim(curLevelType, mData.goalPoint);
            claimed = achieved && FestivalSystem.Instance.IsRewardClaimed(curLevelType, mData.goalPoint);
        }
       
        //背景
        bgBetter.SetActive(achieved && !claimed);
        //目标积分
        textPoint.text = mData.goalPoint.ToString();

        if (preData != null)
        {
            //连接处
            objJunction.SetActive(true);
            bool achievedpre;
            bool claimedpre;
            if (MergeLevelManager.Instance.IsBranch(curLevelType))
            {
                achievedpre = BranchSystem.Instance.CanClaim(preData.goalPoint);
                claimedpre = achieved && BranchSystem.Instance.IsRewardClaimed(preData.goalPoint);
            }
            else
            {
                achievedpre = FestivalSystem.Instance.CanClaim(curLevelType, preData.goalPoint);
                claimedpre = achieved && FestivalSystem.Instance.IsRewardClaimed(curLevelType, preData.goalPoint);
            }
            objYellowUp.SetActive(achievedpre && !claimedpre);
            objYellowDown.SetActive(achieved && !claimed);

            //进度条
            sliderFirst.gameObject.SetActive(false);
            sliderNormal.gameObject.SetActive(true);
            if (MergeLevelManager.Instance.IsBranch(curLevelType))
            {
                if (BranchSystem.Instance.branchPoint - preData.goalPoint > 0)
                    sliderNormal.value = (BranchSystem.Instance.branchPoint - preData.goalPoint) * 1.0f / (mData.goalPoint - preData.goalPoint);
                else
                    sliderNormal.value = 0;
            }
            else
            {
                if (FestivalSystem.Instance.ReturnCurScore(curLevelType) - preData.goalPoint > 0)
                    sliderNormal.value = FestivalSystem.Instance.ReturnCurScore(curLevelType) - preData.goalPoint / (mData.goalPoint - preData.goalPoint);
                else
                    sliderNormal.value = 0;
            }          

            //imgTag
            tagImgNormalList[0].SetActive(!achievedpre);
            tagImgNormalList[1].SetActive(achievedpre && claimedpre);
            tagImgNormalList[2].SetActive(achievedpre && !claimedpre);
        }
        else
        {
            //连接处
            objJunction.gameObject.SetActive(false);

            //进度条
            sliderFirst.gameObject.SetActive(true);
            sliderNormal.gameObject.SetActive(false);          
            if (MergeLevelManager.Instance.IsBranch(curLevelType))
            {
                sliderFirst.value = BranchSystem.Instance.branchPoint * 1.0f / mData.goalPoint;
            }
            else
            {
                sliderFirst.value = FestivalSystem.Instance.ReturnCurScore(curLevelType) * 1.0f / mData.goalPoint;
            }
            //imgTag
            foreach (var img in tagImgNormalList)
            {
                img.SetActive(false);
            }
        }

        //imgTag
        if (nextData != null)
        {
            foreach (var img in tagImgLastList)
            {
                img.SetActive(false);
            }
        }
        else
        {
            tagImgLastList[0].SetActive(!achieved);
            tagImgLastList[1].SetActive(achieved && claimed);
            tagImgLastList[2].SetActive(achieved && !claimed);
        }

        //奖励
        int index = 0;
        foreach (var item in mData.rewardDataList)
        {
            BranchRedeemRewardItem reward = null;
            if (index < rewardItemList.Count)
            {
                reward = rewardItemList[index];
            }
            else
            {
                var go = AssetSystem.Instance.Instantiate(Consts.BranchRedeemRewardItem, rewardsGroup);
                if (go != null && go.TryGetComponent<BranchRedeemRewardItem>(out reward))
                {
                    rewardItemList.Add(reward);
                }
            }
            if (reward != null)
            {
                reward.SetData(item, mData.rewardRareNumList[index]);
                reward.gameObject.SetActive(true);
            }
            index++;
        }
        for (; index < rewardItemList.Count; index++)
        {
            rewardItemList[index].gameObject.SetActive(false);
        }

        //按钮
        btnReceived.SetActive(achieved && claimed);
        btnClaim.gameObject.SetActive(achieved && !claimed);
        btnNoClaim.SetActive(!achieved);

        hasClaimTag.SetActive(achieved && claimed);
    }

    private void OnDestroy()
    {
        if (UI_Manager.Instance != null && gameObject != null)
            UI_Manager.Instance.UnRegisterRefreshEvent(gameObject);
    }
}
