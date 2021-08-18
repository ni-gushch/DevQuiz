using System.IO;
using System.Reflection;
using DevQuiz.Admin.Core.Configurations;
using DevQuiz.Admin.Core.Mappers;
using DevQuiz.Admin.Hosting.MappersProfiles;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     Class with extensions for register additional services
    /// </summary>
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddCustomOptions(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<DataBaseConfiguration>(configuration.GetSection(nameof(DataBaseConfiguration)));

            return services;
        }

        internal static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(_ => { }).ConfigureSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
                var fullName = Assembly.GetExecutingAssembly().FullName;
                if (fullName != null)
                    options.IncludeXmlComments(Path.Combine(Directory.GetCurrentDirectory(),
                        $"{fullName.Split(',')[0]}.xml"));
            });
            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }

        internal static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            return services;
        }

        internal static IServiceCollection AddCustomAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<UserMapperProfile>();
                config.AddProfile<QuestionMapperProfile>();
                config.AddProfile<QuestionsAdminApiMapperProfile>();
            });

            return services;
        }
    }
}