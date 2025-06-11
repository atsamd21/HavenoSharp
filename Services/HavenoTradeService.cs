using Haveno.Proto.Grpc;
using HavenoSharp.Models.Responses;
using HavenoSharp.Singletons;
using Mapster;
using static Haveno.Proto.Grpc.Trades;

namespace HavenoSharp.Services;

public interface IHavenoTradeService
{
    Task<TakeOfferResponse> TakeOfferAsync(Models.Requests.TakeOfferRequest takeOfferRequest, CancellationToken cancellationToken = default);
    Task SendChatMessageAsync(string tradeId, string message, CancellationToken cancellationToken = default);
    Task<List<Models.ChatMessage>> GetChatMessagesAsync(string tradeId, CancellationToken cancellationToken = default);
    Task<Models.TradeInfo> GetTradeAsync(string tradeId, CancellationToken cancellationToken = default);
    Task<List<Models.TradeInfo>> GetTradesAsync(Models.Category category, CancellationToken cancellationToken = default);
    Task CompleteTradeAsync(string tradeId, CancellationToken cancellationToken = default);
    Task ConfirmPaymentReceivedAsync(string tradeId, CancellationToken cancellationToken = default);
    Task ConfirmPaymentSentAsync(string tradeId, CancellationToken cancellationToken = default);
}

public sealed class HavenoTradeService : IHavenoTradeService
{
    private readonly GrpcChannelSingleton _grpcChannelService;
    private TradesClient TradesClient => new(_grpcChannelService.Channel);

    public HavenoTradeService(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
    }

    public async Task<TakeOfferResponse> TakeOfferAsync(Models.Requests.TakeOfferRequest takeOfferRequest, CancellationToken cancellationToken = default)
    {
        var response = await TradesClient.TakeOfferAsync(new TakeOfferRequest 
        { 
            Amount = takeOfferRequest.Amount,
            PaymentAccountId = takeOfferRequest.PaymentAccountId,
            Challenge = takeOfferRequest.Challenge,
            OfferId = takeOfferRequest.OfferId,
        }, cancellationToken: cancellationToken);

        return response.Adapt<TakeOfferResponse>();
    }

    public async Task SendChatMessageAsync(string tradeId, string message, CancellationToken cancellationToken = default)
    {
        await TradesClient.SendChatMessageAsync(new SendChatMessageRequest { TradeId = tradeId, Message = message }, cancellationToken: cancellationToken);
    }

    public async Task<List<Models.ChatMessage>> GetChatMessagesAsync(string tradeId, CancellationToken cancellationToken = default)
    {
        var response = await TradesClient.GetChatMessagesAsync(new GetChatMessagesRequest { TradeId = tradeId }, cancellationToken: cancellationToken);
        return response.Message.Adapt<List<Models.ChatMessage>>();
    }

    public async Task<Models.TradeInfo> GetTradeAsync(string tradeId, CancellationToken cancellationToken = default)
    {
        var response = await TradesClient.GetTradeAsync(new GetTradeRequest { TradeId = tradeId }, cancellationToken: cancellationToken);
        return response.Trade.Adapt<Models.TradeInfo>();
    }

    public async Task<List<Models.TradeInfo>> GetTradesAsync(Models.Category category, CancellationToken cancellationToken = default)
    {
        var response = await TradesClient.GetTradesAsync(new GetTradesRequest { Category = (GetTradesRequest.Types.Category)category }, cancellationToken: cancellationToken);
        return response.Trades.Adapt<List<Models.TradeInfo>>();
    }

    public async Task CompleteTradeAsync(string tradeId, CancellationToken cancellationToken = default)
    {
        await TradesClient.CompleteTradeAsync(new CompleteTradeRequest { TradeId = tradeId }, cancellationToken: cancellationToken);
    }

    public async Task ConfirmPaymentReceivedAsync(string tradeId, CancellationToken cancellationToken = default)
    {
        await TradesClient.ConfirmPaymentReceivedAsync(new ConfirmPaymentReceivedRequest { TradeId = tradeId }, cancellationToken: cancellationToken);
    }

    public async Task ConfirmPaymentSentAsync(string tradeId, CancellationToken cancellationToken = default)
    {
       await TradesClient.ConfirmPaymentSentAsync(new ConfirmPaymentSentRequest { TradeId = tradeId }, cancellationToken: cancellationToken);
    }
}
