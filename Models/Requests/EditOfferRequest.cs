namespace HavenoSharp.Models.Requests;

public class EditOfferRequest
{
    public string OfferId { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public bool UseMarketBasedPrice { get; set; }
    public double MarketPriceMarginPct { get; set; }
    public string TriggerPrice { get; set; } = string.Empty;
    public string PaymentAccountId { get; set; } = string.Empty;
    public string ExtraInfo { get; set; } = string.Empty;
}
