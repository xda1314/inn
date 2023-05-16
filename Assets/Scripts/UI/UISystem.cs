using Ivy;
using Ivy.UI;
using IvyCore;
using Merge.Romance.Contorls;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIShowLayer
{
    None,
    Normal,
    Popup,
    TopResouce,
    TopPopup,
    Dialog,
    Loading,
    Top
}

public class UISystem : UISystemBase<UISystem>
{
    public Transform GameUICoreTran { get; private set; }
    public Transform poolRootTran { get; private set; }


    public Transform normalRootTran { get; private set; }
    public Transform popupRootTran { get; private set; }
    public Transform TopResouceRootTran { get; private set; }
    public Transform TopPopupRootTran { get; private set; }
    public Transform dialogRootTran { get; private set; }
    public Transform loadingRootTran { get; private set; }
    public Transform topRootTran { get; private set; }

    public Camera UICamera { get; private set; }
    public Canvas chatCanvas { get; private set; }
    public Canvas PopupCanvas { get; private set; }
    //public Canvas selectCanvas { get; private set; }

    //public RectTransform uiSelect;
    //public Transform uiSelectTran { get; private set; }
    public UI_MainMenu uiMainMenu { get; private set; }
    public TopResourceManager topResourceManager { get; private set; }
    public UIPanel_TransitionAnimation uiPanel_TransitionAnimation { get; private set; }
    public Branch_Slider branch_Slider { get; private set; }

    private List<string> uiNeedShowList = new List<string>()
    {
     Consts.UI_InternalShop,
     Consts.UI_TopPopup,
     Consts.UIPanel_Collection
    };
    public bool UIIsNeedShowTop(string prefabName)
    {
        foreach (var item in uiNeedShowList)
        {
            if (prefabName == item)
                return true;
        }
        return false;
    }

    private readonly static Dictionary<string, UIPanelBase> mainUIDict = new Dictionary<string, UIPanelBase>();
    private readonly static Dictionary<string, List<UIPanelBase>> cycleDict = new Dictionary<string, List<UIPanelBase>>();//UI回收字典

    public override void InitSystem()
    {
        GameObject gO = new GameObject("UISystem");
        GameObject.DontDestroyOnLoad(gO);
        GameUICoreTran = AssetSystem.Instance.Instantiate("GameCoreUI", gO.transform).transform;

        chatCanvas = GameUICoreTran.Find("ChatCanvas").transform.GetComponent<Canvas>();

        PopupCanvas = GameUICoreTran.Find("PopupCanvas").transform.GetComponent<Canvas>();
        poolRootTran = new GameObject("PoolRoot").transform;
        poolRootTran.SetParent(PopupCanvas.transform);
        UICamera = GameUICoreTran.GetComponentInChildren<Camera>();
        normalRootTran = PopupCanvas.transform.Find("Normal");
        popupRootTran = PopupCanvas.transform.Find("Popup");
        TopResouceRootTran = PopupCanvas.transform.Find("TopResource");
        branch_Slider = PopupCanvas.transform.Find("Branch_Slider").GetComponent<Branch_Slider>();
        TopPopupRootTran = PopupCanvas.transform.Find("TopPopup");
        dialogRootTran = PopupCanvas.transform.Find("Dialog");
        loadingRootTran = PopupCanvas.transform.Find("Loading");
        topRootTran = PopupCanvas.transform.Find("Top");
        uiMainMenu = GameUICoreTran.Find("UI_MainMenu").GetComponent<UI_MainMenu>();
        uiPanel_TransitionAnimation = GameUICoreTran.Find("UIPanel_TransitionAnimation").GetComponent<UIPanel_TransitionAnimation>();
        topResourceManager = TopResouceRootTran.Find("UI_TopResource").GetComponent<TopResourceManager>();
        AddSceneUI();
        base.InitSystem();
    }

    /// <summary>
    /// 添加常驻在场景里面的UI
    /// </summary>
    private void AddSceneUI()
    {
        mainUIDict.Add("UI_MainMenu", GameUICoreTran.Find("UI_MainMenu").GetComponent<UI_MainMenu>());
        mainUIDict.Add("UI_TopResource", TopResouceRootTran.Find("UI_TopResource").GetComponent<TopResourceManager>());
    }

    [Obsolete]
    public void ShowUI(UIDataBase panelData, bool CanSameAsLast, Action startCB = null, Action endCB = null)
    {
        if (panelData == null)
            return;

        // 临时处理解决
        if (CanSameAsLast)
            panelData.SetUniqueID(DateTimeOffset.UtcNow.UtcTicks.ToString());
        ShowUI(panelData, startCB, endCB);
    }

