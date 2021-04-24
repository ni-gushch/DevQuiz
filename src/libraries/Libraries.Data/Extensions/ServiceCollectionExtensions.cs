using System;
using System.Reflection;
using DevQuiz.Libraries.Core;
using DevQuiz.Libraries.Core.Configurations;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data;
using DevQuiz.Libraries.Data.DbContexts;
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

            return services;
        }

        /// <summary>
        /// Register Repositories for DevQuiz
        /// </summary>
        /// <typeparam name="TUser">Generic User Entity</typeparam>
        /// <typeparam name="TQuestion">Generic Question Entity</typeparam>
        /// <typeparam name="TAnswer">Generic Question Answer Entity</typeparam>
        /// <typeparam name="TCategory">Generic Question Category Entity</typeparam>
        /// <typeparam name="TTag">Generic Question Tag Entity</typeparam>
        /// <typeparam name="TUserKey">Generic Key for User Entity</typeparam>
        /// <param name="services">IServiceCollection instance</param>
        /// <returns>Clear IServiceCollection</returns>
        public static IServiceCollection AddDevQuizRepositories<TUser,
            TQuestion, TAnswer, TCategory, TTag, TUserKey>(this IServiceCollection services)
            where TUser : UserBase<TUserKey>
            where TQuestion : QuestionBase<TAnswer, TCategory, TTag>
            where TAnswer : AnswerBase
            where TCategory : CategoryBase<TQuestion>
            where TTag : TagBase<TQuestion>
            where TUserKey : IEquatable<TUserKey>
        {
            services.TryAddScoped<IDevQuizUnitOfWork<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey>, 
                DevQuizUnitOfWork<DevQuizDbContext, TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey>>();

            services.TryAddScoped<IGenericRepository<TUser>, GenericRepository<DevQuizDbContext, TUser>>();
            services.TryAddScoped<IGenericRepository<TQuestion>, GenericRepository<DevQuizDbContext, TQuestion>>();
            services.TryAddScoped<IGenericRepository<TAnswer>, GenericRepository<DevQuizDbContext, TAnswer>>();
            services.TryAddScoped<IGenericRepository<TCategory>, GenericRepository<DevQuizDbContext, TCategory>>();
            services.TryAddScoped<IGenericRepository<TTag>, GenericRepository<DevQuizDbContext, TTag>>();

            return services;
        }
    }
}