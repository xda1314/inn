using I2.Loc;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBg_Storage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_LblAddNewGrid;
    [SerializeField] private TextMeshProUGUI m_UnLockCost;
    [SerializeField] private Button m_UnLockBtn;
    [SerializeField] private GameObject m_IsVip;
    [SerializeField] private GameObject unlockGridBg;

    private UIPanel_Store m_StoreParent;
    public void SetParent(UIPanel_Store panel)
    {
        m_StoreParent = panel;
    }



    private void Start()
    {     
        m_UnLockBtn.onClick.AddListener(UnlockBtnClick);
    }
    /// <summary>
    /// 刷新格子背景
    /// </summary>
    public void RefreshGridBg(bool isVip)
    {
        m_UnLockBtn.gameObject.SetActive(false);
        m_LblAddNewGrid.gameObject.SetActive(false);
        m_IsVip.SetActive(isVip);        
    }
    /// <summary>
    /// 刷新购买
    /// </summary>
    public void RefreshBuyGrid()
    {
        int num = StoreManager.Instance.maxFreeStoreGridCount + 1;
        StorageDefinition.TotalDefinitionsDict.TryGetValue(num, out int unlockCost);
        m_UnLockCost.text = unlockCost.ToString();
        unlockGridBg.SetActive(true);
        m_LblAddNewGrid.text = ScriptLocalization.Get("Obj/Eliminate/InfoBar/Button1");
    }


    /// <summary>
    /// 点击解锁新格子
    /// </summary>
    private void UnlockBtnClick()
    {
        ////AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
        //------判断是否已解锁到最大数量----
        if (StoreManager.Instance.curGridNumWithoutBP >= StorageDefinition.MaxGridNum)
        {
            Debug.LogError("You have obtained the maximum number of grid!");
            return;
        }
        //------判断金币是否够解锁--------
        StorageDefinition.TotalDefinitionsDict.TryGetValue(StoreManager.Instance.maxFreeStoreGridCount + 1, out int needCostNum);
        bool hasEnoughCoins = Currencies.Spend(CurrencyID.Coins, needCostNum, "unlockStore");
        if (!hasEnoughCoins)
        {
            return;
        }
        m_StoreParent.UnlockNewGrid();

    }
}

