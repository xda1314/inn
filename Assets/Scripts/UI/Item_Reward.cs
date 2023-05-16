using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item_Reward : MonoBehaviour
{
    [SerializeField] private Transform itemRoot;
    public Transform ItemRoot
    {
        set {; }
        get { return itemRoot; }
    }
    #region 组件
    [SerializeField] private TextMeshProUGUI lbl_Count;
    [SerializeField] private Material spriteLight;
    [SerializeField] private Transform img_Tranx2;
    [SerializeField] private TextMeshProUGUI text_x2;
    [SerializeField] private TextMeshProUGUI text_des;
    #endregion

    #region 变量
    float radian = 0; // 弧度
    float perRadian = 0.03f; // 每次变化的弧度
    float radius = 2.0f; // 半径
    private Vector3 startPos; // 开始时候的坐标
    private bool is_init = false;
    private bool is_sinAni = false;
    private GameObject _itemObj;
    #endregion

    public void SetData(MergeRewardItem reward,bool isDouble,int index)
    {
        MergeItemDefinition.TotalDefinitionsDict.TryGetValue(reward.ShowRewardPrefabName, out MergeItemDefinition itemDefinition);
        if (itemDefinition ==null) 
        {
            GameDebug.Log("MergeObjectConfig 找不到该数据" + reward.ShowRewardPrefabName);
        }
        else 
        {
            text_des.text = I2.Loc.ScriptLocalization.Get(itemDefinition.locKey_Chain);
        }
        
        img_Tranx2.localScale = Vector3.zero;
        lbl_Count.transform.SetParent(transform);
        img_Tranx2.transform.SetParent(transform);
        AssetSystem.Instance.DestoryGameObject("", _itemObj);
        _itemObj = AssetSystem.Instance.Instantiate(reward.ShowRewardPrefabName, itemRoot);
        if (_itemObj != null)
        {
            _itemObj.transform.localPosition = new Vector3(0f,10f,0f);
            _itemObj.transform.localScale = Vector3.one;
            _itemObj.GetComponent<Image>().material = spriteLight;
            startPos = _itemObj.transform.localPosition;
        }
        lbl_Count.text = reward.num <= 1 ? "" : $"x{reward.num}";
        lbl_Count.transform.SetParent(_itemObj.transform);
        img_Tranx2.transform.SetParent(_itemObj.transform);
        is_init = true;
        if (Random.Range(0, 100) < 50) 
        {
            is_sinAni = true;
        }
        if (isDouble)
            StartCoroutine(PlayTweenAni(index));
    }

    private IEnumerator PlayTweenAni(int index)
    {
        yield return new WaitForSeconds(index * 0.2f);
        //添加粒子特效 TODO
        img_Tranx2.transform.DOScale(1.0f, 0.35f).SetEase(Ease.InQuart);
        //DOTween.To(value => { lbl_Count.text = Mathf.Floor(value).ToString(); }, startValue: 0, endValue: 4396, duration: 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (is_init) 
        {
            radian += perRadian;
            float dy = 0;
            if (is_sinAni) 
            {
                dy = Mathf.Sin(radian) * radius;
            }
            else
            {
                dy = Mathf.Cos(radian) * radius;
            }
            _itemObj.transform.localPosition = startPos + new Vector3(0, dy, 0);
        }
    }

    private void OnDestroy()
    {
        is_init = false;
    }
}
