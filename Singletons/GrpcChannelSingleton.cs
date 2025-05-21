using Grpc.Net.Client;

namespace HavenoSharp.Singletons;

public sealed class GrpcChannelSingleton
{
    private string _password = string.Empty;
    private string _host = string.Empty;

    public GrpcChannel Channel { get; private set; } = GrpcChannel.ForAddress("http://127.0.0.1");

    public GrpcChannelSingleton()
    {

    }

    public void CreateChannel(string host, string password, HttpClient? httpClient = default)
    {
        _host = host;
        _password = password;

        httpClient ??= new HttpClient(new SocketsHttpHandler())
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

        Channel = GrpcChannel.ForAddress(host, channelOptions);
    }
}
