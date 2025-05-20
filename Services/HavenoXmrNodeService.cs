using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using static Haveno.Proto.Grpc.XmrNode;

namespace HavenoSharp.Services;

public interface IHavenoXmrNodeService
{
    Task<bool> IsXmrNodeOnlineAsync();
}

public sealed class HavenoXmrNodeService : IHavenoXmrNodeService
{
    private readonly XmrNodeClient _xmrNodeClient;
    private readonly GrpcChannelSingleton _grpcChannelService;

    public HavenoXmrNodeService(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
        _xmrNodeClient = new(_grpcChannelService.Channel);
    }

    public async Task<bool> IsXmrNodeOnlineAsync()
    {
        var response = await _xmrNodeClient.IsXmrNodeOnlineAsync(new IsXmrNodeOnlineRequest());
        return response.IsRunning;
    }
}
