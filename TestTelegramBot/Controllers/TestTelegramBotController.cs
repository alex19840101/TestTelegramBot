using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
