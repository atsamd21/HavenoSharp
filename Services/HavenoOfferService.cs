using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using Mapster;
using static Haveno.Proto.Grpc.Offers;

namespace HavenoSharp.Services;

public interface IHavenoOfferService
{
    Task<Models.OfferInfo> PostOfferAsync(Models.Requests.PostOfferRequest postOfferRequest);
    Task<Models.OfferInfo> GetMyOfferAsync(string offerId);
    Task<List<Models.OfferInfo>> GetMyOffersAsync(string currencyCode, string direction);
    Task CancelOfferAsync(string offerId);
    Task<Models.OfferInfo> GetOfferAsync(string offerId);
    Task<List<Models.OfferInfo>> GetOffersAsync(string currencyCode, string direction);
}

public sealed class HavenoOfferService : IHavenoOfferService, IDisposable
{
    private readonly OffersClient _offersClient;
    private readonly IGrpcChannelService _grpcChannelService;

    public HavenoOfferService(IGrpcChannelService grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
        _offersClient = new(_grpcChannelService.Channel);
    }

    public async Task<Models.OfferInfo> PostOfferAsync(Models.Requests.PostOfferRequest postOfferRequest)
    {
        var response = await _offersClient.PostOfferAsync(postOfferRequest.Adapt<PostOfferRequest>());
        return response.Offer.Adapt<Models.OfferInfo>();
    }

    public async Task<Models.OfferInfo> GetMyOfferAsync(string offerId)
    {
        var response = await _offersClient.GetMyOfferAsync(new GetMyOfferRequest { Id = offerId });
        return response.Offer.Adapt<Models.OfferInfo>();
    }

    public async Task<List<Models.OfferInfo>> GetMyOffersAsync(string currencyCode, string direction)
    {
        var response = await _offersClient.GetMyOffersAsync(new GetMyOffersRequest() { CurrencyCode = currencyCode, Direction = direction });
        return response.Offers.Adapt<List<Models.OfferInfo>>();
    }

    public async Task CancelOfferAsync(string offerId)
    {
        await _offersClient.CancelOfferAsync(new CancelOfferRequest { Id = offerId });
    }

    public async Task<Models.OfferInfo> GetOfferAsync(string offerId)
    {
        var response = await _offersClient.GetOfferAsync(new GetOfferRequest { Id = offerId });
        return response.Offer.Adapt<Models.OfferInfo>();
    }

    public async Task<List<Models.OfferInfo>> GetOffersAsync(string currencyCode, string direction)
    {
        var response = await _offersClient.GetOffersAsync(new GetOffersRequest { CurrencyCode = currencyCode, Direction = direction });
        return response.Offers.Adapt<List<Models.OfferInfo>>();
    }

    public void Dispose()
    {
        _grpcChannelService.Dispose();
    }
}
