using I2.Loc;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelData_FixSell : UIPanelDataBase
{
    public string prefabName;
    public UIPanelData_FixSell(string name) : base(Consts.UIPanel_FixSell)
    {
        prefabName = name;
    }
}

public class UIPanel_FixSell : UIPanelBase
{
    [SerializeField] private Transform TF_ItemBg;
    [SerializeField] private TextMeshProUGUI T_ItemName;
    [SerializeField] private TextMeshProUGUI T_Describe;
    [SerializeField] private Button Btn_FixSell;
    [SerializeField] private TextMeshProUGUI T_FixSell;
    [SerializeField] private Button Btn_Cancel;
    [SerializeField] private TextMeshProUGUI T_Cancel;

    private string prefabName;
    private GameObject saveGo;
    public override void OnInitUI()
    {
        base.OnInitUI();
        Btn_FixSell.onClick.AddListener(FixBtnClick);
        Btn_Cancel.onClick.AddListener(CloseBtnClick);
    }
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        prefabName = ((UIPanelData_FixSell)UIPanelData).prefabName;
        Refresh();
    }
    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
        AssetSystem.Instance.DestoryGameObject(prefabName, saveGo);
    }

    private void Refresh()
    {
        T_Describe.text =ScriptLocalization.Get("Obj/Eliminate/DoubleConfirmation/Text1");
        T_Cancel.text = ScriptLocalization.Get("Obj/Eliminate/DoubleConfirmation/Text2");
        T_FixSell.text = ScriptLocalization.Get("Obj/Eliminate/DoubleConfirmation/Text3");
        if (MergeItemDefinition.TotalDefinitionsDict.TryGetValue(prefabName, out MergeItemDefinition definition))
        {
            T_ItemName.text = ScriptLocalization.Get(definition.locKey_Name);
            saveGo = AssetSystem.Instance.Instantiate(prefabName, TF_ItemBg);
        }
    }
    private void FixBtnClick()
    {
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.unlockSellReturnBtn);
        MergeController.CurrentController.FixSellItem();
        UISystem.Instance.HideUI(this);
    }
    private void CloseBtnClick()
    {
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
        UISystem.Instance.HideUI(this);
    }
}
