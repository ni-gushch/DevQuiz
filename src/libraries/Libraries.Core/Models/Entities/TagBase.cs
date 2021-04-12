using System.Collections.Generic;

namespace DevQuiz.Libraries.Core.Models.Entities
{
    /// <summary>
    /// Base model of question tag
    /// </summary>
    public class TagBase<TQuestion> : EntityBase<int>
        where TQuestion : class
    {
        /// <summary>
        /// Get or set tag name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Get or set questions list
        /// </summary>
        public List<TQuestion> Questions { get; set; } = new List<TQuestion>();
    }
}