using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyItem : MonoBehaviour
{
    #region 组件
    [SerializeField] private Transform partentTF;
    [SerializeField] private TextMeshProUGUI textCount;
    #endregion

    #region 变量
    GameObject gO = null;
    #endregion

    public void SetData(MergeRewardItem rewardItem) 
    {
        if (gO != null)
            DestroyImmediate(gO);
        gO = AssetSystem.Instance.Instantiate(rewardItem.ShowRewardPrefabName, partentTF);
        if(rewardItem.num > 1) 
        {
            textCount.text ="x" + rewardItem.num.ToString();
        }
        else 
        {
            textCount.text = "";
        }
        
    }
}
