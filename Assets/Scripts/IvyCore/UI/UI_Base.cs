using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using DG.Tweening;

namespace IvyCore
{
    [DisallowMultipleComponent]
    public class UI_Base : SerializedMonoBehaviour
    {
        public enum UIActionType
        {
            eEnter,
            eOut,
            eEnumCount
        }

        [ToggleLeft, LabelText("生成成员变量")]
        public bool generateMember_;
        [ValueDropdown("GetAllComponentsName"), ShowIf("IsShowSelectComponent"), LabelText("变量类型名称")]
        public string componentName_;
        [LabelText("生成独立组件"), ShowIf("IsShowGenerateStandaloneComponent")]
        public bool generateStandaloneComponent_;
        [ShowIf("generateMember_"), LabelText("变量说明"), MultiLineProperty(2)]
        public string componentDescription_;
        [LabelText("初始未激活")]
        public bool notActive_ = false;
        [NonSerialized]
        private Tween enterAction_ = null;
        [NonSerialized]
        private Tween outAction_ = null;

        bool IsShowSelectComponent()
        {
            return generateMember_;
        }

        bool IsShowGenerateStandaloneComponent()
        {
            return GetComponent<UI_Window>() == null && generateMember_;
        }


        public List<string> GetAllComponentsName()
        {
            List<string> names = new List<string>();
            var list = this.GetComponents<Component>();
            names.Add("UnityEngine.GameObject");
            foreach (var t in list)
            {
                names.Add(t.GetType().ToString());
            }
            return names;
        }

        [InfoBox("无效GameAction", InfoMessageType.Error, "isEnterActionInvaild")]
        [LabelText("进场行为"), ValueDropdown("GetActionsDescription")]
        [EditAndClearActionButton]
        public string enterActionDescription_ = string.Empty;

        bool isEnterActionInvaild()
        {
            if (string.IsNullOrEmpty(enterActionDescription_))
                return false;
            return GameActionManager.Instance.IsActionDescriptionNotRegisted(enterActionDescription_);
        }

        static List<string> GetActionsDescription()
        {
            return GameActionManager.Instance.GetAllActionsDescription();
        }
        [InfoBox("无效GameAction", InfoMessageType.Error, "isOutActionVaild")]
        [LabelText("出场行为"), ValueDropdown("GetActionsDescription")]
        [EditAndClearActionButton]
        public string outActionDescription_ = string.Empty;

        bool isOutActionVaild()
        {
            if (string.IsNullOrEmpty(outActionDescription_))
                return false;
            return GameActionManager.Instance.IsActionDescriptionNotRegisted(outActionDescription_);
        }

        public float RunEnterAction(bool recursionChildren = false)
        {
            return RunAction(UIActionType.eEnter, recursionChildren);
        }

        public float RunOutAction(bool recursionChildren = false)
        {
            return RunAction(UIActionType.eOut, recursionChildren);
        }

