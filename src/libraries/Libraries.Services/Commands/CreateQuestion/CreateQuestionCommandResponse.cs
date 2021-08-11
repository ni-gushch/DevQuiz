using DevQuiz.Libraries.Core.Models;

namespace DevQuiz.Libraries.Services.Commands.CreateQuestion
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