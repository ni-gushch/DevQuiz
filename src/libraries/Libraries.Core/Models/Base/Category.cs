using System.Collections.Generic;

namespace DevQuiz.Libraries.Core.Models.Base
{
    /// <summary>
    /// Base model of question category
    /// </summary>
    public class Category
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
        public List<Question> Questions { get; set; }
    }
}