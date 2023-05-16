using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;
using DG.Tweening;
using System.Text;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
using UnityEditor;
#endif
namespace IvyCore
{
    public enum AxisType
    {
        X, Y, Z
    }

    #region 序列行为
    [System.Serializable, ShowOdinSerializedPropertiesInInspector]
    public class ActionSequence : ActionBase
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            GameProtoTypeActionManager.Instance.RegistGameAction(GetActionTypeName(), Create);
        }
#endif

        #region 变量
        public static new string GetActionTypeName()
        {
            return "序列行为";
        }

        [LabelText("循环次数"), Tooltip("-1为无限循环")]
        public int loopTime_ = 1;
        [ShowIf("showLoopType"), LabelText("循环类型")]
        public LoopType loopType_ = LoopType.Restart;
        bool showLoopType()
        {
            if (loopTime_ == 1)
                return false;
            else
                return true;
        }

        [LabelText("序列列表")]
        [ListDrawerSettings(HideAddButton = true, OnTitleBarGUI = "DrawNewAddButton"), ShowInInspector, OdinSerialize, System.NonSerialized, HideReferenceObjectPicker]
        public List<ActionBase> tweenList_ = new List<ActionBase>();
        #endregion

        public new static ActionBase Create()
        {
            return new ActionSequence();
        }

        public ActionSequence() { }
        public ActionSequence(bool showPP) : base(showPP) { showDelayTime_ = false; }
        public ActionSequence(ActionSequence clone) : base(clone)
        {
            loopTime_ = clone.loopTime_;
            loopType_ = clone.loopType_;
            var itr = clone.tweenList_.GetEnumerator();
            while (itr.MoveNext())
            {
                tweenList_.Add(itr.Current.Clone());
            }
        }

        public override Tween Run(GameObject obj, Tween LastAppendTween = null)
        {
            //base.Run(obj,LastAppendTween);
            var tweenBase = this.GetAndLinkTween(obj);
            var tweener = tweenBase as Tween;
            if (tweener != null)
            {
                if (LastAppendTween != null)
                    (tweener as Sequence).Append(LastAppendTween);
                tweener.SetAutoKill(true);
                tweener.Play();
                return tweener;
            }
            return null;
        }

        public override bool IsApplyToCreate(GameObject obj)
        {
            var itr = tweenList_.GetEnumerator();
            while (itr.MoveNext())
            {
                if (!itr.Current.IsApplyToCreate(obj))
                {
                    return false;
                }
            }
            return true;
        }

        public override Tween GetAndLinkTween(GameObject obj, Sequence parentSequence = null)
        {
            Tween returnTween = null;
            if (obj != null)
            {
                if (tweenList_.Count > 0)
                {
                    var seq = DOTween.Sequence();
                    if (loopTime_ != 1)
                    {
                        seq.SetLoops(loopTime_, loopType_);
                    }
                    var itr = tweenList_.GetEnumerator();
                    while (itr.MoveNext())
                    {
                        itr.Current.GetAndLinkTween(obj, seq);
                    }

                    if (parentSequence != null)
                    {
                        seq.SetEase(easeType_);
                        seq.SetDelay(delayTime_);
                        if (isParallel_)
                        {
                            parentSequence.Insert(parallelTime_, seq);
                        }
                        else
                        {
                            parentSequence.Append(seq);
                        }
                    }
                    returnTween = seq;
                }
                else
                {
                    returnTween = DOTween.Sequence().AppendInterval(0.1f);
                }
            }
            return returnTween;
        }
        public int getActionCount()
        {
            return tweenList_.Count;
        }
#if UNITY_EDITOR
        void DrawNewAddButton()
        {
            if (SirenixEditorGUI.ToolbarButton(EditorIcons.Plus))
            {
                GenericMenu menu = new GenericMenu();
                var gam = GameProtoTypeActionManager.Instance;
                var dic = gam.GetRegistGameActionCreateDic();
                menu.AddDisabledItem(new GUIContent("请选择Action:"));
                menu.AddSeparator("");
                foreach (var kv in dic)
                {
                    bool createItem = true;
                    if (createItem)
                    {
                        menu.AddItem(new GUIContent(kv.Key), false, () =>
                        {
                            tweenList_.Add(kv.Value());
                        });
                    }
                    else
                    {
                        menu.AddDisabledItem(new GUIContent(kv.Key));
                    }
                }
                menu.ShowAsContext();
            }
        }

        public override void GetDebugInfo(StringBuilder sb, GameObject go)
        {
            base.GetDebugInfo(sb, go);
            var itr = tweenList_.GetEnumerator();
            while (itr.MoveNext())
            {
                itr.Current.GetDebugInfo(sb, go);
            }
        }
