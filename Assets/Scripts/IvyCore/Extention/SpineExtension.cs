using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpineExtension{

	public static void CopyDataFrom(this Spine.Unity.SkeletonGraphic sg,Spine.Unity.SkeletonAnimation sa)
    {
        sg.skeletonDataAsset = sa.SkeletonDataAsset;
        if(sa.Skeleton.Skin != null)
            sg.initialSkinName = sa.Skeleton.Skin.Name;
        else
            sg.initialSkinName = "";
        if(sa.AnimationState != null && sa.AnimationState.Tracks.Count > 0)
            sg.startingAnimation = sa.AnimationState.GetCurrent(0).Animation.Name;
        sg.Initialize(true);
        if (sa.AnimationState != null && sa.AnimationState.Tracks.Count > 0)
            sg.AnimationState.SetAnimation(0, sa.AnimationState.GetCurrent(0).Animation.Name, true);
        if (sa.Skeleton.Skin != null)
            sg.Skeleton.SetSkin(sa.Skeleton.Skin.Name);
    }

    public static void CopyDataFrom(this Spine.Unity.SkeletonAnimation sa, Spine.Unity.SkeletonGraphic sg)
    {
        sa.skeletonDataAsset = sg.skeletonDataAsset;
        if (sg.Skeleton.Skin != null)
            sa.initialSkinName = sg.Skeleton.Skin.Name;
        else
            sa.initialSkinName = "";
        sa.Initialize(true);
        sa.AnimationState.SetAnimation(0, sg.AnimationState.GetCurrent(0).Animation.Name, true);
        if (sg.Skeleton.Skin != null)
            sa.Skeleton.SetSkin(sg.Skeleton.Skin.Name);
    }
}
