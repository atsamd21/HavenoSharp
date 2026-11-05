namespace HavenoSharp.Models;

public class Alert
{
    public string Message { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public bool IsUpdateInfo { get; set; }
    public string SignatureAsBase64 { get; set; } = string.Empty;
    public byte[] OwnerPubKeyBytes { get; set; } = [];
    public Dictionary<string, string> ExtraData { get; set; } = [];
    public bool IsPreReleaseInfo { get; set; }
}
