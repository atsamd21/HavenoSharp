using Google.Protobuf;
using Grpc.Core;
using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using System.IO;
using static Haveno.Proto.Grpc.Account;

namespace HavenoSharp.Services;

public interface IHavenoAccountService
{
    Task<Stream> BackupAccountAsync(CancellationToken cancellationToken = default);
    Task<Stream> BackupAccount2Async(CancellationToken cancellationToken = default);
    Task BackupAccountToFileAsync(string path, CancellationToken cancellationToken = default);
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

        while (await backupStreamingCall.ResponseStream.MoveNext(cancellationToken))
        {
            memoryStream.Write(backupStreamingCall.ResponseStream.Current.ZipBytes.Memory.Span);
        }

        return memoryStream;
    }

    public async Task<Stream> BackupAccount2Async(CancellationToken cancellationToken = default)
    {
        using var backupStreamingCall = AccountClient.BackupAccount(new BackupAccountRequest(), cancellationToken: cancellationToken);
        var memoryStream = new MemoryStream();

        await foreach (var message in backupStreamingCall.ResponseStream.ReadAllAsync(cancellationToken))
        {
            await memoryStream.WriteAsync(message.ZipBytes.Memory, cancellationToken);
        }

        return memoryStream;
    }

    public async Task BackupAccountToFileAsync(string path, CancellationToken cancellationToken = default)
    {
        using var backupStreamingCall = AccountClient.BackupAccount(new BackupAccountRequest(), cancellationToken: cancellationToken);
        using var fileStream = File.Open(path, FileMode.Create);

        await foreach (var message in backupStreamingCall.ResponseStream.ReadAllAsync(cancellationToken))
        {
            await fileStream.WriteAsync(message.ZipBytes.Memory, cancellationToken);
        }
    }

    public async Task RestoreAccountAsync(Stream zipStream, CancellationToken cancellationToken = default)
    {
        byte[] bytes = new byte[4 * 1024 * 1024 - 4096];

        ulong totalBytesRead = 0;
        int bytesRead;
        while ((bytesRead = await zipStream.ReadAsync(bytes, cancellationToken)) != 0)
        {
            byte[] trimmedBytes = bytes.AsSpan(0, bytesRead).ToArray();

            await AccountClient.RestoreAccountAsync(
                new RestoreAccountRequest
                {
                    Offset = totalBytesRead,
                    HasMore = zipStream.Position != zipStream.Length,
                    ZipBytes = ByteString.CopyFrom(trimmedBytes),
                    TotalLength = (ulong)zipStream.Length
                },
                cancellationToken: cancellationToken);

            totalBytesRead += (ulong)bytesRead;
        }
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
