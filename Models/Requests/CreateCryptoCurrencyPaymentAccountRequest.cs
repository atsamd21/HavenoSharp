namespace HavenoSharp.Models.Requests;

public class CreateCryptoCurrencyPaymentAccountRequest
{
    public string AccountName { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public bool TradeInstant { get; set; }
}
