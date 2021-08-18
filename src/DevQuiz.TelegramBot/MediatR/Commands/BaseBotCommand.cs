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
        ///     Constructor
        /// </summary>
        /// <param name="message"> Message </param>
        protected BaseBotCommand(Message message)
        {
            Message = message;
        }

        /// <summary>
        ///     Message
        /// </summary>
        public Message Message { get; init; }

        /// <summary>
        ///     Chat
        /// </summary>
        public Chat Chat => Message.Chat;
    }
}