using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using Mapster;
using static Haveno.Proto.Grpc.Disputes;

namespace HavenoSharp.Services;

public interface IHavenoDisputeService
{
    Task<List<Models.Dispute>> GetDisputesAsync(CancellationToken cancellationToken = default);
    Task<Models.Dispute> GetDisputeAsync(string tradeId, CancellationToken cancellationToken = default);
    Task OpenDisputeAsync(string tradeId, CancellationToken cancellationToken = default);
    Task SendDisputeChatMessageAsync(Models.Requests.SendDisputeChatMessageRequest request, CancellationToken cancellationToken = default);
}

public sealed class HavenoDisputeService : IHavenoDisputeService
{
    private readonly GrpcChannelSingleton _grpcChannelService;
    private DisputesClient DisputesClient => new(_grpcChannelService.Channel);

    public HavenoDisputeService(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
    }

    public async Task<List<Models.Dispute>> GetDisputesAsync(CancellationToken cancellationToken = default)
    {
        var response = await DisputesClient.GetDisputesAsync(new GetDisputesRequest(), cancellationToken: cancellationToken);
        return response.Disputes.Adapt<List<Models.Dispute>>();
    }

    public async Task<Models.Dispute> GetDisputeAsync(string tradeId, CancellationToken cancellationToken = default)
    {
        var response = await DisputesClient.GetDisputeAsync(new GetDisputeRequest { TradeId = tradeId }, cancellationToken: cancellationToken);
        return response.Dispute.Adapt<Models.Dispute>();
    }

    public async Task OpenDisputeAsync(string tradeId, CancellationToken cancellationToken = default)
    {
        await DisputesClient.OpenDisputeAsync(new OpenDisputeRequest { TradeId = tradeId }, cancellationToken: cancellationToken);
    }

    public async Task SendDisputeChatMessageAsync(Models.Requests.SendDisputeChatMessageRequest request, CancellationToken cancellationToken = default)
    {
        await DisputesClient.SendDisputeChatMessageAsync(request.Adapt<SendDisputeChatMessageRequest>(), cancellationToken: cancellationToken);
    }
}
