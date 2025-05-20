using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using Mapster;
using static Haveno.Proto.Grpc.Price;

namespace HavenoSharp.Services;

public interface IHavenoPriceService
{
    Task<Models.MarketDepthInfo> GetMarketPriceAsync(Models.Requests.MarketDepthRequest marketDepthRequest);
    Task<double> GetMarketPriceAsync(Models.Requests.MarketPriceRequest marketPriceRequest);
    Task<List<Models.MarketPriceInfo>> GetMarketPricesAsync();
}

public sealed class HavenoPriceService : IHavenoPriceService, IDisposable
{
    private readonly PriceClient _priceClient;
    private readonly IGrpcChannelService _grpcChannelService;

    public HavenoPriceService(IGrpcChannelService grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
        _priceClient = new(_grpcChannelService.Channel);
    }

    public async Task<Models.MarketDepthInfo> GetMarketPriceAsync(Models.Requests.MarketDepthRequest marketDepthRequest)
    {
        var response = await _priceClient.GetMarketDepthAsync(marketDepthRequest.Adapt<MarketDepthRequest>());
        return response.MarketDepth.Adapt<Models.MarketDepthInfo>();
    }

    public async Task<double> GetMarketPriceAsync(Models.Requests.MarketPriceRequest marketPriceRequest)
    {
        var response = await _priceClient.GetMarketPriceAsync(marketPriceRequest.Adapt<MarketPriceRequest>());
        return  response.Price;
    }

    public async Task<List<Models.MarketPriceInfo>> GetMarketPricesAsync()
    {
        var response = await _priceClient.GetMarketPricesAsync(new MarketPricesRequest());
        return response.MarketPrice.Adapt<List<Models.MarketPriceInfo>>();
    }

    public void Dispose()
    {
        _grpcChannelService.Dispose();
    }
}
