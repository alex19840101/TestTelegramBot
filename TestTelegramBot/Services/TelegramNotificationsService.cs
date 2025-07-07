using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using TestTelegramBot.Interfaces;

namespace TestTelegramBot.Services
{
    /// <summary> Нотификатор, отправляющий Telegram-уведомления </summary>
    public class TelegramNotificationsService : ITelegramNotificationService
    {
        private readonly Telegram.Bot.ITelegramBotClient _botClient;
        private readonly ILogger<TelegramNotificationsService> _logger;

        /// <summary> Конструктор нотификатора, отправляющего Telegram-уведомления </summary>
        public TelegramNotificationsService(
            Telegram.Bot.ITelegramBotClient botClient,
            ILogger<TelegramNotificationsService> logger)
        {
            _botClient = botClient;
            _logger = logger;
        }

        public async Task<Telegram.Bot.Types.Message> SendMessage(
            Telegram.Bot.Types.ChatId chatId,
            string text,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await _botClient.SendMessage(chatId, text, cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR SendMessage {ChatId}", chatId.Identifier);
                throw;
            }
        }
    }
}
