using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewContainerItemData : UIPanelDataBase
{
    public string prefabName;
    public NewContainerItemData(string prefabName) : base(Consts.UI_ItemDiscovered, UIShowLayer.Top)
    {
        this.prefabName = prefabName;
    }
}

public class UI_ItemDiscovered : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI text_title;
    [SerializeField] private Transform itemTrans;
    [SerializeField] private TextMeshProUGUI text_itemName;
    [SerializeField] private CanvasGroup cg_rating;
    [SerializeField] private TextMeshProUGUI text_rating;
    [SerializeField] private List<GameObject> starGOList = new List<GameObject>();
    [SerializeField] private CanvasGroup cg_volume;
    [SerializeField] private TextMeshProUGUI text_volume;
    [SerializeField] private CanvasGroup cg_cd;
    [SerializeField] private TextMeshProUGUI text_cd;
    [SerializeField] private TextMeshProUGUI text_continue;
    [SerializeField] private Button btn_bgClose;

    private GameObject newItemGO;
    private float posY_rating = 820f, posY_volume = 695f, posY_cd = 565f;

    private void Awake()
    {
        posY_rating = cg_rating.transform.localPosition.y;
        posY_volume = cg_volume.transform.localPosition.y;
        posY_cd = cg_cd.transform.localPosition.y;
    }

    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_bgClose.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
    }

    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        yield return RefreshUI();
    }

    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
    }

    private IEnumerator RefreshUI()
    {
        btn_bgClose.enabled = false;
        //物品信息
        NewContainerItemData data = UIPanelData as NewContainerItemData;
        int starCount = 0;
        if (data != null && !string.IsNullOrEmpty(data.prefabName)
            && MergeItemDefinition.TotalDefinitionsDict.TryGetValue(data.prefabName, out MergeItemDefinition def))
        {
            starCount = (int)def.RarityType;
            AssetSystem.Instance.DestoryGameObject("", newItemGO);
            newItemGO = AssetSystem.Instance.Instantiate(def.PrefabName, itemTrans);
            newItemGO.transform.localPosition = Vector3.zero;
            newItemGO.transform.localScale = Vector3.one * 3f;
            text_itemName.text = I2.Loc.ScriptLocalization.Get(def.locKey_Name);
        }
        //
        RefreshLanguageText();
        //重置属性，做动作
        ResetActionState();
        yield return new WaitForSeconds(0.3f);
        cg_rating.DOFade(1, 0.2f);
        cg_rating.transform.DOLocalMoveY(posY_rating, 0.3f).SetRelative(false).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.2f);
        cg_volume.DOFade(1, 0.2f);
        cg_volume.transform.DOLocalMoveY(posY_volume, 0.3f).SetRelative(false).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.2f);
        cg_cd.DOFade(1, 0.2f);
        cg_cd.transform.DOLocalMoveY(posY_cd, 0.3f).SetRelative(false).SetEase(Ease.OutBack);
        for (int i = 0; i < starCount; i++)
        {
            starGOList[i].transform.DOScale(1, 0.3f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.2f);
        }
        text_continue.gameObject.SetActive(true);
        btn_bgClose.enabled = true;
    }


    private void ResetActionState()
    {
        cg_rating.alpha = 0;
        cg_rating.transform.localPosition = Vector3.up * (posY_rating - 400);
        cg_volume.alpha = 0;
        cg_volume.transform.localPosition = Vector3.up * (posY_volume - 400);
        cg_cd.alpha = 0;
        cg_cd.transform.localPosition = Vector3.up * (posY_cd - 400);
        foreach (var star in starGOList)
        {
            star.transform.localScale = Vector3.zero;
        }
        text_continue.gameObject.SetActive(false);
    }

    private void RefreshLanguageText()
    {
        text_title.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Diacover_Title");
        text_rating.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/DiacoverDescribe1");
        text_volume.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/DiacoverDescribe2");
        text_cd.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/DiacoverDescribe3");
        text_continue.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/DiacoverDescribe4");
    }
}
