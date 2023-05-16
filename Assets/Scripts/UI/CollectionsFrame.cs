using I2.Loc;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectionsFrame : MonoBehaviour
{
    [SerializeField] private GameObject fullDiscoveredIcon; //全解锁显示图标
    [SerializeField] private TextMeshProUGUI frameTitle;    //框架标题
    [SerializeField] private RectTransform frameBackGround;  //框架背景
    [SerializeField] private TextMeshProUGUI discoveredProgress;    //框架底部文字
    [SerializeField] private Transform itemParentTF;

    private List<CollectionItems> collectionItemList = new List<CollectionItems>();   //当前框架下所有Item
    private UIPanel_Collection collectionsManager;
    private CollectionFrameData frameData;
    private readonly Vector3 spawnPosition = new Vector3(-354, 70);    //Item初始位置;
    private const int itemXOffset = 235, itemYOffset = 240;   //X,Y偏移量
    private const int eachLineMaxCount = 4;    //每行Item的最大数量
    private int itemTotalCount;//当前框架的Item总数
    private int currentUnlockCount;
    [HideInInspector] public string _frameTile;//框架名
    private string itemName = null;
    public Action itemClaimAC;

    private void Awake()
    {
        fullDiscoveredIcon.SetActive(false);
        itemClaimAC += ItemCalimed;
    }

    /// <summary>
    /// 计算框架的位置，高度
    /// </summary>
    public void SetupFrameInformation(UIPanel_Collection collectionsManager, CollectionFrameData frameData)
    {
        if (collectionsManager == null || frameData == null)
            return;

        this.collectionsManager = collectionsManager;
        this.frameData = frameData;
        itemTotalCount = frameData.itemTotalCount;
        _frameTile = frameData._frameTile;
        frameBackGround.sizeDelta = new Vector2(frameBackGround.sizeDelta.x, frameData.height);
        transform.localPosition = frameData.localPos;

        discoveredProgress.text = ScriptLocalization.Get("Obj/Main/Illustration/Text2") + ": " + currentUnlockCount.ToString("0") + "/" + itemTotalCount.ToString("0");
    }

    /// <summary>
    /// 创建Item
    /// </summary>
    public void CreateItems()
    {
        if (frameData == null)
            return;

        if (!string.IsNullOrEmpty(itemName) && itemName.CompareTo(frameData.chain.Value.Chain[0].PrefabName) == 0)
        {
            return;
        }

        if (frameData.itemTotalCount != 0)
        {
            collectionItemList.Clear();
            for (int i = 0; i < frameData.itemTotalCount; i++)
            {
                GameObject item = AssetSystem.Instance.Instantiate(Consts.UI_CollectionItem, itemParentTF);
                if (item.TryGetComponent(out CollectionItems component))
                {
                    component.SetupItemArrowActivity(i, frameData.itemTotalCount);
                    collectionItemList.Add(component);
                }
            }
            CalculateItemPositionByCount(frameData.itemTotalCount);
            SetupItemInformation(collectionItemList);
        }
    }

    public string GetFrameName()
    {
        return _frameTile;
    }

    /// <summary>
    /// 初始化设置每个Item属性
    /// </summary>
    /// <param name="chain">读取Item的配置信息</param>
    /// <param name="chainSize">Item的数量</param>
    /// <param name="itemList">已生成Item的列表</param>
    private void SetupItemInformation(List<CollectionItems> itemList)
    {
        if (itemList == null)
            return;
        if (frameData == null)
            return;

        for (int i = 0; i < frameData.itemTotalCount; i++)
        {
            if (frameData.chain.Value.Chain[i] != null && itemList != null)
            {
                //设置Item基本信息
                MergeItemDefinition data = frameData.chain.Value.Chain[i];
                CollectionItemData itemData = new CollectionItemData
                {
                    ID = i,
                    frameData = frameData,
                    itemData = data,
                    uipanel_Collection = collectionsManager,
                };
                itemList[i].SetItemData(itemData);
            }
        }
        itemName = frameData.chain.Value.Chain[0].PrefabName;
    }

    /// <summary>
    /// 根据数量计算Item位置
    /// </summary>
    /// <param name="itemCount">框架下Item数量</param>
    private void CalculateItemPositionByCount(int itemCount)
    {
        int rows = 0;
        int temp = 0;
        for (int i = 0; i < itemCount; i++)
        {
            if (temp >= eachLineMaxCount)
            {
                rows++;
                temp -= eachLineMaxCount;
            }

            collectionItemList[i].transform.localPosition += new Vector3(spawnPosition.x + itemXOffset * temp, spawnPosition.y - itemYOffset * rows);
            temp++;
        }
    }

    private void ItemCalimed()
    {
        currentUnlockCount++;
        RefreshFrameText();
        collectionsManager.RefreshRedPoint();
    }

    private void TryAddClaimedFrame(string frameName)
    {
        collectionsManager.TryAddClaimedDict(frameName);
    }

    public void TryRemoveClaimedDict()
    {
        if (collectionItemList.Count <= 0 || collectionItemList == null)
        {
            return;
        }
        for (int i = 0; i < collectionItemList.Count; i++)
        {
            if (collectionItemList[i] == null)
                continue;
            if (collectionItemList[i].DiscoveryState == MergeItemDiscoveryState.Unlock)
            {
                return;
            }
        }
        if (frameData != null)
            collectionsManager.TryRemoveFrameDict(frameData._frameTile);
    }

    public void RefreshItemState()
    {
        MergeItemChain chainData = frameData.chain.Value;
        if (chainData == null)
            return;
        int unlock = 0;
        for (int i = 0; i < frameData.chain.Value.ChainSize; i++)
        {
            foreach (var item in collectionItemList)
            {
                if (item.ItemName.CompareTo(chainData.Chain[i].PrefabName) == 0)
                {
                    item.RefreshItemState(chainData.Chain[i].m_discoveryState);
                }
            }

            if (chainData.Chain[i].m_discoveryState == MergeItemDiscoveryState.Discovered)
            {
                unlock++;
            }
            if (chainData.Chain[i].m_discoveryState == MergeItemDiscoveryState.Unlock)
            {
                TryAddClaimedFrame(_frameTile);
            }
        }
        currentUnlockCount = unlock;
        RefreshFrameText();
    }

    private void RefreshFrameText()
    {
        if (currentUnlockCount == 0)
            frameTitle.text = ScriptLocalization.Default_Localiztion_Txt;
        else
            frameTitle.text = ScriptLocalization.Get(_frameTile);
        discoveredProgress.text = ScriptLocalization.Get("Obj/Main/Illustration/Text2") + ": " + currentUnlockCount.ToString("0") + "/" + itemTotalCount.ToString("0");
        if (currentUnlockCount >= itemTotalCount)
        {
            fullDiscoveredIcon.SetActive(true);
        }
    }

    public void RefreshLaguageText()
    {
        RefreshFrameText();
        foreach (var item in collectionItemList)
        {
            item.RefreshLanguageText();
        }
    }
}
