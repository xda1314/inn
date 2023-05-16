using UnityEngine;

public abstract class UIMergeBase : UIPanelBase
{
    public MergeController mergeController;

    abstract public void RefreshSelectItemInfo();
    abstract public void FixSellItem();
    abstract public void RefreshRewardBox();
    abstract public void RefreshStoreEffect(bool show);
    abstract public void RefreshBottomStore(int index = -1);//index 用于排序
    abstract public void ShowNewDiscoveryView(string prefabName);
    abstract public void DestroyTaskItemById(string taskId);
    abstract public void SetNeedleNum();
    abstract public void TryTweenTaskBack();
}
