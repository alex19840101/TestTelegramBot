using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using TestTelegramBot.Interfaces;

namespace TestTelegramBot.Services;

/// <summary> Получатель обновлений из Telegram </summary>
public class TelegramUpdatesReceiverServiceBase<TUpdateHandler> : ITelegramUpdatesReceiverService
    where TUpdateHandler : Telegram.Bot.Polling.IUpdateHandler
{
    private readonly Telegram.Bot.ITelegramBotClient _botClient;
    private readonly Telegram.Bot.Polling.IUpdateHandler _updateHandler;
    private readonly ILogger<TelegramUpdatesReceiverServiceBase<TUpdateHandler>> _logger;

    /// <summary> Конструктор получателя обновлений из Telegram </summary>
    public TelegramUpdatesReceiverServiceBase(
        Telegram.Bot.ITelegramBotClient botClient,
        Telegram.Bot.Polling.IUpdateHandler updateHandler,
        ILogger<TelegramUpdatesReceiverServiceBase<TUpdateHandler>> logger)
    {
        _botClient = botClient;
        _updateHandler = updateHandler;
        _logger = logger;
    }

    public async Task ReceiveUpdates(CancellationToken cancellationToken = default)
    {
        var receiverOptions = new Telegram.Bot.Polling.ReceiverOptions()
        {
            AllowedUpdates = [],
            DropPendingUpdates = true
        };

        var me = await _botClient.GetMe(cancellationToken);

        _logger.LogInformation("Start polling {BotName}", me.Username);

        await _botClient.ReceiveAsync(_updateHandler, receiverOptions, cancellationToken);
    }
}