namespace HavenoSharp.Models.Requests;

public class TakeOfferRequest
{
    public ulong Amount { get; set; }
    public string PaymentAccountId { get; set; } = string.Empty;
    public string Challenge { get; set; } = string.Empty;
    public string OfferId { get; set; } = string.Empty;
}
