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
    Task<bool> IsAppInitializedAsync(CancellationToken cancellationToken = default);
    Task<bool> IsAccountOpenAsync(CancellationToken cancellationToken = default);
    Task DeleteAccountAsync(CancellationToken cancellationToken = default);
    Task OpenAccountAsync(string password, CancellationToken cancellationToken = default);
    Task<bool> AccountExistsAsync(CancellationToken cancellationToken = default);
    Task CreateAccountAsync(string password, CancellationToken cancellationToken = default);
    Task CloseAccountAsync(CancellationToken cancellationToken = default);
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

    public async Task<bool> IsAppInitializedAsync(CancellationToken cancellationToken = default)
    {
        var response = await AccountClient.IsAppInitializedAsync(new IsAppInitializedRequest(), cancellationToken: cancellationToken);
        return response.IsAppInitialized;
    }

    public async Task<bool> IsAccountOpenAsync(CancellationToken cancellationToken = default)
    {
        var response = await AccountClient.IsAccountOpenAsync(new IsAccountOpenRequest(), cancellationToken: cancellationToken);
        return response.IsAccountOpen;
    }

    public async Task DeleteAccountAsync(CancellationToken cancellationToken = default)
    {
        await AccountClient.DeleteAccountAsync(new DeleteAccountRequest(), cancellationToken: cancellationToken);
    }

    public async Task OpenAccountAsync(string password, CancellationToken cancellationToken = default)
    {
        await AccountClient.OpenAccountAsync(new OpenAccountRequest { Password = password }, cancellationToken: cancellationToken);
    }

    public async Task<bool> AccountExistsAsync(CancellationToken cancellationToken = default)
    {
        var response = await AccountClient.AccountExistsAsync(new AccountExistsRequest(), cancellationToken: cancellationToken);
        return response.AccountExists;
    }

    public async Task CreateAccountAsync(string password, CancellationToken cancellationToken = default)
    {
        await AccountClient.CreateAccountAsync(new CreateAccountRequest { Password = password }, cancellationToken: cancellationToken);
    }

    public async Task CloseAccountAsync(CancellationToken cancellationToken = default)
    {
        await AccountClient.CloseAccountAsync(new CloseAccountRequest(), cancellationToken: cancellationToken);
    }
}
