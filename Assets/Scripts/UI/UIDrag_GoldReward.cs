using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BatterPassItemData
{
    public int realIndex;
    public int itemHeight;
    public BattlePassRankRewardItem item;
    public Vector3 localPos;
    public Vector2 anchorPos;
}

public class UIDrag_GoldReward : MonoBehaviour
{
    public delegate void RefreshItem(BattlePassRankRewardItem item, int itemIndex);
    public RefreshItem onRefreshItem;
    public delegate BattlePassRankRewardItem CreateItemFunction(string path, Transform parent);
    public CreateItemFunction createItem;
    public delegate void CreateItemFunctionAsync(string path, Transform parent, Action<BattlePassRankRewardItem> callBack);
    public CreateItemFunctionAsync createItemAsync;

    public delegate void DestoryItemFunction(string path, BattlePassRankRewardItem item);
    public DestoryItemFunction destoryItem;

    public BatterPassItemData[] itemArray;


    protected Transform mTrans;
    protected RectTransform mPanel;
    protected ScrollRect mScrollRect;

    public virtual void Init(GameObject scrollView, BatterPassItemData[] itemArray, CreateItemFunction createItem, CreateItemFunctionAsync createItemAsync, DestoryItemFunction destoryItem, RefreshItem onRefreshItem)
    {
        mTrans = transform;
        mPanel = scrollView.GetComponent<RectTransform>();
        mScrollRect = scrollView.GetComponent<ScrollRect>();
        this.itemArray = itemArray;
        this.createItem = createItem;
        this.createItemAsync = createItemAsync;
        this.destoryItem = destoryItem;
        this.onRefreshItem = onRefreshItem;

        mScrollRect.onValueChanged.AddListener(WrapContent);
    }

    public void SpringToItem(int itemIndex, float offset)
    {
        if (mTrans == null)
            return;
        mTrans.transform.localPosition = new Vector3(mTrans.transform.localPosition.x, itemIndex * offset);
    }

    public void WrapContent(Vector2 vect2)
    {
        float extents = 1200;
        Vector3[] corners = new Vector3[4];
        mPanel.GetWorldCorners(corners);

        for (int i = 0; i < 4; ++i)
        {
            Vector3 v = corners[i];
            v = mTrans.InverseTransformPoint(v);
            corners[i] = v;
        }
        Vector3 center = Vector3.Lerp(corners[0], corners[2], 0.5f);

        for (int i = 0; i < itemArray.Length; ++i)
        {
            BatterPassItemData itemData = itemArray[i];
            float distance = Mathf.Abs(itemData.localPos.y - center.y);
            if (distance <= extents)
            {
                if (itemData.item == null)
                {
                    itemData.item = createItem?.Invoke(Consts.UI_BattlePassRankRewardItem, mTrans);
                    itemData.item.transform.localPosition = itemData.localPos;                    
                }
                UpdateItem(itemData.item, i);
            }
            else
            {
                if (itemData.item != null)
                {
                    destoryItem?.Invoke(Consts.UI_BattlePassRankRewardItem, itemData.item);
                    itemData.item = null;
                }
            }
        }
    }

    private void UpdateItem(BattlePassRankRewardItem item, int index)
    {
        onRefreshItem?.Invoke(item, index);
    }

}
