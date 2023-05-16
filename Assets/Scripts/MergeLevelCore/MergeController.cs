using DG.Tweening;
using Ivy;
using IvyCore;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 事件类型
/// </summary>
public enum MergeActionType
{
    Merge,//合成物体

    TaskPrefabComplete,//完成订单预制体
    TaskComplete,//完成一条订单

    ShopBuy,//商店消费（包括消耗钻石、消耗金币和付费，不包括免费获取）
    AddEnergy,//增加体力（包括购买和看广告，不包括自动恢复）
    SpendEnergy,//消耗体力
    MapDataChange,//地图数据变化
    AddOrRemoveItem,//增加或减少item(不包括交换位置，移入移出背包)
}

/// <summary>
/// 事件管理系统
/// </summary>
public static class MergeActionSystem
{
    public static event Action<string> GameActionEvent_Merge;
    public static event Action<string> GameActionEvent_TaskPrefabComplete;
    public static event Action GameActionEvent_TaskComplete;
    public static event Action GameActionEvent_RefreshTaskNum;
    public static event Action GameActionEvent_ShopBuy;
    public static event Action GameActionEvent_AddEnergy;
    public static event Action<int> GameActionEvent_SpendEnergy;
    public static event Action GameActionEvent_MapDataChange;
    public static event Action GameActionEvent_AddOrRemoveItem;

    public static void OnMergeActionEvent(MergeActionType actionType, params object[] msg)
    {
        switch (actionType)
        {
            case MergeActionType.Merge:
                GameDebug.Log("GameActionType.Merge");
                VibrateSystem.Haptic(HapticTypes.Selection);

                if (msg != null && msg.Length > 0 && msg[0] != null && !string.IsNullOrEmpty(msg[0].ToString()))
                {
                    GameActionEvent_Merge?.Invoke(msg[0].ToString());
                }
                break;
            case MergeActionType.TaskPrefabComplete:
                GameDebug.Log("GameActionType.TaskComplete");
                VibrateSystem.Haptic(HapticTypes.Success);


                if (msg != null && msg.Length > 0 && msg[0] != null)
                {
                    GameActionEvent_TaskPrefabComplete?.Invoke(msg[0].ToString());
                }
                break;
            case MergeActionType.ShopBuy:
                GameDebug.Log("GameActionType.ShopBuy");
                VibrateSystem.Haptic(HapticTypes.Selection);

                GameActionEvent_ShopBuy?.Invoke();
                break;
            case MergeActionType.AddEnergy:
                GameDebug.Log("GameActionType.AddEnergy");
                VibrateSystem.Haptic(HapticTypes.Selection);

                GameActionEvent_AddEnergy?.Invoke();
                break;
            case MergeActionType.SpendEnergy:
                GameDebug.Log("GameActionType.SpendEnergy");
                VibrateSystem.Haptic(HapticTypes.Selection);

                if (msg != null && msg.Length > 0 && msg[0] != null && msg[0] is int)
                {
                    GameActionEvent_SpendEnergy?.Invoke((int)msg[0]);
                }
                break;
            case MergeActionType.TaskComplete:
                GameDebug.Log("GameActionType.TaskComplete");

                VibrateSystem.Haptic(HapticTypes.Success);

                GameActionEvent_TaskComplete?.Invoke();
                GameActionEvent_RefreshTaskNum?.Invoke();
                break;
            case MergeActionType.MapDataChange:

                GameActionEvent_MapDataChange?.Invoke();
                GameActionEvent_RefreshTaskNum?.Invoke();
                break;
            case MergeActionType.AddOrRemoveItem:

                GameActionEvent_AddOrRemoveItem?.Invoke();
                break;
            default:
                GameDebug.LogError("未找到类型：" + actionType);
                break;
        }

    }
}



/// <summary>
/// 地图合成逻辑
/// </summary>
public class MergeController : MonoBehaviour
{
    public static MergeController CurrentController { get; private set; }
    //private UIPanel_Merge uiPanel_Merge;
    private UIMergeBase uiMergeBase;

    //[LabelText("地图列数"), BoxGroup("基本设置"), SerializeField]
    private int gridXCount = 7;
    public int GridXCount
    {
        get
        {
            return gridXCount;
        }
    }
    //[LabelText("地图行数"), BoxGroup("基本设置"), SerializeField]
    private int gridYCount = 8;
    public int GridYCount
    {
        get
        {
            return gridYCount;
        }
    }
    [LabelText("地图方格宽度"), BoxGroup("基本设置"), SerializeField]
    private int gridWidth = 150;
    public int GridWidth
    {
        get
        {
            return gridWidth;
        }
    }
    [LabelText("地图方格预设"), BoxGroup("基本设置"), SerializeField]
    private List<MergeGrid> gridprefabList;
    [LabelText("默认格子图片"), SerializeField] private Sprite saveGridDefaultSprite;

    [LabelText("元素拖动层节点"), BoxGroup("UI关联"), SerializeField]
    private Transform dragPanelTran;
    public Transform DragPanelTran
    {
        get
        {
            return dragPanelTran;
        }
    }

    [LabelText("是否自动生成方格"), BoxGroup("UI关联"), SerializeField]
    private List<bool> AutoCreateGridList;
    [LabelText("地图元素节点"), BoxGroup("UI关联"), SerializeField]
    private Transform mapRoot;
    public Transform MapRoot
    {
        get
        {
            return mapRoot;
        }
    }
    [LabelText("地图方格节点"), BoxGroup("UI关联"), SerializeField]
    private Transform gridRoot;
    [LabelText("泡泡节点"), BoxGroup("UI关联"), SerializeField]
    private Transform bubbleRoot;
    [LabelText("背包节点"), BoxGroup("UI关联"), SerializeField]
    private Transform storeTrans;
    public Transform StoreTrans
    {
        get
        {
            return storeTrans;
        }
    }
    [LabelText("临时仓库节点"), BoxGroup("UI关联"), SerializeField]
    private Transform rewardBoxTrans;
    public Transform RewardBoxTrans
    {
        get
        {
            return rewardBoxTrans;
        }
    }
    [LabelText("选中框"), BoxGroup("UI关联"), SerializeField]
    private CanvasGroup selectBox;

    [LabelText("合成特效层"), BoxGroup("UI关联"), SerializeField] private Transform mergeEffectTran;

    public MergeItemData currentSelectItemData { get; private set; }//当前选中的物体

    [HideInInspector] public bool hasItemDragging;//有物体正在拖拽


    private void SetMergeSystem(UIMergeBase mergeBase)
    {
        CurrentController = this;
        this.uiMergeBase = mergeBase;
    }

    private bool hideGrid = false;
    private void CheckMapSize()
    {
        var levelType = MergeLevelManager.Instance.CurrentLevelType;
        switch (levelType)
        {
            case MergeLevelType.mainLine:
            case MergeLevelType.daily2:
            case MergeLevelType.branch_SpurLine4:
            case MergeLevelType.branch_SpurLine5:
            case MergeLevelType.branch_SpurLine6:
                {
                    gridXCount = 7;
                    gridYCount = 8;
                    hideGrid = false;
                }
                break;
            case MergeLevelType.daily1:
                {
                    gridXCount = 7;
                    gridYCount = 7;
                    hideGrid = true;
                }
                break;
            case MergeLevelType.dungeon1:
            case MergeLevelType.branch1:
            case MergeLevelType.halloween:
            case MergeLevelType.christmas:
            case MergeLevelType.lover:
            case MergeLevelType.branch_christmas:
            case MergeLevelType.branch_halloween:
            case MergeLevelType.daily5:

                {
                    gridXCount = 7;
                    gridYCount = 7;
                    hideGrid = false;
                }
                break;
            case MergeLevelType.dungeon2:
                {
                    gridXCount = 7;
                    gridYCount = 6;
                    hideGrid = true;
                }
                break;
            case MergeLevelType.daily3:
            case MergeLevelType.daily4:
            case MergeLevelType.daily6:
            case MergeLevelType.dungeon3:
            case MergeLevelType.dungeon4:
            case MergeLevelType.dungeon5:
                {
                    gridXCount = 7;
                    gridYCount = 8;
                    hideGrid = true;
                }
                break;
#if UNITY_EDITOR
            case MergeLevelType.DebugMap_FreeGrid:
                {
                    gridXCount = MapEditSystem.Instance.CurrentMapGridCount.x;
                    gridYCount = MapEditSystem.Instance.CurrentMapGridCount.y;
                    hideGrid = false;
                }
                break;
#endif
        }

        if (mapRoot.TryGetComponent<RectTransform>(out var mapRect))
            mapRect.sizeDelta = new Vector2(150 * GridXCount, 150 * GridYCount);
        if (gridRoot.TryGetComponent<RectTransform>(out var gridRect))
            gridRect.sizeDelta = new Vector2(150 * GridXCount, 150 * GridYCount);
    }

    private List<MergeGrid> saveGridList = new List<MergeGrid>();
    private List<string> remoteTaskBgNameList = new List<string>();
    /// <summary>
    /// 加载合成界面远程资源（地图,任务背景）
    /// </summary>
    private void TryInitRemoteAsset(GameObject tableBg, GameObject taskBg, GameObject mapBg)
    {
        string tableBgName = string.Empty;
        string mapBgName = string.Empty;
        string gridPrefabName = string.Empty;
        string taskBgPrefabName = string.Empty;
        switch (MergeLevelManager.Instance.CurrentLevelType)
        {
            case MergeLevelType.halloween:
            case MergeLevelType.branch_halloween:
                tableBgName = "RemoteAtlas_Halloween[HalloweenBg2]";
                mapBgName = "RemoteAtlas_Halloween[HalloweenBg1]";
                gridPrefabName = "RemoteAtlas_Halloween[LevelFloor_Halloween_Lattice1]";
                taskBgPrefabName = "RemoteTaskBG_Halloween";
                break;
            case MergeLevelType.christmas:
            case MergeLevelType.branch_christmas:
                tableBgName = "RemoteAtlas_Christmas[ChristmasBg1]";
                mapBgName = "RemoteAtlas_Christmas[ChristmasMapBg]";
                gridPrefabName = "RemoteAtlas_Christmas[LevelFloor_Christmas2]";
                taskBgPrefabName = "RemoteTaskBG_Christmas";
                break;
            case MergeLevelType.lover:
                tableBgName = "RemoteAtlas_Lover[Bg]";
                mapBgName = "RemoteAtlas_Lover[LevelFloor]";
                gridPrefabName = "RemoteAtlas_Lover[LevelFloor_Lattice1]";
                taskBgPrefabName = "RemoteTaskBG_Lover";
                break;
        }

        if (tableBg != null && tableBg.TryGetComponent(out Image tableImg))
        {
            AssetSystem.Instance.LoadAssetAsync<Sprite>(tableBgName, (sprite) =>
             {
                 tableImg.sprite = sprite;
             });
        }
        if (mapBg != null && mapBg.TryGetComponent(out Image mapImg))
        {
            AssetSystem.Instance.LoadAssetAsync<Sprite>(mapBgName, (sprite) =>
             {
                 mapImg.sprite = sprite;
             });
        }
        AssetSystem.Instance.LoadAssetAsync<Sprite>(gridPrefabName, (sprite) =>
         {
             if (gridprefabList[1].TryGetComponent(out Image gridImg))
             {
                 for (int i = 0; i < saveGridList.Count; i++)
                 {
                     saveGridList[i].SetSprite(sprite);
                 }
             }
         });
        if (taskBg != null && !remoteTaskBgNameList.Contains(taskBgPrefabName))
        {
            GameObject go = AssetSystem.Instance.Instantiate(taskBgPrefabName, taskBg.transform);
            remoteTaskBgNameList.Add(taskBgPrefabName);
        }
    }

