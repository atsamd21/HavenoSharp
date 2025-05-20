using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using static Haveno.Proto.Grpc.GetVersion;

namespace HavenoSharp.Services;

public interface IHavenoVersionService
{
    Task<string> GetVersionAsync();
}

public sealed class HavenoVersionService : IHavenoVersionService
{
    private readonly GetVersionClient _getVersionClient;
    private readonly GrpcChannelSingleton _grpcChannelService;

    public HavenoVersionService(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
        _getVersionClient = new(_grpcChannelService.Channel);
    }

    public async Task<string> GetVersionAsync()
    {
        var response = await _getVersionClient.GetVersionAsync(new GetVersionRequest());
        return response.Version;
    }
}
