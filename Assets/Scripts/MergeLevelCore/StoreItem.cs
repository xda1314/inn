using DG.Tweening;
using IvyCore;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public MergeItemData mergeItemData;


    private int curIndex = -1;//当前item在scollview中的顺序
    private ScrollRect scrollRect;
    private Transform parentTrans;
    private bool hasSetParent = false;//是否已设置父物体为DragPanelTran
    public void InitItem(int index, MergeItemData mergeItemData)
    {
        curIndex = index;
        scrollRect = transform.parent.parent.parent.GetComponent<ScrollRect>();
        parentTrans = transform.parent;
        this.mergeItemData = mergeItemData;

        var itemTrans = transform.GetChild(0);
        if (itemTrans != null)
        {
            CheckTaskIcon(mergeItemData, itemTrans);
            CheckMaxLevelIcon(mergeItemData, itemTrans);
        }
    }

    private List<GameObject> saveObj = new List<GameObject>();
    private void CheckTaskIcon(MergeItemData itemData, Transform parent)
    {
        for (int i = 0; i < saveObj.Count; i++)
        {
            Destroy(saveObj[i]);
        }
        saveObj.Clear();

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
            {
                GameObject gO = AssetSystem.Instance.Instantiate(Consts.ItemState_Task, parent, new Vector3(40, -40, 0), Vector3.zero, Vector3.one);
                saveObj.Add(gO);
            }           
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
                            GameObject gO = AssetSystem.Instance.Instantiate(Consts.ItemState_DailyTask, parent, new Vector3(40, -40, 0), Vector3.zero, Vector3.one);
                            saveObj.Add(gO);
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

    #region 事件系统
    private Vector3 startWorldPos;
    private Vector3 startEventDataPos;
    private int dragCount;
    private Vector3 _offsetPos = Vector3.zero;
    bool moveVertical;
    bool canClick = true;//移动过程中不让点击
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.dragging)
            return;
        if (canClick && MergeController.CurrentController.TryGetNearEmptyGrid(Vector2Int.one, out Vector2Int emptyGrid, scrollRect.transform.position))
        {
            MergeController.CurrentController.CreateItem_ByClickStoreItem(mergeItemData, transform.position, emptyGrid);
            MergeController.CurrentController.RemoveItemFromStore(mergeItemData);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (UI_TutorManager.Instance.IsTutoring() && UI_TutorManager.Instance.IsTutorialClickStoreItem)
            return;
        canClick = false;
        scrollRect.OnBeginDrag(eventData);
        startEventDataPos = eventData.position;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector3 worldPos))
        {
            _offsetPos = transform.position - worldPos;
            startWorldPos = transform.position;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (UI_TutorManager.Instance.IsTutoring() && UI_TutorManager.Instance.IsTutorialClickStoreItem)
            return;
        if (dragCount < 1)
        {
            dragCount++;
            return;
        }
        if (dragCount == 1)
        {
            dragCount++;
            float x = Mathf.Abs(eventData.position.x - startEventDataPos.x);
            float y = Mathf.Abs(eventData.position.y - startEventDataPos.y);
            moveVertical = x < y;
            return;
        }
        if (moveVertical)
        {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector3 worldPos))
            {
                transform.position = worldPos + _offsetPos;
                if (!hasSetParent)
                {
                    transform.SetParent(MergeController.CurrentController.DragPanelTran);
                    hasSetParent = true;
                }
                if (MergeController.CurrentController.ConvertLocalPositionToGrid(worldPos, out Vector2Int endgridPos))
                {
                    MergeController.CurrentController.MoveAboveMergeItem(this, endgridPos);
                    transform.localScale = new Vector3(1.5f, 1.5f, 1f);
                }
                else
                {
                    MergeController.CurrentController.RefreshMergeAboveEffectClear();
                    transform.localScale = Vector3.one;
                }
            }
        }
        else
        {
            scrollRect.OnDrag(eventData);//横向滑动同步事件
        }
        // MergeController.MoveAboveMergeItem(this);

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (UI_TutorManager.Instance.IsTutoring() && UI_TutorManager.Instance.IsTutorialClickStoreItem)
            return;
        scrollRect.OnEndDrag(eventData);
        dragCount = 0;

        if (moveVertical)
        {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector3 worldPos))
            {
                if (MergeController.CurrentController.ConvertLocalPositionToGrid(worldPos, out Vector2Int endgridPos))
                {
                    MergeController.CurrentController.TryMergeItemFromStore(this, endgridPos);
                }
                else
                {
                    //没落在棋盘上， //返回原位
                    ReturnStartPos();
                }
            }
        }
        canClick = true;
    }
    public void ReturnStartPos()
    {
        RectTransform rect = transform.GetComponent<RectTransform>();
        transform.position = startWorldPos;
        transform.SetParent(parentTrans);
        transform.SetSiblingIndex(curIndex);
        transform.localScale = Vector3.zero;
        rect.sizeDelta = new Vector2(0, 150);
        var s = DOTween.Sequence();
        s.Append(transform.DOScale(1f, 0.4f));
        s.Insert(0, rect.DOSizeDelta(new Vector2(120, 150), 0.4f));
        hasSetParent = false;
    }
    #endregion 

}
