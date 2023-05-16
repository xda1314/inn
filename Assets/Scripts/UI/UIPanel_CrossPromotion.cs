using DG.Tweening;
using Ivy;
using Ivy.Activity.CrossPromotion;
using Ivy.Consts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelData_CrossPromotion : UIPanelDataBase
{
    public string inAppMessagePackID;

    public UIPanelData_CrossPromotion(string packData, UIShowLayer UIShowLayer = UIShowLayer.Popup) : base(Consts.UIPanel_CrossPromotion, UIShowLayer)
    {
        this.inAppMessagePackID = packData;
    }
}

public class UIPanel_CrossPromotion : UIPanelBase
{
    [SerializeField] private Button btn_Close;
    [SerializeField] private Button btn_ok;
    [SerializeField] private Text txt_ok;
    [SerializeField] private Text txt_title;
    [SerializeField] private Text txt_desc;
    [SerializeField] private Image bgGO;
    [SerializeField] private Transform targetTran;
    [SerializeField] private TextMeshProUGUI txt_num;
    [SerializeField] private Transform loadingIcon;
    [SerializeField] private BtnOutlineFX btnOutlineFX;

    public override void OnInitUI()
    {
        base.OnInitUI();

        btn_Close.onClick.AddListener(() => { UISystem.Instance.HideUI(this); });

        btn_ok.onClick.AddListener(() =>
        {
            if (!Activity_CrossPromotion.Instance.HasPromotion())
                return;
            if (Activity_CrossPromotion.Instance.CheckInstalled() && Activity_CrossPromotion.Instance.TryClaimRewards())
            {
                List<MergeRewardItem> rewardItems = new List<MergeRewardItem>();
                string source = Activity_CrossPromotion.Instance.ActivityConfig.InstalledRewards.Source;
                foreach (var item in Activity_CrossPromotion.Instance.ActivityConfig.InstalledRewards.Rewards)
                {
                    MergeRewardItem rewardItem = new MergeRewardItem();
                    rewardItem.name = item.Name;
                    rewardItem.num = item.Num;
                    rewardItems.Add(rewardItem);
                }
                btnOutlineFX.StopFX();
                GameManager.Instance.GiveRewardItem(rewardItems, source);
                txt_ok.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/Reward/Btn2");
            }
            else
            {
                string package = Activity_CrossPromotion.Instance.ActivityData.PackageID;
                if (package == LisGameConsts.AppID_Android_MergeFarmtown || package == LisGameConsts.AppID_IOS_MergeFarmtown)
                {
#if UNITY_ANDROID
                    AnalyticsUtil.TrackEvent("click_promote_link", new Dictionary<string, string>()
                    {
                        { "label","crosspromotion"},
                        {"promoteapp",LisGameConsts.AppID_Android_MergeFarmtown }
                    });
                    RiseSdk.Instance.OpenPromoteUrl(LisGameConsts.AppID_Android_MergeFarmtown, "https://mergefarmtown.onelink.me/885h/dqf9ux5d");
#elif UNITY_IOS
                    AnalyticsUtil.TrackEvent("click_promote_link", new Dictionary<string, string>()
                    {
                        { "label","crosspromotion"},
                        {"promoteapp",LisGameConsts.AppID_IOS_MergeFarmtown }
                    });
                     RiseSdk.Instance.OpenPromoteUrl(LisGameConsts.AppID_IOS_MergeFarmtown, "https://mergefarmtown.onelink.me/885h/dqf9ux5d");
#endif
                }
                //else if (package == LisGameConsts.AppID_Android_MergeRomance || package == LisGameConsts.AppID_IOS_MergeRomance)
                //{

                //}
                else
                {
#if UNITY_ANDROID
                    AnalyticsUtil.TrackEvent("click_promote_link", new Dictionary<string, string>()
                    {
                        { "label","crosspromotion"},
                        {"promoteapp",LisGameConsts.AppID_Android_MergeElves }
                    });
                    RiseSdk.Instance.OpenPromoteUrl(LisGameConsts.AppID_Android_MergeElves, "https://mergeelves.onelink.me/Rpan/8boqr6se");
#elif UNITY_IOS
                    AnalyticsUtil.TrackEvent("click_promote_link", new Dictionary<string, string>()
                    {
                        { "label","crosspromotion"},
                        {"promoteapp",LisGameConsts.AppID_IOS_MergeElves }
                    });
                     RiseSdk.Instance.OpenPromoteUrl(LisGameConsts.AppID_IOS_MergeElves, "https://mergeelves.onelink.me/Rpan/8boqr6se");
#endif
                }
            }

        });
    }

