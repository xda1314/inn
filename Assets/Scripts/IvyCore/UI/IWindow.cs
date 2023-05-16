using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IvyCore
{
    [System.Obsolete]
    public interface IWindow
    {
        Transform Transform { get; }
        void Init();
        void Show();
        void Hide();
        void Close(bool destroy = true);
        void ClearUIComponents();
    }
}

