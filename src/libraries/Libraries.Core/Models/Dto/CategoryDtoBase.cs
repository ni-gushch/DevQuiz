using System.Collections.Generic;

namespace DevQuiz.Libraries.Core.Models.Dto
{
    /// <summary>
    /// Base model of question category
    /// </summary>
    public class CategoryDtoBase<TQuestion> : DtoBase<int>
        where TQuestion : class
    {
        /// <summary>
        /// Get or set category name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Linked questions
        /// </summary>
        public List<TQuestion> Questions { get; set; } = new List<TQuestion>();
    }
}