#endif
        public override ActionBase Clone()
        {
            ActionSequence newA = new ActionSequence(this);
            return newA;
        }
    }
    #endregion
    #region 延时
    [System.Serializable, ShowOdinSerializedPropertiesInInspector]
    public class Action_Common_DelayTime : ActionBase
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            GameProtoTypeActionManager.Instance.RegistGameAction(GetActionTypeName(), Create);
        }
#endif

        #region 变量
        public static new string GetActionTypeName()
        {
            return "延时行为";
        }
        #endregion

        public Action_Common_DelayTime(bool showPP)
        {
            showParallelProperty_ = false;
            showEaseType_ = false;
        }

        public Action_Common_DelayTime()
        {
            showParallelProperty_ = false;
            showEaseType_ = false;
        }

        public Action_Common_DelayTime(Action_Common_DelayTime clone) : base(clone)
        {
        }

        public override Tween GetAndLinkTween(GameObject obj, Sequence parentSequence)
        {
            parentSequence.AppendInterval(delayTime_);
            return null;
        }

        public new static ActionBase Create()
        {
            return new Action_Common_DelayTime();
        }

        public override ActionBase Clone()
        {
            return base.Clone();
        }
    }
    #endregion
    #region 自定义LOG
    [System.Serializable, ShowOdinSerializedPropertiesInInspector]
    public class Action_Common_CustomLog : ActionBase
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            GameProtoTypeActionManager.Instance.RegistGameAction(GetActionTypeName(), Create);
        }
#endif

        #region 变量
        [LabelText("LOG信息"), ShowInInspector, OdinSerialize, System.NonSerialized]
        public string logInfo = "";
        #endregion

        public static new string GetActionTypeName()
        {
            return "自定义LOG行为";
        }

        public Action_Common_CustomLog(bool showPP)
        {
            showParallelProperty_ = true;
            showDelayTime_ = false;
            showEaseType_ = false;
        }

        public Action_Common_CustomLog()
        {
            showParallelProperty_ = true;
            showDelayTime_ = false;
            showEaseType_ = false;
        }

        public Action_Common_CustomLog(Action_Common_CustomLog clone) : base(clone)
        {
            logInfo = clone.logInfo;
        }

        public override Tween GetAndLinkTween(GameObject obj, Sequence parentSequence)
        {

            if (isParallel_)
            {
                parentSequence.InsertCallback(parallelTime_, () =>
                {
                    Debug.Log(logInfo);
                });
            }
            else
            {
                parentSequence.AppendCallback(() =>
                {
                    Debug.Log(logInfo);
                });
            }
            return null;
        }

        public new static ActionBase Create()
        {
            return new Action_Common_CustomLog();
        }

        public override ActionBase Clone()
        {
            Action_Common_CustomLog na = new Action_Common_CustomLog(this);
            return na;
        }
    }
    #endregion
    #region 激活/解除激活(GameObject组件)
    [System.Serializable, ShowOdinSerializedPropertiesInInspector]
    public class Action_GameObject_ActivateOrDeActivate : ActionBase
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            GameProtoTypeActionManager.Instance.RegistGameAction(GetActionTypeName(), Create);
        }
#endif

        #region 变量
        [LabelText("是否激活"), ShowInInspector, OdinSerialize, System.NonSerialized, HideIf("reverseSet")]
        public bool activate = true;
        [LabelText("反向设置"), ShowInInspector, OdinSerialize, System.NonSerialized]
        public bool reverseSet = false;
        #endregion

        public static new string GetActionTypeName()
        {
            return "GameObject/激活与解除激活";
        }

        public Action_GameObject_ActivateOrDeActivate(bool showPP)
        {
            showParallelProperty_ = false;
            showDelayTime_ = false;
            showEaseType_ = false;
        }

        public Action_GameObject_ActivateOrDeActivate()
        {
            showParallelProperty_ = false;
            showDelayTime_ = false;
            showEaseType_ = false;
        }

        public Action_GameObject_ActivateOrDeActivate(Action_GameObject_ActivateOrDeActivate clone) : base(clone)
        {
            activate = clone.activate;
            reverseSet = clone.reverseSet;
        }

        public override Tween GetAndLinkTween(GameObject obj, Sequence parentSequence)
        {
            if (reverseSet)
            {
                parentSequence.AppendCallback(() =>
                {
                    obj.SetActive(!obj.activeSelf);
                });
            }
            else
            {
                parentSequence.AppendCallback(() =>
                {
                    obj.SetActive(activate);
                });
            }
            return null;
        }

        public new static ActionBase Create()
        {
            return new Action_GameObject_ActivateOrDeActivate();
        }

        public override ActionBase Clone()
        {
            Action_GameObject_ActivateOrDeActivate na = new Action_GameObject_ActivateOrDeActivate(this);
            return na;
        }
    }
    #endregion
    #region 坐标系移动(Transform组件)
    [System.Serializable, ShowOdinSerializedPropertiesInInspector]
    public class Action_Transform_Move : ActionBase
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            GameProtoTypeActionManager.Instance.RegistGameAction(GetActionTypeName(), Create);
        }
