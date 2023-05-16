using Ivy;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel_ExitGame : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI text_title;
    [SerializeField] private TextMeshProUGUI text_content;
    [SerializeField] private TextMeshProUGUI text_confirm;
    [SerializeField] private TextMeshProUGUI text_cancel;
    [SerializeField] private Button btn_confirm;
    [SerializeField] private Button btn_cancel;
    [SerializeField] private Button btn_close;

    [SerializeField] private Button btn_goTO;
    [SerializeField] private TextMeshProUGUI text_goTO;

    [SerializeField] private GameObject[] moreGameArray;

    private int moreGameIndex;

    private void Start()
    {
        btn_confirm.onClick.AddListener(() =>
        {
            Currencies.TrickEnergy();
            RiseSdk.Instance.OnKill();
        });

        btn_cancel.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
        btn_close.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
        btn_goTO.onClick.AddListener(() =>
        {
            if (moreGameIndex == 0)
            {
#if UNITY_ANDROID
                AnalyticsUtil.TrackEvent("click_promote_link", new Dictionary<string, string>()
                    {
                        { "label","exit"},
                        {"promoteapp","com.merge.elves" }
                    });
                RiseSdk.Instance.GetApp("com.merge.elves");
#elif UNITY_IOS
                AnalyticsUtil.TrackEvent("click_promote_link", new Dictionary<string, string>()
                    {
                        { "label","exit"},
                        {"promoteapp","1558066074" }
                    });
            RiseSdk.Instance.GetApp("1558066074");
#endif
            }
            else if (moreGameIndex == 1)
            {
#if UNITY_ANDROID
                AnalyticsUtil.TrackEvent("click_promote_link", new Dictionary<string, string>()
                    {
                        { "label","exit"},
                        {"promoteapp","com.merge.farmtown" }
                    });
                RiseSdk.Instance.GetApp("com.merge.farmtown");
#elif UNITY_IOS
                AnalyticsUtil.TrackEvent("click_promote_link", new Dictionary<string, string>()
                    {
                        { "label","exit"},
                        {"promoteapp","1569751630" }
                    });
            RiseSdk.Instance.GetApp("1569751630");
#endif
            }
            else if (moreGameIndex == 2)
            {
#if UNITY_ANDROID
                AnalyticsUtil.TrackEvent("click_promote_link", new Dictionary<string, string>()
                    {
                        { "label","exit"},
                        {"promoteapp","com.merge.romance" }
                    });
                RiseSdk.Instance.GetApp("com.merge.romance");
#elif UNITY_IOS
                AnalyticsUtil.TrackEvent("click_promote_link", new Dictionary<string, string>()
                    {
                        { "label","exit"},
                        {"promoteapp","6443510628" }
                    });
            RiseSdk.Instance.GetApp("6443510628");
#endif
            }
        });

    }

    public override IEnumerator OnShowUI()
    {
        text_title.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/QuitGameTitle");
        text_content.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/QuitGameDescribe").Replace("\\n", "");
        text_confirm.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/QuitGameButton");
        text_cancel.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/CodeButton1");
        text_goTO.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/QuitGameDescribe1");

        moreGameIndex = SaveUtils.GetInt("moregame_index", -1);
        moreGameIndex++;
        moreGameIndex %= 3;
        for (int i = 0; i < moreGameArray.Length; i++)
        {
            moreGameArray[i].SetActive(i == moreGameIndex);
        }
        SaveUtils.SetInt("moregame_index", moreGameIndex);
        yield return base.OnShowUI();
    }

    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
    }
}
