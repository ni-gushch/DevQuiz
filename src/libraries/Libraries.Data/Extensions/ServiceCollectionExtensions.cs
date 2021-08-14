using System.Reflection;
using DevQuiz.Libraries.Core;
using DevQuiz.Libraries.Core.Configurations;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data;
using DevQuiz.Libraries.Data.Migrations;
using DevQuiz.Libraries.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for IServiceCollection instance
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register DbContexts for DevQuiz
        /// </summary>
        /// <typeparam name="TDbContext">Target db context</typeparam>
        /// <param name="services">IServiceCollection instance</param>
        /// <param name="configuration">IConfiguration instance</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddDevQuizDbContexts<TDbContext>(this IServiceCollection services, IConfiguration configuration)
            where TDbContext : DbContext
        {
            var migrationAssembly = typeof(InitialMigration).GetTypeInfo().Assembly.GetName().Name;
            var dbConfiguration = configuration.GetSection(nameof(DbConfiguration)).Get<DbConfiguration>();

            services.AddDbContext<TDbContext>(opt =>
                opt.UseNpgsql(dbConfiguration.ConnectionString, options =>
                    options.MigrationsAssembly(migrationAssembly)));

            return services;
        }

        /// <summary>
        /// Register Repositories for DevQuiz
        /// </summary>
        /// <typeparam name="TDbContext">Target db context</typeparam>
        /// <param name="services">IServiceCollection instance</param>
        /// <returns>Clear IServiceCollection</returns>
        public static IServiceCollection AddDevQuizRepositories<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            services.TryAddScoped<IUnitOfWork, UnitOfWork<TDbContext>>();
            services.TryAddScoped<IDevQuizUnitOfWork, DevQuizUnitOfWork<TDbContext>>();

            services.TryAddScoped<IGenericRepository<User>, GenericRepository<TDbContext, User>>();
            services.TryAddScoped<IGenericRepository<Question>, GenericRepository<TDbContext, Question>>();
            services.TryAddScoped<IGenericRepository<Answer>, GenericRepository<TDbContext, Answer>>();
            services.TryAddScoped<IGenericRepository<Category>, GenericRepository<TDbContext, Category>>();
            services.TryAddScoped<IGenericRepository<Tag>, GenericRepository<TDbContext, Tag>>();

            return services;
        }
    }
}