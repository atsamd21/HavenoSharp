namespace HavenoSharp.Models.Requests;

public class SendDisputeChatMessageRequest
{
    public string DisputeId { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public List<Attachment> Attachments { get; set; } = [];
}
