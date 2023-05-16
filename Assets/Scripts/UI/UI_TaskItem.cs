using DG.Tweening;
using ivy.game;
using Ivy;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UI_TaskItem : MonoBehaviour
{
    [SerializeField] private Transform headIconBg_M;
    [SerializeField] private Button btn_OpenTask;
    [SerializeField] private Button btn_OpenTask1;
    [SerializeField] private GameObject bg_Complete2;
    [SerializeField] private GameObject bg_Complete1;
    [SerializeField] private GameObject NotCompleteBg2;
    [SerializeField] private GameObject NotCompleteBg1;
    [SerializeField] private GameObject[] bg_CompleteItem;
    [SerializeField] private GameObject[] bg_NoCompleteItem;
    [SerializeField] private Transform[] itemRoot;
    [SerializeField] private Button[] btn_ShowItemInfo;
    [SerializeField] private GameObject[] completeTag;
    [SerializeField] private GameObject[] noCompleteTag;
    [SerializeField] public TextMeshProUGUI[] t_ItemNum;
    [SerializeField] public TextMeshProUGUI[] t_CompleteNum;

    [SerializeField] private GameObject commonBg;
    [SerializeField] private GameObject loveDungeonBg;
    [SerializeField] private GameObject loveDungeonInBg;
    [SerializeField] private TextMeshProUGUI t_LoveNum;
    [SerializeField] private Transform loveItemTrans;

    [SerializeField] private GameObject breathRoot;
    [SerializeField] private Button btn_Spine;

    #region 支线积分任务
    [SerializeField] private GameObject PointBranchBg;
    [SerializeField] private GameObject PointBranchCompleteBg;
    [SerializeField] private Transform pointBranchTrans;
    [SerializeField] private TextMeshProUGUI text_point;
    #endregion

    public string taskId { get; private set; }
    private void Awake()
    {
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine)
        {
            btn_OpenTask.onClick.AddListener(() =>
            {
                UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_Task));
            });
            btn_OpenTask1.onClick.AddListener(() =>
            {
                UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_Task));
            });
            btn_Spine.onClick.AddListener(() =>
            {
                UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_Task));
            });
        }
        else if (MergeLevelManager.Instance.IsBranch(MergeLevelManager.Instance.CurrentLevelType)
            || MergeLevelManager.Instance.IsFestivalBranch(MergeLevelManager.Instance.CurrentLevelType))
        {
            if(MergeLevelManager.Instance.CurrentLevelType== MergeLevelType.branch_SpurLine4
                || MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.branch_SpurLine5
                || MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.branch_SpurLine6) 
            {
                btn_OpenTask.onClick.AddListener(() =>
                {
                    UISystem.Instance.ShowUI(new UIPanelData_BranchRedeem(MergeLevelManager.Instance.CurrentLevelType));
                });
                btn_OpenTask1.onClick.AddListener(() =>
                {
                    UISystem.Instance.ShowUI(new UIPanelData_BranchRedeem(MergeLevelManager.Instance.CurrentLevelType));
                });
                btn_Spine.onClick.AddListener(() =>
                {
                    UISystem.Instance.ShowUI(new UIPanelData_BranchRedeem(MergeLevelManager.Instance.CurrentLevelType));
                });
            }
            else
            {
                btn_OpenTask.onClick.AddListener(() =>
                {
                    UISystem.Instance.ShowUI(new UIPanelData_BranchTask(MergeLevelManager.Instance.CurrentLevelType));
                });
                btn_OpenTask1.onClick.AddListener(() =>
                {
                    UISystem.Instance.ShowUI(new UIPanelData_BranchTask(MergeLevelManager.Instance.CurrentLevelType));
                });
                btn_Spine.onClick.AddListener(() =>
                {
                    UISystem.Instance.ShowUI(new UIPanelData_BranchTask(MergeLevelManager.Instance.CurrentLevelType));
                });
            }   
        }
        else
        {
            btn_OpenTask.onClick.AddListener(() =>
            {
                UISystem.Instance.ShowUI(new UIPanelData_DungeonTask(MergeLevelManager.Instance.CurrentLevelType));
            });
            btn_OpenTask1.onClick.AddListener(() =>
            {
                UISystem.Instance.ShowUI(new UIPanelData_DungeonTask(MergeLevelManager.Instance.CurrentLevelType));
            });
            btn_Spine.onClick.AddListener(() =>
            {
                UISystem.Instance.ShowUI(new UIPanelData_DungeonTask(MergeLevelManager.Instance.CurrentLevelType));
            });
        }     
    }
    #region 主线
    public TaskData taskData;
    bool isCompletebeforeRefresh = false;
    bool isComplete = false;
    bool isCompletePart = false;
    private List<GameObject> saveObjs = new List<GameObject>() { null, null };

    public void InitMainLineItem(string taskId) 
    {
        this.taskId = taskId;
        SetItemBg(ItemBgType.common);
        if (TaskGoalsDefinition.TaskDefinitionsDict.TryGetValue(taskId, out taskData))
        {
            List<MergeRewardItem> needItemList = taskData.taskDefinition.needItemList;
            for (int i = 0; i < needItemList.Count; i++)
            {
                if (saveObjs[i] == null)
                {
                    GameObject go = AssetSystem.Instance.Instantiate(needItemList[i].ShowRewardPrefabName, itemRoot[i], Vector3.zero, Vector3.zero, Vector3.one * 0.7f);
                    saveObjs[i] = go;
                    if (go == null) 
                    {
                        LogUtils.LogErrorToSDK("预制体找不到" + needItemList[i].ShowRewardPrefabName);
                        needItemList[i] = new MergeRewardItem
                        {
                            name = "Candy_9",
                            num = 1
                        };
                        go = AssetSystem.Instance.Instantiate("Candy_9", itemRoot[i], Vector3.zero, Vector3.zero, Vector3.one * 0.7f);
                    }
                    go.GetComponent<Image>().raycastTarget = false;
                    go.transform.SetSiblingIndex(2);
                }
            }
            AssetSystem.Instance.Instantiate("Spine_" + taskData.taskDefinition.PictureName, headIconBg_M);
        }
        else
        {
            GameDebug.LogError("task data error!");
            return;
        }
    }
    public void RefreshMainLineItem()
    {
        isCompletebeforeRefresh = isComplete;
        isComplete = true;
        isCompletePart = false;
        bool hasItemComplete = false;//该任务有完成的道具
        List<MergeRewardItem> needItemList = taskData.taskDefinition.needItemList;

        for (int i = 0; i < needItemList.Count; i++)
        {
            //数据
            int hasItemNumInMap = MergeLevelManager.Instance.ReturnItemNumByPrefabName(needItemList[i].ShowRewardPrefabName, MergeLevelManager.Instance.CurrentLevelType);
            bool complete = hasItemNumInMap >= needItemList[i].num;
            bg_CompleteItem[i].SetActive(complete);
            if (!complete)
                isComplete = false;
            else
                hasItemComplete = true;          
            //显示
            noCompleteTag[i].SetActive(!complete);
            completeTag[i].SetActive(complete);
            if (complete) 
            {
                itemRoot[i].transform.SetAsFirstSibling();
            }
            t_ItemNum[i].gameObject.SetActive(!complete);
            t_CompleteNum[i].gameObject.SetActive(complete);
            t_CompleteNum[i].text= hasItemNumInMap.ToString() + "/" + needItemList[i].num.ToString();
            t_ItemNum[i].text = hasItemNumInMap.ToString() + "/" + needItemList[i].num.ToString();
            btn_ShowItemInfo[i].gameObject.SetActive(true);
            btn_ShowItemInfo[i].GetComponent<ShowItemInfo>().RefreshPrefabName(needItemList[i].ShowRewardPrefabName, null);
            if (IvyCore.UI_TutorManager.Instance.IsTutoring()&&complete)
                btn_ShowItemInfo[i].GetComponent<Image>().raycastTarget = false;
            else
                btn_ShowItemInfo[i].GetComponent<Image>().raycastTarget = true;
        }
        if (hasItemComplete && !isComplete)
            isCompletePart = true;//完成某个道具且没有完成全部道具，判定为完成部分
        RefreshItemUI(needItemList.Count);
        TryBreath(isComplete);
    }
    public bool IsCompleteTask()
    {
        return isComplete;
    }
    public bool IsCompleteTaskBeforeRefresh()
    {
        return isCompletebeforeRefresh;
    }
    public bool IsCompletePart() 
    {
        return isCompletePart;
    }
    public string ReturndDalogueText() 
    {
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine)
        {
            return taskData.taskDefinition.Text;
        }
        else if (MergeLevelManager.Instance.IsBranch(MergeLevelManager.Instance.CurrentLevelType) || MergeLevelManager.Instance.IsFestivalBranch(MergeLevelManager.Instance.CurrentLevelType))
        {
            return branchDefinition.Text;
        }
        else 
        {
            GameDebug.LogError("未配对话");
            return string.Empty;
        }
    }
    public List<Sprite> ReturnNeedItemIcons()
    {
        List<Sprite> spriteList = new List<Sprite>();
        for (int i = 0; i < saveObjs.Count; i++)
        {
            if (saveObjs[i] && saveObjs[i].TryGetComponent(out Image image))
            {
                spriteList.Add(image.sprite);
            }
        }
        return spriteList;
    }
    public List<MergeRewardItem> ReturnRewardItem() 
    {
        List<MergeRewardItem> rewardList = new List<MergeRewardItem>();
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine)
        {
            rewardList = taskData.taskDefinition.rewardItemList;
        }
        else if (MergeLevelManager.Instance.IsBranch(MergeLevelManager.Instance.CurrentLevelType) || MergeLevelManager.Instance.IsFestivalBranch(MergeLevelManager.Instance.CurrentLevelType))
        {
            rewardList = branchDefinition.rewardItemList;
        }
        return rewardList;
    }
    public Sprite ReturnHeadIcon() 
    {
        Sprite sprite = null;
        string spriteName = string.Empty;
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine)
        {
            spriteName = taskData.taskDefinition.CharacterPic + "_Round";
        }
        else if (MergeLevelManager.Instance.IsBranch(MergeLevelManager.Instance.CurrentLevelType) || MergeLevelManager.Instance.IsFestivalBranch(MergeLevelManager.Instance.CurrentLevelType))
        {
            spriteName = branchDefinition.characterPic + "_Round";
        }
        AssetSystem.Instance.LoadAsset<SpriteAtlas>(Consts.HeadIconAtlas, (atlas) => 
        {
            sprite = atlas.GetSprite(spriteName);
        });
        return sprite;
    }
    #endregion

    #region 支线
    private BranchDefinition branchDefinition = new BranchDefinition();

    public void InitPiontBranchItem()
    {
        SetItemBg(ItemBgType.pointBranch);
        foreach (var item in completeTag)
        {
            item.gameObject.SetActive(false);
        }
        foreach (var item in noCompleteTag)
        {
            item.gameObject.SetActive(false);
        }
        AssetSystem.Instance.Instantiate("Spine_Wendy", headIconBg_M);
    }

    public void RefreshPiontBranchItem()
    {
        int score = BranchSystem.Instance.branchPoint;
        if (BranchSystem.Instance.ClaimedAllTask()) 
        {
            //支线任务已经完成 无需处理
            TryBreath(false);
        }
        else 
        {
            BranchRewardDefinition branchReward = BranchSystem.Instance.GetCurrentBranchDef();
            bool isComplete = score >= branchReward.goalPoint;
            text_point.text = score.ToString() + "/" + branchReward.goalPoint.ToString();
            PointBranchCompleteBg.SetActive(isComplete);
            TryBreath(isComplete);
        }
        bg_Complete2.SetActive(false);
        bg_Complete1.SetActive(false);
        bg_Complete2.SetActive(false);
        NotCompleteBg2.SetActive(false);
        NotCompleteBg1.SetActive(false);
        itemRoot[1].gameObject.SetActive(false);
    }


    public void InitBranchItem(string taskId)
    {
        this.taskId = taskId;
        SetItemBg(ItemBgType.common);
        if (BranchDefinition.allBranchdefDic.TryGetValue(MergeLevelManager.Instance.CurrentLevelType, out Dictionary<string, BranchDefinition> defDic)) 
        {
            if (defDic.TryGetValue(taskId, out branchDefinition))
            {
                List<MergeRewardItem> needItemList = branchDefinition.needItemList;
                for (int i = 0; i < needItemList.Count; i++)
                {
                    if (saveObjs[i] == null)
                    {
                        GameObject go = AssetSystem.Instance.Instantiate(needItemList[i].ShowRewardPrefabName, itemRoot[i], Vector3.zero, Vector3.zero, Vector3.one * 0.7f);
                        if (go == null)
                        {
                            LogUtils.LogErrorToSDK("支线预制体找不到" + needItemList[i].ShowRewardPrefabName);
                            needItemList[i] = new MergeRewardItem
                            {
                                name = "Stone_3",
                                num = 1
                            };
                            go = AssetSystem.Instance.Instantiate("Stone_3", itemRoot[i], Vector3.zero, Vector3.zero, Vector3.one * 0.7f);
                        }
                        saveObjs[i] = go;
                        go.GetComponent<Image>().raycastTarget = false;
                        go.transform.SetSiblingIndex(2);
                    }
                }
                AssetSystem.Instance.Instantiate("Spine_" + branchDefinition.pictureName, headIconBg_M);
                
            }
            else
            {
                GameDebug.LogError("task data error!");
                return;
            }
        }     
    }
    public void RefreshBranchItem()
    {
        isCompletebeforeRefresh = isComplete;
        isComplete = true;
        isCompletePart = false;
        bool hasItemComplete = false;//该任务有完成的道具
        List<MergeRewardItem> needItemList = branchDefinition.needItemList;
        for (int i = 0; i < needItemList.Count; i++)
        {
            //数据
            int hasItemNumInMap = MergeLevelManager.Instance.ReturnItemNumByPrefabName(needItemList[i].ShowRewardPrefabName, MergeLevelManager.Instance.CurrentLevelType);
            bool complete = hasItemNumInMap >= needItemList[i].num;
            bg_CompleteItem[i].SetActive(complete);
            if (!complete)
                isComplete = false;
            else
                hasItemComplete = true;
            //显示
            completeTag[i].SetActive(complete);
            if (complete)
            {
                itemRoot[i].transform.SetAsFirstSibling();
            }
            noCompleteTag[i].SetActive(!complete);
            t_ItemNum[i].gameObject.SetActive(!complete);
            t_CompleteNum[i].gameObject.SetActive(complete);
            t_CompleteNum[i].text = hasItemNumInMap.ToString() + "/" + needItemList[i].num.ToString();
            t_ItemNum[i].text = hasItemNumInMap.ToString() + "/" + needItemList[i].num.ToString();
            btn_ShowItemInfo[i].gameObject.SetActive(true);
            btn_ShowItemInfo[i].GetComponent<ShowItemInfo>().RefreshPrefabName(needItemList[i].ShowRewardPrefabName, null);
        }
        if (hasItemComplete && !isComplete)
            isCompletePart = true;
        bg_Complete2.SetActive(isComplete);
        TryBreath(isComplete);
        RefreshItemUI(needItemList.Count);
    }
    #endregion

    private void RefreshItemUI(int count) 
    {
        if (count == 1)
        {
            bg_Complete1.SetActive(isComplete);
            bg_Complete2.SetActive(false);
            NotCompleteBg2.SetActive(false);
            NotCompleteBg1.SetActive(true);
            itemRoot[1].gameObject.SetActive(false);
        }
        else
        {
            bg_Complete1.SetActive(false);
            bg_Complete2.SetActive(isComplete);
            NotCompleteBg2.SetActive(true);
            NotCompleteBg1.SetActive(false);
            itemRoot[1].gameObject.SetActive(true);
        }
    }

    #region 副本
    bool isLastStepConplete = false;//上次刷新时是否完成任务
    //爱心副本
    public void InitLoveDungeonTaskItem(MergeRewardItem needItem)
    {
        SetItemBg(ItemBgType.loveDungeon);
        foreach (var item in completeTag)
        {
            item.gameObject.SetActive(false);
        }
        foreach (var item in noCompleteTag)
        {
            item.gameObject.SetActive(false);
        }
        AssetSystem.Instance.Instantiate("Spine_Wendy", headIconBg_M);
    }
    public void RefreshLoveDungeonItem()
    {
        bool isComplete = false;
        int score = DungeonSystem.Instance.GetDungeonScore(MergeLevelManager.Instance.CurrentLevelType);
        if (DungeonDefinition.DungeonDefDic.TryGetValue(MergeLevelManager.Instance.CurrentLevelType, out DungeonDefinition loveDefinition)) 
        {
            isComplete = score >= loveDefinition.Score[0].num;
        }
        t_LoveNum.text = score.ToString();
        if (isComplete && !isLastStepConplete) 
        {
            UISystem.Instance.ShowUI(new UIPanelData_DungeonTask(MergeLevelManager.Instance.CurrentLevelType));
            isLastStepConplete = true;
        }
        loveDungeonInBg.SetActive(isComplete);
        bg_Complete2.SetActive(isComplete);       
        TryBreath(isComplete);
        bg_Complete1.SetActive(isComplete);
        bg_Complete2.SetActive(false);
        NotCompleteBg2.SetActive(false);
        NotCompleteBg1.SetActive(false);
        itemRoot[1].gameObject.SetActive(false);
    }
    public Transform ReturnLoveTrans() 
    {
        return loveItemTrans;
    }
    public Transform ReturnPointTrans()
    {
        return pointBranchTrans;
    }

    //普通副本
    MergeRewardItem needItem = new MergeRewardItem();
    public void InitCommonDungeonTaskItem(MergeRewardItem needItem) 
    {
        SetItemBg(ItemBgType.common);
        this.needItem = needItem;
        GameObject go = AssetSystem.Instance.Instantiate(needItem.ShowRewardPrefabName, itemRoot[0], Vector3.zero, Vector3.zero, Vector3.one * 0.7f);
        if (go == null)
        {
            LogUtils.LogErrorToSDK("普通副本预制体找不到" + needItem.ShowRewardPrefabName);
        }
        go.transform.SetSiblingIndex(2);
        go.GetComponent<Image>().raycastTarget = false;     
        AssetSystem.Instance.Instantiate("Spine_Man5", headIconBg_M);
        
    }
    public void RefreshCommonDungeonItem()
    {     
        int hasItemNumInMap = MergeLevelManager.Instance.ReturnItemNumByPrefabName(needItem.ShowRewardPrefabName, MergeLevelManager.Instance.CurrentLevelType);
        bool isComplete = hasItemNumInMap >= needItem.num;
        if (isComplete)
        {
            if (!completeTag[0].activeSelf)
                completeTag[0].SetActive(true);
            if (!noCompleteTag[0].activeSelf)
                noCompleteTag[0].SetActive(false);
            if (t_ItemNum[0].gameObject.activeSelf)
                t_ItemNum[0].gameObject.SetActive(false);
            t_CompleteNum[0].text = hasItemNumInMap.ToString() + "/" + needItem.num.ToString();
            t_CompleteNum[0].gameObject.SetActive(true);
            if (isLastStepConplete == false) 
            {
                UISystem.Instance.ShowUI(new UIPanelData_DungeonTask(MergeLevelManager.Instance.CurrentLevelType));
                isLastStepConplete = true;
            }
        }
        else 
        {
            if (completeTag[0].activeSelf)
                completeTag[0].SetActive(false);
            if (!noCompleteTag[0].activeSelf)
                noCompleteTag[0].SetActive(true);
            if (!t_ItemNum[0].gameObject.activeSelf)
                t_ItemNum[0].gameObject.SetActive(true);
            t_ItemNum[0].text = hasItemNumInMap.ToString() + "/" + needItem.num.ToString();
            isLastStepConplete = false;
        }
        bg_Complete2.SetActive(isComplete);
        TryBreath(isComplete);
        RefreshItemUI(1);
    }
    #endregion

    #region 简单任务
    public void InitLoveDailyTaskItem(MergeRewardItem aim)
    {
        SetItemBg(ItemBgType.loveDaily);
        foreach (var item in completeTag)
        {
            item.gameObject.SetActive(false);
        }
        foreach (var item in noCompleteTag)
        {
            item.gameObject.SetActive(false);
        }
        AssetSystem.Instance.Instantiate("Spine_Wendy", headIconBg_M);

    }

    public void RefreshLoveDailyItem()
    {
        bool isComplete = false;
        int score = DailyActiveSystem.Instance.Score;
        if (DailyDefinition.DailyDefDic.TryGetValue(MergeLevelManager.Instance.CurrentLevelType, out DailyDefinition loveDefinition))
        {
            isComplete = score >= loveDefinition.Score.num;
        }
        t_LoveNum.text = score.ToString();
        if (isComplete && !isLastStepConplete)
        {
            UISystem.Instance.ShowUI(new UIPanelData_DungeonTask(MergeLevelManager.Instance.CurrentLevelType));
            isLastStepConplete = true;
        }
        loveDungeonInBg.SetActive(isComplete);
        bg_Complete2.SetActive(false);
        TryBreath(isComplete);
        bg_Complete1.SetActive(false);
        bg_Complete2.SetActive(false);
        NotCompleteBg2.SetActive(false);
        NotCompleteBg1.SetActive(false);
        itemRoot[1].gameObject.SetActive(false);
    }

    private List<MergeRewardItem> dailyAims = new List<MergeRewardItem>();
    public void InitNormalDailyTaskItem(List<MergeRewardItem> aims)
    {
        dailyAims = aims;
        SetItemBg(ItemBgType.common);
        for (int i = 0; i < aims.Count; i++)
        {
            if (saveObjs[i] == null)
            {
                GameObject go = AssetSystem.Instance.Instantiate(aims[i].ShowRewardPrefabName, itemRoot[i], Vector3.zero, Vector3.zero, Vector3.one * 0.7f);
                if (go == null)
                {
                    LogUtils.LogErrorToSDK("支线预制体找不到" + aims[i].ShowRewardPrefabName);
                }
                saveObjs[i] = go;
                go.GetComponent<Image>().raycastTarget = false;
                go.transform.SetSiblingIndex(2);
            }
        }
        AssetSystem.Instance.Instantiate("Spine_" + DailyDefinition.DailyDefDic[MergeLevelManager.Instance.CurrentLevelType].pictureName, headIconBg_M);
    }

    public void RefreshCommonDailyItem()
    {
        isCompletebeforeRefresh = isComplete;
        isComplete = true;
        isCompletePart = false;
        bool hasItemComplete = false;//该任务有完成的道具
        for (int i = 0; i < dailyAims.Count; i++)
        {
            //数据
            int hasItemNumInMap = MergeLevelManager.Instance.ReturnItemNumByPrefabName(dailyAims[i].ShowRewardPrefabName, MergeLevelManager.Instance.CurrentLevelType);
            bool complete = hasItemNumInMap >= dailyAims[i].num;
            bg_CompleteItem[i].SetActive(complete);
            if (!complete)
                isComplete = false;
            else
                hasItemComplete = true;
            //显示
            completeTag[i].SetActive(complete);
            if (complete)
            {
                itemRoot[i].transform.SetAsFirstSibling();
            }
            noCompleteTag[i].SetActive(!complete);
            t_ItemNum[i].gameObject.SetActive(!complete);
            t_CompleteNum[i].gameObject.SetActive(complete);
            t_CompleteNum[i].text = hasItemNumInMap.ToString() + "/" + dailyAims[i].num.ToString();
            t_ItemNum[i].text = hasItemNumInMap.ToString() + "/" + dailyAims[i].num.ToString();
            btn_ShowItemInfo[i].gameObject.SetActive(true);
            btn_ShowItemInfo[i].GetComponent<ShowItemInfo>().RefreshPrefabName(dailyAims[i].ShowRewardPrefabName, null);
        }
        btn_OpenTask.gameObject.SetActive(dailyAims.Count>1);
        if (hasItemComplete && !isComplete)
            isCompletePart = true;
        bg_Complete2.SetActive(isComplete);
        TryBreath(isComplete);
        RefreshItemUI(dailyAims.Count);

    }

    #endregion


    private void SetItemBg(ItemBgType type) 
    {
        switch (type)
        {
            case ItemBgType.common:
                commonBg.SetActive(true);
                loveDungeonBg.SetActive(false);
                PointBranchBg.SetActive(false);
                break;
            case ItemBgType.loveDungeon:
            case ItemBgType.loveDaily:
                bg_Complete1.SetActive(false);
                bg_Complete2.SetActive(false);
                NotCompleteBg2.SetActive(false);
                NotCompleteBg1.SetActive(false);
                commonBg.SetActive(false);
                loveDungeonBg.SetActive(true);
                PointBranchBg.SetActive(false);
                foreach (var item in bg_NoCompleteItem)
                    item.SetActive(false);
                break;
            case ItemBgType.pointBranch:
                bg_Complete1.SetActive(false);
                bg_Complete2.SetActive(false);
                NotCompleteBg2.SetActive(false);
                NotCompleteBg1.SetActive(false);
                commonBg.SetActive(false);
                loveDungeonBg.SetActive(false);
                PointBranchBg.SetActive(true);
                foreach (var item in bg_NoCompleteItem)
                    item.SetActive(false);
                break;
            default:
                break;
        }
    }
    private enum ItemBgType 
    {
        common,
        loveDungeon,
        loveDaily,
        pointBranch,
    }
    Sequence anim;
    private void TryBreath(bool start)
    {
        if (start)
        {
            if (anim == null)
            {
                anim = DOTween.Sequence();
                anim.Append(breathRoot.transform.DOScale(Vector3.one * 0.94f, 0.1f).SetEase(Ease.InOutSine));
                anim.Insert(0.1f, breathRoot.transform.DOScale(Vector3.one * 1.1f, 0.3f).SetEase(Ease.InOutSine));
                anim.Insert(0.4f, breathRoot.transform.DOScale(Vector3.one * 0.98f, 0.2f).SetEase(Ease.InOutSine));
                anim.Insert(0.6f, breathRoot.transform.DOScale(Vector3.one * 1f, 0.1f).SetEase(Ease.InOutSine));

                anim.SetDelay(1f);
                anim.SetLoops(-1);
                anim.Play();
            }
        }
        else 
        {
            if (anim != null)
            {
                anim.Kill();
                anim = null;
                breathRoot.transform.localScale = Vector3.one;
            }
        }
       
    }
}
