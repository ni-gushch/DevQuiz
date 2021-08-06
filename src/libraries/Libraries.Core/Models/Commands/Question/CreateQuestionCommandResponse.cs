namespace DevQuiz.Libraries.Core.Models.Commands.Question
{
    /// <summary>
    /// Response from Create question command
    /// </summary>
    public class CreateQuestionCommandResponse
    {
        /// <summary>
        /// Identifier of new Question
        /// </summary>
        public int Id { get; set; }
    }
}