using UnityEngine;
using UnityEngine.UI;

public class MergeGrid : MonoBehaviour
{
    [HideInInspector] public MergeGridData gridData;

    public Vector2Int gridPos => gridData != null ? gridData.GridPos : Vector2Int.zero;


    public void SetGridData(MergeGridData gridData)
    {
        this.gridData = gridData;
    }

    public void SetAlpha(float a)
    {
        if (TryGetComponent<CanvasGroup>(out var ca))
            ca.alpha = a;
    }
    public void SetSprite(Sprite sprite)
    {
        if (TryGetComponent(out Image img)) 
        {
            img.sprite = sprite;
        }
    }
}
