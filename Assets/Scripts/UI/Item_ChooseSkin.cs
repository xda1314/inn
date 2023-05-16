using DG.Tweening;
using IvyCore;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item_ChooseSkin : MonoBehaviour
{
    [SerializeField] private Transform spineRoot;
    [SerializeField] private GameObject activeBg;
    [SerializeField] private GameObject unActiveBg;

    [SerializeField] private Button btn_Use;
    [SerializeField] private TextMeshProUGUI t_Use;
    [SerializeField] private TextMeshProUGUI t_UnLockAt;
    [SerializeField] private TextMeshProUGUI t_InUse;

    private UIPanel_ChooseSkin uIPanel_ChooseSkin = null;
    private string skinName;
    private bool hasUnLock = false;

    private SkeletonGraphic skeleton;

    public void InitItem(string skinName, UIPanel_ChooseSkin uIPanel)
    {
        uIPanel_ChooseSkin = uIPanel;
        this.skinName = skinName;
        AssetSystem.Instance.LoadAsset<SkeletonDataAsset>(skinName, (skeletonDataAsset) =>
        {
            AssetSystem.Instance.LoadAsset<Material>("SkeletonGraphicDefault", (material) =>
            {
                skeleton = SkeletonGraphic.NewSkeletonGraphicGameObject(skeletonDataAsset, spineRoot, material);
                if (skeleton != null)
                {
                    skeleton.AnimationState.SetAnimation(0, "animation", false);
                }
            });
        });
        btn_Use.onClick.AddListener(UseBtnClick);
    }
    public void RefreshItem(bool isChoose)
    {
        if (skeleton != null)
        {
            skeleton.freeze = true;
        }
        ChooseSkinSystem.getSkinType type = ChooseSkinSystem.Instance.ReturnGetSkinTypeByName(skinName);
        if (ChooseSkinSystem.Instance.ReturnUnLockSkinList().Contains(skinName))
        {
            hasUnLock = true;
            t_Use.text = I2.Loc.ScriptLocalization.Get("Obj/Wallpaper/Btn1");
        }
        else
        {
            hasUnLock = false;
            if (type == ChooseSkinSystem.getSkinType.completeChapter && TaskGoalsDefinition.UnlockSkinDic.TryGetValue(skinName, out int unlockLevel))
            {
                t_UnLockAt.text = string.Format(I2.Loc.ScriptLocalization.Get("Obj/Wallpaper/Btn2"), unlockLevel);
            }
            if (type == ChooseSkinSystem.getSkinType.HalloweenParty)
            {
                t_UnLockAt.text = I2.Loc.ScriptLocalization.Get("Obj/Wallpaper/Btn4");
            }
            if (type == ChooseSkinSystem.getSkinType.ChristmasParty)
            {
                t_UnLockAt.text = I2.Loc.ScriptLocalization.Get("Obj/Wallpaper/Btn5");
            }
            if (type == ChooseSkinSystem.getSkinType.LoverParty)
            {
                t_UnLockAt.text = I2.Loc.ScriptLocalization.Get("Obj/Wallpaper/Btn6");
            }
        }
        t_InUse.text = I2.Loc.ScriptLocalization.Get("Obj/Wallpaper/Btn3");
        t_InUse.transform.parent.gameObject.SetActive(isChoose);

        activeBg.SetActive(hasUnLock);
        unActiveBg.SetActive(!hasUnLock);
        btn_Use.gameObject.SetActive(hasUnLock && !isChoose);
        t_UnLockAt.gameObject.SetActive(!hasUnLock);
    }

    private void ChooseBoxBtnClick()
    {
        if (hasUnLock)
        {
            uIPanel_ChooseSkin.RefreshItem(skinName);
        }

    }
    private void UseBtnClick()
    {
        ChooseSkinSystem.Instance.SetSkinByName(skinName);
        ChooseBoxBtnClick();
    }
}
