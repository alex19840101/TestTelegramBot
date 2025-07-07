using Microsoft.AspNetCore.Mvc;

namespace TestTelegramBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestTelegramBotController : ControllerBase
    {
        private readonly ILogger<TestTelegramBotController> _logger;

        public TestTelegramBotController(ILogger<TestTelegramBotController> logger)
        {
            _logger = logger;
        }
    }
}
