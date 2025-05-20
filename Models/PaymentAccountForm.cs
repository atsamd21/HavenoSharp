namespace HavenoSharp.Models;

public class PaymentAccountForm
{
    public FormId Id { get; set; }
    public List<PaymentAccountFormField> Fields { get; set; } = [];
}
