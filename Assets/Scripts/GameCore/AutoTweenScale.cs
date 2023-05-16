using DG.Tweening;
using UnityEngine;

public class AutoTweenScale : MonoBehaviour
{
    private Vector3 originScale;
    public Ease ease = Ease.InOutQuad;
    public float tweenScale = 1.2f;
    private void OnEnable()
    {
        originScale = transform.localScale;
        transform.DOKill();
        transform.DOScale(tweenScale, 1f).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        transform.DOKill();
        transform.localScale = originScale;
    }
}
