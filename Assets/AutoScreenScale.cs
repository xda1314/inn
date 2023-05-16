using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// 用于无法通过修改尺寸适配背景的情况，比如spine动画
/// </summary>
public class AutoScreenScale : MonoBehaviour
{
    private const int BaseScreenWidth = 1080;
    private const int BaseScreenHeight = 2160;
    [SerializeField] private bool useSelfSize = false;
    [SerializeField, ShowIf("useSelfSize")] private int mWidth;
    [SerializeField, ShowIf("useSelfSize")] private int mHeight;

    void Start()
    {
        if (!useSelfSize)
        {
            if (Screen.height * BaseScreenWidth > (Screen.width * BaseScreenHeight))
            {
                float new_scale_y = Screen.height * 1f * BaseScreenWidth / (Screen.width * BaseScreenHeight);
                transform.localScale = new Vector3(new_scale_y, new_scale_y, 1);
            }
            else
            {
                float new_scale_x = Screen.width * 1f * BaseScreenHeight / (Screen.height * BaseScreenWidth);
                transform.localScale = new Vector3(new_scale_x, new_scale_x, 1);
            }
            Vector3 pos = transform.localPosition;
            transform.localPosition = new Vector3(pos.x, 0, pos.z);
        }
        else if (mWidth > 0 && mHeight > 0)
        {
            var screeScale = 1.0f;
            if (Screen.height * BaseScreenWidth < (Screen.width * BaseScreenHeight))
                screeScale = BaseScreenHeight * 1.0f / Screen.height;
            else
                screeScale = BaseScreenWidth * 1.0f / Screen.width;
            var scaleW = Screen.width * screeScale / mWidth;
            var scaleH = Screen.height * screeScale / mHeight;
            if (scaleW > scaleH)
                transform.localScale = Vector3.one * scaleW;
            else
                transform.localScale = Vector3.one * scaleH;
        }
    }

}
