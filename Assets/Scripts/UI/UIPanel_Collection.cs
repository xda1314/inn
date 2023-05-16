using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectionFrameData
{
    public KeyValuePair<string, MergeItemChain> chain;
    public int id;
    public CollectionsFrame item;
    public string _frameTile;
    public int itemTotalCount;
    public Vector3 localPos;
    public int height;
}

public class CollectionItemData
{
    public int ID;
    public CollectionFrameData frameData;
    public MergeItemDefinition itemData;
    public UIPanel_Collection uipanel_Collection;
}

public class UIPanel_Collection : UIPanelBase
{
    [Header("已发现物品收集界面")]
    [SerializeField] private GameObject propRoot;
    [SerializeField] public RectTransform propScrollRect;
    [SerializeField] private UIDrag_Collection wrapContent;

    private CollectionFrameData[] frameDataArray;
    public static Dictionary<string, CollectionFrameData> claimedItemDict { get; private set; } = new Dictionary<string, CollectionFrameData>();
    private Dictionary<string, CollectionsFrame> frameData = new Dictionary<string, CollectionsFrame>();
    private static Vector2[] posArray;
    private static int[] heightArray;
    private const int frameLineDefaultHeight = 480; //框架Item的默认高度（只有一行时的高度）
    private const int frameLineHeight = 240;        //框架每行的高度（大于一行时的高度）
    public const int frameSpace = 60;              //每个框架之间的间隙高度
    private float StartPosY;
    public static event Action RefreshRedPointState;

    public override void OnInitUI()
    {
        base.OnInitUI();
        InitCollectionData();
        UIPanel_Merge.onEnterAndRefreshRedPointState += () =>
        {
            AddClaimedFrame();
            RefreshRedPointState?.Invoke();
        };      
        InitView();
        InitFrameData();
        CreateCollectionsFrame();
        onPageCollectionEnter();      
    }


    //用于图鉴
    private Dictionary<string, MergeItemChain> NeedJoinCollectionDict = new Dictionary<string, MergeItemChain>();
    private void InitCollectionData()
    {
        foreach (var chain in MergeItemChain.TotalChainsDict)
        {
            if (chain.Value.Chain[0].NeedJoinCollection)
                NeedJoinCollectionDict.Add(chain.Key, chain.Value);
        }
    }

    private void InitView()
    {
        propRoot.SetActive(true);

    }
    public void RefreshLanguageText()
    {
        wrapContent.RefreshCurrentContent();
    }
    #region Prop
    /// <summary>
    /// 通过框架刷新Item
    /// </summary>
    public void RefreshItemByFrame()
    {
        foreach (var item in frameData)
        {
            if (item.Value != null)
            {
                item.Value.RefreshItemState();
            }
        }
    }

