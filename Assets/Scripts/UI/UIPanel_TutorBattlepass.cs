using DG.Tweening;
using I2.Loc;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel_TutorBattlepass : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI t_Title;
    [SerializeField] private TextMeshProUGUI t_Desc;
    [SerializeField] private Button btn_Close;
    [SerializeField] private TextMeshProUGUI t_Continue;
    [SerializeField] private TextMeshProUGUI[] t_ItemName;
    [SerializeField] private GameObject[] tweenObjects;

    private CanvasGroup canvasGroup;
    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_Close.onClick.AddListener(() => ShowPanel(false));
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        ShowPanel(true);
        RefreshUI();
    }
    private void RefreshUI()
    {
        t_Continue.text = ScriptLocalization.Get("Obj/Chain/Home_Button1");
        t_Desc.text = ScriptLocalization.Get("Obj/BattlePass/Help/Text5");
        t_ItemName[0].text = ScriptLocalization.Get("Obj/BattlePass/Help/Text2");
        t_ItemName[1].text = ScriptLocalization.Get("Obj/BattlePass/Help/Text3");
        t_ItemName[2].text = ScriptLocalization.Get("Obj/BattlePass/Help/Text4");
        //if (true) 
        //{
        //    for (int i = 0; i < tweenObjects.Length; i++)
        //    {
        //        tweenObjects[i].SetActive(false);
        //    }
        //    StartCoroutine(SetObjectActive());
        //}
    }
    private IEnumerator SetObjectActive() 
    {
        for (int i = 0; i < tweenObjects.Length; i++)
        {
            tweenObjects[i].SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void ShowPanel(bool show)
    {
        if (show)
        {
            canvasGroup.alpha = 0;
            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, 0.5f).SetEase(Ease.Linear);
        }
        else
        {
            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, 0.5f).SetEase(Ease.Linear).onComplete += () =>
            {
                UISystem.Instance.HideUI(this);
            };
        }
    }
}
