using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

/// <summary>
/// 限时抢购
/// </summary>
public class UI_Shop_Discount : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI lbl_Title;
    [SerializeField] private TextMeshProUGUI lbl_CountDown;
    [SerializeField] private UI_Shop_NormalItem[] normalItemArray;
    [SerializeField] private Button btn_watchAD;
    [SerializeField] private Button btn_nOAD;
    [SerializeField] private TextMeshProUGUI lbl_watchAD;
    [SerializeField] private TextMeshProUGUI lbl_noWatchAD;
    [SerializeField] private Button btn_Refresh;
    [SerializeField] private TextMeshProUGUI lbl_Refresh;
    public float height;
    private Action refreshCB;
    private bool canUpdate = false;

    private void Start()
    {
        btn_Refresh.onClick.AddListener(() =>
        {
            if (Currencies.Spend(CurrencyID.Gems, 5, "shop_refresh"))
            {
                btn_Refresh.interactable = false;
                this.refreshCB?.Invoke();
                Invoke("Reseter", 0.2f);
            }
        });
        btn_watchAD.onClick.AddListener(() =>
        {
            if (AdManager.CanShowAD_Normal())
            {
                AdManager.PlayAd(btn_watchAD.transform.position, AdManager.ADTag.RefreshDiscount, () =>
                {
                    this.refreshCB?.Invoke();
                }, Consts.SaveAD_RefreshDiscount, () =>
                {

                });
            }
            else 
            {
                TextTipSystem.Instance.ShowTip(btn_watchAD.transform.position, I2.Loc.ScriptLocalization.Get("Obj/Chain/WatchVideo_Text6"), TextTipColorType.Yellow);
                RefreshBtnState(AdManager.CanShowAD_Normal());
            }
        });
    }

    public void RefreshUIData(List<ShopPackData> packDataList, Action refreshCB, Action<ShopPackData, Vector3> buyCB)
    {
        canUpdate = true;
        this.refreshCB = refreshCB;
        lbl_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part3");
        lbl_watchAD.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/Engery/Text3");
        lbl_noWatchAD.text = I2.Loc.ScriptLocalization.Get("Obj/Eliminate/Engery/Text3");
        lbl_Refresh.text= I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part4Text");
        // 广告刷新时间
        bool hasAd = TimeManager.IsGetServerUtcSuccess && AdManager.CanShowAD_Normal();
        RefreshBtnState(hasAd);

        for (int i = 0; i < normalItemArray.Length; i++)
        {
            int index = i;
            if (packDataList != null && packDataList.Count > i)
            {
                normalItemArray[i].gameObject.SetActive(true);
                normalItemArray[i].SetItemInfo(packDataList[index], buyCB);
            }
            else
            {
                normalItemArray[i].gameObject.SetActive(false);
            }
        }

    }


    private void Reseter()
    {
        btn_Refresh.interactable = true;
    }

    private void RefreshBtnState(bool canWatchAD)
    {
        btn_nOAD.gameObject.SetActive(!canWatchAD);
        btn_watchAD.gameObject.SetActive(canWatchAD);
    }

    private float timer;
    private void Update()
    {
        if (!canUpdate)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer < 1)
        {
            return;
        }
        timer -= 1;
        lbl_CountDown.text = ExtensionTool.GetFormatTime(TimeManager.Instance.GetTomorrowRefreshTimeSpan());
    }


}
