using Ivy;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel_MoreGame : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI tmp_Title;
    [SerializeField] private Button btn_Close;
    [SerializeField] private Button btn_GoTO;
    [SerializeField] private TextMeshProUGUI tmp_GoTo;
    [SerializeField] private Button btn_GoTO2;
    [SerializeField] private TextMeshProUGUI tmp_GoTo2;

    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_Close.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
        btn_GoTO.onClick.AddListener(() =>
        {
#if UNITY_ANDROID
            AnalyticsUtil.TrackEvent("click_promote_link", new Dictionary<string, string>()
                    {
                        { "label","moregame"},
                        {"promoteapp","com.merge.elves" }
                    });
            RiseSdk.Instance.GetApp("com.merge.elves");
#elif UNITY_IOS
            AnalyticsUtil.TrackEvent("click_promote_link", new Dictionary<string, string>()
                    {
                        { "label","moregame"},
                        {"promoteapp","1558066074" }
                    });
            RiseSdk.Instance.GetApp("1558066074");
#endif
        });

        btn_GoTO2.onClick.AddListener(() =>
        {
#if UNITY_ANDROID
            AnalyticsUtil.TrackEvent("click_promote_link", new Dictionary<string, string>()
                    {
                        { "label","moregame"},
                        {"promoteapp","com.merge.farmtown" }
                    });
            RiseSdk.Instance.GetApp("com.merge.farmtown");
#elif UNITY_IOS
            AnalyticsUtil.TrackEvent("click_promote_link", new Dictionary<string, string>()
                    {
                        { "label","moregame"},
                        {"promoteapp","1569751630" }
                    });
            RiseSdk.Instance.GetApp("1569751630");
#endif
        });
    }

    public override IEnumerator OnShowUI()
    {
        tmp_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Other/Text3");
        tmp_GoTo.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/QuitGameDescribe1");
        tmp_GoTo2.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/QuitGameDescribe1");
        yield return base.OnShowUI();
    }

}
