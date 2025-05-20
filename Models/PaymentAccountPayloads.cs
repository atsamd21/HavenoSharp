namespace HavenoSharp.Models;

public class PaymentAccountPayload
{
    public string Id { get; set; } = string.Empty;
    public string PaymentMethodId { get; set; } = string.Empty;
    public long MaxTradePeriod { get; set; }
    public Dictionary<string, string> ExcludeFromJsonData { get; set; } = [];
    public AliPayAccountPayload AliPayAccountPayload { get; set; } = new();
    public ChaseQuickPayAccountPayload ChaseQuickPayAccountPayload { get; set; } = new();
    public ZelleAccountPayload ZelleAccountPayload { get; set; } = new();
    public CountryBasedPaymentAccountPayload CountryBasedPaymentAccountPayload { get; set; } = new();
    public CryptoCurrencyAccountPayload CryptoCurrencyAccountPayload { get; set; } = new();
    public FasterPaymentsAccountPayload FasterPaymentsAccountPayload { get; set; } = new();
    public InteracETransferAccountPayload InteracETransferAccountPayload { get; set; } = new();
    [Obsolete]
    public OKPayAccountPayload OKPayAccountPayload { get; set; } = new();
    public PerfectMoneyAccountPayload PerfectMoneyAccountPayload { get; set; } = new();
    public SwishAccountPayload SwishAccountPayload { get; set; } = new();
    public USPostalMoneyOrderAccountPayload USPostalMoneyOrderAccountPayload { get; set; } = new();
    public UpholdAccountPayload UpholdAccountPayload { get; set; } = new();
    public CashAppAccountPayload CashAppAccountPayload { get; set; } = new();
    public MoneyBeamAccountPayload MoneyBeamAccountPayload { get; set; } = new();
    public VenmoAccountPayload VenmoAccountPayload { get; set; } = new();
    public PopmoneyAccountPayload PopmoneyAccountPayload { get; set; } = new();
    public RevolutAccountPayload RevolutAccountPayload { get; set; } = new();
    public WeChatPayAccountPayload WeChatPayAccountPayload { get; set; } = new();
    public MoneyGramAccountPayload MoneyGramAccountPayload { get; set; } = new();
    public HalCashAccountPayload HalCashAccountPayload { get; set; } = new();
    public PromptPayAccountPayload PromptPayAccountPayload { get; set; } = new();
    public AdvancedCashAccountPayload AdvancedCashAccountPayload { get; set; } = new();
    public InstantCryptoCurrencyAccountPayload InstantCryptoCurrencyAccountPayload { get; set; } = new();
    public JapanBankAccountPayload JapanBankAccountPayload { get; set; } = new();
    public TransferwiseAccountPayload TransferwiseAccountPayload { get; set; } = new();
    public AustraliaPayidPayload AustraliaPayidPayload { get; set; } = new();
    public AmazonGiftCardAccountPayload AmazonGiftCardAccountPayload { get; set; } = new();
    public PayByMailAccountPayload PayByMailAccountPayload { get; set; } = new();
    public CapitualAccountPayload CapitualAccountPayload { get; set; } = new();
    public PayseraAccountPayload PayseraAccountPayload { get; set; } = new();
    public PaxumAccountPayload PaxumAccountPayload { get; set; } = new();
    public SwiftAccountPayload SwiftAccountPayload { get; set; } = new();
    public CelPayAccountPayload CelPayAccountPayload { get; set; } = new();
    public MoneseAccountPayload MoneseAccountPayload { get; set; } = new();
    public VerseAccountPayload VerseAccountPayload { get; set; } = new();
    public CashAtAtmAccountPayload CashAtAtmAccountPayload { get; set; } = new();
    public PayPalAccountPayload PayPalAccountPayload { get; set; } = new();
    public PaysafeAccountPayload PaysafeAccountPayload { get; set; } = new();
}

public class AliPayAccountPayload
{
    public string AccountNr { get; set; } = string.Empty;
}

public class WeChatPayAccountPayload
{
    public string AccountNr { get; set; } = string.Empty;
}

public class ChaseQuickPayAccountPayload
{
    public string Email { get; set; } = string.Empty;
    public string HolderName { get; set; } = string.Empty;
}

public class ZelleAccountPayload
{
    public string HolderName { get; set; } = string.Empty;
    public string EmailOrMobileNr { get; set; } = string.Empty;
}

