using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Admin.Client.Models.ApiResults;
using DevQuiz.Admin.Client.UI.Abstractions;
using DevQuiz.Admin.Client.UI.Models.InputModels;
using Microsoft.Extensions.Logging;

namespace DevQuiz.Admin.Client.UI
{
    /// <inheritdoc cref="IQuestionService"/>
    public class QuestionService : QuestionReadService, IQuestionService
    {
        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="httpClient">Instance of <see cref="HttpClient"/></param>
        /// <param name="logger">Instance of <see cref="ILogger"/></param>
        public QuestionService(HttpClient httpClient, ILogger<QuestionService> logger)
            : base(httpClient, logger)
        {
        }

        /// <inheritdoc cref="CreateQuestion"/>
        public Task<IdApiResult<int>> CreateQuestion(CreateQuestionInputModel value,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}