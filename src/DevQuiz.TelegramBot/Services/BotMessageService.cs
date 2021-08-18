using System.Linq;
using System.Threading.Tasks;
using DevQuiz.TelegramBot.Constants;
using DevQuiz.TelegramBot.Interfaces;
using DevQuiz.TelegramBot.MediatR.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DevQuiz.TelegramBot.Services
{
    /// <inheritdoc cref="IBotMessageService" />
    public class BotMessageService : IBotMessageService
    {
        private readonly ILogger<BotMessageService> _logger;
        private readonly IMediator _mediator;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="mediator"> Mediator</param>
        /// <param name="logger"> Logge r</param>
        public BotMessageService(
            //IBotService botService,
            //IUserService<UserDto, Guid> userService,
            IMediator mediator,
            ILogger<BotMessageService> logger = null)
        {
            _mediator = mediator;
            _logger = logger ?? NullLogger<BotMessageService>.Instance;
        }

        /// <inheritdoc cref="IBotMessageService.ProcessUpdateAsync(Update)" />
        public async Task ProcessUpdateAsync(Update update)
        {
#if !(DEBUG)
            if (update.Type != UpdateType.Message)
                return;
#endif
            var message = update.Message;

            _logger.LogInformation("Received Message from {0}", message.Chat.Id);

            var firstEntityType = message
                .Entities
                .FirstOrDefault()?
                .Type;

            if (firstEntityType == MessageEntityType.BotCommand)
                switch (message.Text)
                {
                    case BotCommands.Start:
                        await _mediator.Send(new StartCommand(message));
                        break;
                    default:
                        _logger.LogInformation("Unknown bot command \"{0}\"", message.Text);
                        break;
                }
        }
    }
}