using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI_TutorClick : MonoBehaviour, IPointerClickHandler, IPointerDownHandler/*, IPointerUpHandler */
{
    public UnityEvent OnClick;
    public UnityEvent OnPointerUpEvent;
    bool mEventUsed = false;
    Vector2 PointDownPos_ = Vector2.zero;
    public void Awake()
    {
        OnClick = new UnityEvent();
        OnPointerUpEvent = new UnityEvent();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointDownPos_ = eventData.position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Mathf.Pow(eventData.position.x - PointDownPos_.x, 2) + Mathf.Pow(eventData.position.y - PointDownPos_.y, 2) < 100f)
        {
            if (!mEventUsed)
            {
                mEventUsed = true;
                OnClick.Invoke();
            }
        }
    }

    public void ReUseEvent()
    {
        mEventUsed = false;
    }
}
