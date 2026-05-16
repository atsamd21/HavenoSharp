using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using Mapster;

using static Haveno.Proto.Grpc.XmrConnections;
using static Haveno.Proto.Grpc.XmrNode;

namespace HavenoSharp.Services;

public interface IHavenoXmrNodeService
{
    Task<bool> IsXmrNodeOnlineAsync(CancellationToken cancellationToken = default);
    Task SetMoneroNodeAsync(string url, string username, string password, int priority, CancellationToken cancellationToken = default);
    Task<Models.UrlConnection> GetMoneroNodeAsync(CancellationToken cancellationToken = default);
    Task<Models.UrlConnection> CheckConnectionAsync(CancellationToken cancellationToken = default);
    Task<List<Models.UrlConnection>> GetConnectionsAsync(CancellationToken cancellationToken = default);
    Task RemoveConnectionAsync(string url, CancellationToken cancellationToken = default);
    Task SetAutoSwitchAsync(bool autoSwitch, CancellationToken cancellationToken = default);
    Task<bool> GetAutoSwitchAsync(CancellationToken cancellationToken = default);
    Task<Models.UrlConnection> GetBestConnectionAsync(CancellationToken cancellationToken = default);
    Task AddConnectionAsync(string url, string username, string password, int priority, CancellationToken cancellationToken = default);
}

public sealed class HavenoXmrNodeService : IHavenoXmrNodeService
{
    private readonly GrpcChannelSingleton _grpcChannelService;
    private XmrNodeClient XmrNodeClient => new(_grpcChannelService.Channel);
    private XmrConnectionsClient XmrConnectionsClient => new(_grpcChannelService.Channel);

    public HavenoXmrNodeService(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
    }

    public async Task<bool> IsXmrNodeOnlineAsync(CancellationToken cancellationToken = default)
    {
        var response = await XmrNodeClient.IsXmrNodeOnlineAsync(new IsXmrNodeOnlineRequest(), cancellationToken: cancellationToken);
        return response.IsRunning;
    }

    public async Task<Models.UrlConnection> GetBestConnectionAsync(CancellationToken cancellationToken = default)
    {
        var response = await XmrConnectionsClient.GetBestConnectionAsync(new GetBestConnectionRequest(), cancellationToken: cancellationToken);
        return response.Connection.Adapt<Models.UrlConnection>();
    }

    public async Task<bool> GetAutoSwitchAsync(CancellationToken cancellationToken = default)
    {
        var response = await XmrConnectionsClient.GetAutoSwitchAsync(new GetAutoSwitchRequest(), cancellationToken: cancellationToken);
        return response.AutoSwitch;
    }

    public async Task SetMoneroNodeAsync(string url, string username, string password, int priority, CancellationToken cancellationToken = default)
    {
        await XmrConnectionsClient.SetConnectionAsync(new SetConnectionRequest
        {
            Url = url,
            Connection = new UrlConnection
            {
                Priority = priority,
                Url = url,
                Username = username,
                Password = password
            }
        }, cancellationToken: cancellationToken);
    }

    public async Task<Models.UrlConnection> GetMoneroNodeAsync(CancellationToken cancellationToken = default)
    {
        var response = await XmrConnectionsClient.GetConnectionAsync(new GetConnectionRequest(), cancellationToken: cancellationToken);
        return response.Connection.Adapt<Models.UrlConnection>();
    }

    public async Task<Models.UrlConnection> CheckConnectionAsync(CancellationToken cancellationToken = default)
    {
        var response = await XmrConnectionsClient.CheckConnectionAsync(new CheckConnectionRequest(), cancellationToken: cancellationToken);
        return response.Connection.Adapt<Models.UrlConnection>();
    }

    public async Task<List<Models.UrlConnection>> GetConnectionsAsync(CancellationToken cancellationToken = default)
    {
        var response = await XmrConnectionsClient.GetConnectionsAsync(new GetConnectionsRequest(), cancellationToken: cancellationToken);
        return response.Connections.Adapt<List<Models.UrlConnection>>();
    }

    public async Task RemoveConnectionAsync(string url, CancellationToken cancellationToken = default)
    {
        await XmrConnectionsClient.RemoveConnectionAsync(new RemoveConnectionRequest() { Url = url }, cancellationToken: cancellationToken);
    }

    public async Task SetAutoSwitchAsync(bool autoSwitch, CancellationToken cancellationToken = default)
    {
        await XmrConnectionsClient.SetAutoSwitchAsync(new SetAutoSwitchRequest { AutoSwitch = autoSwitch }, cancellationToken: cancellationToken);
    }

    public async Task AddConnectionAsync(string url, string username, string password, int priority, CancellationToken cancellationToken = default)
    {
        await XmrConnectionsClient.AddConnectionAsync(new AddConnectionRequest
        {
            Connection = new UrlConnection
            {
                Priority = priority,
                Url = url,
                Username = username,
                Password = password
            }
        },
        cancellationToken: cancellationToken);
    }
}
