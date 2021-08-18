using DevQuiz.Shared.Models;
using DevQuiz.Shared.Models.Abstractions;

namespace DevQuiz.Admin.Services.Commands
{
    /// <summary>
    ///     Command for update question action
    /// </summary>
    public class UpdateQuestionCommand : IBaseCommand, IHasKey<int>
    {
        /// <summary>
        ///     Identifier of question for update
        /// </summary>
        public int Id { get; set; }
    }
}