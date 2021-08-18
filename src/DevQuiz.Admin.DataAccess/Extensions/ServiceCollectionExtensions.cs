using System.Reflection;
using DevQuiz.Admin.Core;
using DevQuiz.Admin.Core.Configurations;
using DevQuiz.Admin.Core.Models.Entities;
using DevQuiz.Admin.Core.Repositories;
using DevQuiz.Admin.DataAccess;
using DevQuiz.Admin.DataAccess.Migrations;
using DevQuiz.Admin.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     Extensions for IServiceCollection instance
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Register DbContexts for DevQuiz
        /// </summary>
        /// <typeparam name="TDbContext">Target db context</typeparam>
        /// <param name="services">IServiceCollection instance</param>
        /// <param name="configuration">IConfiguration instance</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddDevQuizDbContexts<TDbContext>(this IServiceCollection services,
            IConfiguration configuration)
            where TDbContext : DbContext
        {
            var migrationAssembly = typeof(InitialMigration).GetTypeInfo().Assembly.GetName().Name;
            var dbConfiguration = configuration.GetSection(nameof(DataBaseConfiguration)).Get<DataBaseConfiguration>();

            services.AddDbContext<TDbContext>(opt =>
                opt.UseNpgsql(dbConfiguration.ConnectionString, options =>
                    options.MigrationsAssembly(migrationAssembly)));

            return services;
        }

        /// <summary>
        ///     Register Repositories for DevQuiz
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