using DevQuiz.Shared.Models;
using DevQuiz.Shared.Models.Abstractions;

namespace DevQuiz.Admin.Services.Commands
{
    /// <summary>
    ///     Command for delete question action
    /// </summary>
    public class DeleteQuestionCommand : IBaseCommand, IHasKey<int>
    {
        /// <summary>
        ///     Identifier of deleted question
        /// </summary>
        public int Id { get; set; }
    }
}