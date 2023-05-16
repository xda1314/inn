using System.Collections;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// 转场界面
/// </summary>
public class UIPanel_TransitionAnimation : UIPanelBase
{
    [SerializeField] private RectTransform transitionAnimation;
    public static bool needTween = true;
    private void Start()
    {
        transitionAnimation.anchoredPosition = new Vector2(0, transitionAnimation.rect.height);
    }

    public IEnumerator TransitionAnimationDown()
    {
        yield return null;
        if (needTween) 
        {
            transitionAnimation.DOLocalMoveY(0, 0.3f).SetEase(Ease.InQuad);
            yield return new WaitForSeconds(0.3f);
        }      
    }
    public IEnumerator TransitionAnimationUp()
    {
        yield return null;
        if (needTween) 
        {
            yield return new WaitForSeconds(0.2f);
            transitionAnimation.DOLocalMoveY(transitionAnimation.rect.height, 0.3f).SetEase(Ease.InQuad);
        }    
    }
}
