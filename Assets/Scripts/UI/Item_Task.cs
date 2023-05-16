using ivy.game;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Item_Task : MonoBehaviour
{
    [SerializeField] private Image headIcon;
    [SerializeField] private GameObject completeBg;
    //need item
    [SerializeField] private Transform[] NeedItemBg;
    [SerializeField] private GameObject[] notCompleteItemBgs;
    [SerializeField] private GameObject[] completeItemBgs;
    [SerializeField] private Transform[] itemRoot;
    [SerializeField] private GameObject[] completeTag;
    [SerializeField] private TextMeshProUGUI[] t_ItemNum;
    [SerializeField] private TextMeshProUGUI[] t_CompleteNum;
    [SerializeField] private ShowItemInfo[] showItemInfos;
    //reward
    [SerializeField] private TextMeshProUGUI t_Reward;
    [Header("主线")]
    [SerializeField] private GameObject mainLineRoot;
    [SerializeField] private Transform rewardRoot_M;
    [SerializeField] private TextMeshProUGUI t_RewardExp;
    [SerializeField] private Button btn_Complete_M;
    [SerializeField] private TextMeshProUGUI t_Complete_M;
    [SerializeField] private Button btn_NotComplete_M;
    [SerializeField] private TextMeshProUGUI t_NotComplete_M;
    [Header("支线")]
    [SerializeField] private GameObject branchRoot;
    [SerializeField] private Transform rewardRoot_B;
    [SerializeField] private TextMeshProUGUI t_RewardPoint;
    [SerializeField] private Button btn_Complete_B;
    [SerializeField] private TextMeshProUGUI t_Complete_B;
    [SerializeField] private Button btn_NotComplete_B;
    [SerializeField] private TextMeshProUGUI t_NotComplete_B;

    TaskData taskData;
    private bool isComplete;
    private bool isCompletePart;
    private List<GameObject> saveNeedItem = new List<GameObject>();
    private string taskId;
    private UIPanel_Task uiPanel_Task;
    /// <summary>
    /// 初始化主线任务
    /// </summary>
    /// <param name="taskId"></param>
    public void InitMainLineNeedItems(string taskId, UIPanel_Task panel)
    {
        mainLineRoot.SetActive(true);
        branchRoot.SetActive(false);
        this.taskId = taskId;
        uiPanel_Task = panel;
        if (TaskGoalsDefinition.TaskDefinitionsDict.TryGetValue(taskId, out taskData))
        {
            List<MergeRewardItem> needItemList = taskData.taskDefinition.needItemList;
            for (int i = 0; i < needItemList.Count; i++)
            {
                GameObject go = AssetSystem.Instance.Instantiate(needItemList[i].ShowRewardPrefabName, itemRoot[i]);
                saveNeedItem.Add(go);
            }
            CreatReward();
            CreatHeadIcon();
        }
        btn_Complete_M.onClick.AddListener(() =>
        {
            CompleteBtnClick();
        });
    }
    /// <summary>
    /// 刷新主线任务需要的物品
    /// </summary>
    public void RefreshMainLineNeedItems()
    {
        isComplete = true;
        List<MergeRewardItem> needItemList = taskData.taskDefinition.needItemList;
        for (int i = 0; i < needItemList.Count; i++)
        {
            //数据
            int hasItemNumInMap = MergeLevelManager.Instance.ReturnItemNumByPrefabName(needItemList[i].ShowRewardPrefabName, MergeLevelManager.Instance.CurrentLevelType);
            bool complete = hasItemNumInMap >= needItemList[i].num;
            if (!complete)
                isComplete = false;
            if (complete && !isComplete)
                isCompletePart = true;
            //显示
            notCompleteItemBgs[i].SetActive(true);
            completeItemBgs[i].SetActive(complete);
            itemRoot[i].parent.gameObject.SetActive(true);
            completeTag[i].SetActive(complete);
            if (complete)
            {
                NeedItemBg[i].transform.SetAsFirstSibling();
            }
            t_ItemNum[i].transform.gameObject.SetActive(!complete);
            t_CompleteNum[i].gameObject.SetActive(complete);
            t_CompleteNum[i].text = hasItemNumInMap.ToString() + "/" + needItemList[i].num.ToString();
            t_ItemNum[i].text = hasItemNumInMap.ToString() + "/" + needItemList[i].num.ToString();
            showItemInfos[i].RefreshPrefabName(needItemList[i].ShowRewardPrefabName, null);
        }
        completeBg.SetActive(isComplete);
        btn_Complete_M.gameObject.SetActive(isComplete);
        btn_NotComplete_M.gameObject.SetActive(!isComplete);

        t_Reward.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Orders_Rewards");
        t_Reward.color = isComplete ? new Color32(68, 123, 56, 255) : new Color32(128, 55, 31, 255);
        t_Complete_M.text = t_NotComplete_M.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Orders_Button3");

#if UNITY_EDITOR
        if (DebugSetting.CanUseDebugConfig(out SO_DebugConfig sO_Debug))
        {
            if (sO_Debug.CompleteTaskWithoutItem)
            {
                btn_Complete_M.gameObject.SetActive(true);
                btn_NotComplete_M.gameObject.SetActive(false);
            }
        }
#endif
    }
    /// <summary>
    /// 刷新奖励
    /// </summary>
    private void CreatReward()
    {
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine)
        {
            t_RewardExp.transform.parent.gameObject.SetActive(true);
            t_RewardPoint.transform.parent.gameObject.SetActive(false);
            List<MergeRewardItem> rewardList = taskData.taskDefinition.rewardItemList;
            for (int i = 0; i < rewardList.Count; i++)
            {
                if (rewardList[i].name == "exp")
                {
                    t_RewardExp.text = rewardList[i].num.ToString();
                }
                else
                {
                    GameObject reward = AssetSystem.Instance.Instantiate(rewardList[i].ShowRewardPrefabName, rewardRoot_M);
                    reward.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
                }
            }
        }
        else
        {
            t_RewardExp.transform.parent.gameObject.SetActive(false);
            t_RewardPoint.transform.parent.gameObject.SetActive(true);
            List<MergeRewardItem> rewardList = branchDefinition.rewardItemList;
            t_RewardPoint.transform.parent.gameObject.SetActive(false);
            for (int i = 0; i < rewardList.Count; i++)
            {
                if (rewardList[i].name == "Points")
                {
                    t_RewardPoint.transform.parent.gameObject.SetActive(true);
                    t_RewardPoint.text = rewardList[i].num.ToString();
                }
                else
                {
                    GameObject reward = AssetSystem.Instance.Instantiate(rewardList[i].ShowRewardPrefabName, rewardRoot_B);
                    reward.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
                }
            }
        }
    }
    private void CreatHeadIcon()
    {
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine)
        {
            AssetSystem.Instance.LoadAsset<SpriteAtlas>(Consts.HeadIconAtlas, (atlas) =>
            {
                headIcon.sprite = atlas.GetSprite(taskData.taskDefinition.CharacterPic);
            });
        }
        else
        {
            AssetSystem.Instance.LoadAsset<SpriteAtlas>(Consts.HeadIconAtlas, (atlas) =>
            {
                headIcon.sprite = atlas.GetSprite(branchDefinition.characterPic);
            });
        }
    }
    public bool IsCompleteTask()
    {
        return isComplete;
    }
    public bool IsCompletePart() 
    {
        return isCompletePart;
    }

    private void CompleteBtnClick()
    {
        AdManager.TryPlayInterstitialAD(AdManager.ADTag.complete_orders, RemoteConfigSystem.remoteKey_si_complete_orders);
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine)
        {
            uiPanel_Task.RefreshTaskView(taskId, btn_Complete_M.transform.Find("Star").position);
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.finishTaskAndGetStar);
            AssetSystem.Instance.DestoryGameObject(Consts.Item_Task, gameObject);
        }
        else
        {
            uiPanel_BranchTask.RefreshTaskView(MergeLevelManager.Instance.CurrentLevelType, taskId, t_RewardPoint.transform.position);
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.finishTaskAndGetStar);
            AssetSystem.Instance.DestoryGameObject(Consts.Item_Task, gameObject);
        }
    }



    #region 支线
    private UIPanel_BranchTask uiPanel_BranchTask;
    private BranchDefinition branchDefinition;
    /// <summary>
    /// 初始化支线任务
    /// </summary>
    /// <param name="taskId"></param>
    public void InitBranchNeedItems(MergeLevelType levelType, string taskId, UIPanel_BranchTask panel)
    {
        mainLineRoot.SetActive(false);
        branchRoot.SetActive(true);
        this.taskId = taskId;
        uiPanel_BranchTask = panel;
        if (BranchDefinition.allBranchdefDic.TryGetValue(levelType, out Dictionary<string, BranchDefinition> defDic))
        {
            if (defDic.TryGetValue(taskId, out branchDefinition))
            {
                List<MergeRewardItem> needItemList = branchDefinition.needItemList;
                for (int i = 0; i < needItemList.Count; i++)
                {
                    GameObject go = AssetSystem.Instance.Instantiate(needItemList[i].ShowRewardPrefabName, itemRoot[i]);
                    saveNeedItem.Add(go);
                }
                CreatReward();
                CreatHeadIcon();
            }
            btn_Complete_B.onClick.AddListener(() =>
            {
                CompleteBtnClick();
            });
        }
    }
    /// <summary>
    /// 刷新支线任务需要的物品
    /// </summary>
    public void RefreshBranchNeedItems()
    {
        isComplete = true;
        List<MergeRewardItem> needItemList = branchDefinition.needItemList;
        for (int i = 0; i < needItemList.Count; i++)
        {
            //数据
            int hasItemNumInMap = MergeLevelManager.Instance.ReturnItemNumByPrefabName(needItemList[i].ShowRewardPrefabName, MergeLevelManager.Instance.CurrentLevelType);
            bool complete = hasItemNumInMap >= needItemList[i].num;
            if (!complete)
                isComplete = false;
            if (complete && !isComplete)
                isCompletePart = true;
            //显示
            notCompleteItemBgs[i].SetActive(true);
            completeItemBgs[i].SetActive(complete);
            itemRoot[i].parent.gameObject.SetActive(true);
            completeTag[i].SetActive(complete);
            if (complete)
            {
                NeedItemBg[i].transform.SetAsFirstSibling();
            }
            t_ItemNum[i].transform.gameObject.SetActive(!complete);
            t_ItemNum[i].text = "x" + needItemList[i].num.ToString();
            showItemInfos[i].RefreshPrefabName(needItemList[i].ShowRewardPrefabName, null);
        }
        completeBg.SetActive(isComplete);
        btn_Complete_B.gameObject.SetActive(isComplete);
        btn_NotComplete_B.gameObject.SetActive(!isComplete);

        t_Reward.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Orders_Rewards");
        t_Reward.color = isComplete ? new Color32(68, 123, 56, 255) : new Color32(128, 55, 31, 255);
        t_Complete_B.text = t_NotComplete_B.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Orders_Button3");

#if UNITY_EDITOR
        if (DebugSetting.CanUseDebugConfig(out SO_DebugConfig sO_Debug))
        {
            if (sO_Debug.CompleteTaskWithoutItem)
            {
                btn_Complete_B.gameObject.SetActive(true);
                btn_NotComplete_B.gameObject.SetActive(false);
            }
        }
#endif
    }
    #endregion
}
