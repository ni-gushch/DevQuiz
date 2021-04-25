using Telegram.Bot.Types;

namespace DevQuiz.TelegramBot.MediatR.Commands
{
    public record StartCommand : BaseCommand
    {
        public StartCommand(Message message) : base(message) { }
    }
}
