namespace HavenoSharp.Models;

public class PaymentAccount
{
    public string Id { get; set; } = string.Empty;
    public long CreationDate { get; set; }
    public PaymentMethod PaymentMethod { get; set; } = new();
    public string AccountName { get; set; } = string.Empty;
    public List<TradeCurrency> TradeCurrencies { get; set; } = [];
    public TradeCurrency SelectedTradeCurrency { get; set; } = new();
    public PaymentAccountPayload PaymentAccountPayload { get; set; } = new();
}
