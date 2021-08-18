using DevQuiz.Admin.Client.UI;
using DevQuiz.Admin.Client.UI.Abstractions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     Class for registration additional services
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Register client for dev quiz admin service
        /// </summary>
        /// <param name="services">Instance of <see cref="IServiceCollection"/></param>
        /// <returns>Original instance of <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddDevQuizAdminUIClient(this IServiceCollection services)
        {
            services.AddHttpClient<IQuestionService, QuestionService>();
            
            return services;
        }
    }
}