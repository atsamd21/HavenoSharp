namespace HavenoSharp.Models;

public class TradeStatistic
{
    public string Currency { get; set; } = string.Empty;
    public long Price { get; set; }
    public long Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public long Date { get; set; }
    public string Arbitrator { get; set; } = string.Empty;
    public byte[] Hash { get; set; } = [];
    public string MakerDepositTxId { get; set; } = string.Empty;
    public string TakerDepositTxId { get; set; } = string.Empty;
    public Dictionary<string, string> ExtraData { get; set; } = [];
}