    /// <summary>
    ///创建收藏页面框架并且创建Item
    /// </summary>
    public void CreateCollectionsFrame()
    {
        Dictionary<string, MergeItemChain> chainDict = NeedJoinCollectionDict;
        if (chainDict == null)
            return;
        int arrayIndex = 0;
        float startPosY = 0;
        CollectionFrameData[] frameArray = new CollectionFrameData[chainDict.Count];


        foreach (var chain in chainDict)
        {
            CollectionFrameData itemData = new CollectionFrameData
            {
                chain = chain,
                _frameTile = chain.Value.Chain[0].ChainName,
                itemTotalCount = chain.Value.ChainSize,
                height = heightArray[arrayIndex],
                localPos = posArray[arrayIndex],
                id = arrayIndex
            };
            frameArray[arrayIndex] = itemData;
            startPosY = itemData.localPos.y - itemData.height;
            arrayIndex++;
        }
        StartPosY = startPosY;
        frameDataArray = frameArray;
        wrapContent.Init(propScrollRect.gameObject, frameArray, CreateItem, RefreshItem, DestroyItem);
        RectTransform rect = wrapContent.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, Mathf.Abs(StartPosY) - 300);
        wrapContent.SpringToItem(0, false);
    }

    //初始化框架高度、位置数据
    private void InitFrameData()
    {
        Dictionary<string, MergeItemChain> chainDict = NeedJoinCollectionDict;

        posArray = new Vector2[chainDict.Count];
        heightArray = new int[chainDict.Count];

        var arrays = chainDict.ToArray();
        for (int i = 0; i < chainDict.Count; i++)
        {
            heightArray[i] = CalculateHeightByItemCount(arrays[i].Value.ChainSize);
            posArray[i] = CalculatePositionByIndex(i);
        }
    }

    /// <summary>
    /// 根据高度计算框架位置
    /// </summary>
    /// <param name="currentIndex">当前框架索引</param>
    /// <returns></returns>
    private Vector2 CalculatePositionByIndex(int currentIndex)
    {
        int posY = 60;
        for (int i = 0; i < currentIndex; i++)
        {
            posY += heightArray[i];
        }
        
        float width = 540f;
        if (UISystem.Instance.uiMainMenu != null) 
        {
            RectTransform rectMain = UISystem.Instance.uiMainMenu.GetComponent<RectTransform>();
            width = rectMain.rect.width / 2;
        }
        
        return new Vector2(width, -(posY + currentIndex * frameSpace + heightArray[currentIndex] / 2));
    }

    /// <summary>
    /// 根据Item总数计算框架高度
    /// </summary>
    /// <param name="totalCount">Item总数</param>
    /// <returns></returns>
    private int CalculateHeightByItemCount(int totalCount)
    {
        int rows = CalculateRowsByItemCount(totalCount);
        return (frameLineDefaultHeight + (rows - 1) * frameLineHeight);
    }

    /// <summary>
    /// 根据Item总数计算框架行数
    /// </summary>
    /// <param name="totalCount">Item总数</param>
    /// <returns></returns>
    private int CalculateRowsByItemCount(int totalCount)
    {
        int rows = 1;
        int temp = 0;
        for (int i = 0; i < totalCount; i++)
        {
            if (temp >= 4)
            {
                rows++;
                temp -= 4;
            }
            temp++;
        }
        return rows;
    }

    /// <summary>
    /// 创建框架
    /// </summary>
    /// <param name="path"></param>
    /// <param name="tran"></param>
    /// <returns></returns>
    private CollectionsFrame CreateItem(string path, Transform tran)
    {
        GameObject gO = AssetSystem.Instance.Instantiate(Consts.UI_CollectionFrame, wrapContent.transform);
        if (gO == null)
            return null;
        CollectionsFrame frame = gO.GetComponent<CollectionsFrame>();
        return frame;
    }

    /// <summary>
    /// 创建框架下的Item
    /// </summary>
    /// <param name="item"></param>
    /// <param name="index"></param>
    private void RefreshItem(CollectionsFrame item, int index)
    {
        //创建Item，设置信息
        item.SetupFrameInformation(this, frameDataArray[index]);
        item.CreateItems();
        item.RefreshItemState();
        if (!frameData.ContainsKey(item.GetFrameName()))
        {
            frameData.Add(item.GetFrameName(), item);
        }
    }

    private void DestroyItem(string path, CollectionsFrame item)
    {
        AssetSystem.Instance.DestoryGameObject(Consts.UI_CollectionFrame, item.gameObject);
    }

    /// <summary>
    /// 获取下一个可领取的框架索引
    /// </summary>
    /// <returns></returns>
    private int GetNextClaimableIndex()
    {
        int index = claimedItemDict.Count;
        if (index == 0)
            return 1;
        var listData = claimedItemDict.Values.OrderBy(p => p.id).ToList();
        return listData[0].id;
    }

    private void AddClaimedFrame()
    {
        if (frameDataArray == null || frameDataArray.Length == 0)
            return;

        foreach (var frame in frameDataArray)
        {
            if (frame == null)
                continue;
            foreach (var item in frame.chain.Value.Chain)
            {
                if (item == null)
                    continue;
                if (item.m_discoveryState == MergeItemDiscoveryState.Unlock)
                {
                    TryAddClaimedDict(frame._frameTile);
                    continue;
                }
            }
        }
    }

    /// <summary>
    /// 添加可解锁框架
    /// </summary>
    /// <param name="frameName"></param>
    public void TryAddClaimedDict(string frameName)
    {
        if (string.IsNullOrEmpty(frameName))
            return;
        if (claimedItemDict.ContainsKey(frameName))
            return;
        if (frameDataArray.Length < 0)
            return;
        for (int i = 0; i < frameDataArray.Length; i++)
        {
            if (frameDataArray[i]._frameTile.CompareTo(frameName) == 0)
            {
                claimedItemDict.Add(frameName, frameDataArray[i]);
            }
        }
        RefreshRedPointState?.Invoke();
    }

    /// <summary>
    /// 移除可解锁框架
    /// </summary>
    /// <param name="frameName"></param>
    public void TryRemoveFrameDict(string frameName)
    {
        if (string.IsNullOrEmpty(frameName))
            return;

        if (!claimedItemDict.ContainsKey(frameName))
            return;
        string targetFrame = null;
        foreach (var item in claimedItemDict)
        {
            if (item.Key.CompareTo(frameName) == 0)
                targetFrame = frameName;
        }
        if (!string.IsNullOrEmpty(targetFrame))
        {
            claimedItemDict.Remove(targetFrame);
            SliderToFrame();
        }
    }

    /// <summary>
    /// 滑动到可领取框架位置
    /// </summary>
    public void SliderToFrame(bool act = true)
    {
        if (claimedItemDict != null && wrapContent != null)
        {
            if (claimedItemDict.Count > 0)
            {
                claimedItemDict.OrderBy(p => p.Key).ToDictionary(p => p.Key, o => o.Value);
                int index = GetNextClaimableIndex();
                wrapContent.SpringToItem(index, act);
            }
        }
    }

    public void onPageCollectionEnter()
    {
        if (claimedItemDict != null && wrapContent != null)
        {
            AddClaimedFrame();
            RefreshItemByFrame();
           
            if (claimedItemDict.Count > 0)
            {
                Dictionary<string, CollectionFrameData> sortDict = claimedItemDict.OrderBy(p => p.Key).ToDictionary(p => p.Key, o => o.Value);
                int index = GetNextClaimableIndex();
                wrapContent.SpringToItem(index, false);
            }
            else
                wrapContent.SpringToItem(0, false);
        }
    }
    /// <summary>
    /// 在本界面领取发现物品奖励时刷新
    /// </summary>
    public void RefreshRedPoint() 
    {
        RefreshRedPointState?.Invoke();
    }
    #endregion

}
