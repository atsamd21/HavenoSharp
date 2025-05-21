using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using static Haveno.Proto.Grpc.XmrNode;

namespace HavenoSharp.Services;

public interface IHavenoXmrNodeService
{
    Task<bool> IsXmrNodeOnlineAsync(CancellationToken cancellationToken = default);
}

public sealed class HavenoXmrNodeService : IHavenoXmrNodeService
{
    private readonly GrpcChannelSingleton _grpcChannelService;
    private XmrNodeClient XmrNodeClient => new(_grpcChannelService.Channel);

    public HavenoXmrNodeService(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
    }

    public async Task<bool> IsXmrNodeOnlineAsync(CancellationToken cancellationToken = default)
    {
        var response = await XmrNodeClient.IsXmrNodeOnlineAsync(new IsXmrNodeOnlineRequest(), cancellationToken: cancellationToken);
        return response.IsRunning;
    }
}
