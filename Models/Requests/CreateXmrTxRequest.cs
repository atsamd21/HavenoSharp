namespace HavenoSharp.Models.Requests;

public class CreateXmrTxRequest
{
    public List<XmrDestination> Destinations { get; set; } = [];
}
