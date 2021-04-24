namespace DevQuiz.Libraries.Core.Models.Dto
{
    /// <summary>
    /// Base answer model
    /// </summary>
    public class AnswerDtoBase : DtoBase<int>
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