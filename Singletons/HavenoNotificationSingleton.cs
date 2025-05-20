using Grpc.Core;
using Haveno.Proto.Grpc;
using Mapster;
using static Haveno.Proto.Grpc.Notifications;

namespace HavenoSharp.Singletons;

public sealed class HavenoNotificationSingleton
{
    private readonly GrpcChannelSingleton _grpcChannelService;

    private readonly SemaphoreSlim _semaphore = new(1, 1);

    private NotificationsClient _notificationsClient;

    private CancellationTokenSource _cancellationTokenSource;
    private Task? _notificationHandlerTask;

    public TaskCompletionSource<bool> IsInitialized { get; private set; } = new();

    public event Action<Models.NotificationMessage>? NotificationMessageReceived;

    public HavenoNotificationSingleton(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
        _notificationsClient = new(_grpcChannelService.Channel);
        _cancellationTokenSource = new();
    }

    private async Task NotificationHandler(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var registerResponse = _notificationsClient.RegisterNotificationListener(new RegisterNotificationListenerRequest(), cancellationToken: cancellationToken);
                if (!IsInitialized.Task.IsCompleted)
                    IsInitialized.SetResult(true);

                while (!cancellationToken.IsCancellationRequested)
                {
                    var metadata = await registerResponse.ResponseHeadersAsync;

                    await foreach (var message in registerResponse.ResponseStream.ReadAllAsync(cancellationToken))
                    {
                        NotificationMessageReceived?.Invoke(message.Adapt<Models.NotificationMessage>());
                    }
                }
            }
            catch (TaskCanceledException)
            {
                IsInitialized = new();
                _notificationHandlerTask = null;
                return;
            }
            catch (RpcException)
            {

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
                IsInitialized = new();
                _notificationHandlerTask = null;
                return;
            }
        }
    }

    public async Task RegisterNotificationListener(CancellationToken cancellationToken = default)
    {
        if (!await _semaphore.WaitAsync(0))
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
                _notificationHandlerTask = NotificationHandler(linkedCancellationTokenSource.Token);
            }
            else
            {
                _notificationHandlerTask = NotificationHandler(_cancellationTokenSource.Token);
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
