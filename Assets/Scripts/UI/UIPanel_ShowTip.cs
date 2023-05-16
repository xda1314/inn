using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelData_ShowTip : UIPanelDataBase
{
    public string prefabName { get; private set; }
    public UIPanelData_ShowTip(string prefabName) : base(Consts.UIPanel_ShowTip, UIShowLayer.TopPopup)
    {
        this.prefabName = prefabName;
    }
}
/// <summary>
/// 展示物品获取途径和匹配链界面
/// </summary>
public class UIPanel_ShowTip : UIPanelBase
{
    [SerializeField] private Button btn_Close;
    [SerializeField] private TextMeshProUGUI lbl_Title;
    [SerializeField] private TextMeshProUGUI m_LblDiscribe;
    [SerializeField] private TextMeshProUGUI m_LblCollection;
    [SerializeField] private HorizontalLayoutGroup m_SpawnBgGrid;
    [SerializeField] private GameObject m_ChainRoot;

    private string prefabName;
    private MergeItemChain itemChain;
    private MergeItemDefinition itemDefinition;
    List<UI_ShowTip_Item> allCollectionItemList = new List<UI_ShowTip_Item>();
    List<GameObject> containBgList = new List<GameObject>();

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
        prefabName = ((UIPanelData_ShowTip)UIPanelData).prefabName;
        MergeItemDefinition.TotalDefinitionsDict.TryGetValue(prefabName, out itemDefinition);
        MergeItemChain.TotalChainsDict.TryGetValue(itemDefinition.ChainID, out itemChain);

        if (itemDefinition.PrefabName == "MagicWand")
            lbl_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Name/MagicWand");
        else
            lbl_Title.text = I2.Loc.ScriptLocalization.Get(itemDefinition.locKey_Chain);
        m_LblCollection.text = I2.Loc.ScriptLocalization.Get(itemDefinition.locKey_Output);
        if (itemDefinition.PrefabName == "MagicWand")
        {
            m_LblDiscribe.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/MagicWandDescribe");
        }
        else
        {
            switch (itemDefinition.locKey_Chain)
            {
                case "Obj/Chain/EXP":
                    m_LblDiscribe.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/XPDescribe");
                    break;
                case "Obj/Chain/UniversalPrefb":
                    m_LblDiscribe.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/UniversalPrefbDescribe");
                    break;
                default:
                    m_LblDiscribe.text = I2.Loc.ScriptLocalization.Get("Obj/Main/Illustration/Text4");
                    break;
            }
        }

        // m_LblDiscribe.text = itemDefinition.locKey_Chain == "Obj/Chain/EXP" ? I2.Loc.ScriptLocalization.Get("Obj/Chain/XPDescribe") : I2.Loc.ScriptLocalization.Get("Obj/Main/Illustration/Text4");
        InitContainInfo();
        if (itemDefinition.HasChain)
        {
            InitChainInfo();
            ShowChain();
        }

        yield return base.OnShowUI();

    }
    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
        for (int i = 0; i < containBgList.Count; i++)
        {
            Destroy(containBgList[i]);
        }
        containBgList.Clear();

        for (int i = 0; i < allCollectionItemList.Count; i++)
        {
            Destroy(allCollectionItemList[i].gameObject);
        }
        allCollectionItemList.Clear();
    }

    /// <summary>
    /// 创建容器item和附加监听
    /// </summary>
    private void InitContainInfo()
    {
        List<string> spawnList = itemDefinition.originList;
        for (int i = 0; i < spawnList.Count; i++)
        {
            //创建背景
            GameObject bgObj = AssetSystem.Instance.Instantiate(Consts.UI_CommomItemBg, m_SpawnBgGrid.transform);
            GameObject bg = bgObj.transform.Find("bg").gameObject;
            bg.SetActive(true);
            bgObj.transform.localScale = Vector3.one * 0.9f;
            ShowItemInfo showInfo = bgObj.transform.Find("ShowInfoBtn").GetComponent<ShowItemInfo>();
            showInfo.gameObject.SetActive(true);

            containBgList.Add(bgObj);
            //创建item
            AssetSystem.Instance.Instantiate(spawnList[i], bg.transform);
            //附加监听
            showInfo.RefreshPrefabName(spawnList[i], this);

        }
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
    private void ShowChain()
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
    }

}
