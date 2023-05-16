using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RewardItem : MonoBehaviour
{
    [SerializeField] private Transform partentTF;

    public void Create(MergeRewardItem rewardItem)
    {
        if (string.IsNullOrEmpty(rewardItem.name))
            return;

        GameObject gO = AssetSystem.Instance.Instantiate(rewardItem.ShowRewardPrefabName, partentTF);
        if (gO != null)
        {
            if (gO.TryGetComponent(out Sprite sprite))
            {
                sprite.texture.height = 200;
                sprite.texture.width = 200;
                gO.transform.localScale = 0.1f * Vector3.one;

                gO.transform.DOScale(1, 0.8f).SetEase(Ease.OutQuad);
            }
        }
    }
}
