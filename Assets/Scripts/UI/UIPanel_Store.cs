using DG.Tweening;
using IvyCore;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 仓库界面
/// </summary>
public class UIPanel_Store : UIPanelBase
{
    [SerializeField] private Button Btn_Close;
    [SerializeField] private Button btn_ShowTutorial;
    [SerializeField] private TextMeshProUGUI Lbl_Title;
    [SerializeField] private TextMeshProUGUI LblDiscribe;
    [SerializeField] private GridLayoutGroup ItemBgGrid;
    [SerializeField] private Transform ItemGrid;
    //底部按钮
    [SerializeField] private Button btn_BuyGridByGem;
    [SerializeField] private TextMeshProUGUI t_GemCost;
    [SerializeField] private TextMeshProUGUI t_UnlockByGem;

    [SerializeField] private Transform moveItemRoot;

    List<ItemBg_Storage> gridBgList = new List<ItemBg_Storage>();
    List<ItemBg_Storage> bpGridBgList = new List<ItemBg_Storage>();
    ItemBg_Storage buyGrid = null;
    private int costGem;

    public override void OnInitUI()
    {
        base.OnInitUI();
        Btn_Close.onClick.AddListener(() => UISystem.Instance.HideUI(this));
        btn_BuyGridByGem.onClick.AddListener(BuyGridByGemClick);
        btn_ShowTutorial.onClick.AddListener(() =>
        {
            UISystem.Instance.ShowUI(new UIPanelData_Tutorial(TutorialType.store));
        });
        InitGridBg(true);
    }
    public override IEnumerator OnShowUI()
    {
        AchievementManager.Instance.UpdateAchievement(AchievementType.warehouse, StoreManager.Instance.curGridNumWithoutBP);

        Lbl_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/Backpack/Title");
        LblDiscribe.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/Backpack/Text1");
        t_UnlockByGem.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/Backpack/Text3");
        InitItem();
        RefreshGemBtnActive();
        InitGridBg(false);
        yield return base.OnShowUI();
        yield return new WaitForSeconds(0.3f);
        if (UI_TutorManager.Instance.Tutorial_ReadyToMergeBackpackOut)
        {
            UI_TutorManager.Instance.Tutorial_ReadyToMergeBackpackOut = false;
        }
    }
    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();

