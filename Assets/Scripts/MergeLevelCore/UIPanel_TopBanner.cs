using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Ivy;
using Ivy.Mail;

public class UIPanel_TopBanner : MonoBehaviour
{
    [SerializeField] private Transform parent;

    public static Action refreshBanner;
    UI_BannerItem[] itemArray;//所有banner
    List<UI_BannerItem> curBannerList = new List<UI_BannerItem>();//当前需要展示的banner列表
    private List<CanvasGroup> canvasGroups = new List<CanvasGroup>();
    private int curIndex = 0;
    const float carouselTime = 15;
    float countDownTime = carouselTime;
    private void Awake()
    {
        InitItem();
    }

    private void InitItem()
    {
        itemArray = parent.GetComponentsInChildren<UI_BannerItem>();
        for (int i = 0; i < itemArray.Length; i++)
        {
            itemArray[i].Init();
            canvasGroups.Add(itemArray[i].GetComponent<CanvasGroup>());
            itemArray[i].gameObject.SetActive(false);
        }
        refreshBanner += () =>
        {
            RefreshCurBannerList();
        };
    }
    private void Update()
    {
        countDownTime -= Time.deltaTime;
        if (countDownTime < 0)
        {
            ChangeIndex();
            itemArray[curIndex].Refresh();
            ShowBanner();
            if (curIndex == 4)
            {
                if (RemoteConfigSystem.Instance.GetRemoteConfig_Int("strategy_banner_level") != 0)//有远程配置时间
                {
                    int remoteTime = RemoteConfigSystem.Instance.GetRemoteConfig_Int("strategy_banner");
                    if (remoteTime > 0)
                    {
                        countDownTime += remoteTime;
                    }
                    else
                    {
                        countDownTime += 30;
                    }
                }
                else
                {
                    countDownTime += 30;
                }
            }
            else
            {
                countDownTime += carouselTime;
            }
        }
    }
    int recursionCount = 0;//递归次数
    private void ChangeIndex()
    {
        recursionCount++;
        if (recursionCount > itemArray.Length)
        {
            GameDebug.LogError("强制跳出递归");
            recursionCount = 0;
            return;
        }

        curIndex++;
        if (curIndex >= itemArray.Length)
        {
            curIndex = 0;
        }
        if (!curBannerList.Contains(itemArray[curIndex]))
        {
            ChangeIndex();
        }
        recursionCount = 0;

    }
    private void ShowBanner()
    {
        for (int i = 0; i < canvasGroups.Count; i++)
        {
            if (canvasGroups[i].gameObject.activeSelf)
            {
                if (i == 4)
                {
                    RiseSdk.Instance.CloseBanner();
                }
                canvasGroups[i].gameObject.SetActive(false);
            }
        }
        canvasGroups[curIndex].gameObject.SetActive(true);
        canvasGroups[curIndex].alpha = 0;
        DOTween.To(() => canvasGroups[curIndex].alpha, (alpha) =>
        {
            canvasGroups[curIndex].alpha = alpha;
        }, 1, 1f);
        if (curIndex == 4)
        {
            RiseSdk.Instance.ShowBanner(3);
        }
    }

    bool isInit = true;
    /// <summary>
    /// 刷新当前需要显示的banner列表
    /// </summary>
    private void RefreshCurBannerList()
    {
        curBannerList.Clear();
        for (int i = 0; i < itemArray.Length; i++)
        {
            switch (i)
            {
                case 0:
                    if (MailSystem.Instance.GetShowEmails().Count > 0)
                        curBannerList.Add(itemArray[i]);
                    break;
                case 1:
                    if (UIPanel_Spin.SpinWheelFree())
                        curBannerList.Add(itemArray[i]);
                    break;
                case 2:
                    if (GiftPackageManager.Instance.GiftIndexList.Contains(1001))
                        curBannerList.Add(itemArray[i]);
                    break;
                case 3:
                    if (TaskGoalsManager.Instance.HasTaskCanClaim())
                        curBannerList.Add(itemArray[i]);
                    break;
                case 4:
                    if (RemoteConfigSystem.Instance.GetRemoteConfig_Int("strategy_banner_level") != 0)
                    {
                        curBannerList.Add(itemArray[i]);
                    }
                    break;
                case 5:
                    curBannerList.Add(itemArray[i]);
                    break;
                case 6:
                    curBannerList.Add(itemArray[i]);
                    break;
                case 7:
                    if (GameManager.Instance.playerData.UnCollectedExp >= GameManager.Instance.playerData.NextExpLevelNeedExp)
                        curBannerList.Add(itemArray[i]);
                    break;
                case 8:
                    if (MergeChapterRewardDefinition.chapterRewards.TryGetValue(SaveUtils.GetInt(Consts.ChapterRewardLastGotId) + 1, out MergeChapterRewardDefinition mergeChapterRewardDefinition))
                    {
                        if (TaskGoalsManager.Instance.curLevelIndex > mergeChapterRewardDefinition.Chapter)
                        {
                            curBannerList.Add(itemArray[i]);
                        }
                    }
                    break;
                case 9:
                    if (MergeStarRewardDefinition.starRewards.TryGetValue(SaveUtils.GetInt(Consts.StarRewardLastGotID) + 1, out MergeStarRewardDefinition mergeStarRewardDefinition))
                    {
                        if (TaskGoalsManager.Instance.YellowStarNum >= mergeStarRewardDefinition.StarCount)
                        {
                            curBannerList.Add(itemArray[i]);
                        }
                    }
                    break;
                case 10:
                    if (ShopSystem.Instance.CoinsRandomBagList.Count > 0)
                    {
                        curBannerList.Add(itemArray[i]);
                    }
                    break;
                case 11:
                    if (ShopSystem.Instance.CoinsRandomBagList.Count > 1)
                    {
                        curBannerList.Add(itemArray[i]);
                    }
                    break;
                case 12:
                    if (ShopSystem.Instance.CoinsRandomBagList.Count > 2)
                    {
                        curBannerList.Add(itemArray[i]);
                    }
                    break;
                default:
                    break;
            }
        }

        //GameDebug.LogError(curBannerList.Count);
        if (isInit)
        {
            ChangeIndex();
            itemArray[curIndex].Refresh();//初始化刷新第一个数据
            ShowBanner();
            isInit = false;
        }
    }
}
