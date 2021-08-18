using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace DevQuiz.Admin.Client.Base
{
    /// <summary>
    /// Base service client realization 
    /// </summary>
    public class ClientServiceBase
    {
        /// <summary>
        /// Instance of <see cref="HttpClient"/> 
        /// </summary>
        protected readonly HttpClient HttpClient;
        /// <summary>
        /// Instance of <see cref="ILogger"/>
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="httpClient">Instance of <see cref="HttpClient"/></param>
        /// <param name="logger">Instance of <see cref="ILogger"/></param>
        public ClientServiceBase(HttpClient httpClient, ILogger logger)
        {
            HttpClient = httpClient;
            Logger = logger;
        }
    }
}