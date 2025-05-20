namespace HavenoSharp.Models;

public class TradeCurrency
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public CryptoCurrency CryptoCurrency { get; set; } = new();
    public TraditionalCurrency TraditionalCurrency { get; set; } = new();
}
