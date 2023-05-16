using IvyCore;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel_ChooseLanguage : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI text_Title;
    [SerializeField] private Button[] btn_Language;
    [SerializeField] private Button btn_Close;
    [SerializeField] private Image img_btn_brown;
    [SerializeField] private Image img_btn_green;

    List<string> languageList = new List<string>();

    public override void OnInitUI()
    {
        base.OnInitUI();
        text_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Set/Button/Text6");
        btn_Close.onClick.AddListener(CloseButtonClick);
        languageList = LanguageManager.GetLanguageToDisplay();
        RefrshText();
        SetButtonState();
    }

    private void RefrshText()
    {
        for (int i = 0; i < btn_Language.Length; i++)
        {
            int index = i;
            btn_Language[i].onClick.AddListener(() => ChooseLanguageClick(index));
            if (btn_Language[i].transform.GetChild(0).TryGetComponent(out Text text) && i < languageList.Count)
            {
                text.text = languageList[i];
                btn_Language[i].gameObject.SetActive(true);
            }
            else
                btn_Language[i].gameObject.SetActive(false);
            if (btn_Language[i].transform.GetChild(1).TryGetComponent(out Text text1) && i < languageList.Count)
            {
                text1.text = languageList[i];
            }
            btn_Language[i].gameObject.AddComponent<UIButtonScale>();
        }
    }
    private void SetButtonState()
    {
        int index = 0;
        string language = LanguageManager.CurrentLangaugeCode;
        switch (language)
        {
            case "en":
                SetLanguageUI(0);
                break;
            case "es":
                SetLanguageUI(1);
                break;
            case "de":
                SetLanguageUI(2);
                break;
            case "fr":
                SetLanguageUI(3);
                break;
            case "it":
                SetLanguageUI(4);
                break;
            case "pt-BR":
                SetLanguageUI(5);
                break;
            case "ko":
                SetLanguageUI(6);
                break;
            case "ja":
                SetLanguageUI(7);
                break;
            case "ru":
                SetLanguageUI(8);
                break;
            case "id":
                SetLanguageUI(9);
                break;
            case "zh-TW":
                SetLanguageUI(10);
                break;
            case "zh-CN":
                SetLanguageUI(11);
                break;
        }
    }

    private void SetLanguageUI(int index)
    {
        if (btn_Language == null)
            return;

        for (int i = 0; i < btn_Language.Length; i++)
        {
            if (i == index)
                btn_Language[i].transform.GetComponent<Image>().sprite = img_btn_brown.sprite;
            else
                btn_Language[i].transform.GetComponent<Image>().sprite = img_btn_green.sprite;
            btn_Language[i].transform.Find("text_brown").gameObject.SetActive(i == index);
            btn_Language[i].transform.Find("text_green").gameObject.SetActive(i != index);
        }
    }

    private void ChooseLanguageClick(int index)
    {
        string chooseLanguage = languageList[index];
        // 检测是否是需要下载的语言
        if (LanguageManager.LocalizedLanguageNameToCode.TryGetValue(chooseLanguage, out var languageCode))
        {
            // 直接切换
            ChangeFont(index, chooseLanguage);
        }
        else
        {
            GameDebug.LogError("无法找到此字体");
        }
    }

    private void ChangeFont(int index, string chooseLanguage)
    {
        LanguageManager.SetNewLanguage(chooseLanguage);
        if (this != null)
        {
            SetLanguageUI(index);
            if (text_Title != null)
                text_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Set/Button/Text6");
        }
        Invoke("ChangeFontCB", 0);
    }

    private void ChangeFontCB()
    {
        IvyCore.UI_Manager.Instance.InvokeRefreshEvent("", "RefreshEvent_LanguageChanged");
    }

    private void CloseButtonClick()
    {
        UISystem.Instance.HideUI(this, null);
    }
}
