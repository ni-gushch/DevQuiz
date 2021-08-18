using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Admin.Client.Abstractions;
using DevQuiz.Admin.Client.Base;
using DevQuiz.Admin.Client.Models.ApiResults;
using Microsoft.Extensions.Logging;

namespace DevQuiz.Admin.Client
{
    /// <inheritdoc cref="IQuestionReadService"/> 
    public class QuestionReadService : ClientServiceBase, IQuestionReadService
    {
        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="httpClient">Instance of <see cref="HttpClient"/></param>
        /// <param name="logger">Instance of <see cref="ILogger"/></param>
        public QuestionReadService(HttpClient httpClient, ILogger<QuestionReadService> logger)
            : base(httpClient, logger)
        {
        }

        /// <inheritdoc cref="GetAll"/>
        public Task<List<QuestionApiResult>> GetAll(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}