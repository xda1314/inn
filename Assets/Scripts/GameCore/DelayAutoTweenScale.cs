using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DelayAutoTweenScale : MonoBehaviour
{
    private Vector3 originScale;
    private Sequence sequence;
    private void OnEnable()
    {
        originScale = transform.localScale;
        transform.DOKill();
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1.1f, 0.375f).SetEase(Ease.InOutQuad).SetLoops(4, LoopType.Yoyo));
        sequence.AppendInterval(4.0f);
        sequence.SetLoops(-1);
    }

    private void OnDisable()
    {
        if (sequence != null) 
        {
            sequence.Kill();
        }
        transform.DOKill();
        transform.localScale = originScale;
    }
}
