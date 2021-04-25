using MediatR;
using Telegram.Bot.Types;

namespace DevQuiz.TelegramBot.MediatR.Commands
{
    public abstract record BaseCommand : IRequest
    {
        public Message Message { get; init; }

        public BaseCommand(Message message) => Message = message;
    }
}
