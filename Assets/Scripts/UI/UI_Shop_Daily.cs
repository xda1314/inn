using ivy.game;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 每日商场
/// </summary>
public class UI_Shop_Daily : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI lbl_Title;
    [SerializeField] private TextMeshProUGUI lbl_CountDown;
    [SerializeField] private UI_Shop_NormalItem[] normalItemArray;
    public static event Action<bool> RefreshRedPointState;
    public float height;
    private bool canUpdate = false;
    private bool allSellOut;

    private List<ShopPackData> GetShopPackDataList(List<ShopPackData> packDataList)
    {
        List<ShopPackData> newPackDataList = new List<ShopPackData>();
        Dictionary<int, List<ShopPackData>> dic = new Dictionary<int, List<ShopPackData>>();
        for (int i = 0; i < packDataList.Count; i++)
        {
            if (ShopPackDefinition.DefinitionMap.TryGetValue(packDataList[i].saveID, out ShopPackDefinition shopPackDefinition))
            {
                if (!dic.ContainsKey(shopPackDefinition.order))
                {
                    dic.Add(shopPackDefinition.order, new List<ShopPackData>());
                }
                if (dic.TryGetValue(shopPackDefinition.order, out List<ShopPackData> list))
                {
                    list.Add(packDataList[i]);
                }
            }
        }
        foreach (var item in dic.Values)
        {
            if (item.Count <= 0)
            {
                GameDebug.LogError("ShopPackData error");
            }
            else if (item.Count == 1)
            {
                newPackDataList.Add(item[0]);
            }
            else
            {
                int unlock = int.Parse(TimeManager.Instance.UtcNow().DayOfYear.ToString());
                int random = unlock % item.Count;
                newPackDataList.Add(item[random]);
            }
        }

        return newPackDataList;
    }

    public void RefreshUIData(List<ShopPackData> packDataList1, Action<ShopPackData, Vector3> cb)
    {
        List<ShopPackData> packDataList = GetShopPackDataList(packDataList1);
        allSellOut = true;
        canUpdate = true;
        lbl_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Title/Part1");
        lbl_CountDown.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/SpinWheel_Describe1") + " " + MyTimer.ReturnTextUntilSecond_MaxShowTwo((int)TimeManager.Instance.GetTomorrowRefreshTimeSpan().TotalSeconds);
        for (int i = 0; i < normalItemArray.Length; i++)
        {
            int index = i;
            if (packDataList != null && packDataList.Count > i)
            {
                normalItemArray[i].gameObject.SetActive(true);
                normalItemArray[i].SetItemInfo(packDataList[index], cb);
                //如果sellout全为true，allsellou 为true
                if (packDataList[i].currencyID == CurrencyID.Free || packDataList[i].currencyID == CurrencyID.AD)
                {
                    if (!normalItemArray[i].sellOut)
                    {
                        allSellOut = false;
                    }
                    GameObject redPoint = AssetSystem.Instance.Instantiate(Consts.UI_RedPoint, normalItemArray[i].transform);
                    if (redPoint != null && normalItemArray[i].redPoint == null)
                    {
                        redPoint.SetActive(false);
                        if (normalItemArray[i].sellOut)
                        {
                            redPoint.SetActive(false);
                            continue;
                        }
                        else
                        {
                            redPoint.transform.localPosition = new Vector2(131, -70);
                            if (redPoint.TryGetComponent(out RedPointAnimation anim))
                            {
                                anim.RefreshPosition();
                            }
                            redPoint.SetActive(true);
                            normalItemArray[i].redPoint = redPoint;
                            redPoint.SetActive(true);
                        }
                    }
                    else
                    {
                        AssetSystem.Instance.DestoryGameObject(Consts.UI_RedPoint, redPoint);
                    }
                }
                if (normalItemArray[i].sellOut && normalItemArray[i].redPoint != null)
                {
                    normalItemArray[i].redPoint.SetActive(false);
                }
            }
            else
            {
                normalItemArray[i].gameObject.SetActive(false);
            }
        }
        RefreshRedPointState?.Invoke(!allSellOut);
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
        lbl_CountDown.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/SpinWheel_Describe1") +" "+ MyTimer.ReturnTextUntilSecond_MaxShowTwo((int)TimeManager.Instance.GetTomorrowRefreshTimeSpan().TotalSeconds);
    }
}
