using System;
using System.IO;
using System.Reflection;
using DevQuiz.Libraries.Core.Mappers;
using DevQuiz.Libraries.Data.DbContexts;
using DevQuiz.Libraries.Data.Models;
using DevQuiz.Libraries.Services.Dto;
using DevQuiz.TelegramBot.Constants;
using DevQuiz.TelegramBot.Extensions;
using DevQuiz.TelegramBot.Interfaces;
using DevQuiz.TelegramBot.Mappers;
using DevQuiz.TelegramBot.MediatR.Commands;
using DevQuiz.TelegramBot.MediatR.Handlers;
using DevQuiz.TelegramBot.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DevQuiz.TelegramBot
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration of web application
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// Application web host environment
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">IConfiguration instance</param>
        /// <param name="webHostEnvironment">WebHostEnvironment instance</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Method for configure web app services
        /// </summary>
        /// <param name="services">Web app services collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomOptions(Configuration);

            services.AddDevQuizDbContexts<DevQuizDbContext>(Configuration);
            services.AddDevQuizRepositories<DevQuizDbContext, User, Question, Answer, Category, Tag, Guid>();
            services.AddDevQuizServices<User, UserDto, Guid,
                Question, Answer, Category, Tag, QuestionDto, AnswerDto, CategoryDto, TagDto>();

            services.AddAutoMapper(config =>
            {
                config.AddProfile<UserMapperProfile<User, UserDto, Guid>>();
                config.AddProfile<QuestionMapperProfile<Question, Answer, Category, Tag, QuestionDto, AnswerDto, CategoryDto, TagDto>>();
                config.AddProfile<UserBotMapperProfile<UserDto, Guid>>();
            });

            services.AddHttpClient();
            services.AddHttpClient(TypedHttpClients.TelegramApi, it =>
            {
                it.BaseAddress = new Uri("https://api.telegram.org");
            });

            services.AddSwaggerGen(options =>
            {

            }).ConfigureSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
                options.IncludeXmlComments(Path.Combine(Directory.GetCurrentDirectory(), "DevQuiz.TelegramBot.xml"));
            });
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSingleton<IBotService, BotService>()
                .AddScoped<IBotMessageService, BotMessageService>()
                .AddScoped<IRequestHandler<StartCommand, Unit>, StartCommandHandler<UserDto, Guid>>();

            services.AddControllers()
                .AddNewtonsoftJson();
        }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplication Builder object</param>
        /// <param name="env">IWwbHostEnvironment object</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "DevQuiz telegram bot api");
                cfg.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
