namespace HavenoSharp.Models;

public class PaymentMethod
{
    public string Id { get; set; } = string.Empty;
    public long MaxTradePeriod { get; set; }
    public long MaxTradeLimit { get; set; }
    public List<string> SupportedAssetCodes { get; set; } = [];
}
