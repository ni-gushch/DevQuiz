using Telegram.Bot.Types;

namespace DevQuiz.TelegramBot.MediatR.Commands
{
    /// <summary>
    ///     Command when "/start" received
    /// </summary>
    public record StartCommand : BaseBotCommand
    {
        /// <inheritdoc />
        public StartCommand(Message message) : base(message) { }
    }
}
