using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using Ivy;

public class UIPanel_Suggest : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI t_Title;
    [SerializeField] private Button btn_Close;
    [SerializeField] private Text t_Desc;
    [SerializeField] private InputField t_Suggest;
    [SerializeField] private Button btn_Rate;
    [SerializeField] private TextMeshProUGUI t_Rate;

    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_Close.onClick.AddListener(CloseBtnClick);
        btn_Rate.onClick.AddListener(RateBtnClick);
    }
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        t_Title.text = I2.Loc.ScriptLocalization.Get("Obj/RateUs/Text3");
        t_Rate.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/SetLoinFailure4");
        string desc = I2.Loc.ScriptLocalization.Get("Obj/RateUs/Text2");
        desc = desc.Replace("\\n", "\n");
        t_Desc.text = desc;
    }


    private void CloseBtnClick()
    {
        UISystem.Instance.HideUI(this);
    }
    private void RateBtnClick()
    {
        if (!string.IsNullOrEmpty(t_Suggest.text)) 
        {
            Dictionary<string, object> jsonParamsDict = new Dictionary<string, object>();
            jsonParamsDict.Add("rate", UIPanel_Evaluate.starNum);
            jsonParamsDict.Add("feedback", t_Suggest.text);
            string jsonParams = JsonConvert.SerializeObject(jsonParamsDict);
            RiseSdk.Instance.CloudFunction("admin-send_rate_feedback", jsonParams);
        }       

        CloseBtnClick();
    }
}
