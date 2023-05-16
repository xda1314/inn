using Ivy.RewardCode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel_RewardCode : UIPanelBase
{
    [SerializeField] private Button _btn_close;
    [SerializeField] private InputField _inputField;
    [SerializeField] private Text txt_title;
    [SerializeField] private Button btn_ok;
    [SerializeField] private Text txt_ok;

    public override void OnInitUI()
    {
        base.OnInitUI();
        _btn_close.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
        btn_ok.onClick.AddListener(() =>
        {
            if (string.IsNullOrEmpty(_inputField.text))
                return;
            RewardCodeSystem.Instance.CollectRewardCodeAsync(_inputField.text, (result, reward) =>
            {
                switch (result)
                {
                    case RewardCodeSystem.RewardCodeResult.Success:
                        {
                            UISystem.Instance.HideUI(this);
                            List<MergeRewardItem> mergeRewardItems = new List<MergeRewardItem>();
                            if (reward != null && reward.Count > 0)
                            {
                                foreach (var item in reward.Rewards)
                                {
                                    MergeRewardItem rewardItem = new MergeRewardItem();
                                    rewardItem.name = item.Name;
                                    rewardItem.num = item.Num;
                                    if (rewardItem.IsValidReward())
                                        mergeRewardItems.Add(rewardItem);
                                }
                            }
                            GameManager.Instance.GiveRewardItem(mergeRewardItems, "CDKey", Vector3.zero);
                        }
                        break;
                    case RewardCodeSystem.RewardCodeResult.Fail_Claimed:
                        UISystem.Instance.ShowUI(new UIPanelData_LoginTip(I2.Loc.ScriptLocalization.Get("Obj/Main/Tasks/Text1"), I2.Loc.ScriptLocalization.Get("Obj/Redemption_Code/NotTip4"), I2.Loc.ScriptLocalization.Get("Obj/Chain/SetLoinFailure4"), null));
                        break;
                    case RewardCodeSystem.RewardCodeResult.Fail_MaxLimit:
                        UISystem.Instance.ShowUI(new UIPanelData_LoginTip(I2.Loc.ScriptLocalization.Get("Obj/Main/Tasks/Text1"), I2.Loc.ScriptLocalization.Get("Obj/Redemption_Code/NotTip3"), I2.Loc.ScriptLocalization.Get("Obj/Chain/SetLoinFailure4"), null));
                        break;
                    case RewardCodeSystem.RewardCodeResult.Fail_Expired:
                        UISystem.Instance.ShowUI(new UIPanelData_LoginTip(I2.Loc.ScriptLocalization.Get("Obj/Main/Tasks/Text1"), I2.Loc.ScriptLocalization.Get("Obj/Redemption_Code/NotTip1"), I2.Loc.ScriptLocalization.Get("Obj/Chain/SetLoinFailure4"), null));
                        break;
                    case RewardCodeSystem.RewardCodeResult.Fail_Not_Started:
                        UISystem.Instance.ShowUI(new UIPanelData_LoginTip(I2.Loc.ScriptLocalization.Get("Obj/Main/Tasks/Text1"), I2.Loc.ScriptLocalization.Get("Obj/Redemption_Code/NotTip2"), I2.Loc.ScriptLocalization.Get("Obj/Chain/SetLoinFailure4"), null));
                        break;
                    case RewardCodeSystem.RewardCodeResult.Fail_Not_Exists:
                        UISystem.Instance.ShowUI(new UIPanelData_LoginTip(I2.Loc.ScriptLocalization.Get("Obj/Main/Tasks/Text1"), I2.Loc.ScriptLocalization.Get("Obj/Redemption_Code/NotTip"), I2.Loc.ScriptLocalization.Get("Obj/Chain/SetLoinFailure4"), null));
                        break;
                    case RewardCodeSystem.RewardCodeResult.Fail_Others:
                        UISystem.Instance.ShowUI(new UIPanelData_LoginTip(I2.Loc.ScriptLocalization.Get("Obj/Main/Tasks/Text1"), I2.Loc.ScriptLocalization.Get("Obj/Redemption_Code/NotTip"), I2.Loc.ScriptLocalization.Get("Obj/Chain/SetLoinFailure4"), null));
                        break;
                    case RewardCodeSystem.RewardCodeResult.Error:
                    default:
                        UISystem.Instance.ShowUI(new UIPanelData_LoginTip(I2.Loc.ScriptLocalization.Get("Obj/Main/Tasks/Text1"), I2.Loc.ScriptLocalization.Get("Obj/Redemption_Code/NotTip"), I2.Loc.ScriptLocalization.Get("Obj/Chain/SetLoinFailure4"), null));
                        break;
                }
            });
        });
    }

    public override IEnumerator OnShowUI()
    {
        txt_title.text = "Whoops!";
        txt_ok.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/CunDangButton");
        _inputField.placeholder.GetComponent<Text>().text = "";
        _inputField.text = string.Empty;
        yield return base.OnShowUI();
    }
}
