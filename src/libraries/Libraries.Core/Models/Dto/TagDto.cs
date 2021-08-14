using System.Collections.Generic;

namespace DevQuiz.Libraries.Core.Models.Dto
{
    /// <summary>
    /// Base model of question tag
    /// </summary>
    public class TagDto : DtoBase<int>
    {
        /// <summary>
        /// Get or set tag name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Get or set questions list
        /// </summary>
        public List<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
    }
}