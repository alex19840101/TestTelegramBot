using System;
using Microsoft.Extensions.Logging;

namespace TestTelegramBot.Services;

public class PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger)
    : PollingServiceBase<TelegramUpdatesReceiverService>(serviceProvider, logger);