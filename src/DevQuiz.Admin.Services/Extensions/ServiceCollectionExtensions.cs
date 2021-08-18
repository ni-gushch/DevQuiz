using System.Collections.Generic;
using System.Reflection;
using DevQuiz.Admin.Core.Services;
using DevQuiz.Admin.Services;
using DevQuiz.Admin.Services.Handlers.Admin;
using DevQuiz.Admin.Services.MapperProfiles;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     Extensions methods for IServiceCollection instance
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Add services for working with DevQuiz db
        /// </summary>
        /// <param name="services">IServiceCollection instance</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddDevQuizServices(this IServiceCollection services)
        {
            services.TryAddScoped<IUserService, UserService>();
            services.TryAddScoped<IQuestionService, QuestionService>();

            return services;
        }

        /// <summary>
        ///     Register Handlers for commands that contains in Business logic layer
        /// </summary>
        /// <param name="services"></param>
        /// <param name="additionalMediatrAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddDevQuizMediatrServices(this IServiceCollection services,
            IEnumerable<Assembly> additionalMediatrAssemblies = null)
        {
            var assembliesForMediatr = new List<Assembly>
            {
                typeof(CreateQuestionCommandHandler).Assembly
            };
            if (additionalMediatrAssemblies != null)
                assembliesForMediatr.AddRange(additionalMediatrAssemblies);

            services.AddMediatR(assembliesForMediatr, opt => { });

            return services;
        }

        /// <summary>
        ///     Register AutoMapping services for DevQuiz
        /// </summary>
        /// <param name="services">Instance of <see cref="IServiceCollection" /></param>
        /// <returns>Original instance of <see cref="IServiceCollection" /></returns>
        public static IServiceCollection AddDevQuizMapperServices(this IServiceCollection services)
        {
            services.AddAutoMapper(opt => { opt.AddProfile<DevQuizBusinessLogicMapperProfile>(); });

            return services;
        }
    }
}