        protected float RunAction(UIActionType type, bool recursionChildren = false)
        {
            var gam = GameActionManager.Instance;
            if (recursionChildren)
            {
                var baseTrans = transform;
                if (baseTrans != null)
                {
                    //不处理未激活对象
                    var childList = baseTrans.GetComponentsInChildren<UI_Base>();
                    for (var i = 0; i < childList.Length; ++i)
                    {
                        ActionSequence seq = null;
                        switch (type)
                        {
                            case UIActionType.eEnter:
                                {
                                    var enterAction = gam.GetActionByDescription(childList[i].enterActionDescription_);
                                    if (enterAction != null)
                                    {
                                        seq = enterAction.m_Action;
                                    }
                                }
                                break;
                            case UIActionType.eOut:
                                {
                                    var outAction = gam.GetActionByDescription(childList[i].outActionDescription_);
                                    if (outAction != null)
                                    {
                                        seq = outAction.m_Action;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        if (seq != null && seq.getActionCount() > 0)
                        {
                            var gameObj = childList[i].gameObject;
                            childList[i].ResetToInitPosition();
                            seq.Run(gameObj);
                        }
                    }
                }
            }
            else
            {
                ActionSequence seq = null;
                switch (type)
                {
                    case UIActionType.eEnter:
                        {
                            var enterAction = gam.GetActionByDescription(enterActionDescription_);
                            if (enterAction != null)
                            {
                                seq = enterAction.m_Action;
                                if (seq != null && seq.getActionCount() > 0)
                                {
                                    if (enterAction_ != null && enterAction_.IsPlaying() && !enterAction_.IsComplete())
                                    {
                                        enterAction_.Kill(true);
                                    }
                                    if (outAction_ != null && outAction_.IsPlaying() && !outAction_.IsComplete())
                                    {
                                        outAction_.Kill(true);
                                    }
                                }
                            }
                        }
                        break;
                    case UIActionType.eOut:
                        {
                            var outAction = gam.GetActionByDescription(outActionDescription_);
                            if (outAction != null)
                            {
                                seq = outAction.m_Action;
                                if (seq != null && seq.getActionCount() > 0)
                                {
                                    if (outAction_ != null && outAction_.IsPlaying() && !outAction_.IsComplete())
                                    {
                                        outAction_.Kill(true);
                                    }
                                    if (enterAction_ != null && enterAction_.IsPlaying() && !enterAction_.IsComplete())
                                    {
                                        enterAction_.Kill(true);
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
                if (seq != null && seq.getActionCount() > 0)
                {

                    this.ResetToInitPosition();
                    var tween = seq.Run(gameObject);
                    switch (type)
                    {
                        case UIActionType.eEnter:
                            enterAction_ = tween;
                            enterAction_.OnComplete(() =>
                            {
                                enterAction_ = null;
                            });
                            break;
                        case UIActionType.eOut:
                            outAction_ = tween;
                            outAction_.OnComplete(() =>
                            {
                                outAction_ = null;
                            });
                            break;
                    }
                    return tween.Duration(false);
                }
            }
            return 0;
        }

        protected void ResetToInitPosition()
        {
            this.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(m_initAnchoredPosition3D.x, m_initAnchoredPosition3D.y, m_initAnchoredPosition3D.z);
            //this.transform.position = new Vector3(m_initPosition.x, m_initPosition.y, m_initPosition.z);
            //this.transform.localPosition = new Vector3(m_initLocalPosition.x, m_initLocalPosition.y, m_initLocalPosition.z);
        }

        [HideInInspector]
        public Vector3 m_initPosition;
        [HideInInspector]
        public Vector3 m_initLocalPosition;
        [HideInInspector]
        public Vector3 m_initAnchoredPosition3D;

        public void Awake()
        {
            m_initPosition = this.transform.position;
            m_initLocalPosition = this.transform.localPosition;
            m_initAnchoredPosition3D = this.GetComponent<RectTransform>().anchoredPosition3D;
            //if (notActive_)
            //{
            //    this.gameObject.SetActive(false);
            //}
        }

        public void OnDestroy()
        {
            //var uim = UI_Manager.Instance;
            //if (uim != null)
            //    uim.UnRegisterRefreshEvent(this);
        }

        //#if UNITY_EDITOR
        //        static Vector3[] fourCorners = new Vector3[4];
        //        static Rect tempUseRect = new Rect();

        //        private void OnDrawGizmos()
        //        {
        //            var gam = GameActionManager.Instance;
        //            bool showError = false;
        //            bool showWarn = false;
        //            if (!string.IsNullOrEmpty(enterActionDescription_))
        //            {
        //                if (gam.IsActionDescriptionNotRegisted(enterActionDescription_))
        //                {
        //                    showError = true;
        //                }
        //                if (!showError)
        //                {
        //                    var action = gam.GetActionByDescription(enterActionDescription_);
        //                    showWarn = !action.m_Action.IsApplyToCreate(this.gameObject);
        //                }
        //            }
        //            if (!showError && !string.IsNullOrEmpty(outActionDescription_))
        //            {
        //                if (gam.IsActionDescriptionNotRegisted(outActionDescription_))
        //                {
        //                    showError = true;
        //                }
        //                if (!showError && !showWarn)
        //                {
        //                    var action = gam.GetActionByDescription(outActionDescription_);
        //                    showWarn = !action.m_Action.IsApplyToCreate(this.gameObject);
        //                }
        //            }
        //            if (showError || showWarn)
        //            {
        //                var rectCom = this.GetComponent<RectTransform>();
        //                rectCom.GetWorldCorners(fourCorners);
        //                var midX = fourCorners[0].x + (fourCorners[2].x - fourCorners[0].x) * 0.5f;
        //                var midY = fourCorners[0].y + (fourCorners[2].y - fourCorners[0].y) * 0.5f;
        //                tempUseRect.Set(midX - 100, midY - 100, 200, 200);
        //                if (showError)
        //                {
        //                    Gizmos.DrawGUITexture(tempUseRect, GlobleConfig.GetUnityBuildinIcon("console.erroricon") as Texture);
        //                }
        //                else if (showWarn)
        //                {
        //                    Gizmos.DrawGUITexture(tempUseRect, GlobleConfig.GetUnityBuildinIcon("console.warnicon") as Texture);
        //                }
        //            }
        //        }
        //#endif
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class EditAndClearActionButtonAttribute : Attribute
    {
        public EditAndClearActionButtonAttribute()
        {
        }
    }
}