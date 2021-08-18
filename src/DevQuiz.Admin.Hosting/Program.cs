using System;
using System.Threading.Tasks;
using DevQuiz.Admin.Core.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DevQuiz.Admin.Hosting
{
    /// <summary>
    ///     Entrypoint of project
    /// </summary>
    public class Program
    {
        private static string AspNetCoreEnvironmentName => "ASPNETCORE_ENVIRONMENT";

        /// <summary>
        ///     Entry method
        /// </summary>
        /// <param name="args">Args for project</param>
        /// <returns></returns>
        public static Task Main(string[] args)
        {
            return CreateHostBuilder(args)
                .Build()
                .RunAsync();
        }

        /// <summary>
        ///     Creating web app host
        /// </summary>
        /// <param name="args">Application arguments</param>
        /// <returns>Web app host object</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var netCoreEnvironmentVariable = Environment.GetEnvironmentVariable(AspNetCoreEnvironmentName) ??
                                             Environments.Production;
            var configuration = BuildConfigurations(args, netCoreEnvironmentVariable);

#if(DEBUG)
            var connectionString = configuration.GetSection(nameof(DataBaseConfiguration)).Get<DataBaseConfiguration>();
            if (connectionString is null)
                throw new NullReferenceException(
                    $"{nameof(connectionString)} configuration is not set in user secrets");
            Environment.SetEnvironmentVariable(
                $"{nameof(DataBaseConfiguration)}:{nameof(DataBaseConfiguration.ConnectionString)}",
                connectionString.ConnectionString);
#endif

            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, builder) => { builder.AddConfiguration(configuration); })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }

        private static IConfiguration BuildConfigurations(string[] args, string aspNetCoreEnvironment)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{aspNetCoreEnvironment}.json", true, true)
                .AddEnvironmentVariables();

            if (aspNetCoreEnvironment.Equals(Environments.Development))
                configurationBuilder.AddUserSecrets<Startup>();

            configurationBuilder.AddCommandLine(args);
            return configurationBuilder.Build();
        }
    }
}