#endif

        #region 变量
        public static new string GetActionTypeName()
        {
            return "Transform/移动行为(世界或本地坐标系)";
        }
        [LabelText("使用世界坐标系"), ShowInInspector, OdinSerialize, System.NonSerialized]
        bool useWorldCoordinate = false;
        [LabelText("移动坐标"), ShowInInspector, OdinSerialize, System.NonSerialized, HideIf("separateCoordinateAxis_")]
        public Vector3 position_ = Vector3.zero;
        [LabelText("移动值"), ShowInInspector, SerializeField, ShowIf("separateCoordinateAxis_")]
        public float separateAxisValue_ = 0;
        [LabelText("移动时间"), ShowInInspector, OdinSerialize, System.NonSerialized, MinValue(0f)]
        public float duration_ = 1f;
        [LabelText("是否相对移动"), ShowInInspector, SerializeField]
        public bool relative_ = true;
        [LabelText("移动坐标设为起始坐标"), ShowInInspector, SerializeField]
        public bool from_ = false;
        [LabelText("分离坐标轴"), ShowInInspector, SerializeField]
        public bool separateCoordinateAxis_ = false;
        [LabelText("坐标轴类型"), ShowInInspector, SerializeField, ShowIf("separateCoordinateAxis_")]
        public AxisType axisType_ = AxisType.X;

        #endregion

        public override Tween GetAndLinkTween(GameObject obj, Sequence parentSequence)
        {
            Tweener tween = null;
            if (useWorldCoordinate)
            {
                if (separateCoordinateAxis_)
                {
                    switch (axisType_)
                    {
                        case AxisType.X:
                            tween = obj.transform.DOMoveX(separateAxisValue_, duration_);
                            break;
                        case AxisType.Y:
                            tween = obj.transform.DOMoveY(separateAxisValue_, duration_);
                            break;
                        case AxisType.Z:
                            tween = obj.transform.DOMoveZ(separateAxisValue_, duration_);
                            break;
                    }
                }
                else
                    tween = obj.transform.DOMove(position_, duration_);
            }
            else
            {
                if (separateCoordinateAxis_)
                {
                    switch (axisType_)
                    {
                        case AxisType.X:
                            tween = obj.transform.DOLocalMoveX(separateAxisValue_, duration_);
                            break;
                        case AxisType.Y:
                            tween = obj.transform.DOLocalMoveY(separateAxisValue_, duration_);
                            break;
                        case AxisType.Z:
                            tween = obj.transform.DOLocalMoveZ(separateAxisValue_, duration_);
                            break;
                    }
                }
                else
                    tween = obj.transform.DOLocalMove(position_, duration_);
            }

            switch (actionEaseType_)
            {
                case ActionEaseType.eDefault:
                    tween.SetEase(easeType_);
                    break;
                case ActionEaseType.eAnimationCurve:
                    if (easeAnimationCurve_ != null)
                        tween.SetEase(easeAnimationCurve_);
                    break;
            }
            tween.SetDelay(delayTime_);
            if (from_ && relative_)
                tween.From(true);
            else if (from_ && !relative_)
                tween.From();
            else
                tween.SetRelative(relative_);

            if (isParallel_)
            {
                parentSequence.Insert(parallelTime_, tween);
            }
            else
            {
                parentSequence.Append(tween);
            }
            return tween;
        }

        public Action_Transform_Move() : base() { }
        public Action_Transform_Move(Action_Transform_Move clone) : base(clone)
        {
            this.useWorldCoordinate = clone.useWorldCoordinate;
            this.separateAxisValue_ = clone.separateAxisValue_;
            this.duration_ = clone.duration_;
            this.relative_ = clone.relative_;
            this.from_ = clone.from_;
            this.separateCoordinateAxis_ = clone.separateCoordinateAxis_;
            this.axisType_ = clone.axisType_;
            position_ = new Vector3(clone.position_.x, clone.position_.y, clone.position_.z);
        }
        public new static ActionBase Create()
        {
            return new Action_Transform_Move();
        }
        public override ActionBase Clone()
        {
            Action_Transform_Move na = new Action_Transform_Move(this);
            return na;
        }
    }
    #endregion
    #region 坐标系旋转(Transform组件)
    [System.Serializable, ShowOdinSerializedPropertiesInInspector]
    public class Action_Transform_Rotate : ActionBase
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            GameProtoTypeActionManager.Instance.RegistGameAction(GetActionTypeName(), Create);
        }
