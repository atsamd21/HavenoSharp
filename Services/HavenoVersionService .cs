using Haveno.Proto.Grpc;
using static Haveno.Proto.Grpc.GetVersion;

namespace HavenoSharp.Services;

public interface IHavenoVersionService
{
    Task<string> GetVersionAsync();
}

public sealed class HavenoVersionService : IHavenoVersionService, IDisposable
{
    private readonly GetVersionClient _getVersionClient;
    private readonly IGrpcChannelService _grpcChannelService;

    public HavenoVersionService(IGrpcChannelService grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
        _getVersionClient = new(_grpcChannelService.Channel);
    }

    public async Task<string> GetVersionAsync()
    {
        var response = await _getVersionClient.GetVersionAsync(new GetVersionRequest());
        return response.Version;
    }

    public void Dispose()
    {
        _grpcChannelService.Dispose();
    }
}
