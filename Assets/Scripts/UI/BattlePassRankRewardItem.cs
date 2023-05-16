using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattlePassRankRewardItem : MonoBehaviour
{
    //vip
    [SerializeField] private GameObject[] vipBgs;//0未解锁，1已解锁未领取，2已领取
    [SerializeField] private Button btn_VipClaim;         //领取免费奖励按钮
    [SerializeField] private TextMeshProUGUI t_VipClaim;      //付费奖励按钮文本   
    [SerializeField] private Transform vipItemTran;
    [SerializeField] private GameObject iconVIP_Got;     //付费奖励已领取图标
    [SerializeField] private GameObject icon_Lock;   //付费奖励未解锁图标

    //free
    [SerializeField] private GameObject[] freeBgs;
    [SerializeField] private Button btn_FreeClaim;        //领取免费奖励按钮
    [SerializeField] private TextMeshProUGUI t_FreeClaim;       //免费奖励按钮文本
    [SerializeField] private Transform freeItemTran;
    [SerializeField] private GameObject iconFree_Got;    //免费奖励已领取图标

    //middle
    [SerializeField] private GameObject centerIndexBg;
    [SerializeField] private TextMeshProUGUI t_CenterIndex;    //中间位置文本   
    [SerializeField] private GameObject firstLockItem;

    private CanvasGroup freeItemCanvans;
    private CanvasGroup vipItemCanvans;
    private BattlePassRewardState freeItemState_BP;    //BattlePass免费奖励状态
    private BattlePassRewardState vipItemState_BP;     //BattlePass付费奖励状态
    private GameObject freeItem;
    private GameObject vipItem;
    private BattlePassDefinition battlePassDefinition;
    private int curIndex = -1;
    private bool hasRefresh = false;
    public void InitItem() 
    {
        btn_FreeClaim.onClick.AddListener(GetBattlePassReward_Free);
        btn_VipClaim.onClick.AddListener(GetBattlePassReward_VIP);
        freeItemCanvans = freeItemTran.GetComponent<CanvasGroup>();
        vipItemCanvans = vipItemTran.GetComponent<CanvasGroup>();
    }

    public void RefreshItemBySlider(BattlePassDefinition definition,int index)
    {
        battlePassDefinition = definition;
        curIndex = index;
        if (string.IsNullOrEmpty(battlePassDefinition.freeReward.name) && string.IsNullOrEmpty(battlePassDefinition.vipReward.name))
            return;

        if (freeItem == null) 
        {
            freeItem = AssetSystem.Instance.Instantiate(battlePassDefinition.freeReward.name, freeItemTran);
        }
        if (vipItem == null) 
        {
            vipItem = AssetSystem.Instance.Instantiate(battlePassDefinition.vipReward.name, vipItemTran);
        }

        //if (!hasRefresh) //只在创建时刷新一次,滑动无需刷新,跟随创建时刷新即可
        //{
        //    RefreshItem();
        //    hasRefresh = true;
        //}
        RefreshItem();
        RefreshLockPos();
    }
    public void RefreshItem()
    {
        SetupInfomation();
        SwitchState();
        RefreshFreeState(freeItemState_BP);
        RefreshVipState(vipItemState_BP);
    }

    /// <summary>
    /// BattlePass状态切换
    /// </summary>
    /// <param name="isFree">是否购买</param>
    public void SwitchState()
    {
        BattlePassSystem.freeClaimedData.TryGetValue(curIndex, out bool hasClaimedFree);
        freeItemState_BP = hasClaimedFree ? BattlePassRewardState.Claimed : freeItemState_BP;

        BattlePassSystem.payClaimedData.TryGetValue(curIndex, out bool hasClaimedVip);
        vipItemState_BP = hasClaimedVip ? BattlePassRewardState.Claimed : vipItemState_BP;      
    }


    private void RefreshFreeState(BattlePassRewardState itemState) 
    {
        for (int i = 0; i < freeBgs.Length; i++)
        {
            if (freeBgs[i].activeSelf)
                freeBgs[i].SetActive(false);
        }

        switch (itemState)
        {
            case BattlePassRewardState.Lock:
                freeBgs[0].SetActive(true);
                iconFree_Got.SetActive(false);
                btn_FreeClaim.gameObject.SetActive(false);
                freeItemCanvans.alpha = 1;
                break;
            case BattlePassRewardState.NoReward:
                //GameDebug.LogError("NoReward");
                //iconFree_Got.SetActive(false);
                //btn_FreeClaim.gameObject.SetActive(false);
                //break;
            case BattlePassRewardState.UnlockButNotClaimed:
                freeBgs[1].SetActive(true);
                iconFree_Got.SetActive(false);
                btn_FreeClaim.gameObject.SetActive(true);
                freeItemCanvans.alpha = 1;
                break;
            case BattlePassRewardState.Claimed:
                freeBgs[2].SetActive(true);
                iconFree_Got.SetActive(true);
                btn_FreeClaim.gameObject.SetActive(false);
                freeItemCanvans.alpha = 0.5f;
                break;
            default:
                GameDebug.LogError($"{itemState}---错误");
                break;
        }
    }
    private void RefreshVipState(BattlePassRewardState itemState) 
    {
        for (int i = 0; i < vipBgs.Length; i++)
        {
            if (vipBgs[i].activeSelf)
                vipBgs[i].SetActive(false);
        }
        if (!BattlePassSystem.Instance.IsPay)
        {
            icon_Lock.SetActive(true);
        }
        else 
        {
            icon_Lock.SetActive(false);
        }

        switch (itemState)
        {
            case BattlePassRewardState.Lock:
                vipBgs[0].SetActive(true);
                iconVIP_Got.SetActive(false);
                btn_VipClaim.gameObject.SetActive(false);
                vipItemCanvans.alpha = 1;
                break;
            case BattlePassRewardState.NoReward:
                //GameDebug.LogError("NoReward");
                //icon_Lock.SetActive(true);
                //iconVIP_Got.SetActive(false);
                //btn_VipClaim.gameObject.SetActive(false);
                //break;
            case BattlePassRewardState.UnlockButNotClaimed:
                vipBgs[1].SetActive(true);
                iconVIP_Got.SetActive(false);             
                btn_VipClaim.gameObject.SetActive(true);
                vipItemCanvans.alpha = 1;
                break;
            case BattlePassRewardState.Claimed:
                vipBgs[2].SetActive(true);
                iconVIP_Got.SetActive(true);
                btn_VipClaim.gameObject.SetActive(false);
                vipItemCanvans.alpha = 0.5f;
                break;
            default:
                GameDebug.LogError($"{itemState}---错误");
                break;
        }
    }
    private void RefreshLockPos() 
    {
        if (battlePassDefinition.stage < BattlePassSystem.Instance.curExpLevel + 1)
        {
            centerIndexBg.SetActive(true);
        }
        else 
        {
            centerIndexBg.SetActive(false);
        }
        if (battlePassDefinition.stage == BattlePassSystem.Instance.curExpLevel + 1)
        {
            firstLockItem.SetActive(true);
            this.transform.SetAsLastSibling();
        }
        else 
        {
            if (firstLockItem.activeSelf)
                firstLockItem.SetActive(false);
        }
    }

    public void SetupInfomation()
    {
        freeItemState_BP = BattlePassSystem.Instance.GetRewardStateInPoint(true, curIndex, out MergeRewardItem rewardItem);
        vipItemState_BP = BattlePassSystem.Instance.GetRewardStateInPoint(false, curIndex, out MergeRewardItem rewardItem1);
        t_CenterIndex.text = (curIndex + 1).ToString();
        t_FreeClaim.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/Level/Button");
        t_VipClaim.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/Level/Button");
       
    }

    private void GetBattlePassReward_Free()
    {
        if (BattlePassSystem.Instance.TryClaimReward(true, curIndex, freeItemTran.position, out MergeRewardItem rewardItem))
        {
            RefreshFreeState(BattlePassRewardState.Claimed);
        }
    }

    public void GetBattlePassReward_VIP()
    {
        if (BattlePassSystem.Instance.TryClaimReward(false, curIndex, vipItemTran.position, out MergeRewardItem rewardItem))
        {
            RefreshVipState(BattlePassRewardState.Claimed);
        }
    }


}
