using DevQuiz.TelegramBot.Controllers;
using DevQuiz.TelegramBot.Interfaces;
using NSubstitute;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Xunit;

namespace TelegramBot.Tests.Controllers
{
    public class UpdateControllerTests
    {        
        [Fact]        
        public async Task Post_NewUpdate_CalledProcessUpdateAsync()
        {
            var mockBotMessageService = Substitute.For<IBotMessageService>();
            var updateController = new UpdateController(mockBotMessageService);
            var update = new Update();

            await updateController.Post(update);

            await mockBotMessageService
                .Received()
                .ProcessUpdateAsync(update);
        }
    }
}
