using DevQuiz.TelegramBot.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.TelegramBot.MediatR.Handlers
{
    /// <summary>
    ///     Base handler of commands for telegram bot
    /// </summary>
    public class BaseBotCommandHandler
    {
        /// <summary>
        ///     Logger
        /// </summary>
        protected readonly ILogger _logger;
        
        /// <summary>
        ///     Bot service
        /// </summary>
        protected readonly IBotService _botService;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="logger"> Logger </param>
        /// <param name="botService"> Bot service </param>
        public BaseBotCommandHandler(ILogger logger, IBotService botService)
        {
            _logger = logger ?? NullLogger<BaseBotCommandHandler>.Instance;
            _botService = botService;
        }
    }
}