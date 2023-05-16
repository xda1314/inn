using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIPanel_CompleteLevel : UIPanelBase
{
    [SerializeField] TextMeshProUGUI t_Complete;

    public override IEnumerator OnShowUI()
    {
        t_Complete.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/OrdersDescribe");
        t_Complete.transform.localScale = Vector3.zero;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(t_Complete.transform.DOScale(Vector3.one * 1.3f, 1f));
        sequence.Insert(1f, t_Complete.transform.DOScale(Vector3.one, 0.5f));
        sequence.Play();
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.complete);
        yield return base.OnShowUI();
        StartCoroutine(ShowJigsaw());
    }


    private IEnumerator ShowJigsaw()
    {
        yield return new WaitForSeconds(3);
        UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_Jigsaw, UIShowLayer.TopPopup), null, () =>
        {
            UISystem.Instance.HideUI(this);
        });
    }
}
