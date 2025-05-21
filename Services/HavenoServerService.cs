using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using static Haveno.Proto.Grpc.ShutdownServer;

namespace HavenoSharp.Services;

public interface IHavenoServerService
{
    Task IsXmrNodeOnlineAsync(CancellationToken cancellationToken = default);
}

public sealed class HavenoServerService : IHavenoServerService
{
    private readonly GrpcChannelSingleton _grpcChannelService;
    private ShutdownServerClient ShutdownServerClient => new(_grpcChannelService.Channel);

    public HavenoServerService(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
    }

    public async Task IsXmrNodeOnlineAsync(CancellationToken cancellationToken = default)
    {
        await ShutdownServerClient.StopAsync(new StopRequest(), cancellationToken: cancellationToken);
    }
}
