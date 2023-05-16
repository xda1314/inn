using Ivy.Mail;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item_Mail : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI t_Desc;
    [SerializeField] Button btn_Claim;
    [SerializeField] TextMeshProUGUI t_Claim;

    public void Refresh(UIPanel_Mail panel, MailData data)
    {
        btn_Claim.onClick.AddListener(() =>
        {
            panel.ClaimMail(data, this);
        });
        t_Desc.text = I2.Loc.ScriptLocalization.Get("Obj/MailBox/SendMessage/Text1");
        t_Claim.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/PointsRewardButton1");
    }
}
