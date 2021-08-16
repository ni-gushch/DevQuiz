using DevQuiz.Admin.Core.Models;

namespace DevQuiz.Admin.Services.Commands
{
    /// <summary>
    /// Create question command response
    /// </summary>
    public class CreateQuestionCommandResponse : IHasKey<int>
    {
        /// <summary>
        /// Identifier of new Question
        /// </summary>
        public int Id { get; set; }
    }
}