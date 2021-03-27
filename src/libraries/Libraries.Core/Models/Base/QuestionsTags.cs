namespace DevQuiz.Libraries.Core.Models.Base
{
    /// <summary>
    /// Model for creating many-to-many link between Question and tag
    /// </summary>
    public class QuestionsTags
    {
        /// <summary>
        /// Get or set unique identifier of question
        /// </summary>
        public int QuestionId { get; set; }
        /// <summary>
        /// Get or set question object
        /// </summary>
        public Question Question { get; set; }

        /// <summary>
        /// Get or set unique identifier of question tag
        /// </summary>
        public int TagId { get; set; }
        /// <summary>
        /// Get or set tag object
        /// </summary>
        public Tag Tag { get; set; }
    }
}