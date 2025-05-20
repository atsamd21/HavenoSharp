namespace HavenoSharp.Models.Responses;

public class CreateXmrTxResponse
{
    public string Hash { get; set; } = string.Empty;
    public string Fee { get; set; } = string.Empty;
    public bool IsConfirmed { get; set; }
    public bool IsLocked { get; set; }
    public ulong Height { get; set; }
    public ulong Timestamp { get; set; }
    public List<XmrIncomingTransfer> IncomingTransfers { get; set; } = [];
    public XmrOutgoingTransfer OutgoingTransfer { get; set; } = new();
    public string Metadata { get; set; } = string.Empty;
}
