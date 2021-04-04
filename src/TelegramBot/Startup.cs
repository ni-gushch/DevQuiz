using System;
using DevQuiz.Libraries.Core.Mappers;
using DevQuiz.Libraries.Data.Extensions;
using DevQuiz.Libraries.Data.Models;
using DevQuiz.Libraries.Services.Dto;
using DevQuiz.Libraries.Services.Extensions;
using DevQuiz.TelegramBot.Extensions;
using DevQuiz.TelegramBot.Interfaces;
using DevQuiz.TelegramBot.Services;
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
        public IConfiguration Configuration{ get; }
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

            services.AddDevQuizDbContexts(Configuration);
            services.AddDevQuizRepositories<User, Guid>();
            services.AddDevQuizServices<User, UserDto, Guid>();
            
            services.AddAutoMapper(new [] {
                typeof(Startup),
                typeof(UserMapperProfile<User, UserDto, Guid>)
            });

            services.AddSingleton<IBotService, BotService>();
            services.AddScoped<IBotMessageService, BotMessageService>();

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

            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
