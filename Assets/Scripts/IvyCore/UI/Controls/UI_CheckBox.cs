using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IvyCore.UI
{
    [Serializable]
    public class ImageOptions
    {
        public Color m_Color = Color.white;
        public Material m_Material = null;
        public Image.Type m_ImageType = Image.Type.Sliced;
        public bool m_PreserveAspect = false;
        public Image.FillMethod m_FillMethod = Image.FillMethod.Horizontal;
        public int m_FillOrigin = 0;
        public float m_FillAmount = 0.5f;
        public bool m_Clockwise = true;

        public void SynchronousToImage(Image srcImage)
        {
            srcImage.color = m_Color;
            srcImage.material = m_Material;
            srcImage.type = m_ImageType;
            srcImage.preserveAspect = m_PreserveAspect;
            srcImage.fillMethod = m_FillMethod;
            srcImage.fillOrigin = m_FillOrigin;
            srcImage.fillClockwise = m_Clockwise;
            srcImage.fillAmount = m_FillAmount;
        }
    }

    [Serializable]
    public class SpineOptions
    {
        public Color m_Color = Color.white;
        public bool m_Loop=true;
        public float m_TimeScale=1.0f;
        public bool m_UnScaleTime=false;
        public bool m_Freeze=false;
    }


    [RequireComponent(typeof(RectTransform))]
    public class UI_CheckBox : Selectable, IPointerClickHandler, ISubmitHandler, ICanvasElement, IPointerDownHandler, IPointerUpHandler
    {
        public enum CheckBoxShowType
        {
            eSprite,
            eSpine
        }
        #region 通用数据区
        [Serializable]
        public class CheckedEvent : UnityEvent<bool>
        { }

        public CheckedEvent onValueChanged = new CheckedEvent();

        [Tooltip("Is the checkBox currently checked or unChecked?")]
        [SerializeField]
        private bool m_IsChecked = true;

        [SerializeField]
        private bool m_PrototypeObjectSizeSynchronousChange = true;
        public bool prototypeObjectSizeSynchronousChange
        {
            get { return m_PrototypeObjectSizeSynchronousChange; }
            set { m_PrototypeObjectSizeSynchronousChange = value; }
        }

        [SerializeField]
        private UI_CheckBoxGroup m_Group;
        public UI_CheckBoxGroup group
        {
            get { return m_Group; }
            set
            {
                m_Group = value;
//#if UNITY_EDITOR
                //if (Application.isPlaying)
//#endif
                {
                    SetToggleGroup(m_Group, true);
                }
            }
        }

        [SerializeField]
        private CheckBoxShowType m_CheckBoxShowType = CheckBoxShowType.eSprite;
        public CheckBoxShowType checkBoxShowType
        {
            get { return m_CheckBoxShowType; }
            set { m_CheckBoxShowType = value; }
        }

        private Transform m_PrototypeObjectTransform;
        public UnityEngine.Transform prototypeObjectTransform
        {
            get
            {
                if(m_PrototypeObjectTransform==null&&this.transform.childCount>0)
                {
                    RefreshPrototypeObjectTransform();
                }
                return m_PrototypeObjectTransform;
            }
            set { m_PrototypeObjectTransform = value; }
        }
        #endregion

        #region sprite数据区
        [SerializeField]
        private Sprite m_checkedSprite;
        [SerializeField]
        private Sprite m_unCheckedSprite;
        [SerializeField]
        private bool m_useSpriteAdvanceOptions=false;

        public bool useSpriteAdvanceOptions
        {
            get { return m_useSpriteAdvanceOptions; }
            set { m_useSpriteAdvanceOptions = value; }
        }

        public Sprite checkedSprite
        {
            get { return m_checkedSprite; }
            set
            {
                m_checkedSprite = value;
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    Set(m_IsChecked, false, false);
#endif
            }
        }
        [SerializeField]
        private ImageOptions m_CheckSpriteOptions;
        public ImageOptions checkSpriteOptions
        {
            get { return m_CheckSpriteOptions; }
            set { m_CheckSpriteOptions = value; }
        }

        public Sprite unCheckedSprite
        {
            get { return m_unCheckedSprite; }
            set
            {
                m_unCheckedSprite = value;
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    Set(m_IsChecked, false, false);
#endif
            }
        }
        [SerializeField]
        private ImageOptions m_UnCheckSpriteOptions;
        public ImageOptions unCheckSpriteOptions
        {
            get { return m_UnCheckSpriteOptions; }
            set { m_UnCheckSpriteOptions = value; }
        }
        #endregion

        #region spine数据区
        [SerializeField]
        private Spine.Unity.SkeletonDataAsset m_SkeletonDataAsset;
        public Spine.Unity.SkeletonDataAsset skeletonDataAsset
        {
            get { return m_SkeletonDataAsset; }
            set
            {
                m_SkeletonDataAsset = value;
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    if (m_SkeletonGraphic != null)
                    {
                        m_SkeletonGraphic.skeletonDataAsset = value;
                    }
                }
#endif
            }
        }
        [SerializeField]
        private Material m_SpineMaterial;
        public UnityEngine.Material spineMaterial
        {
            get { return m_SpineMaterial; }
            set {
                m_SpineMaterial = value;
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    if (m_SkeletonGraphic != null)
                    {
                        m_SkeletonGraphic.material = value;
                    }

                }
#endif
            }
        }

        [SerializeField]
        private string m_SpineCheckedSkinName="";
        public string spineCheckedSkinName
        {
            get { return m_SpineCheckedSkinName; }
            set {
                m_SpineCheckedSkinName = value;
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    if (m_SkeletonGraphic != null&&m_IsChecked)
                    {
                        m_SkeletonGraphic.initialSkinName = value;
                    }

                }
#endif
            }
        }

        [SerializeField]
        private string m_SpineUnCheckedSkinName="";
        public string spineUnCheckedSkinName
        {
            get { return m_SpineUnCheckedSkinName; }
            set {
                m_SpineUnCheckedSkinName = value;
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    if (m_SkeletonGraphic != null && !m_IsChecked)
                    {
                        m_SkeletonGraphic.initialSkinName = value;
                    }

                }
#endif
            }
        }

        [SerializeField]
        private string m_SpineCheckedAnimationName="";
        public string spineCheckedAnimationName
        {
            get { return m_SpineCheckedAnimationName; }
            set {
                m_SpineCheckedAnimationName = value;
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    if (m_SkeletonGraphic != null && m_IsChecked)
                    {
                        m_SkeletonGraphic.startingAnimation = value;
                    }

                }
#endif
            }
        }

        [SerializeField]
        private string m_SpineUnCheckedAnimationName="";
        public string spineUnCheckedAnimationName
        {
            get { return m_SpineUnCheckedAnimationName; }
            set {
                m_SpineUnCheckedAnimationName = value;
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    if (m_SkeletonGraphic != null && !m_IsChecked)
                    {
                        m_SkeletonGraphic.startingAnimation = value;
                    }

                }
#endif
            }
        }

        [SerializeField]
        private bool m_useSpineAdvanceOptions = false;
        public bool useSpineAdvanceOptions
        {
            get { return m_useSpineAdvanceOptions; }
            set { m_useSpineAdvanceOptions = value; }
        }
        [SerializeField]
        private SpineOptions m_CheckedSpineOptions;
        public IvyCore.UI.SpineOptions checkedSpineOptions
        {
            get { return m_CheckedSpineOptions; }
            set { m_CheckedSpineOptions = value; }
        }
        [SerializeField]
        private SpineOptions m_UnCheckedSpineOptions;
        public IvyCore.UI.SpineOptions unCheckedSpineOptions
        {
            get { return m_UnCheckedSpineOptions; }
            set { m_UnCheckedSpineOptions = value; }
        }

        public const string CheckBoxSpriteNodeName = "CheckBoxSprite";
        public const string CheckBoxSpineNodeName = "CheckBoxSpine";

        [SerializeField]
        private Spine.Unity.SkeletonGraphic m_SkeletonGraphic;
        #endregion

        #region 控件构建区
        public void RemovePreAddedChild()
        {
            var tr = this.transform;
            var checkBoxSpriteNode = tr.Find(CheckBoxSpriteNodeName);
            if (checkBoxSpriteNode != null)
            {
                GameObject.DestroyImmediate(checkBoxSpriteNode.gameObject);
            }
            var checkBoxSpineNode = tr.Find(CheckBoxSpineNodeName);
            if (checkBoxSpineNode != null)
            {
                GameObject.DestroyImmediate(checkBoxSpineNode.gameObject);
            }
            m_SkeletonGraphic = null;
            m_PrototypeObjectTransform = null;
        }

        public void GenerateByShowType()
        {
            RemovePreAddedChild();
            switch (m_CheckBoxShowType)
            {
                case CheckBoxShowType.eSprite:
                    {
                        GameObject checkBoxSprite = new GameObject();
                        var rt = checkBoxSprite.AddComponent<RectTransform>();
                        checkBoxSprite.AddComponent<CanvasRenderer>();
                        var imageCom = checkBoxSprite.AddComponent<Image>();
                        this.targetGraphic = imageCom;
                        this.image = imageCom;
                        checkBoxSprite.name = CheckBoxSpriteNodeName;
                        rt.anchoredPosition3D = new Vector3(0, 0, 0);
                        checkBoxSprite.transform.SetParent(this.transform, false);
                    }
                    break;
                case CheckBoxShowType.eSpine:
                    {
                        GameObject checkBoxSpine = new GameObject();
                        var rt = checkBoxSpine.AddComponent<RectTransform>();
                        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
                        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 30);
                        var skeletonGraphic = checkBoxSpine.AddComponent<Spine.Unity.SkeletonGraphic>();
                        this.targetGraphic = skeletonGraphic;
                        checkBoxSpine.name = CheckBoxSpineNodeName;
                        rt.anchoredPosition3D = new Vector3(0, 0, 0);
                        checkBoxSpine.transform.SetParent(this.transform, false);
                        m_SkeletonGraphic = skeletonGraphic;
                    }
                    break;
                default:
                    break;
            }

        }
        #endregion

        protected UI_CheckBox()
        {
        }

        protected override void Awake()
        {
            base.Awake();
            if (group != null)
            {
                SetToggleGroup(group, false);
            }
            Set(m_IsChecked, false, false);
        }

        void RefreshPrototypeObjectTransform()
        {
            var tr = this.transform;
            switch (m_CheckBoxShowType)
            {
                case CheckBoxShowType.eSprite:
                    {
                        prototypeObjectTransform = tr.Find(CheckBoxSpriteNodeName);
                    }
                    break;
                case CheckBoxShowType.eSpine:
                    {
                        prototypeObjectTransform = tr.Find(CheckBoxSpineNodeName);
                    }
                    break;
            }
        }

        public bool isChecked
        {
            get { return m_IsChecked; }
            set
            {
                Set(value);
            }
        }

        public void Refresh()
        {
            Set(m_IsChecked, false, false);
        }

        public void Set(bool value)
        {
            Set(value, true);
        }

        public void Set(bool value, bool sendCallback)
        {
            Set(value, sendCallback, true);
        }

        public void Set(bool value, bool sendCallback, bool checkValueChange)
        {
            if (checkValueChange)
            {
                if (m_IsChecked == value)
                    return;
            }

            // if we are in a group and set to true, do group logic
            m_IsChecked = value;
            if (m_Group != null && this.gameObject.activeSelf)
            {
                if (m_IsChecked && /*(!m_Group.AnyTogglesOn() && */!m_Group.allowSwitchOff)
                {
                    m_IsChecked = true;
                    m_Group.NotifyToggleOn(this);
                }
            }
            if (m_CheckBoxShowType == CheckBoxShowType.eSprite)
            {
                if(image!=null)
                {
                    image.sprite = m_IsChecked ? checkedSprite : unCheckedSprite;
                    if (m_IsChecked)
                    {
                        if (m_useSpriteAdvanceOptions)
                        {
                            checkSpriteOptions.SynchronousToImage(image);
                        }
                    }
                    else
                    {
                        if (m_useSpriteAdvanceOptions)
                        {
                            unCheckSpriteOptions.SynchronousToImage(image);
                        }
                    }
                }
            }
            else if(m_CheckBoxShowType == CheckBoxShowType.eSpine)
            {
                if(m_SkeletonGraphic!=null)
                {
                    if (m_SkeletonDataAsset != null)
                    {
                        if(m_SkeletonGraphic.skeletonDataAsset!=m_SkeletonDataAsset)
                        {
                            m_SkeletonGraphic.skeletonDataAsset = m_SkeletonDataAsset;
                            m_SkeletonGraphic.Initialize(false);
                        }

                        if (m_SkeletonGraphic!=null&&m_SkeletonGraphic.Skeleton == null)
                        {
                            m_SkeletonGraphic.Initialize(false);
                        }

                        m_SkeletonGraphic.Skeleton.SetSkin(m_IsChecked? m_SpineCheckedSkinName:m_SpineUnCheckedSkinName);
                        if (m_useSpineAdvanceOptions)
                        {
#if UNITY_EDITOR
                            if (!Application.isPlaying)
                            {
                                m_SkeletonGraphic.timeScale = m_IsChecked ? m_CheckedSpineOptions.m_TimeScale : m_UnCheckedSpineOptions.m_TimeScale;
                                m_SkeletonGraphic.startingLoop = m_IsChecked ? m_CheckedSpineOptions.m_Loop : m_UnCheckedSpineOptions.m_Loop;
                            }
#endif
                            m_SkeletonGraphic.Skeleton.SetToSetupPose();
                            m_SkeletonGraphic.AnimationState.SetAnimation(0,m_IsChecked?m_SpineCheckedAnimationName:m_SpineUnCheckedAnimationName,m_IsChecked?m_CheckedSpineOptions.m_Loop:m_UnCheckedSpineOptions.m_Loop);
                            m_SkeletonGraphic.AnimationState.TimeScale = m_IsChecked ? m_CheckedSpineOptions.m_TimeScale : m_UnCheckedSpineOptions.m_TimeScale;
                            m_SkeletonGraphic.unscaledTime = m_IsChecked ? m_CheckedSpineOptions.m_UnScaleTime : m_UnCheckedSpineOptions.m_UnScaleTime;
                            m_SkeletonGraphic.color = m_IsChecked ? m_CheckedSpineOptions.m_Color : m_UnCheckedSpineOptions.m_Color;
                            m_SkeletonGraphic.freeze = m_IsChecked ? m_CheckedSpineOptions.m_Freeze : m_UnCheckedSpineOptions.m_Freeze;
                        }
                        else
                        {
#if UNITY_EDITOR
                            if (!Application.isPlaying)
                            {
                                m_SkeletonGraphic.timeScale = 1.0f;
                                m_SkeletonGraphic.startingLoop = true;
                            }
#endif
                            m_SkeletonGraphic.Skeleton.SetToSetupPose();
                            m_SkeletonGraphic.AnimationState.SetAnimation(0, m_IsChecked ? m_SpineCheckedAnimationName : m_SpineUnCheckedAnimationName, true);
                            m_SkeletonGraphic.AnimationState.TimeScale = 1.0f;
                            m_SkeletonGraphic.unscaledTime = false;
                            m_SkeletonGraphic.color = Color.white;
                            m_SkeletonGraphic.freeze = false;
                        }
#if UNITY_EDITOR
                        if (!Application.isPlaying)
                        {
                            m_SkeletonGraphic.UpdateMesh();
                            m_SkeletonGraphic.Update(0);
                            m_SkeletonGraphic.LateUpdate();
                        }
#endif
                    }

                }
            }

            if (sendCallback)
            {
                onValueChanged.Invoke(m_IsChecked);
            }
        }

        private void SetToggleGroup(UI_CheckBoxGroup newGroup, bool setMemberValue)
        {
            UI_CheckBoxGroup oldGroup = m_Group;

            //Sometimes IsActive returns false in OnDisable so don't check for it.
            // Rather remove the toggle too often than too little.
            if (m_Group != null&&newGroup!=m_Group)
                m_Group.UnregisterToggle(this);

            // At runtime the group variable should be set but not when calling this method from OnEnable or OnDisable.
            // That's why we use the setMemberValue parameter.
            if (setMemberValue)
                m_Group = newGroup;

            // Only register to the new group if this Toggle is active.
            if (newGroup != null && IsActive())
                newGroup.RegisterToggle(this);

            // If we are in a new group, and this toggle is on, notify group.
            // Note: Don't refer to m_Group here as it's not guaranteed to have been set.
            if (newGroup != null && newGroup != oldGroup && isChecked && IsActive())
                newGroup.NotifyToggleOn(this);
        }

        public void GraphicUpdateComplete()
        {
        }

        public void LayoutComplete()
        {
        }

        protected new void OnValidate()
        {
            //base.OnValidate();
        }
        
        public virtual void Rebuild(CanvasUpdate executing)
        {
#if UNITY_EDITOR
            if (executing == CanvasUpdate.Prelayout)
                onValueChanged.Invoke(m_IsChecked);
#endif
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            SetToggleGroup(m_Group, false);
#if UNITY_EDITOR
            if(!Application.isPlaying)
                Set(m_IsChecked, false, false);
#endif
        }

        protected override void OnDisable()
        {
            SetToggleGroup(null, false);
            base.OnDisable();
        }

        private void InternalToggle()
        {
            if (!IsActive() || !IsInteractable())
                return;
            isChecked = !isChecked;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            if(m_Group!=null&&m_IsChecked)
            {
            }
            else
            {
                InternalToggle();
            }
            
        }

        void ISubmitHandler.OnSubmit(BaseEventData eventData)
        {
            InternalToggle();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            this.transform.DOScale(new Vector3(0.8f,0.8f,0.8f), 0.3f).SetRelative(false);
        }

        public void Update()
        {
#if UNITY_EDITOR
            if(prototypeObjectSizeSynchronousChange)
            {
                SynchronousRectTransformToChild();
            }
#endif
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            this.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).SetRelative(false);
        }

        public void SynchronousRectTransformToChild()
        {
            switch (checkBoxShowType)
            {
                case CheckBoxShowType.eSprite:
                    {
                        var checkBoxSpriteNode = this.transform.Find(CheckBoxSpriteNodeName);
                        if (checkBoxSpriteNode != null)
                        {
                            var destRT = checkBoxSpriteNode.GetComponent<RectTransform>();
                            var srcRT = this.GetComponent<RectTransform>();
                            if (destRT != null && srcRT != null)
                            {
                                destRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, srcRT.sizeDelta.x);
                                destRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, srcRT.sizeDelta.y);
                                destRT.anchoredPosition3D = new Vector3(0, 0, 0);
                            }
                        }
                    }
                    break;
                case CheckBoxShowType.eSpine:
                    {
                        var checkBoxSpineNode = this.transform.Find(CheckBoxSpineNodeName);
                        if (checkBoxSpineNode != null)
                        {
                            var destRT = checkBoxSpineNode.GetComponent<RectTransform>();
                            var srcRT = this.GetComponent<RectTransform>();
                            if(destRT != null&&srcRT!=null)
                            {
                                destRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, srcRT.sizeDelta.x);
                                destRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, srcRT.sizeDelta.y);
                                destRT.anchoredPosition3D = new Vector3(0, 0, 0);
                            }
                        }
                    }
                    break;
            }
        }
    }
}