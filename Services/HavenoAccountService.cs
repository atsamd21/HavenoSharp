using Google.Protobuf;
using Grpc.Core;
using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using static Haveno.Proto.Grpc.Account;

namespace HavenoSharp.Services;

public interface IHavenoAccountService
{
    Task<Stream> BackupAccountAsync();
    Task RestoreAccountAsync(Stream zipStream);
}

public sealed class HavenoAccountService : IHavenoAccountService, IDisposable
{
    private readonly AccountClient _accountClient;
    private readonly IGrpcChannelService _grpcChannelService;

    public HavenoAccountService(IGrpcChannelService grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
        _accountClient = new(_grpcChannelService.Channel);
    }

    public async Task<Stream> BackupAccountAsync()
    {
        using var backupStreamingCall = _accountClient.BackupAccount(new BackupAccountRequest());
        var memoryStream = new MemoryStream();

        while (await backupStreamingCall.ResponseStream.MoveNext())
        {
            memoryStream.Write(backupStreamingCall.ResponseStream.Current.ZipBytes.Memory.Span);
        }

        return memoryStream;
    }

    public async Task RestoreAccountAsync(Stream zipStream)
    {
        var zipBytes = await ByteString.FromStreamAsync(zipStream);

        await _accountClient.RestoreAccountAsync(new RestoreAccountRequest 
        { 
            Offset = 0, 
            HasMore = false, 
            ZipBytes = zipBytes, 
            TotalLength = (ulong)zipBytes.Length 
        });
    }

    public void Dispose()
    {
        _grpcChannelService.Dispose();
    }
}
