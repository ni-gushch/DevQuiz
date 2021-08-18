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
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDevQuizAdminUIClient(this IServiceCollection services)
        {
            return services;
        }
    }
}