    public override IEnumerator OnShowUI()
    {
        var panelData = (UIPanelData_CrossPromotion)UIPanelData;
        if (!string.IsNullOrEmpty(panelData.inAppMessagePackID))
        {
            InAppMessageSystem.Instance.SetPackShow(panelData.inAppMessagePackID);
        }

        loadingIcon.DOKill();

        txt_desc.text = I2.Loc.ScriptLocalization.Get("Obj/Recommend/Games");

        if (Activity_CrossPromotion.Instance.CheckInstalled())
        {
            if (Activity_CrossPromotion.Instance.ActivityData.Claimed)
                txt_ok.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/Reward/Btn2");
            else
                txt_ok.text = I2.Loc.ScriptLocalization.Get("Obj/SpurLine/Reward/Btn1");
        }
        else
        {
            txt_ok.text = I2.Loc.ScriptLocalization.Get("Obj/Dungeon/ButtonText2");
        }


        if (Activity_CrossPromotion.Instance.ActivityConfig.InstalledRewards != null
            && Activity_CrossPromotion.Instance.ActivityConfig.InstalledRewards.Count > 0)
        {
            MergeRewardItem mergeRewardItem = new MergeRewardItem();
            var reward = Activity_CrossPromotion.Instance.ActivityConfig.InstalledRewards.Rewards[0];
            mergeRewardItem.name = reward.Name;
            mergeRewardItem.num = reward.Num;
            if (AssetSystem.Instance.ContainsKey(mergeRewardItem.ShowRewardPrefabName))
                AssetSystem.Instance.Instantiate(mergeRewardItem.ShowRewardPrefabName, targetTran);
            txt_num.text = mergeRewardItem.ShowRewardCountTxt;
        }

        string uri = "https://cdn.lisgame.com/promote/";
        string package = Activity_CrossPromotion.Instance.ActivityData.PackageID;
        if (package == LisGameConsts.AppID_Android_MergeFarmtown || package == LisGameConsts.AppID_IOS_MergeFarmtown)
        {
            txt_title.text = I2.Loc.ScriptLocalization.Get("Obj/Dungeon/ButtonText2") + " Merge Farmtown";
            uri += LisGameConsts.AppID_Android_MergeFarmtown + ".jpg";
        }
        else if (package == LisGameConsts.AppID_Android_MergeRomance || package == LisGameConsts.AppID_IOS_MergeRomance)
        {
            txt_title.text = I2.Loc.ScriptLocalization.Get("Obj/Dungeon/ButtonText2") + " Merge Romance";
            uri += LisGameConsts.AppID_Android_MergeRomance + ".jpg";
        }
        else
        {
            txt_title.text = I2.Loc.ScriptLocalization.Get("Obj/Dungeon/ButtonText2") + " Merge Elves";
            uri += LisGameConsts.AppID_Android_MergeElves + ".jpg";
        }

        if (bgGO.sprite != null)
        {
            bgGO.gameObject.SetActive(true);
        }
        else
        {
            bgGO.gameObject.SetActive(false);
            loadingIcon.DOKill();
            loadingIcon.DOLocalRotate(Vector3.forward * -360, 3, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);

            DownloadTextureManager.LoadTextureAsync(uri, (texture2D) =>
            {
                if (this != null)
                {
                    loadingIcon.DOKill();
                    var spriteTemp = texture2D.Texture2DToSprite();
                    bgGO.sprite = spriteTemp;
                    bgGO.gameObject.SetActive(true);
                }
            }, null);
        }

        yield return base.OnShowUI();
    }

    public override IEnumerator OnHideUI()
    {
        loadingIcon.DOKill();
        var panelData = (UIPanelData_CrossPromotion)UIPanelData;
        if (!string.IsNullOrEmpty(panelData.inAppMessagePackID))
        {
            InAppMessageSystem.Instance.SetPackHide(panelData.inAppMessagePackID);
        }
        yield return base.OnHideUI();
    }

}