#endif

        #region 变量
        public static new string GetActionTypeName()
        {
            return "Transform/旋转行为(世界或本地坐标系)";
        }
        [LabelText("使用世界坐标系"), ShowInInspector, OdinSerialize, System.NonSerialized]
        bool useWorldCoordinate = false;
        [LabelText("旋转值"), ShowInInspector, OdinSerialize, System.NonSerialized]
        public Vector3 rotation_ = Vector3.zero;
        [LabelText("旋转时间"), ShowInInspector, OdinSerialize, System.NonSerialized, MinValue(0f)]
        public float duration_ = 1f;
        [LabelText("是否相对旋转"), ShowInInspector, SerializeField]
        public bool relative_ = true;
        [LabelText("旋转值设为起始旋转"), ShowInInspector, SerializeField]
        public bool from_ = true;
        #endregion
        public Action_Transform_Rotate() : base() { }
        public Action_Transform_Rotate(Action_Transform_Rotate clone) : base(clone)
        {
            useWorldCoordinate = clone.useWorldCoordinate;
            rotation_ = clone.rotation_;
            duration_ = clone.duration_;
            relative_ = clone.relative_;
            from_ = clone.from_;
        }
        public override Tween GetAndLinkTween(GameObject obj, Sequence parentSequence)
        {
            Tweener tween = null;
            if (useWorldCoordinate)
            {
                tween = obj.transform.DORotate(rotation_, duration_);
            }
            else
            {
                tween = obj.transform.DOLocalRotate(rotation_, duration_);
            }

            switch (actionEaseType_)
            {
                case ActionEaseType.eDefault:
                    tween.SetEase(easeType_);
                    break;
                case ActionEaseType.eAnimationCurve:
                    if (easeAnimationCurve_ != null)
                        tween.SetEase(easeAnimationCurve_);
                    break;
            }
            tween.SetDelay(delayTime_);
            if (from_ && relative_)
                tween.From(true);
            else if (from_ && !relative_)
                tween.From();
            else
                tween.SetRelative(relative_);
            if (isParallel_)
            {
                parentSequence.Insert(parallelTime_, tween);
            }
            else
            {
                parentSequence.Append(tween);
            }
            return tween;
        }

        public new static ActionBase Create()
        {
            return new Action_Transform_Rotate();
        }

        public override ActionBase Clone()
        {
            Action_Transform_Rotate na = new Action_Transform_Rotate(this);
            return na;
        }
    }
    #endregion
    #region 缩放(Transform组件)
    [System.Serializable, ShowOdinSerializedPropertiesInInspector]
    public class ActionScale : ActionBase
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            GameProtoTypeActionManager.Instance.RegistGameAction(GetActionTypeName(), Create);
        }
#endif

        #region 变量
        public static new string GetActionTypeName()
        {
            return "Transform/缩放行为";
        }
        [LabelText("缩放系数"), ShowInInspector, OdinSerialize, System.NonSerialized]
        public Vector3 scale_ = Vector3.one;
        [LabelText("缩放时间"), ShowInInspector, OdinSerialize, System.NonSerialized, MinValue(0f)]
        public float duration_ = 1f;
        [LabelText("是否相对缩放"), ShowInInspector, SerializeField]
        public bool relative_ = true;
        [LabelText("缩放系数设为起始缩放"), ShowInInspector, SerializeField]
        public bool from_ = false;
        #endregion
        public ActionScale() : base() { }
        public ActionScale(ActionScale clone) : base(clone)
        {
            scale_ = new Vector3(clone.scale_.x, clone.scale_.y, clone.scale_.z);
            duration_ = clone.duration_;
            relative_ = clone.relative_;
            from_ = clone.from_;
        }
        public override Tween GetAndLinkTween(GameObject obj, Sequence parentSequence)
        {
            Tweener tween = null;
            tween = obj.transform.DOScale(scale_, duration_);

            switch (actionEaseType_)
            {
                case ActionEaseType.eDefault:
                    tween.SetEase(easeType_);
                    break;
                case ActionEaseType.eAnimationCurve:
                    if (easeAnimationCurve_ != null)
                        tween.SetEase(easeAnimationCurve_);
                    break;
            }
            tween.SetDelay(delayTime_);
            if (from_ && relative_)
                tween.From(true);
            else if (from_ && !relative_)
                tween.From();
            else
                tween.SetRelative(relative_);

            if (isParallel_)
            {
                parentSequence.Insert(parallelTime_, tween);
            }
            else
            {
                parentSequence.Append(tween);
            }
            return tween;
        }

        public new static ActionBase Create()
        {
            return new ActionScale();
        }

        public override ActionBase Clone()
        {
            ActionScale na = new ActionScale(this);
            return na;
        }
    }
    #endregion
    #region 透明度调整(CanvasGroup组件)
    [System.Serializable, ShowOdinSerializedPropertiesInInspector]
    public class Action_CanvasGroup_Fade : ActionBase
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            GameProtoTypeActionManager.Instance.RegistGameAction(GetActionTypeName(), Create);
        }
