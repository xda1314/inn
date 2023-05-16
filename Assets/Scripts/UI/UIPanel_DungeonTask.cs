using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIPanelData_DungeonTask : UIPanelDataBase
{
    public MergeLevelType levelType;
    public UIPanelData_DungeonTask(MergeLevelType levelType) : base(Consts.UIPanel_DungeonTask)
    {
        this.levelType = levelType;
    }
}
public class UIPanel_DungeonTask : UIPanelBase
{
    [SerializeField] private Button Btn_Close;
    [SerializeField] private TextMeshProUGUI t_Title;
    [SerializeField] private TextMeshProUGUI t_Desc;
    [SerializeField] private Transform needItemRoot1;
    [SerializeField] private TextMeshProUGUI t_NeedItemNum1;
    [SerializeField] private Transform needItemRoot2;
    [SerializeField] private TextMeshProUGUI t_NeedItemNum2;

    [SerializeField] private TextMeshProUGUI t_Rewards;
    [SerializeField] private Transform rewardsRoot;

    [SerializeField] private Button btn_Complete;
    [SerializeField] private TextMeshProUGUI t_Complete;
    [SerializeField] private GameObject btn_NotComplete;
    [SerializeField] private TextMeshProUGUI t_NotComplete;

    private MergeLevelType curLevelType;
    MergeRewardItem targetItem = new MergeRewardItem();
    DungeonDefinition definition;

    List<GameObject> saveObjs = new List<GameObject>();
    List<GameObject> rewardItemList = new List<GameObject>();
    public override void OnInitUI()
    {
        base.OnInitUI();
        Btn_Close.onClick.AddListener(CloseBtnClick);
        btn_Complete.onClick.AddListener(CompleteBtnClick);
    }
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        curLevelType = ((UIPanelData_DungeonTask)UIPanelData).levelType;
       
