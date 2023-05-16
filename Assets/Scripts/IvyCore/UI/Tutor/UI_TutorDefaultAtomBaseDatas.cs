using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using ivy.game;

namespace IvyCore
{
    [System.Serializable, ShowOdinSerializedPropertiesInInspector, HideReferenceObjectPicker]
    public class UI_TutorStart : UI_TutorAtomBaseData
    {
        #region 注册创建回调
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            UI_TutorAtomBaseDataManager.Instance.RegistTutorAtomBaseData(GetShowInMenuName(), Create);
        }
#endif
        #endregion
        public static new string GetShowInMenuName()
        {
            return "遮挡层控制";
        }

        public static UI_TutorStart Create()
        {
            return new UI_TutorStart();
        }

        bool TimeCheck(float v)
        {
            if (v >= 0.0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void OnBeginTutor(UI_TutorLayer l)
        {
            l.gameObject.SetActive(true);
            base.OnBeginTutor(l);
            var layerColor = l.MaskLayer_.color;
            layerColor[3] = StartAlpha_;
            if (!KeepStartAlpha_)
                l.MaskLayer_.color = layerColor;
            l.MaskLayer_.DOFade(EndAlpha_, FadeInTime_).OnComplete(() =>
            {
                DOTween.Sequence().AppendInterval(GoNextTutorTime_).AppendCallback(() =>
                {
                    UI_TutorManager.Instance.GoNextTutor();
                });
            });
        }

        bool ShowStartAlpha()
        {
            return !KeepStartAlpha_;
        }

        #region 变量
        [LabelText("遮挡层渐现时间"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("遮挡层控制"), ValidateInput("TimeCheck", DefaultMessage = "遮挡层渐现时间不能为负数!")]
        public float FadeInTime_ = 0.5f;
        [LabelText("起始透明度不变"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("遮挡层控制")]
        bool KeepStartAlpha_ = false;
        [LabelText("遮挡层起始透明度"), ShowIf("ShowStartAlpha"), PropertyRange(0, 1.0f), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("遮挡层控制")]
        public float StartAlpha_ = 0.0f;
        [LabelText("遮挡层终止透明度"), PropertyRange(0, 1.0f), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("遮挡层控制")]
        public float EndAlpha_ = 0.8f;
        [LabelText("执行下一教学时间(遮挡层渐现时间后)"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("遮挡层控制"), ValidateInput("TimeCheck", DefaultMessage = "执行下一教学时间不能为负数!")]
        public float GoNextTutorTime_ = 0;
        #endregion

        public UI_TutorStart() { }
        public UI_TutorStart(UI_TutorStart src)
        {
            FadeInTime_ = src.FadeInTime_;
            KeepStartAlpha_ = src.KeepStartAlpha_;
            StartAlpha_ = src.StartAlpha_;
            EndAlpha_ = src.EndAlpha_;
            GoNextTutorTime_ = src.GoNextTutorTime_;
        }
        public override UI_TutorAtomBaseData Clone()
        {
            return new UI_TutorStart(this);
        }
    }

    [System.Serializable, ShowOdinSerializedPropertiesInInspector, HideReferenceObjectPicker]
    public class UI_TutorEnd : UI_TutorAtomBaseData
    {
        #region 注册创建回调
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            UI_TutorAtomBaseDataManager.Instance.RegistTutorAtomBaseData(GetShowInMenuName(), Create);
        }
#endif
        #endregion
        public static new string GetShowInMenuName()
        {
            return "结束教学";
        }

        public static UI_TutorEnd Create()
        {
            return new UI_TutorEnd();
        }

        bool FadeOutTimeCheck(float v, ref string message)
        {
            if (v >= 0.0f)
            {
                return true;
            }
            else
            {
                message = "遮挡层渐隐时间不能为负数!";
                return false;
            }
        }

        public override void OnBeginTutor(UI_TutorLayer l)
        {
            base.OnBeginTutor(l);

            l.MaskLayer_.DOFade(EndAlpha_, FadeInTime_).OnComplete(() =>
            {
                l.gameObject.SetActive(false);
                UI_TutorManager.Instance.EndCurrentTutor();
            });
        }
        #region 变量
        [LabelText("遮挡层渐隐时间"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("结束教学"), ValidateInput("FadeOutTimeCheck")]
        public float FadeInTime_ = 0.3f;
        [LabelText("遮挡层终止透明度"), PropertyRange(0, 1.0f), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("结束教学")]
        public float EndAlpha_ = 0.1f;
        #endregion

        public UI_TutorEnd() { }
        public UI_TutorEnd(UI_TutorEnd src)
        {
            FadeInTime_ = src.FadeInTime_;
            EndAlpha_ = src.EndAlpha_;
        }
        public override UI_TutorAtomBaseData Clone()
        {
            return new UI_TutorEnd(this);
        }
    }

    [System.Serializable, ShowOdinSerializedPropertiesInInspector, HideReferenceObjectPicker]
    public class UI_TutorTouchUIObjet : UI_TutorAtomBaseData
    {
        #region 注册创建回调
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            UI_TutorAtomBaseDataManager.Instance.RegistTutorAtomBaseData(GetShowInMenuName(), Create);
        }
#endif
        #endregion

        public static new string GetShowInMenuName()
        {
            return "点击UI控件";
        }

        public static UI_TutorTouchUIObjet Create()
        {
            return new UI_TutorTouchUIObjet();
        }
        Button mBtnCom_ = null;
        public override void OnBeginTutor(UI_TutorLayer l)
        {
            //var go = UI_Manager.Instance.GetWindow<GameObject>(WindowName_, LayerName_);
            UISystem.Instance.TryGetUI<UIPanelBase>(WindowName_, out var go);
            if (go != null)
            {
                Transform control = null;
                switch (mControlFindType_)
                {
                    case ControlFindType.eName:
                        control = go.transform.GetChildByName(ControlName_, true);
                        break;
                    case ControlFindType.ePath:
                        {
                            var names = ControlPath_.Split('/');
                            if (names.Length == 0)
                            {
                                control = go.transform;
                            }
                            else
                            {
                                var tempTrans = go.transform;
                                var tempIndex = 0;
                                for (var i = 0; i < names.Length; ++i)
                                {
                                    tempTrans = tempTrans.GetChildByName(names[tempIndex], false);
                                    ++tempIndex;
                                    if (tempTrans == null)
                                    {
                                        break;
                                    }
                                }
                                control = tempTrans;
                            }
                        }
                        break;

                }

                if (control != null)
                {
                    var maskableGraphic = control.GetComponent<MaskableGraphic>();
                    if (maskableGraphic == null)
                    {
                        mPreAddMaskableGraphics_ = control.gameObject.AddComponent<Image>();
                        mPreAddMaskableGraphics_.sprite = null;
                        mPreAddMaskableGraphics_.color = new Color(0, 0, 0, 0);
                    }
                }
                else
                {
                    UI_TutorManager.Instance.ForceEndTutor();
                    return;
                }

                Transform focusControl = null;
                Vector3 posFocus = Vector3.zero;
                //聚焦merge页当前选中格子
                if (FocusMergeSelectedItem)
                {
                    //判空
                    if (MergeController.CurrentController != null && MergeController.CurrentController.currentSelectItemData != null)
                    {
                        //当前选中的合成物体
                        focusControl = MergeController.CurrentController.currentSelectItemData.ItemGO.transform;
                        //获取位置
                        posFocus = focusControl.position;
                        //无意义的判空
                        if (focusControl != null)
                        {
                            //获取MaskableGraphic 组件
                            var maskableGraphic = focusControl.GetComponent<MaskableGraphic>();
                            if (maskableGraphic == null)
                            {
                                mPreAddMaskableGraphicsFocus_ = focusControl.gameObject.AddComponent<Image>();
                                mPreAddMaskableGraphicsFocus_.sprite = null;
                                mPreAddMaskableGraphicsFocus_.color = new Color(0, 0, 0, 0);
                            }
                        }
                        else
                        {
                            UI_TutorManager.Instance.ForceEndTutor();
                            return;
                        }
                    }
                }

                //遮罩处理
                MaskObject_ = new GameObject();
                var img = MaskObject_.AddComponent<Image>();
                img.sprite = MaskImage_;
                img.type = MaskImageType_;
                img.raycastTarget = false;
                MaskObject_.transform.SetParent(l.transform, false);
                MaskObject_.transform.SetAsFirstSibling();
                MaskObject_.transform.position = control.transform.position;
                var controlRT = control.GetComponent<RectTransform>();
                var size = controlRT.rect.size;
                if (CustomizeSize)
                    MaskObject_.GetComponent<RectTransform>().sizeDelta = MaskImageSize_;
                else
                    MaskObject_.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x, size.y);
                img.material = AssetSystem.Instance.LoadAsset<Material>("__UI__/__StencilWrite__", null).Result;

                go.TryGetComponent<Canvas>(out var windowCanvas);
                if (windowCanvas == null || windowCanvas.worldCamera == null)
                    windowCanvas = UISystem.Instance.PopupCanvas;
                if (windowCanvas.worldCamera != null)
                {
                    //screenPos = windowCanvas.worldCamera.WorldToScreenPoint(control.transform.position);
                    Vector3[] fourCorners = new Vector3[4];
                    controlRT.GetWorldCorners(fourCorners);
                    var screenPos0 = windowCanvas.worldCamera.WorldToScreenPoint(fourCorners[0]);
                    var screenPos1 = windowCanvas.worldCamera.WorldToScreenPoint(fourCorners[1]);
                    var screenPos2 = windowCanvas.worldCamera.WorldToScreenPoint(fourCorners[2]);
                    var screenPos3 = windowCanvas.worldCamera.WorldToScreenPoint(fourCorners[3]);
                    var screenPos = new Vector3(screenPos0.x + (screenPos3.x - screenPos0.x) * 0.5f,
                        screenPos0.y + (screenPos2.y - screenPos0.y) * 0.5f, screenPos0.z);
                    var sw = Screen.width;
                    var sh = Screen.height;
                    var rt = l.GetComponent<RectTransform>();
                    if (rt != null)
                    {
                        MaskObject_.transform.localPosition = new Vector3(rt.rect.size.x * screenPos.x / sw - rt.rect.size.x * 0.5f, rt.rect.size.y * (screenPos.y) / sh - rt.rect.size.y * 0.5f, screenPos.z);
                    }
                    else
                        MaskObject_.transform.position = screenPos;
                    if (!mEnableMaskObjectAlwaysFollowControl_)
                    {
                        l.UI_RayIgnore_.SetNotIgnoreScreenZone(screenPos0.x,
                        screenPos3.x,
                        screenPos1.y,
                        screenPos0.y,
                        mDelayTouchVaildTime_);
                    }

                }
                else
                {
                    var screenPos = control.transform.position;
                    l.UI_RayIgnore_.SetNotIgnoreScreenZone(screenPos.x - size.x * 0.5f, screenPos.x + size.x * 0.5f, screenPos.y + size.y * 0.5f, screenPos.y - size.y * 0.5f, mDelayTouchVaildTime_);
                }

                //聚焦merge页当前选中格子
                if (FocusMergeSelectedItem)
                {
                    //创建新物品 属性处理
                    FocusMaskObject_ = new GameObject();
                    var imgFocus = FocusMaskObject_.AddComponent<Image>();
                    imgFocus.sprite = FocusMaskImage_;
                    imgFocus.type = FocusMaskImageType_;
                    imgFocus.raycastTarget = false;
                    FocusMaskObject_.transform.SetParent(l.transform, false);
                    FocusMaskObject_.transform.SetAsFirstSibling();

                    //转换坐标
                    if (windowCanvas.worldCamera != null)
                        FocusMaskObject_.transform.position = windowCanvas.worldCamera.WorldToScreenPoint(posFocus);
                    else
                        FocusMaskObject_.transform.position = posFocus;

                    FocusMaskObject_.GetComponent<RectTransform>().sizeDelta = FocusMaskImageSize_;
                    imgFocus.material = AssetSystem.Instance.LoadAsset<Material>("__UI__/__StencilWrite__", null).Result;
                }

                if (prefabShowObject_ != null)
                {
#if UNITY_EDITOR
                    ShowObject_ = PrefabUtility.InstantiatePrefab(prefabShowObject_) as GameObject;
#else
                    ShowObject_ = GameObject.Instantiate(prefabShowObject_);
#endif
                    ShowObject_.transform.SetParent(l.transform, false);
                    ShowObject_.transform.position = MaskObject_.transform.position;
                    var scl = ShowObject_.transform.localScale;
                    ShowObject_.transform.localScale = new Vector3(FlipprefabShowX ? scl.x * -1 : scl.x, FlipprefabShowY ? scl.y * -1 : scl.y, scl.z);
                    ShowObject_.transform.SetAsLastSibling();
                    var showObjs = ShowObject_.GetComponentsInChildren<UI_TutorShowObject>();
                    for (var i = 0; i < showObjs.Length; ++i)
                    {
                        showObjs[i].RunEnterAction();
                    }
                }
                if (windowCanvas.worldCamera != null && mEnableMaskObjectAlwaysFollowControl_)
                {
                    var ttzf = MaskObject_.AddComponent<UI_TudorTouchZoneFollow>();
                    if (ttzf != null)
                    {
                        ttzf.Init(l, controlRT, windowCanvas, ShowObject_, mDelayTouchVaildTime_, null);
                    }

                }
                if (MaskImageEnterAction_ != null)
                    MaskImageEnterAction_.Run(img.gameObject);
                if (DialogInfo != null)
                    DialogInfo.ShowDialoa(l);

                mBtnCom_ = control.GetComponent<Button>();
                if (mBtnCom_ != null)
                {
                    mBtnCom_.onClick.AddListener(DoGoNextTutor);
                }
                else
                {
                    ControlObject_ = control.GetComponent<UI_TutorClick>();
                    if (ControlObject_ == null)
                    {
                        ControlObject_ = control.gameObject.AddComponent<UI_TutorClick>();
                    }
                    CallBack_ = () =>
                    {
                        UI_TutorManager.Instance.GoNextTutor();
                    };
                    ControlObject_.OnClick.AddListener(CallBack_);
                    //ControlObject_.OnPointerUpEvent.AddListener(CallBack_);
                }
            }
            else
                UI_TutorManager.Instance.ForceEndTutor();
        }
        void DoGoNextTutor()
        {
            UI_TutorManager.Instance.GoNextTutor();
        }
        public override void OnEndTutor(UI_TutorLayer l)
        {
            base.OnEndTutor(l);
            if (mBtnCom_ != null)
            {
                mBtnCom_.onClick.RemoveListener(DoGoNextTutor);
            }
            l.UI_RayIgnore_.SetEnable(false);
            if (mPreAddMaskableGraphics_ != null)
            {
                GameObject.Destroy(mPreAddMaskableGraphics_);
            }
            if (mPreAddMaskableGraphicsFocus_ != null) 
            {
                GameObject.Destroy(mPreAddMaskableGraphicsFocus_);
            }
               
            if (ShowObject_ != null)
            {
                if (prefabShowObjectAutoDestory_)
                {
                    GameObject.Destroy(ShowObject_);
                }
                else
                {
                    var showObjs = ShowObject_.GetComponentsInChildren<UI_TutorShowObject>();
                    for (var i = 0; i < showObjs.Length; ++i)
                    {
                        showObjs[i].RunOutAction();
                    }
                }
            }
            if (ControlObject_ != null)
            {
                GameObject.Destroy(ControlObject_);
            }
            if (MaskObject_ != null)
            {
                if (MaskImageOutAction_ != null)
                {
                    MaskImageOutAction_.Run(MaskObject_, DOTween.Sequence().AppendCallback(() => { GameObject.Destroy(MaskObject_); }));
                }
                else
                    GameObject.Destroy(MaskObject_);
            }
        }

        string CheckMember()
        {
            if (string.IsNullOrEmpty(WindowName_))
            {
                return "窗体名称非法";
            }
            if (mControlFindType_ == ControlFindType.eName && string.IsNullOrEmpty(ControlName_))
            {
                return "控件名称非法";
            }
            if (mControlFindType_ == ControlFindType.ePath && string.IsNullOrEmpty(ControlPath_))
            {
                return "控件路径非法";
            }
            if (MaskImage_ == null)
            {
                return "遮罩图片非法";
            }
            if (FocusMergeSelectedItem && FocusMaskImage_ == null)
                return "遮罩图片非法";
            return "";
        }
        bool IsShowCheckInfo()
        {
            if (string.IsNullOrEmpty(WindowName_))
            {
                return true;
            }
            if (mControlFindType_ == ControlFindType.eName && string.IsNullOrEmpty(ControlName_))
            {
                return true;
            }
            if (mControlFindType_ == ControlFindType.ePath && string.IsNullOrEmpty(ControlPath_))
            {
                return true;
            }
            if (MaskImage_ == null)
            {
                return true;
            }
            if (FocusMergeSelectedItem && FocusMaskImage_ == null) 
            {
                return true;
            }
            return false;
        }
        bool ShowPrefabShowObjectAutoDestory()
        {
            return prefabShowObject_ != null;
        }
        #region 变量
        public enum ControlFindType
        {
            eName,
            ePath,
        }

        [InfoBox("$CheckMember", "IsShowCheckInfo", InfoMessageType = InfoMessageType.Error)]
        [LabelText("窗体名称"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件")]
        string WindowName_;
        [LabelText("查找控件方式"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件")]
        ControlFindType mControlFindType_;
        [LabelText("控件名称"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件"), ShowIf("@mControlFindType_==ControlFindType.eName")]
        string ControlName_;
        [LabelText("控件路径"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件"), ShowIf("@mControlFindType_==ControlFindType.ePath")]
        string ControlPath_;
        [LabelText("遮罩图片"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件"), PreviewField(Alignment = ObjectFieldAlignment.Left, Height = 40)]
        Sprite MaskImage_ = null;
        [LabelText("遮罩图片展示方式"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件")]
        Image.Type MaskImageType_ = Image.Type.Simple;
        [LabelText("自定义遮罩尺寸"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件")]
        bool CustomizeSize = false;
        [LabelText("遮罩图片尺寸"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件"), ShowIf("@CustomizeSize")]
        Vector2 MaskImageSize_ = Vector2.one * 100;
        [LabelText("可点击延时"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件")]
        float mDelayTouchVaildTime_ = 0f;
        [LabelText("遮罩图片进场动作"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件")]
        ActionSequence MaskImageEnterAction_ = null;
        [LabelText("遮罩图片出场动作"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件")]
        ActionSequence MaskImageOutAction_ = null;
        [LabelText("教学展示对象"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件"), DisableInNonPrefabs]
        GameObject prefabShowObject_ = null;
        [LabelText("翻转教学对象_X"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件"), ShowIf("@prefabShowObject_!=null")]
        bool FlipprefabShowX = false;
        [LabelText("翻转教学对象_Y"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件"), ShowIf("@prefabShowObject_!=null")]
        bool FlipprefabShowY = false;
        [Tooltip("勾选时:进入下一条教学时展示对象自动释放\n不勾选时:需要通过展示对象自行控制释放")]
        [LabelText("教学展示对象自动释放"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件"), ShowIf("ShowPrefabShowObjectAutoDestory")]
        bool prefabShowObjectAutoDestory_ = true;
        //[LabelText("遮罩对象持续追踪控件"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件")]
        bool mEnableMaskObjectAlwaysFollowControl_ = false;
        [LabelText("文本展示"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件")]
        UI_TutorDialog DialogInfo = null;
        [LabelText("是否聚焦merge页当前选中格子"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件")]
        bool FocusMergeSelectedItem = false;
        [LabelText("遮罩图片"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件/聚焦merge"), ShowIf("@FocusMergeSelectedItem"), PreviewField(Alignment = ObjectFieldAlignment.Left, Height = 40)]
        Sprite FocusMaskImage_ = null;
        [LabelText("遮罩图片展示方式"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件/聚焦merge"), ShowIf("@FocusMergeSelectedItem")]
        Image.Type FocusMaskImageType_ = Image.Type.Simple;
        [LabelText("遮罩图片尺寸"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件/聚焦merge"), ShowIf("@FocusMergeSelectedItem")]
        Vector2 FocusMaskImageSize_ = Vector2.one * 100;
        [LabelText("遮罩图片进场动作"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件/聚焦merge"), ShowIf("@FocusMergeSelectedItem")]
        ActionSequence FocusMaskImageEnterAction_ = null;
        [LabelText("遮罩图片出场动作"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件/聚焦merge"), ShowIf("@FocusMergeSelectedItem")]
        ActionSequence FocusMaskImageOutAction_ = null;

        UnityAction CallBack_;
        UI_TutorClick ControlObject_;
        GameObject MaskObject_;
        GameObject FocusMaskObject_;
        GameObject ShowObject_ = null;
        private Image mPreAddMaskableGraphics_ = null;
        private Image mPreAddMaskableGraphicsFocus_ = null;
        #endregion
        public UI_TutorTouchUIObjet() { }
        public UI_TutorTouchUIObjet(UI_TutorTouchUIObjet src) : base(src)
        {
            this.WindowName_ = src.WindowName_;
            this.ControlName_ = src.ControlName_;
            this.MaskImage_ = src.MaskImage_;
            if (src.MaskImageEnterAction_ != null)
                this.MaskImageEnterAction_ = src.MaskImageEnterAction_.Clone() as ActionSequence;
            else
            {
                this.MaskImageEnterAction_ = null;
            }
            if (src.MaskImageOutAction_ != null)
                this.MaskImageOutAction_ = src.MaskImageOutAction_.Clone() as ActionSequence;
            else
            {
                this.MaskImageOutAction_ = null;
            }

            //聚焦merge页当前选中格子
            if (src.FocusMergeSelectedItem)
            {
                this.FocusMergeSelectedItem = true;
                //遮罩图片
                this.FocusMaskImage_ = src.FocusMaskImage_;
                //遮罩图片进场动作
                if (src.FocusMaskImageEnterAction_ != null)
                    this.FocusMaskImageEnterAction_ = src.FocusMaskImageEnterAction_.Clone() as ActionSequence;
                else
                {
                    this.FocusMaskImageEnterAction_ = null;
                }
                //遮罩图片出场动作
                if (src.FocusMaskImageOutAction_ != null)
                    this.FocusMaskImageOutAction_ = src.FocusMaskImageOutAction_.Clone() as ActionSequence;
                else
                {
                    this.FocusMaskImageOutAction_ = null;
                }
            }

            this.prefabShowObject_ = src.prefabShowObject_;
            this.prefabShowObjectAutoDestory_ = src.prefabShowObjectAutoDestory_;
            if (src.DialogInfo != null)
                this.DialogInfo = src.DialogInfo.Clone();
            else
                this.DialogInfo = null;
        }

        public override UI_TutorAtomBaseData Clone()
        {
            return new UI_TutorTouchUIObjet(this);
        }
    }

    [System.Serializable, ShowOdinSerializedPropertiesInInspector, HideReferenceObjectPicker]
    public class UI_TutorTouchMenuBtnObjet : UI_TutorAtomBaseData
    {
        #region 注册创建回调
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            UI_TutorAtomBaseDataManager.Instance.RegistTutorAtomBaseData(GetShowInMenuName(), Create);
        }
#endif
        #endregion

        public static new string GetShowInMenuName()
        {
            return "点击底栏菜单按钮";
        }

        public static UI_TutorTouchMenuBtnObjet Create()
        {
            return new UI_TutorTouchMenuBtnObjet();
        }

        public override void OnBeginTutor(UI_TutorLayer l)
        {
            //var go = UI_Manager.Instance.GetWindow<GameObject>(WindowName_, LayerName_);
            UISystem.Instance.TryGetUI<UIPanelBase>("UI_MainMenu", out var go);
            if (go != null)
            {
                Transform control = null;
                var names = ControlPath_.Split('/');
                if (names.Length == 0)
                {
                    control = go.transform;
                }
                else
                {
                    var tempTrans = go.transform;
                    var tempIndex = 0;
                    for (var i = 0; i < names.Length; ++i)
                    {
                        tempTrans = tempTrans.GetChildByName(names[tempIndex], false);
                        ++tempIndex;
                        if (tempTrans == null)
                            break;
                    }
                    control = tempTrans;
                }

                if (control == null)
                {
                    UI_TutorManager.Instance.ForceEndTutor();
                    return;
                }

                var maskableGraphic = control.GetComponent<MaskableGraphic>();
                if (maskableGraphic == null)
                {
                    mPreAddMaskableGraphics_ = control.gameObject.AddComponent<Image>();
                    mPreAddMaskableGraphics_.sprite = null;
                    mPreAddMaskableGraphics_.color = new Color(0, 0, 0, 0);
                }
                MaskObject_ = new GameObject();
                var img = MaskObject_.AddComponent<Image>();
                img.sprite = MaskImage_;
                img.type = MaskImageType_;
                img.raycastTarget = false;
                MaskObject_.transform.SetParent(l.transform, false);
                MaskObject_.transform.SetAsFirstSibling();
                MaskObject_.transform.position = control.transform.position;
                var controlRT = control.GetComponent<RectTransform>();
                var size = controlRT.rect.size;
                if (CustomizeSize)
                    MaskObject_.GetComponent<RectTransform>().sizeDelta = MaskImageSize_;
                else
                    MaskObject_.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x, size.y);
                img.material = AssetSystem.Instance.LoadAsset<Material>("__UI__/__StencilWrite__", null).Result;

                go.TryGetComponent<Canvas>(out var windowCanvas);
                if (windowCanvas == null || windowCanvas.worldCamera == null)
                    windowCanvas = UISystem.Instance.PopupCanvas;
                if (windowCanvas.worldCamera != null)
                {
                    //screenPos = windowCanvas.worldCamera.WorldToScreenPoint(control.transform.position);
                    Vector3[] fourCorners = new Vector3[4];
                    controlRT.GetWorldCorners(fourCorners);
                    var screenPos0 = windowCanvas.worldCamera.WorldToScreenPoint(fourCorners[0]);
                    var screenPos1 = windowCanvas.worldCamera.WorldToScreenPoint(fourCorners[1]);
                    var screenPos2 = windowCanvas.worldCamera.WorldToScreenPoint(fourCorners[2]);
                    var screenPos3 = windowCanvas.worldCamera.WorldToScreenPoint(fourCorners[3]);
                    var screenPos = new Vector3(screenPos0.x + (screenPos3.x - screenPos0.x) * 0.5f,
                        screenPos0.y + (screenPos2.y - screenPos0.y) * 0.5f, screenPos0.z);
                    var sw = Screen.width;
                    var sh = Screen.height;
                    var rt = l.GetComponent<RectTransform>();
                    if (rt != null)
                    {
                        MaskObject_.transform.localPosition = new Vector3(rt.rect.size.x * screenPos.x / sw - rt.rect.size.x * 0.5f, rt.rect.size.y * (screenPos.y) / sh - rt.rect.size.y * 0.5f, screenPos.z);
                    }
                    else
                        MaskObject_.transform.position = screenPos;
                    if (!mEnableMaskObjectAlwaysFollowControl_)
                    {
                        l.UI_RayIgnore_.SetNotIgnoreScreenZone(screenPos0.x,
                        screenPos3.x,
                        screenPos1.y,
                        screenPos0.y,
                        mDelayTouchVaildTime_);
                    }

                }
                else
                {
                    var screenPos = control.transform.position;
                    l.UI_RayIgnore_.SetNotIgnoreScreenZone(screenPos.x - size.x * 0.5f, screenPos.x + size.x * 0.5f, screenPos.y + size.y * 0.5f, screenPos.y - size.y * 0.5f, mDelayTouchVaildTime_);
                }

                if (prefabShowObject_ != null)
                {
#if UNITY_EDITOR
                    ShowObject_ = PrefabUtility.InstantiatePrefab(prefabShowObject_) as GameObject;
#else
                    ShowObject_ = GameObject.Instantiate(prefabShowObject_);
#endif
                    ShowObject_.transform.SetParent(l.transform, false);
                    ShowObject_.transform.position = MaskObject_.transform.position;
                    var scl = ShowObject_.transform.localScale;
                    ShowObject_.transform.localScale = new Vector3(FlipprefabShowX ? scl.x * -1 : scl.x, FlipprefabShowY ? scl.y * -1 : scl.y, scl.z);
                    ShowObject_.transform.SetAsLastSibling();
                    var showObjs = ShowObject_.GetComponentsInChildren<UI_TutorShowObject>();
                    for (var i = 0; i < showObjs.Length; ++i)
                    {
                        showObjs[i].RunEnterAction();
                    }
                }
                if (windowCanvas.worldCamera != null && mEnableMaskObjectAlwaysFollowControl_)
                {
                    var ttzf = MaskObject_.AddComponent<UI_TudorTouchZoneFollow>();
                    if (ttzf != null)
                    {
                        ttzf.Init(l, controlRT, windowCanvas, ShowObject_, mDelayTouchVaildTime_, null);
                    }

                }
                if (MaskImageEnterAction_ != null)
                    MaskImageEnterAction_.Run(img.gameObject);
                if (DialogInfo != null)
                    DialogInfo.ShowDialoa(l);

                ControlObject_ = control.GetComponent<UI_TutorClick>();
                if (ControlObject_ == null)
                    ControlObject_ = control.gameObject.AddComponent<UI_TutorClick>();
                ControlObject_.OnClick.AddListener(() =>
                {
                    AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.moveHomePage);
                    HVPageView.SkipToPage(MenuIndex_);
                    UI_TutorManager.Instance.GoNextTutor();
                });
            }
            else
                UI_TutorManager.Instance.ForceEndTutor();
        }
        void DoGoNextTutor()
        {
            UI_TutorManager.Instance.GoNextTutor();
        }
        public override void OnEndTutor(UI_TutorLayer l)
        {
            base.OnEndTutor(l);
            l.UI_RayIgnore_.SetEnable(false);
            if (mPreAddMaskableGraphics_ != null)
            {
                GameObject.Destroy(mPreAddMaskableGraphics_);
            }
            if (ShowObject_ != null)
            {
                if (prefabShowObjectAutoDestory_)
                {
                    GameObject.Destroy(ShowObject_);
                }
                else
                {
                    var showObjs = ShowObject_.GetComponentsInChildren<UI_TutorShowObject>();
                    for (var i = 0; i < showObjs.Length; ++i)
                    {
                        showObjs[i].RunOutAction();
                    }
                }
            }
            if (ControlObject_ != null)
            {
                GameObject.Destroy(ControlObject_);
            }
            if (MaskObject_ != null)
            {
                if (MaskImageOutAction_ != null)
                {
                    MaskImageOutAction_.Run(MaskObject_, DOTween.Sequence().AppendCallback(() => { GameObject.Destroy(MaskObject_); }));
                }
                else
                    GameObject.Destroy(MaskObject_);
            }
        }

        string CheckMember()
        {
            if (MaskImage_ == null)
            {
                return "遮罩图片非法";
            }
            return "";
        }
        bool IsShowCheckInfo()
        {
            if (MaskImage_ == null)
            {
                return true;
            }
            return false;
        }
        bool ShowPrefabShowObjectAutoDestory()
        {
            return prefabShowObject_ != null;
        }
        #region 变量
        public enum ControlFindType
        {
            eName,
            ePath,
        }

        [InfoBox("$CheckMember", "IsShowCheckInfo", InfoMessageType = InfoMessageType.Error)]
        [LabelText("控件路径"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击底栏菜单按钮")]
        string ControlPath_;
        [LabelText("跳转索引"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击底栏菜单按钮"), PropertyRange(0, 4)]
        int MenuIndex_ = 0;
        [LabelText("遮罩图片"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击底栏菜单按钮"), PreviewField(Alignment = ObjectFieldAlignment.Left, Height = 40)]
        Sprite MaskImage_ = null;

        [LabelText("遮罩图片展示方式"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击底栏菜单按钮")]
        Image.Type MaskImageType_ = Image.Type.Simple;
        [LabelText("自定义遮罩尺寸"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击底栏菜单按钮")]
        bool CustomizeSize = false;
        [LabelText("遮罩图片尺寸"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击底栏菜单按钮"), ShowIf("@CustomizeSize")]
        Vector2 MaskImageSize_ = Vector2.one * 100;
        [LabelText("可点击延时"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击底栏菜单按钮")]
        float mDelayTouchVaildTime_ = 0f;
        [LabelText("遮罩图片进场动作"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击底栏菜单按钮")]
        ActionSequence MaskImageEnterAction_ = null;
        [LabelText("遮罩图片出场动作"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击底栏菜单按钮")]
        ActionSequence MaskImageOutAction_ = null;
        [LabelText("教学展示对象"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击底栏菜单按钮"), DisableInNonPrefabs]
        GameObject prefabShowObject_ = null;
        [LabelText("翻转教学对象_X"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击底栏菜单按钮"), ShowIf("@prefabShowObject_!=null")]
        bool FlipprefabShowX = false;
        [LabelText("翻转教学对象_Y"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击底栏菜单按钮"), ShowIf("@prefabShowObject_!=null")]
        bool FlipprefabShowY = false;
        [Tooltip("勾选时:进入下一条教学时展示对象自动释放\n不勾选时:需要通过展示对象自行控制释放")]
        [LabelText("教学展示对象自动释放"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击底栏菜单按钮"), ShowIf("ShowPrefabShowObjectAutoDestory")]
        bool prefabShowObjectAutoDestory_ = true;
        //[LabelText("遮罩对象持续追踪控件"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击UI控件")]
        bool mEnableMaskObjectAlwaysFollowControl_ = false;
        [LabelText("文本展示"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击底栏菜单按钮")]
        UI_TutorDialog DialogInfo = null;

        UI_TutorClick ControlObject_;
        GameObject MaskObject_;
        GameObject ShowObject_ = null;
        private Image mPreAddMaskableGraphics_ = null;
        #endregion
        public UI_TutorTouchMenuBtnObjet() { }
        public UI_TutorTouchMenuBtnObjet(UI_TutorTouchMenuBtnObjet src) : base(src)
        {
            this.ControlPath_ = src.ControlPath_;
            this.MenuIndex_ = src.MenuIndex_;
            this.MaskImage_ = src.MaskImage_;
            if (src.MaskImageEnterAction_ != null)
                this.MaskImageEnterAction_ = src.MaskImageEnterAction_.Clone() as ActionSequence;
            else
            {
                this.MaskImageEnterAction_ = null;
            }
            if (src.MaskImageOutAction_ != null)
                this.MaskImageOutAction_ = src.MaskImageOutAction_.Clone() as ActionSequence;
            else
            {
                this.MaskImageOutAction_ = null;
            }
            this.prefabShowObject_ = src.prefabShowObject_;
            this.prefabShowObjectAutoDestory_ = src.prefabShowObjectAutoDestory_;
            if (src.DialogInfo != null)
                this.DialogInfo = src.DialogInfo.Clone();
            else
                this.DialogInfo = null;
        }

        public override UI_TutorAtomBaseData Clone()
        {
            return new UI_TutorTouchMenuBtnObjet(this);
        }
    }

    [System.Serializable, ShowOdinSerializedPropertiesInInspector, HideReferenceObjectPicker]
    public class UI_TutorClickAreaUI : UI_TutorAtomBaseData
    {
        #region 注册创建回调
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            UI_TutorAtomBaseDataManager.Instance.RegistTutorAtomBaseData(GetShowInMenuName(), Create);
        }
#endif
        #endregion

        public static new string GetShowInMenuName()
        {
            return "点击场景UI控件(弃用)";
        }

        public static UI_TutorClickAreaUI Create()
        {
            return new UI_TutorClickAreaUI();
        }
        Button mBtnCom_ = null;
        public override void OnBeginTutor(UI_TutorLayer l)
        {
            UI_TutorManager.Instance.ForceEndTutor();
        }
        void DoGoNextTutor()
        {
            UI_TutorManager.Instance.GoNextTutor();
        }
        public override void OnEndTutor(UI_TutorLayer l)
        {
            base.OnEndTutor(l);
            if (mBtnCom_ != null)
            {
                mBtnCom_.onClick.RemoveListener(DoGoNextTutor);
            }
            l.UI_RayIgnore_.SetEnable(false);
            if (ShowObject_ != null)
                GameObject.Destroy(ShowObject_);
            if (ControlObject_ != null)
                GameObject.Destroy(ControlObject_);
            if (MaskObject_ != null)
                GameObject.Destroy(MaskObject_);
        }

        string CheckMember()
        {
            if (string.IsNullOrEmpty(ControlPath_))
                return "控件路径非法";
            if (MaskImage_ == null)
                return "遮罩图片非法";
            return "";
        }
        bool IsShowCheckInfo()
        {
            if (string.IsNullOrEmpty(ControlPath_))
            {
                return true;
            }
            if (MaskImage_ == null)
            {
                return true;
            }
            return false;
        }
        bool ShowPrefabShowObjectAutoDestory()
        {
            return prefabShowObject_ != null;
        }
        #region 变量
        public enum ControlFindType
        {
            eName,
            ePath,
        }

        [InfoBox("$CheckMember", "IsShowCheckInfo", InfoMessageType = InfoMessageType.Error)]
        [LabelText("控件路径"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击场景UI控件")]
        string ControlPath_;
        [LabelText("遮罩图片"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击场景UI控件"), PreviewField(Alignment = ObjectFieldAlignment.Left, Height = 40)]
        Sprite MaskImage_ = null;
        [LabelText("遮罩图片展示方式"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击场景UI控件")]
        Image.Type MaskImageType_ = Image.Type.Simple;
        [LabelText("遮罩图片尺寸"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击场景UI控件")]
        Vector2 MaskImageSize_ = Vector2.one * 100;
        [LabelText("遮罩图片进场动作"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击场景UI控件")]
        ActionSequence MaskImageEnterAction_ = null;
        [LabelText("可点击延时"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击场景UI控件")]
        float mDelayTouchVaildTime_ = 0f;
        [LabelText("教学展示对象"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击场景UI控件"), DisableInNonPrefabs]
        GameObject prefabShowObject_ = null;
        [LabelText("翻转教学对象_X"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击场景UI控件"), ShowIf("@prefabShowObject_!=null")]
        bool FlipprefabShowX = false;
        [LabelText("翻转教学对象_Y"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击场景UI控件"), ShowIf("@prefabShowObject_!=null")]
        bool FlipprefabShowY = false;
        [LabelText("文本展示"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("点击场景UI控件")]
        UI_TutorDialog DialogInfo = null;

        UnityAction CallBack_;
        UI_TutorClick ControlObject_;
        GameObject MaskObject_;
        GameObject ShowObject_ = null;
        #endregion
        public UI_TutorClickAreaUI() { }
        public UI_TutorClickAreaUI(UI_TutorClickAreaUI src) : base(src)
        {
            this.ControlPath_ = src.ControlPath_;
            this.MaskImage_ = src.MaskImage_;
            this.MaskImageType_ = src.MaskImageType_;
            if (src.MaskImageEnterAction_ != null)
                this.MaskImageEnterAction_ = src.MaskImageEnterAction_.Clone() as ActionSequence;
            else
                this.MaskImageEnterAction_ = null;
            this.mDelayTouchVaildTime_ = src.mDelayTouchVaildTime_;
            this.prefabShowObject_ = src.prefabShowObject_;
            if (src.DialogInfo != null)
                this.DialogInfo = src.DialogInfo.Clone();
            else
                this.DialogInfo = null;
        }

        public override UI_TutorAtomBaseData Clone()
        {
            return new UI_TutorClickAreaUI(this);
        }
    }

    [System.Serializable, ShowOdinSerializedPropertiesInInspector, HideReferenceObjectPicker]
    public class UI_TutorDelayTime : UI_TutorAtomBaseData
    {
        #region 注册创建回调
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            UI_TutorAtomBaseDataManager.Instance.RegistTutorAtomBaseData(GetShowInMenuName(), Create);
        }
#endif
        #endregion
        public static new string GetShowInMenuName()
        {
            return "延迟";
        }

        public static UI_TutorDelayTime Create()
        {
            return new UI_TutorDelayTime();
        }

        bool TimeCheck(float v, ref string message)
        {
            if (v >= 0.0f)
            {
                return true;
            }
            else
            {
                message = "延迟时间不能为负数!";
                return false;
            }
        }

        public override void OnBeginTutor(UI_TutorLayer l)
        {
            base.OnBeginTutor(l);
            DOTween.Sequence().AppendInterval(DelayTime_).AppendCallback(() =>
            {
                UI_TutorManager.Instance.GoNextTutor();
            }).Play();
            if (DialogInfo != null)
                DialogInfo.ShowDialoa(l);
        }
        #region 变量
        [LabelText("延迟时间"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("延迟"), ValidateInput("TimeCheck")]
        public float DelayTime_ = 0.5f;
        [LabelText("文本展示"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("延迟")]
        UI_TutorDialog DialogInfo = null;
        #endregion

        public UI_TutorDelayTime(UI_TutorDelayTime src) : base(src)
        {
            DelayTime_ = src.DelayTime_;
            if (src.DialogInfo != null)
                this.DialogInfo = src.DialogInfo.Clone();
            else
                this.DialogInfo = null;
        }
        public UI_TutorDelayTime() { }
        public override UI_TutorAtomBaseData Clone()
        {
            return new UI_TutorDelayTime(this);
        }
    }

    [System.Serializable, ShowOdinSerializedPropertiesInInspector, HideReferenceObjectPicker]
    public class UI_TutorShowTips : UI_TutorAtomBaseData
    {
        #region 注册创建回调
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            UI_TutorAtomBaseDataManager.Instance.RegistTutorAtomBaseData(GetShowInMenuName(), Create);
        }
#endif
        #endregion
        public static new string GetShowInMenuName()
        {
            return "教学纯展示";
        }

        public static UI_TutorShowTips Create()
        {
            return new UI_TutorShowTips();
        }

        bool TimeCheck(float v, ref string message)
        {
            if (v >= 0.0f)
            {
                return true;
            }
            else
            {
                message = "时间不能为负数!";
                return false;
            }
        }

        public override void OnBeginTutor(UI_TutorLayer l)
        {
            base.OnBeginTutor(l);
            if (PrefabShowObject_ != null)
            {
#if UNITY_EDITOR
                ShowObject_ = PrefabUtility.InstantiatePrefab(PrefabShowObject_) as GameObject;
#else
                ShowObject_ = GameObject.Instantiate(PrefabShowObject_);
#endif
                ShowObject_.transform.SetParent(l.transform, false);
                ShowObject_.transform.SetAsLastSibling();
                var showObjs = ShowObject_.GetComponentsInChildren<UI_TutorShowObject>();
                for (var i = 0; i < showObjs.Length; ++i)
                {
                    showObjs[i].RunEnterAction();
                }
            }

            if (AutoGoNextTutorTime > 0)
            {
                RunningSequence_ = DOTween.Sequence().AppendInterval(AutoGoNextTutorTime).AppendCallback(() =>
                {
                    UI_TutorManager.Instance.GoNextTutor();
                }).SetAutoKill(true);
            }
            if (CanTouchGoNextTutor)
            {
                if (TouchGoNextTutorDelayTime > 0)
                {
                    RunningSequence_ = DOTween.Sequence().AppendInterval(TouchGoNextTutorDelayTime).AppendCallback(() =>
                    {
                        ControlObject_ = l.MaskLayer_.GetComponent<UI_TutorClick>();
                        if (ControlObject_ == null)
                        {
                            ControlObject_ = l.MaskLayer_.gameObject.AddComponent<UI_TutorClick>();
                        }
                        CallBack_ = () =>
                        {
                            UI_TutorManager.Instance.GoNextTutor();
                        };
                        ControlObject_.OnClick.AddListener(CallBack_);
                    }).SetAutoKill(true);
                }
                else
                {
                    ControlObject_ = l.MaskLayer_.GetComponent<UI_TutorClick>();
                    if (ControlObject_ == null)
                    {
                        ControlObject_ = l.MaskLayer_.gameObject.AddComponent<UI_TutorClick>();
                    }
                    CallBack_ = () =>
                    {
                        UI_TutorManager.Instance.GoNextTutor();
                    };
                    ControlObject_.OnClick.AddListener(CallBack_);
                }
            }
        }

        public override void OnEndTutor(UI_TutorLayer l)
        {
            if (ShowObject_ != null)
            {
                if (prefabShowObjectAutoDestory_)
                {
                    GameObject.Destroy(ShowObject_);
                }
                else
                {
                    var showObjs = ShowObject_.GetComponentsInChildren<UI_TutorShowObject>();
                    for (var i = 0; i < showObjs.Length; ++i)
                    {
                        showObjs[i].RunOutAction();
                    }
                }
            }
            if (RunningSequence_ != null)
            {
                if (RunningSequence_.IsPlaying())
                {
                    RunningSequence_.Pause();
                }
                RunningSequence_.Kill();
            }
            if (ControlObject_ != null)
            {
                GameObject.Destroy(ControlObject_);
            }
        }

        bool ShowPrefabShowObjectAutoDestory()
        {
            return PrefabShowObject_ != null;
        }
        bool IsCanTouchGoNextTutor()
        {
            return CanTouchGoNextTutor;
        }
        #region 变量

        [LabelText("自动执行下一步教学时间"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("教学纯展示"), ValidateInput("TimeCheck")]
        public float AutoGoNextTutorTime = 0.5f;
        [LabelText("点击进入下一步"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("教学纯展示")]
        public bool CanTouchGoNextTutor = true;
        [LabelText("点击进入生效延迟"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("教学纯展示"), ShowIf("IsCanTouchGoNextTutor")]
        public float TouchGoNextTutorDelayTime = 0.0f;
        [LabelText("教学展示对象"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("教学纯展示"), DisableInNonPrefabs]
        GameObject PrefabShowObject_ = null;
        [Tooltip("勾选时:进入下一条教学时展示对象自动释放\n不勾选时:需要通过展示对象自行控制释放")]
        [LabelText("教学展示对象自动释放"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("教学纯展示"), ShowIf("ShowPrefabShowObjectAutoDestory")]
        bool prefabShowObjectAutoDestory_ = true;

        GameObject ShowObject_ = null;
        UI_TutorClick ControlObject_ = null;
        UnityAction CallBack_ = null;
        Sequence RunningSequence_ = null;
        #endregion

        public UI_TutorShowTips() { }
        public UI_TutorShowTips(UI_TutorShowTips src)
        {
            AutoGoNextTutorTime = src.AutoGoNextTutorTime;
            CanTouchGoNextTutor = src.CanTouchGoNextTutor;
            TouchGoNextTutorDelayTime = src.TouchGoNextTutorDelayTime;
            PrefabShowObject_ = src.PrefabShowObject_;
            prefabShowObjectAutoDestory_ = src.prefabShowObjectAutoDestory_;
        }
        public override UI_TutorAtomBaseData Clone()
        {
            return new UI_TutorShowTips(this);
        }
    }

    #region Merge专属
    [System.Serializable, ShowOdinSerializedPropertiesInInspector, HideReferenceObjectPicker]
    public class UI_TutorMergeClick : UI_TutorAtomBaseData
    {
        #region 注册创建回调
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            UI_TutorAtomBaseDataManager.Instance.RegistTutorAtomBaseData(GetShowInMenuName(), Create);
        }
#endif
        #endregion

        public static new string GetShowInMenuName()
        {
            return "Merge专属/点击事件";
        }

        public static UI_TutorMergeClick Create()
        {
            return new UI_TutorMergeClick();
        }
        Button mBtnCom_ = null;
        public override void OnBeginTutor(UI_TutorLayer l)
        {
            UI_TutorManager.Instance.curMergeClickPos = ClickMapPos;

            UISystem.Instance.TryGetUI<UIPanelBase>(Consts.UIPanel_Merge, out var go);
            if (go != null && go.gameObject.activeSelf)
            {
                Transform control = null;
                var curMap = MergeLevelManager.Instance.CurrentMapData;
                if (curMap != null && curMap.itemDataDict.TryGetValue(ClickMapPos, out var mapItem))
                {
                    if (mapItem != null && mapItem.ItemGO != null)
                    {
                        var maskableGraphic = mapItem.ItemGO.GetComponent<MaskableGraphic>();
                        if (maskableGraphic == null)
                        {
                            mPreAddMaskableGraphics_ = mapItem.ItemGO.gameObject.AddComponent<Image>();
                            mPreAddMaskableGraphics_.sprite = null;
                            mPreAddMaskableGraphics_.color = new Color(0, 0, 0, 0);
                        }
                        control = mapItem.ItemGO.transform;
                    }
                }

                if (control == null)
                {
                    UI_TutorManager.Instance.ForceEndTutor();
                    return;
                }

                //遮罩处理
                MaskObject_ = new GameObject();
                var img = MaskObject_.AddComponent<Image>();
                img.sprite = MaskImage_;
                img.type = MaskImageType_;
                img.raycastTarget = false;
                img.material = AssetSystem.Instance.LoadAsset<Material>("__UI__/__StencilWrite__", null).Result;
                MaskObject_.transform.SetParent(l.transform, false);
                MaskObject_.transform.SetAsFirstSibling();
                MaskObject_.GetComponent<RectTransform>().sizeDelta = Vector2.one * UI_TutorManager.GridWidth;
                var rx = UI_TutorManager.GridWidth * (1 + MaskAddLeft + MaskAddRight);
                var ry = UI_TutorManager.GridWidth * (1 + MaskAddUp + MaskAddDown);
                MaskObject_.GetComponent<RectTransform>().sizeDelta = new Vector2(rx, ry);

                var uiCamera = UISystem.Instance.UICamera;
                if (uiCamera != null)
                {
                    MaskObject_.transform.position = uiCamera.WorldToScreenPoint(control.position);
                    ShowObjectPos_ = MaskObject_.transform.position;

                    Vector3[] fourCorners = new Vector3[4];
                    control.GetComponent<RectTransform>().GetWorldCorners(fourCorners);
                    var screenPos0 = uiCamera.WorldToScreenPoint(fourCorners[0]);
                    var screenPos1 = uiCamera.WorldToScreenPoint(fourCorners[1]);
                    var screenPos2 = uiCamera.WorldToScreenPoint(fourCorners[2]);
                    var screenPos3 = uiCamera.WorldToScreenPoint(fourCorners[3]);

                    l.UI_RayIgnore_.SetNotIgnoreScreenZone(screenPos0.x,
                        screenPos3.x,
                        screenPos1.y,
                        screenPos0.y,
                        mDelayTouchVaildTime_);
                }
                else
                {
                    var screenPos = control.position;
                    MaskObject_.transform.position = screenPos;
                    ShowObjectPos_ = screenPos;

                    var size = control.GetComponent<RectTransform>().rect.size;
                    l.UI_RayIgnore_.SetNotIgnoreScreenZone(screenPos.x - size.x * 0.5f,
                        screenPos.x + size.x * 0.5f,
                        screenPos.y + size.y * 0.5f,
                        screenPos.y - size.y * 0.5f,
                        mDelayTouchVaildTime_);
                }

                if (MaskAddLeft != MaskAddRight)
                {
                    var pos = MaskObject_.transform.localPosition;
                    var xOffset = (MaskAddRight - MaskAddLeft) * UI_TutorManager.GridWidth * 0.5f;
                    MaskObject_.transform.localPosition = new Vector3(pos.x + xOffset, pos.y, pos.z);
                }
                if (MaskAddUp != MaskAddDown)
                {
                    var pos = MaskObject_.transform.localPosition;
                    var yOffset = (MaskAddUp - MaskAddDown) * UI_TutorManager.GridWidth * 0.5f;
                    MaskObject_.transform.localPosition = new Vector3(pos.x, pos.y + yOffset, pos.z);
                }

                if (prefabShowObject_ != null)
                {
#if UNITY_EDITOR
                    ShowObject_ = PrefabUtility.InstantiatePrefab(prefabShowObject_) as GameObject;
#else
                    ShowObject_ = GameObject.Instantiate(prefabShowObject_);
#endif
                    ShowObject_.transform.SetParent(l.transform, false);
                    ShowObject_.transform.position = ShowObjectPos_;
                    var scl = ShowObject_.transform.localScale;
                    ShowObject_.transform.localScale = new Vector3(FlipprefabShowX ? scl.x * -1 : scl.x, FlipprefabShowY ? scl.y * -1 : scl.y, scl.z);
                    ShowObject_.transform.SetAsLastSibling();
                }

                if (DialogInfo != null)
                    DialogInfo.ShowDialoa(l);

                if (MaskImageEnterAction_ != null)
                    MaskImageEnterAction_.Run(img.gameObject);

                mBtnCom_ = control.GetComponent<Button>();
                if (mBtnCom_ != null)
                    mBtnCom_.onClick.AddListener(DoGoNextTutor);
                else
                {
                    ControlObject_ = control.GetComponent<UI_TutorClick>();
                    if (ControlObject_ == null)
                        ControlObject_ = control.gameObject.AddComponent<UI_TutorClick>();
                    ControlObject_.OnClick.AddListener(DoGoNextTutor);
                }
                clickTimes_ = 0;
            }
            else
                UI_TutorManager.Instance.ForceEndTutor();
        }
        void DoGoNextTutor()
        {
            if (++clickTimes_ >= ClickTimes)
                UI_TutorManager.Instance.GoNextTutor();
            else if (ControlObject_ != null)
                ControlObject_.ReUseEvent();
        }
        public override void OnEndTutor(UI_TutorLayer l)
        {
            UI_TutorManager.Instance.curMergeClickPos = Vector2Int.zero;
            base.OnEndTutor(l);
            if (mBtnCom_ != null)
                mBtnCom_.onClick.RemoveListener(DoGoNextTutor);
            l.UI_RayIgnore_.SetEnable(false);
            if (mPreAddMaskableGraphics_ != null)
                GameObject.Destroy(mPreAddMaskableGraphics_);
            if (ShowObject_ != null)
                GameObject.Destroy(ShowObject_);
            if (ControlObject_ != null)
                GameObject.Destroy(ControlObject_);
            if (MaskObject_ != null)
            {
                if (MaskImageOutAction_ != null)
                    MaskImageOutAction_.Run(MaskObject_, DOTween.Sequence().AppendCallback(() => { GameObject.Destroy(MaskObject_); }));
                else
                    GameObject.Destroy(MaskObject_);
            }
        }

        string CheckMember()
        {
            if (ClickMapPos.x <= 0 || ClickMapPos.y <= 0)
                return "点击位置非法";
            if (ClickTimes <= 0)
                return "点击次数非法";
            if (MaskImage_ == null)
                return "遮罩图片非法";
            if (MaskAddUp < 0 || MaskAddDown < 0 || MaskAddLeft < 0 || MaskAddRight < 0)
                return "遮罩区域扩展非法";
            return "";
        }
        bool IsShowCheckInfo()
        {
            if (ClickMapPos.x <= 0 || ClickMapPos.y <= 0)
                return true;
            if (ClickTimes <= 0)
                return true;
            if (MaskImage_ == null)
                return true;
            if (MaskAddUp < 0 || MaskAddDown < 0 || MaskAddLeft < 0 || MaskAddRight < 0)
                return true;
            return false;
        }
        #region 变量
        public enum ControlFindType
        {
            eName,
            ePath,
        }
        [InfoBox("$CheckMember", "IsShowCheckInfo", InfoMessageType = InfoMessageType.Error)]
        [LabelText("点击位置"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge点击事件")]
        Vector2Int ClickMapPos = Vector2Int.one;
        [LabelText("点击次数"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge点击事件")]
        int ClickTimes = 1;
        [LabelText("遮罩图片"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge点击事件"), PreviewField(Alignment = ObjectFieldAlignment.Left, Height = 40)]
        Sprite MaskImage_ = null;
        [LabelText("遮罩图片展示方式"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge点击事件")]
        Image.Type MaskImageType_ = Image.Type.Simple;
        [LabelText("上"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge点击事件/遮罩区域扩展")]
        int MaskAddUp = 0;
        [LabelText("下"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge点击事件/遮罩区域扩展")]
        int MaskAddDown = 0;
        [LabelText("左"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge点击事件/遮罩区域扩展")]
        int MaskAddLeft = 0;
        [LabelText("右"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge点击事件/遮罩区域扩展")]
        int MaskAddRight = 0;
        [LabelText("遮罩图片进场动作"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge点击事件")]
        ActionSequence MaskImageEnterAction_ = null;
        [LabelText("遮罩图片出场动作"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge点击事件")]
        ActionSequence MaskImageOutAction_ = null;
        [LabelText("可点击延时"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge点击事件")]
        float mDelayTouchVaildTime_ = 0f;
        [LabelText("教学展示对象"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge点击事件"), DisableInNonPrefabs]
        GameObject prefabShowObject_ = null;
        [LabelText("翻转教学对象_X"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge点击事件"), ShowIf("@prefabShowObject_!=null")]
        bool FlipprefabShowX = false;
        [LabelText("翻转教学对象_Y"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge点击事件"), ShowIf("@prefabShowObject_!=null")]
        bool FlipprefabShowY = false;
        [LabelText("文本展示"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge点击事件")]
        UI_TutorDialog DialogInfo = null;

        UI_TutorClick ControlObject_;
        GameObject MaskObject_;
        GameObject ShowObject_ = null;
        private Vector3 ShowObjectPos_;
        private Image mPreAddMaskableGraphics_ = null;
        private int clickTimes_ = 0;
        #endregion
        public UI_TutorMergeClick() { }
        public UI_TutorMergeClick(UI_TutorMergeClick src) : base(src)
        {
            this.ClickMapPos = src.ClickMapPos;
            this.MaskImage_ = src.MaskImage_;
            this.MaskImageType_ = src.MaskImageType_;
            this.MaskAddUp = src.MaskAddUp;
            this.MaskAddDown = src.MaskAddDown;
            this.MaskAddLeft = src.MaskAddLeft;
            this.MaskAddRight = src.MaskAddRight;
            if (src.MaskImageEnterAction_ != null)
                this.MaskImageEnterAction_ = src.MaskImageEnterAction_.Clone() as ActionSequence;
            else
                this.MaskImageEnterAction_ = null;
            if (src.MaskImageOutAction_ != null)
                this.MaskImageOutAction_ = src.MaskImageOutAction_.Clone() as ActionSequence;
            else
                this.MaskImageOutAction_ = null;
            this.mDelayTouchVaildTime_ = src.mDelayTouchVaildTime_;
            this.prefabShowObject_ = src.prefabShowObject_;
            if (src.DialogInfo != null)
                this.DialogInfo = src.DialogInfo.Clone();
            else
                this.DialogInfo = null;
        }

        public override UI_TutorAtomBaseData Clone()
        {
            return new UI_TutorMergeClick(this);
        }
    }

    [System.Serializable, ShowOdinSerializedPropertiesInInspector, HideReferenceObjectPicker]
    public class UI_TutorMergeDrag : UI_TutorAtomBaseData
    {
        #region 注册创建回调
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnGameLoaded()
        {
            UI_TutorAtomBaseDataManager.Instance.RegistTutorAtomBaseData(GetShowInMenuName(), Create);
        }
#endif
        #endregion

        public static new string GetShowInMenuName()
        {
            return "Merge专属/拖拽事件";
        }

        public static UI_TutorMergeDrag Create()
        {
            return new UI_TutorMergeDrag();
        }
        public override void OnBeginTutor(UI_TutorLayer l)
        {
            UI_TutorManager.Instance.curMergeDragStartPos = DragStartPos;

            UISystem.Instance.TryGetUI<UIPanelBase>(Consts.UIPanel_Merge, out var go);
            if (go != null && go.gameObject.activeSelf)
            {
                Transform control = null;
                Transform endTrans = null;
                Transform bagControl = null;
                var curMap = MergeLevelManager.Instance.CurrentMapData;
                if (curMap != null && curMap.itemDataDict.TryGetValue(DragStartPos, out var startItem))
                {
                    if (startItem != null && startItem.ItemGO != null)
                    {
                        var maskableGraphic = startItem.ItemGO.GetComponent<MaskableGraphic>();
                        if (maskableGraphic == null)
                        {
                            mPreAddMaskableGraphics_ = startItem.ItemGO.gameObject.AddComponent<Image>();
                            mPreAddMaskableGraphics_.sprite = null;
                            mPreAddMaskableGraphics_.color = new Color(0, 0, 0, 0);
                        }
                        control = startItem.ItemGO.transform;
                    }

                    if (DragToBag)
                    {
                        bagControl = go.transform.GetChildByName(BagControlName, true);
                        var maskableGraphic = bagControl.GetComponent<MaskableGraphic>();
                        if (maskableGraphic == null)
                        {
                            mPreAddMaskableGraphicsForBag_ = bagControl.gameObject.AddComponent<Image>();
                            mPreAddMaskableGraphicsForBag_.sprite = null;
                            mPreAddMaskableGraphicsForBag_.color = new Color(0, 0, 0, 0);
                            bagControl = mPreAddMaskableGraphicsForBag_.transform;
                        }
                    }
                    else if (curMap.gridDataDict.TryGetValue(DragEndPos, out var endGrid))
                    {
                        if (endGrid != null && endGrid.GridGO != null)
                            endTrans = endGrid.GridGO.transform;
                    }
                }

                if (control == null || (DragToBag && bagControl == null) || (!bagControl && endTrans == null))
                {
                    UI_TutorManager.Instance.ForceEndTutor();
                    return;
                }

                //遮罩处理
                MaskObject_ = new GameObject();
                var img = MaskObject_.AddComponent<Image>();
                img.sprite = MaskImage_;
                img.type = MaskImageType_;
                img.raycastTarget = false;
                img.material = AssetSystem.Instance.LoadAsset<Material>("__UI__/__StencilWrite__", null).Result;
                MaskObject_.transform.SetParent(l.transform, false);
                MaskObject_.transform.SetAsFirstSibling();

                if (DragToBag)
                {
                    //MaskObject_.GetComponent<RectTransform>().sizeDelta = Vector2.one * UI_TutorManager.GridWidth;
                    var rx = UI_TutorManager.GridWidth * (1 + MaskAddLeft + MaskAddRight);
                    var ry = UI_TutorManager.GridWidth * (1 + MaskAddUp + MaskAddDown);
                    MaskObject_.GetComponent<RectTransform>().sizeDelta = new Vector2(rx, ry);

                    BagMaskObject_ = new GameObject();
                    var img2 = BagMaskObject_.AddComponent<Image>();
                    img2.sprite = BagMaskImage_;
                    img2.type = BagMaskImageType_;
                    img2.raycastTarget = false;
                    img2.material = AssetSystem.Instance.LoadAsset<Material>("__UI__/__StencilWrite__", null).Result;
                    BagMaskObject_.transform.SetParent(l.transform, false);
                    BagMaskObject_.transform.SetAsFirstSibling();
                    var size = bagControl.GetComponent<RectTransform>().rect.size;
                    BagMaskObject_.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x, size.y) * BagMaskSacle_;
                }
                else
                {
                    UI_TutorManager.Instance.curMergeDragEndPos = DragEndPos;
                    var rx = UI_TutorManager.GridWidth * (Mathf.Abs(DragEndPos.x - DragStartPos.x) + 1 + MaskAddLeft + MaskAddRight);
                    var ry = UI_TutorManager.GridWidth * (Mathf.Abs(DragEndPos.y - DragStartPos.y) + 1 + MaskAddUp + MaskAddDown);
                    MaskObject_.GetComponent<RectTransform>().sizeDelta = new Vector2(rx, ry);
                }

                var uiCamera = UISystem.Instance.UICamera;
                if (uiCamera != null)
                {
                    var sPos = uiCamera.WorldToScreenPoint(control.position);
                    moveStartPos = sPos;
                    if (DragToBag)
                    {
                        MaskObject_.transform.position = sPos;
                        var ePos = uiCamera.WorldToScreenPoint(bagControl.position);
                        BagMaskObject_.transform.position = ePos;
                        moveEndPos = ePos;
                    }
                    else
                    {
                        var ePos = uiCamera.WorldToScreenPoint(endTrans.position);
                        moveEndPos = ePos;
                        MaskObject_.transform.position = sPos + (ePos - sPos) * 0.5f;
                    }

                    Vector3[] fourCorners = new Vector3[4];
                    control.GetComponent<RectTransform>().GetWorldCorners(fourCorners);
                    var screenPos0 = uiCamera.WorldToScreenPoint(fourCorners[0]);
                    var screenPos1 = uiCamera.WorldToScreenPoint(fourCorners[1]);
                    var screenPos2 = uiCamera.WorldToScreenPoint(fourCorners[2]);
                    var screenPos3 = uiCamera.WorldToScreenPoint(fourCorners[3]);
                    l.UI_RayIgnore_.SetNotIgnoreScreenZone(screenPos0.x,
                        screenPos3.x,
                        screenPos1.y,
                        screenPos0.y,
                        mDelayTouchVaildTime_);
                }
                else
                {
                    if (DragToBag)
                    {
                        MaskObject_.transform.position = control.position;
                        BagMaskObject_.transform.position = bagControl.position;
                        moveEndPos = bagControl.position;
                    }
                    else
                    {
                        MaskObject_.transform.position = control.position + (endTrans.position - control.position) * 0.5f;
                        moveEndPos = endTrans.position;
                    }
                    var screenPos = control.position;
                    moveStartPos = screenPos;
                    var size = control.GetComponent<RectTransform>().rect.size;
                    l.UI_RayIgnore_.SetNotIgnoreScreenZone(screenPos.x - size.x * 0.5f,
                        screenPos.x + size.x * 0.5f,
                        screenPos.y + size.y * 0.5f,
                        screenPos.y - size.y * 0.5f,
                        mDelayTouchVaildTime_);
                }

                if (MaskAddLeft != MaskAddRight)
                {
                    var pos = MaskObject_.transform.localPosition;
                    var xOffset = (MaskAddRight - MaskAddLeft) * UI_TutorManager.GridWidth * 0.5f;
                    MaskObject_.transform.localPosition = new Vector3(pos.x + xOffset, pos.y, pos.z);
                }
                if (MaskAddUp != MaskAddDown)
                {
                    var pos = MaskObject_.transform.localPosition;
                    var yOffset = (MaskAddUp - MaskAddDown) * UI_TutorManager.GridWidth * 0.5f;
                    MaskObject_.transform.localPosition = new Vector3(pos.x, pos.y + yOffset, pos.z);
                }

                if (prefabShowObject_ != null)
                {
#if UNITY_EDITOR
                    ShowObject_ = PrefabUtility.InstantiatePrefab(prefabShowObject_) as GameObject;
#else
                    ShowObject_ = GameObject.Instantiate(prefabShowObject_);
#endif
                    ShowObject_.transform.SetParent(l.transform, false);
                    ShowObject_.transform.position = MaskObject_.transform.position;
                    ShowObject_.transform.SetAsLastSibling();

                    var _canvasGroup = ShowObject_.GetComponent<CanvasGroup>();
                    if (_canvasGroup == null)
                        _canvasGroup = ShowObject_.gameObject.AddComponent<CanvasGroup>();
                    _canvasGroup.alpha = 0;
                    var sequence = DOTween.Sequence();
                    sequence.Append(ShowObject_.transform.DOMove(moveStartPos, 0.1f))
                        .Append(DOTween.To(value => _canvasGroup.alpha = value, 0, 1, 0.3f))
                        .Append(ShowObject_.transform.DOMove(moveEndPos, 1f).SetEase(Ease.InOutSine))
                        .Append(DOTween.To(value => _canvasGroup.alpha = value, 1, 0, 0.3f))
                        .AppendInterval(0.2f);
                    sequence.SetLoops(-1);
                    sequence.Play();
                }
                if (MaskImageEnterAction_ != null)
                    MaskImageEnterAction_.Run(MaskObject_);
                if (DragToBag && BagMaskImageEnterAction_ != null)
                    BagMaskImageEnterAction_.Run(BagMaskObject_);
                if (DialogInfo != null)
                    DialogInfo.ShowDialoa(l);
            }
            else
                UI_TutorManager.Instance.ForceEndTutor();
        }

        public override void OnEndTutor(UI_TutorLayer l)
        {
            UI_TutorManager.Instance.curMergeDragStartPos = Vector2Int.zero;
            UI_TutorManager.Instance.curMergeDragEndPos = Vector2Int.zero;
            base.OnEndTutor(l);
            l.UI_RayIgnore_.SetEnable(false);
            if (mPreAddMaskableGraphics_ != null)
                GameObject.Destroy(mPreAddMaskableGraphics_);
            if (mPreAddMaskableGraphicsForBag_ != null)
                GameObject.Destroy(mPreAddMaskableGraphicsForBag_);
            if (ShowObject_ != null)
                GameObject.Destroy(ShowObject_);
            if (MaskObject_ != null)
            {
                if (MaskImageOutAction_ != null)
                    MaskImageOutAction_.Run(MaskObject_, DOTween.Sequence().AppendCallback(() => { GameObject.Destroy(MaskObject_); }));
                else
                    GameObject.Destroy(MaskObject_);
            }
            if (BagMaskObject_ != null)
                GameObject.Destroy(BagMaskObject_);
        }

        string CheckMember()
        {
            if (DragStartPos.x <= 0 || DragStartPos.y <= 0)
                return "拖拽起点位置非法";
            if (!DragToBag && (DragEndPos.x <= 0 || DragEndPos.y <= 0))
                return "拖拽终点位置非法";
            if (DragToBag && string.IsNullOrEmpty(BagControlName))
                return "背包UI控件名称非法";
            if (MaskImage_ == null)
                return "遮罩图片非法";
            if (MaskAddUp < 0 || MaskAddDown < 0 || MaskAddLeft < 0 || MaskAddRight < 0)
                return "遮罩区域扩展非法";
            if (DragToBag && BagMaskImage_ == null)
                return "背包遮罩图片非法";
            return "";
        }
        bool IsShowCheckInfo()
        {
            if (DragStartPos.x <= 0 || DragStartPos.y <= 0)
                return true;
            if (!DragToBag && (DragEndPos.x <= 0 || DragEndPos.y <= 0))
                return true;
            if (DragToBag && string.IsNullOrEmpty(BagControlName))
                return true;
            if (MaskImage_ == null)
                return true;
            if (MaskAddUp < 0 || MaskAddDown < 0 || MaskAddLeft < 0 || MaskAddRight < 0)
                return true;
            if (DragToBag && BagMaskImage_ == null)
                return true;
            return false;
        }
        #region 变量
        public enum ControlFindType
        {
            eName,
            ePath,
        }
        [InfoBox("$CheckMember", "IsShowCheckInfo", InfoMessageType = InfoMessageType.Error)]
        [LabelText("拖拽至背包"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件")]
        bool DragToBag = false;
        [LabelText("拖拽起点位置"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件")]
        Vector2Int DragStartPos = Vector2Int.one;
        [LabelText("拖拽终点位置"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件"), ShowIf("@!DragToBag")]
        Vector2Int DragEndPos = Vector2Int.one;
        [LabelText("背包UI控件名称"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件"), ShowIf("DragToBag")]
        string BagControlName;
        [LabelText("遮罩图片"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件/遮罩图片"), PreviewField(Alignment = ObjectFieldAlignment.Left, Height = 40)]
        Sprite MaskImage_ = null;
        [LabelText("遮罩图片展示方式"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件/遮罩图片")]
        Image.Type MaskImageType_ = Image.Type.Simple;
        //[LabelText("遮罩区域缩放系数"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件/遮罩图片")]
        //float MaskSacle_ = 1;
        [LabelText("上"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件/遮罩图片/遮罩区域扩展")]
        int MaskAddUp = 0;
        [LabelText("下"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件/遮罩图片/遮罩区域扩展")]
        int MaskAddDown = 0;
        [LabelText("左"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件/遮罩图片/遮罩区域扩展")]
        int MaskAddLeft = 0;
        [LabelText("右"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件/遮罩图片/遮罩区域扩展")]
        int MaskAddRight = 0;
        [LabelText("遮罩图片进场动作"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件/遮罩图片")]
        ActionSequence MaskImageEnterAction_ = null;
        [LabelText("遮罩图片出场场动作"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件/遮罩图片")]
        ActionSequence MaskImageOutAction_ = null;
        [LabelText("背包遮罩图片"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件/背包遮罩图片"), ShowIf("DragToBag"), PreviewField(Alignment = ObjectFieldAlignment.Left, Height = 40)]
        Sprite BagMaskImage_ = null;
        [LabelText("背包遮罩图片展示方式"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件/背包遮罩图片"), ShowIf("DragToBag")]
        Image.Type BagMaskImageType_ = Image.Type.Simple;
        [LabelText("背包遮罩区域缩放系数"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件/背包遮罩图片"), ShowIf("DragToBag")]
        float BagMaskSacle_ = 1;
        [LabelText("背包遮罩图片进场动作"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件/背包遮罩图片"), ShowIf("DragToBag")]
        ActionSequence BagMaskImageEnterAction_ = null;
        [LabelText("可拖拽延时"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件")]
        float mDelayTouchVaildTime_ = 0f;
        [LabelText("教学展示对象"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件"), DisableInNonPrefabs]
        GameObject prefabShowObject_ = null;
        [LabelText("文本展示"), ShowInInspector, OdinSerialize, System.NonSerialized, BoxGroup("Merge拖拽事件")]
        UI_TutorDialog DialogInfo = null;

        GameObject MaskObject_;
        GameObject BagMaskObject_;
        GameObject ShowObject_ = null;
        private Image mPreAddMaskableGraphics_ = null;
        private Image mPreAddMaskableGraphicsForBag_ = null;
        private Vector3 moveStartPos, moveEndPos;
        #endregion
        public UI_TutorMergeDrag() { }
        public UI_TutorMergeDrag(UI_TutorMergeDrag src) : base(src)
        {
            this.DragToBag = src.DragToBag;
            this.DragStartPos = src.DragStartPos;
            this.DragEndPos = src.DragEndPos;
            this.BagControlName = src.BagControlName;
            this.MaskImage_ = src.MaskImage_;
            this.MaskImageType_ = src.MaskImageType_;
            //this.MaskSacle_ = src.MaskSacle_;
            if (src.MaskImageEnterAction_ != null)
                this.MaskImageEnterAction_ = src.MaskImageEnterAction_.Clone() as ActionSequence;
            else
                this.MaskImageEnterAction_ = null;
            if (src.MaskImageOutAction_ != null)
                this.MaskImageOutAction_ = src.MaskImageOutAction_.Clone() as ActionSequence;
            else
                this.MaskImageOutAction_ = null;
            this.BagMaskImage_ = src.BagMaskImage_;
            this.BagMaskImageType_ = src.BagMaskImageType_;
            this.BagMaskSacle_ = src.BagMaskSacle_;
            if (src.BagMaskImageEnterAction_ != null)
                this.BagMaskImageEnterAction_ = src.BagMaskImageEnterAction_.Clone() as ActionSequence;
            else
                this.BagMaskImageEnterAction_ = null;
            this.mDelayTouchVaildTime_ = src.mDelayTouchVaildTime_;
            this.prefabShowObject_ = src.prefabShowObject_;
            if (src.DialogInfo != null)
                this.DialogInfo = src.DialogInfo.Clone();
            else
                this.DialogInfo = null;
        }

        public override UI_TutorAtomBaseData Clone()
        {
            return new UI_TutorMergeDrag(this);
        }
    }
    #endregion

    [System.Serializable]
    public class UI_TutorAtomBaseData
    {
        public UI_TutorAtomBaseData() { }
        public UI_TutorAtomBaseData(UI_TutorAtomBaseData src) { }
        public virtual void OnBeginTutor(UI_TutorLayer l) { }
        public virtual void OnEndTutor(UI_TutorLayer l)
        {
            l.HideDialog();
        }
        public static string GetShowInMenuName() { return "UI_TutorAtomBaseData"; }
        public virtual UI_TutorAtomBaseData Clone()
        {
            return new UI_TutorAtomBaseData(this);
        }
    }

    public class UI_TutorDialog
    {
        [LabelText("是否使用多语言"), ShowInInspector, OdinSerialize, System.NonSerialized]
        private bool UseTranslate = false;
        [LabelText("文本内容"), ShowInInspector, OdinSerialize, System.NonSerialized, ShowIf("@!UseTranslate")]
        private string StrContent;
        [LabelText("多语言键值"), ShowInInspector, OdinSerialize, System.NonSerialized, ShowIf("UseTranslate")]
        private string StrLanguageKey;
        [Tooltip("仅支持上下调整位置，默认居中显示，(-1,1)区间调整位置")]
        [LabelText("展示位置"), ShowInInspector, OdinSerialize, System.NonSerialized, Range(-1f, 1f)]
        private float ShowPosYRatio = 0f;

        public void ShowDialoa(UI_TutorLayer l)
        {
            var height = l.GetComponent<RectTransform>().sizeDelta.y;
            var posy = height * 0.5f * ShowPosYRatio;
            if (UseTranslate && !string.IsNullOrEmpty(StrLanguageKey))
                l.SetDialogByKey(StrLanguageKey, posy);
            else if (!UseTranslate && !string.IsNullOrEmpty(StrContent))
                l.SetDialogByContent(StrContent, posy);
        }

        public UI_TutorDialog(UI_TutorDialog clone)
        {
            UseTranslate = clone.UseTranslate;
            StrContent = clone.StrContent;
            StrLanguageKey = clone.StrLanguageKey;
            ShowPosYRatio = clone.ShowPosYRatio;
        }
        public UI_TutorDialog Clone()
        {
            UI_TutorDialog clone = new UI_TutorDialog(this);
            return clone;
        }
    }
}