#endif

        #region 变量
        public static new string GetActionTypeName()
        {
            return "CanvasGroup/透明度";
        }
        [LabelText("透明度"), ShowInInspector, OdinSerialize, System.NonSerialized, MinValue(0f), MaxValue(1.0f)]
        public float opacity_ = 1.0f;
        [LabelText("变化时间"), ShowInInspector, OdinSerialize, System.NonSerialized, MinValue(0f)]
        public float duration_ = 1f;
        [LabelText("是否相对变化"), ShowInInspector, SerializeField]
        public bool relative_ = true;
        [LabelText("设为起始透明度"), ShowInInspector, SerializeField]
        public bool from_ = false;
        #endregion

        public Action_CanvasGroup_Fade(bool showPP)
        {
            showParallelProperty_ = true;
            showDelayTime_ = true;
            showEaseType_ = true;
        }

        public Action_CanvasGroup_Fade()
        {
            showParallelProperty_ = true;
            showDelayTime_ = true;
            showEaseType_ = true;
        }

        public Action_CanvasGroup_Fade(Action_CanvasGroup_Fade clone) : base(clone)
        {
            opacity_ = clone.opacity_;
            duration_ = clone.duration_;
            relative_ = clone.relative_;
            from_ = clone.from_;
        }

        public override Tween GetAndLinkTween(GameObject obj, Sequence parentSequence)
        {
            var canvasGroup = obj.GetComponent<CanvasGroup>();
            if (canvasGroup)
            {
                var tween = canvasGroup.DOFade(opacity_, duration_);
                tween.SetEase(easeType_).SetDelay(delayTime_);
                if (from_ && relative_)
                    tween.From(true);
                else if (from_ && !relative_)
                    tween.From();
                else
                    tween.SetRelative(relative_);
                if (isParallel_)
                {
                    parentSequence.Insert(parallelTime_, tween);
                }
                else
                {
                    parentSequence.Append(tween);
                }
            }
            return null;
        }

        public override bool IsApplyToCreate(GameObject obj)
        {
            if (obj.GetComponent<CanvasGroup>() != null)
                return true;
            return false;
        }

        public new static ActionBase Create()
        {
            return new Action_CanvasGroup_Fade();
        }

        public override ActionBase Clone()
        {
            Action_CanvasGroup_Fade na = new Action_CanvasGroup_Fade(this);
            return na;
        }
    }
    #endregion
    #region 释放指定窗体(通用)
    [System.Serializable, ShowOdinSerializedPropertiesInInspector]
    public class Action_Common_RemoveWindow : ActionBase
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            GameProtoTypeActionManager.Instance.RegistGameAction(GetActionTypeName(), Create);
        }
#endif
        [OdinSerialize, System.NonSerialized, ValueDropdown("GetRegisterUILayerName")]
        public string layerName_ = "";
        [OdinSerialize, System.NonSerialized, ValueDropdown("GetRegisterUIWindowName")]
        public string windowName_ = "";

        static List<string> UILayerNameList_;
#if UNITY_EDITOR
        public List<string> GetRegisterUILayerName()
        {
            if (UILayerNameList_ == null)
                UILayerNameList_ = new List<string>();
            UILayerNameList_.Clear();
            UILayerNameList_.Add("");
            var data = AssetDatabase.LoadAssetAtPath<UI_LayerRegisterData>("Assets/" + UI_LayerRegisterData.DataFilePath + "/" + UI_LayerRegisterData.DataFileName);
            if (data != null)
            {
                foreach (var layerData in data.dataList_)
                {
                    UILayerNameList_.Add(layerData.registName_);
                }
            }
            return UILayerNameList_;
        }
