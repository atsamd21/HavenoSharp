using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using HavenoSharp.Singletons;

namespace HavenoSharp.Services;

public interface IGrpcChannelService : IDisposable
{
    GrpcChannel Channel { get; }
}

// Singleton?
public sealed class GrpcChannelService : IGrpcChannelService
{
    private readonly GrpcSettingsSingleton _grpcSettingsSingleton;

    public GrpcChannel Channel { get; private set; }

    public GrpcChannelService(GrpcSettingsSingleton grpcSettingsSingleton)
    {
        _grpcSettingsSingleton = grpcSettingsSingleton;

        Channel = CreateChannel(_grpcSettingsSingleton.Host, _grpcSettingsSingleton.Password);

        _grpcSettingsSingleton.SettingsChanged += HandleSettingsChanged;
    }

    private GrpcChannel CreateChannel(string host, string password)
    {
        var httpClient = new HttpClient(new SocketsHttpHandler())
        {
            DefaultRequestVersion = new Version(2, 0),
            DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact,
            Timeout = Timeout.InfiniteTimeSpan
        };

        httpClient.DefaultRequestHeaders.Add("password", password);

        var channelOptions = new GrpcChannelOptions 
        { 
            HttpClient = httpClient,
            MaxReceiveMessageSize = null,
            DisposeHttpClient = true
        };

        return GrpcChannel.ForAddress(host, channelOptions);
    }

    private void HandleSettingsChanged(string host, string password)
    {
        // Channel not disposed here!
        Channel = CreateChannel(host, password);
    }

    public void Dispose()
    {
        _grpcSettingsSingleton.SettingsChanged -= HandleSettingsChanged;
        Channel.Dispose();
    }
}
