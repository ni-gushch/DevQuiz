using System;
using System.Collections.Generic;
using System.Reflection;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Services;
using DevQuiz.Libraries.Services;
using DevQuiz.Libraries.Services.Handlers.Admin;
using DevQuiz.Libraries.Services.MapperProfiles;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions methods for IServiceCollection instance
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services for working with DevQuiz db
        /// </summary>
        /// <typeparam name="TUser">Generic User Entity</typeparam>
        /// <typeparam name="TQuestion">Generic Question Entity</typeparam>
        /// <typeparam name="TAnswer">Generic Question Answer Entity</typeparam>
        /// <typeparam name="TCategory">Generic Question Category Entity</typeparam>
        /// <typeparam name="TTag">Generic Question Tag Entity</typeparam>
        /// <typeparam name="TUserDto">Generic User Dto</typeparam>
        /// <typeparam name="TQuestionDto">Generic Question Dto</typeparam>
        /// <typeparam name="TAnswerDto">Generic Question Answer Dto</typeparam>
        /// <typeparam name="TCategoryDto">Generic Question Category Dto</typeparam>
        /// <typeparam name="TTagDto">Generic Question Tag Dto</typeparam>
        /// <typeparam name="TUserKey">Generic Key for User Entity</typeparam>
        /// <param name="services">IServiceCollection instance</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddDevQuizServices<TUser, TUserDto, TUserKey,
            TQuestion, TAnswer, TCategory, TTag, TQuestionDto, TAnswerDto, TCategoryDto, TTagDto>(this IServiceCollection services)
            where TUser : UserBase<TUserKey>
            where TUserDto : UserDtoBase<TUserKey>
            where TQuestion : QuestionBase<TAnswer, TCategory, TTag>
            where TAnswer : AnswerBase
            where TCategory : CategoryBase<TQuestion>
            where TTag : TagBase<TQuestion>
            where TQuestionDto : QuestionDtoBase<TAnswerDto, TCategoryDto, TTagDto>
            where TAnswerDto : AnswerDtoBase
            where TCategoryDto : CategoryDtoBase<TQuestionDto>
            where TTagDto : TagDtoBase<TQuestionDto>
            where TUserKey : IEquatable<TUserKey>
        {
            services.TryAddScoped<IUserService<TUserDto, TUserKey>, 
                UserService<TUser, TUserDto, TUserKey, TQuestion, TAnswer, TCategory, TTag>>();
            services.TryAddScoped<IQuestionService<TQuestionDto, TAnswerDto, TCategoryDto, TTagDto>, 
                QuestionService<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey, TQuestionDto, TAnswerDto, TCategoryDto, TTagDto>>();

            return services;
        }

        public static IServiceCollection AddDevQuizMediatrServices(this IServiceCollection services,
            IEnumerable<Assembly> additionalMediatrAssemblies = null)
        {
            var assembliesForMediatr = new List<Assembly>()
            {
                typeof(CreateQuestionCommandHandler<,,,,,>).Assembly
            };
            if(additionalMediatrAssemblies != null)
                assembliesForMediatr.AddRange(additionalMediatrAssemblies);

            services.AddMediatR(assembliesForMediatr, opt =>
            {
                
            });

            return services;
        }

        /// <summary>
        /// Register AutoMapping services for DevQuiz 
        /// </summary>
        /// <param name="services">Instance of <see cref="IServiceCollection"/></param>
        /// <typeparam name="TQuestion">Concrete type of Question</typeparam>
        /// <typeparam name="TAnswer">Concrete type of Answer</typeparam>
        /// <typeparam name="TCategory">Concrete type of Category</typeparam>
        /// <typeparam name="TTag">Concrete type of Tag</typeparam>
        /// <returns>Original instance of <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddDevQuizMapperServices<TQuestion, TAnswer, TCategory, TTag>(this IServiceCollection services)
            where TQuestion : QuestionBase<TAnswer, TCategory, TTag>
            where TAnswer : AnswerBase
            where TCategory : CategoryBase<TQuestion>
            where TTag : TagBase<TQuestion>
        {
            services.AddAutoMapper(opt =>
            {
                opt.AddMaps(typeof(DevQuizBusinessLogicMapperProfile<TQuestion, TAnswer, TCategory, TTag>).Assembly);
            });
            
            return services;
        }
    }
}