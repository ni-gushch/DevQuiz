using MediatR;
using Telegram.Bot.Types;

namespace DevQuiz.TelegramBot.MediatR.Commands
{
    /// <summary>
    ///     Base bot command
    /// </summary>
    public abstract record BaseBotCommand : IRequest
    {
        /// <summary>
        ///     Message
        /// </summary>
        public Message Message { get; init; }

        /// <summary>
        ///     Chat
        /// </summary>
        public Chat Chat => Message.Chat;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="message"> Message </param>
        public BaseBotCommand(Message message) => Message = message;
    }
}
