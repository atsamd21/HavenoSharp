using Grpc.Core;
using Haveno.Proto.Grpc;
using HavenoSharp.Extensions;
using HavenoSharp.Models;
using HavenoSharp.Services;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

using static Haveno.Proto.Grpc.Notifications;

namespace HavenoSharp.Singletons;

public sealed class HavenoNotificationSingleton
{
    private readonly GrpcChannelSingleton _grpcChannelService;
    private readonly IServiceProvider _serviceProvider;
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private CancellationTokenSource _cancellationTokenSource;
    private Task? _notificationHandlerTask;
    private DateTime _lastMessageTime = new();

    private NotificationsClient NotificationsClient => new(_grpcChannelService.Channel);
    public TaskCompletionSource<bool> IsInitialized { get; private set; } = new();
    public ConcurrentDictionary<string, Models.TradeInfo> TradeInfos { get; private set; } = [];

    public event Action<Models.NotificationMessage>? NotificationMessageReceived;

    public HavenoNotificationSingleton(GrpcChannelSingleton grpcChannelService, IServiceProvider serviceProvider)
    {
        _grpcChannelService = grpcChannelService;
        _serviceProvider = serviceProvider;
        _cancellationTokenSource = new();
    }

    public async Task StopNotificationListener()
    {
        if (!await _semaphore.WaitAsync(0, CancellationToken.None))
        {
            return;
        }

        try
        {
            if (_notificationHandlerTask is not null)
            {
                _cancellationTokenSource.Cancel();

                await _notificationHandlerTask;
                _notificationHandlerTask = null;

                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new();
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Poll once to get inital
        await PollAsync(cancellationToken);
        // Switch to the notification listenter
        await RegisterNotificationListenerAsync(cancellationToken);
    }

    public async Task PollAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var tradeService = _serviceProvider.GetRequiredService<IHavenoTradeService>();

                var trades = await tradeService.GetTradesAsync(Category.Open, cancellationToken);

                List<Models.TradeInfo> updatedTrades = [];
                foreach (var trade in trades)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;

                    TradeInfos.AddOrUpdate(trade.TradeId, trade, (key, old) =>
                    {
                        updatedTrades.Add(trade);
                        return trade;
                    });
                }

                foreach (var trade in updatedTrades)
                {
                    List<ChatMessage>? chatMessages = null;

                    try
                    {
                        chatMessages = await tradeService.GetChatMessagesAsync(trade.TradeId, cancellationToken);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        continue;
                    }

                    if (chatMessages is null)
                        continue;

                    foreach (var message in chatMessages.Where(x => x.Date.ToDateTime() > _lastMessageTime).ToList())
                    {
                        if (cancellationToken.IsCancellationRequested)
                            return;

                        var isMyMessage = message.SenderNodeAddress.HostName.Split(".")[0] != trade.TradePeerNodeAddress.Split(".")[0] && message.SenderNodeAddress.HostName.Split(".")[0] != trade.ArbitratorNodeAddress.Split(".")[0];
                        if (isMyMessage)
                            continue;

                        _lastMessageTime = message.Date.ToDateTime();

                        NotificationMessageReceived?.Invoke(message.Adapt<Models.NotificationMessage>());
                    }
                }

                return;
            }
            catch (TaskCanceledException)
            {
                return;
            }
            catch (Exception)
            {

            }
        }

        if (!IsInitialized.Task.IsCompleted)
            IsInitialized.SetResult(true);
    }

    private void Reset()
    {
        IsInitialized = new();
        _notificationHandlerTask = null;
    }

    private async Task NotificationHandler(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                using var registerResponse = NotificationsClient.RegisterNotificationListener(new RegisterNotificationListenerRequest(), cancellationToken: cancellationToken);

                while (!cancellationToken.IsCancellationRequested)
                {
                    var metadata = await registerResponse.ResponseHeadersAsync;
                    if (!IsInitialized.Task.IsCompleted)
                        IsInitialized.SetResult(true);

                    await foreach (var message in registerResponse.ResponseStream.ReadAllAsync(cancellationToken))
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            Reset();
                            return;
                        }

                        NotificationMessageReceived?.Invoke(message.Adapt<Models.NotificationMessage>());
                    }
                }

                Reset();
                return;
            }
            catch (TaskCanceledException)
            {
                Reset();
                return;
            }
            catch (RpcException rpcException)
            {
                if (rpcException.InnerException is OperationCanceledException)
                {
                    Reset();
                    return;
                }
            }
            catch (ObjectDisposedException)
            {

            }
            catch (Exception)
            {

            }

            try
            {
                await Task.Delay(500, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                Reset();
                return;
            }
        }
    }

    public async Task RegisterNotificationListenerAsync(CancellationToken cancellationToken = default)
    {
        if (!await _semaphore.WaitAsync(0, CancellationToken.None))
        {
            return; 
        }

        try
        {
            if (_notificationHandlerTask is not null)
            {
                _cancellationTokenSource.Cancel();

                await _notificationHandlerTask;
                _notificationHandlerTask = null;

                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new();
            }

            if (cancellationToken != default)
            {
                var linkedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _cancellationTokenSource.Token);
                _notificationHandlerTask = Task.Factory.StartNew(() => NotificationHandler(linkedCancellationTokenSource.Token), TaskCreationOptions.LongRunning);
            }
            else
            {
                _notificationHandlerTask = Task.Factory.StartNew(() => NotificationHandler(_cancellationTokenSource.Token), TaskCreationOptions.LongRunning);
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
