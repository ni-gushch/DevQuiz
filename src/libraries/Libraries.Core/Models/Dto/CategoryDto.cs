using System.Collections.Generic;

namespace DevQuiz.Libraries.Core.Models.Dto
{
    /// <summary>
    /// Base model of question category
    /// </summary>
    public class CategoryDto : DtoBase<int>
    {
        /// <summary>
        /// Get or set category name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Linked questions
        /// </summary>
        public List<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
    }
}