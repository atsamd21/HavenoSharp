namespace HavenoSharp.Models;

public class TradeInfo
{
    public OfferInfo Offer { get; set; } = new();
    public string TradeId { get; set; } = string.Empty;
    public string ShortId { get; set; } = string.Empty;
    public ulong Date { get; set; }
    public string Role { get; set; } = string.Empty;
    public ulong Amount { get; set; }
    public ulong MakerFee { get; set; }
    public ulong TakerFee { get; set; }
    public ulong BuyerSecurityDeposit { get; set; }
    public ulong SellerSecurityDeposit { get; set; }
    public ulong BuyerDepositTxFee { get; set; }
    public ulong SellerDepositTxFee { get; set; }
    public ulong BuyerPayoutTxFee { get; set; }
    public ulong SellerPayoutTxFee { get; set; }
    public ulong BuyerPayoutAmount { get; set; }
    public ulong SellerPayoutAmount { get; set; }
    public string Price { get; set; } = string.Empty;
    public string ArbitratorNodeAddress { get; set; } = string.Empty;
    public string TradePeerNodeAddress { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Phase { get; set; } = string.Empty;
    public string PeriodState { get; set; } = string.Empty;
    public string PayoutState { get; set; } = string.Empty;
    public string DisputeState { get; set; } = string.Empty;
    public bool IsDepositsPublished { get; set; }
    public bool IsDepositsConfirmed { get; set; }
    public bool IsDepositsUnlocked { get; set; }
    public bool IsPaymentSent { get; set; }
    public bool IsPaymentReceived { get; set; }
    public bool IsPayoutPublished { get; set; }
    public bool IsPayoutConfirmed { get; set; }
    public bool IsPayoutUnlocked { get; set; }
    public bool IsCompleted { get; set; }
    public string ContractAsJson { get; set; } = string.Empty;
    public ContractInfo Contract { get; set; } = new();
    public string TradeVolume { get; set; } = string.Empty;
    public string MakerDepositTxId { get; set; } = string.Empty;
    public string TakerDepositTxId { get; set; } = string.Empty;
    public string PayoutTxId { get; set; } = string.Empty;
    public string StartTime { get; set; } = string.Empty;
    public string MaxDurationMs { get; set; } = string.Empty;
    public string DeadlineTime { get; set; } = string.Empty;
}