using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel_Community : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI tmp_title;
    [SerializeField] private Button btn_facebook;
    [SerializeField] private Button btn_Instagram;
    [SerializeField] private Button btn_Youtube;
    [SerializeField] private Button btn_TikTok;
    [SerializeField] private Button btn_Twitter;

    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_facebook.onClick.AddListener(() =>
        {

        });
        btn_Instagram.onClick.AddListener(() =>
        {

        });
        btn_Youtube.onClick.AddListener(() =>
        {

        });
        btn_TikTok.onClick.AddListener(() =>
        {

        });
        btn_Twitter.onClick.AddListener(() =>
        {

        });

    }

    public override IEnumerator OnShowUI()
    {
        tmp_title.text = I2.Loc.ScriptLocalization.Get("");
        yield return base.OnShowUI();
    }

}
