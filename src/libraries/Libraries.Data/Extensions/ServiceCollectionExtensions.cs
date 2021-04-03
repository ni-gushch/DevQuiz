using System.Reflection;
using DevQuiz.Libraries.Core.Configurations;
using DevQuiz.Libraries.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuiz.Libraries.Data.Extensions
{
    /// <summary>
    /// Extensions for IServiceCollection instance
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register DbContexts for DevQuiz
        /// </summary>
        /// <param name="services">IServiceCollestion instance</param>
        /// <param name="configuration">IConfiguration instance</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddDevQuizDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var migrationAssembly = typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly.GetName().Name;
            var dbConfiguration = configuration.GetSection(nameof(DbConfiguration));

            //services.AddDbContext<DevQuizDbContext>(opt => 
            //    opt.UseNpgsql)

            return services;
        }

        /// <summary>
        /// Register Repositories for DevQuiz
        /// </summary>
        /// <param name="services">IServiceCollestion instance</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddDevQuizRepositories(this IServiceCollection services)
        {

            return services;
        }
    }
}