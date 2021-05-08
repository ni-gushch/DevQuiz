using AutoMapper;
using DevQuiz.Libraries.Core;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data;
using DevQuiz.Libraries.Data.DbContexts;
using DevQuiz.Libraries.Data.Models;
using DevQuiz.Libraries.Data.Repositories;
using DevQuiz.Libraries.Data.Tests.Helpers;
using DevQuiz.Libraries.Services;
using DevQuiz.Libraries.Services.Dto;
using DevQuiz.TelegramBot.Mappers;
using DevQuiz.TelegramBot.MediatR.Commands;
using DevQuiz.TelegramBot.MediatR.Handlers;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Xunit;
using User = DevQuiz.Libraries.Data.Models.User;

namespace TelegramBot.Tests.MediatR.Handlers
{
    public class StartCommandHandlerTests : DevQuizContextSeedDataHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DevQuizDbContext _dbContext;

        public StartCommandHandlerTests()
        {
            var serviceCollection = new ServiceCollection()
                .AddScoped(opt => new DevQuizDbContext(ContextOptions))
                .AddScoped<IUnitOfWork, DevQuizUnitOfWork<DevQuizDbContext, User, Guid>>()
                .AddScoped<IGenericRepository<User>, GenericRepository<DevQuizDbContext, User>>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _dbContext = serviceProvider.GetRequiredService<DevQuizDbContext>();
        }

        [Fact]
        public async Task Test()
        {
            var mockService = Substitute
                .For<FakeUserService<User, UserDto, Guid, Question, Answer, Category, Tag>>();

            var config = new MapperConfiguration(
                config => config.AddProfile<UserBotMapperProfile<UserDto, Guid>>());            
            var mapper = new Mapper(config);

            var handler = new StartCommandHandler<UserDto, Guid>(mockService, mapper, null);

            var command = new StartCommand(new Message() { Chat = new Chat()});
            
            await handler.Handle(command, new CancellationToken());
            
            await mockService.Received().CreateAsync(Arg.Any<UserDto>());
        }
    }
}
