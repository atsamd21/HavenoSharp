namespace HavenoSharp.Models.Requests;

public class PostOfferRequest
{
    public string CurrencyCode { get; set; } = string.Empty;
    public string Direction { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public bool UseMarketBasedPrice { get; set; }
    public double MarketPriceMarginPct { get; set; }
    public ulong Amount { get; set; }
    public ulong MinAmount { get; set; }
    public double SecurityDepositPct { get; set; }
    public string TriggerPrice { get; set; } = string.Empty;
    public bool ReserveExactAmount { get; set; }
    public string PaymentAccountId { get; set; } = string.Empty;
    public bool IsPrivateOffer { get; set; }
    public bool BuyerAsTakerWithoutDeposit { get; set; }
    public string ExtraInfo { get; set; } = string.Empty;
    public string SourceOfferId { get; set; } = string.Empty;
}
