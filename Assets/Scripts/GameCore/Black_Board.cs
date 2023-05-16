using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black_Board
{
    //不管链接上firestore还是没链接上 都会读下这张表
    public const string CollectionName = "black_board";

    public const string Blocked_account_All = "core_block_account_all";
    public const string Blocked_account_Cloud = "core_block_account_cloud";

    public static Black_Board Instance = new Black_Board();

    /// <summary>
    /// 账号已被封禁
    /// </summary>
    public static bool IsBlockAccount_All { get; private set; } = false;
    /// <summary>
    /// 账号已被封禁，但可以在本地玩，禁止联网
    /// </summary>
    public static bool IsBlockAccount_CanPlayLocal { get; private set; } = false;

    /// <summary>
    /// 检查封禁情况
    /// </summary>
    public void CheckBlockAccountData()
    {
        IsBlockAccount_All = SaveUtils.GetBool(Blocked_account_All, false);
        IsBlockAccount_CanPlayLocal = SaveUtils.GetBool(Blocked_account_Cloud, false);
        if (IsBlockAccount_All)
        {
            GameManager.Instance.StopCoroutine(CheckBlockUI());
            GameManager.Instance.StartCoroutine(CheckBlockUI());
        }
    }

    public IEnumerator CheckBlockUI()
    {
        while (true)
        {
            if (!IsBlockAccount_All)
            {
                UISystem.Instance.HideUI("UIPanel_BlackBoard");
                yield break;
            }

            if (UISystem.Instance.TryGetUI<UIPanel_BlackBoard>("UIPanel_BlackBoard", out _))
            {
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                UISystem.Instance.ShowUI(new UIPanelDataBase("UIPanel_BlackBoard", UIShowLayer.Top));
                yield return null;
            }
        }
    }

    /// <summary>
    /// 封禁账号
    /// </summary>
    public void BlockAccount(bool onlyLocal = false)
    {
        if (onlyLocal)
        {
            IsBlockAccount_All = false;
            IsBlockAccount_CanPlayLocal = true;
        }
        else
        {
            IsBlockAccount_All = true;
            IsBlockAccount_CanPlayLocal = false;
        }
        SaveUtils.SetBool(Blocked_account_All, IsBlockAccount_All);
        SaveUtils.SetBool(Blocked_account_Cloud, IsBlockAccount_CanPlayLocal);
        CheckBlockAccountData();
    }

    /// <summary>
    /// 解除封禁账号
    /// </summary>
    public void UnBlockAccount()
    {
        IsBlockAccount_All = false;
        IsBlockAccount_CanPlayLocal = false;
        SaveUtils.SetBool(Blocked_account_All, IsBlockAccount_All);
        SaveUtils.SetBool(Blocked_account_Cloud, IsBlockAccount_CanPlayLocal);

        if (UISystem.Instance.TryGetUI<UIPanel_BlackBoard>("UIPanel_BlackBoard", out var panel))
        {
            UISystem.Instance.HideUI(panel);
        }
    }


    public void OnReadFirestoreSuccess(string jsonStr)
    {
        try
        {
            if (string.IsNullOrEmpty(jsonStr) || jsonStr == "{}")
            {
                UnBlockAccount();
                return;
            }

            if (!string.IsNullOrEmpty(jsonStr))
            {
                Dictionary<string, object> Data = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);
                if (Data != null)
                {
                    if (Data.TryGetValue("block", out var blockObj))
                    {
                        if (int.TryParse(blockObj.ToString(), out int blockTemp) && blockTemp == 1)
                        {
                            // 封禁账号
                            BlockAccount();
                            return;
                        }
                    }
                    else if (Data.TryGetValue("block_local", out var blockLocalObj))
                    {
                        if (int.TryParse(blockLocalObj.ToString(), out int blockLocalTemp) && blockLocalTemp == 1)
                        {
                            // 仅封禁本地账号
                            BlockAccount(true);
                            return;
                        }
                    }
                }
            }
            UnBlockAccount();
        }
        catch (Exception e)
        {
            DebugSetting.LogError(e, "Black Board Parse error!");
        }
    }

}