namespace DevQuiz.Libraries.Core.Models.Base
{
    /// <summary>
    /// Base model of question tag
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Get or set unique identifier of question tag
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Get or set tag name
        /// </summary>
        public string Name { get; set; }
    }
}