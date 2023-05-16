using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 金币和钻石商城
/// </summary>
public class UI_Shop_GemsOrCoins : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI lbl_ShopTitle;
    [SerializeField] private UI_Shop_NormalItem[] shopItemArray;
    [SerializeField] private TextMeshProUGUI lbl_RefreshTime;


    public void RefreshUIData(bool isGems, List<PayPackDefinition> packDataList, Action<PayPackDefinition, Vector3> cb)
    {
        lbl_RefreshTime.text = ExtensionTool.GetFormatTime(TimeManager.Instance.GetTomorrowRefreshTimeSpan());
        if (isGems)
        {
            lbl_ShopTitle.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part5");

            for (int i = 0; i < shopItemArray.Length; i++)
            {
                if (packDataList != null && packDataList.Count > i)
                {
                    if (i != 3)
                        shopItemArray[i].AdjustPosition();
                }
            }
        }
        else
        {
            lbl_ShopTitle.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part6");
            for (int i = 0; i < shopItemArray.Length; i++)
            {
                if (packDataList != null && packDataList.Count > i)
                {
                    if (i != shopItemArray.Length - 1)
                        shopItemArray[i].AdjustPosition();
                }
            }
        }
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
