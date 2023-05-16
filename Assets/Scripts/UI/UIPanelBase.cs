using DG.Tweening;
using Ivy.UI;
using IvyCore;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public abstract class UIPanelBase : UIBase
{
    // 供UISystem使用，判断UI类型
    public UIType uiType = UIType.BASE;

    //UI类型
    public enum UIType
    {
        BASE,
        Popup,
        Tip,
    }

    protected Button BgCloseBtn;

    public override void OnInitUI()
    {
        IsInit = true;
        if (_maskGroup != null)
        {
            BgCloseBtn = _maskGroup.transform.Find("Mask").GetComponent<Button>();

            if (BgCloseBtn != null)
            {
                //BgCloseBtn.interactable = false;
                //Invoke("ResetCloseBtn", 1.0f);
                BgCloseBtn.onClick.AddListener(() =>
                {
                    UISystem.Instance.HideUI(this);
                });
            }
        }
    }

    private void ResetCloseBtn()
    {
        //BgCloseBtn.interactable = true;
    }


    public override IEnumerator OnShowUI()
    {
        yield return null;
        SetMainGroupAlpha(true);
        ShowTween();
    }

    public override IEnumerator OnHideUI()
    {
        yield return HideTween();
        _mainGroup.transform.localScale = Vector3.one;
    }

    public virtual float ShowTween()
    {
        // 弹窗动画
        var childList = transform.GetComponentsInChildren<UI_Base>();
        float maxSec = 0;
        for (var i = 0; i < childList.Length; ++i)
        {
            float sec = childList[i].RunEnterAction();
            if (maxSec < sec)
            {
                maxSec = sec;
            }
        }
        return maxSec;
    }

    public virtual IEnumerator HideTween()
    {
        // 弹窗动画
        var childList = transform.GetComponentsInChildren<UI_Base>();
        float maxSec = 0;
        for (var i = 0; i < childList.Length; ++i)
        {
            float sec = childList[i].RunOutAction();
            if (maxSec < sec)
            {
                maxSec = sec;
            }
        }
        yield return new WaitForSeconds(maxSec);
    }





    public void SetBgCloseBtnEnable(bool tag)
    {
        if (BgCloseBtn != null)
            BgCloseBtn.enabled = tag;
    }
}
