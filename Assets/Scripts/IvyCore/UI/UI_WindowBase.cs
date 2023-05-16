using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IvyCore
{
    [System.Obsolete]
    public class UI_WindowBase : UIPanelBase, IWindow
    {

        public Transform Transform
        {
            get { return transform; }
        }

        public virtual void ClearUIComponents()
        {
        }

        public virtual void Start()
        {
            Init();
        }

        public virtual void Init()
        {
        }

        public virtual void OnClose() { }
        public void Close(bool destroy = true)
        {
            OnClose();
            if (UI_Manager.Instance.RemoveWindowByObj(this.gameObject))
            {

            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public virtual void OnHide() { }
        public void Hide()
        {
            OnHide();
            gameObject.SetActive(false);
        }

        public virtual void OnShow() { }
        public void Show()
        {
            gameObject.SetActive(true);
            OnShow();
        }
    }
}


