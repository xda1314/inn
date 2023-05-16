using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BranchRedeemRewardItem : MonoBehaviour
{
    [SerializeField] private GameObject bgNormal;
    [SerializeField] private GameObject bgBetter;
    [SerializeField] private Transform itemRoot;
    [SerializeField] private TextMeshProUGUI textCount;
    [SerializeField] private Button btnInfo;

    private GameObject _itemObj;
    private MergeItemDefinition mData;
    public void SetData(MergeRewardItem reward, int rare)
    {
        bgNormal.SetActive(rare == 0);
        bgBetter.SetActive(rare == 1);
        AssetSystem.Instance.DestoryGameObject("", _itemObj);
        _itemObj = AssetSystem.Instance.Instantiate(reward.ShowRewardPrefabName, itemRoot);
        _itemObj.transform.localPosition = Vector3.zero;
        _itemObj.transform.localScale = Vector3.one;
        textCount.text = $"x{reward.num}";
        mData = null;
        if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(reward.name, out var def))
        {
            mData = def;
        }
    }

    private void Start()
    {
        btnInfo.onClick.AddListener(() =>
        {
            if (mData != null)
            {
                if (mData.WindowType == MergeItemWindowType.universal)
                    UISystem.Instance.ShowUI(new UIPanelData_ShowTip(mData.PrefabName), true);
                else if (mData.WindowType == MergeItemWindowType.count)
                    UISystem.Instance.ShowUI(new UIPanelData_ShowCountTip(mData.PrefabName), true);
                else if (mData.WindowType == MergeItemWindowType.output)
                    UISystem.Instance.ShowUI(new UIPanelData_ShowOutputTip(mData.PrefabName));
                else if (mData.WindowType == MergeItemWindowType.special)
                    UISystem.Instance.ShowUI(new UIPanelData_ShowSpecialTip(mData.PrefabName));
                else if (mData.WindowType == MergeItemWindowType.rare)
                    UISystem.Instance.ShowUI(new UIPanelData_ShowRareTip(mData.PrefabName));
            }
                    
        });
    }
}
