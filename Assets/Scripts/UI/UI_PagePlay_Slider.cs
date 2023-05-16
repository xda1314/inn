using DG.Tweening;
using ivy.game;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 主界面 进度条展示
/// </summary>
public class UI_PagePlay_Slider : UIPanelBase, IPointerDownHandler, IPointerUpHandler
{
    #region 组件
    [SerializeField] private Slider slider_;
    [SerializeField] private Button btn_getReward;
    [SerializeField] private Image img_bg;
    [SerializeField] private TextMeshProUGUI text_curCount;
    [SerializeField] private TextMeshProUGUI text_goalCount;
    [SerializeField] private TextMeshProUGUI text_countDown;
    [SerializeField] private TextMeshProUGUI text_completeCountDown;
    [SerializeField] private MergeLevelType mergeLevel;
    [SerializeField] private Sprite normal_sprite;
    [SerializeField] private Sprite complete_sprite;
    [SerializeField] private Transform fly_trans;
    [SerializeField] private Transform img_gift;
    [SerializeField] private RectTransform page_play;
    [SerializeField] private Image img_start;
    [SerializeField] private Sprite sprite_start;
    [SerializeField] private Sprite sprite_exp;
    #endregion

    #region 变量
    public bool is_init;
    private DateTimeOffset endTime;
    private float last_point = 0f;
    private float currentPoint = 0f;
    private float goalPoint = 0f;
    public static Action refreshAction;
    private Vector3 saveScale = Vector3.one;
    #endregion

    private void Start()
    {
        RectTransform rect = transform.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(page_play.sizeDelta.x-80, rect.sizeDelta.y);
        saveScale = transform.localScale;
        gameObject.SetActive(false);
        refreshAction = () => { CheckRefreshUI(); };
        CheckRefreshUI();
    }

    public bool CheckRefreshUI() 
    {
        if (BranchSystem.Instance.GetIsOpen())
        {
            mergeLevel = BranchSystem.Instance.CurBranchDef.BranchType;
            BranchRewardDefinition branchReward=new BranchRewardDefinition();
            endTime = BranchSystem.Instance.Branch_EndTime;
            branchReward = BranchSystem.Instance.GetCurrentBranchDef();
            currentPoint = BranchSystem.Instance.branchPoint;
            if(mergeLevel == MergeLevelType.branch_SpurLine4 
                || mergeLevel == MergeLevelType.branch_SpurLine5
                || mergeLevel == MergeLevelType.branch_SpurLine6)
                img_start.sprite = sprite_exp;
            else
                img_start.sprite = sprite_start;
            if (branchReward!=null)
                goalPoint = branchReward.goalPoint;
            else 
            {
                HideUIAni();
                return false;
            }   
        }
        else
        {
            HideUIAni();
            return false;
        }
        if (is_init && last_point < currentPoint)
        {
            PlayTweenPointsFly((int)(currentPoint - last_point), fly_trans.position);
        }
        else
        {
            RefreshUI(currentPoint, goalPoint);
        }
        last_point = currentPoint;
        is_init = true;
        Page_Play.checkIsHasSlider?.Invoke(true);
        return true;
    }

    private void HideUIAni() 
    {
        transform.DOScale(Vector3.zero, 0.3f).SetDelay(0.5f).OnComplete(()=> 
        {
            Page_Play.checkIsHasSlider?.Invoke(false);
            gameObject.SetActive(false);
        });
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        btn_getReward.transform.DOKill();
        btn_getReward.transform.DOScale(saveScale * 0.9f, 0.06f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       
        btn_getReward.transform.DOKill();
        btn_getReward.transform.DOScale(saveScale, 0.06f).OnComplete(() => GetReward());
    }

    private void ShowUIAni()
    {
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.3f).SetDelay(0.5f);
    }



