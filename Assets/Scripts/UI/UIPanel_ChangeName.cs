using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 改名弹窗
/// </summary>
public class UIPanel_ChangeName :UIPanelBase
{
    [SerializeField] private TextMeshProUGUI text_Title;
    [SerializeField] private InputField intputPlayerName;
    [SerializeField] private Text text_ShowInputName;
    [SerializeField] private Button btn_Fix;
    [SerializeField] private TextMeshProUGUI text_Fix;
    [SerializeField] private Button btn_Close;
    [SerializeField] private TextMeshProUGUI text_des;
    private Page_Setting panel;
    private void Awake()
    {
        base.uiType = UIType.Tip;
    }
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        text_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Set/NameButton/Title");
        text_ShowInputName.text = I2.Loc.ScriptLocalization.Get("Obj/Set/NameButton/Text1");
        text_Fix.text = I2.Loc.ScriptLocalization.Get("Obj/Set/NameButton/Text3");
        text_des.text= I2.Loc.ScriptLocalization.Get("Obj/Set/NameButton/Text1");
        intputPlayerName.text = I2.Loc.ScriptLocalization.Get("Obj/Set/NameButton/Text2");
        btn_Close.onClick.AddListener(()=>UISystem.Instance.HideUI(this, null));
        btn_Fix.onClick.AddListener(OKBtnClock);
    }


    public static event Action<string> changeName;
    private void OKBtnClock()
    {
        if (intputPlayerName.text != "")
        {
            PlayerData.SetPlayerName(intputPlayerName.text);
            changeName?.Invoke(intputPlayerName.text);

            //if (UISystem.Instance.TryGetUI(Consts.UIPanel_Setting, out UIPanelBase popup))
            //{
            //    UIPanel_Setting panel = popup as UIPanel_Setting;
            //    if (panel)
            //    {
            //        panel.ChangePlayerName(intputPlayerName.text);
            //    }
            //}
            GameManager.Instance.UploadNameOrProfileToCloud();
        }
        UISystem.Instance.HideUI(this, null);
    }
}
