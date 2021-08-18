using Telegram.Bot;

namespace DevQuiz.TelegramBot.Interfaces
{
    /// <summary>
    ///     Service for managing telegram bot client
    /// </summary>
    public interface IBotService
    {
        /// <summary>
        ///     Instance of telegram bot client
        /// </summary>
        public TelegramBotClient Client { get; }
    }
}