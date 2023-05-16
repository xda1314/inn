using UnityEngine;

public class AutoRectScale : MonoBehaviour
{
    [SerializeField] private RectTransform adaptRectTrans;
    void Start()
    {
        if (TryGetComponent<RectTransform>(out var selfRectTrans))
        {
            var size1 = adaptRectTrans.rect.size;
            var size2 = selfRectTrans.rect.size;
            var scaleX = size1.x / size2.x;
            var scaleY = size1.y / size2.y;
            this.transform.localScale = Vector3.one * (scaleX > scaleY ? scaleX : scaleY);
        }
    }
}
