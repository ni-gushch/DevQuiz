using DevQuiz.Libraries.Core.Configurations;
using DevQuiz.TelegramBot.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuiz.TelegramBot.Extensions
{
    /// <summary>
    /// Extensions for IServiceCollection instance
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbConfiguration>(configuration.GetSection(nameof(DbConfiguration)));
            services.Configure<BotConfiguration>(configuration.GetSection(nameof(BotConfiguration)));

            return services;
        }
    }
}