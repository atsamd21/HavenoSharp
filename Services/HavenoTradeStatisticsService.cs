using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using Mapster;
using static Haveno.Proto.Grpc.GetTradeStatistics;

namespace HavenoSharp.Services;

public interface IHavenoTradeStatisticsService
{
    Task<List<Models.TradeStatistic>> GetTradeStatisticsAsync(CancellationToken cancellationToken = default);
}

public sealed class HavenoTradeStatisticsService : IHavenoTradeStatisticsService
{
    private readonly GrpcChannelSingleton _grpcChannelService;
    private GetTradeStatisticsClient GetTradeStatisticsClient => new(_grpcChannelService.Channel);

    public HavenoTradeStatisticsService(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
    }

    public async Task<List<Models.TradeStatistic>> GetTradeStatisticsAsync(CancellationToken cancellationToken = default)
    {
        var response = await GetTradeStatisticsClient.GetTradeStatisticsAsync(new GetTradeStatisticsRequest(), cancellationToken: cancellationToken);
        return response.TradeStatistics.Adapt<List<Models.TradeStatistic>>();
    }
}
