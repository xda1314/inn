using Coffee.UIExtensions;
using DG.Tweening;
using IvyCore;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BranchItem : MonoBehaviour
{
    [SerializeField] private UIEffect blurEffect;
    [SerializeField] private Image img_outline;
    [SerializeField] private Image img_bg;
    [SerializeField] private Image img_Title;
    [SerializeField] private Material material_Grey;

    [SerializeField] private TextMeshProUGUI text_title;
    [SerializeField] private TextMeshProUGUI text_des;
    [SerializeField] private TextMeshProUGUI btn_entText;
    [SerializeField] private TextMeshProUGUI btn_noEntText;
    [SerializeField] private Button btn_enter;
    [SerializeField] private GameObject btn_noEnter;
    [SerializeField] private TextMeshProUGUI text_countDown;

    private bool isOpen = false;
    private bool isActive = true;
    private MergeLevelType levelType;
    private Page_Dungeon page_Dungeon = null;
    private void Start()
    {
        btn_enter.onClick.AddListener(() =>
        {
            VibrateSystem.Haptic(MoreMountains.NiceVibrations.HapticTypes.Selection);
            if (MergeLevelManager.Instance.IsBranch(levelType))
            {
                if (BranchSystem.Instance.ClaimedAllTask())
                {
                    transform.DOLocalMoveX(-400,0.4f).SetEase(Ease.OutCubic).OnComplete(()=> 
                    {
                        SaveUtils.SetBool("Is_Show_Branch", false);
                        gameObject.SetActive(false);
                    });
                }
                else
                    BranchSystem.Instance.EnterGame();
            }
            else
            {
                if (page_Dungeon == null)
                    return;
                if (FestivalSystem.Instance.IsFestivalOpen(levelType))
                    MergeLevelManager.Instance.ShowMergePanelByDungeonType(levelType);
            }
        });
        UI_Manager.Instance.RegisterRefreshEvent(gameObject, (str) =>
        {
            RefreshLanguageText();
        }, "RefreshEvent_LanguageChanged");
        BranchSystem.Instance.refreshByReset += () => { SetTimeOffset(); };
    }

    public void RefreshBranch(Page_Dungeon panel, MergeLevelType type)
    {
        page_Dungeon = panel;
        levelType = type;
        if (MergeLevelManager.Instance.IsBranch(levelType))
        {
            isOpen = BranchSystem.Instance.GetIsOpen();
            if (levelType == MergeLevelType.branch_halloween)
            {
                AssetSystem.Instance.LoadAssetAsync<Sprite>("RemoteAtlas_Halloween[Dungeons_Activity_Bg1]", (sprite) =>
                {
                    img_bg.sprite = sprite;
                });
                AssetSystem.Instance.LoadAssetAsync<Sprite>("RemoteAtlas_Halloween[FloorTitle3]", (sprite) =>
                {
                    img_Title.sprite = sprite;
                });
            }
            else if (levelType == MergeLevelType.branch_christmas)
            {
                AssetSystem.Instance.LoadAssetAsync<Sprite>("RemoteAtlas_Christmas[Christmas_Bg2]", (sprite) =>
                {
                    img_bg.sprite = sprite;
                });
                AssetSystem.Instance.LoadAssetAsync<Sprite>("RemoteAtlas_Christmas[FloorTitle4]", (sprite) =>
                {
                    img_Title.sprite = sprite;
                });
            }
            else if (levelType == MergeLevelType.branch_SpurLine4)
            {
                AssetSystem.Instance.LoadAssetAsync<Sprite>("UISpriteAtlas_Merge[BGMergeSpurLine4]", (sprite) =>
                {
                    img_bg.sprite = sprite;
                });
                AssetSystem.Instance.LoadAssetAsync<Sprite>("UISpriteAtlas_Merge[FloorTitle]", (sprite) =>
                {
                    img_Title.sprite = sprite;
                });
            }
            else if (levelType == MergeLevelType.branch_SpurLine5)
            {
                AssetSystem.Instance.LoadAssetAsync<Sprite>("UISpriteAtlas_Merge[BGMergeSpurLine5]", (sprite) =>
                {
                    img_bg.sprite = sprite;
                });
                AssetSystem.Instance.LoadAssetAsync<Sprite>("UISpriteAtlas_Merge[FloorTitle]", (sprite) =>
                {
                    img_Title.sprite = sprite;
                });
            }
            else if (levelType == MergeLevelType.branch_SpurLine6)
            {
                AssetSystem.Instance.LoadAssetAsync<Sprite>("UISpriteAtlas_Merge[BGMergeSpurLine6]", (sprite) =>
                {
                    img_bg.sprite = sprite;
                });
                AssetSystem.Instance.LoadAssetAsync<Sprite>("UISpriteAtlas_Merge[FloorTitle]", (sprite) =>
                {
                    img_Title.sprite = sprite;
                });
            }
            else
            {
                AssetSystem.Instance.LoadAssetAsync<Sprite>("UISpriteAtlas_Merge[Dungeons_Activity_Bg]", (sprite) =>
                {
                    img_bg.sprite = sprite;
                });
                AssetSystem.Instance.LoadAssetAsync<Sprite>("UISpriteAtlas_Merge[FloorTitle]", (sprite) =>
                {
                    img_Title.sprite = sprite;
                });
            }
            
            if (SaveUtils.GetBool("Is_Show_Branch",false)) 
            {
                
            }
            else
            {
                //支线活动开启并且未领完所有任务积分奖励
                gameObject.SetActive(!BranchSystem.Instance.ClaimedAllTask()&& 5 <= TaskGoalsManager.Instance.curLevelIndex
                    && BranchSystem.Instance.IsActive());
            }
        }
        else if (MergeLevelManager.Instance.IsFestivalBranch(levelType))
        {
            isOpen = FestivalSystem.Instance.IsFestivalOpen(levelType);
            isActive = FestivalSystem.Instance.IsActive(levelType);
            if (levelType == MergeLevelType.halloween)
            {
                AssetSystem.Instance.LoadAssetAsync<Sprite>("RemoteAtlas_Halloween[Dungeons_Activity_Bg1]", (sprite) =>
                {
                    img_bg.sprite = sprite;
                });
                AssetSystem.Instance.LoadAssetAsync<Sprite>("RemoteAtlas_Halloween[FloorTitle3]", (sprite) =>
                {
                    img_Title.sprite = sprite;
                });
            }
            else if (levelType == MergeLevelType.christmas)
            {
                AssetSystem.Instance.LoadAssetAsync<Sprite>("RemoteAtlas_Christmas[Christmas_Bg2]", (sprite) =>
                {
                    img_bg.sprite = sprite;
                });
                AssetSystem.Instance.LoadAssetAsync<Sprite>("RemoteAtlas_Christmas[FloorTitle4]", (sprite) =>
                {
                    img_Title.sprite = sprite;
                });
            }
            else if (levelType == MergeLevelType.lover)
            {
                AssetSystem.Instance.LoadAssetAsync<Sprite>("RemoteAtlas_Lover[Dungeons_Activity_Bg3]", (sprite) =>
                {
                    img_bg.sprite = sprite;
                });
                AssetSystem.Instance.LoadAssetAsync<Sprite>("RemoteAtlas_Lover[FloorTitle5]", (sprite) =>
                {
                    img_Title.sprite = sprite;
                });
            }
        }
        RefreshLanguageText();
        SetTimeOffset();
        RefreshState();
    }

    private void RefreshLanguageText()
    {
        btn_noEntText.text = I2.Loc.ScriptLocalization.Get("Obj/Dungeon/ButtonText2");
        if (MergeLevelManager.Instance.IsBranch(levelType))
        {
            if (!isOpen)
            {
                int branchOpenLevel = BranchSystem.Instance.Remote_UnlockLevel;
                btn_noEntText.text = string.Format(I2.Loc.ScriptLocalization.Get("Obj/Chain/EctypeDescribe2"), branchOpenLevel);
            }

            if (levelType == MergeLevelType.branch_halloween)
            {
                text_title.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine2Title");
                text_des.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine2Describe");
            }
            else if (levelType == MergeLevelType.branch_christmas)
            {
                text_title.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine3Title");
                text_des.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine3Describe");
            }
            else if (levelType == MergeLevelType.branch_SpurLine4)
            {
                text_title.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine4Title");
                text_des.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine4Describe");
            }
            else if (levelType == MergeLevelType.branch_SpurLine5)
            {
                text_title.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine5Title");
                text_des.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine5Describe");
            }
            else if (levelType == MergeLevelType.branch_SpurLine6)
            {
                text_title.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine6Title");
                text_des.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine6Describe");
            }
            else 
            {
                text_title.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine1Title");
                text_des.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/SpurLine1Describe");
            }
            btn_entText.text = I2.Loc.ScriptLocalization.Get("Obj/Dungeon/ButtonText2");
            if (BranchSystem.Instance.ClaimedAllTask()) 
                btn_entText.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/EctypeDescribe11");
        }
        else if (MergeLevelManager.Instance.IsFestivalBranch(levelType))
        {
            if (!isActive)
                btn_noEntText.text = string.Format(I2.Loc.ScriptLocalization.Get("Obj/Chain/EctypeDescribe2"), 5);
            btn_entText.text = FestivalSystem.Instance.HasClaimAllReward(levelType) ? I2.Loc.ScriptLocalization.Get("Obj/Festival/Festival1Btn") : I2.Loc.ScriptLocalization.Get("Obj/Dungeon/ButtonText2");
            if (levelType == MergeLevelType.halloween)
            {
                text_title.text = I2.Loc.ScriptLocalization.Get("Obj/Festival/Festival1Title");
                text_des.text = I2.Loc.ScriptLocalization.Get("Obj/Festival/Festival1Describe");
            }
            else if (levelType == MergeLevelType.christmas)
            {
                text_title.text = I2.Loc.ScriptLocalization.Get("Obj/Festival/Festival2Title");
                text_des.text = I2.Loc.ScriptLocalization.Get("Obj/Festival/Festival2Describe");
            }
            else if (levelType == MergeLevelType.lover)
            {
                text_title.text = I2.Loc.ScriptLocalization.Get("Obj/Festival/Festival3Title");
                text_des.text = I2.Loc.ScriptLocalization.Get("Obj/Festival/Festival3Describe");
            }
        }
    }

    private void RefreshState()
    {
        //img_bg.material = isOpen ? null : material_Grey;
        blurEffect.enabled = !isOpen;
        img_outline.gameObject.SetActive(!isOpen);
        text_countDown.transform.parent.gameObject.SetActive(isOpen);
        btn_noEnter.gameObject.SetActive(!isOpen || !isActive);
        btn_enter.gameObject.SetActive(isOpen && isActive);
    }

    private int leftTime = -1;
    private void SetTimeOffset()
    {
        if (MergeLevelManager.Instance.IsBranch(levelType))
        {
            leftTime = (int)(BranchSystem.Instance.curStartTime.AddDays(BranchSystem.Instance.NewLoopTime) - TimeManager.Instance.UtcNow()).TotalSeconds;
            text_countDown.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(leftTime);
        }
        else
        {
            DateTimeOffset endTimeOffset = FestivalSystem.Instance.GetFestivalEndTime(levelType);
            if (TimeManager.Instance.UtcNow() < endTimeOffset)
            {
                leftTime = (int)(endTimeOffset - TimeManager.Instance.UtcNow()).TotalSeconds;
                text_countDown.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(leftTime);
            }
            else
            {
                text_countDown.gameObject.SetActive(false);
            }
        }
    }

    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < 1)
        {
            return;
        }
        timer--;
        leftTime--;
        if (text_countDown != null)
        {
            if (MergeLevelManager.Instance.IsBranch(levelType))
            {
                leftTime = (int)(BranchSystem.Instance.curStartTime.AddDays(BranchSystem.Instance.NewLoopTime) - TimeManager.Instance.UtcNow()).TotalSeconds;
                if (leftTime > 0) 
                {
                    text_countDown.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(leftTime);
                }
                else if(BranchSystem.Instance.ClaimedAllTask() && isOpen)
                {
                    text_countDown.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/EctypeDescribe11");
                }
                var open = BranchSystem.Instance.GetIsOpen();
                if (open != isOpen)
                {
                    isOpen = open;
                    RefreshState();
                }
            }
            else
            {
                leftTime = (int)(FestivalSystem.Instance.GetFestivalEndTime(levelType) - TimeManager.Instance.UtcNow()).TotalSeconds;
                text_countDown.text = MyTimer.ReturnTextUntilSecond_MaxShowTwo(leftTime);
                var open = isOpen = FestivalSystem.Instance.IsFestivalOpen(levelType);
                if (open != isOpen)
                {
                    isOpen = open;
                    RefreshState();
                }
            }
        }
    }
}
