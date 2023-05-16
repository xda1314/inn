using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 礼盒商城
/// </summary>
public class UI_Shop_Packages : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI lbl_ShopTitle;
    [SerializeField] private TextMeshProUGUI lbl_RefreshTime;
    [SerializeField] private UI_Shop_NormalItem[] shopItemArray;

    public void RefreshUIData(List<ShopPackData> packDataList, Action<ShopPackData, Vector3> cb)
    {
        lbl_ShopTitle.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part4");
        lbl_RefreshTime.text = ExtensionTool.GetFormatTime(TimeManager.Instance.GetTomorrowRefreshTimeSpan());
        for (int i = 0; i < shopItemArray.Length; i++)
        {
            int index = i;

            if (packDataList != null && packDataList.Count > i)
            {
                shopItemArray[i].gameObject.SetActive(true);
                shopItemArray[i].SetItemInfo(packDataList[index], cb);
            }
            else
            {
                shopItemArray[i].gameObject.SetActive(false);
            }
        }
    }
}
