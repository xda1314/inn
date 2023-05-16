#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
public class UI_Tools_RaycastChecker : MonoBehaviour
{

    static Vector3[] fourCorners = new Vector3[4];
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (MaskableGraphic g in GameObject.FindObjectsOfType<MaskableGraphic>())
        {
            if (g.raycastTarget)
            {
                RectTransform rectTransform = g.transform as RectTransform;
                rectTransform.GetWorldCorners(fourCorners);
                for (int i = 0; i < 4; i++)
                    Gizmos.DrawLine(fourCorners[i], fourCorners[(i + 1) % 4]);
            }
        }
    }
}
#endif