#endif
        static List<string> UIWindowNameList_;
#if UNITY_EDITOR

        public List<string> GetRegisterUIWindowName()
        {
            if (UIWindowNameList_ == null)
                UIWindowNameList_ = new List<string>();
            UIWindowNameList_.Clear();
            var data = AssetDatabase.LoadAssetAtPath<UI_WindowRegisterData>("Assets/" + UI_WindowRegisterData.DataFilePath + "/" + UI_WindowRegisterData.DataFileName);
            if (data != null)
            {
                foreach (var windowData in data.dataList_)
                {
                    UIWindowNameList_.Add(windowData.registName_);
                }
            }
            return UIWindowNameList_;
        }
#endif
        public override Tween GetAndLinkTween(GameObject obj, Sequence parentSequence)
        {
            parentSequence.AppendCallback(() =>
            {
                if (string.IsNullOrEmpty(layerName_))
                    UI_Manager.Instance.RemoveWindowByName(windowName_);
                else
                    UI_Manager.Instance.RemoveWindowByName(layerName_, windowName_);
            });
            return null;
        }

        public Action_Common_RemoveWindow(bool showPP)
        {
            showParallelProperty_ = false;
            showDelayTime_ = false;
            showEaseType_ = false;
            //Init();
        }

        public Action_Common_RemoveWindow()
        {
            showParallelProperty_ = false;
            showDelayTime_ = false;
            showEaseType_ = false;
            //Init();
        }
        public Action_Common_RemoveWindow(Action_Common_RemoveWindow clone) : base(clone)
        {
            layerName_ = clone.layerName_;
            windowName_ = clone.windowName_;
        }

        public static new string GetActionTypeName()
        {
            return "UI相关/释放窗体行为";
        }

        public new static ActionBase Create()
        {
            return new Action_Common_RemoveWindow();
        }

        public override ActionBase Clone()
        {
            Action_Common_RemoveWindow na = new Action_Common_RemoveWindow(this);
            return na;
        }
    }
    #endregion
    #region 通知指定窗体离场(通用)
    [System.Serializable, ShowOdinSerializedPropertiesInInspector]
    public class Action_Common_NotifyWindowRunOutAction : ActionBase
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            GameProtoTypeActionManager.Instance.RegistGameAction(GetActionTypeName(), Create);
        }
#endif
        [OdinSerialize, System.NonSerialized, ValueDropdown("GetRegisterUILayerName")]
        public string layerName_ = "";
        [OdinSerialize, System.NonSerialized, ValueDropdown("GetRegisterUIWindowName")]
        public string windowName_ = "";

        static List<string> UILayerNameList_;
#if UNITY_EDITOR
        public List<string> GetRegisterUILayerName()
        {
            if (UILayerNameList_ == null)
                UILayerNameList_ = new List<string>();
            UILayerNameList_.Clear();
            UILayerNameList_.Add("");
            var data = AssetDatabase.LoadAssetAtPath<UI_LayerRegisterData>("Assets/" + UI_LayerRegisterData.DataFilePath + "/" + UI_LayerRegisterData.DataFileName);
            if (data != null)
            {
                foreach (var layerData in data.dataList_)
                {
                    UILayerNameList_.Add(layerData.registName_);
                }
            }
            return UILayerNameList_;
        }
#endif
        static List<string> UIWindowNameList_;
#if UNITY_EDITOR

        public List<string> GetRegisterUIWindowName()
        {
            if (UIWindowNameList_ == null)
                UIWindowNameList_ = new List<string>();
            UIWindowNameList_.Clear();
            var data = AssetDatabase.LoadAssetAtPath<UI_WindowRegisterData>("Assets/" + UI_WindowRegisterData.DataFilePath + "/" + UI_WindowRegisterData.DataFileName);
            if (data != null)
            {
                foreach (var windowData in data.dataList_)
                {
                    UIWindowNameList_.Add(windowData.registName_);
                }
            }
            return UIWindowNameList_;
        }
