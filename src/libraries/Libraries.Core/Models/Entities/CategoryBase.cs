using System.Collections.Generic;

namespace DevQuiz.Libraries.Core.Models.Entities
{
    /// <summary>
    /// Base model of question category
    /// </summary>
    public class CategoryBase<TQuestion>
        where TQuestion : class
    {
        /// <summary>
        /// Get or set unique identifier of question category
        /// </summary>
        public int Id { get; set; }
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