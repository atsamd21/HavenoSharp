namespace HavenoSharp.Models;

public class MarketDepthInfo
{
    public string CurrencyCode { get; set; } = string.Empty;
    public List<double> BuyPrices { get; set; } = [];
    public List<double> BuyDepth { get; set; } = [];
    public List<double> SellPrices { get; set; } = [];
    public List<double> SellDepth { get; set; } = [];
}
