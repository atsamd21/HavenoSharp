namespace HavenoSharp.Models;

public class ContractInfo
{
    public string BuyerNodeAddress { get; set; } = string.Empty;
    public string SellerNodeAddress { get; set; } = string.Empty;
    public string ArbitratorNodeAddress { get; set; } = string.Empty;
    public bool IsBuyerMakerAndSellerTaker { get; set; }
    public string MakerAccountId { get; set; } = string.Empty;
    public string TakerAccountId { get; set; } = string.Empty;
    public PaymentAccountPayload MakerPaymentAccountPayload { get; set; } = new();
    public PaymentAccountPayload TakerPaymentAccountPayload { get; set; } = new();
    public string MakerPayoutAddressString { get; set; } = string.Empty;
    public string TakerPayoutAddressString { get; set; } = string.Empty;
    public ulong LockTime { get; set; }
}
