namespace HavenoSharp.Models;

public class Contract
{
    public OfferPayload OfferPayload { get; set; } = new();
    public long TradeAmount { get; set; }
    public long TradePrice { get; set; }
    public NodeAddress ArbitratorNodeAddress { get; set; } = new();
    public bool IsBuyerMakerAndSellerTaker { get; set; }
    public string MakerAccountId { get; set; } = string.Empty;
    public string TakerAccountId { get; set; } = string.Empty;
    public string MakerPaymentMethodId { get; set; } = string.Empty;
    public string TakerPaymentMethodId { get; set; } = string.Empty;
    public byte[] MakerPaymentAccountPayloadHash { get; set; } = [];
    public byte[] TakerPaymentAccountPayloadHash { get; set; } = [];
    public PubKeyRing MakerPubKeyRing { get; set; } = new();
    public PubKeyRing TakerPubKeyRing { get; set; } = new();
    public NodeAddress BuyerNodeAddress { get; set; } = new();
    public NodeAddress SellerNodeAddress { get; set; } = new();
    public string MakerPayoutAddressString { get; set; } = string.Empty;
    public string TakerPayoutAddressString { get; set; } = string.Empty;
    public string MakerDepositTxHash { get; set; } = string.Empty;
    public string TakerDepositTxHash { get; set; } = string.Empty;
}
