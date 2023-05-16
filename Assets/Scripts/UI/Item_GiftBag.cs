using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item_GiftBag : MonoBehaviour
{
    [SerializeField] private Transform itemRoot;
    [SerializeField] private TextMeshProUGUI text_name;
    [SerializeField] private TextMeshProUGUI text_lv;
    [SerializeField] private Button btnInfo;

    private GameObject _itemObj;
    private MergeItemDefinition mData;
    public void SetData(MergeRewardItem reward)
    {
        AssetSystem.Instance.DestoryGameObject("", _itemObj);
        _itemObj = AssetSystem.Instance.Instantiate(reward.ShowRewardPrefabName, itemRoot);
        _itemObj.transform.localPosition = Vector3.zero;
        _itemObj.transform.localScale = Vector3.one;

        mData = null;
        if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(reward.name, out var def))
        {
            mData = def;
            text_name.text = I2.Loc.ScriptLocalization.Get(mData.ItemName);
            text_lv.text = $"Lv.{mData.Level}";
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
