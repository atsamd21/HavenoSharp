using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using static Haveno.Proto.Grpc.XmrNode;

namespace HavenoSharp.Services;

public interface IHavenoXmrNodeService
{
    Task<bool> IsXmrNodeOnlineAsync();
}

public sealed class HavenoXmrNodeService : IHavenoXmrNodeService, IDisposable
{
    private readonly XmrNodeClient _xmrNodeClient;
    private readonly IGrpcChannelService _grpcChannelService;

    public HavenoXmrNodeService(IGrpcChannelService grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
        _xmrNodeClient = new(_grpcChannelService.Channel);
    }

    public async Task<bool> IsXmrNodeOnlineAsync()
    {
        var response = await _xmrNodeClient.IsXmrNodeOnlineAsync(new IsXmrNodeOnlineRequest());
        return response.IsRunning;
    }

    public void Dispose()
    {
        _grpcChannelService.Dispose();
    }
}
