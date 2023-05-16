using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonScale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Transform tweenTarget;
    [LabelText("手动设置初始缩放比例"), SerializeField] private bool _manualTag = false;
    [LabelText("初始缩放比例:"), ShowIf("_manualTag"), SerializeField] private Vector3 saveScale = Vector3.one;
    [LabelText("是否需要呼吸动画"), SerializeField] private bool needBreath = false;
    void Awake()
    {
        if (tweenTarget == null)
        {
            tweenTarget = transform;
        }
        if (!_manualTag)
            saveScale = tweenTarget.localScale;
    }
    private void OnEnable()
    {
        TryShowBreathTween();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        KillAnimation();
        //tweenTarget.localScale = saveScale * 0.9f;
        tweenTarget.DOKill();
        tweenTarget.DOScale(saveScale * 0.9f, 0.06f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //tweenTarget.localScale = saveScale;
        tweenTarget.DOKill();
        tweenTarget.DOScale(saveScale, 0.06f);
    }


    Sequence anim;
    private void TryShowBreathTween()
    {
        if (needBreath)
        {
            if (anim == null)
            {
                anim = DOTween.Sequence();
            }
            anim.Append(gameObject.transform.DOScale(Vector3.one * 0.94f, 0.35f).SetEase(Ease.InOutSine));
            anim.Insert(0.35f, gameObject.transform.DOScale(Vector3.one * 1.1f, 0.55f).SetEase(Ease.InOutSine));
            anim.Insert(0.9f, gameObject.transform.DOScale(Vector3.one * 0.98f, 0.35f).SetEase(Ease.InOutSine));
            anim.Insert(1.25f, gameObject.transform.DOScale(Vector3.one * 1f, 0.35f).SetEase(Ease.InOutSine));

            anim.SetDelay(1f);
            anim.SetLoops(-1);
            anim.Play();
        }
    }

    private void KillAnimation()
    {
        if (needBreath)
        {
            if (anim != null)
            {
                anim.Kill();
                anim = null;
            }
        }
    }
}
