using System.Threading.Tasks;
using DevQuiz.TelegramBot.Controllers;
using DevQuiz.TelegramBot.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
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

        [Fact]
        public async Task Post_NewUpdate_ReturnOkResult()
        {
            var mockBotMessageService = Substitute.For<IBotMessageService>();
            var updateController = new UpdateController(mockBotMessageService);
            var update = new Update();

            var result = await updateController.Post(update);

            Assert.True(result is OkResult);
        }
    }
}