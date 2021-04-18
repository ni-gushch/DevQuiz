using System;
using System.Reflection;
using DevQuiz.Libraries.Core.Configurations;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data.DbContexts;
using DevQuiz.Libraries.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
        /// <param name="services">IServiceCollection instance</param>
        /// <param name="configuration">IConfiguration instance</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddDevQuizDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var migrationAssembly = typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly.GetName().Name;
            var dbConfiguration = configuration.GetSection(nameof(DbConfiguration)).Get<DbConfiguration>();

            services.AddDbContext<DevQuizDbContext>(opt =>
                opt.UseNpgsql(dbConfiguration.ConnectionString, options =>
                    options.MigrationsAssembly(migrationAssembly)));
            
            services.TryAddScoped<IUnitOfWork , UnitOfWork<DevQuizDbContext>>(); 

            return services;
        }

        /// <summary>
        /// Register Repositories for DevQuiz
        /// </summary>
        /// <param name="services">IServiceCollection instance</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddDevQuizRepositories<TUser,
            TQuestion, TAnswer, TCategory, TTag, TKey>(this IServiceCollection services)
            where TUser : UserBase<TKey>
            where TQuestion : QuestionBase<TAnswer, TCategory, TTag>
            where TAnswer : AnswerBase
            where TCategory : CategoryBase<TQuestion>
            where TTag : TagBase<TQuestion>
            where TKey : IEquatable<TKey>
        {
            services.TryAddScoped<IGenericRepositoryEntityFramework<TUser>, GenericRepositoryEntityFramework<DevQuizDbContext, TUser>>();
            services.TryAddScoped<IGenericRepositoryEntityFramework<TQuestion>, GenericRepositoryEntityFramework<DevQuizDbContext, TQuestion>>();
            services.TryAddScoped<IGenericRepositoryEntityFramework<TAnswer>, GenericRepositoryEntityFramework<DevQuizDbContext, TAnswer>>();
            services.TryAddScoped<IGenericRepositoryEntityFramework<TCategory>, GenericRepositoryEntityFramework<DevQuizDbContext, TCategory>>();
            services.TryAddScoped<IGenericRepositoryEntityFramework<TTag>, GenericRepositoryEntityFramework<DevQuizDbContext, TTag>>();

            return services;
        }
    }
}