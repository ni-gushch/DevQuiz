using DevQuiz.Libraries.Core.Models;

namespace DevQuiz.Libraries.Services.Commands
{
    /// <summary>
    /// Command for update question action 
    /// </summary>
    public class UpdateQuestionCommand : IBaseCommand, IHasKey<int>
    {
        /// <summary>
        /// Identifier of question for update
        /// </summary>
        public int Id { get; set; }
    }
}