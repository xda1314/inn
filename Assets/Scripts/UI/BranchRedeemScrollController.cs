using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class BranchRedeemScrollController : MonoBehaviour, IEnhancedScrollerDelegate
{
    private SmallList<BranchRewardDefinition> _data;

    [SerializeField] private EnhancedScroller scroller;

    [SerializeField] private EnhancedScrollerCellView cellViewPrefab;

    [SerializeField] private int preloadStart = 0;//起始处多预加载几个cell
    [SerializeField] private int preloadEnd = 0;//结尾处多预加载几个cell

    void Start()
    {
        scroller.Delegate = this;
    }

    private MergeLevelType curLevelType;
    public void SetData(SmallList<BranchRewardDefinition> data, MergeLevelType levelType)
    {
        _data = data;
        curLevelType = levelType;
        scroller.ReloadData();

    }

    public void JumpToCurrentData(int scroll_index = 0)
    {
        scroller.JumpToDataIndex(scroll_index, 0, 0, true, EnhancedScroller.TweenType.easeOutSine, 0.25f);
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        if (_data != null)
            return _data.Count;
        return 0;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 245f;
    }

    public int GetPreloadStart()
    {
        return preloadStart;
    }

    public int GetPreloadEnd()
    {
        return preloadEnd;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        BranchRedeemCell cellView = scroller.GetCellView(cellViewPrefab) as BranchRedeemCell;
        cellView.name = $"Cell_{dataIndex}";
        BranchRewardDefinition defPre = null;
        BranchRewardDefinition defNext = null;
        if (dataIndex - 1 >= 0)
            defPre = _data[dataIndex - 1];
        if (dataIndex + 1 < _data.Count)
            defNext = _data[dataIndex + 1];
        cellView.SetData(_data[dataIndex], defPre, defNext, curLevelType);
        return cellView;
    }
}
