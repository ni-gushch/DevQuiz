using System.Collections.Generic;
using DevQuiz.Admin.Core.Models;

namespace DevQuiz.Admin.Hosting.Models.ApiResults
{
    /// <summary>
    /// ApiResult for represent question information
    /// </summary>
    public class QuestionApiResult : IHasKey<int>
    {
        /// <inheritdoc cref="IHasKey{TKey}.Id"/>
        public int Id { get; set; }
        /// <summary>
        /// Get question text
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Get category
        /// </summary>
        public ValueModel<int> RightAnswer { get; set; }
        /// <summary>
        /// Get category
        /// </summary>
        public ValueModel<int> Category { get; set; }
        /// <summary>
        /// Get list of answers
        /// </summary>
        public List<ValueModel<int>> Answers { get; set; }
        /// <summary>
        /// Get tags list
        /// </summary>
        public List<ValueModel<int>> Tags { get; set; }
    }
}
