using DG.Tweening;
using IvyCore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 选择主界面皮肤界面
/// </summary>
public class UIPanel_ChooseSkin : UIPanelBase
{
    [SerializeField] private Button Btn_Close;
    [SerializeField] private TextMeshProUGUI Lbl_Title;
    [SerializeField] private GridLayoutGroup grid;

    private Dictionary<string, Item_ChooseSkin> skinItemDic = new Dictionary<string, Item_ChooseSkin>();
    public override void OnInitUI()
    {
        base.OnInitUI();
        Btn_Close.onClick.AddListener(() => UISystem.Instance.HideUI(this));
        InitItem();
    }
    public override IEnumerator OnShowUI()
    {
        RefreshItem(ChooseSkinSystem.Instance.curSkinName);
        Lbl_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Wallpaper/Title");
        yield return null;//避免动画卡顿，延迟2帧
        yield return null;//避免动画卡顿
        yield return base.OnShowUI();
    }
    private void InitItem()
    {
        List<string> allSkinList = ChooseSkinSystem.Instance.ReturnAllSkinList();
        for (int i = 0; i < allSkinList.Count; i++)
        {
            GameObject go = AssetSystem.Instance.Instantiate(Consts.Item_ChooseSkin, grid.transform);
            if (go && go.TryGetComponent(out Item_ChooseSkin item))
            {
                item.InitItem(allSkinList[i], this);
                skinItemDic.Add(allSkinList[i], item);
            }
        }
    }
    public void RefreshItem(string selectItem)
    {
        foreach (var item in skinItemDic)
        {
            item.Value.RefreshItem(item.Key == selectItem);
        }
    }

}
