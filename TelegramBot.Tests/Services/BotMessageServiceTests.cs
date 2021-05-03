using DevQuiz.TelegramBot.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Xunit;

namespace TelegramBot.Tests.Services
{
    public class BotMessageServiceTests
    {
        [Fact]
        public async Task ProcessUpdateAsync_LogWithChatId()
        {
            var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
            var mockLogger = Substitute.For<Logger<BotMessageService>>(loggerFactory);
            var service = new BotMessageService(null, mockLogger);
            var message = new Message
            {
                Chat = new(),
                Entities = new MessageEntity[] { new() }
            };
            var update = new Update() { 
                Message = message                
            };

            await service.ProcessUpdateAsync(update);

            mockLogger
                .Received()
                .LogInformation(Arg.Is<string>(p => p.Contains("Received")));
        }
    }
}
