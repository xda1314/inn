using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIDrag_Collection : MonoBehaviour
{

    public delegate void RefreshFrame(CollectionsFrame item, int index);
    public RefreshFrame onRefresh;
    public delegate CollectionsFrame CreateFrame(string path, Transform parent);
    public CreateFrame onCreate;
    public delegate void DestoryFrame(string path, CollectionsFrame item);
    public DestoryFrame onDestory;

    public CollectionFrameData[] frameDataArray;

    protected Transform mTrans;
    protected RectTransform mPanel;
    protected ScrollRect mScrollRect;
    float scrollViewHeight;

    public void Init(GameObject scrollView, CollectionFrameData[] array, CreateFrame create, RefreshFrame refresh, DestoryFrame destory)
    {
        mTrans = transform;
        mPanel = scrollView.GetComponent<RectTransform>();
        mScrollRect = scrollView.GetComponent<ScrollRect>();
        frameDataArray = array;
        onCreate = create;
        onRefresh = refresh;
        onDestory = destory;

        scrollViewHeight = mScrollRect.GetComponent<RectTransform>().rect.height;
        mScrollRect.onValueChanged.AddListener(WrapContent);
    }

    public void SpringToItem(int itemIndex, bool act = true)
    {
        if (mTrans == null)
            return;

        float height = 0;     
        float leftheight = 0;
        for (int i = 0; i < frameDataArray.Length; i++)
        {
            if (i < itemIndex)
                height += frameDataArray[i].height + UIPanel_Collection.frameSpace;
            else
                leftheight += frameDataArray[i].height + UIPanel_Collection.frameSpace;
        }
        if (leftheight < scrollViewHeight) 
        {
            height = height + leftheight - scrollViewHeight + UIPanel_Collection.frameSpace;
        }


        if (act)
        {
            mScrollRect.enabled = false;
            float duration = Mathf.Abs((height - mTrans.localPosition.y) / 5000);
            duration = duration < 1f ? 1f : duration;
            Sequence sequence = DOTween.Sequence();
            updateWrapContent = true;
            sequence.Append(mTrans.DOLocalMoveY(height, duration).SetEase(Ease.OutQuad)).AppendCallback(() =>
            {
                updateWrapContent = false;
                updateWrapFrameCount = 0;
                mScrollRect.enabled = true;
            });
        }
        else
        {
            var pos = mTrans.localPosition;
            mTrans.localPosition = new Vector3(pos.x, height, pos.z);
            WrapContent(mScrollRect.normalizedPosition);
        }
    }

    private void WrapContent(Vector2 vect)
    {
        float extents = 3000;
        Vector3[] corners = new Vector3[4];
        mPanel.GetWorldCorners(corners);

        for (int i = 0; i < 4; ++i)
        {
            Vector3 v = corners[i];
            v = mTrans.InverseTransformPoint(v);
            corners[i] = v;
        }
        Vector3 center = Vector3.Lerp(corners[0], corners[2], 0.5f);
        for (int i = 0; i < frameDataArray.Length; ++i)
        {
            CollectionFrameData itemData = frameDataArray[i];
            float distance = Mathf.Abs(itemData.localPos.y - center.y);
            if (distance <= extents)
            {
                if (itemData.item == null)
                {
                    itemData.item = onCreate?.Invoke(Consts.UI_CollectionFrame, mTrans);
                    itemData.item.transform.localPosition = itemData.localPos;
                    UpdateItem(itemData.item, i);
                }
            }
            else
            {
                if (itemData.item != null)
                {
                    onDestory?.Invoke(Consts.UI_CollectionFrame, itemData.item);
                    itemData.item = null;
                }
            }

        }
    }
    private void UpdateItem(CollectionsFrame item, int index)
    {
        onRefresh?.Invoke(item, index);
    }

    private bool updateWrapContent = false;
    private int updateWrapFrameCount = 0;
    private void Update()
    {
        if (updateWrapContent)
        {
            updateWrapFrameCount += 1;
            if (updateWrapFrameCount > 1)
            {
                updateWrapFrameCount = 0;
                WrapContent(mScrollRect.normalizedPosition);
            }
        }
    }

    public void RefreshCurrentContent()
    {
        for (int i = 0; i < frameDataArray.Length; ++i)
        {
            CollectionFrameData itemData = frameDataArray[i];
            if (itemData.item != null)
                itemData.item.RefreshLaguageText();
        }
    }
}
