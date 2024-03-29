using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using IvyCore;

public class UIPanel_LevelRewards : UIPanelBase
{
    [Header("NotAchieveObj")]
    [SerializeField] private TextMeshProUGUI textTitle;
    [SerializeField] private TextMeshProUGUI textNotAchieveTips;
    [SerializeField] private TextMeshProUGUI textOpenBoxTips;
    [SerializeField] private TextMeshProUGUI textContinue;
    [SerializeField] private TextMeshProUGUI textOpen;
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnOpen;

    [Header("AchieveObj")]
    [SerializeField] private GameObject achieveObj;
    [SerializeField] private CanvasGroup achieveBgMask;
    [SerializeField] private TextMeshProUGUI textCongratulate;
    [SerializeField] private TextMeshProUGUI textDouble;
    [SerializeField] private TextMeshProUGUI textDouble_N;
    [SerializeField] private TextMeshProUGUI textNoDouble;
    [SerializeField] private Transform rotateLightTrans;
    [SerializeField] private Transform boxOpenedTrans;
    [SerializeField] private Transform boxClosedTrans;
    [SerializeField] private List<Transform> gitfItemTransList;
    [SerializeField] private Button btnDouble;
    [SerializeField] private Button btnDouble_N;
    [SerializeField] private Button btnNoDouble;

    private List<Item_GiftBag> giftItemList = new List<Item_GiftBag>();
    private MergeChapterRewardDefinition mChapterRewardDef = null;

    public override void OnInitUI()
    {
        base.OnInitUI();
        btnClose.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
        btnContinue.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
        btnOpen.onClick.AddListener(() =>
        {
            StartCoroutine(ShowAchieveUI());
        });
        btnDouble.onClick.AddListener(() =>
        {
            AdManager.PlayAd(Vector3.zero, AdManager.ADTag.DoubleLevelReward, () =>
            {
                GetDoubleRewards();
            }, "", () =>
            {
                //GameManager.Instance.ResetRewardList(mChapterRewardDef.rewardItemList);
                //GameManager.Instance.PlayAdFail();
            });
        });
        btnNoDouble.onClick.AddListener(() =>
        {
            UISystem.Instance.HideUI(this);
        });
    }
    public override IEnumerator OnShowUI()
    {
        yield return base.OnShowUI();
        CheckChapterReward();
    }

    public override IEnumerator OnHideUI()
    {
        yield return base.OnHideUI();
        achieveObj.SetActive(false);
        UISystem.Instance.uiMainMenu.TryStartOpenDungeonTutor();
    }

    private void CheckChapterReward()
    {
        bool achieved = false;
        var lastGotRewardIndex = SaveUtils.GetInt(Consts.ChapterRewardLastGotId);
        var curId = lastGotRewardIndex + 1;
        if (MergeChapterRewardDefinition.chapterRewards.TryGetValue(curId, out mChapterRewardDef))
        {
            if (TaskGoalsManager.Instance.curLevelIndex > mChapterRewardDef.Chapter)
            {
                achieved = true;
            }
            else
            {
                achieved = false;
            }
        }
        else
        {
            achieved = false;
        }
        Refresh(achieved);
    }

    private void Refresh(bool isActive)
    {
        achieveObj.SetActive(false);
        btnOpen.gameObject.SetActive(isActive);
        btnContinue.gameObject.SetActive(!isActive);
        textNotAchieveTips.gameObject.SetActive(!isActive);
        textOpenBoxTips.gameObject.SetActive(isActive);
        if (isActive)
        {
            textTitle.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/LevelChest_Title");
            textOpenBoxTips.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/StarChestDescribe4");
            textOpen.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Home_Button2");
            textCongratulate.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/StarChestDescribe3");
            textNoDouble.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/LevelChestButton");
            for (int i = 0; i < giftItemList.Count; i++)
            {
                if (giftItemList[i].gameObject.activeSelf)
                    giftItemList[i].gameObject.SetActive(false);
            }
            if (SaveUtils.GetBool(Consts.SaveKey_Tutorial_InnLevelChest2))
            {
                GameDebug.Log("执行InnLevelChest2教学");
                UI_TutorManager.Instance.RunTutorWithName("InnLevelChest2");
                SaveUtils.SetBool(Consts.SaveKey_Tutorial_InnLevelChest2, false);
            }
        }
        else
        {
            textTitle.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/LevelChest_Title");
            textContinue.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Home_Button1");
            if (mChapterRewardDef != null)
            {
                textNotAchieveTips.text = string.Format(I2.Loc.ScriptLocalization.Get("Obj/Chain/StarChestDescribe1"), mChapterRewardDef.Chapter + 1);
            }
            else 
            {
                textNotAchieveTips.gameObject.SetActive(false);
            }
        }
    }