    public void InitMapInfo(UIMergeBase mergeBase, GameObject tableBg, GameObject taskBg, GameObject mapBg)
    {
        SetMergeSystem(mergeBase);
        CheckMapSize();

        currentSelectItemData = null;
        hasItemDragging = false;
        saveGridList.Clear();

        //1、加载地图（格子）
        if (gridprefabList == null || gridprefabList.Count == 0)
        {
            Debug.LogError("Map Resouces Error!");
            return;
        }
        if (gridRoot.TryGetComponent<UnityEngine.UI.GridLayoutGroup>(out var gridLayout))
            gridLayout.enabled = true;
        List<MergeGrid> hideGridList = new List<MergeGrid>();
        int listIndex = 0;
        for (int j = 1; j <= GridYCount; j++)
        {
            for (int i = 1; i <= GridXCount; i++)
            {
                MergeGrid grid = null;
                var trans = gridRoot.GetChildByName($"Floor_{listIndex + 1}");
                if (trans != null && trans.TryGetComponent<MergeGrid>(out var g))
                {
                    grid = g;
                    grid.gameObject.SetActive(true);
                }
                else
                {
                    //var remainder = listIndex % gridprefabList.Count;
                    grid = Instantiate(gridprefabList[1].gameObject).GetComponent<MergeGrid>();
                    grid.gameObject.name = $"Floor_{listIndex + 1}";
                    grid.transform.SetParent(gridRoot, false);
                }

                Vector2Int pos = new Vector2Int(i, j);
                if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(pos, out var gridData))
                {
                    grid.SetGridData(gridData);
                    if (hideGrid)
                        grid.SetAlpha(0);
                    else
                    {
                        if ((pos.x + pos.y) % 2 == 0)
                            grid.SetAlpha(0);
                        else
                            grid.SetAlpha(1);
                        saveGridList.Add(grid);
                    }
                    gridData.ChangeGridGO(grid);
                }
                else
                {
                    hideGridList.Add(grid);
                }
                listIndex++;
            }
        }
        //隐藏多余的grid（由大地图关卡切换至小地图关卡时会出现此情况）
        while (true)
        {
            var trans = gridRoot.GetChildByName($"Floor_{listIndex + 1}");
            if (trans != null)
                trans.gameObject.SetActive(false);
            else
                break;
            listIndex++;
        }
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gridRoot.GetComponent<RectTransform>());
        //根据地图布局，隐藏对应的grid
        if (gridLayout != null)
            gridLayout.enabled = false;
        var itr = hideGridList.GetEnumerator();
        while (itr.MoveNext())
            itr.Current.gameObject.SetActive(false);

        //加载grid资源
        if (MergeLevelManager.Instance.IsFestivalBranch(MergeLevelManager.Instance.CurrentLevelType)
            || MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.branch_christmas
            || MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.branch_halloween
            )
        {
            TryInitRemoteAsset(tableBg, taskBg, mapBg);
        }
#if UNITY_EDITOR
        else if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.DebugMap_FreeGrid
            && DebugSetting.CanUseDebugMap(out var debugMapSO)
            && debugMapSO.Debug_EnterDebugMap
            && debugMapSO.Debug_MapGridSprite != null)
        {
            for (int i = 0; i < saveGridList.Count; i++)
            {
                saveGridList[i].SetSprite(debugMapSO.Debug_MapGridSprite);
            }
        }
