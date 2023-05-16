using Ivy.Mail;
using Ivy.RewardPacks;
using IvyCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel_Mail : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI t_Title;
    [SerializeField] private Button btn_Close;
    //有邮件
    [SerializeField] private GameObject hasMailRoot;
    [SerializeField] private Transform layoutTrans;
    //无邮件
    [SerializeField] private GameObject NoMailRoot;
    [SerializeField] private TextMeshProUGUI t_NoMail;

    private List<MailData> mailList;
    private List<Item_Mail> saveItems = new List<Item_Mail>();
    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_Close.onClick.AddListener(() => { UISystem.Instance.HideUI(this); });
    }

    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        RefreshItem();
        RefreshView();
    }
    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
        for (int i = 0; i < saveItems.Count; i++)
        {
            AssetSystem.Instance.DestoryGameObject(Consts.Item_Mail, saveItems[i].gameObject);
        }
        saveItems.Clear();
    }
    /// <summary>
    /// 刷新邮件
    /// </summary>
    private void RefreshItem()
    {
        MailSystem.Instance.RefreshMail(() =>
        {
            mailList = MailSystem.Instance.GetShowEmails();
            for (int i = 0; i < mailList.Count; i++)
            {
                GameObject go = AssetSystem.Instance.Instantiate(Consts.Item_Mail, layoutTrans);
                if (go.TryGetComponent(out Item_Mail _item))
                {
                    _item.Refresh(this, mailList[i]);
                    saveItems.Add(_item);
                }
            }
            RefreshView();
        });
    }
    private void RefreshView()
    {
        t_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Mailbox");
        t_NoMail.text = I2.Loc.ScriptLocalization.Get("Obj/MailBox/NoMail/Text1");
        hasMailRoot.SetActive(saveItems.Count > 0);
        NoMailRoot.SetActive(saveItems.Count <= 0);
    }

    public void ClaimMail(MailData data, Item_Mail item)
    {
        MailSystem.Instance.SetMailReceived(data, () =>
         {
             List<MergeRewardItem> rewardItem = new List<MergeRewardItem>();
             foreach (var giftItem in data.gift.Rewards)
             {
                 MergeRewardItem mergeRewardItem = new MergeRewardItem();
                 mergeRewardItem.name = giftItem.Name;
                 mergeRewardItem.num = giftItem.Num;
                 rewardItem.Add(mergeRewardItem);
             }

             //发放奖励
             GameManager.Instance.GiveRewardItem(rewardItem, "mail");
             //删除ui
             mailList.Remove(data);
             if (saveItems.Contains(item))
             {
                 saveItems.Remove(item);
                 AssetSystem.Instance.DestoryGameObject(Consts.Item_Mail, item.gameObject);
             }
             RefreshView();
             Page_Setting.refreshMailRedPoint?.Invoke();
         });
    }
    //private void ClaimAllMail()
    //{
    //    for (int i = 0; i < mailList.Count; i++)
    //    {
    //        MailUtils.ReceivedMail(mailList[i], () =>
    //        {
    //            GameManager.Instance.GiveRewardItem(mailList[i].gift, "ClaimMail");
    //            //删除ui
    //            mailList.Remove(mailList[i]);
    //            if (saveItems.Contains(saveItems[i]))
    //            {
    //                AssetSystem.Instance.DestoryGameObject(Consts.Item_Mail, saveItems[i].gameObject);
    //                saveItems.Remove(saveItems[i]);
    //            }
    //            RefreshView();
    //        });
    //    }
    //}
}