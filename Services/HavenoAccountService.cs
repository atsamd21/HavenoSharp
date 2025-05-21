using Google.Protobuf;
using Grpc.Core;
using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using static Haveno.Proto.Grpc.Account;

namespace HavenoSharp.Services;

public interface IHavenoAccountService
{
    Task<Stream> BackupAccountAsync(CancellationToken cancellationToken = default);
    Task RestoreAccountAsync(Stream zipStream, CancellationToken cancellationToken = default);
}

public sealed class HavenoAccountService : IHavenoAccountService
{
    private readonly GrpcChannelSingleton _grpcChannelService;
    private AccountClient AccountClient => new(_grpcChannelService.Channel);

    public HavenoAccountService(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
    }

    public async Task<Stream> BackupAccountAsync(CancellationToken cancellationToken = default)
    {
        using var backupStreamingCall = AccountClient.BackupAccount(new BackupAccountRequest(), cancellationToken: cancellationToken);
        var memoryStream = new MemoryStream();

        while (await backupStreamingCall.ResponseStream.MoveNext())
        {
            memoryStream.Write(backupStreamingCall.ResponseStream.Current.ZipBytes.Memory.Span);
        }

        return memoryStream;
    }

    public async Task RestoreAccountAsync(Stream zipStream, CancellationToken cancellationToken = default)
    {
        var zipBytes = await ByteString.FromStreamAsync(zipStream, cancellationToken: cancellationToken);

        await AccountClient.RestoreAccountAsync(new RestoreAccountRequest 
        { 
            Offset = 0, 
            HasMore = false, 
            ZipBytes = zipBytes, 
            TotalLength = (ulong)zipBytes.Length 
        });
    }
}