        RefreshUI();
    }
    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
    }
    private void RefreshUI()
    {
        Btn_Close.enabled = true;
        btn_Complete.enabled = true;
        t_Rewards.text = I2.Loc.ScriptLocalization.Get("Obj/Main/Tasks/Text1");
        t_Complete.text = I2.Loc.ScriptLocalization.Get("Obj/Main/Tasks/ButtonText4");
        t_NotComplete.text = I2.Loc.ScriptLocalization.Get("Obj/Main/Tasks/ButtonText3");
        if (MergeLevelManager.Instance.IsDailyActive(curLevelType))
        {
            RefreshDailyUI();
        }
        else if (DungeonDefinition.DungeonDefDic.TryGetValue(curLevelType, out definition))
        {
            RefreshDungeonUI();
        }
        
    }

    private void RefreshDungeonUI() 
    {
        t_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Dungeon/Title");
        if (saveObjs.Count > 0)
        {
            for (int i = saveObjs.Count - 1; i >= 0; i--)
            {
                Destroy(saveObjs[i]);
            }
        }
        for (int i = 0; i < rewardItemList.Count; i++)
        {
            AssetSystem.Instance.DestoryGameObject(Consts.DungeonTaskRewardItem, rewardItemList[i]);
        }
        rewardItemList.Clear();


        if (definition.AmiMergeItems.Count != 1 && definition.Score.Count != 1)
        {
            GameDebug.LogError("dungeon need item count must be one! error count:" + definition.AmiMergeItems.Count);
            return;
        }
        targetItem = curLevelType == MergeLevelType.dungeon2 ? definition.Score[0] : definition.AmiMergeItems[0];
        saveObjs.Add(AssetSystem.Instance.Instantiate(targetItem.ShowRewardPrefabName, needItemRoot1));

        List<MergeRewardItem> rewards = definition.ItemRewardList;
        for (int i = 0; i < rewards.Count; i++)
        {
            GameObject rewardGo = AssetSystem.Instance.Instantiate(Consts.DungeonTaskRewardItem, rewardsRoot);
            Transform ItemBg = rewardGo.transform.Find("ItemRoot");
            if (ItemBg != null)
            {
                AssetSystem.Instance.Instantiate(rewards[i].ShowRewardPrefabName, ItemBg, Vector3.zero, Vector3.zero, Vector3.one * 0.8f);
            }
            Transform itemNum = rewardGo.transform.Find("ItemNum");
            if (itemNum != null && itemNum.TryGetComponent(out TextMeshProUGUI text))
            {
                text.text = "x" + rewards[i].num.ToString();
            }
            rewardItemList.Add(rewardGo);
        }
        t_Desc.gameObject.SetActive(true);
        t_Desc.text = I2.Loc.ScriptLocalization.Get(definition.GetTextDescription(DungeonState.unlock));

        int hasItemNum = curLevelType == MergeLevelType.dungeon2 ?
            DungeonSystem.Instance.GetDungeonScore(curLevelType) :
            MergeLevelManager.Instance.ReturnItemNumByPrefabName(targetItem.ShowRewardPrefabName, curLevelType);
        t_NeedItemNum1.text = hasItemNum.ToString() + "/" + targetItem.num.ToString();
        bool isComplete = hasItemNum >= targetItem.num;
        btn_Complete.gameObject.SetActive(isComplete);
        btn_NotComplete.gameObject.SetActive(!isComplete);
        t_NeedItemNum2.transform.parent.gameObject.SetActive(false);
    }

    private void RefreshDailyUI() 
    {
        t_Title.text = I2.Loc.ScriptLocalization.Get(DailyDefinition.DailyDefDic[curLevelType].textTitle);
        if (saveObjs.Count > 0)
        {
            for (int i = saveObjs.Count - 1; i >= 0; i--)
            {
                Destroy(saveObjs[i]);
            }
        }
        for (int i = 0; i < rewardItemList.Count; i++)
        {
            AssetSystem.Instance.DestoryGameObject(Consts.DungeonTaskRewardItem, rewardItemList[i]);
        }
        rewardItemList.Clear();
        List<MergeRewardItem> items = new List<MergeRewardItem>();
        int hasItemNum1 = 0;
        bool isComplete;
        if (curLevelType == MergeLevelType.daily4)
        {
            items.Add(DailyDefinition.DailyDefDic[curLevelType].Score);
            hasItemNum1 = DailyActiveSystem.Instance.Score;
        }
        else
        {
            items = DailyDefinition.DailyDefDic[curLevelType].AmiMergeItems;
            hasItemNum1 = MergeLevelManager.Instance.ReturnItemNumByPrefabName(items[0].ShowRewardPrefabName, curLevelType);
        }
        saveObjs.Add(AssetSystem.Instance.Instantiate(items[0].ShowRewardPrefabName, needItemRoot1));
        t_NeedItemNum1.text = hasItemNum1.ToString() + "/" + items[0].num.ToString();
        isComplete = hasItemNum1 >= items[0].num;
        int hasItemNum2;
        if (items.Count > 1)
        {
            needItemRoot2.transform.parent.gameObject.SetActive(true);
            hasItemNum2 = MergeLevelManager.Instance.ReturnItemNumByPrefabName(items[1].ShowRewardPrefabName, curLevelType);
            t_NeedItemNum2.text = hasItemNum2.ToString() + "/" + items[1].num.ToString();
            saveObjs.Add(AssetSystem.Instance.Instantiate(items[1].ShowRewardPrefabName, needItemRoot2));
            if (isComplete) 
            {
                isComplete = hasItemNum2 >= items[1].num;
            }
        }
        else
        {
            needItemRoot2.transform.parent.gameObject.SetActive(false);
        }

        List<MergeRewardItem> rewards = DailyActiveSystem.Instance.ItemRewardList;
        for (int i = 0; i < rewards.Count; i++)
        {
            GameObject rewardGo = AssetSystem.Instance.Instantiate(Consts.DungeonTaskRewardItem, rewardsRoot);
            Transform ItemBg = rewardGo.transform.Find("ItemRoot");
            if (ItemBg != null)
            {
                AssetSystem.Instance.Instantiate(rewards[i].ShowRewardPrefabName, ItemBg, Vector3.zero, Vector3.zero, Vector3.one * 0.8f);
            }
            Transform itemNum = rewardGo.transform.Find("ItemNum");
            if (itemNum != null && itemNum.TryGetComponent(out TextMeshProUGUI text))
            {
                text.text = "x" + rewards[i].num.ToString();
            }
            rewardItemList.Add(rewardGo);
        }
        t_Desc.gameObject.SetActive(false);
        btn_Complete.gameObject.SetActive(isComplete);
        btn_NotComplete.gameObject.SetActive(!isComplete);
    }

    private void CompleteBtnClick()
    {
        if (MergeLevelManager.Instance.IsDailyActive(curLevelType))
        {
            //发放奖励
            GameManager.Instance.GiveRewardItem(DailyActiveSystem.Instance.ItemRewardList, "Complete:" + MergeLevelManager.Instance.CurrentLevelType, MergeLevelType.mainLine, false, () =>
            {
                Btn_Close.enabled = false;
                btn_Complete.enabled = false;
                Invoke("DelayHide", 1.5f);
            });
        }
        else 
        {
            //发放奖励
            GameManager.Instance.GiveRewardItem(definition.ItemRewardList, "Complete:" + definition.type.ToString(), MergeLevelType.mainLine, false, () =>
            {
                Btn_Close.enabled = false;
                btn_Complete.enabled = false;
                Invoke("DelayHide", 1.5f);
            });
        }
    }
    private void DelayHide() 
    {
        
        if (MergeController.CurrentController != null)
            MergeController.CurrentController.HideMergeUI();
        if (MergeLevelManager.Instance.IsDailyActive(curLevelType))
        {
            UISystem.Instance.uiMainMenu.pageToIndex = 2;
            DailyActiveSystem.Instance.DailyComplete();
        }
        else
        {
            UISystem.Instance.uiMainMenu.pageToIndex = 3;
            DungeonSystem.Instance.DungeonComplete(curLevelType);
        }
        UISystem.Instance.HideUI(this);
    }
    private void CloseBtnClick()
    {
        UISystem.Instance.HideUI(this);
    }
}