        for (int i = 0; i < storeItemList.Count; i++)
        {
            Destroy(storeItemList[i].gameObject);
        }
        storeItemList.Clear();
        localPosList.Clear();
    }
    /// <summary>
    /// 初始化格子背景位置
    /// </summary>
    private void InitGridBg(bool isInit)
    {
        for (int i = 0; i < gridBgList.Count; i++)
        {
            //防止上次新增格子dotween动画没播放导致本次打开界面格子显示异常
            gridBgList[i].transform.localScale = Vector3.one * 0.9f;
            if (!gridBgList[i].gameObject.activeSelf)
                gridBgList[i].gameObject.SetActive(true);
        }

        List<GameObject> tweenGoList = new List<GameObject>();
        for (int i = gridBgList.Count; i < StoreManager.Instance.curGridNumWithoutBP; i++)
        {            
            //当前解锁的免费格子
            GameObject go = AssetSystem.Instance.Instantiate(Consts.UI_StoreItemBG, ItemBgGrid.transform, Vector3.zero, Vector3.zero, Vector3.one * 0.9f);
            if (!isInit) 
            {
                go.transform.SetSiblingIndex(gridBgList.Count);
                go.transform.localScale = Vector3.zero;
                go.SetActive(false);
                tweenGoList.Add(go);
            }
            ItemBg_Storage itemBg = go.GetComponent<ItemBg_Storage>();         
            if (itemBg != null)
            {
                gridBgList.Add(itemBg);
                itemBg.SetParent(this);
                itemBg.RefreshGridBg(false);
            }
        }
        StartCoroutine(TweenScaleForNewGrid(tweenGoList));
        if (bpGridBgList.Count == 0 && BattlePassSystem.Instance.IsPay) 
        {
            for (int i = StoreManager.Instance.curGridNumWithoutBP; i < StoreManager.Instance.curGridNumWithoutBP + StoreManager.Instance.buyBattlePassBouns; i++)
            {
                //买battlepass送的两个格子
                GameObject go = AssetSystem.Instance.Instantiate(Consts.UI_StoreItemBG, ItemBgGrid.transform, Vector3.zero, Vector3.zero, Vector3.one * 0.9f);

                ItemBg_Storage itemBg = go.GetComponent<ItemBg_Storage>();
                if (itemBg != null)
                {
                    bpGridBgList.Add(itemBg);
                    itemBg.SetParent(this);
                    itemBg.RefreshGridBg(true);
                }
            }
        }
        if (StoreManager.Instance.curGridNumWithoutBP < StorageDefinition.MaxGridNum)
        {
            //购买按钮的格子位置
            if (buyGrid == null)
            {
                GameObject buyGridGo = AssetSystem.Instance.Instantiate(Consts.UI_StoreItemBG, ItemBgGrid.transform, Vector3.zero, Vector3.zero, Vector3.one * 0.9f);
                buyGrid = buyGridGo.GetComponent<ItemBg_Storage>();
            }
            if (buyGrid != null)
            {
                buyGrid.SetParent(this);
                buyGrid.RefreshBuyGrid();
            }
        }
        else 
        {
            if (buyGrid != null) 
            {
                buyGrid.gameObject.SetActive(false);
            }
        }
        
        ItemGrid.transform.SetAsLastSibling();
    }
    private IEnumerator TweenScaleForNewGrid(List<GameObject> list) 
    {
        yield return null;
        for (int i = 0; i < list.Count; i++)
        {
            list[i].SetActive(true);
            DOTween.Sequence().Append(list[i].transform.DOScale(Vector3.one * 0.9f, 0.15f).SetEase(Ease.Linear));
            yield return new WaitForSeconds(0.15f);
        }
        list.Clear();
    }

    [HideInInspector] public List<ItemBg_Storage> storeBgList = new List<ItemBg_Storage>();
    [HideInInspector] public List<Item_Storage> storeItemList = new List<Item_Storage>();
    private List<Vector3> localPosList = new List<Vector3>();
    /// <summary>
    /// 打开界面初始化格子上的物品
    /// </summary>
    private void InitItem()
    {        
        List<MergeItemData> itemDataList = MergeLevelManager.Instance.CurrentMapData.storePackList;
        for (int i = 0; i < itemDataList.Count; i++)
        {
            GameObject itemObj = AssetSystem.Instance.Instantiate(Consts.Item_Storage, ItemGrid.transform);
            Item_Storage item = itemObj.GetComponent<Item_Storage>();
            string prefabName = itemDataList[i].Definition.PrefabName;
            AssetSystem.Instance.Instantiate(prefabName, item.transform);
            item.RefreshItem(this, itemDataList[i], item);
            item.SetIndex(i);
            storeItemList.Add(item);

            CheckTaskIcon(itemDataList[i], item.transform);
            CheckMaxLevelIcon(itemDataList[i], item.transform);
        }
        InitItemPos();
    }

    private void CheckTaskIcon(MergeItemData itemData, Transform parent)
    {
        if (itemData != null && !itemData.IsInBox && !itemData.IsInBubble && !itemData.IsLocked)
        {
            bool showTaskCheck = false;//是否显示主线任务icon
            List<string> taskIdList = TaskGoalsManager.Instance.leftTaskIdList;
            for (int i = 0; i < taskIdList.Count; i++)
            {
                if (TaskGoalsDefinition.TaskDefinitionsDict.TryGetValue(taskIdList[i], out TaskData taskData))
                {
                    for (int j = 0; j < taskData.taskDefinition.needItemList.Count; j++)
                    {
                        if (itemData.PrefabName == taskData.taskDefinition.needItemList[j].ShowRewardPrefabName)
                        {
                            showTaskCheck = true;
                            break;
                        }
                    }
                }
                if (showTaskCheck)
                    break;
            }

            if (showTaskCheck)//优先显示主线任务标志
                AssetSystem.Instance.Instantiate(Consts.ItemState_Task, parent, new Vector3(40, -40, 0), Vector3.zero, Vector3.one);
            else
            {
                foreach (var item in DailyTaskSystem.Instance.dailyTaskLocationData.Values)
                {
                    if (!item.hasClaim
                        && DailyTaskDefinition.dailyTaskDefinitionDic.TryGetValue(item.dailyTaskId, out DailyTaskDefinition dailyTaskDefinition)
                        && dailyTaskDefinition.taskType == DailyTaskType.Collect)
                    {
                        if (itemData.PrefabName == dailyTaskDefinition.targetPrefab)
                        {//判断显示日常任务icon
                            AssetSystem.Instance.Instantiate(Consts.ItemState_DailyTask, parent, new Vector3(40, -40, 0), Vector3.zero, Vector3.one);
                            break;
                        }
                    }
                }
            }
        }
    }

    private void CheckMaxLevelIcon(MergeItemData itemData, Transform parent)
    {
        if (itemData != null && !itemData.IsInBox && !itemData.IsInBubble && !itemData.IsLocked)
        {
            if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(itemData.PrefabName, out MergeItemDefinition itemDefinition))
            {
                if (itemDefinition.IsMaxLevel)
                    AssetSystem.Instance.Instantiate(Consts.ItemState_MaxLevel, parent, new Vector3(-40, -40, 0), Vector3.zero, Vector3.one);
            }
        }
    }

    /// <summary>
    /// 点击背包里面的物品返回场景
    /// </summary>
    public bool TrySetItemToMap(MergeItemData itemData)
    {
        if (itemData == null)
            return false;

        if (MergeController.CurrentController != null && MergeController.CurrentController.TryGetNearEmptyGrid(Vector2Int.one, out Vector2Int grid))
        {
            MergeController.CurrentController.StorePackUIReturnMap(grid, itemData);
            return true;
        }
        return false;
    }
    /// <summary>
    /// 解锁新格子刷新
    /// </summary>
    public void UnlockNewGrid()
    {
        StoreManager.Instance.UnlockStoreGridByCoin();
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.unlockStorePackOne);
        InitGridBg(false);
        RefreshGemBtnActive();
    }

    private void RefreshGemBtnActive() 
    {
        bool active = StorageDefinition.MaxGridNum - StoreManager.Instance.curGridNumWithoutBP > StoreManager.Instance.addGridNumByGemOnce;
        btn_BuyGridByGem.transform.parent.gameObject.SetActive(active);
        if (StoreManager.Instance.buyGridByGemIndex == 0)
            costGem = 50;
        else if (StoreManager.Instance.buyGridByGemIndex == 1)
            costGem = 150;
        else if (StoreManager.Instance.buyGridByGemIndex >= 2)
            costGem = 200;
        t_GemCost.text = costGem.ToString();
    }
    private void BuyGridByGemClick() 
    {
        if (Currencies.Spend(CurrencyID.Gems, costGem, "unlockStore")) 
        {
            StoreManager.Instance.UnlockStoreGridByGem();
            InitGridBg(false);
            RefreshGemBtnActive();
        }     
    }

    public void SetParent(Transform target, bool toMoveTrans) 
    {
        if (toMoveTrans)
        {
            target.SetParent(moveItemRoot);
        }
        else 
        {
            target.SetParent(ItemGrid.transform);
        }
    }
    public void ExchangeTwoItem(Item_Storage fromItem, Item_Storage toItem) 
    {    
        List<MergeItemData> storeDataList = MergeLevelManager.Instance.CurrentMapData.storePackList;
        MergeItemData fromData = storeDataList[fromItem.curIndex];
        MergeItemData toData = storeDataList[toItem.curIndex];
        storeDataList[fromItem.curIndex] = toData;
        storeDataList[toItem.curIndex] = fromData;
        MergeController.CurrentController.RefreshBottomStore();      

        int formIndex = fromItem.curIndex;
        int toIndex = toItem.curIndex;
        TweenExchange(fromItem, formIndex, toItem, toIndex);

        this.storeItemList[formIndex] = toItem;
        this.storeItemList[toIndex] = fromItem;
        fromItem.transform.SetSiblingIndex(toIndex);
        fromItem.SetIndex(toIndex);
        toItem.transform.SetSiblingIndex(formIndex);
        toItem.SetIndex(formIndex);
    }
    public void MoveToEnd(Item_Storage moveItem) 
    {        
        List<MergeItemData> storeDataList = MergeLevelManager.Instance.CurrentMapData.storePackList;
        MergeItemData moveData = storeDataList[moveItem.curIndex];
        storeDataList.Remove(moveData);
        storeDataList.Add(moveData);
        MergeController.CurrentController.RefreshBottomStore();

        int index = moveItem.curIndex;
        storeItemList.Remove(moveItem);
        storeItemList.Add(moveItem);
        moveItem.transform.SetSiblingIndex(storeItemList.Count - 1);
        moveItem.SetIndex(storeItemList.Count - 1);
        for (int i = index; i < storeItemList.Count - 1; i++) 
        {
            storeItemList[i].SetIndex(storeItemList[i].curIndex - 1);
        }

        TweenToEnd(moveItem, index);
    }
    public void SetIndex(Item_Storage item) 
    {
        item.transform.SetSiblingIndex(item.curIndex);
    }

    private int GridWidth = 170;
    public int TryReturnIndexByGrid(Vector3 worldPos) 
    {
        if (storeBgList.Count != gridBgList.Count + bpGridBgList.Count) 
        {
            storeBgList.Clear();
            for (int i = 0; i < gridBgList.Count; i++)
            {
                storeBgList.Add(gridBgList[i]);
            }
            for (int i = 0; i < bpGridBgList.Count; i++)
            {
                storeBgList.Add(bpGridBgList[i]);
            }
        }
        Vector3 moveEndLocalPos = ItemBgGrid.transform.InverseTransformPoint(worldPos);
        for (int i = 0; i < storeBgList.Count; i++)
        {
            float offectX = Math.Abs(storeBgList[i].transform.localPosition.x - moveEndLocalPos.x);
            float offectY = Math.Abs(storeBgList[i].transform.localPosition.y - moveEndLocalPos.y);
            if (offectX <= GridWidth / 2 && offectY <= GridWidth / 2) 
            {
                //落在格子上
                return i;
            }
        }
        return -1;
    }

    #region 布局
    int startX = 100;
    int startY = -95;
    int num = 5;
    public void InitItemPos() 
    {
        for (int i = 0; i < storeItemList.Count; i++)
        {
            float row = startX + GridWidth * (i % num);
            float col = startY - GridWidth * (i / num);
            storeItemList[i].transform.localPosition = new Vector3(row, col, 0);
            localPosList.Add(storeItemList[i].transform.localPosition);
        }
    }
    /// <summary>
    /// 移动倒末尾动画
    /// </summary>
    /// <param name="index">移动的物品index</param>
    private void TweenToEnd(Item_Storage moveItem, int startIndex) 
    {
        moveItem.transform.DOMove(storeBgList[storeItemList.Count - 1].transform.position, 0.2f).onComplete += () =>
        {
            SetParent(moveItem.transform, false);
        };
        for (int i = startIndex; i < storeItemList.Count - 1; i++) 
        {
            storeItemList[i].transform.DOLocalMove(localPosList[i], 0.2f);
        }
    }
    private void TweenExchange(Item_Storage fromItem, int fromIndex, Item_Storage toItem, int toIndex) 
    {
        fromItem.transform.DOLocalMove(localPosList[toIndex], 0.2f);
        toItem.transform.DOLocalMove(localPosList[fromIndex], 0.2f);
    }
    public void TweenDeleteItem(int deleteIndex) 
    {
        for (int i = deleteIndex + 1; i < storeItemList.Count; i++)
        {
            storeItemList[i].transform.DOLocalMove(localPosList[i - 1], 0.2f);
            storeItemList[i].SetIndex(storeItemList[i].curIndex - 1);
        }
    } 
    #endregion
}
