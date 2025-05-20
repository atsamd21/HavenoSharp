using Haveno.Proto.Grpc;
using HavenoSharp.Singletons;
using Mapster;
using static Haveno.Proto.Grpc.GetTradeStatistics;

namespace HavenoSharp.Services;

public interface IHavenoTradeStatisticsService
{
    Task<List<Models.TradeStatistics>> GetTradeStatisticsAsync();
}

public sealed class HavenoTradeStatisticsService : IHavenoTradeStatisticsService
{
    private readonly GetTradeStatisticsClient _getTradeStatisticsClient;
    private readonly GrpcChannelSingleton _grpcChannelService;

    public HavenoTradeStatisticsService(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
        _getTradeStatisticsClient = new(_grpcChannelService.Channel);
    }

    public async Task<List<Models.TradeStatistics>> GetTradeStatisticsAsync()
    {
        var response = await _getTradeStatisticsClient.GetTradeStatisticsAsync(new GetTradeStatisticsRequest());
        return response.TradeStatistics.Adapt<List<Models.TradeStatistics>>();
    }
}
