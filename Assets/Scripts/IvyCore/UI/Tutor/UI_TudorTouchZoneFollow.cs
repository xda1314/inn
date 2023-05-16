using UnityEngine;

namespace IvyCore
{
    public class UI_TudorTouchZoneFollow : MonoBehaviour
    {
        UI_TutorLayer mUI_TutorLayer_;
        RectTransform mDestControlRT_;
        Canvas mWorldCanvas_;
        Vector3[] fourCorners = new Vector3[4];
        RectTransform mUI_TutorLayerRT_;
        GameObject mShowObject_ = null;
        float mSetIgnoreZoneDelayTime_;
        System.Action mAfterIgnoreZoneTimeAction_;
        float mTimer_ = 0;
        bool mWillDelayCallAction_ = false;
        public void Init(UI_TutorLayer l, RectTransform rt, Canvas wc, GameObject so, float dt, System.Action ac)
        {
            mUI_TutorLayer_ = l;
            mDestControlRT_ = rt;
            mWorldCanvas_ = wc;
            mUI_TutorLayerRT_ = mUI_TutorLayer_.GetComponent<RectTransform>();
            mShowObject_ = so;
            mSetIgnoreZoneDelayTime_ = dt;
            mAfterIgnoreZoneTimeAction_ = ac;
            mWillDelayCallAction_ = dt > 0;
        }

        public void Update()
        {
            if (mUI_TutorLayer_ != null && mDestControlRT_ != null && mWorldCanvas_ != null)
            {
                var dt = Time.unscaledDeltaTime;

                mDestControlRT_.GetWorldCorners(fourCorners);
                Vector3 screenPos;
                if (mWorldCanvas_.worldCamera != null)
                {
                    screenPos = mWorldCanvas_.worldCamera.WorldToScreenPoint(mDestControlRT_.transform.position);
                    var screenPos0 = mWorldCanvas_.worldCamera.WorldToScreenPoint(fourCorners[0]);
                    var screenPos1 = mWorldCanvas_.worldCamera.WorldToScreenPoint(fourCorners[1]);
                    var screenPos2 = mWorldCanvas_.worldCamera.WorldToScreenPoint(fourCorners[2]);
                    var screenPos3 = mWorldCanvas_.worldCamera.WorldToScreenPoint(fourCorners[3]);
                    screenPos = new Vector3(screenPos0.x + (screenPos3.x - screenPos0.x) * 0.5f,
                        screenPos0.y + (screenPos2.y - screenPos0.y) * 0.5f, screenPos0.z);
                    var sw = Screen.width;
                    var sh = Screen.height;
                    var rt = mUI_TutorLayerRT_;
                    if (rt != null)
                    {
                        this.transform.localPosition = new Vector3(rt.rect.size.x * screenPos.x / sw - rt.rect.size.x * 0.5f, rt.rect.size.y * (screenPos.y) / sh - rt.rect.size.y * 0.5f, screenPos.z);
                    }
                    else
                        this.transform.position = screenPos;
                    if (mShowObject_ != null)
                    {
                        mShowObject_.transform.position = this.transform.position;
                    }


                    if (mWillDelayCallAction_)
                    {
                        mSetIgnoreZoneDelayTime_ -= dt;
                        if (mSetIgnoreZoneDelayTime_ <= 0)
                        {
                            mWillDelayCallAction_ = false;
                            mUI_TutorLayer_.UI_RayIgnore_.SetNotIgnoreScreenZone(screenPos0.x,
                           screenPos3.x,
                           screenPos1.y,
                           screenPos0.y,
                           0, mAfterIgnoreZoneTimeAction_);
                        }
                        else
                        {
                            //mUI_TutorLayer_.UI_RayIgnore_.SetNotIgnoreScreenZone(screenPos0.x,
                            //screenPos3.x,
                            //screenPos1.y,
                            //screenPos0.y,
                            //0, null);
                        }
                    }
                    else
                    {
                        mUI_TutorLayer_.UI_RayIgnore_.SetNotIgnoreScreenZone(screenPos0.x,
                           screenPos3.x,
                           screenPos1.y,
                           screenPos0.y,
                           0, null);
                    }
                }
            }

        }
    }
}
