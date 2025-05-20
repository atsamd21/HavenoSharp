namespace HavenoSharp.Models;

public class Country
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Region Region { get; set; } = new();
}
