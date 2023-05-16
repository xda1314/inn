using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightingAnimation : MonoBehaviour
{
    Sequence tween;
    Transform trans = null;
    private void Start()
    {
        trans = GetComponent<Transform>();
        PlayAnimation();
    }


  
    private void PlayAnimation()
    {
        if (tween == null)
            tween = DOTween.Sequence();
        if (trans == null)
            return;

        tween.SetLoops(-1);
        trans.localPosition = Vector3.zero + Vector3.up * 20;
        tween.Append(trans.DOLocalMoveY(-20, 1f).SetEase(Ease.InQuad));
        tween.Append(trans.DOLocalMoveY(20, 1f).SetEase(Ease.OutQuad));
        tween.Play();
    }
}