    /// <summary>
    /// 展示UI
    /// </summary>
    /// <param name="panelData"></param>
    /// <param name="canSameAsCurrentShow">是否可以与上一个打开的UI预制体名称相同，防止用户重复点击</param>
    public override void ShowUI(UIDataBase panelData, Action startCB = null, Action endCB = null)
    {
        if (panelData == null || string.IsNullOrEmpty(panelData.UIPrefabName))
            return;

        Internal_ShowUI(panelData, () =>
        {
            //#if UNITY_IOS
            //            // 隐藏苹果登录按钮
            //            uiMainMenu?.TryHideLogInAppleIcon();
            //#endif
            startCB?.Invoke();
        }, () =>
        {
            if (panelData.UIPrefabName == Consts.UIPanel_ShowRewards)
            {

            }
            else
                AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Window_Open);
            CheckInputControl();

            endCB?.Invoke();
        }, (UIBase createUI) =>
        {
            createUI.SetMainGroupAlpha(false);
            createUI.SetMaskGroupAlpha(false, false);

            UIPanelBase uiBase = createUI as UIPanelBase;
            //属于同级关系弹窗 关闭上一个弹窗
            if (uiBase != null)
            {
                if (_uiList.Count > 0 && _uiList[_uiList.Count - 1] != null)
                {
                    UIPanelBase hideUI = _uiList[_uiList.Count - 1] as UIPanelBase;
                    if (hideUI.uiType != UIPanelBase.UIType.BASE && uiBase.uiType != UIPanelBase.UIType.Tip)
                    {
                        uiBase.gameObject.SetActive(false);
                        HideUI(hideUI);
                    }
                }
            }
        });

    }


    private void CheckInputControl()
    {
        if (InputControl.CheckInstance)
        {
            InputControl.Instance.SetActive(_uiList.Count == 0);
            if (_uiList.Count > 0 && UI_TutorManager.Instance.IsTutoring())//教学中途会可能打开页面,会影响事件逻辑
                UI_TutorManager.Instance.IsMenuInputActive = false;
        }
    }

    private Transform GetUIParent(UIShowLayer uiLayer)
    {
        Transform parentTran = null;
        switch (uiLayer)
        {
            case UIShowLayer.Normal:
                parentTran = normalRootTran;
                break;
            case UIShowLayer.Popup:
                parentTran = popupRootTran;
                break;
            case UIShowLayer.TopResouce:
                parentTran = TopResouceRootTran;
                break;
            case UIShowLayer.TopPopup:
                parentTran = TopPopupRootTran;
                break;
            case UIShowLayer.Dialog:
                parentTran = dialogRootTran;
                break;
            case UIShowLayer.Loading:
                parentTran = loadingRootTran;
                break;
            case UIShowLayer.Top:
                parentTran = topRootTran;
                break;
            default:
                parentTran = GameUICoreTran;
                break;
        }
        return parentTran;
    }

    /// <summary>
    /// 关闭UI
    /// </summary>
    /// <param name="popupUI"></param>
    /// <param name="startCB"></param>
    public override void HideUI(UIBase popupUI, Action startCB = null, Action endCB = null)
    {
        if (popupUI == null)
        {
            Debug.LogWarning("popup UI is null.");
            return;
        }

        Internal_HideUI(popupUI, () =>
        {
            startCB?.Invoke();
            AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Window_Close);
        }, () =>
        {
            CheckInputControl();

            //#if UNITY_IOS
            //            // 尝试显示苹果登录按钮
            //            if (!HasUI)
            //            {
            //                uiMainMenu?.TryShowLogInAppleIcon();
            //            }
            //#endif
            endCB?.Invoke();
        });
    }


    // ----------临时修改，（禁止通过此方法去查找主界面非打开ui）
    public new bool TryGetUI<K>(string prefabName, out K ui) where K : UIBase
    {
        foreach (var item in mainUIDict)
        {
            if (item.Value != null && item.Key.Equals(prefabName))
            {
                if (typeof(K).IsAssignableFrom(item.Value.GetType()))
                {
                    ui = item.Value as K;
                    return true;
                }
            }
        }
        return base.TryGetUI(prefabName, out ui);
    }

    public bool TryGetUIOnlyOpen<K>(string prefabName, out K ui) where K : UIBase
    {
        return base.TryGetUI(prefabName, out ui);
    }

    public void AdaptTopUI()
    {
        if (topResourceManager.TryGetComponent<UI_NotchAdapter>(out var adapt))
            adapt.AdaptNotch();
    }

    protected override UIBase GetOrCreateUI(UIDataBase panelData)
    {
        UIPanelBase uiBase = null;
        if (mainUIDict.TryGetValue(panelData.UIPrefabName, out UIPanelBase main_uiList))
        {
            uiBase = main_uiList;
            uiBase.IsAlwaysInHierarchy = true;
            return uiBase;
        }

        if (cycleDict.TryGetValue(panelData.UIPrefabName, out List<UIPanelBase> goList))
        {
            if (goList.Count > 0 && goList[0] != null)
            {
                uiBase = goList[0];
                uiBase.transform.SetParent(GetUIParent(((UIPanelDataBase)panelData).UIShowLayer), false);
                goList.RemoveAt(0);

                uiBase.transform.SetAsLastSibling();
                uiBase.IsAlwaysInHierarchy = false;
                return uiBase;
            }
        }

        GameObject tempGO = AssetSystem.Instance.Instantiate(panelData.UIPrefabName, GetUIParent(((UIPanelDataBase)panelData).UIShowLayer));
        if (tempGO != null)
        {
            uiBase = tempGO.GetComponent<UIPanelBase>();
            uiBase.transform.SetAsLastSibling();
            uiBase.IsAlwaysInHierarchy = false;
        }
        else
        {
            LogUtils.LogError("Cannot create panel:" + panelData.UIPrefabName);
        }
        return uiBase;
    }


    protected override void CycleOrDestoryUI(UIBase popupUI)
    {
        popupUI.gameObject.SetActive(false);
        if (!popupUI.NeedCycle)
        {
            GameObject.Destroy(popupUI.gameObject);
            return;
        }

        if (!popupUI.IsAlwaysInHierarchy)
        {
            if (cycleDict.TryGetValue(popupUI.UIPanelData.UIPrefabName, out List<UIPanelBase> goList))
            {
                goList.Add(popupUI as UIPanelBase);
            }
            else
            {
                goList = new List<UIPanelBase>();
                goList.Add(popupUI as UIPanelBase);
                cycleDict.Add(popupUI.UIPanelData.UIPrefabName, goList);
            }
        }
    }
}
