using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Admin.Client.Abstractions;
using DevQuiz.Admin.Client.Models.ApiResults;
using DevQuiz.Admin.Client.UI.Models.InputModels;

namespace DevQuiz.Admin.Client.UI.Abstractions
{
    /// <inheritdoc cref="IQuestionReadService"/>
    public interface IQuestionService : IQuestionReadService
    {
        /// <summary>
        ///     Create new Question
        /// </summary>
        /// <param name="value">Create question model</param>
        /// <param name="cancellationToken">Instance of <see cref="CancellationToken" /></param>
        /// <returns>Identifier of new question</returns>
        Task<IdApiResult<int>> CreateQuestion(CreateQuestionInputModel value,
            CancellationToken cancellationToken);
    }
}