using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using Mapster;
using static Haveno.Proto.Grpc.Price;

namespace HavenoSharp.Services;

public interface IHavenoPriceService
{
    Task<Models.MarketDepthInfo> GetMarketPriceAsync(Models.Requests.MarketDepthRequest marketDepthRequest, CancellationToken cancellationToken = default);
    Task<double> GetMarketPriceAsync(Models.Requests.MarketPriceRequest marketPriceRequest, CancellationToken cancellationToken = default);
    Task<List<Models.MarketPriceInfo>> GetMarketPricesAsync(CancellationToken cancellationToken = default);
}

public sealed class HavenoPriceService : IHavenoPriceService
{
    private readonly GrpcChannelSingleton _grpcChannelService;
    private PriceClient PriceClient => new(_grpcChannelService.Channel);

    public HavenoPriceService(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
    }

    public async Task<Models.MarketDepthInfo> GetMarketPriceAsync(Models.Requests.MarketDepthRequest marketDepthRequest, CancellationToken cancellationToken = default)
    {
        var response = await PriceClient.GetMarketDepthAsync(marketDepthRequest.Adapt<MarketDepthRequest>(), cancellationToken: cancellationToken);
        return response.MarketDepth.Adapt<Models.MarketDepthInfo>();
    }

    public async Task<double> GetMarketPriceAsync(Models.Requests.MarketPriceRequest marketPriceRequest, CancellationToken cancellationToken = default)
    {
        var response = await PriceClient.GetMarketPriceAsync(marketPriceRequest.Adapt<MarketPriceRequest>(), cancellationToken: cancellationToken);
        return  response.Price;
    }

    public async Task<List<Models.MarketPriceInfo>> GetMarketPricesAsync(CancellationToken cancellationToken = default)
    {
        var response = await PriceClient.GetMarketPricesAsync(new MarketPricesRequest(), cancellationToken: cancellationToken);
        return response.MarketPrice.Adapt<List<Models.MarketPriceInfo>>();
    }
}
