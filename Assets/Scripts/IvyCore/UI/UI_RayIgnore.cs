using System.Collections;
using UnityEngine;

public class UI_RayIgnore : MonoBehaviour, ICanvasRaycastFilter
{
    float left, right, top, bottom;
    bool enable = false;
    System.Action mCallBack_ = null;
    public void SetNotIgnoreScreenZone(float l, float r, float t, float b, float dt = 0f, System.Action cb = null)
    {
        if (dt > 0f)
        {
            StartCoroutine(SetNotIgnoreScreenZoneDelay(l, r, t, b, dt, cb));
        }
        else
        {
            enable = true;
            left = l;
            right = r;
            top = t;
            bottom = b;
            mCallBack_ = cb;
            if (mCallBack_ != null)
                mCallBack_.Invoke();
        }
    }

    IEnumerator SetNotIgnoreScreenZoneDelay(float l, float r, float t, float b, float dt, System.Action cb = null)
    {
        yield return new WaitForSecondsRealtime(dt);
        SetNotIgnoreScreenZone(l, r, t, b, 0, cb);

    }

    //实现镂空区域射线穿透的接口
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        if (enable && sp.x >= left && sp.x <= right && sp.y >= bottom && sp.y <= top)
            return false;
        return true;
    }

    public void SetEnable(bool e)
    {
        enable = e;
    }
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawSphere(new Vector3(), 400);
    //}


}
