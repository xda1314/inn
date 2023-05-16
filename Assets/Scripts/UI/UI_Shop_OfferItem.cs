using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop_OfferItem : MonoBehaviour
{
    // Start is called before the first frame update
    #region 组件
    [SerializeField] private Transform[] iconContainerTran;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TextMeshProUGUI[] txt_nums;
    [SerializeField] private Button btn_buy;
    [SerializeField] private TextMeshProUGUI txt_Paycost;
    [SerializeField] private Button Btn_NoBuy;
    [SerializeField] private TextMeshProUGUI text_sellOut;
    [SerializeField] private TextMeshProUGUI text_num;
    [SerializeField] private TextMeshProUGUI txt_bagKInd;
    [SerializeField] private Image img_tag;
    [SerializeField] private Image img_gem;
    [SerializeField] private Image img_bg;
    [SerializeField] private ImageSpritesContainer spritesContainer;
    [SerializeField] private TextMeshProUGUI[] txt_tag;
    [SerializeField] private GameObject go_discount;
    [SerializeField] private TextMeshProUGUI text_discount;
    #endregion

    #region 变量
    public Action timeOutCB;
    public Action BuyCallBack;
    private PayPackDefinition packDef;
    private Action<PayPackDefinition, Vector3> payCB; 
    #endregion

    /// <summary>
    /// 用于不同商品不同类之间基于同一预制体的切换
    /// </summary>
    public void ClearIcon()
    {
        this.packDef = null;

        foreach (var item in iconContainerTran)
        {
            for (int i = item.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(item.transform.GetChild(i).gameObject);
            }
        }
    }

    private void Start()
    {
        BuyCallBack = null;
        BuyCallBack += () =>
        {
            if (this != null && packDef != null && packDef.LimitToOne)
            {
                btn_buy.gameObject.SetActive(false);
                Btn_NoBuy.gameObject.SetActive(true);
                SaveUtils.SetBool(Consts.SaveKey_BuyLimitGift, true);
            }
        };
    }

    public void SetItemInfo(PayPackDefinition payPackDefinition, Action<PayPackDefinition, Vector3> payCB)
    {
        if (payPackDefinition == null || payPackDefinition.RewardItems.Count <= 0)
        {
            Debug.LogError("reward is null");
            payCB = null;
            return;
        }

        if (payPackDefinition.payType== "HOT") 
        {
            go_discount.gameObject.SetActive(true);
            text_discount.text= I2.Loc.ScriptLocalization.Get("Obj/Shop/Popular");
        }
        else if (payPackDefinition.payType == "POP")
        {
            go_discount.gameObject.SetActive(true);
            text_discount.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Pop");
        }
        else if (payPackDefinition.payType == "BEST")
        {
            go_discount.gameObject.SetActive(true);
            text_discount.text = I2.Loc.ScriptLocalization.Get("Obj/Shop/Best");
        }
        else 
        {
            go_discount.gameObject.SetActive(false);
        }


        img_tag.sprite = spritesContainer.GetSprite(payPackDefinition.tag);
        foreach (var txt_bagKInd in txt_tag)
        {
            txt_bagKInd.text = I2.Loc.ScriptLocalization.Get(payPackDefinition.sameKind);
        }
        txt_tag[0].gameObject.SetActive(payPackDefinition.tag == "img_tag1");
        txt_tag[1].gameObject.SetActive(payPackDefinition.tag == "img_tag2");
        txt_tag[2].gameObject.SetActive(payPackDefinition.tag == "img_tag3");
        txt_tag[3].gameObject.SetActive(payPackDefinition.tag == "img_tag4");
        txt_tag[4].gameObject.SetActive(payPackDefinition.tag == "img_tag5");
        txt_tag[5].gameObject.SetActive(payPackDefinition.tag == "img_tag6");
        txt_tag[6].gameObject.SetActive(payPackDefinition.tag == "img_tag7");
        if (payPackDefinition.tag== "img_tag7") 
        {
            img_bg.sprite= spritesContainer.GetSprite("img_tag7_bg");
        }
        text_num.text = payPackDefinition.RewardItems[0].num.ToString();
        if (payPackDefinition.RewardItems[0].num < 600) 
        {
            img_gem.sprite= spritesContainer.GetSprite("Shop_Gems3");
        }
        else if (payPackDefinition.RewardItems[0].num < 5000)
        {
            img_gem.sprite = spritesContainer.GetSprite("Shop_Gems4");
        }
        else 
        {
            img_gem.sprite = spritesContainer.GetSprite("Shop_Gems5");
        }
        int reward_count = payPackDefinition.RewardItems.Count;
        if (reward_count == 7 || reward_count ==6) 
        {
            rectTransform.sizeDelta = new Vector2(380f, rectTransform.sizeDelta.y);
        }
        for (int index = 1; index < reward_count; index++)
        {
            if (iconContainerTran.Length < index-1)
                return;
            if (iconContainerTran[index-1].childCount <= 0
            || this.packDef == null || this.packDef != null 
            && this.packDef.RewardItems.Count> index && 
            this.packDef.RewardItems[index].ShowRewardPrefabName != payPackDefinition.RewardItems[index].ShowRewardPrefabName)
            {
                for (int i = iconContainerTran[index-1].childCount - 1; i >= 0; i--)
                {
                    if(iconContainerTran[index - 1].transform.GetChild(i).gameObject.name== "num_bg") 
                    {
                        continue;
                    }
                    DestroyImmediate(iconContainerTran[index - 1].transform.GetChild(i).gameObject);
                }
                AssetSystem.Instance.InstantiateAsync(payPackDefinition.RewardItems[index].ShowRewardPrefabName, iconContainerTran[index - 1], gO =>
                {
                    RectTransform rect = gO.GetComponents<RectTransform>()[0];
                    if (gO.name == "Icon_Reward_Energy")
                        rect.sizeDelta = new Vector2(120, 120);
                    rect.pivot = new Vector2(0.5f, 0.5f);
                    rect.localPosition = Vector2.zero;
                    rect.sizeDelta = new Vector2(105, 105);
                    gO.transform.SetAsFirstSibling();
                });
            }
            if (txt_nums.Length > index - 1)
            {
                int num = payPackDefinition.RewardItems[index].num;
                txt_nums[index - 1].transform.parent.gameObject.SetActive(num > 1);
                if (num >= 10) 
                {
                    txt_nums[index - 1].text = num.ToString();
                }
                else
                {
                    txt_nums[index - 1].text = "x" + num.ToString();
                }
                
            }
        }
        for (int i = payPackDefinition.RewardItems.Count - 1; i < iconContainerTran.Length; i++)
        {
            iconContainerTran[i].gameObject.SetActive(false);
        }
        this.packDef = payPackDefinition;
        this.payCB = payCB;
        btn_buy.gameObject.SetActive(true);
        btn_buy.onClick.RemoveAllListeners();
        btn_buy.onClick.AddListener(() =>
        {
            this.payCB?.Invoke(payPackDefinition, iconContainerTran[0].transform.position);
        });
        Btn_NoBuy.gameObject.SetActive(false);
        txt_Paycost.text = payPackDefinition.Cost.ToString();
        text_sellOut.text = I2.Loc.ScriptLocalization.Get("Obj/Chain/Shop_Over");
        UI_ShopContainer.shopGetBillingPriceInfoCB += (info) =>
        {
            if (this == null || txt_Paycost == null || packDef == null || packDef.UnityID != info.UnityID)
                return;
            txt_Paycost.text = info.Price;
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