    private void GetDoubleRewards()
    {
        achieveObj.transform.localScale = Vector3.zero;
        foreach (var item in giftItemList)
        {
            item.gameObject.SetActive(false);
        }
        StartCoroutine(ShowAchieveUI(true));
    }

    //展示奖励动画
    private IEnumerator ShowAchieveUI(bool getDouble = false)
    {
        achieveObj.SetActive(true);
        if (getDouble == false)
        {
            var lastGotRewardIndex = SaveUtils.GetInt(Consts.ChapterRewardLastGotId);
            var curId = lastGotRewardIndex + 1;
            SaveUtils.SetInt(Consts.ChapterRewardLastGotId, curId);
            IvyCore.UI_Manager.Instance.InvokeRefreshEvent("", "page_Play_RefreshChapterChest");
        }
        rotateLightTrans.localScale = Vector3.zero;
        btnDouble.gameObject.SetActive(false);
        btnNoDouble.gameObject.SetActive(false);
        achieveBgMask.alpha = 0;
        boxOpenedTrans.gameObject.SetActive(false);
        boxClosedTrans.gameObject.SetActive(true);
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.chest_open);

        achieveObj.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.3f);
        achieveBgMask.DOFade(1, 0.6f);
        DOTween.Sequence().Append(boxClosedTrans.DOLocalRotate(Vector3.forward * -10, 0.1f).SetEase(Ease.OutSine))
            .Append(boxClosedTrans.DOLocalRotate(Vector3.forward * 10, 0.1f).SetEase(Ease.InOutSine))
            .Append(boxClosedTrans.DOLocalRotate(Vector3.forward * -10, 0.1f).SetEase(Ease.InOutSine))
            .Append(boxClosedTrans.DOLocalRotate(Vector3.forward * 10, 0.1f).SetEase(Ease.InOutSine))
            .Append(boxClosedTrans.DOLocalRotate(Vector3.forward * -10, 0.1f).SetEase(Ease.InOutSine))
            .Append(boxClosedTrans.DOLocalRotate(Vector3.zero, 0.1f).SetEase(Ease.InOutSine));
        yield return new WaitForSeconds(0.6f);
        rotateLightTrans.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutSine);
        boxOpenedTrans.gameObject.SetActive(true);
        boxClosedTrans.gameObject.SetActive(false);

        AnalyticsUtil.TrackEvent("claim_level_chest", new Dictionary<string, string>()
        {
            {"label", mChapterRewardDef.Id.ToString()}
        });
        //刷新奖励信息
        if (mChapterRewardDef.rewardItemList != null)
        {
            GameManager.Instance.GiveRewardItem(mChapterRewardDef.rewardItemList, "OpenLevelRewardBox", Vector3.zero);
            int index = 0;
            foreach (var item in mChapterRewardDef.rewardItemList)
            {
                Item_GiftBag gift = null;
                if (index < giftItemList.Count)
                {
                    gift = giftItemList[index];
                }
                else
                {
                    var go = AssetSystem.Instance.Instantiate(Consts.Item_GiftBag, gitfItemTransList[index]);
                    if (go != null && go.TryGetComponent<Item_GiftBag>(out var reward))
                    {
                        gift = reward;
                        giftItemList.Add(reward);
                    }
                }
                if (gift != null)
                {
                    gift.SetData(item);
                    gift.gameObject.SetActive(true);
                    gift.transform.localScale = Vector3.zero;
                    gift.transform.position = boxOpenedTrans.position;
                    gift.transform.DOLocalMove(Vector3.zero, 0.3f).SetEase(Ease.OutSine);
                    AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.chest_reward_show);
                    gift.transform.DOScale(Vector3.one, 0.3f);
                    yield return new WaitForSeconds(0.15f);
                }
                index++;
            }
        }

        if (getDouble)
        {
            yield return new WaitForSeconds(1.5f);
            UISystem.Instance.HideUI(this);
        }
        else
        {
            if (!AdManager.CanShowAD_Normal())
            {
                btnDouble.gameObject.SetActive(false);
                btnDouble_N.gameObject.SetActive(true);
                textDouble_N.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/WatchVideo_Text5");
            }
            else
            {
                btnDouble.gameObject.SetActive(true);
                btnDouble_N.gameObject.SetActive(false);
                textDouble.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Home_Button3");
            }
            btnNoDouble.gameObject.SetActive(true);

        }
    }
}
