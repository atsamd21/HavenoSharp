namespace HavenoSharp.Models;

public class DisputeResult
{
    public string TradeId { get; set; } = string.Empty;
    public int TraderId { get; set; }
    public WinnerType Winner { get; set; }
    public int ReasonOrdinal { get; set; }
    public bool TamperProofEvidence { get; set; }
    public bool IdVerification { get; set; }
    public bool ScreenCast { get; set; }
    public string SummaryNotes { get; set; } = string.Empty;
    public ChatMessage ChatMessage { get; set; } = new();
    public byte[] ArbitratorSignature { get; set; } = [];
    public long BuyerPayoutAmountBeforeCost { get; set; }
    public long SellerPayoutAmountBeforeCost { get; set; }
    public SubtractFeeFromType SubtractFeeFrom { get; set; }
    public byte[] ArbitratorPubKey { get; set; } = [];
    public long CloseDate { get; set; }
    public bool IsLoserPublisher { get; set; }
}
