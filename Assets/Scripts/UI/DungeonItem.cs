using Coffee.UIExtensions;
using IvyCore;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DungeonItem : MonoBehaviour
{
    [SerializeField] private UIEffect blurEffect;
    [SerializeField] private Image img_bg;
    [SerializeField] private TextMeshProUGUI text_title;
    [SerializeField] private TextMeshProUGUI text_des;
    [SerializeField] private TextMeshProUGUI btn_entText;
    [SerializeField] private TextMeshProUGUI btn_noEntText;
    [SerializeField] private Button btn_enter;
    [SerializeField] private GameObject btn_noEnter;
    [SerializeField] private GameObject finishTag;
    [SerializeField] private TextMeshProUGUI t_Complete;
    [SerializeField] private ParticleSystem effect_Finish;
    [SerializeField] private Sprite[] img_sprites;
    [SerializeField] private CanvasGroup rewardRoot;


    private DungeonDefinition mDef;
    private DungeonState mState;
    private Action hideAction;
    private void Start()
    {
        UI_Manager.Instance.RegisterRefreshEvent(gameObject, (str) =>
        {
            RefreshLanguageText();
        }, "RefreshEvent_LanguageChanged");
    }

    public void SetData(DungeonDefinition def, DungeonState state)
    {
        mDef = def;
        mState = state;
        RefreshUI();
    }

    public void SetHideAction(Action ac)
    {
        hideAction = ac;
    }

    public void PlayEffect() 
    {
        effect_Finish.Play();
    }
    private void RefreshLanguageText()
    {
        btn_entText.text = I2.Loc.ScriptLocalization.Get("Obj/Dungeon/ButtonText2");
        btn_noEntText.text = I2.Loc.ScriptLocalization.Get("Obj/Dungeon/ButtonText2");
        t_Complete.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/EctypeDescribe11");

        if (mState == DungeonState.locked)
        {
            int nextDungeonOpenLevel = mDef.unlockChapter;
            btn_noEntText.text = string.Format(I2.Loc.ScriptLocalization.Get("Obj/Chain/EctypeDescribe2"), nextDungeonOpenLevel);
        }
        else if (mState == DungeonState.finished)
            btn_noEntText.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/DairyTaskButton2");
        text_des.text = I2.Loc.ScriptLocalization.Get(mDef.GetDescription(mState));
        text_title.text = I2.Loc.ScriptLocalization.Get(mDef.GetTitle(mState));
    }

    private void RefreshUI()
    {    
        foreach (var item in img_sprites)
        {
            if (mDef.GetBg(mState) == item.name)
            {
                img_bg.sprite = item;
            }
        }
        text_des.gameObject.SetActive(mState == DungeonState.locked);
        btn_enter.gameObject.SetActive(mState == DungeonState.unlock);
        btn_noEnter.SetActive(mState == DungeonState.locked);
        finishTag.SetActive(mState == DungeonState.finished);
        if (mState == DungeonState.locked || mState == DungeonState.unlock) 
        {
             blurEffect.enabled = true;
        }
        else if(mState == DungeonState.finished)
        {
            blurEffect.enabled = false;
        }
        rewardRoot.gameObject.SetActive(mState != DungeonState.locked);
        rewardRoot.alpha = 1;
        if (mState == DungeonState.finished) 
        {
            DOTween.To(() => rewardRoot.alpha, (alpha) =>
            {
                rewardRoot.alpha = alpha;
            }, 0, 0.8f);
        }     
        RefreshLanguageText();
        if (mDef == null)
            return;
        RegisterBtnEvent();
        CreatRewardItems(mDef.ItemRewardList);
    }

    private void CreatRewardItems(List<MergeRewardItem> ItemRewardList)
    {
        if (mState == DungeonState.locked) 
        {
            rewardRoot.gameObject.SetActive(false);
        }
        else 
        {
            rewardRoot.gameObject.SetActive(true);
            for (int i = 0; i < ItemRewardList.Count; i++)
            {
                string item_str = "item" + (i + 1).ToString();
                Transform item = rewardRoot.transform.Find(item_str);
                Transform img_shadow = item.Find("img_shadow");
                for (int j = img_shadow.childCount - 1; j >= 0; j--)
                    DestroyImmediate(img_shadow.GetChild(j).gameObject);
                string prefabName = ItemRewardList[i].ShowRewardPrefabName;

                if (item.TryGetComponent(out ShowItemInfo showItemInfo))
                {
                    showItemInfo.RefreshPrefabName(prefabName, null);
                }
                AssetSystem.Instance.InstantiateAsync(prefabName, img_shadow, gO =>
                {
                    RectTransform rect = gO.GetComponents<RectTransform>()[0];
                    rect.pivot = new Vector2(0.5f, 0);
                    rect.localScale = new Vector2(0.5f, 0.5f);
                    rect.localPosition = new Vector2(0, -20);
                });
            }
        }      
    }

    private void RegisterBtnEvent()
    {
        btn_enter.onClick.AddListener(() =>
        {
            hideAction?.Invoke();
            VibrateSystem.Haptic(MoreMountains.NiceVibrations.HapticTypes.Selection);
            MergeLevelManager.Instance.ShowMergePanelByDungeonType(mDef.type);
            Invoke("DelayRefresh", 1f);
        });
    }
    private void DelayRefresh()
    {
        UI_Manager.Instance.InvokeRefreshEvent("", "RefreshEvent_LanguageChanged");
    }
}
