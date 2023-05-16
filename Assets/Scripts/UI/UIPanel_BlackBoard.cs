using Ivy;
using Ivy.Firebase;
using IvyCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 账号已被封禁界面
/// </summary>
public class UIPanel_BlackBoard : UIPanelBase
{
    [SerializeField] public Button btn_GoTo;
    [SerializeField] public TextMeshProUGUI tmp_GoTo;
    [SerializeField] public TextMeshProUGUI tmp_title;
    [SerializeField] public TextMeshProUGUI tmp_desc;

    public override void OnInitUI()
    {
        IsInit = true;
        btn_GoTo.onClick.AddListener(() =>
        {
            if (Black_Board.IsBlockAccount_All)
            {
                try
                {
                    string str = "";
                    Dictionary<string, string> playerDict = new Dictionary<string, string>();
                    playerDict.Add("level", TaskGoalsManager.Instance?.curLevelIndex.ToString() ?? "0");
                    playerDict.Add("gems", Currencies.Gems.ToString());
                    playerDict.Add("coins", Currencies.Coins.ToString());
                    playerDict.Add("pay_orders", (GameManager.Instance?.playerData?.Pay_Orders.ToString()) ?? "0");
                    playerDict.Add("pay_total", (GameManager.Instance?.playerData?.Pay_Totals.ToString()) ?? "0");
                    str = JsonConvert.SerializeObject(playerDict);
                    RiseSdk.Instance.HelpEngagement(FirebaseSystem.Instance.FirebaseID, str);
                }
                catch (Exception e)
                {
                    RiseSdk.Instance.HelpEngagement(FirebaseSystem.Instance.FirebaseID, "");
                }
            }
            else
            {
                UISystem.Instance.HideUI(this);
            }
        });
    }

    public override IEnumerator OnShowUI()
    {
        tmp_GoTo.text = I2.Loc.ScriptLocalization.Get("Obj/Banner/Btn/Text");
        tmp_title.text = I2.Loc.ScriptLocalization.Get("Obj/Other/Text5");
        tmp_desc.text = I2.Loc.ScriptLocalization.Get("Obj/Other/Text6");
        yield return base.OnShowUI();
    }

}
