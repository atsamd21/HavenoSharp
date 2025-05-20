namespace HavenoSharp.Models;

public class PaymentAccountFormField
{
    public FieldId Id { get; set; }
    public Component Component { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public uint MinLength { get; set; }
    public uint MaxLength { get; set; }
    public List<TradeCurrency> SupportedCurrencies { get; set; } = [];
    public List<Country> SupportedCountries { get; set; } = [];
    public List<Country> SupportedSepaEuroCountries { get; set; } = [];
    public List<Country> SupportedSepaNonEuroCountries { get; set; } = [];
    public List<string> RequiredForCountries { get; set; } = [];
}