#endif
        public override Tween GetAndLinkTween(GameObject obj, Sequence parentSequence)
        {
            parentSequence.AppendCallback(() =>
            {
                if (string.IsNullOrEmpty(layerName_))
                    UI_Manager.Instance.NotifyWindowRunEnterOrOutAction(windowName_, false);
                else
                    UI_Manager.Instance.NotifyWindowRunEnterOrOutAction(layerName_, windowName_, false);
            });
            return null;
        }

        public Action_Common_NotifyWindowRunOutAction(bool showPP)
        {
            showParallelProperty_ = false;
            showDelayTime_ = false;
            showEaseType_ = false;
            //Init();
        }

        public Action_Common_NotifyWindowRunOutAction()
        {
            showParallelProperty_ = false;
            showDelayTime_ = false;
            showEaseType_ = false;
            //Init();
        }

        public Action_Common_NotifyWindowRunOutAction(Action_Common_NotifyWindowRunOutAction clone)
        {
            layerName_ = clone.layerName_;
            windowName_ = clone.windowName_;
        }

        public static new string GetActionTypeName()
        {
            return "UI相关/通知窗体离场行为";
        }

        public new static ActionBase Create()
        {
            return new Action_Common_NotifyWindowRunOutAction();
        }

        public override ActionBase Clone()
        {
            Action_Common_NotifyWindowRunOutAction na = new Action_Common_NotifyWindowRunOutAction(this);
            return na;
        }
    }
    #endregion
    #region 场景跳转(通用)
    [System.Serializable, ShowOdinSerializedPropertiesInInspector]
    public class Action_Common_ChangeScene : ActionBase
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            GameProtoTypeActionManager.Instance.RegistGameAction(GetActionTypeName(), Create);
        }
#endif
        #region 变量
        [OdinSerialize, System.NonSerialized, CustomValueDrawer("ScenePathCustomDrawer")]
        public string scenePath_;
        //#if UNITY_EDITOR
        //[LabelText("跳转场景"), ShowInInspector,System.NonSerialized,OnValueChanged("SceneAssetValueChanged"),AssetsOnly]
        //public SceneAsset sceneAsset;
        //#endif
        #endregion
#if UNITY_EDITOR
        string ScenePathCustomDrawer(string path, GUIContent label)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("跳转场景:");
            var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
            var newScene = SirenixEditorFields.UnityObjectField(oldScene, typeof(SceneAsset), false);
            GUILayout.EndHorizontal();
            if (newScene != oldScene)
            {
                if (newScene == null)
                    return "";
                else
                    return (AssetDatabase.GetAssetPath(newScene));
            }
            return path;
        }
#endif
        public override Tween GetAndLinkTween(GameObject obj, Sequence parentSequence)
        {
            parentSequence.AppendCallback(() =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(scenePath_);
            });
            return null;
        }

        public Action_Common_ChangeScene(bool showPP)
        {
            showParallelProperty_ = false;
            showDelayTime_ = false;
            showEaseType_ = false;
            //Init();
        }

        public Action_Common_ChangeScene()
        {
            showParallelProperty_ = false;
            showDelayTime_ = false;
            showEaseType_ = false;
            //Init();
        }

        public Action_Common_ChangeScene(Action_Common_ChangeScene clone) : base(clone)
        {
            scenePath_ = clone.scenePath_;
        }

        public static new string GetActionTypeName()
        {
            return "场景跳转行为";
        }

        public new static ActionBase Create()
        {
            return new Action_Common_ChangeScene();
        }

        public override ActionBase Clone()
        {
            Action_Common_ChangeScene na = new Action_Common_ChangeScene(this);
            return na;
        }
    }

    #endregion
    #region 释放对象(GameObject组件)
    [System.Serializable, ShowOdinSerializedPropertiesInInspector]
    public class Action_GameObject_Destory : ActionBase
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            GameProtoTypeActionManager.Instance.RegistGameAction(GetActionTypeName(), Create);
        }
