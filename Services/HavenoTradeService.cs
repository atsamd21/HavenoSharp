using Haveno.Proto.Grpc;
using HavenoSharp.Models.Responses;
using HavenoSharp.Singletons;
using Mapster;
using static Haveno.Proto.Grpc.Trades;

namespace HavenoSharp.Services;

public interface IHavenoTradeService
{
    Task<TakeOfferResponse> TakeOfferAsync(Models.Requests.TakeOfferRequest takeOfferRequest);
    Task SendChatMessageAsync(string tradeId, string message);
    Task<List<Models.ChatMessage>> GetChatMessagesAsync(string tradeId);
    Task<Models.TradeInfo> GetTradeAsync(string tradeId);
    Task<List<Models.TradeInfo>> GetTradesAsync(Models.Category category);
    Task CompleteTradeAsync(string tradeId);
    Task ConfirmPaymentReceivedAsync(string tradeId);
    Task ConfirmPaymentSentAsync(string tradeId);
}

public sealed class HavenoTradeService : IHavenoTradeService, IDisposable
{
    private readonly TradesClient _accountClient;
    private readonly IGrpcChannelService _grpcChannelService;

    public HavenoTradeService(IGrpcChannelService grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
        _accountClient = new(_grpcChannelService.Channel);
    }

    public async Task<TakeOfferResponse> TakeOfferAsync(Models.Requests.TakeOfferRequest takeOfferRequest)
    {
        var response = await _accountClient.TakeOfferAsync(new TakeOfferRequest 
        { 
            Amount = takeOfferRequest.Amount,
            PaymentAccountId = takeOfferRequest.PaymentAccountId,
            Challenge = takeOfferRequest.Challenge,
            OfferId = takeOfferRequest.OfferId,
        });

        return response.Adapt<TakeOfferResponse>();
    }

    public async Task SendChatMessageAsync(string tradeId, string message)
    {
        await _accountClient.SendChatMessageAsync(new SendChatMessageRequest { TradeId = tradeId, Message = message });
    }

    public async Task<List<Models.ChatMessage>> GetChatMessagesAsync(string tradeId)
    {
        var response = await _accountClient.GetChatMessagesAsync(new GetChatMessagesRequest { TradeId = tradeId });
        return response.Message.Adapt<List<Models.ChatMessage>>();
    }

    public async Task<Models.TradeInfo> GetTradeAsync(string tradeId)
    {
        var response = await _accountClient.GetTradeAsync(new GetTradeRequest { TradeId = tradeId });
        return response.Trade.Adapt<Models.TradeInfo>();
    }

    public async Task<List<Models.TradeInfo>> GetTradesAsync(Models.Category category)
    {
        var response = await _accountClient.GetTradesAsync(new GetTradesRequest { Category = (GetTradesRequest.Types.Category)category });
        return response.Trades.Adapt<List<Models.TradeInfo>>();
    }

    public async Task CompleteTradeAsync(string tradeId)
    {
        await _accountClient.CompleteTradeAsync(new CompleteTradeRequest { TradeId = tradeId });
    }

    public async Task ConfirmPaymentReceivedAsync(string tradeId)
    {
        await _accountClient.ConfirmPaymentReceivedAsync(new ConfirmPaymentReceivedRequest { TradeId = tradeId });
    }

    public async Task ConfirmPaymentSentAsync(string tradeId)
    {
       await _accountClient.ConfirmPaymentSentAsync(new ConfirmPaymentSentRequest { TradeId = tradeId });
    }

    public void Dispose()
    {
        _grpcChannelService.Dispose();
    }
}
