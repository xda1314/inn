using DG.Tweening;
using I2.Loc;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelData_Tutorial :UIPanelDataBase
{
    public TutorialType tutorialType { get; private set; }
    public UIPanelData_Tutorial(TutorialType type) : base(Consts.UIPanel_Tutorial_Merge)
    {
        tutorialType = type;
    }
}
public class UIPanel_Tutorial_Merge : UIPanelBase
{
    [SerializeField] private GameObject[] tutorRoot;
    [SerializeField] private TextMeshProUGUI t_Title;
    [SerializeField] private Button btn_Close;
    [SerializeField] private TextMeshProUGUI t_Close;

    private TutorialType tutorialType = TutorialType.none;
    private CanvasGroup canvas_Button;
    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_Close.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
        canvas_Button = btn_Close.GetComponent<CanvasGroup>();
    }
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        tutorialType = ((UIPanelData_Tutorial)UIPanelData).tutorialType;
        ShowAnimation();
        canvas_Button.alpha = 0;
        Invoke("InvokeShowBtn", 1f);
        t_Title.text = ScriptLocalization.Get("Obj/Teach/Basic/Title");
        t_Close.text = ScriptLocalization.Get("Obj/Teach/Basic/Btn");
    }
    private void InvokeShowBtn() 
    {
        DOTween.To(() => canvas_Button.alpha, (alpha) =>
        {
            canvas_Button.alpha = alpha;
        }, 1, 0.5f);
    }
    private void ShowAnimation() 
    {
        for (int i = 0; i < tutorRoot.Length; i++)
        {
            if (tutorRoot[i].activeSelf)
                tutorRoot[i].SetActive(false);
        }
        try
        {
            switch (tutorialType)
            {
                case TutorialType.merge:
                    tutorRoot[0].SetActive(true);
                    btn_Close.transform.localPosition = new Vector3(0, -350, 0);
                    break;
                case TutorialType.store:
                    tutorRoot[1].SetActive(true);
                    btn_Close.transform.localPosition = new Vector3(0, -450, 0);
                    break;
                default:
                    break;
            }
        }
        catch (Exception e) 
        {
            GameDebug.LogError(e);
        }
      
    }

}
public enum TutorialType 
{
    none,
    merge,
    store,
}
