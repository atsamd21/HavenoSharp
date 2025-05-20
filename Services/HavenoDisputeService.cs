using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using Mapster;
using static Haveno.Proto.Grpc.Disputes;

namespace HavenoSharp.Services;

public interface IHavenoDisputeService
{
    Task<List<Models.Dispute>> GetDisputesAsync();
    Task<Models.Dispute> GetDisputeAsync(string tradeId);
    Task OpenDisputeAsync(string tradeId);
    Task SendDisputeChatMessageAsync(Models.Requests.SendDisputeChatMessageRequest request);
}

public sealed class HavenoDisputeService : IHavenoDisputeService
{
    private readonly DisputesClient _disputesClient;
    private readonly GrpcChannelSingleton _grpcChannelService;

    public HavenoDisputeService(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
        _disputesClient = new(_grpcChannelService.Channel);
    }

    public async Task<List<Models.Dispute>> GetDisputesAsync()
    {
        var response = await _disputesClient.GetDisputesAsync(new GetDisputesRequest());
        return response.Disputes.Adapt<List<Models.Dispute>>();
    }

    public async Task<Models.Dispute> GetDisputeAsync(string tradeId)
    {
        var response = await _disputesClient.GetDisputeAsync(new GetDisputeRequest { TradeId = tradeId });
        return response.Dispute.Adapt<Models.Dispute>();
    }

    public async Task OpenDisputeAsync(string tradeId)
    {
        await _disputesClient.OpenDisputeAsync(new OpenDisputeRequest { TradeId = tradeId });
    }

    public async Task SendDisputeChatMessageAsync(Models.Requests.SendDisputeChatMessageRequest request)
    {
        await _disputesClient.SendDisputeChatMessageAsync(request.Adapt<SendDisputeChatMessageRequest>());
    }
}