#endif
        else
        {
            //设置默认格子
            for (int i = 0; i < saveGridList.Count; i++)
            {
                saveGridList[i].SetSprite(saveGridDefaultSprite);
            }
        }
        //2、加载item  
        if (mapRoot.childCount > 0)
        {
            LogUtils.LogError("数据异常，有物体没有被删除！");
            for (int i = mapRoot.childCount - 1; i >= 0; i--)
            {
                Destroy(mapRoot.GetChild(i).gameObject);
            }
        }

        foreach (var item in MergeLevelManager.Instance.CurrentMapData.itemDataDict)
        {
            if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(item.Value.PrefabName, out var def))
            {
                if (IsCreatePrefab(def.ItemId.ToString().Substring(0, 1)))
                {
                    CreateItem_WithData(item.Value);
                }
            }
        }

        var removeList = new List<MergeItemData>();
        foreach (var item in MergeLevelManager.Instance.CurrentMapData.bubbleDataList)
        {
            if (CreateBubbleItemByLocalData(item))
                removeList.Add(item);
        }
        for (int i = removeList.Count - 1; i >= 0; i--)
            MergeLevelManager.Instance.CurrentMapData.RemoveDataFromBubbleList(removeList[i]);
        RefreshMergeItemByChangeItem();  //刷新物品层级

    }

    private bool IsCreatePrefab(string s)
    {
        if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine)
            return s == "1" || s == "2" || s == "3";
        return true;
    }

    public void CleanMap()
    {
        currentSelectItemData = null;
        hasItemDragging = false;

        foreach (var item in MergeLevelManager.Instance.CurrentMapData.itemDataDict)
        {
            if (item.Value.ItemGO != null)
            {
                AssetSystem.Instance.UnspawnMergeItem(item.Value.ItemGO);

            }
        }

        foreach (var item in MergeLevelManager.Instance.CurrentMapData.storePackList)
        {
            if (item.ItemGO != null)
            {
                AssetSystem.Instance.UnspawnMergeItem(item.ItemGO);
            }
        }

        foreach (var item in MergeLevelManager.Instance.CurrentMapData.bubbleDataList)
        {
            if (item.BubbleGO != null)
            {
                AssetSystem.Instance.UnspawnBubbleItem(item.BubbleGO);
            }
        }
    }

    /// <summary>
    /// 合成逻辑
    /// </summary>
    public void TryMergeItem(MergeItem draggingItem, Vector2Int endGridPos)
    {
        MergeItem endItem = null;
        MergeGrid endGrid = null;

        if (draggingItem == null)
        {
            GameDebug.LogError("[MergeLogic] data error!");
            return;
        }

        if (draggingItem.ItemData == null || draggingItem.ItemData.Definition == null)
        {
            GameDebug.LogError("[MergeLogic] data error 2!");
            return;
        }

        if (MergeLevelManager.Instance.CurrentMapData == null)
        {
            return;
        }

        Vector3 originGridWorldPos = Vector3.zero;
        Vector2Int originGridPos = draggingItem.ItemData.GridPos;
        if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(originGridPos, out var originGrid))
        {
            originGridWorldPos = originGrid.GridGO.transform.position;
        }
        else
        {
            GameDebug.LogError("[MergeLogic] 找不到原来的格子位置");
            return;
        }


        if (originGridPos == endGridPos)
        {
            //物品又被放回原来的位置
            draggingItem.PlayTween_Flying(originGridWorldPos, () =>
            {
                ShowWithTween_BoxSelected(originGridWorldPos);
                RefreshMergeItemByChangeItem();
            });
        }
        else
        {
            if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(endGridPos, out var endGridData))
            {
                if (endGridData == null || endGridData.GridGO == null)
                {
                    GameDebug.LogError("[MergeLogic] 目标位置的Grid物体或GameObject为空");
                    return;
                }
                endGrid = endGridData.GridGO;
                Vector3 endGridWorldPos = endGridData.GridGO.transform.position;
                if (MergeLevelManager.Instance.CurrentMapData.itemDataDict.TryGetValue(endGridPos, out var endItemData))
                {
                    if (endItemData == null || endItemData.ItemGO == null || endItemData.Definition == null)
                    {
                        GameDebug.LogError("[MergeLogic] 目标位置的Item物体或Definition为空");
                        MergeLevelManager.Instance.CurrentMapData.ChangeItemData(endGridPos, null);
                        return;
                    }
                    endItem = endItemData.ItemGO;
                    if (endItem == null || endItem.ItemData == null || !endItem.interactable)
                    {
                        //返回原位置
                        draggingItem.PlayTween_Flying(originGridWorldPos, () =>
                        {
                            ShowWithTween_BoxSelected(originGridWorldPos);
                            RefreshMergeItemByChangeItem();
                        });
                    }
                    else if (draggingItem.ItemData.CanMerge() && endItemData.CanMerge() && endItemData.PrefabName == draggingItem.ItemData.PrefabName)
                    {
                        //同一类物体且都不是在泡泡里面（合成）
                        MergeLevelManager.Instance.CurrentMapData.ChangeItemData(originGridPos, null);
                        mergeNum++;
                        MergeTwoItem(draggingItem.ItemData, endGridPos, endItem.transform.position);
                        AssetSystem.Instance.UnspawnMergeItem(draggingItem);
                        if (endItem.ItemData.IsLocked)
                            endItem.ShowLockedOpenEffect(true);
                        else
                            AssetSystem.Instance.UnspawnMergeItem(endItem);
                        MergeActionSystem.OnMergeActionEvent(MergeActionType.Merge, draggingItem.ItemData.PrefabName);
                    }
                    else if ((endItemData.Definition.CategoryType == MergeItemCategoryType.swallowC
                        || endItemData.Definition.CategoryType == MergeItemCategoryType.swallowZ
                        || endItemData.Definition.CategoryType == MergeItemCategoryType.swallowF)
                        && endItemData.canSwallow && endItemData.SwallowMergeItem(draggingItem.ItemData.PrefabName))
                    {
                        draggingItem.PlayTween_BeSwallow();
                        endItem.Invoke("PlayTween_Devour", 0.65f);
                        SetCurrentSelectData(null);
                        MergeLevelManager.Instance.CurrentMapData.ChangeItemData(originGridPos, null);
                        MergeLevelManager.Instance.CurrentMapData.AddOrRemoveItemData();
                        //AssetSystem.Instance.UnspawnMergeItem(draggingItem);
                        MergeActionSystem.OnMergeActionEvent(MergeActionType.MapDataChange);
                    }
                    else if (endItemData.IsLocked && !endItemData.IsInBox && !endItemData.IsInBubble
                        && draggingItem.ItemData.Definition.CategoryType == MergeItemCategoryType.clean)
                    {//clean道具
                        endItemData.SetData_OpenPack();
                        uiMergeBase.ShowNewDiscoveryView(endItemData.PrefabName);
                        SetCurrentSelectData(null);
                        OpenNearBox(endGridPos);
                        MergeLevelManager.Instance.CurrentMapData.ChangeItemData(originGridPos, null);
                        MergeLevelManager.Instance.CurrentMapData.AddOrRemoveItemData();
                        AssetSystem.Instance.UnspawnMergeItem(draggingItem);
                    }
                    else if (draggingItem.ItemData.Definition.CategoryType == MergeItemCategoryType.universal
                        && endItemData.Definition.CategoryType != MergeItemCategoryType.taskBox
                        && !endItemData.PrefabName.Contains("PiggyBank_") && endItemData.CanMerge()
                        && MergeItemChain.GetAfterChainItem(endItemData.Definition, out var afterChainItem))
                    {
                        if (CreateItem_InGridDirectly(afterChainItem.PrefabName, endGridPos))
                        {
                            OpenNearBox(endGridPos);
                            MergeLevelManager.Instance.CurrentMapData.ChangeItemData(originGridPos, null);
                            AssetSystem.Instance.UnspawnMergeItem(draggingItem);
                            if (endItem.ItemData.IsLocked)
                                endItem.ShowLockedOpenEffect(true);
                            else
                                AssetSystem.Instance.UnspawnMergeItem(endItem);
                        }
                    }
                    else if (draggingItem.ItemData.Definition.CategoryType == MergeItemCategoryType.splitUp
                        && endItemData.Definition.CategoryType != MergeItemCategoryType.taskBox
                        && !endItemData.PrefabName.Contains("PiggyBank_") && !endItemData.IsInBox && !endItemData.IsInBubble && !endItemData.IsLocked
                        && MergeItemChain.GetBeforeChainItem(endItemData.Definition, out var beforeChainItem) && beforeChainItem.CanMerge)
                    {
                        if (CreateItem_InGridDirectly(beforeChainItem.PrefabName, endGridPos))
                        {
                            MergeLevelManager.Instance.CurrentMapData.ChangeItemData(originGridPos, null);
                            AssetSystem.Instance.UnspawnMergeItem(draggingItem);
                            if (TryGetNearEmptyGrid(endGridPos, out var gridPos))
                                CreateItem_FlyToGrid(beforeChainItem.PrefabName, endItem.transform.position, gridPos);
                            AssetSystem.Instance.UnspawnMergeItem(endItem);
                        }
                    }
                    else if (endItemData.IsInBox || endItemData.IsLocked)
                    {
                        //返回原位置
                        draggingItem.PlayTween_Flying(originGridWorldPos, () =>
                        {
                            ShowWithTween_BoxSelected(originGridWorldPos);
                            RefreshMergeItemByChangeItem();
                        });
                    }
                    else
                    {
                        //两个物体且不能合成,交换位置(包括满级处理)
                        MergeLevelManager.Instance.CurrentMapData.ChangeItemData(originGridPos, null);
                        MergeLevelManager.Instance.CurrentMapData.ChangeItemData(endGridPos, draggingItem.ItemData);
                        if (TryGetEmptyGridAround(endGridPos, out Vector2Int emptyGrid, true)
                             && MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(emptyGrid, out var gridData)
                             && gridData != null
                             && gridData.GridGO != null)
                        {
                            MergeLevelManager.Instance.CurrentMapData.ChangeItemData(emptyGrid, endItem.ItemData);
                        }
                        else
                        {
                            //GameDebug.LogError("cannot found empty!");
                            MergeLevelManager.Instance.CurrentMapData.ChangeItemData(originGridPos, endItemData);
                        }
                        //endItem.ResetItemInfo(endItem.ItemData);
                        //draggingItem.ResetItemInfo(draggingItem.ItemData);
                        draggingItem.ReturnToSelfGridPos(true);
                        endItem.ReturnToSelfGridPos();
                    }
                }
                else
                {
                    //当前格子上没有物体
                    MergeLevelManager.Instance.CurrentMapData.ChangeItemData(originGridPos, null);
                    MergeLevelManager.Instance.CurrentMapData.ChangeItemData(endGridPos, draggingItem.ItemData);

                    if (endGrid != null)
                    {
                        //draggingItem.ResetItemInfo(draggingItem.ItemData);
                        draggingItem.PlayTween_Flying(endGridWorldPos, () =>
                        {
                            ShowWithTween_BoxSelected(endGridWorldPos);
                        });
                    }
                }
            }
            else
            {
                //没有该格子，返回原来位置
                draggingItem.PlayTween_Flying(originGridWorldPos, () =>
                {
                    ShowWithTween_BoxSelected(originGridWorldPos);
                    RefreshMergeItemByChangeItem();
                });
            }
        }
    }
    /// <summary>
    /// 尝试将从背包拖出来的物体与棋盘物品合成
    /// </summary>
    /// <param name="dragStoreItem"></param>
    /// <param name="endGridPos"></param>
    public void TryMergeItemFromStore(StoreItem dragStoreItem, Vector2Int endGridPos)
    {
        if (dragStoreItem == null || endGridPos == null)
        {
            //返回原位
            dragStoreItem.ReturnStartPos();
            return;
        }
        //落在棋盘上
        if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(endGridPos, out var endGridData))
        {
            if (endGridData == null || endGridData.GridGO == null)
            {
                GameDebug.LogError("[MergeLogic] 目标位置的Grid物体或GameObject为空");
                return;
            }
            Vector3 endGridWorldPos = endGridData.GridGO.transform.position;//终点格子位置
            if (MergeLevelManager.Instance.CurrentMapData.itemDataDict.TryGetValue(endGridPos, out var endItemData))
            {
                //落点格子上有物体
                MergeItem endItem = endItemData.ItemGO;

                if (endItemData.IsInBox)
                {
                    dragStoreItem.ReturnStartPos();
                    return;
                }
                if (dragStoreItem.mergeItemData.PrefabName == endItemData.PrefabName && dragStoreItem.mergeItemData.CanMerge() && endItemData.CanMerge())
                {
                    //合成
                    MergeTwoItem(dragStoreItem.mergeItemData, endGridPos, endGridData.GridGO.transform.position);
                    RemoveItemFromStore(dragStoreItem.mergeItemData);
                    if (endItem != null && endItem.ItemData != null)
                    {
                        if (endItem.ItemData.IsLocked)
                            endItem.ShowLockedOpenEffect(true);
                        else
                            AssetSystem.Instance.UnspawnMergeItem(endItem);
                    }
                    MergeActionSystem.OnMergeActionEvent(MergeActionType.Merge, endItem.ItemData.PrefabName);
                    RefreshMergeAboveEffect(false, endItemData.ItemGO.transform.position);
                }
                else
                {
                    if (!endItemData.IsLocked)
                    {
                        //交换
                        CreateItem_ByClickStoreItem(dragStoreItem.mergeItemData, endGridWorldPos, endGridPos, false);
                        AssetSystem.Instance.UnspawnMergeItem(endItem);
                        RemoveItemFromStore(dragStoreItem.mergeItemData, out int index);
                        MergeLevelManager.Instance.CurrentMapData.AddToStorePackList(endItem.ItemData, index);
                        uiMergeBase.RefreshBottomStore();
                    }
                    else
                    {
                        //返回原地
                        dragStoreItem.ReturnStartPos();
                    }
                }
            }
            else
            {
                //落点格子没有物体
                CreateItem_ByClickStoreItem(dragStoreItem.mergeItemData, endGridWorldPos, endGridPos, false);
                RemoveItemFromStore(dragStoreItem.mergeItemData);
            }
        }
    }
    public void RemoveItemFromStore(MergeItemData itemData)
    {
        MergeLevelManager.Instance.CurrentMapData.RemoveStorePackList(itemData);
        uiMergeBase.RefreshBottomStore();
    }
    public void RemoveItemFromStore(MergeItemData itemData, out int x)
    {
        x = MergeLevelManager.Instance.CurrentMapData.RemoveStorePackListWithCount(itemData);
        //uiMergeBase.RefreshBottomStore();
    }
    //返回用于合成的两个物品剩余合成次数
    private int ReturnMergeItemLeftSpawnCount(MergeItemData dragItemData, MergeItemData endItemData)
    {
        List<MergeItemData> twoItemDataList = new List<MergeItemData>();
        twoItemDataList.Add(dragItemData);
        twoItemDataList.Add(endItemData);
        int count = 0;
        for (int i = 0; i < twoItemDataList.Count; i++)
        {
            count += twoItemDataList[i].chargeRemainUseTimes;
            count += twoItemDataList[i].totalChargedCount * twoItemDataList[i].Definition.CanSpawnCountByOneCharge;
        }
        return count;
    }
    /// <summary>
    /// 合成两个物体
    /// </summary>
    /// <param name="dragItemData">拖拽的物体信息</param>
    /// <param name="endGridPos">终点位置信息</param>
    /// <param name="endGridWorldPos">终点格子坐标</param>
    public bool MergeTwoItem(MergeItemData dragItemData, Vector2Int endGridPos, Vector3 endGridWorldPos)
    {
        AudioManager.Instance.PlayMergeSound(dragItemData.Definition.Level);

        int leftSpawnCount = 0;
        if (dragItemData.Definition.CategoryType == MergeItemCategoryType.container
            && MergeLevelManager.Instance.CurrentMapData.itemDataDict.TryGetValue(endGridPos, out var endItemData)
            && endItemData.Definition.CategoryType == MergeItemCategoryType.container)
        {
            leftSpawnCount = ReturnMergeItemLeftSpawnCount(dragItemData, endItemData);
        }

        if (CreateItem_InGridDirectly(dragItemData.Definition.MergeOutputPrefab, endGridPos, true, leftSpawnCount))
        {
            OpenNearBox(endGridPos);

            //掉落固定经验道具
            if (dragItemData.Definition.fixedSpawnList != null && dragItemData.Definition.fixedSpawnList.Count > 0)
            {
                foreach (var item in dragItemData.Definition.fixedSpawnList)
                {
                    for (int i = 0; i < item.num; i++)
                    {
                        if (TryGetNearEmptyGrid(endGridPos, out var targetPos))
                        {
                            CreateItem_FlyToGrid(item.name, endGridWorldPos, targetPos);
                        }
                    }
                }
            }

            //概率产出带泡泡的物体
            if (!UI_TutorManager.Instance.IsTutoring()
                && dragItemData.Definition.BubbleEqul > 0 && !string.IsNullOrEmpty(dragItemData.Definition.MergeOutputPrefab))
            {
                float random = UnityEngine.Random.Range(0, 100f);
#if UNITY_EDITOR
                if (DebugSetting.CanUseDebugConfig(out var debugSO)
                    && debugSO.DebugSpawnBubble
                    && random <= debugSO.DebugSpawnBubbleRate
                    || random <= dragItemData.Definition.BubbleEqul)
#else
                if (random <= dragItemData.Definition.BubbleEqul)
#endif
                {
                    //if (TryGetNearEmptyGrid(endGridPos, out var targetPos))
                    //{
                    //    CreateItem_FlyToGrid(dragItemData.Definition.MergeOutputPrefab, endGridWorldPos, targetPos, dragItemData.Definition.DieTime);
                    //}
                    CreateBubbleItem(dragItemData.Definition.MergeOutputPrefab, dragItemData.Definition.DieTime, endGridWorldPos);
                }
            }

            TryKillMergeTween(dragItemData);
            return true;
        }

        return false;
    }

    /// <summary>
    /// 点击已经选中的物体
    /// </summary>
    /// <param name="clickItem"></param>
    /// <param name="Definition"></param>
    public void OnClickSelectItem(MergeItemData selectItemData)
    {
        if (selectItemData == null || selectItemData.Definition == null || selectItemData.ItemGO == null)
        {
            return;
        }

        if (selectItemData.IsInBubble || selectItemData.IsInBox || selectItemData.IsLocked)
        {
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Tap_Eliminate);
            return;
        }
        switch (selectItemData.Definition.CategoryType)
        {
            case MergeItemCategoryType.container:
                {
                    if (selectItemData.CanSpawnItem_ByContainer())
                    {
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Produce);

                        SpawnRandomItem_ByContainer(selectItemData.ItemGO, selectItemData.SpawnOneItem_ByInfiniteContainer);
                        uiMergeBase.RefreshSelectItemInfo();
                    }
                    else
                    {
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Tap_Eliminate);
                    }
                }
                break;
            case MergeItemCategoryType.dualSkills:
                {
                    if (selectItemData.chargeRemainUseTimes > 0 || selectItemData.totalChargedCount > 0)
                    {
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Produce);

                        SpawnRandomItem_ByContainer(selectItemData.ItemGO, selectItemData.SpawnOneItem_ByInfiniteContainer);
                        uiMergeBase.RefreshSelectItemInfo();
                    }
                    else
                    {
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Tap_Eliminate);
                    }
                }
                break;
            case MergeItemCategoryType.consumable:
                {
                    AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Use_Coins);

                    MergeLevelManager.Instance.CurrentMapData.ChangeItemData(selectItemData.GridPos, null);
                    Vector3 screenPos = selectItemData.ItemGO.transform.position;
                    GameManager.Instance.GiveRewardItem(selectItemData.Definition.Strength, "CollectOnMap", screenPos);

                    AssetSystem.Instance.UnspawnMergeItem(selectItemData.ItemGO);
                    HideWithTween_BoxSelected();
                    SetCurrentSelectData(null);
                }
                break;
            case MergeItemCategoryType.finiteContainer:
            case MergeItemCategoryType.taskBox:
            case MergeItemCategoryType.wake:
                {
                    if (selectItemData.CanSpawnItem_ByContainer())
                    {
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Produce);
                        bool allRight = SpawnRandomItem_ByContainer(selectItemData.ItemGO, selectItemData.SpawnOneItem_ByFiniteContainer);
                        //判断有限容器是否生成结束
                        if (selectItemData.IsSpawnFinish_ByFiniteContainer() /*|| !allRight*/)
                        {
                            MergeLevelManager.Instance.CurrentMapData.ChangeItemData(selectItemData.GridPos, null);
                            MergeLevelManager.Instance.CurrentMapData.AddOrRemoveItemData();
                            //销毁物体
                            if (selectItemData.Definition.CategoryType == MergeItemCategoryType.finiteContainer
                                || selectItemData.Definition.CategoryType == MergeItemCategoryType.taskBox)
                            {
                                selectItemData.ItemGO.ShowItemDeadEffect();
                                AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Break_Initial);
                            }
                            else
                                AssetSystem.Instance.UnspawnMergeItem(selectItemData.ItemGO);

                            if (selectItemData.Definition.IsSpawnPrefabWithDie)
                                CreateItem_InGridDirectly(selectItemData.Definition.DieCreatePrefab, selectItemData.GridPos);

                            HideWithTween_BoxSelected();
                            SetCurrentSelectData(null);
                        }
                    }
                    else
                    {
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Tap_Eliminate);
                    }
                }
                break;
            case MergeItemCategoryType.modelSwitch:
                {
                    if (selectItemData.CanSpawnItem_ByContainer())
                    {
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Produce);
                        SpawnRandomItem_ByContainer(selectItemData.ItemGO, selectItemData.SpawnOneItem_ByModelSwitch);
                        //判断是否生成结束
                        if (selectItemData.IsSpawnFinish_ByModelSwitch())
                        {
                            MergeLevelManager.Instance.CurrentMapData.ChangeItemData(selectItemData.GridPos, null);
                            MergeLevelManager.Instance.CurrentMapData.AddOrRemoveItemData();
                            //销毁物体
                            AssetSystem.Instance.UnspawnMergeItem(selectItemData.ItemGO);

                            if (selectItemData.Definition.IsSpawnPrefabWithDie)
                            {
                                CreateItem_InGridDirectly(selectItemData.Definition.DieCreatePrefab, selectItemData.GridPos);
                            }

                            HideWithTween_BoxSelected();
                            SetCurrentSelectData(null);
                        }
                    }
                    else
                    {
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Tap_Eliminate);
                    }
                }
                break;
            case MergeItemCategoryType.boxed:
                {
                    GameDebug.Log("click boxed");
                    if (selectItemData.CanSpawnItem_ByContainer())
                    {
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Produce);
                        SpawnRandomItem_ByContainer(selectItemData.ItemGO, selectItemData.SpawnOneItem_ByBoxed, true);
                        //判断有限容器是否生成结束
                        if (selectItemData.IsSpawnFinish_ByBoxed())
                        {
                            MergeLevelManager.Instance.CurrentMapData.ChangeItemData(selectItemData.GridPos, null);
                            MergeLevelManager.Instance.CurrentMapData.AddOrRemoveItemData();
                            //销毁物体
                            AssetSystem.Instance.UnspawnMergeItem(selectItemData.ItemGO);

                            if (selectItemData.Definition.IsSpawnPrefabWithDie)
                            {
                                CreateItem_InGridDirectly(selectItemData.Definition.DieCreatePrefab, selectItemData.GridPos);
                            }
                            HideWithTween_BoxSelected();
                            SetCurrentSelectData(null);
                        }
                        else
                        {
                            uiMergeBase.RefreshSelectItemInfo();
                        }
                    }
                    else
                    {
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Tap_Eliminate);
                    }
                }
                break;
            case MergeItemCategoryType.swallowC:
                {
                    if (selectItemData.CanSpawnItem_ByContainer())
                    {
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Produce);
                        SpawnRandomItem_ByContainer(selectItemData.ItemGO, selectItemData.SpawnOneItem_BySwallowC);
                        uiMergeBase.RefreshSelectItemInfo();
                    }
                    else
                    {
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Tap_Eliminate);
                    }
                }
                break;
            case MergeItemCategoryType.swallowF:
                {
                    if (selectItemData.CanSpawnItem_ByContainer())
                    {
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Produce);
                        SpawnRandomItem_ByContainer(selectItemData.ItemGO, selectItemData.SpawnOneItem_BySwallowF);
                        //判断有限容器是否生成结束
                        if (selectItemData.IsSpawnFinish_BySwallowF())
                        {
                            MergeLevelManager.Instance.CurrentMapData.ChangeItemData(selectItemData.GridPos, null);
                            //销毁物体
                            AssetSystem.Instance.UnspawnMergeItem(selectItemData.ItemGO);
                            //死亡后生成？
                            if (selectItemData.Definition.IsSpawnPrefabWithDie)
                                CreateItem_InGridDirectly(selectItemData.Definition.DieCreatePrefab, selectItemData.GridPos);

                            HideWithTween_BoxSelected();
                            SetCurrentSelectData(null);
                        }
                        //uiMergeBase.RefreshSelectItemInfo();
                    }
                    else
                    {
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Tap_Eliminate);
                    }
                }
                break;
            case MergeItemCategoryType.booster:
                {
                    AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Tap_Eliminate);
                    selectItemData.StartUpBooster();
                }
                break;
            case MergeItemCategoryType.timeBoster:
                {
                    AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Tap_Eliminate);
                    ShowTimeBoosterEffect(selectItemData.GridPos);
                    MergeLevelManager.Instance.CurrentMapData.ChangeItemData(selectItemData.GridPos, null);
                    MergeLevelManager.Instance.CurrentMapData.AddOrRemoveItemData();
                    MergeLevelManager.Instance.CurrentMapData.ClearMapItemCD(selectItemData.Definition.SpeedUpSenconds);
                    AssetSystem.Instance.UnspawnMergeItem(selectItemData.ItemGO);
                    HideWithTween_BoxSelected();
                    SetCurrentSelectData(null);
                }
                break;
            default:
                {
                    AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Tap_Eliminate);
                }
                break;
        }

    }

    public void ShowTimeBoosterEffect(Vector2Int pos)
    {
        foreach (var kv in MergeLevelManager.Instance.CurrentMapData.itemDataDict)
        {
            if (kv.Key == pos)
                continue;
            var item = kv.Value.ItemGO;
            if (item != null)
            {
                //float delay = Mathf.Abs(pos.x - kv.Key.x) + Mathf.Abs(pos.y - kv.Key.y) - 1;
                var disX = Mathf.Abs(pos.x - kv.Key.x);
                var disY = Mathf.Abs(pos.y - kv.Key.y);
                float delay = Mathf.Sqrt(Mathf.Pow(disX, 2) + Mathf.Pow(disY, 2)) - 1;
                item.PlayTween_BoxExpand(delay * 0.1f);
            }
        }
    }

    /// <summary>
    /// 点击箱子产出物品
    /// </summary>
    private bool SpawnRandomItem_ByContainer(MergeItem clickItem, Action cb, bool isLocked = false)
    {
        if (currentSelectItemData == null || currentSelectItemData.Definition == null)
        {
            Debug.LogError("当前选择数据为空");
            return true;
        }

        if ((string.IsNullOrEmpty(currentSelectItemData.Definition.PondId) && currentSelectItemData.Definition.PondId == "0")
            || currentSelectItemData.IsLocked
            || currentSelectItemData.IsInBox
            || currentSelectItemData.IsInBubble
            )
        {
            Debug.LogError("前三级,锁住,泡泡里面的点击的盒子不产生道具");
            return true;//前三级,锁住,泡泡里面的点击的盒子不产生道具
        }

        if (currentSelectItemData.Definition.NeedEnergy && !CanAffordEnergy(true))
        {
            Debug.LogError("体力不足");
            return true;
        }

        MergeRewardItem rewardItem;
        if (currentSelectItemData.Definition.CategoryType == MergeItemCategoryType.modelSwitch)
        {
            if (currentSelectItemData.modelSwitchChanged)
                rewardItem = MergeRewardItemPool.FindItemFromPool(currentSelectItemData.Definition.ModelSwitchPondID2, currentSelectItemData);
            else
                rewardItem = MergeRewardItemPool.FindItemFromPool(currentSelectItemData.Definition.ModelSwitchPondID1, currentSelectItemData);
        }
        else if (currentSelectItemData.Definition.CategoryType == MergeItemCategoryType.taskBox)
        {
            if (!string.IsNullOrEmpty(currentSelectItemData.firstOpenTaskBoxPond))
            {
                if (currentSelectItemData.firstOpenTaskBoxPond.StartsWith("Loot_"))
                {
                    if (!LootTable.LootTableDic.TryGetValue(currentSelectItemData.firstOpenTaskBoxPond, out _))
                    {
                        Debug.LogError("firstOpenTaskBoxPond数据出错" + currentSelectItemData.firstOpenTaskBoxPond);
                        currentSelectItemData.CheckFirstOpenTaskBoxPond();
                    }
                }
                else
                {
                    if (!MergeItemDefinition.TotalDefinitionsDict.TryGetValue(currentSelectItemData.firstOpenTaskBoxPond, out _))
                    {
                        Debug.LogError("firstOpenTaskBoxPond数据出错" + currentSelectItemData.firstOpenTaskBoxPond);
                        currentSelectItemData.CheckFirstOpenTaskBoxPond();
                    }
                }
                rewardItem = MergeRewardItemPool.FindItemFromPool(currentSelectItemData.firstOpenTaskBoxPond, currentSelectItemData);
            }

            else
            {
                rewardItem = new MergeRewardItem();
                Debug.LogError("TaskBox_Pond_Err!");
                currentSelectItemData.CheckFirstOpenTaskBoxPond();//已点过开启宝箱出错的玩家更包后重新查找池子
            }
        }
        else
            rewardItem = MergeRewardItemPool.FindItemFromPool(currentSelectItemData.Definition.PondId, currentSelectItemData);

        if (!rewardItem.IsValidReward() || !rewardItem.IsRewardPrefab)
        {
            Debug.LogError("数据出错" + rewardItem.name);
            return false;
        }
        if (TryGetNearEmptyGrid(clickItem.ItemData.GridPos, out var gridPos))
        {
            CreateItem_FlyToGrid(rewardItem.name, clickItem.transform.position, gridPos, 0, isLocked);
            cb?.Invoke();
            if (currentSelectItemData.Definition.NeedEnergy)
                SpendEnergy();
            produceNum++;
        }
        return true;
    }

    public bool CanAffordEnergy(bool showUI, int count = 1)
    {
        bool canAfford = true;
        if (!Currencies.CanAffordOrTip(CurrencyID.Energy, count))
        {
            canAfford = false;
            if (showUI)
                UISystem.Instance.ShowUI(new TopPopupData(TopResourceType.Energy));
        }
        return canAfford;
    }

    public void SpendEnergy(int count = 1)
    {
        if (MergeLevelManager.Instance != null && MergeLevelManager.Instance.CurrentLevelType != MergeLevelType.none)
        {
            if (MergeLevelManager.Instance.CurrentLevelType == MergeLevelType.mainLine)
            {
                Currencies.Spend(CurrencyID.Energy, count, "Lv" + TaskGoalsManager.Instance.curLevelIndex);
            }
            else
            {
                Currencies.Spend(CurrencyID.Energy, count, MergeLevelManager.Instance.CurrentLevelType.ToString());
            }
        }
        //DailyTaskSystem.Instance.DailyTaskEvent_SpendEnergy?.Invoke();
    }

    private void TryKillMergeTween(MergeItemData data)
    {
        if (DoTweenItemData == null || data == null)
            return;
        if (DoTweenItemData.PrefabName == data.PrefabName)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].PrefabName == data.PrefabName)
                {
                    itemList[i].ItemGO?.KillPlayTween_CanMerge();
                }
            }
        }
    }

    /// <summary>
    /// 返回目标位置最近的一个没有被占用的空格子位置
    /// </summary>
    /// <param name="startSearchGrid"></param>
    public bool TryGetNearEmptyGrid(Vector2Int startSearchGrid, out Vector2Int emptyGrid, bool showTips = true)
    {
        if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(startSearchGrid, out var startGrid)
            && !MergeLevelManager.Instance.CurrentMapData.itemDataDict.TryGetValue(startSearchGrid, out _))
        {
            emptyGrid = startSearchGrid;
            GameDebug.Log("找到空位置：" + emptyGrid);
            return true;
        }

        int index = 1;
        List<Vector2Int> list = new List<Vector2Int>();
        Vector2Int pos;
        while (index < 20)
        {
            if (index > GridXCount && index > GridYCount)
            {
                break;
            }

            for (int i = -index; i < index; i++)
            {
                for (int j = -index; j < index; j++)
                {
                    pos = new Vector2Int(startSearchGrid.x + i, startSearchGrid.y + j);
                    if (list.Contains(pos))
                    {
                        continue;
                    }

                    list.Add(pos);
                    if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(pos, out _)
                        && !MergeLevelManager.Instance.CurrentMapData.itemDataDict.TryGetValue(pos, out _))
                    {
                        emptyGrid = pos;
                        GameDebug.Log("找到空位置：" + emptyGrid);
                        return true;
                    }
                }
            }

            index++;
        }

        emptyGrid = Vector2Int.zero;
        if (showTips)
        {
            VibrateSystem.Haptic(MoreMountains.NiceVibrations.HapticTypes.Selection);
            TextTipSystem.Instance.ShowTip(startGrid.GridGO.transform.position + Vector3.up * 0.1f, I2.Loc.ScriptLocalization.Get("Obj/code/Eliminate/Text"), TextTipColorType.Yellow);
        }

        return false;
    }

    /// <summary>
    /// 返回目标位置最近的一个没有被占用的空格子位置
    /// </summary>
    /// <param name="startSearchGrid"></param>
    public bool TryGetNearEmptyGrid(Vector2Int startSearchGrid, out Vector2Int emptyGrid, Vector3 showTextPos)
    {
        if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(startSearchGrid, out var startGrid)
            && !MergeLevelManager.Instance.CurrentMapData.itemDataDict.TryGetValue(startSearchGrid, out _))
        {
            emptyGrid = startSearchGrid;
            GameDebug.Log("找到空位置：" + emptyGrid);
            return true;
        }

        int index = 1;
        List<Vector2Int> list = new List<Vector2Int>();
        Vector2Int pos;
        while (index < 20)
        {
            if (index > GridXCount && index > GridYCount)
            {
                break;
            }

            for (int i = -index; i < index; i++)
            {
                for (int j = -index; j < index; j++)
                {
                    pos = new Vector2Int(startSearchGrid.x + i, startSearchGrid.y + j);
                    if (list.Contains(pos))
                    {
                        continue;
                    }

                    list.Add(pos);
                    if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(pos, out _)
                        && !MergeLevelManager.Instance.CurrentMapData.itemDataDict.TryGetValue(pos, out _))
                    {
                        emptyGrid = pos;
                        GameDebug.Log("找到空位置：" + emptyGrid);
                        return true;
                    }
                }
            }

            index++;
        }

        emptyGrid = Vector2Int.zero;
        TextTipSystem.Instance.ShowTip(showTextPos + Vector3.up * 0.1f, I2.Loc.ScriptLocalization.Get("Obj/code/Eliminate/Text"), TextTipColorType.Yellow);
        //TextTipSystem.Instance.ShowTip(Vector3.zero, I2.Loc.ScriptLocalization.Get("Obj/code/Eliminate/Text"), TextTipColorType.Red);
        return false;
    }

    /// <summary>
    /// 返回目标位置周围空格子位置(九宫格)
    /// </summary>
    /// 目标位置
    /// <param name="startSearchGrid"></param>
    /// 返回结果
    /// <param name="emptyGrid"></param>
    /// 是否要求相邻
    /// <param name="bySide"></param>
    /// <returns></returns>
    public bool TryGetEmptyGridAround(Vector2Int startSearchGrid, out Vector2Int emptyGrid, bool bySide = false)
    {
        if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(startSearchGrid, out var startGrid)
            && !MergeLevelManager.Instance.CurrentMapData.itemDataDict.TryGetValue(startSearchGrid, out _))
        {
            emptyGrid = startSearchGrid;
            GameDebug.Log("找到空位置：" + emptyGrid);
            return true;
        }

        //遍历九宫格
        for (int x = startSearchGrid.x - 1; x <= startSearchGrid.x + 1; x++)
        {
            if (x > 0 && x <= GridXCount)
            {
                for (int y = startSearchGrid.y - 1; y <= startSearchGrid.y + 1; y++)
                {
                    if (bySide && x != startSearchGrid.x && y != startSearchGrid.y)
                        continue;
                    if (y > 0 && y <= GridYCount)
                    {
                        var vet = new Vector2Int(x, y);
                        if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(vet, out _)
                            && !MergeLevelManager.Instance.CurrentMapData.itemDataDict.TryGetValue(vet, out _))
                        {
                            emptyGrid = vet;
                            GameDebug.Log("找到空位置：" + emptyGrid);
                            return true;
                        }
                    }
                }
            }
        }

        emptyGrid = Vector2Int.zero;
        GameDebug.Log("没有找到空位置！");
        return false;
    }

    /// <summary>
    /// 直接在格子上生成物体
    /// </summary>
    /// <param name="prefabName"></param>
    /// <param name="parent"></param>
    /// <param name="targetGridPos"></param>
    public bool CreateItem_InGridDirectly(string prefabName, Vector2Int targetGridPos, bool showEffect = true, int leftCount = 0)
    {
        if (string.IsNullOrEmpty(prefabName))
        {
            GameDebug.LogError("prefabName is empty！");
            return false;
        }

        if (prefabName.StartsWith("Loot_"))
        {
            prefabName = FindItemFromPool(prefabName);
        }

        if (!MergeItemDefinition.TotalDefinitionsDict.TryGetValue(prefabName, out var def))
        {
            GameDebug.LogError("Objects表中没找到prefabName！" + prefabName);
            def = null;
            return false;
        }

        if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(targetGridPos, out var gridData))
        {
            //数据
            var chargeFinishDate = DateTimeOffset.MaxValue;
            if (def.CategoryType == MergeItemCategoryType.wake)
                chargeFinishDate = TimeManager.Instance.UtcNow().AddSeconds(def.ChargeLoopCDSecond);
            MergeItemData data = new MergeItemData(
                prefabName,
                targetGridPos,
                def,
                false,
                false,
                false,
                DateTimeOffset.MaxValue,
                leftCount,
                1,// def.TotalChargeCount,  初始化一次充能
                chargeFinishDate,
                0,
                def.TotalChargeCount_Auto > 0 ? 1 : 0,
                DateTimeOffset.MaxValue,
                false,
                DateTimeOffset.MinValue,
                0,
                0,
                0,
                def.CategoryType == MergeItemCategoryType.swallowC || def.CategoryType == MergeItemCategoryType.swallowZ || def.CategoryType == MergeItemCategoryType.swallowF,
                0,
                DateTimeOffset.MinValue,
                false,
                DateTimeOffset.MaxValue,
                DateTimeOffset.MinValue,
                DateTimeOffset.MinValue,
                string.Empty);
            MergeLevelManager.Instance.CurrentMapData.ChangeItemData(targetGridPos, data);
            MergeLevelManager.Instance.CurrentMapData.AddOrRemoveItemData();

            MergeItem mergeItem = AssetSystem.Instance.SpawnMergeItem(prefabName, mapRoot);
            if (mergeItem != null)
            {
                mergeItem.transform.position = gridData.GridGO.transform.position;
                mergeItem.gameObject.SetActive(true);
                mergeItem.InitItemInfo(uiMergeBase, this, data);
                if (showEffect)
                {
                    SetCurrentSelectData(data);
                    mergeItem.PlayTween_CreateByScale();
                    ShowWithTween_BoxSelected(gridData.GridGO.transform.position, null, false, 0.15f);
                    PlayMergeEffect(mergeItem.spriteRootTran);
                }
                uiMergeBase.ShowNewDiscoveryView(prefabName);
                return true;
            }
            else
                GameDebug.LogError("cannot spawnGameObject!" + prefabName);
        }
        return false;
    }

    /// <summary>
    /// 解锁周围盒子
    /// </summary>
    public void OpenNearBox(Vector2Int targetPos)
    {
        bool isPlayOpenBox = false;
        List<Vector2Int> posIntList = new List<Vector2Int>();
        posIntList.Add(new Vector2Int(targetPos.x + 1, targetPos.y));
        posIntList.Add(new Vector2Int(targetPos.x - 1, targetPos.y));
        posIntList.Add(new Vector2Int(targetPos.x, targetPos.y + 1));
        posIntList.Add(new Vector2Int(targetPos.x, targetPos.y - 1));
        for (int i = 0; i < posIntList.Count; i++)
        {
            if (MergeLevelManager.Instance.CurrentMapData.itemDataDict.TryGetValue(posIntList[i], out var mergeItemData))
            {
                if (mergeItemData != null && mergeItemData.ItemGO != null && mergeItemData.IsInBox)
                {
                    mergeItemData.SetData_OpenBox();
                    if (!isPlayOpenBox)
                    {
                        isPlayOpenBox = true;
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Break_Box);
                    }
                    if (!mergeItemData.IsLocked)
                    {
                        GameManager.Instance.TryNewDiscoveryItem(mergeItemData.PrefabName);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 转换世界坐标到格子坐标
    /// </summary>
    /// <param name="worldPos"></param>
    /// <param name="gridPos"></param>
    /// <returns></returns>
    public bool ConvertLocalPositionToGrid(Vector3 worldPos, out Vector2Int gridPos)
    {
        Vector3 localPos = mapRoot.InverseTransformPoint(worldPos);
        Vector3 leftUpPos = new Vector3(-GridXCount * 0.5f * GridWidth, GridYCount * 0.5f * GridWidth, 0);
        Vector3 offset = new Vector3(localPos.x - leftUpPos.x, -(localPos.y - leftUpPos.y), 0);
        if (offset.x > 0 && offset.y > 0)
        {
            int gridX = Mathf.CeilToInt(offset.x / GridWidth);
            int gridY = Mathf.CeilToInt(offset.y / GridWidth);
            if (gridX > GridXCount || gridY > GridYCount)
            {
                //GameDebug.Log("超出格子区域");
                gridPos = Vector2Int.zero;
                return false;
            }
            else
            {
                //GameDebug.Log("目标格子坐标：" + new Vector2Int(gridX, gridY));
                gridPos = new Vector2Int(gridX, gridY);
                return true;
            }
        }
        else
        {
            //GameDebug.Log("超出格子区域");
            gridPos = Vector2Int.zero;
            return false;
        }

    }

    public string FindItemFromPool(string pondID)
    {
        int max = 50;
        var currentPond = pondID;
        while (true)
        {
            if (max < 0)
            {
                Ivy.LogUtils.LogError("池子配表存在异常情况!" + pondID);
                break;
            }
            max--;
            if (LootTable.LootTableDic.TryGetValue(currentPond, out var lootTable))
            {
                var reward = lootTable.pool.GetRandomRewardItem();
                if (reward.IsValidReward())
                {
                    if (reward.name.StartsWith("Loot_"))
                    {
                        // 继续抽
                        currentPond = reward.name;
                    }
                    else
                    {
                        return reward.name;
                    }
                }
            }
            else
            {
                break;
            }
        }
        return currentPond;
    }


    /// <summary>
    /// 在其他地方生成物体，并飞向格子
    /// </summary>
    /// <param name="prefabName"></param>
    /// <param name="fromWorldPos"></param>
    /// <param name="toGrid"></param>
    /// <param name="inbubbleSecond">小于等于0的时候，不在气泡</param>
    /// <param name="isLocked"></param>
    public void CreateItem_FlyToGrid(string prefabName, Vector3 fromWorldPos, Vector2Int toGrid, int inbubbleSecond = 0, bool isLocked = false, bool selected = false, bool deafaultJump = false)
    {
        if (string.IsNullOrEmpty(prefabName))
        {
            GameDebug.LogError("prefabName is empty！");
            return;
        }

        if (!MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(toGrid, out var mergeGrid))
        {
            return;
        }

        if (prefabName.StartsWith("Loot_")) 
        {
            prefabName = FindItemFromPool(prefabName);
        }

        if (!MergeItemDefinition.TotalDefinitionsDict.TryGetValue(prefabName, out var def))
        {
            GameDebug.LogError("Objects表中没找到prefabName！" + prefabName);
            def = null;
            return;
        }

        //数据
        var chargeFinishDate = DateTimeOffset.MaxValue;
        if (def.CategoryType == MergeItemCategoryType.wake)
            chargeFinishDate = TimeManager.Instance.UtcNow().AddSeconds(def.ChargeLoopCDSecond);
        MergeItemData data = new MergeItemData(
            prefabName,
            toGrid,
            def,
            inbubbleSecond > 0,
            isLocked,
            false,
            inbubbleSecond > 0 && def != null ? TimeManager.Instance.UtcNow().AddSeconds(inbubbleSecond) : DateTimeOffset.MaxValue,
            0,
            1,// def.TotalChargeCount,  初始化一次充能
            chargeFinishDate,
            0,
            def.TotalChargeCount_Auto > 0 ? 1 : 0,
            DateTimeOffset.MaxValue,
            false,
            DateTimeOffset.MinValue,
            0,
            0,
            0,
            def.CategoryType == MergeItemCategoryType.swallowC || def.CategoryType == MergeItemCategoryType.swallowZ || def.CategoryType == MergeItemCategoryType.swallowF,
            0,
            DateTimeOffset.MinValue,
            false,
            DateTimeOffset.MaxValue,
            DateTimeOffset.MinValue,
            DateTimeOffset.MinValue,
            string.Empty);
        MergeLevelManager.Instance.CurrentMapData.ChangeItemData(toGrid, data);
        MergeLevelManager.Instance.CurrentMapData.AddOrRemoveItemData();
        if (selected)
        {
            SetCurrentSelectData(data);
            if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(data.GridPos, out var originGrid))
                ShowWithTween_BoxSelected(originGrid.GridGO.transform.position, null, false, 0.3f);
        }

        //显示
        MergeItem mergeItem = AssetSystem.Instance.SpawnMergeItem(prefabName, mapRoot);
        if (mergeItem != null)
        {
            mergeItem.transform.localScale = Vector3.one;
            mergeItem.transform.position = fromWorldPos;
            mergeItem.gameObject.SetActive(true);
            mergeItem.InitItemInfo(uiMergeBase, this, data);
            mergeItem.PlayTween_CreateByFly(mergeGrid.GridGO.transform.position, deafaultJump);
        }
        else
        {
            GameDebug.LogError("cannot spawnGameObject!" + prefabName);
        }

        uiMergeBase.ShowNewDiscoveryView(prefabName);

        ////刷新订单
        //TaskGoalsManager.Instance.RefreshTask();
        //uiPanel_Merge.RefreshTaskUI();
        //uiPanel_Merge.CheckTryStartTutorial();
    }

    /// <summary>
    /// 生成一个新的泡泡
    /// </summary>
    /// <param name="prefabName"></param>
    /// <param name="inbubbleSecond"></param>
    /// <param name="fromWorldPos"></param>
    public void CreateBubbleItem(string prefabName, int inbubbleSecond, Vector3 fromWorldPos)
    {
        if (string.IsNullOrEmpty(prefabName))
        {
            GameDebug.LogError("prefabName is empty！");
            return;
        }

        if (!MergeItemDefinition.TotalDefinitionsDict.TryGetValue(prefabName, out var def))
        {
            GameDebug.LogError("Objects表中没找到prefabName！" + prefabName);
            def = null;
            return;
        }

        //数据
        MergeItemData data = new MergeItemData(
            prefabName,
            Vector2Int.zero,
            def,
            true,
            false,
            false,
            TimeManager.Instance.UtcNow().AddSeconds(inbubbleSecond),
            0,
            def.TotalChargeCount,
            DateTimeOffset.MaxValue,
            0,
            0,
            DateTimeOffset.MaxValue,
            false,
            DateTimeOffset.MinValue,
            0,
            0,
            0,
            false,
            0,
            DateTimeOffset.MinValue,
            false,
            DateTimeOffset.MaxValue,
            DateTimeOffset.MinValue,
            DateTimeOffset.MinValue,
            string.Empty);

        if (data != null)
        {
            MergeLevelManager.Instance.CurrentMapData.AddDataToBubbleList(data);
            //显示
            BubbleItem bubbleItem = AssetSystem.Instance.SpawnBubbleItem(bubbleRoot);
            if (bubbleItem != null)
            {
                AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Produce_Bubble);
                bubbleItem.gameObject.SetActive(true);
                bubbleItem.transform.position = fromWorldPos;
                bubbleItem.InitWithData(data, this);
            }
            else
                GameDebug.LogError("cannot spawnBubbleItem!");
        }
    }

    /// <summary>
    /// 根据本地数据生成泡泡
    /// </summary>
    /// <param name="bubble"></param>
    /// <returns>是否已结束生命周期</returns>
    public bool CreateBubbleItemByLocalData(MergeItemData bubble)
    {
        bool isOutTime = false;
        if (bubble != null)
        {
            if (ExtensionTool.IsDateSmallerThanNow(bubble.bubbleDieTime))
            {
                if (TryGetNearEmptyGrid(Vector2Int.one, out var emptyPos, false))
                {
                    CreateItem_InGridDirectly(bubble.Definition.BubbleDieOutputPrefab, emptyPos, false);
                }
                else
                {
                    var mergeRewardItem = new MergeRewardItem
                    {
                        name = bubble.Definition.BubbleDieOutputPrefab,
                        num = 1
                    };
                    RewardBoxManager.Instance.AddRewardItem(MergeLevelManager.Instance.CurrentLevelType, mergeRewardItem, false);
                }
                isOutTime = true;
            }
            else
            {
                //显示
                BubbleItem bubbleItem = AssetSystem.Instance.SpawnBubbleItem(bubbleRoot);
                if (bubbleItem != null)
                {
                    bubbleItem.gameObject.SetActive(true);
                    bubbleItem.transform.position = bubbleRoot.transform.position;
                    bubbleItem.InitWithData(bubble, this);
                }
                else
                    GameDebug.LogError("cannot spawnBubbleItem!");
            }
        }
        else
            GameDebug.LogError("LocalBubbleDataErr!");
        return isOutTime;
    }

    /// <summary>
    /// 从仓库UI中返回地图
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="itemData"></param>
    /// <param name="flyToTarget"></param>
    /// <param name="startFlyWorldPos"></param>
    public void StorePackUIReturnMap(Vector2Int grid, MergeItemData itemData)
    {
        if (itemData == null)
        {
            return;
        }

        if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(grid, out MergeGridData mergeGridData))
        {
            if (!MergeLevelManager.Instance.CurrentMapData.itemDataDict.ContainsKey(grid))
            {
                //移除背包数据
                MergeLevelManager.Instance.CurrentMapData.RemoveStorePackList(itemData);

                //添加到场景数据
                MergeLevelManager.Instance.CurrentMapData.ChangeItemData(grid, itemData);

                //显示
                MergeItem mergeItem = AssetSystem.Instance.SpawnMergeItem(itemData.PrefabName, mapRoot);
                mergeItem.transform.position = mergeGridData.GridGO.transform.position;
                mergeItem.gameObject.SetActive(true);
                mergeItem.InitItemInfo(uiMergeBase, this, itemData);
                mergeItem.PlayTween_CreateByScale();

                //刷新底部滑动背包
                uiMergeBase.RefreshBottomStore();
                //刷新提示信息
                SetCurrentSelectData(itemData);
                //选中返回场景中的物体
                ShowWithTween_BoxSelected(mergeItem.transform.position, null, false, 0.15f);
                if (!itemData.ItemGO.gameObject.activeSelf)
                    itemData.ItemGO.gameObject.SetActive(true);
                //播放从背包返回合成界面粒子特效
                mergeItem.RefreshBagToMapEffet();
            }
        }
    }
    /// <summary>
    /// 点击底部背包物品返回棋盘
    /// </summary>
    /// <param name="prefabName"></param>
    /// <param name="endWorldPos"></param>
    /// <param name="targetGridPos"></param>
    public void CreateItem_ByClickStoreItem(MergeItemData mergeItemData, Vector3 endWorldPos, Vector2Int targetGridPos, bool showDoTween = true)
    {
        if (string.IsNullOrEmpty(mergeItemData.PrefabName))
        {
            GameDebug.LogError("prefabName is empty！");
            return;
        }

        if (!MergeItemDefinition.TotalDefinitionsDict.TryGetValue(mergeItemData.PrefabName, out var def))
        {
            GameDebug.LogError("Objects表中没找到prefabName！" + mergeItemData.PrefabName);
            return;
        }

        if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(targetGridPos, out var gridData))
        {
            //数据
            var chargeFinishDate = DateTimeOffset.MaxValue;
            if (def.CategoryType == MergeItemCategoryType.wake)
                chargeFinishDate = TimeManager.Instance.UtcNow().AddSeconds(def.ChargeLoopCDSecond);
            MergeLevelManager.Instance.CurrentMapData.ChangeItemData(targetGridPos, mergeItemData);

            MergeItem mergeItem = AssetSystem.Instance.SpawnMergeItem(mergeItemData.PrefabName, mapRoot);
            if (mergeItem != null)
            {
                //mergeItem.transform.SetAsLastSibling();
                mergeItem.transform.position = endWorldPos;
                mergeItem.gameObject.SetActive(true);
                mergeItem.InitItemInfo(uiMergeBase, this, mergeItemData);
                SetCurrentSelectData(mergeItemData);
                mergeItem.transform.SetParent(DragPanelTran);
                //动画
                if (showDoTween)
                {
                    mergeItem.PlayTween_FlyingFromStore(endWorldPos, gridData.GridGO.transform.position, () =>
                    {
                        ShowWithTween_BoxSelected(gridData.GridGO.transform.position, null, false, 0.15f);
                        mergeItem.RefreshBagToMapEffet();
                        RefreshMergeItemByChangeItem();
                    });
                }
                else
                {
                    mergeItem.PlayTween_Flying(endWorldPos, () =>
                     {
                         ShowWithTween_BoxSelected(gridData.GridGO.transform.position, null, false, 0.15f);
                         mergeItem.RefreshBagToMapEffet();
                         RefreshMergeItemByChangeItem();
                     });
                }
            }
            else
            {
                GameDebug.LogError("cannot spawnGameObject!" + mergeItemData.PrefabName);
            }
        }
    }

    /// <summary>
    /// 关闭mergeUI
    /// </summary>
    public void HideMergeUI()
    {
        if (this.gameObject.activeSelf)
        {
            Page_Play.refreshDailyTaskRedPoint?.Invoke();
            UISystem.Instance.HideUI(uiMergeBase);
        }
    }

    private int index = 0;
    public MergeItem CreateItem_WithData(MergeItemData mergeItemData)
    {
        if (mergeItemData == null || string.IsNullOrEmpty(mergeItemData.PrefabName))
        {
            return null;
        }
        MergeItem mergeItem = AssetSystem.Instance.SpawnMergeItem(mergeItemData.PrefabName, mapRoot);
        if (mergeItem == null)
        {
            return null;
        }

        if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(mergeItemData.GridPos, out var gridData))
        {
            index++;
            //修改位置
            mergeItem.transform.position = gridData.GridGO.transform.position;
            if (!mergeItem.gameObject.activeSelf)
                mergeItem.gameObject.SetActive(true);
            mergeItem.InitItemInfo(uiMergeBase, this, mergeItemData);  
        }                                                                               
        return mergeItem;
    }

    private bool isRevoking = false;//是否处于可撤销出售过程中,如果是则不会自动生成道具
    public bool IsRevoking()
    {
        return isRevoking;
    }

    public void SellSelectedItem()
    {
        if (currentSelectItemData != null)
        {
            isRevoking = true;
            MergeLevelManager.Instance.CurrentMapData.ChangeItemData(currentSelectItemData.GridPos, null);
            MergeLevelManager.Instance.CurrentMapData.AddOrRemoveItemData();
            AssetSystem.Instance.UnspawnMergeItem(currentSelectItemData.ItemGO);
        }
        else
            Debug.LogError("Err:currentSelectItemData is null!");
    }

    public void SetCurrentSelectData(MergeItemData itemData = null)
    {
        isRevoking = false;
        currentSelectItemData = itemData;
        uiMergeBase.RefreshSelectItemInfo();
    }

    private float countDownTime = 0;//提示可合成物品时间间隔
    private List<MergeItemData> itemList;
    private MergeItemData DoTweenItemData;
    private bool hasFound = false;
    private void Update()
    {
        if (!hasItemDragging /*&& !TutorialSystem.Instance.IsInTutorial*/)
        {
            countDownTime += Time.deltaTime;
            if (countDownTime >= 3f)
            {
                countDownTime = 0;
                if (!UI_TutorManager.Instance.IsTutoring() && MergeLevelManager.Instance.CurrentMapData != null)
                {
                    hasFound = false;
                    itemList = MergeLevelManager.Instance.CurrentMapData.itemDataDict.Values.ToList();
                    //List<TaskData> taskList = TaskGoalsManager.Instance.m_TaskList;
                    //List<string> taskPrefabNameList = new List<string>();
                    //for (int i = 0; i < taskList.Count; i++)
                    //{
                    //    for (int j = 0; j < taskList[i].NeedItemData.Count; j++)
                    //    {
                    //        taskPrefabNameList.Add(taskList[i].NeedItemData[j].name);
                    //    }
                    //}
                    int num1 = 0, num2 = 0, level = 0;//4级为封锁道具和非任务道具、3级为非任务道具、2级为封锁道具任务道具、1级为任务道具、0级为合成不了
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (hasFound)
                        {
                            break;
                        }

                        if (itemList[i] == null || itemList[i].Definition == null || !itemList[i].CanMerge())// || taskPrefabNameList.Contains(itemList[i].PrefabName))
                        {
                            continue;
                        }


                        for (int j = i + 1; j < itemList.Count; j++)
                        {
                            if (itemList[j] == null || itemList[j].Definition == null || !itemList[j].CanMerge())// || taskPrefabNameList.Contains(itemList[i].PrefabName)) 
                            {
                                continue;
                            }

                            if (!string.IsNullOrEmpty(itemList[i].PrefabName)
                                && itemList[i].PrefabName == itemList[j].PrefabName)
                            {
                                if (itemList[i].ItemGO == null || itemList[j].ItemGO == null)
                                {
                                    continue;
                                }
                                if (itemList[i].IsLocked && itemList[j].IsLocked)
                                {
                                    continue;
                                }
                                if (level < 4 && (itemList[i].IsLocked || itemList[j].IsLocked) && !(itemList[i].ItemGO.IsTaskItem() || itemList[j].ItemGO.IsTaskItem())/*都为非任务道具*/ )
                                {
                                    level = 4;
                                }
                                else if (level < 3 && !(itemList[i].ItemGO.IsTaskItem() || itemList[j].ItemGO.IsTaskItem())/*都为非任务道具*/)
                                {
                                    level = 3;
                                }
                                else if (level < 2 && (itemList[i].IsLocked || itemList[j].IsLocked))
                                {
                                    level = 2;
                                }
                                else if (level < 1)
                                {
                                    level = 1;
                                }
                                else
                                    continue;
                                num1 = i;
                                num2 = j;
                                /*itemList[i].ItemGO?.PlayTween_CanMerge();
                                itemList[j].ItemGO?.PlayTween_CanMerge();
                                DoTweenItemData = itemList[i];

                                hasFound = true;
                                break;*/
                            }
                        }
                        if (level == 4)
                            break;
                    }
                    if (level != 0 && level != 1)
                    {
                        itemList[num1].ItemGO?.PlayTween_CanMerge();
                        itemList[num2].ItemGO?.PlayTween_CanMerge();
                        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.merge_tips);
                        DoTweenItemData = itemList[num1];
                        //GameDebug.Log("等级为" + level);
                        hasFound = true;
                    }
                }

            }
        }
        else
        {
            if (countDownTime != 0)
                countDownTime = 0;
        }

        commitCountDownTime -= Time.deltaTime;
        if (commitCountDownTime < 0)
        {
            commitCountDownTime += 600;
            TryCommitMergeCount();
            TryCommitProduceCount();
        }
    }

    public void ResetTipCountDown()
    {
        countDownTime = 0;
    }




    #region 选中框动画
    private Sequence selectBoxSequence;

    public void StopTween_BoxSelected()
    {
        if (selectBoxSequence != null)
        {
            selectBoxSequence.Kill(true);
        }
        selectBox.gameObject.SetActive(false);
        selectBox.alpha = 1;
    }


    public void HideWithTween_BoxSelected()
    {
        if (selectBoxSequence != null)
        {
            selectBoxSequence.Kill();
        }

        selectBoxSequence = DOTween.Sequence();
        selectBoxSequence.Append(DOTween.To(setter: value =>
        {
            selectBox.alpha = value;
        }, startValue: 1, endValue: 0, duration: 0.08f).SetEase(Ease.OutQuad).SetDelay(0.05f));
        selectBoxSequence.onComplete = () =>
        {
            selectBox.gameObject.SetActive(false);
            selectBox.alpha = 1;
        };
        selectBoxSequence.onKill = () =>
        {
            selectBox.alpha = 1;
        };
    }

    // 展示选中框动画
    public void ShowWithTween_BoxSelected(Vector3 worldPos, Action completeCallBack = null, bool needFirstShrink = false, float delay = 0)
    {
        selectBox.transform.position = worldPos;
        selectBox.gameObject.SetActive(true);

        if (selectBoxSequence != null)
        {
            selectBoxSequence.Kill();
        }

        selectBoxSequence = DOTween.Sequence();
        if (delay > 0)
        {
            selectBoxSequence.AppendInterval(delay);
        }
        if (needFirstShrink)
        {
            selectBoxSequence.Append(selectBox.transform.DOScale(Vector3.one * 0.8f, 0.12f).SetEase(Ease.OutQuad));
            selectBoxSequence.Append(selectBox.transform.DOScale(Vector3.one * 1.1f, 0.23f).SetEase(Ease.OutQuad));
            selectBoxSequence.Append(selectBox.transform.DOScale(Vector3.one, 0.15f).SetEase(Ease.OutQuad));
            selectBoxSequence.Insert(0, DOTween.To(setter: value =>
            {
                selectBox.alpha = value;
            }, startValue: 1, endValue: 0, duration: 0.05f).SetEase(Ease.OutQuad));
            selectBoxSequence.Insert(0.2f, DOTween.To(setter: value =>
            {
                selectBox.alpha = value;
            }, startValue: 0, endValue: 1, duration: 0.05f).SetEase(Ease.OutQuad));
        }
        else
        {
            selectBoxSequence.Append(selectBox.transform.DOScale(Vector3.one * 1.1f, 0.18f).SetEase(Ease.OutQuad));
            selectBoxSequence.Append(selectBox.transform.DOScale(Vector3.one, 0.18f).SetEase(Ease.OutQuad));
        }
        selectBoxSequence.Play();
        selectBoxSequence.onComplete = () =>
        {
            selectBox.transform.localScale = Vector3.one;
            selectBox.alpha = 1;
            completeCallBack?.Invoke();
        };
        selectBoxSequence.onKill = () =>
        {
            selectBox.alpha = 1;
            selectBox.transform.localScale = Vector3.one;
        };
    }

    #endregion

    public void FixSellItem()
    {
        uiMergeBase.FixSellItem();
    }
    public void RefreshRewardBox()
    {
        uiMergeBase.RefreshRewardBox();
    }
    public void DestroyTaskItemById(string taskId)
    {
        uiMergeBase.DestroyTaskItemById(taskId);
    }
    public void SetNeedleNum()
    {
        uiMergeBase.SetNeedleNum();
    }
    public void RefreshBottomStore()
    {
        uiMergeBase.RefreshBottomStore();
    }
    public void RefreshSelectItemInfo()
    {
        uiMergeBase.RefreshSelectItemInfo();
    }
    public void TryTweenTaskBack()
    {
        uiMergeBase.TryTweenTaskBack();
    }
    /// <summary>
    /// 判断是否移动到可合成物体上方，是则播放可合成特效
    /// </summary>
    public void MoveAboveMergeItem(MergeItem draggingItem)
    {
        if (!draggingItem.ItemData.CanMerge())
        {
            return;
        }

        if (ConvertLocalPositionToGrid(draggingItem.gameObject.transform.position, out Vector2Int endgridPos))
        {
            if (MergeLevelManager.Instance.CurrentMapData.itemDataDict.TryGetValue(endgridPos, out var endItemData))
            {
                if (draggingItem.ItemData != endItemData && endItemData.CanMerge() && draggingItem.ItemData.PrefabName == endItemData.PrefabName)
                {
                    RefreshMergeAboveEffect(true, endItemData.ItemGO.transform.position);
                }
                else
                {
                    RefreshMergeAboveEffect(false, endItemData.ItemGO.transform.position);
                }
            }
            else
            {
                RefreshMergeAboveEffect(false, Vector3.zero);
            }
        }
        else
        {
            RefreshMergeAboveEffect(false, Vector3.zero);
        }
    }
    public void MoveAboveMergeItem(StoreItem draggingItem, Vector2Int endGridPos)
    {
        if (draggingItem == null || endGridPos == null)
        {
            /*if(draggingItem != null)
            {
                draggingItem.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
            }*/
            return;
        }
        //在棋盘上
        if (MergeLevelManager.Instance.CurrentMapData.gridDataDict.TryGetValue(endGridPos, out var endGridData))
        {
            if (endGridData == null || endGridData.GridGO == null)
            {
                GameDebug.LogError("[MergeLogic] 目标位置的Grid物体或GameObject为空");
                return;
            }
            if (MergeLevelManager.Instance.CurrentMapData.itemDataDict.TryGetValue(endGridPos, out var endItemData))
            {

                if (endItemData.IsInBox)
                {
                    RefreshMergeAboveEffect(false, Vector3.zero);
                    //draggingItem.ReturnStartPos();
                    return;
                }
                if (draggingItem.mergeItemData.PrefabName == endItemData.PrefabName && endItemData.CanMerge())
                {
                    RefreshMergeAboveEffect(true, endItemData.ItemGO.transform.position);
                }
                else
                {
                    RefreshMergeAboveEffect(false, endItemData.ItemGO.transform.position);
                }
            }
            else
            {
                RefreshMergeAboveEffect(false, Vector3.zero);
            }
        }
        else
        {
            RefreshMergeAboveEffect(false, Vector3.zero);
        }
    }
    public void RefreshMergeAboveEffectClear()
    {
        RefreshMergeAboveEffect(false, Vector3.zero);
    }
    public void RefreshMergeItemByCompleteTask()
    {
        foreach (var item in MergeLevelManager.Instance.CurrentMapData.itemDataDict)
        {
            if (item.Value.ItemGO != null)
            {
                item.Value.ItemGO.RefreshTaskCheckIcon();
            }
        }
        uiMergeBase.RefreshBottomStore();
    }
    public void RefreshMergeItemByChangeItem()  //刷新物品层级
    {
        int itemNum = mapRoot.childCount;
        int MaxLevel = 0;
        for (int i = 0, num = 0; i < 56; i++)  //排序
        {

            for (int j = 0; j < itemNum - num; j++)
            {
                MergeItem item = mapRoot.GetChild(j).GetComponent<MergeItem>();
                int index = item.GetItemSiblingIndexByPos();
                MaxLevel = item.ItemData.Definition.Hierarchy > MaxLevel ? item.ItemData.Definition.Hierarchy : MaxLevel;
                if (index == i)
                {
                    item.SetItemSibling("Last");
                    num++;
                    break;
                }
            }
        }
        //GameDebug.LogError(MaxLevel);
        if (MaxLevel > 0)
        {
            int num = 0;  //更新过的数量
            for (int i = 1; i <= MaxLevel; i++)   //层级
            {
                int lift = itemNum - num;
                for (int j = 0, index = 0; j < lift; j++)
                {
                    MergeItem item = mapRoot.GetChild(index).GetComponent<MergeItem>();
                    if (item.ItemData.Definition.Hierarchy == i)
                    {
                        item.SetItemSibling("Last");
                        num++;
                        continue;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
        }
    }

    private GameObject mergeEffect; //合成特效
    private void PlayMergeEffect(Transform parentTF)
    {
        if (parentTF == null)
            return;

        if (mergeEffect == null)
        {
            mergeEffect = AssetSystem.Instance.Instantiate(Consts.ItemMerge_Effect, transform);
        }

        if (!mergeEffect.activeSelf)
        {
            mergeEffect.SetActive(true);
        }
        mergeEffect.transform.SetParent(parentTF);
        mergeEffect.transform.localPosition = Vector3.zero;
        mergeEffect.transform.localRotation = Quaternion.identity;
        mergeEffect.transform.localScale = Vector3.one;
        mergeEffect.transform.SetAsFirstSibling();
        if (mergeEffect.transform.GetChild(0).TryGetComponent(out ParticleSystem particle))
        {
            particle.Play();
        }
    }

    private ParticleSystem effect_MergeAbove;
    bool isShowAboveEffect = false;
    /// <summary>
    /// 移动到其它物品上方
    /// </summary>
    public void RefreshMergeAboveEffect(bool showEffect, Vector3 endPos)
    {
        if (effect_MergeAbove == null)
        {
            GameObject gO = AssetSystem.Instance.Instantiate("Effect_MergeAboveOther", mergeEffectTran);
            if (gO != null)
                effect_MergeAbove = gO.GetComponent<ParticleSystem>();
        }
        if (effect_MergeAbove == null)
            return;

        effect_MergeAbove.transform.position = endPos;
        if (showEffect)
        {
            if (!isShowAboveEffect)
            {
                isShowAboveEffect = true;
                effect_MergeAbove.Play();
            }
        }
        else
        {
            if (isShowAboveEffect)
            {
                isShowAboveEffect = false;
                effect_MergeAbove.Stop();
                effect_MergeAbove.Clear();
            }
        }
    }

    #region 合成打点
    private int lastMergeNum = 0;
    private int mergeNum = 0;
    private int lastProduceNum = 0;
    private int produceNum = 0;
    private float commitCountDownTime = 600f;//十分钟记录一次合成和产出次数打点
    private void TryCommitMergeCount()
    {
        if (mergeNum - lastMergeNum == 0)
        {
            GameDebug.Log("十分钟内未合成");
            return;
        }
        AnalyticsUtil.recordCoreAction("merge", mergeNum - lastMergeNum);
        lastMergeNum = mergeNum;
    }
    private void TryCommitProduceCount()
    {
        if (produceNum - lastProduceNum == 0)
        {
            GameDebug.Log("十分钟内未产出");
            return;
        }
        AnalyticsUtil.recordCoreAction("click", produceNum - lastProduceNum);
        lastProduceNum = produceNum;
    }
    public void CommitByQuitGame()
    {
        if (mergeNum > lastMergeNum)
        {
            AnalyticsUtil.recordCoreAction("merge", mergeNum - lastMergeNum);
            AnalyticsUtil.commitCoreAction("merge");
            lastMergeNum = mergeNum;
        }
        if (produceNum > lastProduceNum)
        {
            AnalyticsUtil.recordCoreAction("click", produceNum - lastProduceNum);
            AnalyticsUtil.commitCoreAction("click");
            lastProduceNum = produceNum;
        }
    }
    #endregion
}
