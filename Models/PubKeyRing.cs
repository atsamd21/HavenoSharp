namespace HavenoSharp.Models;

public class PubKeyRing
{
    public byte[] SignaturePubKeyBytes { get; set; } = [];
    public byte[] EncryptionPubKeyBytes { get; set; } = [];
}