public class CountryBasedPaymentAccountPayload
{
    public string CountryCode { get; set; } = string.Empty;
    public List<string> AcceptedCountryCodes { get; set; } = [];
    public BankAccountPayload BankAccountPayload { get; set; } = new();
    public CashDepositAccountPayload CashDepositAccountPayload { get; set; } = new();
    public SepaAccountPayload SepaAccountPayload { get; set; } = new();
    public WesternUnionAccountPayload WesternUnionAccountPayload { get; set; } = new();
    public SepaInstantAccountPayload SepaInstantAccountPayload { get; set; } = new();
    public F2FAccountPayload F2FAccountPayload { get; set; } = new();
    public UpiAccountPayload UpiAccountPayload { get; set; } = new();
    public PaytmAccountPayload PaytmAccountPayload { get; set; } = new();
    public IfscBasedAccountPayload IfscBasedAccountPayload { get; set; } = new();
    public NequiAccountPayload NequiAccountPayload { get; set; } = new();
    public BizumAccountPayload BizumAccountPayload { get; set; } = new();
    public PixAccountPayload PixAccountPayload { get; set; } = new();
    public SatispayAccountPayload SatispayAccountPayload { get; set; } = new();
    public StrikeAccountPayload StrikeAccountPayload { get; set; } = new();
    public TikkieAccountPayload TikkieAccountPayload { get; set; } = new();
    public TransferwiseUsdAccountPayload TransferwiseUsdAccountPayload { get; set; } = new();
    public SwiftAccountPayload SwiftAccountPayload { get; set; } = new();
}

