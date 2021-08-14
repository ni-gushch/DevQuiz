using System;
using System.IO;
using DevQuiz.Libraries.Core.Configurations;
using DevQuiz.Libraries.Core.Mappers;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Services.Dto;
using DevQuiz.TelegramBot.Configurations;
using DevQuiz.TelegramBot.Interfaces;
using DevQuiz.TelegramBot.Mappers;
using DevQuiz.TelegramBot.MediatR.Commands;
using DevQuiz.TelegramBot.MediatR.Handlers;
using DevQuiz.TelegramBot.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuiz.TelegramBot.Extensions
{
    /// <summary>
    /// Extensions for IServiceCollection instance
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddCustomOptions(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<DbConfiguration>(configuration.GetSection(nameof(DbConfiguration)));
            services.Configure<BotConfiguration>(configuration.GetSection(nameof(BotConfiguration)));

            return services;
        }

        internal static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options => { }).ConfigureSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
                options.IncludeXmlComments(Path.Combine(Directory.GetCurrentDirectory(), "DevQuiz.TelegramBot.xml"));
            });
            services.AddSwaggerGenNewtonsoftSupport();
            
            return services;
        }

        internal static IServiceCollection AddTelegramBotServices(this IServiceCollection services)
        {
            services.AddSingleton<IBotService, BotService>()
                .AddScoped<IBotMessageService, BotMessageService>()
                .AddScoped<IRequestHandler<StartCommand, Unit>, StartCommandHandler<UserDto, Guid>>();
            
            return services;
        }

        internal static IServiceCollection AddCustomAutoMapper<TUser, TUserDto, TKey,
            TQuestion, TAnswer, TCategory, TTag,
            TQuestionDto, TAnswerDto, TCategoryDto, TTagDto>(
            this IServiceCollection services)
            where TUser : User<TKey>
            where TUserDto : UserDtoBase<TKey>
            where TKey : IEquatable<TKey>
            where TQuestion : Question
            where TAnswer : Answer
            where TCategory : Category
            where TTag : Tag
            where TQuestionDto : QuestionDtoBase<TAnswerDto, TCategoryDto, TTagDto>
            where TAnswerDto : AnswerDtoBase
            where TCategoryDto : CategoryDtoBase<TQuestionDto>
            where TTagDto : TagDtoBase<TQuestionDto>
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<UserMapperProfile<TUser, TUserDto, TKey>>();
                config.AddProfile<QuestionMapperProfile<TQuestion, TAnswer, TCategory, TTag, TQuestionDto, TAnswerDto,
                    TCategoryDto, TTagDto>>();
                config.AddProfile<UserBotMapperProfile<TUserDto, TKey>>();
                config.AddProfile<QuestionsAdminApiMapperProfile>();
            });
            
            services.AddDevQuizMapperServices<TQuestion, TAnswer, TCategory, TTag>();

            return services;
        }
    }
}