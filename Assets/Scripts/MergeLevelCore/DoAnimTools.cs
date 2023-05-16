using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class DoAnimTools
{
    //public class DoAnimTweenInfo
    //{
    //    public Transform doTrans;
    //    public Sequence doContainer;
    //    public Action<string> doEndFunc;
    //}

    //public static Dictionary<MergeItem, DoAnimTweenInfo> DoEndPools = new Dictionary<MergeItem, DoAnimTweenInfo>();


    //public static bool HasDoAnim(this MergeItem item)
    //{
    //    if (DoEndPools.TryGetValue(item, out DoAnimTweenInfo value))
    //    {
    //        return true;
    //    }
    //    return false;
    //}


    //public static void StopAllDoAnim()
    //{
    //    if (!DoEndPools.IsNullOrEmpty())
    //    {
    //        foreach (MergeItem item in DoEndPools.Keys)
    //        {
    //            DoEndPools[item].doContainer?.Kill();
    //            DoEndPools[item].doEndFunc?.Invoke("stop");
    //            item.IsDoAnim = false;
    //        }
    //        DoEndPools.Clear();
    //    }
    //}

    //public static bool StopDoAnim(this MergeItem item, string message = "success")
    //{
    //    if (item.IsDoAnim && DoEndPools.TryGetValue(item, out DoAnimTweenInfo value))
    //    {
    //        if (!"success".Equals(message))
    //        {
    //            value.doContainer?.Kill();
    //        }
    //        value.doEndFunc?.Invoke(message);
    //        item.IsDoAnim = false;
    //        DoEndPools.Remove(item);
    //        return true;
    //    }
    //    return false;
    //}

    //public static void JustDoAnim(MergeItem item, Sequence s, Action<string> onEndfunc)
    //{
    //    DoAnimTweenInfo tweenInfo = new DoAnimTweenInfo();
    //    tweenInfo.doTrans = item.transform;
    //    tweenInfo.doContainer = s;
    //    tweenInfo.doEndFunc = onEndfunc;
    //    DoEndPools.Add(item, tweenInfo);
    //    s.Play();
    //    s.OnComplete(() =>
    //    {
    //        item.StopDoAnim();
    //    });
    //}


    //public static void DoBoxJump(this MergeItem item, float jump, float scaleX, float angle, float time, Action completeCallBack)
    //{
    //    item.StopDoAnim();
    //    item.IsDoAnim = true;
    //    Sequence s = DOTween.Sequence();
    //    Vector3 defaultPosition = item.spriteRootTran.localPosition;
    //    Vector3 defaultScale = item.spriteRootTran.localScale;
    //    s.Append(item.spriteRootTran.DOScaleX(defaultScale.x * scaleX, time / 8).SetEase(Ease.OutQuad));
    //    s.Append(item.spriteRootTran.DOScaleX(defaultScale.x, time / 8).SetEase(Ease.OutQuad));
    //    s.Insert(time / 8, item.spriteRootTran.DOLocalMoveY(defaultPosition.y + jump, time / 8).SetEase(Ease.OutQuad));
    //    s.Append(item.spriteRootTran.DORotate(new Vector3(0, 0, angle), time / 8).SetEase(Ease.OutQuad));
    //    s.Append(item.spriteRootTran.DORotate(new Vector3(0, 0, 0), time / 8).SetEase(Ease.OutQuad));
    //    s.Append(item.spriteRootTran.DORotate(new Vector3(0, 0, angle), time / 8).SetEase(Ease.OutQuad));
    //    s.Append(item.spriteRootTran.DORotate(new Vector3(0, 0, 0), time / 8).SetEase(Ease.OutQuad));
    //    s.Append(item.spriteRootTran.DOScaleX(defaultScale.x * scaleX, time / 8).SetEase(Ease.OutQuad));
    //    s.Append(item.spriteRootTran.DOScaleX(defaultScale.x, time / 8).SetEase(Ease.OutQuad));
    //    s.Insert(time * 3 / 4, item.spriteRootTran.DOLocalMoveY(defaultPosition.y, time / 8).SetEase(Ease.OutQuad));
    //    s.AppendInterval(1f);
    //    JustDoAnim(item, s, delegate (string message)
    //    {
    //        item.spriteRootTran.localScale = defaultScale;
    //        item.spriteRootTran.localPosition = defaultPosition;
    //        completeCallBack?.Invoke();
    //    });
    //}


    //public static void DoScalePivotTwice(this MergeItem item, float toScale, float time, Action completeCallBack)
    //{
    //    item.StopDoAnim();
    //    item.IsDoAnim = true;
    //    Sequence s = DOTween.Sequence();
    //    Vector3 defaultScale = item.spriteRootTran.localScale;
    //    float fromScale = defaultScale.y;
    //    toScale = fromScale * toScale;
    //    UIWidget uiwidget = item.spriteRootTran.GetComponent<UIWidget>();
    //    UIWidget.Pivot prePivot = uiwidget.pivot;
    //    Vector3 defaultPosition = uiwidget.cachedTransform.localPosition;
    //    UIWidget[] componets = item.spriteRootTran.GetComponentsInChildren<UIWidget>();
    //    Dictionary<UIWidget, Vector3> widgetVectors = new Dictionary<UIWidget, Vector3>();
    //    for (int i = 0, len = componets.Length; i < len; i++)
    //    {
    //        if (componets[i] != uiwidget)
    //        {
    //            widgetVectors.Add(componets[i], componets[i].cachedTransform.localPosition);
    //            if (componets[i].GetComponent<UITweener>() != null)
    //            {
    //                componets[i].GetComponent<UITweener>().timeScale = 0;
    //            }
    //        }
    //    }
    //    uiwidget.pivot = UIWidget.Pivot.Bottom;
    //    foreach (UIWidget widget in widgetVectors.Keys)
    //    {
    //        widget.pivot = UIWidget.Pivot.Bottom;
    //    }
    //    s.AppendInterval(0.1f).Append(item.spriteRootTran.DOScale(toScale, time / 4).SetEase(Ease.OutQuad));
    //    s.Insert(0.1f + time / 4, item.spriteRootTran.DOScale(defaultScale, time / 4).SetEase(Ease.InQuad));
    //    s.Insert(0.1f + time / 2, item.spriteRootTran.DOScale(toScale, time / 4).SetEase(Ease.OutQuad));
    //    s.Insert(0.1f + time * 3 / 4, item.spriteRootTran.DOScale(defaultScale, time / 4).SetEase(Ease.InQuad));
    //    s.OnUpdate(() =>
    //    {
    //        foreach (UIWidget widget in widgetVectors.Keys)
    //        {
    //            widget.cachedTransform.localPosition = widgetVectors[widget];
    //        }
    //    });
    //    JustDoAnim(item, s, delegate (string message)
    //    {
    //        uiwidget.pivot = prePivot;
    //        foreach (UIWidget widget in widgetVectors.Keys)
    //        {
    //            widget.pivot = prePivot;
    //        }
    //        item.spriteRootTran.localScale = defaultScale;
    //        item.spriteRootTran.localPosition = defaultPosition;
    //        foreach (UIWidget widget in widgetVectors.Keys)
    //        {
    //            widget.cachedTransform.localPosition = widgetVectors[widget];
    //            if (widget.GetComponent<UITweener>() != null)
    //            {
    //                widget.GetComponent<UITweener>().timeScale = 1;
    //            }
    //        }
    //        completeCallBack?.Invoke();
    //    });
    //}


    //public const float d_Distance_frame = 2f;

    //public static void DoParabolaWithScale(this MergeItem item, Vector2 start, Vector2 end, float time, Action completeCallBack)
    //{
    //    item.StopDoAnim();
    //    item.IsDoAnim = true;
    //    Sequence s = DOTween.Sequence();
    //    Vector3 defaultScale = item.transform.localScale;
    //    item.transform.localScale = Vector3.zero;
    //    s.Append(DOTween.To(setter: value =>
    //    {
    //        float height = Vector2.Distance(start, end) / d_Distance_frame;
    //        Vector2 vector = Parabola(start, end, height, value);
    //        item.SetRootPosition(vector.x, vector.y);
    //    }, startValue: 0, endValue: 1, duration: time).SetEase(Ease.OutQuad));
    //    s.Insert(0, item.transform.DOScale(defaultScale, time / 2).SetEase(Ease.OutQuad));
    //    JustDoAnim(item, s, delegate (string message)
    //    {
    //        item.SetRootPosition(end.x, end.y);
    //        completeCallBack?.Invoke();
    //    });
    //}

    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        float Func(float x) => 4 * (-height * x * x + height * x);

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, Func(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
    {
        float Func(float x) => 4 * (-height * x * x + height * x);

        var mid = Vector2.Lerp(start, end, t);

        return new Vector2(mid.x, Func(t) + Mathf.Lerp(start.y, end.y, t));
    }


    public static Vector2 Bezier(Vector2 p0, Vector2 p1, Vector2 p2, float t)
    {
        return (1 - t) * (1 - t) * p0 + 2 * t * (1 - t) * p1 + t * t * p2;
    }

}
