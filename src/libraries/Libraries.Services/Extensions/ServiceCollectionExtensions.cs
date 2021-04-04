using System;
using System.Collections.Generic;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Services;
using DevQuiz.Libraries.Services.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuiz.Libraries.Services.Extensions
{
    /// <summary>
    /// Extensions methods for IServiceCollection instance
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services for working with DevQuiz db
        /// </summary>
        /// <param name="services">IServiceCollection instance</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddDevQuizServices<TUser, TUserDto, TKey>(this IServiceCollection services)
            where TUser : UserBase<TKey>
            where TUserDto : UserDtoBase<TKey>
            where TKey : IEquatable<TKey>
        {
            services.AddTransient<IUserService<TUserDto, TUserDto, List<TUserDto>, bool, TKey>, UserService<TUser, TUserDto, TKey>>();

            return services;
        }
    }
}