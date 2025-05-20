using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using static Haveno.Proto.Grpc.ShutdownServer;

namespace HavenoSharp.Services;

public interface IHavenoServerService
{
    Task IsXmrNodeOnlineAsync();
}

public sealed class HavenoServerService : IHavenoServerService
{
    private readonly ShutdownServerClient _shutdownServerClient;
    private readonly GrpcChannelSingleton _grpcChannelService;

    public HavenoServerService(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
        _shutdownServerClient = new(_grpcChannelService.Channel);
    }

    public async Task IsXmrNodeOnlineAsync()
    {
        await _shutdownServerClient.StopAsync(new StopRequest());
    }
}
