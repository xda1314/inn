using Ivy.Consts;
using Ivy.InAppMessage;
using Ivy.Timer;
using IvyCore;

public class InAppMessageSystem : InAppMessageSystemBase<InAppMessageSystem>
{
    public override bool TryShowPackUIAuto()
    {
        if (!GameLoader.Instance.IsLoadingFinish)
            return false;

        if (UI_TutorManager.Instance != null && UI_TutorManager.Instance.IsTutoring())
            return false;

        if (InAppMessageSystem.Instance.CurrentPackData != null)
        {
            //switch (InAppMessageSystem.Instance.CurrentPackData.packArea)
            //{
            //    case "home":
            //        {
            //        }
            //        break;
            //    default:
            //        break;
            //}

            if (InAppMessageSystem.Instance.CurrentPackData.packType == InAppMessagePackType.CrossPromotion)
            {
                if (!InAppMessageSystem.Instance.CurrentPackData._hasCheckDownloadAsset)
                {
                    InAppMessageSystem.Instance.CurrentPackData._hasCheckDownloadAsset = true;
                    string uri = "https://cdn.lisgame.com/promote/";
                    string package = InAppMessageSystem.Instance.CurrentPackData.crossPromotion_packageID;
                    if (package == LisGameConsts.AppID_Android_MergeFarmtown || package == LisGameConsts.AppID_IOS_MergeFarmtown)
                    {
                        uri += LisGameConsts.AppID_Android_MergeFarmtown + ".jpg";
                    }
                    else if (package == LisGameConsts.AppID_Android_MergeRomance || package == LisGameConsts.AppID_IOS_MergeRomance)
                    {
                        uri += LisGameConsts.AppID_Android_MergeRomance + ".jpg";
                    }
                    else
                    {
                        uri += LisGameConsts.AppID_Android_MergeElves + ".jpg";
                    }

                    DownloadTextureManager.DownloadTextureAsync(uri, () =>
                    {
                        InAppMessageSystem.Instance.CurrentPackData._hasTryDownloadAsset = true;
                    }, () =>
                    {
                        TimerSystem.Instance.AddTimer(60, () =>
                        {
                            DownloadTextureManager.DownloadTextureAsync(uri, () =>
                            {
                                InAppMessageSystem.Instance.CurrentPackData._hasTryDownloadAsset = true;
                            }, () =>
                            {
                                InAppMessageSystem.Instance.CurrentPackData._hasTryDownloadAsset = true;
                            });
                        });
                    });
                }

                if (InAppMessageSystem.Instance.CurrentPackData._hasTryDownloadAsset && !UISystem.Instance.HasUI)
                {
                    UISystem.Instance.AddToWaitShowList(new UIPanelData_CrossPromotion(InAppMessageSystem.Instance.CurrentPackData.id));
                    return true;
                }
            }
            else
            {
                if (!UISystem.Instance.HasUI)
                {
                    UISystem.Instance.AddToWaitShowList(new UIPanelDataBase(Consts.UIPanel_InAppMessage));
                    return true;
                }
            }

        }

        return false;
    }
}
