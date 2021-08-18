using System;
using System.Linq;
using System.Threading.Tasks;
using DevQuiz.TelegramBot.Constants;
using DevQuiz.TelegramBot.MediatR.Commands;
using DevQuiz.TelegramBot.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace TelegramBot.Tests.Services
{
    public class BotMessageServiceTests
    {
        [Fact]
        public async Task ProcessUpdateAsync_NotNullMessage_NotEmptyLogInformation()
        {
            var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
            var mockLogger = Substitute.For<Logger<BotMessageService>>(loggerFactory);
            var service = new BotMessageService(null, mockLogger);
            var message = new Message
            {
                Chat = new Chat(),
                Entities = new MessageEntity[] {new()}
            };
            var update = new Update
            {
                Message = message
            };

            await service.ProcessUpdateAsync(update);

            mockLogger
                .Received()
                .LogInformation(Arg.Is<string>(s => !string.IsNullOrEmpty(s)));
        }

        [Fact]
        public async Task ProcessUpdateAsync_StartCommand_MediatorSendStartCommand()
        {
            var mockMediator = Substitute.For<IMediator>();
            var service = new BotMessageService(mockMediator);
            var message = new Message
            {
                Chat = new Chat(),
                Entities = new MessageEntity[] {new() {Type = MessageEntityType.BotCommand}},
                Text = BotCommands.Start
            };
            var update = new Update
            {
                Message = message
            };

            await service.ProcessUpdateAsync(update);

            await mockMediator
                .Received()
                .Send(Arg.Is(new StartCommand(message)));
        }

        [Fact]
        public void AllRequests_ShouldHaveMatchingHandler()
        {
            var types = typeof(BotMessageService)
                .Assembly
                .GetTypes();

            var requestTypes = types
                .Where(t => t.IsSubclassOf(typeof(BaseBotCommand)))
                .ToList();

            var handlerTypes = types
                .Where(t => t
                    .GetInterfaces()
                    .Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                .ToList();

            requestTypes
                .ForEach(rt => Assert.Contains(handlerTypes, ht => IsHandlerForRequest(ht, rt)));
        }

        private static bool IsHandlerForRequest(Type handlerType, Type requestType)
        {
            return handlerType
                .GetInterfaces()
                .Any(it => it
                    .GenericTypeArguments
                    .Any(at => at == requestType));
        }
    }
}