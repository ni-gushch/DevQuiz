using Microsoft.Extensions.Logging;
using DevQuiz.TelegramBot.Interfaces;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using DevQuiz.Libraries.Core.Services;
using DevQuiz.Libraries.Services.Dto;
using System;
using System.Linq;
using MediatR;
using DevQuiz.TelegramBot.MediatR.Commands;

namespace DevQuiz.TelegramBot.Services
{
    /// <inheritdoc cref="IBotMessageService" />
    public class BotMessageService : IBotMessageService
    {
        //private readonly IBotService _botService;
        //private readonly IUserService<UserDto, Guid> _userService;
        private readonly IMediator _mediator;
        private readonly ILogger<BotMessageService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="botService">Instance of bot service</param>
        /// <param name="userService">Instance of user service</param>
        /// <param name="logger">Logger</param>
        public BotMessageService(
            //IBotService botService,
            //IUserService<UserDto, Guid> userService,
            IMediator mediator,
            ILogger<BotMessageService> logger = null)
        {
            //_botService = botService;
            //_userService = userService;
            _mediator = mediator;
            _logger = logger ?? NullLogger<BotMessageService>.Instance;
        }

        /// <inheritdoc cref="IBotMessageService.ProcessUpdateAsync(Update)" />
        public async Task ProcessUpdateAsync(Update update)
        {            
            if (update.Type != UpdateType.Message)
                return;

            var message = update.Message;

            _logger.LogInformation("Received Message from {0}", message.Chat.Id);

            var firstEntityType = message
                .Entities
                .FirstOrDefault()?
                .Type;            

            if (firstEntityType == MessageEntityType.BotCommand)
            {
                switch (message.Text)
                {
                    case Constants.BotCommands.Start:
                        await _mediator.Send(new StartCommand(message));
                        break;
                    default:
                        break;
                }
                    
            }

            //switch (message.Type)
            //{
            //    case MessageType.Text:
            //        // Echo each Message
            //        await _botService.Client.SendTextMessageAsync(message.Chat.Id, message.Text);
            //        break;
            //}
        }       
    }
}