    private void DoTipAni() 
    {
        img_gift.transform.localScale = saveScale;
        img_gift.transform.DOKill();
        img_gift.transform.DOScale(1.2f, 1f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    private void KillAni() 
    {
        img_gift.transform.DOKill();
        img_gift.transform.DOScale(1.0f, 1f).SetEase(Ease.InOutQuad);
    }


    public void RefreshUI(float current_point, float goal_point) 
    {
        ShowUIAni();
        text_goalCount.text = goal_point.ToString();
        slider_.value = current_point / goal_point;
        text_curCount.text = current_point.ToString();
        if (current_point >= goal_point)
        {
            img_bg.sprite = complete_sprite;
            DoTipAni();
        }
        else
        {
            img_bg.sprite = normal_sprite;
            KillAni();
        }
        text_completeCountDown.transform.parent.gameObject.SetActive(current_point >= goal_point);
        text_countDown.transform.parent.gameObject.SetActive(!(current_point >= goal_point));

    }



    //领取奖励
    public virtual void GetReward() 
    {
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.Button);
        BranchRewardDefinition branchReward = null;
        branchReward = BranchSystem.Instance.GetCurrentBranchDef();
        if (branchReward != null && BranchSystem.Instance.GetIsOpen() 
            && MergeLevelManager.Instance.IsBranch(mergeLevel) )
            UISystem.Instance.ShowUI(new UIPanelData_BranchRedeem(mergeLevel));
    }

    private void Update()
    {
        if (!is_init)
            return;
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
        var leftSeconds = (int)(endTime - TimeManager.Instance.UtcNow()).TotalSeconds;
        if (leftSeconds > 0)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
            text_countDown.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(leftSeconds);
            text_completeCountDown.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(leftSeconds);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private int GetSplitNum(int num)
    {
        int showCount;
        if (num >= 40)
        {
            showCount = 20;
        }
        else if (num >= 20)
        {
            showCount = 10;
        }
        else if (num >= 10)
        {
            showCount = 5;
        }
        else 
        {
            showCount = 2;
        }
        return showCount;
    }


    private float flyPointCount = 0;
    /// <summary>
    /// 订单点击完成时播放积分动画
    /// </summary>
    public void PlayTweenPointsFly(int pointCount, Vector3 tweenFromWorldPos)
    {
        flyPointCount = pointCount;
        int num = GetSplitNum(pointCount);
        Vector3 localPosition = slider_.handleRect.transform.InverseTransformPoint(tweenFromWorldPos);
        float delay = 0f;
        for (int i = 0; i < num; i++)
        {
            GameObject fly = AssetSystem.Instance.Instantiate(Consts.Icon_Reward_Points, slider_.handleRect.transform, localPosition, Vector3.zero, Vector3.zero);
            int reduce = 0;
            if (i == num - 1) 
            {
                reduce = pointCount - (int)(flyPointCount / num) * i;
            }
            else 
            {
                reduce = (int)(flyPointCount / num);
            }
            
            var tempDelay = delay;
            var scalePos = localPosition + new Vector3(UnityEngine.Random.Range(-60, 60), UnityEngine.Random.Range(-60, 60), 0);
            delay += UnityEngine.Random.Range(1, 3) * 0.1f;
            DOTween.Sequence().AppendInterval(tempDelay)
                .Append(fly.transform.DOScale(Vector3.one * 0.7f, 0.3f).SetEase(Ease.OutBack))
                .Insert(tempDelay, fly.transform.DOLocalMove(scalePos, 0.3f))
                .Append(fly.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InOutSine))
                .AppendCallback(() =>
                {
                    flyPointCount -= reduce;
                    RefreshSlider();
                    RefreshUI(currentPoint - flyPointCount, goalPoint);
                    AssetSystem.Instance.DestoryGameObject("", fly);
                });
        }
    }

    private void RefreshSlider()
    {
        float val = 0.0f;
        if (last_point >= goalPoint)
        {
            slider_.value = 1.0f;
        }else
        {
            float branchPoint = 0.0f;
            branchPoint = currentPoint - flyPointCount;
            val = branchPoint / goalPoint;
            DOTween.To(() => slider_.value, x => slider_.value = x, val, 0.1f);
        }
    }
}
