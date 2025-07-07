using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestTelegramBot.Interfaces;

namespace TestTelegramBot.Controllers
{
    /// <summary> Контроллер для тестовой отправки уведомлений </summary>
    [ApiController]
    [Asp.Versioning.ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class TestTelegramBotController : ControllerBase
    {
        private readonly ILogger<TestTelegramBotController> _logger;
        private readonly ITelegramNotificationService _telegramNotificationService;

        /// <summary> Конструктор контроллера для тестовой отправки уведомлений </summary>
        public TestTelegramBotController(
            ITelegramNotificationService telegramNotificationService,
            ILogger<TestTelegramBotController> logger)
        {
            _telegramNotificationService = telegramNotificationService;
            _logger = logger;
        }

        
        /// <summary> Отправка сообщения </summary>
        /// <param name="telegramChatId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task SendMessage(
            [FromQuery] long telegramChatId,
            [FromBody] string message)
        {
            await _telegramNotificationService.SendMessage(
                chatId: new Telegram.Bot.Types.ChatId(telegramChatId),
                text: message);
        }
    }
}
