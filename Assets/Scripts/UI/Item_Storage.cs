using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Item_Storage : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private UIPanel_Store storePanel;
    private MergeItemData itemData;
    private Item_Storage storeItem;
    [HideInInspector] public int curIndex { get; private set; } = -1;
    /// <summary>
    /// 刷新物品
    /// </summary>
    /// <param name="data"></param>
    /// <param name="go"></param>
    public void RefreshItem(UIPanel_Store panel, MergeItemData data, Item_Storage storeItem)
    {
        storePanel = panel;
        itemData = data;
        this.storeItem = storeItem;
    }
    public void SetIndex(int index)
    {
        curIndex = index;
    }


    #region 事件
    private Vector3 startWorldPos;
    private Vector3 _offsetPos = Vector3.zero;
    bool canClick = true;//移动过程中不让点击

    public void OnBeginDrag(PointerEventData eventData)
    {
        canClick = false;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector3 worldPos))
        {
            _offsetPos = transform.position - worldPos;
            startWorldPos = transform.position;
            storePanel.SetParent(transform, true);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector3 worldPos))
        {
            transform.position = worldPos + _offsetPos;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector3 worldPos))
        {
            int targetIndex = storePanel.TryReturnIndexByGrid(worldPos);
            //GameDebug.LogError(curIndex + "     " + targetIndex);
            if (targetIndex == -1)
            {
                //复位
                ReturnStartPos();
            }
            else 
            {
                if (targetIndex < storePanel.storeItemList.Count)
                {
                    //交换
                    storePanel.SetParent(transform, false);
                    storePanel.ExchangeTwoItem(storePanel.storeItemList[curIndex], storePanel.storeItemList[targetIndex]);                                     
                }
                else 
                {
                    //移到末尾
                    storePanel.MoveToEnd(this);
                }
            }
        }
        canClick = true;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.dragging || !canClick)
            return;

        //返回场景中，并存入数据
        if (storePanel.TrySetItemToMap(itemData))
        {
            //回收背包中的物品
            Sequence sequence = DOTween.Sequence();
            sequence.Append(storeItem.gameObject.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack));
            sequence.onComplete += (() =>
            {
                AssetSystem.Instance.DestoryGameObject(itemData.PrefabName, storeItem.gameObject);
                storePanel.TweenDeleteItem(curIndex);
                storePanel.storeItemList.Remove(storeItem);
            });
        }
        else
        {
            //格子满了
            GameDebug.Log("格子已经满了");
        }
    }
    #endregion
    private void ReturnStartPos()
    {    
        transform.DOMove(startWorldPos, 0.2f).onComplete+=()=> 
        {
            storePanel.SetParent(transform, false);
            storePanel.SetIndex(this);
        };      
    }
}
