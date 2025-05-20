namespace HavenoSharp.Models;

public class Balances
{
    public ulong AvailableXMRBalance { get; set; }
    public ulong XMRBalance { get; set; }
    public ulong PendingXMRBalance { get; set; }
    public ulong ReservedOfferBalance { get; set; }
    public ulong ReservedTradeBalance { get; set; }
    public string PrimaryAddress { get; set; } = string.Empty;
}
