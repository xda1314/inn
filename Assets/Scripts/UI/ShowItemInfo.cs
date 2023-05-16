using Ivy.UI;
using IvyCore;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShowItemInfo : MonoBehaviour
{
    private UIPanelBase curPanel;
    [SerializeField] private GameObject Icon_Lightning;
    public void RefreshPrefabName(string prefabName, UIPanelBase curPanel) //传的是点击的物品名字，不是按钮的名字
    {
        this.curPanel = curPanel;
        MergeItemDefinition.TotalDefinitionsDict.TryGetValue(prefabName, out MergeItemDefinition m_Definition);
        if (Icon_Lightning != null)
            Icon_Lightning.gameObject.SetActive(m_Definition.NeedEnergy);

        Button btn = gameObject.GetComponent<Button>();
        if (btn != null && m_Definition != null)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => ShowTipBtnClick(m_Definition));
        }
    }


    private void ShowTipBtnClick(MergeItemDefinition m_Definition)
    {
        if (m_Definition == null)
        {
            GameDebug.LogError("MergeItemDefinition is null!");
            return;
        }

        if (UI_TutorManager.Instance.IsTutoring()
            && UI_TutorManager.Instance.GetCurrentRunningTutorData() != null
            && UI_TutorManager.Instance.GetCurrentRunningTutorData().Name == "InnAdventure1")
        {
            return;
        }

        if (m_Definition.WindowType == MergeItemWindowType.universal)
            UISystem.Instance.ShowUI(new UIPanelData_ShowTip(m_Definition.PrefabName), true);
        else if (m_Definition.WindowType == MergeItemWindowType.count)
            UISystem.Instance.ShowUI(new UIPanelData_ShowCountTip(m_Definition.PrefabName), true);
        else if (m_Definition.WindowType == MergeItemWindowType.output)
            UISystem.Instance.ShowUI(new UIPanelData_ShowOutputTip(m_Definition.PrefabName));
        else if (m_Definition.WindowType == MergeItemWindowType.special)
            UISystem.Instance.ShowUI(new UIPanelData_ShowSpecialTip(m_Definition.PrefabName));
        else if (m_Definition.WindowType == MergeItemWindowType.rare)
            UISystem.Instance.ShowUI(new UIPanelData_ShowRareTip(m_Definition.PrefabName));

        int showTipPanelCount = 0;
        List<string> checkList = new List<string>
        {
            Consts.UIPanel_ShowTip,
            Consts.UIPanel_ShowCountTip,
            Consts.UIPanel_ShowOutputTip,
            Consts.UIPanel_ShowSpecialTip,
            Consts.UIPanel_ShowRareTip
        };
        if (UISystem.Instance.TryGetUIList(checkList, out List<UIBase> allUIList))
        {
            showTipPanelCount = allUIList.Count;
        }
        if (showTipPanelCount >= 3)
        {
            if (curPanel != null)
            {
                UISystem.Instance.HideUI(curPanel);
            }
        }
    }
}
