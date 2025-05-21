using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using static Haveno.Proto.Grpc.GetVersion;

namespace HavenoSharp.Services;

public interface IHavenoVersionService
{
    Task<string> GetVersionAsync(CancellationToken cancellationToken = default);
}

public sealed class HavenoVersionService : IHavenoVersionService
{
    private readonly GrpcChannelSingleton _grpcChannelService;
    private GetVersionClient GetVersionClient => new(_grpcChannelService.Channel);

    public HavenoVersionService(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
    }

    public async Task<string> GetVersionAsync(CancellationToken cancellationToken = default)
    {
        var response = await GetVersionClient.GetVersionAsync(new GetVersionRequest(), cancellationToken: cancellationToken);
        return response.Version;
    }
}
