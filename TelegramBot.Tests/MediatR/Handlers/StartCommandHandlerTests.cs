using AutoMapper;
using DevQuiz.Libraries.Data.Tests.Helpers;
using DevQuiz.Libraries.Services;
using DevQuiz.TelegramBot.Mappers;
using DevQuiz.TelegramBot.MediatR.Commands;
using DevQuiz.TelegramBot.MediatR.Handlers;
using NSubstitute;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Models.Dto;
using Telegram.Bot.Types;
using Xunit;

namespace TelegramBot.Tests.MediatR.Handlers
{
    public class StartCommandHandlerTests : DevQuizContextSeedDataHelper
    {
        [Fact]
        public async Task Handle_NewUser_CallCreateUser()
        {
            var mockService = Substitute.For<FakeUserService>();

            var mapperConfiguration = new MapperConfiguration(
                config => config.AddProfile<UserBotMapperProfile>());            
            var mapper = new Mapper(mapperConfiguration);
            var handler = new StartCommandHandler(mockService, mapper, null);
            var message = new Message()
            {
                Chat = new ()
                {
                    FirstName = "FirstName",
                    LastName = "LastName"
                }
            };
            var command = new StartCommand(message);
            var cancellationToken = new CancellationToken();
            
            await handler.Handle(command, cancellationToken);
            
            await mockService
                .ReceivedWithAnyArgs()
                .CreateAsync(default, cancellationToken);
        }
        
        [Fact]
        public async Task Handle_NewUser_NotCallUpdateUser()
        {
            var mockService = Substitute
                .For<FakeUserService>();

            var mapperConfiguration = new MapperConfiguration(
                config => config.AddProfile<UserBotMapperProfile>());            
            var mapper = new Mapper(mapperConfiguration);
            var handler = new StartCommandHandler(mockService, mapper, null);
            var message = new Message()
            {
                Chat = new ()
                {
                    FirstName = "FirstName",
                    LastName = "LastName"
                }
            };
            var command = new StartCommand(message);
            var cancellationToken = new CancellationToken();
            
            await handler.Handle(command, cancellationToken);
            
            await mockService
                .DidNotReceiveWithAnyArgs()
                .UpdateAsync(default, cancellationToken);
        }
        
        [Fact]
        public async Task Handle_NewUserName_CallUpdateUser()
        {
            var mockService = Substitute
                .For<FakeUserService>();
            mockService.UserDtoes = new List<UserDto>()
            {
                new ()
                {
                    FirstName = "OldFirstName",
                    LastName = "LastName"
                }
            };
            var mapperConfiguration = new MapperConfiguration(
                config => config.AddProfile<UserBotMapperProfile>());            
            var mapper = new Mapper(mapperConfiguration);
            var handler = new StartCommandHandler(mockService, mapper, null);
            var message = new Message()
            {
                Chat = new ()
                {
                    FirstName = "NewFirstName",
                    LastName = "LastName"
                }
            };
            var command = new StartCommand(message);
            
            await handler.Handle(command, new CancellationToken());
            
            await mockService
                .ReceivedWithAnyArgs()
                .UpdateAsync(default);
        }
        
        [Fact]
        public async Task Handle_ExistingUser_NotCreateOrUpdateUser()
        {
            var mockService = Substitute
                .For<FakeUserService>();
            mockService.UserDtoes = new List<UserDto>()
            {
                new ()
                {
                    FirstName = "FirstName",
                    LastName = "LastName"
                }
            };
            var mapperConfiguration = new MapperConfiguration(
                config => config.AddProfile<UserBotMapperProfile>());            
            var mapper = new Mapper(mapperConfiguration);
            var handler = new StartCommandHandler(mockService, mapper, null);
            var message = new Message()
            {
                Chat = new ()
                {
                    FirstName = "FirstName",
                    LastName = "LastName"
                }
            };
            var command = new StartCommand(message);
            var cancellationToken = new CancellationToken();
            
            await handler.Handle(command, cancellationToken);
            
            await mockService
                .DidNotReceiveWithAnyArgs()
                .UpdateAsync(default, cancellationToken);

            await mockService
                .DidNotReceiveWithAnyArgs()
                .CreateAsync(default, cancellationToken);
        }
    }
}
