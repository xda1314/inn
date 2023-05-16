using System.Collections.Generic;

public class ShopPackData
{
    public string saveID;
    public List<MergeRewardItem> rewardItem;
    public int countLimit;
    public int cost;
    public CurrencyID currencyID;
    public string OriginalPrice;
    public long refreshTime;
    public string sameKind;
    public ShopPackData(
        string saveID,
        List<MergeRewardItem> rewardItem,
        int countLimit,
        int cost,
        CurrencyID currencyID,
        string OriginalPrice = "",
        long refreshTime = 0,
        string sameKind = "")
    {
        this.saveID = saveID;
        this.rewardItem = rewardItem;
        this.countLimit = countLimit;
        this.cost = cost;
        this.currencyID = currencyID;
        this.OriginalPrice = OriginalPrice;
        this.refreshTime = refreshTime;
        this.sameKind = sameKind;
    }

    public int todayBuyCount;
    public MergeLevelType MergeLevel = MergeLevelType.none;
}

/// <summary>
/// 货币ID
/// </summary>
public enum CurrencyID
{
    NONE=0,
    Exp,
    Coins,
    Gems,
    Energy,
    Free,
    AD,
    GemsOrCoins,
    Pay,
    Needle
}
