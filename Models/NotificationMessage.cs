namespace HavenoSharp.Models;

public class NotificationMessage
{
    public string Id { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
    public long Timestamp { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public TradeInfo Trade { get; set; } = new();
    public ChatMessage ChatMessage { get; set; } = new();
}
