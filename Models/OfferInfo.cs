namespace HavenoSharp.Models;

public enum OfferDirection
{
    BUY,
    SELL
}

public class OfferInfo
{
    public string Id { get; set; } = string.Empty;
    public string Direction { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public bool UseMarketBasedPrice { get; set; }
    public double MarketPriceMarginPct { get; set; }
    public ulong Amount { get; set; }
    public ulong MinAmount { get; set; }
    public double MakerFeePct { get; set; }
    public double TakerFeePct { get; set; }
    public double PenaltyFeePct { get; set; }
    public double BuyerSecurityDepositPct { get; set; }
    public double SellerSecurityDepositPct { get; set; }
    public string Volume { get; set; } = string.Empty;
    public string MinVolume { get; set; } = string.Empty;
    public string TriggerPrice { get; set; } = string.Empty;
    public string PaymentAccountId { get; set; } = string.Empty;
    public string PaymentMethodId { get; set; } = string.Empty;
    public string PaymentMethodShortName { get; set; } = string.Empty;
    public string BaseCurrencyCode { get; set; } = string.Empty;
    public string CounterCurrencyCode { get; set; } = string.Empty;
    public ulong Date { get; set; }
    public string State { get; set; } = string.Empty;
    public bool IsActivated { get; set; }
    public bool IsMyOffer { get; set; }
    public string OwnerNodeAddress { get; set; } = string.Empty;
    public string PubKeyRing { get; set; } = string.Empty;
    public string VersionNr { get; set; } = string.Empty;
    public int ProtocolVersion { get; set; }
    public string ArbitratorSigner { get; set; } = string.Empty;
    public string SplitOutputTxHash { get; set; } = string.Empty;
    public ulong SplitOutputTxFee { get; set; }
    public bool IsPrivateOffer { get; set; }
    public string Challenge { get; set; } = string.Empty;   
    public string ExtraInfo { get; set; } = string.Empty;
}