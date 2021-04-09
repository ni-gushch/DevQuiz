namespace DevQuiz.Libraries.Core.Models.Entities
{
    /// <summary>
    /// Base answer model
    /// </summary>
    public class AnswerBase : EntityBase<int>
    {
        /// <summary>
        /// Get or set answer text
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Get or set unique identifier of question
        /// </summary>
        public int QuestionId { get; set; }
    }
}