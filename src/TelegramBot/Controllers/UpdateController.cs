using System.Threading.Tasks;
using DevQuiz.TelegramBot.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace DevQuiz.TelegramBot.Controllers
{
    /// <summary>
    /// Update controller
    /// </summary>
    [Route("api/[controller]")]
    public class UpdateController : Controller
    {
        private readonly IBotMessageService _botMessageService;

        public UpdateController(IBotMessageService botMeggaseService)
        {
            _botMessageService = botMeggaseService;
        }

        // POST api/update
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            await _botMessageService.ProcessMessageAsync(update);
            return Ok();
        }
    }
}