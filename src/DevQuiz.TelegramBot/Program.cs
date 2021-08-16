using System;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
        public static async Task Main(string[] args) => 
            await CreateHostBuilder(args)
                .Build()
                .RunAsync();
        

        /// <summary>
        /// Creating web app host
        /// </summary>
        /// <param name="args">Application arguments</param>
        /// <returns>Web app host object</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) 
        {
            var netCoreEnvironmentVariable = Environment.GetEnvironmentVariable(AspNetCoreEnvironmentName) ?? Environments.Production;
            var configuration = BuildConfigurations(args, netCoreEnvironmentVariable);

#if(DEBUG)
            var connectionString = configuration.GetSection(nameof(DbConfiguration)).Get<DbConfiguration>();
            Environment.SetEnvironmentVariable($"{nameof(DbConfiguration)}:{nameof(DbConfiguration.ConnectionString)}", connectionString.ConnectionString);
#endif

            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, builder) => 
                {
                    builder.AddConfiguration(configuration);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
            
        private static IConfiguration BuildConfigurations(string[] args, string aspNetCoreEnvironment)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional:false, reloadOnChange:true)
                .AddJsonFile($"appsettings.{aspNetCoreEnvironment}.json", optional:true, reloadOnChange:true)
                .AddEnvironmentVariables();

            if(aspNetCoreEnvironment.Equals(Environments.Development))
                configurationBuilder.AddUserSecrets<Startup>();

            configurationBuilder.AddCommandLine(args);
            return configurationBuilder.Build();
        }
    }
}
