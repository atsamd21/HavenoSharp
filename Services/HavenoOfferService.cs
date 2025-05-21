using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using Mapster;
using static Haveno.Proto.Grpc.Offers;

namespace HavenoSharp.Services;

public interface IHavenoOfferService
{
    Task<Models.OfferInfo> PostOfferAsync(Models.Requests.PostOfferRequest postOfferRequest, CancellationToken cancellationToken = default);
    Task<Models.OfferInfo> GetMyOfferAsync(string offerId, CancellationToken cancellationToken = default);
    Task<List<Models.OfferInfo>> GetMyOffersAsync(string currencyCode, string direction, CancellationToken cancellationToken = default);
    Task CancelOfferAsync(string offerId, CancellationToken cancellationToken = default);
    Task<Models.OfferInfo> GetOfferAsync(string offerId, CancellationToken cancellationToken = default);
    Task<List<Models.OfferInfo>> GetOffersAsync(string currencyCode, string direction, CancellationToken cancellationToken = default);
}

public sealed class HavenoOfferService : IHavenoOfferService
{
    private readonly GrpcChannelSingleton _grpcChannelService;
    private OffersClient OffersClient => new(_grpcChannelService.Channel);

    public HavenoOfferService(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
    }

    public async Task<Models.OfferInfo> PostOfferAsync(Models.Requests.PostOfferRequest postOfferRequest, CancellationToken cancellationToken = default)
    {
        var response = await OffersClient.PostOfferAsync(postOfferRequest.Adapt<PostOfferRequest>(), cancellationToken: cancellationToken);
        return response.Offer.Adapt<Models.OfferInfo>();
    }

    public async Task<Models.OfferInfo> GetMyOfferAsync(string offerId, CancellationToken cancellationToken = default)
    {
        var response = await OffersClient.GetMyOfferAsync(new GetMyOfferRequest { Id = offerId }, cancellationToken: cancellationToken);
        return response.Offer.Adapt<Models.OfferInfo>();
    }

    public async Task<List<Models.OfferInfo>> GetMyOffersAsync(string currencyCode, string direction, CancellationToken cancellationToken = default)
    {
        var response = await OffersClient.GetMyOffersAsync(new GetMyOffersRequest() { CurrencyCode = currencyCode, Direction = direction }, cancellationToken: cancellationToken);
        return response.Offers.Adapt<List<Models.OfferInfo>>();
    }

    public async Task CancelOfferAsync(string offerId, CancellationToken cancellationToken = default)
    {
        await OffersClient.CancelOfferAsync(new CancelOfferRequest { Id = offerId }, cancellationToken: cancellationToken);
    }

    public async Task<Models.OfferInfo> GetOfferAsync(string offerId, CancellationToken cancellationToken = default)
    {
        var response = await OffersClient.GetOfferAsync(new GetOfferRequest { Id = offerId }, cancellationToken: cancellationToken);
        return response.Offer.Adapt<Models.OfferInfo>();
    }

    public async Task<List<Models.OfferInfo>> GetOffersAsync(string currencyCode, string direction, CancellationToken cancellationToken = default)
    {
        var response = await OffersClient.GetOffersAsync(new GetOffersRequest { CurrencyCode = currencyCode, Direction = direction }, cancellationToken: cancellationToken);
        return response.Offers.Adapt<List<Models.OfferInfo>>();
    }
}
