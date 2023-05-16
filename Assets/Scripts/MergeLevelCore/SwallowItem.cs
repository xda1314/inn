using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwallowItem : MonoBehaviour
{
    [SerializeField] private Transform _itemTrans;
    [SerializeField] private TextMeshProUGUI _itemCout;
    [SerializeField] private GameObject _itemComplete;
    [SerializeField] private ShowItemInfo itemInfo;

    private bool hasStart = false;
    public MergeItemData ItemData { get; private set; }
    private string _prefabName;
    private GameObject _itemObj;

    private void Start()
    {
        hasStart = true;
        InitUI();
    }

    public void InitWithData(MergeItemData data, string name)
    {
        ItemData = data;
        _prefabName = name;
        if(!string.IsNullOrEmpty(name))
            itemInfo.RefreshPrefabName(name, null);
        if (hasStart)
            InitUI();
    }

    private void InitUI()
    {
        AssetSystem.Instance.DestoryGameObject("", _itemObj);
        _itemObj = AssetSystem.Instance.Instantiate(_prefabName, this.transform);
        _itemObj.transform.SetParent(_itemTrans, false);
        _itemObj.transform.localPosition = Vector3.zero;
        _itemObj.transform.localScale = Vector3.one;

        if (ItemData.curSwallowItemDic.ContainsKey(_prefabName))
        {
            var getCount = 0;
            if (ItemData.swallowedItemDic.ContainsKey(_prefabName))
                getCount = ItemData.swallowedItemDic[_prefabName];
            if (getCount >= ItemData.curSwallowItemDic[_prefabName])
            {
                _itemCout.gameObject.SetActive(false);
                _itemComplete.SetActive(true);
            }
            else
            {
                _itemCout.gameObject.SetActive(true);
                _itemCout.text = $"{getCount}/{ItemData.curSwallowItemDic[_prefabName]}";
                _itemComplete.SetActive(false);
            }
        }
        else
        {
            _itemCout.gameObject.SetActive(true);
            _itemCout.text = "-/-";
            _itemComplete.SetActive(false);
        }
    }
}
