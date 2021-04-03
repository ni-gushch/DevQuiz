using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Hosting;

namespace DevQuiz.TelegramBot
{
    /// <summary>
    /// App entry point
    /// </summary>
    public class Program
    {
        private static string AspNetCoreEnvironmentName => "ASPNETCORE_ENVIRONMENT";

        /// <summary>
        /// Main class
        /// </summary>
        /// <param name="args">Application arguments</param>
        public static void Main(string[] args)
        {
            var netCoreEnvironmentVariable = Environment.GetEnvironmentVariable(AspNetCoreEnvironmentName) ?? Environments.Production;
            var configuration = BuildConfigurations(args, netCoreEnvironmentVariable);

            CreateHostBuilder(args, configuration)
                .Build()
                .Run();
        }

        /// <summary>
        /// Creating web app host
        /// </summary>
        /// <param name="args">Application arguments</param>
        /// <param name="configuration">IConfiguration instance</param>
        /// <returns>Web app host object</returns>
        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, builder) => 
                {
                    builder.AddConfiguration(configuration);
                    if(hostContext.HostingEnvironment.IsDevelopment())
                        builder.AddUserSecrets<Program>();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static IConfiguration BuildConfigurations(string[] args, string aspNetCoreEnvironment)
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional:false, reloadOnChange:true)
                .AddJsonFile($"appsettings.{aspNetCoreEnvironment}.json", optional:true, reloadOnChange:true)
                .Build();
        } 
        
    }
}
