using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RedPointAnimation : MonoBehaviour
{

    private void Awake()
    {
        originPosition = transform.localPosition;
    }

    public void RefreshPosition()
    {
        originPosition = transform.localPosition;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        anim = DOTween.Sequence();

        position = originPosition;
        animPosition = new Vector2(originPosition.x, originPosition.y + 4);
        transform.localScale = Vector3.one;

        PlayAnimation();
    }

    Sequence anim;
    Vector2 originPosition;
    Vector2 animPosition;
    Vector2 position;
    private void PlayAnimation()
    {
        if (anim == null || animPosition == null)
            return;
        anim.Append(gameObject.transform.DOScaleY(0.8f, 0.3f).SetEase(Ease.OutQuad));
        anim.AppendInterval(0.2f);
        anim.Insert(0.5f,gameObject.transform.DOScaleY(1f, 0.2f).SetEase(Ease.OutQuad));
        anim.AppendInterval(0.2f);
        //上升
        anim.Insert(0.7f,gameObject.transform.DOLocalMove(animPosition, 0.3f).SetEase(Ease.Linear));
        anim.Insert(0.7f, gameObject.transform.DOScaleX(1f, 0.1f).SetEase(Ease.Linear));
        anim.AppendInterval(0.1f);
        anim.Insert(1.1f, gameObject.transform.DOScaleX(0.9f, 0.1f).SetEase(Ease.Linear));
        anim.AppendInterval(0.1f);
        anim.Insert(1.2f, gameObject.transform.DOScaleX(0.8f, 0.1f).SetEase(Ease.Linear));
        anim.AppendInterval(0.1f);
        //下降
        anim.Insert(1.3f, gameObject.transform.DOScaleX(0.9f, 0.1f).SetEase(Ease.Linear));
        anim.AppendInterval(0.1f);
        anim.Insert(1.4f, gameObject.transform.DOScaleX(1f, 0.1f).SetEase(Ease.Linear));
        anim.Insert(1.4f, gameObject.transform.DOLocalMove(position, 0.2f).SetEase(Ease.Linear));
        anim.AppendInterval(0.3f);

        anim.SetLoops(-1);
        anim.SetAutoKill(false);
        anim.Play();
    }

    private void KillAnimation()
    {
        if (anim != null)
        {
            anim.Kill();
            anim.Rewind();
            anim = null;
        }
    }

    private void OnDisable()
    {
        KillAnimation();
    }
}
