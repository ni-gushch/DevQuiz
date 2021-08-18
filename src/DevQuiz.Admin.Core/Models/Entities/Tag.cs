using System.Collections.Generic;

namespace DevQuiz.Admin.Core.Models.Entities
{
    /// <summary>
    ///     Base model of question tag
    /// </summary>
    public class Tag : EntityBase<int>
    {
        /// <summary>
        ///     Get or set tag name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Get or set questions list
        /// </summary>
        public List<Question> Questions { get; set; } = new();
    }
}