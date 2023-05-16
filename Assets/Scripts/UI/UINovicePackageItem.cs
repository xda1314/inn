using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UINovicePackageItem : UIPanelBase
{
    #region 组件
    [SerializeField] private TextMeshProUGUI text_kind;
    [SerializeField] private Transform ItemContainer;
    [SerializeField] private TextMeshProUGUI text_count;
    [SerializeField] private Button btn_InfoTips;
    #endregion

    #region 变量
    private MergeRewardItem rewardItem;
    #endregion


    /// <summary>
    /// 销毁物品
    /// </summary>
    private void DestoryItemUI()
    {
        for (int i = ItemContainer.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(ItemContainer.transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// 实例化物品
    /// </summary>
    /// <param name="exchangeData"></param>
    private void InstanteItemUI(MergeRewardItem rewardItem)
    {
        AssetSystem.Instance.InstantiateAsync(rewardItem.ShowRewardPrefabName, ItemContainer, gO =>
        {
            RectTransform rect = gO.GetComponents<RectTransform>()[0];
            rect.sizeDelta = new Vector2(150, 150);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.localPosition = Vector2.zero;
        });
        if (!rewardItem.IsRewardPrefab)
        {
            text_count.text = "x" + rewardItem.num.ToString();
        }
        else if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(rewardItem.ShowRewardPrefabName, out MergeItemDefinition mergeObjectItem))
        {           
            text_count.text = string.Format(I2.Loc.ScriptLocalization.Get("Obj/Item/Info/Level"), mergeObjectItem.Level.ToString());
        }
        else 
        {
            Debug.LogError("MergeObjectConfig表中没找到prefabName！" + rewardItem.ShowRewardPrefabName);
            text_count.text = "";
        }
        
    }

    /// <summary>
    /// 设置奖励种类
    /// </summary>
    private void SetTextKind(MergeRewardItem rewardItem) 
    {
        if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(rewardItem.name, out MergeItemDefinition mergeItemDefinition))
        {
            text_kind.text =  I2.Loc.ScriptLocalization.Get(mergeItemDefinition.locKey_Name);
            btn_InfoTips.gameObject.SetActive(true);
            if(mergeItemDefinition.CategoryType== MergeItemCategoryType.used)
                btn_InfoTips.gameObject.SetActive(false);
        }
        else
        {
            if ("coins" == rewardItem.name) 
            {
                text_kind.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Coins");
                btn_InfoTips.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 设置数据
    /// </summary>
    /// <param name="exchangeData"></param>
    public void SetData(MergeRewardItem rewardItem)
    {
        SetTextKind(rewardItem);
        if (this.rewardItem.name == rewardItem.name && this.rewardItem.num == rewardItem.num)
        {
            return;
        }
        DestoryItemUI();
        InstanteItemUI(rewardItem);
        this.rewardItem = rewardItem;
        ShowItemInfo itemInfo = btn_InfoTips.GetComponent<ShowItemInfo>();
        if (itemInfo == null)
        {
            itemInfo = btn_InfoTips.gameObject.AddComponent<ShowItemInfo>().GetComponent<ShowItemInfo>();
        }
        itemInfo.RefreshPrefabName(rewardItem.name, null);
    }

}