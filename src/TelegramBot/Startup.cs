using System;
using System.Reflection;
using DevQuiz.Libraries.Data.DbContexts;
using DevQuiz.TelegramBot.Constants;
using DevQuiz.TelegramBot.Extensions;
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
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Application web host environment
        /// </summary>
        private IWebHostEnvironment WebHostEnvironment { get; }

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
            services.AddDevQuizRepositories<DevQuizDbContext>();
            services.AddDevQuizServices();
            services.AddCustomAutoMapper();

            services.AddHttpClient();
            services.AddHttpClient(TypedHttpClients.TelegramApi.ClientName,
                it => { it.BaseAddress = new Uri(TypedHttpClients.TelegramApi.Address); });

            services.AddCustomSwagger();

            services.AddTelegramBotServices();

            services.AddDevQuizMediatrServices(new[] {Assembly.GetExecutingAssembly()});

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

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}