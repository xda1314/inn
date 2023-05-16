using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IvyCore
{
    [RequireComponent(typeof(RectTransform)), RequireComponent(typeof(Image)), ExecuteInEditMode]
    public class UI_ReferenceImage : MonoBehaviour
    {
#if UNITY_EDITOR
        [Sirenix.OdinInspector.Button("选择外部图片", Sirenix.OdinInspector.ButtonSizes.Medium)]
        void SelectImage()
        {
            string path = UnityEditor.EditorUtility.OpenFilePanel("选择外部图片", "", "");
            if (path.Length > 0)
            {
                ReferenceImagePath = path;
            }
        }
#endif
        string m_ImagePath = "";

        public string ReferenceImagePath
        {
            get { return m_ImagePath; }
            set { LoadReferenceImage(value); }
        }
#if UNITY_EDITOR
        [SerializeField]
        [HideInInspector]
        private Image m_Image = null;
#endif
        Vector3 m_LastPos = new Vector3(-1, -1);
        Vector2 m_LastSize = Vector2.zero;
        // Use this for initialization
        void Start()
        {
#if UNITY_EDITOR
            Init();
#else
            this.gameObject.SetActive(false);
#endif
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadReferenceImage(string path)
        {
            Init();
            if (!m_ImagePath.Equals(path))
            {
                m_ImagePath = path;
#if UNITY_EDITOR
                m_Image.sprite = UI_Utility.LoadSpriteFromExternalResource(m_ImagePath);
                m_Image.SetNativeSize();
                this.gameObject.name = StringTools.GetFileNamePath(m_ImagePath);
#endif
            }
        }

        private void Init()
        {
#if UNITY_EDITOR
            if (m_Image == null)
            {
                m_Image = GetComponent<Image>();
            }
#endif
        }
    }
}