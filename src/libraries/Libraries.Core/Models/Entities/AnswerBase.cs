namespace DevQuiz.Libraries.Core.Models.Entities
{
    /// <summary>
    /// Base answer model
    /// </summary>
    public class AnswerBase
    {
        /// <summary>
        /// Get or set unique identifier of answer
        /// </summary>
        public int Id { get; set; }
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