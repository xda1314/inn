using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Ivy;

/// <summary>
/// 评价弹窗
/// </summary>
public class UIPanel_Evaluate : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI t_Title;
    [SerializeField] private Button btn_Close;
    [SerializeField] private TextMeshProUGUI t_Desc;
    [SerializeField] private Button btn_Rate;
    [SerializeField] private TextMeshProUGUI t_Rate;
    [SerializeField] private Button[] btn_Star;
    [SerializeField] private GameObject[] star;

    public static int starNum = 5;
    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_Close.onClick.AddListener(CloseBtnClick);
        btn_Rate.onClick.AddListener(RateBtnClick);
        for (int i = 0; i < btn_Star.Length; i++)
        {
            int index = i;
            btn_Star[i].onClick.AddListener(() => StarBtnClick(index));
        }
    }
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        t_Title.text = t_Rate.text = I2.Loc.ScriptLocalization.Get("Obj/Other/Text4");
        t_Desc.text = I2.Loc.ScriptLocalization.Get("Obj/RateUs/Text1");
    }


    private void StarBtnClick(int index) 
    {
        starNum = index + 1;
        for (int i = 0; i < star.Length; i++)
        {
            star[i].SetActive(i <= index);
        }
    }
    private void CloseBtnClick() 
    {     
        UISystem.Instance.HideUI(this);
    }
    private void RateBtnClick() 
    {
        if (starNum >= 4)
        {
#if UNITY_ANDROID
            RiseSdk.Instance.GetApp("com.merge.inn");
#elif UNITY_IOS
            RiseSdk.Instance.GetApp("1597263539");
#endif
        }
        else 
        {
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_Suggest));
        }
        CloseBtnClick();
    }
}
