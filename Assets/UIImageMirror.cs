using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 对UI图形进行镜像处理
/// Image - Sample顶点顺序
/// ------
/// |1 /2|
/// |0/ 3|
/// ------
/// </summary>
[RequireComponent(typeof(Image))]
public class UIImageMirror : BaseMeshEffect
{
    public enum MirrorDir
    {
        Horizontal, // 水平镜像
        Vertical,   // 垂直镜像
        Quater,     // 四方镜像（先水平，后垂直）
    }

    protected const int AxisX = 0;
    protected const int AxisY = 1;

    [SerializeField]
    private MirrorDir _direction;

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive()) return;

        Image img = graphic as Image;
        if (null == img) return;

        if (img.type == Image.Type.Simple)
        {
            _SimpleMirror(vh);
        }
    }


    #region ========= Image.Type.Simple模式 =========

    private void _SimpleMirror(VertexHelper vh)
    {
        Rect rect = graphic.GetPixelAdjustedRect();
        ShrinkVert(vh, rect);

        Vector2 doubleCenter = rect.center * 2;
        switch (_direction)
        {
            case MirrorDir.Horizontal:
                SimpleMirrorHor(vh, doubleCenter.x);
                break;

            case MirrorDir.Vertical:
                SimpleMirrorVer(vh, doubleCenter.y);
                break;

            case MirrorDir.Quater:
                SimpleMirrorQuat(vh, doubleCenter);
                break;
        }
    }

    /// <summary>
    /// Simple模式的水平镜像
    /// 顶点布局：
    /// -----------
    /// |1 /2| \ 5|
    /// |0/ 3|  \4|
    /// -----------
    /// </summary>
    protected void SimpleMirrorHor(VertexHelper vh, float doubleX)
    {
        AddMirrorVert(vh, 0, AxisX, doubleX);  // 顶点4
        AddMirrorVert(vh, 1, AxisX, doubleX);  // 顶点5

        vh.AddTriangle(2, 4, 3);
        vh.AddTriangle(2, 5, 4);
    }

    /// <summary>
    /// Simple模式的垂直镜像
    /// 顶点布局：
    /// ------
    /// |4\ 5|
    /// |  \ |
    /// ------
    /// |1 /2|
    /// |0/ 3|
    /// ------
    /// </summary>
    protected void SimpleMirrorVer(VertexHelper vh, float doubleY)
    {
        AddMirrorVert(vh, 0, AxisY, doubleY);  // 顶点4
        AddMirrorVert(vh, 3, AxisY, doubleY);  // 顶点5

        vh.AddTriangle(2, 1, 4);
        vh.AddTriangle(2, 4, 5);
    }

    /// <summary>
    /// Simple模式的四方镜像
    /// 顶点布局：
    /// -----------
    /// |6 /7| \ 8|
    /// | /  |  \ |
    /// -----------
    /// |1 /2| \ 5|
    /// |0/ 3|  \4|
    /// -----------
    /// </summary>
    protected void SimpleMirrorQuat(VertexHelper vh, Vector2 doubleCenter)
    {
        // 水平
        AddMirrorVert(vh, 0, AxisX, doubleCenter.x);   // 顶点4
        AddMirrorVert(vh, 1, AxisX, doubleCenter.x);   // 顶点5
        vh.AddTriangle(2, 4, 3);
        vh.AddTriangle(2, 5, 4);

        // 垂直
        AddMirrorVert(vh, 0, AxisY, doubleCenter.y);   // 顶点6
        AddMirrorVert(vh, 3, AxisY, doubleCenter.y);   // 顶点7
        AddMirrorVert(vh, 4, AxisY, doubleCenter.y);   // 顶点8
        vh.AddTriangle(7, 1, 6);
        vh.AddTriangle(7, 2, 1);
        vh.AddTriangle(7, 5, 2);
        vh.AddTriangle(7, 8, 5);
    }

    #endregion


    /// <summary>
    /// 添加单个镜像顶点
    /// </summary>
    /// <param name="vh"></param>
    /// <param name="srcVertIdx">镜像源顶点的索引值</param>
    /// <param name="axis">轴向：0-X轴；1-Y轴</param>
    /// <param name="doubleCenter">Rect.center轴向分量的两倍值</param>
    protected static void AddMirrorVert(VertexHelper vh, int srcVertIdx, int axis, float doubleCenter)
    {
        UIVertex vert = UIVertex.simpleVert;
        vh.PopulateUIVertex(ref vert, srcVertIdx);
        Vector3 pos = vert.position;
        pos[axis] = doubleCenter - pos[axis];
        vert.position = pos;
        vh.AddVert(vert);
    }

    /// <summary>
    /// 收缩顶点坐标
    /// 根据镜像类型，将原始顶点坐标向“起始点(左/下)”收缩
    /// </summary>
    protected void ShrinkVert(VertexHelper vh, Rect rect)
    {
        int count = vh.currentVertCount;

        UIVertex vert = UIVertex.simpleVert;
        for (int i = 0; i < count; ++i)
        {
            vh.PopulateUIVertex(ref vert, i);
            Vector3 pos = vert.position;
            if (MirrorDir.Horizontal == _direction || MirrorDir.Quater == _direction)
            {
                pos.x = (rect.x + pos.x) * 0.5f;
            }
            if (MirrorDir.Vertical == _direction || MirrorDir.Quater == _direction)
            {
                pos.y = (rect.y + pos.y) * 0.5f;
            }
            vert.position = pos;
            vh.SetUIVertex(vert, i);
        }
    }


    #region ======设置Image的原尺寸======

    private RectTransform _rectTrans;
    public RectTransform RectTrans
    {
        get
        {
            if (null == _rectTrans)
            {
                _rectTrans = GetComponent<RectTransform>();
            }
            return _rectTrans;
        }
    }

    public void SetNativeSize()
    {
        Image img = graphic as Image;
        if (null == img) return;

        Sprite sprite = img.overrideSprite;
        if (null == sprite) return;

        float w = sprite.rect.width / img.pixelsPerUnit;
        float h = sprite.rect.height / img.pixelsPerUnit;
        RectTrans.anchorMax = RectTrans.anchorMin;
        switch (_direction)
        {
            case MirrorDir.Horizontal:
                RectTrans.sizeDelta = new Vector2(w * 2, h);
                break;
            case MirrorDir.Vertical:
                RectTrans.sizeDelta = new Vector2(w, h * 2);
                break;
            case MirrorDir.Quater:
                RectTrans.sizeDelta = new Vector2(w * 2, h * 2);
                break;
        }

        img.SetVerticesDirty();
    }

    #endregion
}