using TMPro;
using UnityEngine;

public class UI_Spin_Item : MonoBehaviour
{
    [SerializeField] private Transform iconRoot;
    [SerializeField] private TextMeshProUGUI lbl_Count;

    private GameObject _itemObj;
    public void SetData(string prefabName, int count)
    {
        AssetSystem.Instance.DestoryGameObject("", _itemObj);
        _itemObj = AssetSystem.Instance.Instantiate(prefabName, iconRoot);
        _itemObj.transform.localPosition = Vector3.zero;
        _itemObj.transform.localScale = Vector3.one;

        lbl_Count.text = count > 1 ? count.ToString() : "";
    }
}
