using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UIPanel_DownloadFail : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI tmp_Title;
    [SerializeField] private TextMeshProUGUI tmp_Desc;
    [SerializeField] private Button btn_Close;
    [SerializeField] private Button btn_Retry;
    [SerializeField] private TextMeshProUGUI tmp_Retry;
    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_Close.onClick.AddListener(() =>
        {
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_ExitGame, UIShowLayer.Top));
        });
        btn_Retry.onClick.AddListener(() =>
        {
            GameLoader.Instance.StartCoroutine(GameLoader.Instance.DownloadAsset(true));
            UISystem.Instance.HideUI(this);
        });
    }
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        tmp_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Download/Text5");
        tmp_Desc.text = I2.Loc.ScriptLocalization.Get("Obj/Download/Text6");
        tmp_Retry.text = I2.Loc.ScriptLocalization.Get("Obj/Download/Text7");
    }

}
