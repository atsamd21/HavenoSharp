namespace HavenoSharp.Models;

public class XmrIncomingTransfer
{
    public string Amount { get; set; } = string.Empty;
    public int AccountIndex { get; set; }
    public int SubaddressIndex { get; set; }
    public string Address { get; set; } = string.Empty;
    public ulong NumSuggestedConfirmations { get; set; }
}
