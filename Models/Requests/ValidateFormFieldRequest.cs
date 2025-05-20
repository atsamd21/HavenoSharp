namespace HavenoSharp.Models.Requests;

public class ValidateFormFieldRequest
{
    public PaymentAccountForm Form { get; set; } = new();
    public FieldId FieldId { get; set; }
    public string Value { get; set; } = string.Empty;
}