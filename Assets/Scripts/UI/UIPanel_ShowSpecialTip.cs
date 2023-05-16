using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIPanelData_ShowSpecialTip : UIPanelDataBase
{
    public string prefabName { get; private set; }
    public UIPanelData_ShowSpecialTip(string prefabName) : base(Consts.UIPanel_ShowSpecialTip, UIShowLayer.TopPopup)
    {
        this.prefabName = prefabName;
    }
}
/// <summary>
/// 展示物品获取途径和匹配链界面
/// </summary>
public class UIPanel_ShowSpecialTip : UIPanelBase
{
    [SerializeField] private Button btn_Close;
    [SerializeField] private TextMeshProUGUI lbl_Title;
    [SerializeField] private TextMeshProUGUI m_LblCollection;
    [SerializeField] private GameObject m_ChainRoot;

    private string prefabName;
    private MergeItemChain itemChain;
    private MergeItemDefinition itemDefinition;
    List<UI_ShowTip_Item> allCollectionItemList = new List<UI_ShowTip_Item>();

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

        prefabName = ((UIPanelData_ShowSpecialTip)UIPanelData).prefabName;
        MergeItemDefinition.TotalDefinitionsDict.TryGetValue(prefabName, out itemDefinition);
        MergeItemChain.TotalChainsDict.TryGetValue(itemDefinition.ChainID, out itemChain);
        lbl_Title.text = I2.Loc.ScriptLocalization.Get(itemDefinition.locKey_Chain);
        m_LblCollection.text = I2.Loc.ScriptLocalization.Get(itemDefinition.locKey_Output);
        InitChainInfo();
        yield return ShowChain();
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
    }

    /// <summary>
    /// 初始化要显示的匹配链
    /// </summary>
    private void InitChainInfo()
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
    /// <summary>
    /// 显示匹配链
    /// </summary>
    private IEnumerator ShowChain()
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
                    allCollectionItemList[i].RefreshChain(itemChain.Chain[i], i, true);
                }
                else
                {
                    allCollectionItemList[i].RefreshChain(itemChain.Chain[i], i, false);
                }
            }
            allCollectionItemList[i].gameObject.transform.localScale = Vector3.one * 0.9f;
        }
        yield return null;
    }

}


