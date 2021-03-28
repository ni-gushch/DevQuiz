namespace DevQuiz.Libraries.Core.Models.Entities
{
    /// <summary>
    /// Base answer model
    /// </summary>
    public class AnswerBase : Entity<int>
    {
        /// <summary>
        /// Get or set answer text
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Get or set unique identitfier of question
        /// </summary>
        public int QuestionId { get; set; }
    }
}