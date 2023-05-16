using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Page_Dungeon : UIPanelBase
{
    [SerializeField] private VerticalLayoutGroup layout;
    [SerializeField] public ScrollRect dungeonScroll;
    [SerializeField] private GameObject dungeonItem;
    private List<DungeonItem> dungeonItemList = new List<DungeonItem>();
   
    public override void OnInitUI()
    {
        base.OnInitUI();
        //副本刷新
        DungeonSystem.Instance.CheckDungeonEventByComplete += () =>
        {
            StartCoroutine(RefreshDungeonUIByComplete());
        };
        DungeonSystem.Instance.CheckDungeonEventByOpen += () =>
        {
            RefreshDungeonUI();
        };
        FestivalSystem.Instance.refreshFestival += () =>
        {
            RefreshTotalBranch();
        };
        BranchSystem.Instance.RefreshBranchEvent += () =>
        {
            RefreshBranchUI();
        };
        DailyActiveSystem.Instance.refreshDaily += () =>
        {
            InitDailyAcitve();
        };
        InitDailyAcitve();
        InitTotalBranch();//支线初始化
        InitDungeonUI();
        RefreshLanguageText();
    }

    public void RefreshBranchUI() 
    {
        InitTotalBranch();
        RefreshTotalBranch();
    }

    #region 多语言刷新
    public void RefreshLanguageText()
    {
        //副本
        int curLevelIndex = TaskGoalsManager.Instance.curLevelIndex;
        int nextDungeonOpenLevel = 0;
        foreach (var item in DungeonDefinition.DungeonDefDic)
        {
            if (curLevelIndex < item.Value.unlockChapter)
            {
                nextDungeonOpenLevel = item.Value.unlockChapter;
                break;
            }
        }
    }
    #endregion

    #region 每日活动
    private GameObject dailyGo;
    private void InitDailyAcitve()
    {
        DailyActiveSystem dailyActive = DailyActiveSystem.Instance;
        if (dailyActive == null)
            return;
        if (dailyGo==null)
            dailyGo = AssetSystem.Instance.Instantiate(Consts.DailyGoItem, layout.transform);
        if (DailyActiveSystem.Instance.GetNoComplete() && dailyGo.TryGetComponent(out DailyGoItem item)) 
        {
            dailyGo.SetActive(true);
            item.Init();
        }
        else
        {
            dailyGo.SetActive(false);
        }
    }
    #endregion

    #region 支线
    bool hasFestival = true;//最多只能允许存在一个节日活动，否则界面超框
    private Dictionary<MergeLevelType, BranchItem> saveBranchDic = new Dictionary<MergeLevelType, BranchItem>();
    private GameObject go;
    private void InitTotalBranch() 
    {
        //普通支线
        if(go == null)
          go = AssetSystem.Instance.Instantiate(Consts.BranchItem, layout.transform);
        if (go && go.TryGetComponent(out BranchItem item) && BranchSystem.Instance != null && BranchSystem.Instance.CurBranchDef != null)
        {
            MergeLevelType mergeLevel = BranchSystem.Instance.CurBranchDef.BranchType;
            if (MergeLevelManager.Instance.IsBranch(mergeLevel)) 
            {
                item.RefreshBranch(this, BranchSystem.Instance.CurBranchDef.BranchType);
                if (saveBranchDic.ContainsKey(MergeLevelType.branch1)) 
                {
                    saveBranchDic.Remove(MergeLevelType.branch1);
                }
                if (saveBranchDic.ContainsKey(MergeLevelType.branch_christmas))
                {
                    saveBranchDic.Remove(MergeLevelType.branch_christmas);
                }
                if (saveBranchDic.ContainsKey(MergeLevelType.branch_SpurLine4))
                {
                    saveBranchDic.Remove(MergeLevelType.branch_SpurLine4);
                }
                if (saveBranchDic.ContainsKey(MergeLevelType.branch_SpurLine5))
                {
                    saveBranchDic.Remove(MergeLevelType.branch_SpurLine5);
                }
                if (saveBranchDic.ContainsKey(MergeLevelType.branch_SpurLine6))
                {
                    saveBranchDic.Remove(MergeLevelType.branch_SpurLine6);
                }
                if (saveBranchDic.ContainsKey(MergeLevelType.branch_halloween))
                {
                    saveBranchDic.Remove(MergeLevelType.branch_halloween);
                }
                saveBranchDic[BranchSystem.Instance.CurBranchDef.BranchType] = item;
            }
            else
                go.gameObject.SetActive(false);
        } 
        else
            go.gameObject.SetActive(false);
        //节日活动
        MergeLevelType[] festivalArray = { MergeLevelType.halloween, MergeLevelType.christmas, MergeLevelType.lover };
        for (int i = 0; i < festivalArray.Length; i++)
        {
            if (FestivalSystem.Instance.IsFestivalOpen(festivalArray[i]))
            {
                hasFestival = true;
                GameObject go2 = AssetSystem.Instance.Instantiate(Consts.BranchItem, layout.transform);
                if (go2 && go2.TryGetComponent(out BranchItem item2))
                {
                    item2.RefreshBranch(this, festivalArray[i]);
                    item2.transform.SetAsFirstSibling();
                    saveBranchDic[festivalArray[i]] = item2;
                }
                break;//只允许显示一个节日活动
            }
            else
            {
                hasFestival = false;
            }
        }
    }
    private void RefreshTotalBranch()
    {

        foreach (var item in saveBranchDic)
        {
            item.Value.RefreshBranch(this, item.Key);
        }
    }
    #endregion

    #region 副本
    private IEnumerator RefreshDungeonUIByComplete() 
    {       
        dungeonItem.gameObject.SetActive(false);
        Dictionary<MergeLevelType, DungeonState> dungeonStateDic = DungeonSystem.Instance.GetCurrentDungeonState();
        yield return new WaitForSeconds(1f);
        float dungeonItemHeight = saveBranchDic.Count * 500 + (saveBranchDic.Count - 1) * layout.spacing;
        float maxHeight = saveBranchDic.Count * 500 + dungeonItemList.Count * 494 + (saveBranchDic.Count + dungeonItemList.Count - 1) * layout.spacing;
        int dungeonIndex = 0;
        foreach (var item in dungeonStateDic)
        {
            dungeonItemHeight += 494;          
            if (DungeonSystem.Instance.completeType == item.Key)
                break;
            else
                dungeonItemHeight += layout.spacing;
            dungeonIndex++;
        }
        float screenHeight = dungeonScroll.GetComponent<RectTransform>().rect.height;
        float viewHeight = screenHeight - layout.padding.top - layout.padding.bottom;
        if (dungeonItemHeight <= screenHeight / 2)
        {
            dungeonItemHeight = 0;
        }
        else if (dungeonItemHeight >= maxHeight - viewHeight / 2) 
        {
            dungeonItemHeight = maxHeight - viewHeight;
        }
        else 
        {
            dungeonItemHeight = dungeonItemHeight - screenHeight / 2;
        }
        layout.transform.localPosition = new Vector3(layout.transform.localPosition.x, dungeonItemHeight, layout.transform.localPosition.z);
        yield return null;
        if (dungeonIndex < dungeonItemList.Count) 
        {
            dungeonItemList[dungeonIndex].PlayEffect();
            yield return new WaitForSeconds(1.5f);
            if (DungeonDefinition.DungeonDefDic.TryGetValue(DungeonSystem.Instance.completeType, out DungeonDefinition dungeonDefinition))
            {
                dungeonItemList[dungeonIndex].SetData(dungeonDefinition, dungeonStateDic[DungeonSystem.Instance.completeType]);
            }
        }     
    }
    private void RefreshDungeonUI() 
    {
        dungeonItem.gameObject.SetActive(false);
        Dictionary<MergeLevelType, DungeonState> dungeonStateDic = DungeonSystem.Instance.GetCurrentDungeonState();
        int index = 0;
        //解锁  锁住  已完成的副本
        foreach (var v in dungeonStateDic)
        {
            if (DungeonDefinition.DungeonDefDic.TryGetValue(v.Key, out DungeonDefinition dungeonDefinition))
            {
                if(index > dungeonItemList.Count-1) 
                {
                    var item = Instantiate(dungeonItem, layout.transform).GetComponent<DungeonItem>();
                    item.gameObject.SetActive(true);
                    item.SetData(dungeonDefinition, v.Value);
                    dungeonItemList.Add(item);
                }
                else 
                {
                    dungeonItemList[index].SetData(dungeonDefinition, v.Value);
                }
                index++;
            }
        }
    }
    private void InitDungeonUI()
    {
        dungeonItem.gameObject.SetActive(false);
        Dictionary<MergeLevelType, DungeonState> dungeonStateDic = DungeonSystem.Instance.GetCurrentDungeonState();
        //解锁  锁住  已完成的副本
        foreach (var v in dungeonStateDic)
        {
            if (DungeonDefinition.DungeonDefDic.TryGetValue(v.Key, out DungeonDefinition dungeonDefinition))
            {
                var item = Instantiate(dungeonItem, layout.transform).GetComponent<DungeonItem>();
                item.gameObject.SetActive(true);
                item.SetData(dungeonDefinition, v.Value);
                dungeonItemList.Add(item);
            }
        }
    }
    #endregion
}
