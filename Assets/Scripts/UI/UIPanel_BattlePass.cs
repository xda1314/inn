using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using I2.Loc;
using TMPro;

public class UIPanel_BattlePass : UIPanelBase
{
    [SerializeField] private Button btn_OpenDailyTsk;
    [SerializeField] private Button btn_ShowTutor;
    [SerializeField] private Button btn_Gata;
    [SerializeField] private GameObject scrollView;
    [SerializeField] private UIDrag_GoldReward wrapContent;
    [SerializeField] private RectTransform layoutGroup;
    [SerializeField] private TextMeshProUGUI t_NextRank;
    [SerializeField] private TextMeshProUGUI t_ExpNum;
    [SerializeField] private TextMeshProUGUI t_CountDownTime;

    [SerializeField] private Slider slider;
    [SerializeField] private Button btn_Buy;
    [SerializeField] private TextMeshProUGUI t_Buy;
    [SerializeField] private Button btn_Exit;
    [SerializeField] private TextMeshProUGUI t_GoldPass;
    [SerializeField] private TextMeshProUGUI t_Free;
    //预览
    [SerializeField] private TextMeshProUGUI t_PreviewLevel;
    [SerializeField] private Transform previewVipBg;
    [SerializeField] private TextMeshProUGUI t_PreviewVip;
    [SerializeField] private Transform previewFreeBg;
    [SerializeField] private TextMeshProUGUI t_PreviewFree;
   

