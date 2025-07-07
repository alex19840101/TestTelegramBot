using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestTelegramBot.Interfaces;

namespace TestTelegramBot.Services;

/// <summary> Сервис для опроса Telegram Bot API </summary>
public abstract class PollingServiceBase<TReceiverService> : BackgroundService
    where TReceiverService : ITelegramUpdatesReceiverService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<PollingServiceBase<TReceiverService>> _logger;

    /// <summary> Конструктор сервиса для опроса Telegram Bot API </summary>
    public PollingServiceBase(
        IServiceProvider serviceProvider,
        ILogger<PollingServiceBase<TReceiverService>> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting polling service");

        await DoWork(cancellationToken);
    }

    private async Task DoWork(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await using var scope = _serviceProvider.CreateAsyncScope();
                var receiver = scope.ServiceProvider.GetRequiredService<TReceiverService>();

                await receiver.ReceiveUpdates(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("ReceiveUpdates exception: {Exception}", ex);

                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
        }
    }
}