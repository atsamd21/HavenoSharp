namespace HavenoSharp.Models;

public class XmrOutgoingTransfer
{
    public string Amount { get; set; } = string.Empty;
    public int AccountIndex { get; set; }
    public List<int> SubaddressIndices { get; set; } = [];
    public List<XmrDestination> Destinations { get; set; } = [];
}
