using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using Mapster;

using static Haveno.Proto.Grpc.XmrConnections;
using static Haveno.Proto.Grpc.XmrNode;

namespace HavenoSharp.Services;

public interface IHavenoXmrNodeService
{
    Task<bool> IsXmrNodeOnlineAsync(CancellationToken cancellationToken = default);
    Task SetMoneroNodeAsync(string url, string username, string password, CancellationToken cancellationToken = default);
    Task<Models.UrlConnection> GetMoneroNodeAsync(CancellationToken cancellationToken = default);
    Task<Models.UrlConnection> CheckConnectionAsync(CancellationToken cancellationToken = default);
    Task<List<Models.UrlConnection>> GetConnectionsAsync(CancellationToken cancellationToken = default);
}

public sealed class HavenoXmrNodeService : IHavenoXmrNodeService
{
    private readonly GrpcChannelSingleton _grpcChannelService;
    private XmrNodeClient XmrNodeClient => new(_grpcChannelService.Channel);
    private XmrConnectionsClient XmrConnections => new(_grpcChannelService.Channel);

    public HavenoXmrNodeService(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
    }

    public async Task<bool> IsXmrNodeOnlineAsync(CancellationToken cancellationToken = default)
    {
        var response = await XmrNodeClient.IsXmrNodeOnlineAsync(new IsXmrNodeOnlineRequest(), cancellationToken: cancellationToken);
        return response.IsRunning;
    }

    public async Task SetMoneroNodeAsync(string url, string username, string password, CancellationToken cancellationToken = default)
    {
        await XmrConnections.SetConnectionAsync(new SetConnectionRequest
        {
            Url = url,
            Connection = new UrlConnection
            {
                Priority = 1,
                Url = url,
                Username = username,
                Password = password
            }
        }, cancellationToken: cancellationToken);
    }

    public async Task<Models.UrlConnection> GetMoneroNodeAsync(CancellationToken cancellationToken = default)
    {
        var response = await XmrConnections.GetConnectionAsync(new GetConnectionRequest(), cancellationToken: cancellationToken);
        return response.Connection.Adapt<Models.UrlConnection>();
    }

    public async Task<Models.UrlConnection> CheckConnectionAsync(CancellationToken cancellationToken = default)
    {
        var response = await XmrConnections.CheckConnectionAsync(new CheckConnectionRequest(), cancellationToken: cancellationToken);
        return response.Connection.Adapt<Models.UrlConnection>();
    }

    public async Task<List<Models.UrlConnection>> GetConnectionsAsync(CancellationToken cancellationToken = default)
    {
        var response = await XmrConnections.GetConnectionsAsync(new GetConnectionsRequest(), cancellationToken: cancellationToken);
        return response.Connections.Adapt<List<Models.UrlConnection>>();
    }
}
