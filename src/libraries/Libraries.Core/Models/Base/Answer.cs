namespace DevQuiz.Libraries.Core.Models.Base
{
    /// <summary>
    /// Base answer model
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// Get or set unique identifier of answer
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Get or set answer text
        /// </summary>
        public string Text { get; set; }
    }
}