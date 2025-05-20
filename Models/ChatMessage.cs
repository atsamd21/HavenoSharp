namespace HavenoSharp.Models;

public class ChatMessage
{
    public long Date { get; set; }
    public string TradeId { get; set; } = string.Empty;
    public int TraderId { get; set; }
    public bool SenderIsTrader { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<Attachment> Attachments { get; set; } = [];
    public bool Arrived { get; set; }
    public bool StoredInMailbox { get; set; }
    public bool IsSystemMessage { get; set; }
    public NodeAddress SenderNodeAddress { get; set; } = new();
    public string Uid { get; set; } = string.Empty;
    public string SendMessageError { get; set; } = string.Empty;
    public bool Acknowledged { get; set; }
    public string AckError { get; set; } = string.Empty;
    public SupportType Type { get; set; }
    public bool WasDisplayed { get; set; }
}
