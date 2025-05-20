using Haveno.Proto.Grpc;
using Mapster;
using static Haveno.Proto.Grpc.GetTradeStatistics;

namespace HavenoSharp.Services;

public interface IHavenoTradeStatisticsService
{
    Task<List<Models.TradeStatistics>> GetTradeStatisticsAsync();
}

public sealed class HavenoTradeStatisticsService : IHavenoTradeStatisticsService, IDisposable
{
    private readonly GetTradeStatisticsClient _getTradeStatisticsClient;
    private readonly IGrpcChannelService _grpcChannelService;

    public HavenoTradeStatisticsService(IGrpcChannelService grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
        _getTradeStatisticsClient = new(_grpcChannelService.Channel);
    }

    public async Task<List<Models.TradeStatistics>> GetTradeStatisticsAsync()
    {
        var response = await _getTradeStatisticsClient.GetTradeStatisticsAsync(new GetTradeStatisticsRequest());
        return response.TradeStatistics.Adapt<List<Models.TradeStatistics>>();
    }

    public void Dispose()
    {
        _grpcChannelService.Dispose();
    }
}
