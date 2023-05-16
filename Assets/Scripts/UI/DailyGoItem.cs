using Coffee.UIExtensions;
using IvyCore;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DailyGoItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text_title;
    [SerializeField] private TextMeshProUGUI text_enter;
    [SerializeField] private TextMeshProUGUI text_noEnter;
    [SerializeField] private TextMeshProUGUI text_countDown;
    [SerializeField] private Button btn_enter;
    [SerializeField] private GameObject btn_noEnter;
    [SerializeField] private ParticleSystem effect_Finish;
    [SerializeField] private CanvasGroup rewardRoot;
    private DailyActiveSystem dailyActiveSystem;


    private void Start()
    {
        UI_Manager.Instance.RegisterRefreshEvent(gameObject, (str) =>
        {
            RefreshLanguageText();
        }, "RefreshEvent_LanguageChanged");
        btn_enter.onClick.AddListener(() =>
        {
            if (dailyActiveSystem == null)
                return;
            MergeLevelManager.Instance.ShowMergePanelByDungeonType(dailyActiveSystem.Daily_Type);
        });
    }

    public void Init()
    {
        dailyActiveSystem = DailyActiveSystem.Instance;
        if (dailyActiveSystem == null)
            return;
        btn_enter.gameObject.SetActive(dailyActiveSystem.GetNoComplete());
        btn_noEnter.SetActive(!dailyActiveSystem.GetNoComplete());
        rewardRoot.gameObject.SetActive(dailyActiveSystem.GetNoComplete());
        rewardRoot.alpha = 1;
        RefreshLanguageText();
        CreatRewardItems(dailyActiveSystem.ItemRewardList);
    }

    private void RefreshLanguageText()
    {
        text_enter.text = I2.Loc.ScriptLocalization.Get("Obj/Dungeon/ButtonText2");
        text_noEnter.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/DairyTaskButton2");
        text_title.text = I2.Loc.ScriptLocalization.Get(DailyDefinition.DailyDefDic[dailyActiveSystem.Daily_Type].textTitle);
    }

    private void CreatRewardItems(List<MergeRewardItem> ItemRewardList)
    {
        rewardRoot.gameObject.SetActive(dailyActiveSystem.GetNoComplete());
        if (dailyActiveSystem.GetNoComplete()) 
        {
            for (int i = 0; i < ItemRewardList.Count; i++)
            {
                string item_str = "item" + i.ToString();
                Transform item = rewardRoot.transform.Find(item_str);
                if (item == null)
                    continue;
                Transform img_shadow = item.Find("img_shadow");
                if(img_shadow!=null && img_shadow.childCount > 0) 
                {
                    for (int j = img_shadow.childCount - 1; j >= 0; j--)
                        DestroyImmediate(img_shadow.GetChild(j).gameObject);
                }
                
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


    private void Update()
    {
        //每日活动
        if (DailyActiveSystem.Instance.GetNoComplete())
        {
            var leftSeconds = (int)(TimeManager.Instance.GetTomorrowRefreshTimeSpan()).TotalSeconds;
            if (leftSeconds > 0)
            {
                text_countDown.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(leftSeconds);
            }
        }
    }
}
