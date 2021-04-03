using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace DevQuiz.TelegramBot.Interfaces
{
    /// <summary>
    /// Service for processing messages received from the bot
    /// </summary>
    public interface IBotMessageService
    {
        /// <summary>
        /// Processing a message
        /// </summary>
        /// <param name="updateMessage">New message</param>
        Task ProcessMessageAsync(Update updateMessage);
    }
}