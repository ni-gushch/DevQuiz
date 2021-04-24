using Microsoft.Extensions.Logging;
using DevQuiz.TelegramBot.Interfaces;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using DevQuiz.Libraries.Core.Services;
using DevQuiz.Libraries.Services.Dto;
using System;

namespace DevQuiz.TelegramBot.Services
{
    /// <inheritdoc cref="IBotMessageService" />
    public class BotMessageService : IBotMessageService
    {
        private readonly IBotService _botService;
        private readonly IUserService<UserDto, Guid> _userService;
        private readonly ILogger<BotMessageService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="botService">Instance of bot service</param>
        /// <param name="userService">Instance of user service</param>
        /// <param name="logger">Logger</param>
        public BotMessageService(IBotService botService,
            IUserService<UserDto, Guid> userService,
            ILogger<BotMessageService> logger = null)
        {
            _botService = botService;
            _userService = userService;
            _logger = logger ?? NullLogger<BotMessageService>.Instance;
        }

        /// <inheritdoc cref="IBotMessageService.ProcessUpdateAsync(Update)" />
        public async Task ProcessUpdateAsync(Update updateMessage)
        {
            switch(updateMessage.Type)
            {
                case UpdateType.Message:
                    await ProcessMessageAsync(updateMessage.Message);
                    break;
                case UpdateType.EditedMessage:
                    await ProcessEditedMessage(updateMessage.Message, updateMessage.EditedMessage);
                    break;
            }
            if (updateMessage.Type != UpdateType.Message)
                return;

            var message = updateMessage.Message;

            _logger.LogInformation("Received Message from {0}", message.Chat.Id);

            switch (message.Type)
            {
                case MessageType.Text:
                    // Echo each Message
                    await _botService.Client.SendTextMessageAsync(message.Chat.Id, message.Text);
                    break;
            }
        }
        
        private async Task ProcessMessageAsync(Message message)
        {

        }

        private async Task ProcessEditedMessage(Message originalMessage, Message editedMessage)
        {

        }
    }
}