#endif

        #region 变量
        public static new string GetActionTypeName()
        {
            return "GameObject/销毁";
        }
        [LabelText("销毁对象"), ShowInInspector, OdinSerialize, System.NonSerialized]
        GameObject WillDestoryGameObject_ = null;
        #endregion

        public Action_GameObject_Destory(bool showPP)
        {
            showParallelProperty_ = false;
            showDelayTime_ = true;
            showEaseType_ = false;
        }

        public Action_GameObject_Destory()
        {
            showParallelProperty_ = false;
            showDelayTime_ = true;
            showEaseType_ = false;
        }

        public Action_GameObject_Destory(Action_GameObject_Destory clone) : base(clone)
        {
            WillDestoryGameObject_ = clone.WillDestoryGameObject_;
        }

        public override Tween GetAndLinkTween(GameObject obj, Sequence parentSequence)
        {
            if (WillDestoryGameObject_ != null)
                parentSequence.AppendCallback(() =>
                {
                    GameObject.Destroy(WillDestoryGameObject_, delayTime_);
                });
            return null;
        }

        public override bool IsApplyToCreate(GameObject obj)
        {
            return true;
        }

        public new static ActionBase Create()
        {
            return new Action_GameObject_Destory();
        }

        public override ActionBase Clone()
        {
            Action_GameObject_Destory na = new Action_GameObject_Destory(this);
            return na;
        }
    }
    #endregion

    #region ActionBase
    [System.Serializable, ShowOdinSerializedPropertiesInInspector]
    public class ActionBase
    {
        public static ActionBase Create()
        {
            return new ActionBase();
        }

        public virtual Tween Run(GameObject obj, Tween LastAppendTween = null)
        {
            var tweenBase = this.GetAndLinkTween(obj);
            var tweener = tweenBase as Tween;
            if (tweener != null)
            {
                tweener.SetAutoKill(true);
                tweener.Play();
                return tweener;
            }
            return null;
        }

        public static string GetActionTypeName()
        {
            return "GameActionBase";
        }

        public ActionBase(bool showPP)
        {
            showParallelProperty_ = showPP;
        }

        public ActionBase()
        {
            showParallelProperty_ = true;
        }

        public ActionBase(ActionBase ab)
        {
            this.actionEaseType_ = ab.actionEaseType_;
            this.delayTime_ = ab.delayTime_;
            if (ab.easeAnimationCurve_ != null)
            {
                this.easeAnimationCurve_ = new AnimationCurve(ab.easeAnimationCurve_.keys);
            }
            this.easeType_ = ab.easeType_;
            this.isParallel_ = ab.isParallel_;
            this.parallelTime_ = ab.parallelTime_;
            this.showDelayTime_ = ab.showDelayTime_;
            this.showEaseType_ = ab.showEaseType_;
            this.showParallelProperty_ = ab.showParallelProperty_;
            this.splitStr_ = ab.splitStr_;
        }

        public virtual void GetDebugInfo(StringBuilder sb, GameObject go)
        {
            var actionTypeName = this.GetType().InvokeMember("GetActionTypeName",
            System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static
            | System.Reflection.BindingFlags.Public, null, null,
            null);
            sb.Append(string.Format("<color=#{0}>{1}({2})</color>", IsApplyToCreate(go) ? "00ff00" : "ff0000", this.GetType().ToString(), actionTypeName)).AppendLine();
        }

        public virtual Tween GetAndLinkTween(GameObject obj, Sequence parentSequence = null)
        {
            return null;
        }

        public virtual bool IsApplyToCreate(GameObject obj)
        {
            return true;
        }

        [OdinSerialize, System.NonSerialized, HideInInspector]
        protected bool showParallelProperty_ = true;
        [OdinSerialize, System.NonSerialized, HideInInspector]
        protected bool showEaseType_ = true;
        [OdinSerialize, System.NonSerialized, HideInInspector]
        protected bool showDelayTime_ = true;

        [Title("$GetActionTypeName")]
        [GUIColor(0.5f, 1, 0), ReadOnly, HideLabel]
        public string splitStr_ = "";

        [HorizontalGroup("Parallel")]
        [LabelText("是否并列执行"), ShowIf("showParallelProperty_")]
        public bool isParallel_ = false;

        [HorizontalGroup("Parallel")]
        [ShowIf("isParallel_"), LabelText("并列执行起始时间"), MinValue(0f)]
        public float parallelTime_ = 0f;

        public enum ActionEaseType
        {
            eDefault,
            eAnimationCurve
        }
        [LabelText("缓冲类型"), ShowIf("showEaseType_")]
        public ActionEaseType actionEaseType_ = ActionEaseType.eDefault;

        [LabelText("缓冲预设类型"), ShowIf("ShowDefaultEaseType")]
        public Ease easeType_ = Ease.Unset;

        [LabelText("缓冲曲线"), ShowIf("ShowAnimationCurveEaseType")]
        public AnimationCurve easeAnimationCurve_ = null;

        public bool ShowDefaultEaseType()
        {
            if (showEaseType_ && actionEaseType_ == ActionEaseType.eDefault)
                return true;
            return false;
        }

        public bool ShowAnimationCurveEaseType()
        {
            if (showEaseType_ && actionEaseType_ == ActionEaseType.eAnimationCurve)
                return true;
            return false;
        }

        [ShowInInspector, SerializeField, LabelText("延时"), ShowIf("showDelayTime_")]
        public float delayTime_ = 0;

        public virtual ActionBase Clone()
        {
            ActionBase newA = new ActionBase(this);
            return newA;
        }
    }
    #endregion
}
