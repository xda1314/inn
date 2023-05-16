using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;

public class UIPanelData_ShowOutputTip : UIPanelDataBase
{
    public string prefabName { get; private set; }
    public UIPanelData_ShowOutputTip(string prefabName) : base(Consts.UIPanel_ShowOutputTip, UIShowLayer.TopPopup)
    {
        this.prefabName = prefabName;
    }
}
/// <summary>
/// 展示物品获取途径和匹配链界面
/// </summary>
public class UIPanel_ShowOutputTip : UIPanelBase
{
    [SerializeField] private Button btn_Close;
    [SerializeField] private TextMeshProUGUI lbl_Title;
    [SerializeField] private TextMeshProUGUI m_LblDiscribe;
    [SerializeField] private TextMeshProUGUI m_LblCollection;
    [SerializeField] private GameObject m_ChainRoot;
    [SerializeField] private GameObject m_ContainRoot;

    private string prefabName;
    private MergeItemChain itemChain;
    private MergeItemDefinition itemDefinition;
    List<UI_ShowTip_Item> allCollectionItemList = new List<UI_ShowTip_Item>();
    List<GameObject> ItemsInContainList = new List<GameObject>();//容器可取出的物品列表
    private void Awake()
    {
        base.uiType = UIType.Tip;
    }

    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_Close.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
    }
    public override IEnumerator OnShowUI()
    {
        prefabName = ((UIPanelData_ShowOutputTip)UIPanelData).prefabName;
        MergeItemDefinition.TotalDefinitionsDict.TryGetValue(prefabName, out itemDefinition);
        MergeItemChain.TotalChainsDict.TryGetValue(itemDefinition.ChainID, out itemChain);
        if (itemChain == null || itemChain.Chain == null || itemChain.Chain.Length == 0)
            GameDebug.LogError("匹配练数据错误,匹配链id:" + itemDefinition.ChainID);
        lbl_Title.text = I2.Loc.ScriptLocalization.Get(itemDefinition.locKey_Chain);
        m_LblCollection.text = I2.Loc.ScriptLocalization.Get(itemDefinition.locKey_Output);
        m_LblDiscribe.text = I2.Loc.ScriptLocalization.Get("Obj/Main/Illustration/Text5");
        InitChainInfo();
        yield return ShowChain();
        yield return InitCanGetItemFromContain();
        yield return base.OnShowUI();
    }

    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
        for (int i = 0; i < allCollectionItemList.Count; i++)
        {
            Destroy(allCollectionItemList[i].gameObject);
        }
        allCollectionItemList.Clear();

        for (int i = 0; i < ItemsInContainList.Count; i++)
        {
            Destroy(ItemsInContainList[i]);
        }
        ItemsInContainList.Clear();
    }

    public static List<string> FindPrefabsFromPool(string pondId)
    {
        int max = 50;
        var currentPond = pondId;
        List<string> itemsList = new List<string>();
        if (!pondId.IsNullOrEmpty() && !pondId.StartsWith("Loot_"))
        {
            return itemsList;
        }
        while (true)
        {
            if (max < 0) break;
            max--;
            if (LootTable.LootTableDic.TryGetValue(currentPond, out var lootTable))
            {
                foreach (var item in lootTable.pool.rewardItemsList)
                {
                    if (item.rewardItem.name.StartsWith("Loot_"))
                    {
                        // 继续寻找
                        currentPond = item.rewardItem.name;
                    }
                    else
                    {
                        itemsList.Add(item.rewardItem.name);
                    }
                }
            }
            else
            {
                break;
            }
        }

        return itemsList;
    }

    private bool IsCanAdd(List<string> pondList, string str) 
    {
        foreach (var item in pondList)
        {
            if (item == str)
                return false;
        }
        return true;
    }

    /// <summary>
    /// 初始可从容器中获得的物品
    /// </summary>
    private IEnumerator InitCanGetItemFromContain()
    {
        try
        {
            List<string> itemsList = new List<string>();
            List<string> pondList = new List<string>();
            if (itemDefinition.CategoryType == MergeItemCategoryType.taskBox)
            {
                foreach (var item in itemDefinition.taskBoxPondDict)
                {
                    if (TaskGoalsManager.Instance.curLevelIndex <= item.Key)
                    {
                        pondList.Add(item.Value);
                        break;
                    }
                }
            }
            else
            {
                pondList.Add(itemDefinition.PondId);
            }

            for (int index = 0; index < pondList.Count; index++)
            {
                if (pondList[index].StartsWith("Loot_"))
                {
                    List<string> l = FindPrefabsFromPool(pondList[index]);
                    foreach (var item in l)
                    {
                        if (itemsList.Count >= 12 || !IsCanAdd(itemsList, item))
                            break;
                        itemsList.Add(item);
                    }
                }
                else
                {
                    if (itemsList.Count < 12 && IsCanAdd(itemsList, pondList[index]))
                        itemsList.Add(pondList[index]);
                }
            }

            foreach (string name in itemsList)
            {
                GameObject go = AssetSystem.Instance.Instantiate(Consts.UI_ShowTip_Item, m_ContainRoot.transform);
                ItemsInContainList.Add(go);
                UI_ShowTip_Item item = go.GetComponent<UI_ShowTip_Item>();
                if (item != null)
                    item.RefreshContaner(name, itemDefinition.CategoryType == MergeItemCategoryType.boxed);
                go.transform.localScale = Vector3.one * 0.9f;
            }

        }
        catch (Exception e)
        {
        }
        yield return null;
    }

    /// <summary>
    /// 初始化要显示的匹配链
    /// </summary>
    private void InitChainInfo()
    {
        if (itemChain != null && itemChain.Chain != null && itemChain.Chain.Length > 0)
        {
            for (int i = 0; i < itemChain.Chain.Length; i++)
            {
                if (itemChain.Chain[i] != null)
                {
                    GameObject item = AssetSystem.Instance.Instantiate(Consts.UI_ShowTip_Item, m_ChainRoot.transform);
                    allCollectionItemList.Add(item.GetComponent<UI_ShowTip_Item>());
                    item.GetComponent<UI_ShowTip_Item>().SetupItemPosition(i, itemChain.ChainSize);
                }
            }
        }
    }
    /// <summary>
    /// 显示匹配链
    /// </summary>
    private IEnumerator ShowChain()
    {
        if (itemChain != null && itemChain.Chain != null && itemChain.Chain.Length > 0)
        {
            int indexInChain = -1;
            for (int i = 0; i < itemChain.Chain.Length; i++)
            {
                if (prefabName == itemChain.Chain[i].PrefabName)
                {
                    indexInChain = i;
                    break;
                }
            }
            for (int i = 0; i < allCollectionItemList.Count; i++)
            {
                if (itemChain.Chain[i] != null)
                {
                    if (indexInChain == i)
                    {
                        allCollectionItemList[i].RefreshChain(itemChain.Chain[i], i, true, itemDefinition.CategoryType == MergeItemCategoryType.boxed);
                    }
                    else
                    {
                        allCollectionItemList[i].RefreshChain(itemChain.Chain[i], i, false, itemDefinition.CategoryType == MergeItemCategoryType.boxed);
                    }
                }
                allCollectionItemList[i].gameObject.transform.localScale = Vector3.one * 0.9f;
            }
            yield return null;
        }
    }
}


