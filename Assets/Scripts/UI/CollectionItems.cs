using I2.Loc;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionItems : MonoBehaviour
{
    [Header("Basic")]
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private GameObject discoveredBg;
    [SerializeField] private GameObject undiscoveredGO;
    [SerializeField] private GameObject undiscoveredBg;
    [SerializeField] private GameObject discoveredGO;
    [SerializeField] private GameObject boxGO;
    [SerializeField] private GameObject itemIcon;
    [SerializeField] private Button clickableGO;
    [SerializeField] private ParticleSystem boxParticle;
    [SerializeField] private GameObject arrowGO;
    #region 稀有度
    [Header("Rarity")]
    [SerializeField] private GameObject[] rarityArrayGO;
    #endregion

    #region 星级
    [Header("STARS")]
    [SerializeField] private GameObject[] starArrayGO;
    #endregion

    #region 加号
    //[Header("PLUS")]
    //[SerializeField] private GameObject[] plusArrayGO;
    //[SerializeField] private GameObject plusBG;
    //[SerializeField] private TextMeshProUGUI bestText;
    #endregion
    [HideInInspector] public MergeItemDiscoveryState DiscoveryState { get; private set; }
    [HideInInspector] public string ItemName { get; private set; }

    private CollectionsFrame frameData;

    private CollectionItemData mData;

    private UIPanel_Collection uiPanel_Collection;
    private RectTransform scrollViewRT;
    private string itemChainName;
    private MergeRewardItem rewardItem;

    private ItemRarityType itemRarity;

    private GameObject iconGO;
    private void Start()
    {
        clickableGO.onClick.AddListener(DiscoveredItem);
    }

    private void OnEnable()
    {
        SetParticleActivity();
    }

    private void SetItemBasicInformation()
    {
        frameData = mData.frameData.item;
        uiPanel_Collection = mData.uipanel_Collection;
        scrollViewRT = uiPanel_Collection.propScrollRect;
        uiPanel_Collection.propScrollRect.GetComponent<ScrollRect>().onValueChanged.AddListener(WrapContent);
        ItemName = mData.itemData.PrefabName;
        itemChainName = mData.itemData.ItemName;
        itemNameText.text = ScriptLocalization.Get(mData.itemData.locKey_Name);
        rewardItem = mData.itemData.BookUnlockReward;
        
        itemRarity = mData.itemData.RarityType;
        RefreshItemState(mData.itemData.m_discoveryState);
        CreateItemAndSetup();
        if (string.IsNullOrEmpty(rewardItem.name))
        {
            DiscoveryState = MergeItemDiscoveryState.Discovered;    //改变当前状态
            RefreshItemState(DiscoveryState);
            frameData.TryRemoveClaimedDict();                                        
            frameData.itemClaimAC?.Invoke();
            boxGO.SetActive(false);
            clickableGO.onClick.RemoveListener(DiscoveredItem); 
        }
    }

    const float upExtents = 974.5f;     //上方界限
    const float bottomExtents = 982.5f; //下方界限
    private void WrapContent(Vector2 arg0)
    {
        SetParticleActivity();
    }

    private void SetParticleActivity()
    {
        if (scrollViewRT == null)
            return;

        Vector3[] corners = new Vector3[4];
        scrollViewRT.GetWorldCorners(corners);

        for (int i = 0; i < 4; ++i)
        {
            Vector3 v = corners[i];
            v = scrollViewRT.InverseTransformPoint(v);
            corners[i] = v;
        }
        Vector3 center = Vector3.Lerp(corners[0], corners[2], 0.5f);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(scrollViewRT,
                transform.position, Camera.main, out var screenPos);
        //float dis = Mathf.Abs(screenPos.y - center.y);
        //if (dis > upExtents && dis <= bottomExtents)
        //{
        //    if (boxGO.activeSelf)
        //        boxParticle.gameObject.SetActive(true);
        //}
        //else
        //{
        //    if (boxParticle.gameObject.activeSelf)
        //        boxParticle.gameObject.SetActive(false);
        //}
    }

    //按钮领取奖励
    private void DiscoveredItem()
    {
        if (string.IsNullOrEmpty(rewardItem.name))
        {
            Debug.LogError($"{mData.itemData.PrefabName}为空");
            return;
        }

        //AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.chainBookOpen);
        GameManager.Instance.GiveRewardItem(rewardItem, "ClickDiscover", transform.position); //发放奖励
        AudioManager.Instance.PlayEffect(AudioManager.audioClipSO.chainBookOpen);
        DiscoveryState = MergeItemDiscoveryState.Discovered;    //改变当前状态
        RefreshItemState(DiscoveryState);
        frameData.TryRemoveClaimedDict();

        frameData.itemClaimAC?.Invoke();
        boxGO.SetActive(false);
        clickableGO.onClick.RemoveListener(DiscoveredItem);
    }

    public void SetItemData(CollectionItemData data)
    {
        mData = data;
        SetItemBasicInformation();
    }
    public void SetupItemArrowActivity(int id, int itemCount)
    {
        if (id == itemCount - 1 || id == 3 || id == 7 || id == 11 || id == 15 || id == 19 || id == 23)
            arrowGO.SetActive(false);
        else
            arrowGO.SetActive(true);
    }
    //根据稀有度改变item外观
    public void CreateItemAndSetup()
    {
        SetupItemStarsIcon();
        //SetupItemPlusIcon();
        SetupItemUnlockRarity();
    }
    private void SetupItemStarsIcon()
    {
        switch (itemRarity)
        {
            case ItemRarityType.Simple:
                foreach (var item in starArrayGO)
                {
                    item.SetActive(false);
                }
                break;
            case ItemRarityType.Star1:
                foreach (var item in starArrayGO)
                {
                    item.SetActive(false);
                }
                starArrayGO[0].SetActive(true);
                break;
            case ItemRarityType.star2:
                foreach (var item in starArrayGO)
                {
                    item.SetActive(false);
                }
                starArrayGO[1].SetActive(true);
                break;
            case ItemRarityType.Star3:
                foreach (var item in starArrayGO)
                {
                    item.SetActive(false);
                }
                starArrayGO[2].SetActive(true);
                break;
            case ItemRarityType.Star4:
                foreach (var item in starArrayGO)
                {
                    item.SetActive(false);
                }
                starArrayGO[3].SetActive(true);
                break;
            case ItemRarityType.Star5:
                foreach (var item in starArrayGO)
                {
                    item.SetActive(false);
                }
                starArrayGO[4].SetActive(true);
                break;
            default:
                break;
        }
    }

    //private void SetupItemPlusIcon()
    //{
    //    if (DiscoveryState == MergeItemDiscoveryState.Undiscovered)
    //    {
    //        switch (specialDisplay)
    //        {
    //            case 0:
    //                plusBG.SetActive(false);
    //                break;
    //            case 1:
    //                plusBG.SetActive(true);
    //                foreach (var item in plusArrayGO)
    //                {
    //                    item.SetActive(false);
    //                }
    //                plusArrayGO[0].SetActive(true);
    //                break;
    //            case 2:
    //                plusBG.SetActive(true);
    //                foreach (var item in plusArrayGO)
    //                {
    //                    item.SetActive(false);
    //                }
    //                plusArrayGO[1].SetActive(true);
    //                break;
    //            case 3:
    //                plusBG.SetActive(true);
    //                foreach (var item in plusArrayGO)
    //                {
    //                    item.SetActive(false);
    //                }
    //                plusArrayGO[2].SetActive(true);
    //                break;
    //            case 4:
    //                plusBG.SetActive(true);
    //                foreach (var item in plusArrayGO)
    //                {
    //                    item.SetActive(false);
    //                }
    //                plusArrayGO[3].SetActive(true);
    //                break;
    //            default:
    //                plusBG.SetActive(false);
    //                break;
    //        }
    //    }
    //}

    private void SetupItemUnlockRarity()
    {
        if (DiscoveryState == MergeItemDiscoveryState.Undiscovered)
        {
            switch (itemRarity)
            {
                case ItemRarityType.Simple:
                case ItemRarityType.Star1:
                    foreach (var item in rarityArrayGO)
                    {
                        item.SetActive(false);
                    }
                    rarityArrayGO[0].SetActive(true);
                    break;
                case ItemRarityType.star2:
                    foreach (var item in rarityArrayGO)
                    {
                        item.SetActive(false);
                    }
                    rarityArrayGO[1].SetActive(true);
                    break;
                case ItemRarityType.Star3:
                    foreach (var item in rarityArrayGO)
                    {
                        item.SetActive(false);
                    }
                    rarityArrayGO[2].SetActive(true);
                    break;
                case ItemRarityType.Star4:
                    foreach (var item in rarityArrayGO)
                    {
                        item.SetActive(false);
                    }
                    rarityArrayGO[3].SetActive(true);
                    break;
                case ItemRarityType.Star5:
                    foreach (var item in rarityArrayGO)
                    {
                        item.SetActive(false);
                    }
                    rarityArrayGO[4].SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    public void RefreshItemState(MergeItemDiscoveryState state)
    {
        DiscoveryState = state;
        switch (DiscoveryState)
        {
            case MergeItemDiscoveryState.NONE:
            case MergeItemDiscoveryState.Undiscovered:
                undiscoveredGO.SetActive(true);
                undiscoveredBg.SetActive(true);
                //icon_Question.SetActive(true);
                discoveredGO.SetActive(false);
                boxGO.SetActive(false);
                break;
            case MergeItemDiscoveryState.Unlock:
                undiscoveredGO.SetActive(false);
                discoveredGO.SetActive(false);
                undiscoveredBg.SetActive(false);
                //icon_Question.SetActive(false);
                boxGO.SetActive(true);
                boxParticle.Play();
                break;
            case MergeItemDiscoveryState.Discovered:
                undiscoveredGO.SetActive(false);
                undiscoveredBg.SetActive(false);
                discoveredGO.SetActive(true);
                boxGO.SetActive(false);
                if (iconGO == null)
                {
                    iconGO = AssetSystem.Instance.Instantiate(ItemName, itemIcon.transform);
                }
                break;
        }
        BookSaveSystem.Instance.SaveData(ItemName, DiscoveryState);
    }

    public void RefreshLanguageText()
    {
        itemNameText.text = ScriptLocalization.Get(mData.itemData.locKey_Name);
    }
}
