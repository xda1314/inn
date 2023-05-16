using System;
using System.Collections;
using System.Collections.Generic;
using Ivy;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
using UnityEditor;
#endif
using UnityEngine;

[ExecuteAlways]
public class UI_NotchAdapter : MonoBehaviour
{
    public enum AdapterType
    {
        ePosY_Minus_AdaptY,
        eOffsetMaxY_Minus_AdaptY,
        eSizeDeltaY_Set_AdaptY,
        eSizeDeltaY_Minus_AdaptY,
    }

    [System.Serializable]
    public class UI_NA_Data
    {
        [LabelText("需要适配的UI对象")]
        public RectTransform mRectTransform_;
        [LabelText("适配方式")]
        public AdapterType mAdapterType_;
    }
    [ListDrawerSettings(ShowIndexLabels = true, Expanded = true), LabelText("屏幕适配数据"), ShowInInspector]
    public List<UI_NA_Data> mAdaptDatas_ = new List<UI_NA_Data>();

    private void Awake()
    {
    }

    private void OnDestroy()
    {

    }

    public void AdaptNotch()
    {
        if (RiseSdk.Instance.HasNotch())
        {
            DoAdapterLogic();
        }
    }

    private float mPreviewNotchHeight_ = 60;

    void DoAdapterLogic()
    {
        if (mAdaptDatas_ != null)
        {
            for (var i = 0; i < mAdaptDatas_.Count; ++i)
            {
                var data = mAdaptDatas_[i];
                if (data != null)
                {
                    GameManager gm = GameManager.Instance;
                    switch (data.mAdapterType_)
                    {
                        case AdapterType.ePosY_Minus_AdaptY:
                            {
                                if (data.mRectTransform_ != null)
                                {
                                    var mPos = data.mRectTransform_.transform.localPosition;
                                    data.mRectTransform_.transform.localPosition = new Vector3(mPos.x, mPos.y - gm.MNotchAdaptY_, mPos.z);
                                }
                            }
                            break;
                        case AdapterType.eSizeDeltaY_Minus_AdaptY:
                            {
                                if (data.mRectTransform_ != null)
                                {
                                    var rSize = data.mRectTransform_.sizeDelta;
                                    data.mRectTransform_.sizeDelta = new Vector2(rSize.x, rSize.y - gm.MNotchAdaptY_);

                                }
                            }
                            break;
                        case AdapterType.eSizeDeltaY_Set_AdaptY:
                            {
                                if (data.mRectTransform_ != null)
                                {
                                    var rSize = data.mRectTransform_.sizeDelta;
                                    data.mRectTransform_.sizeDelta = new Vector2(rSize.x, gm.MNotchAdaptY_);

                                }
                            }
                            break;
                        case AdapterType.eOffsetMaxY_Minus_AdaptY:
                            {
                                if (data.mRectTransform_ != null)
                                {
                                    var rectTop = data.mRectTransform_.offsetMax.y;
                                    var rectRight = data.mRectTransform_.offsetMax.x;
                                    data.mRectTransform_.offsetMax = new Vector2(rectRight, rectTop - gm.MNotchAdaptY_);
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Application.isPlaying)
        {
#if UNITY_ANDROID
            if (Screen.height != (int)Screen.safeArea.height && GameManager.Instance != null)
            {
                GameManager.Instance.MNotchAdaptY_ = GetNotchVector().y;
                DoAdapterLogic();
            }
#else
            if (RiseSdk.Instance.HasNotch())
            {
                DoAdapterLogic();
            }
#endif
        }
    }
    private Vector2 GetNotchVector()
    {
        Vector2 offect = Vector2.zero;
#if UNITY_ANDROID
        if (Screen.height != (int)Screen.safeArea.height)
            offect = new Vector2(0, Screen.height - Screen.safeArea.yMax);
#elif UNITY_IOS
            //if (Screen.height != (int)Screen.safeArea.height)
            //    offect = new Vector2(0, 90);
#endif
        return offect;
    }
}