public class BankAccountPayload
{
    public string HolderName { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;
    public string BankId { get; set; } = string.Empty;
    public string BranchId { get; set; } = string.Empty;
    public string AccountNr { get; set; } = string.Empty;
    public string AccountType { get; set; } = string.Empty;
    public string HolderTaxId { get; set; } = string.Empty;
    public string NationalAccountId { get; set; } = string.Empty;
    [Obsolete]
    public string Email { get; set; } = string.Empty;
    public NationalBankAccountPayload NationalBankAccountPayload { get; set; } = new();
    public SameBankAccountPayload SameBankAccontPayload { get; set; } = new();
    public SpecificBanksAccountPayload SpecificBanksAccountPayload { get; set; } = new();
    public AchTransferAccountPayload AchTransferAccountPayload { get; set; } = new();
    public DomesticWireTransferAccountPayload DomesticWireTransferAccountPayload { get; set; } = new();
}

public class AchTransferAccountPayload
{
    public string HolderAddress { get; set; } = string.Empty;
}

public class DomesticWireTransferAccountPayload
{
    public string HolderAddress { get; set; } = string.Empty;
}

public class NationalBankAccountPayload 
{ 
    public string Placeholder { get; set; } = string.Empty;
}

public class SameBankAccountPayload
{
    public string Placeholder { get; set; } = string.Empty;
}

public class SpecificBanksAccountPayload
{
    public List<string> AcceptedBanks { get; set; } = [];
}

public class JapanBankAccountPayload
{
    public string BankName { get; set; } = string.Empty;
    public string BankCode { get; set; } = string.Empty;
    public string BankBranchName { get; set; } = string.Empty;
    public string BankBranchCode { get; set; } = string.Empty;
    public string BankAccountType { get; set; } = string.Empty;
    public string BankAccountName { get; set; } = string.Empty;
    public string BankAccountNumber { get; set; } = string.Empty;
}

public class AustraliaPayidPayload
{
    public string BankAccountName { get; set; } = string.Empty;
    public string PayId { get; set; } = string.Empty;
    public string ExtraInfo { get; set; } = string.Empty;
}

public class CashDepositAccountPayload
{
    public string HolderName { get; set; } = string.Empty;
    public string HolderEmail { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;
    public string BankId { get; set; } = string.Empty;
    public string BranchId { get; set; } = string.Empty;
    public string AccountNr { get; set; } = string.Empty;
    public string AccountType { get; set; } = string.Empty;
    public string Requirements { get; set; } = string.Empty;
    public string HolderTaxId { get; set; } = string.Empty;
    public string NationalAccountId { get; set; } = string.Empty;
}

public class MoneyGramAccountPayload
{
    public string HolderName { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class HalCashAccountPayload
{
    public string MobileNr { get; set; } = string.Empty;
}

public class WesternUnionAccountPayload
{
    public string HolderName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class AmazonGiftCardAccountPayload
{
    public string EmailOrMobileNr { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
}

public class SepaAccountPayload
{
    public string HolderName { get; set; } = string.Empty;
    public string Iban { get; set; } = string.Empty;
    public string Bic { get; set; } = string.Empty;
}

public class SepaInstantAccountPayload
{
    public string HolderName { get; set; } = string.Empty;
    public string Iban { get; set; } = string.Empty;
    public string Bic { get; set; } = string.Empty;
}

public class CryptoCurrencyAccountPayload
{
    public string Address { get; set; } = string.Empty;
}

public class InstantCryptoCurrencyAccountPayload
{
    public string Address { get; set; } = string.Empty;
}

public class FasterPaymentsAccountPayload
{
    public string HolderName { get; set; } = string.Empty;
    public string SortCode { get; set; } = string.Empty;
    public string AccountNr { get; set; } = string.Empty;
}

public class InteracETransferAccountPayload
{
    public string Email { get; set; } = string.Empty;
    public string HolderName { get; set; } = string.Empty;
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
}

public class OKPayAccountPayload
{
    public string AccountNr { get; set; } = string.Empty;
}

public class UpholdAccountPayload
{
    public string AccountId { get; set; } = string.Empty;
    public string AccountOwner { get; set; } = string.Empty;
}

public class CashAppAccountPayload
{
    public string EmailOrMobileNrOrCashtag { get; set; } = string.Empty;
    public string ExtraInfo { get; set; } = string.Empty;
}

public class MoneyBeamAccountPayload
{
    public string AccountId { get; set; } = string.Empty;
}

public class VenmoAccountPayload
{
    public string EmailOrMobileNrOrUsername { get; set; } = string.Empty;
}

public class PayPalAccountPayload
{
    public string EmailOrMobileNrOrUsername { get; set; } = string.Empty;
    public string ExtraInfo { get; set; } = string.Empty;
}

public class PopmoneyAccountPayload
{
    public string AccountId { get; set; } = string.Empty;
    public string HolderName { get; set; } = string.Empty;
}

public class RevolutAccountPayload
{
    public string Username { get; set; } = string.Empty;
}

public class PerfectMoneyAccountPayload
{
    public string AccountNr { get; set; } = string.Empty;
}

public class SwishAccountPayload
{
    public string MobileNr { get; set; } = string.Empty;
    public string HolderName { get; set; } = string.Empty;
}

public class USPostalMoneyOrderAccountPayload
{
    public string PostalAddress { get; set; } = string.Empty;
    public string HolderName { get; set; } = string.Empty;
}

public class F2FAccountPayload
{
    public string Contact { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ExtraInfo { get; set; } = string.Empty;
}

public class IfscBasedAccountPayload
{
    public string HolderName { get; set; } = string.Empty;
    public string AccountNr { get; set; } = string.Empty;
    public string Ifsc { get; set; } = string.Empty;
    public object Payload { get; set; } = string.Empty;
}

public class NeftAccountPayload { }

public class RtgsAccountPayload { }

public class ImpsAccountPayload { }

public class UpiAccountPayload
{
    public string VirtualPaymentAddress { get; set; } = string.Empty;
}

public class PaytmAccountPayload
{
    public string EmailOrMobileNr { get; set; } = string.Empty;
}

public class PayByMailAccountPayload
{
    public string PostalAddress { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public string ExtraInfo { get; set; } = string.Empty;
}

public class CashAtAtmAccountPayload
{
    public string ExtraInfo { get; set; } = string.Empty;
}

public class PromptPayAccountPayload
{
    public string PromptPayId { get; set; } = string.Empty;
}

public class AdvancedCashAccountPayload
{
    public string AccountNr { get; set; } = string.Empty;
}

public class TransferwiseAccountPayload
{
    public string Email { get; set; } = string.Empty;
}

public class TransferwiseUsdAccountPayload
{
    public string Email { get; set; } = string.Empty;
    public string HolderName { get; set; } = string.Empty;
    public string BeneficiaryAddress { get; set; } = string.Empty;
}

public class PayseraAccountPayload
{
    public string Email { get; set; } = string.Empty;
}

public class PaxumAccountPayload
{
    public string Email { get; set; } = string.Empty;
}

public class CapitualAccountPayload
{
    public string AccountNr { get; set; } = string.Empty;
}

public class CelPayAccountPayload
{
    public string Email { get; set; } = string.Empty;
}

public class NequiAccountPayload
{
    public string MobileNr { get; set; } = string.Empty;
}

public class BizumAccountPayload
{
    public string MobileNr { get; set; } = string.Empty;
}

public class PixAccountPayload
{
    public string PixKey { get; set; } = string.Empty;
}

public class MoneseAccountPayload
{
    public string MobileNr { get; set; } = string.Empty;
    public string HolderName { get; set; } = string.Empty;
}

public class SatispayAccountPayload
{
    public string MobileNr { get; set; } = string.Empty;
    public string HolderName { get; set; } = string.Empty;
}

public class StrikeAccountPayload
{
    public string HolderName { get; set; } = string.Empty;
}

public class TikkieAccountPayload
{
    public string Iban { get; set; } = string.Empty;
}

public class VerseAccountPayload
{
    public string HolderName { get; set; } = string.Empty;
}

public class SwiftAccountPayload
{
    public string BeneficiaryName { get; set; } = string.Empty;
    public string BeneficiaryAccountNr { get; set; } = string.Empty;
    public string BeneficiaryAddress { get; set; } = string.Empty;
    public string BeneficiaryCity { get; set; } = string.Empty;
    public string BeneficiaryPhone { get; set; } = string.Empty;
    public string SpecialInstructions { get; set; } = string.Empty;
    public string BankSwiftCode { get; set; } = string.Empty;
    public string BankCountryCode { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;
    public string BankBranch { get; set; } = string.Empty;
    public string BankAddress { get; set; } = string.Empty;
    public string IntermediarySwiftCode { get; set; } = string.Empty;
    public string IntermediaryCountryCode { get; set; } = string.Empty;
    public string IntermediaryName { get; set; } = string.Empty;
    public string IntermediaryBranch { get; set; } = string.Empty;
    public string IntermediaryAddress { get; set; } = string.Empty;
}

public class PaysafeAccountPayload
{
    public string Email { get; set; } = string.Empty;
}