    private Dictionary<int, BattlePassDefinition> battlePassDefinition;
    private float StartPosY;
    private const int height = 300;
    private bool todayIsOpened;
    public static Action RefreshBattlepassScore;

    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_Buy.onClick.AddListener(ClickBuyVIP);
        btn_Exit.onClick.AddListener(ClickCloseButton);
        btn_Gata.onClick.AddListener(GataBtnClick);
        btn_OpenDailyTsk.onClick.AddListener(() => 
        {
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_DailyTask));
            ClickCloseButton();
        });
        btn_ShowTutor.onClick.AddListener(() =>
        {
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_TutorBattlepass, UIShowLayer.TopPopup));
        });
        battlePassDefinition = BattlePassDefinition.DefinitionsDict;
        RefreshBattlepassScore += () =>
        {
            RefreshView();
        };
        StartCoroutine(CeateBattlePassItem());
    }
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();      
        RefreshView();
        countDownTime = (int)(BattlePassSystem.Instance.TryGetCurrentMonthFinishDate() - TimeManager.ServerUtcNow()).TotalSeconds;
        t_CountDownTime.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(countDownTime);
        //HintBuy();

       
    }
    /// <summary>
    /// 刷新页面
    /// </summary>
    public void RefreshView() 
    {
        int curExp = BattlePassSystem.Instance.TotalExp;
        int curLevel = BattlePassSystem.Instance.curExpLevel;
        int nextLevel = curLevel + 1 > BattlePassDefinition.maxStage ? BattlePassDefinition.maxStage : curLevel + 1;
        int needExp = 0;
        if (BattlePassDefinition.DefinitionsDict.TryGetValue(nextLevel, out BattlePassDefinition nextLevelDefinition)) 
        {
            needExp = nextLevelDefinition.indexExp;
            int showCurExp = -1;
            if (nextLevel >= BattlePassDefinition.maxStage) 
            {
                showCurExp = nextLevelDefinition.indexExp;
            }
            else if (BattlePassDefinition.DefinitionsDict.TryGetValue(curLevel, out BattlePassDefinition curLevelDefinition))
            {
                showCurExp = curExp - curLevelDefinition.allExp;
            }
            else
            {
                showCurExp = curExp;
            }
            slider.value = (float)showCurExp / needExp;
            t_ExpNum.text = showCurExp.ToString() + "/" + needExp.ToString();
        }
        t_NextRank.text = nextLevel.ToString();

        if (gameObject.activeSelf)
        {
            int index2 = GetCurrentCanCollectRealIndex();
            wrapContent.SpringToItem(index2, height);
        }
        wrapContent.WrapContent(Vector2.zero);
        RefreshPreviewItem();
        RefreshLanguage();
    }
    private void RefreshLanguage() 
    {
        if (BattlePassSystem.Instance.IsPay)
        {
            t_Buy.text = ScriptLocalization.Get("Obj/Chain/PaidButton");
        }
        else
        {
            t_Buy.text = ScriptLocalization.Get("Obj/Chain/BatterpassText3");
        }
        t_PreviewVip.text = ScriptLocalization.Get("Obj/Chain/BatterpassText4");
        t_PreviewFree.text = ScriptLocalization.Get("Obj/Chain/BatterpassText5");
        t_GoldPass.text = ScriptLocalization.Get("Obj/Chain/BatterpassText6");
        t_Free.text = ScriptLocalization.Get("Obj/Chain/PointsRewardText1");
    }


    int countDownTime = 0;
    float timer;
    private void Update()
    {
        if (countDownTime > 0) 
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                timer -= 1;
                countDownTime--;
                t_CountDownTime.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(countDownTime);
            }
        }
       
    }

    private int GetCurrentCanCollectRealIndex()
    {
        int index = BattlePassSystem.Instance.curExpLevel;
        if (index == 0)
        {
            return 0;
        }
        for (int i = 1; i <= index; i++)
        {
            if (BattlePassSystem.Instance.GetRewardStateInPoint(true, i - 1, out _) == BattlePassRewardState.UnlockButNotClaimed)
            {
                return i - 1;
            }
            if (BattlePassSystem.Instance.IsPay && BattlePassSystem.Instance.GetRewardStateInPoint(false, i - 1, out _) == BattlePassRewardState.UnlockButNotClaimed)
            {
                return i - 1;
            }
        }
        return index - 1;
    }

    private IEnumerator CeateBattlePassItem()
    {
        yield return new WaitForEndOfFrame();

        if (battlePassDefinition == null)
            yield return null;

        float endPosY = 0;
        BatterPassItemData[] itemArray = new BatterPassItemData[battlePassDefinition.Count];
        int halfCount = Mathf.FloorToInt(battlePassDefinition.Count / 2);
        for (int i = 0; i < battlePassDefinition.Count; i++)
        {
            BatterPassItemData itemData = new BatterPassItemData
            {
                localPos = new Vector3(0, endPosY - 150, 0),
                anchorPos = new Vector2(0, (halfCount - i) * height),
                itemHeight = height,
                realIndex = i,
            };
            itemArray[i] = itemData;
            endPosY -= height;
        }
        StartPosY = endPosY;
        layoutGroup.sizeDelta = new Vector2(0, Mathf.Abs(StartPosY));
        wrapContent.Init(scrollView, itemArray, CreateItem, CreateItemAsync, DestoryItem, RefreshItem);
    }

    private void RefreshItem(BattlePassRankRewardItem item, int index)
    {
        item.RefreshItemBySlider(battlePassDefinition[index + 1], index);
    }
  

    private void CreateItemAsync(string path, Transform parent, Action<BattlePassRankRewardItem> callBack)
    {

    }
    private BattlePassRankRewardItem CreateItem(string path, Transform parent)
    {
        GameObject gO = AssetSystem.Instance.Instantiate(Consts.UI_BattlePassRankRewardItem, parent);
        if (gO == null)
            return null;

        BattlePassRankRewardItem itemData = gO.GetComponent<BattlePassRankRewardItem>();
        itemData.InitItem();
        return itemData;
    }
    private void DestoryItem(string path, BattlePassRankRewardItem item)
    {
        AssetSystem.Instance.DestoryGameObject(Consts.UI_BattlePassRankRewardItem, item.gameObject);
    }


    /// <summary>
    /// 提示购买battlepass加速礼包
    /// </summary>
    private void HintBuy()
    {
        int days = (BattlePassSystem.Instance.TryGetCurrentMonthFinishDate() - TimeManager.ServerUtcNow()).Days;
        if (days <= 2 && BattlePassSystem.Instance.needShowBuySpeedUpBag && !todayIsOpened) 
        {
            UISystem.Instance.ShowUI(new BuyBP_Data(this, Consts.UIPanel_BuyBattlePassSpeedBag));
        }
        todayIsOpened = true;
    }

    private string saveFreePreviewName = string.Empty;
    private GameObject saveFreePreviewObj = null;
    private string saveVipPreviewName = string.Empty;
    private GameObject saveVipPreviewObj = null;
    /// <summary>
    /// 刷新预览奖励  
    /// </summary>
    private void RefreshPreviewItem() 
    {
        int Level = BattlePassSystem.Instance.curExpLevel < 1 ? 1 : BattlePassSystem.Instance.curExpLevel;//0级当1级处理
        if (battlePassDefinition.TryGetValue(Level, out BattlePassDefinition definition))
        {
            int previewLevel = definition.stageReward;//预览奖励等级
            t_PreviewLevel.text = previewLevel.ToString();
            if (battlePassDefinition.TryGetValue(previewLevel, out BattlePassDefinition previewDefinition)) 
            {
                if (string.IsNullOrEmpty(saveFreePreviewName))
                {
                    saveFreePreviewName = previewDefinition.freeReward.ShowRewardPrefabName;
                    saveFreePreviewObj = AssetSystem.Instance.Instantiate(saveFreePreviewName, previewFreeBg);
                }
                else if (saveFreePreviewName != previewDefinition.freeReward.ShowRewardPrefabName)
                {
                    AssetSystem.Instance.DestoryGameObject(saveFreePreviewName, saveFreePreviewObj);
                    saveFreePreviewName = previewDefinition.freeReward.ShowRewardPrefabName;
                    saveFreePreviewObj = AssetSystem.Instance.Instantiate(saveFreePreviewName, previewFreeBg);
                }

                if (string.IsNullOrEmpty(saveVipPreviewName))
                {
                    saveVipPreviewName = previewDefinition.vipReward.ShowRewardPrefabName;
                    saveVipPreviewObj = AssetSystem.Instance.Instantiate(saveVipPreviewName, previewVipBg);
                }
                else if (saveVipPreviewName != previewDefinition.vipReward.ShowRewardPrefabName)
                {
                    AssetSystem.Instance.DestoryGameObject(saveVipPreviewName, saveVipPreviewObj);
                    saveVipPreviewName = previewDefinition.vipReward.ShowRewardPrefabName;
                    saveVipPreviewObj = AssetSystem.Instance.Instantiate(saveVipPreviewName, previewVipBg);
                }
            }        
        }
    }



    #region 按钮
    private void ClickBuyVIP()
    {
        try
        {
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_BuyBattlePass, UIShowLayer.TopPopup));
        }
        catch (Exception e)
        {
            GameDebug.LogError("打开页面错误:" + e);
        }
    }

    private void ClickCloseButton()
    {
        Page_Play.refreshBattlepassRedPoint?.Invoke();
        UISystem.Instance.HideUI(this);
    }

    private void GataBtnClick() 
    {
        BattlePassSystem.Instance.AddExpByCompleteDailyTask(60);
    }
    #endregion
}
