using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Admin.Client.Models.ApiResults;

namespace DevQuiz.Admin.Client.Abstractions
{
    /// <summary>
    ///     Client service for get access to questions
    /// </summary>
    public interface IQuestionReadService
    {
        /// <summary>
        ///     Get information about all available questions
        /// </summary>
        /// <param name="cancellationToken">Instance of <see cref="CancellationToken" /></param>
        /// <returns>Collection of available questions</returns>
        Task<List<QuestionApiResult>> GetAll(CancellationToken cancellationToken);
    }
}