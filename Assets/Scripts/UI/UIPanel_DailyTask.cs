using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using DG.Tweening;
using TMPro;

public class UIPanel_DailyTask : UIPanelBase
{
    [SerializeField] private Button btn_Close;
    [SerializeField] private TextMeshProUGUI t_Title;
    [SerializeField] private TextMeshProUGUI t_CD;
    [SerializeField] private RectTransform grid;
    [SerializeField] private Slider slider_Progress;
    [SerializeField] private TextMeshProUGUI t_Progress;
    [SerializeField] private TextMeshProUGUI t_CurLevel;
    [SerializeField] private TextMeshProUGUI t_NextLevel;
    [SerializeField] private Button btn_View;
    [SerializeField] private TextMeshProUGUI t_View;
    [SerializeField] private GameObject redPoint;
    [SerializeField] private ParticleSystem effect_SliderFull;

    private List<UI_DailyTaskItem> taskDataList = new List<UI_DailyTaskItem>();
    public override void OnInitUI()
    {
        base.OnInitUI();
        btn_Close.onClick.AddListener(() => CloseBtnClick());
        btn_View.onClick.AddListener(() =>
        {
            UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_BattlePass));
            CloseBtnClick();
        });
        InitItem();
        InitSlider();      
    }
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        RefreshUI();
        RefreshItem();
    }
    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
    }
    private void RefreshUI()
    {
        t_Title.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/DairyTaskTitle");
        t_View.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine1Button4");
        //leftTime = (int)TimeManager.Instance.GetTomorrowRefreshTimeSpan().TotalSeconds;
        leftTime = (int)(BattlePassSystem.Instance.TryGetCurrentMonthFinishDate() - TimeManager.ServerUtcNow()).TotalSeconds;
        t_CD.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(leftTime);
        redPoint.SetActive(BattlePassSystem.Instance.HasRewerdCanClaim());
    }

    int curLevel;
    int nextLevel;
    int showCurExp = -1;
    int needExp = 0;
    bool startSlider = false;
    float curTotalScore = -1;
    private void InitSlider() 
    {
        curTotalScore = BattlePassSystem.Instance.TotalExp;
        int curExp = BattlePassSystem.Instance.TotalExp;
        int curLevel = BattlePassSystem.Instance.curExpLevel;
        int nextLevel = curLevel + 1 > BattlePassDefinition.maxStage ? BattlePassDefinition.maxStage : curLevel + 1;
        if (BattlePassDefinition.DefinitionsDict.TryGetValue(nextLevel, out BattlePassDefinition nextLevelDefinition))
        {
            needExp = nextLevelDefinition.indexExp;
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
            t_Progress.text = showCurExp.ToString() + "/" + needExp.ToString();
        }
        t_CurLevel.text = curLevel.ToString();
        t_NextLevel.text = nextLevel.ToString();
        slider_Progress.value = (float)showCurExp / needExp;
    }
    public void RefreshSlider(Vector3 worldPosFrom) 
    {
        int curExp = BattlePassSystem.Instance.TotalExp;
        curLevel = BattlePassSystem.Instance.curExpLevel;
        nextLevel = curLevel + 1 > BattlePassDefinition.maxStage ? BattlePassDefinition.maxStage : curLevel + 1;       
        if (BattlePassDefinition.DefinitionsDict.TryGetValue(nextLevel, out BattlePassDefinition nextLevelDefinition))
        {
            needExp = nextLevelDefinition.indexExp;           
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
        }       
        PlayTweenScoreFly(Consts.Icon_Reward_BPScore, worldPosFrom, t_CurLevel.transform.position);
    }

    /// <summary>
    /// 积分动画
    /// </summary>
    public void PlayTweenScoreFly(string prefabName, Vector3 tweenFromWorldPos, Vector3 tweenToWorldPos)
    {
        Vector3 localPosition = transform.InverseTransformPoint(tweenFromWorldPos);
        GameObject go = AssetSystem.Instance.Instantiate(prefabName, transform, localPosition, Vector3.zero, Vector3.one);

        Sequence sequence = DOTween.Sequence();
        float duration = 0.5f;
        Vector2 p0 = tweenFromWorldPos;
        Vector2 p2 = tweenToWorldPos; ;
        Vector2 p1 = new Vector2(Vector2.Lerp(p0, p2, 0.3f).x, p0.y - 0.5f);

        sequence.Append(DOTween.To(setter: value =>
        {
            Vector2 vector = DoAnimTools.Bezier(p0, p1, p2, value);
            if (go != null)
                go.transform.position = new Vector3(vector.x, vector.y, 0);
        }, startValue: 0, endValue: 1, duration: duration).SetEase(Ease.InQuad));

        sequence.onComplete = () =>
        {
            AssetSystem.Instance.DestoryGameObject(prefabName, go);
            startSlider = true;
        };
    }

    private void InitItem()
    {
        if (DailyTaskSystem.Instance.hasInitDailyTaskSuccess)
        {
            foreach (var item in DailyTaskSystem.Instance.dailyTaskLocationData)
            {
                if (DailyTaskDefinition.dailyTaskDefinitionDic.TryGetValue(item.Value.dailyTaskId, out DailyTaskDefinition dailyTaskDefinition)) 
                {
                    GameObject go = AssetSystem.Instance.Instantiate(Consts.UI_DailyTaskItem, grid);
                    if (go)
                    {
                        UI_DailyTaskItem dailyTaskItem = go.GetComponent<UI_DailyTaskItem>();
                        if (dailyTaskItem != null)
                        {
                            dailyTaskItem.InitItem(dailyTaskDefinition, this);
                            taskDataList.Add(dailyTaskItem);
                        }
                    }
                }
            }
        }
        else
        {
            GameDebug.LogError("init daily task fail");
        }
    }
    private void RefreshItem()  
    {
        for (int i = 0; i < taskDataList.Count; i++)
        {
            taskDataList[i].RefreshItem();
        }
        SortRefreshItems();
    }
    public void SortRefreshItems() 
    {
        List<UI_DailyTaskItem> completeList = new List<UI_DailyTaskItem>();
        List<UI_DailyTaskItem> hasClaimList = new List<UI_DailyTaskItem>();
        int siblingIndex = 0;
        int maxDailyTaskNum = DailyTaskSystem.Instance.dailyTaskLocationData.Count;
        for (int i = 0; i < taskDataList.Count; i++)
        {
            if (taskDataList[i].ReturnIsComplete())
                completeList.Add(taskDataList[i]);
            if (taskDataList[i].ReturnHasClaim())
                hasClaimList.Add(taskDataList[i]);
        }
        for (int i = 0; i < completeList.Count; i++)
        {
            completeList[i].transform.SetSiblingIndex(siblingIndex);
            siblingIndex++;
        }
        for (int i = 0; i < hasClaimList.Count; i++)
        {
            hasClaimList[i].transform.SetSiblingIndex(maxDailyTaskNum - 1);
            maxDailyTaskNum--;
        }
    }


    int leftTime = -1;
    float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            timer --;
            leftTime--;
            t_CD.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(leftTime);
        }

        if (startSlider && curTotalScore < BattlePassSystem.Instance.TotalExp)  
        {
            int beforeScore = (int)curTotalScore / 100;
            curTotalScore += Time.deltaTime * 30;
            slider_Progress.value = curTotalScore % 100 / needExp;
            int nowScore = (int)curTotalScore / 100;
            if (beforeScore < nowScore) 
            {
                effect_SliderFull.Play();
            }
            if (curTotalScore >= BattlePassSystem.Instance.TotalExp) 
            {
                startSlider = false;
                t_CurLevel.text = curLevel.ToString();
                t_NextLevel.text = nextLevel.ToString();
                t_Progress.text = showCurExp.ToString() + "/" + needExp.ToString();
            }
        }
    }
    public void OpenWhichUI(DailyTaskType taskType)
    {
        switch (taskType)
        {
            case DailyTaskType.AddEnergy:
                try
                {
                    UISystem.Instance.ShowUI(new TopPopupData(TopResourceType.Energy));
                }
                catch (Exception e)
                {
                    GameDebug.LogError("弹出体力界面错误:" + e);
                }
                break;
            case DailyTaskType.Collect:
                MergeLevelManager.Instance.ShowMergePanelByDungeonType(MergeLevelType.mainLine);
                break;
            case DailyTaskType.ShopBuy:
                HVPageView.SkipToPage(0);
                break;
            case DailyTaskType.SpendEnergy:
                MergeLevelManager.Instance.ShowMergePanelByDungeonType(MergeLevelType.mainLine);
                break;
            case DailyTaskType.TaskComplete:
                MergeLevelManager.Instance.ShowMergePanelByDungeonType(MergeLevelType.mainLine);
                UISystem.Instance.ShowUI(new UIPanelDataBase(Consts.UIPanel_Task));
                break;
            default:
                GameDebug.LogError("DailyTaskType error");
                break;
        }

        CloseBtnClick();
    }
    private void CloseBtnClick() 
    {
        Page_Play.refreshDailyTaskRedPoint?.Invoke();
        UISystem.Instance.HideUI(this);
    }
}
