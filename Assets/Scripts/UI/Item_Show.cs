using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item_Show : MonoBehaviour
{
    [SerializeField] private Transform itemRoot;

    #region 组件
    [SerializeField] private TextMeshProUGUI text_Count;
    [SerializeField] private Button btn_info; 
    #endregion

    #region 变量
    private GameObject _itemObj;
    #endregion

    public void SetData(MergeRewardItem reward)
    {
        AssetSystem.Instance.DestoryGameObject("", _itemObj);
        _itemObj = AssetSystem.Instance.Instantiate(reward.ShowRewardPrefabName, itemRoot);
        if (_itemObj != null)
        {
            _itemObj.transform.localPosition = new Vector3(0f,10f,0f);
            _itemObj.transform.localScale = Vector3.one;
        }
        text_Count.text = reward.num <= 1 ? "" : $"x{reward.num}";
        ShowItemInfo itemInfo = btn_info.GetComponent<ShowItemInfo>();
        if (itemInfo == null)
        {
            itemInfo = btn_info.gameObject.AddComponent<ShowItemInfo>().GetComponent<ShowItemInfo>();
        }
        itemInfo.RefreshPrefabName(reward.name, null);
    }

}
