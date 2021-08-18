using System.Collections.Generic;

namespace DevQuiz.Admin.Core.Models.Entities
{
    /// <summary>
    ///     Base model of question category
    /// </summary>
    public class Category : EntityBase<int>
    {
        /// <summary>
        ///     Get or set category name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Linked questions
        /// </summary>
        public List<Question> Questions { get; set; } = new();
    }
}