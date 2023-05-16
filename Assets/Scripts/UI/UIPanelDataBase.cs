using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Ivy.UI;

public class UIPanelDataBase : UIDataBase
{
    public new UIShowLayer UIShowLayer { get; private set; }

    public UIPanelDataBase(string prefabName, UIShowLayer UIShowLayer = UIShowLayer.Popup) : base(prefabName, UIShowLayer.ToString())
    {
        this.UIShowLayer = UIShowLayer;
    }

}
