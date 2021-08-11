using DevQuiz.Libraries.Core.Models;

namespace DevQuiz.Libraries.Services.Commands
{
    /// <summary>
    /// Command for delete question action
    /// </summary>
    public class DeleteQuestionCommand : IBaseCommand, IHasKey<int>
    {
        /// <summary>
        /// Identifier of deleted question
        /// </summary>
        public int Id { get; set; }
    }
}