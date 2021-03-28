using System.Collections.Generic;

namespace DevQuiz.Libraries.Core.Models.Entities
{
    /// <summary>
    /// Base question model
    /// </summary>
    public class QuestionBase<TAnswer, TCategory, TTag> : AggregateEntity<int>
        where TAnswer : class
        where TCategory : class
        where TTag : class
    {
        /// <summary>
        /// Get or set question text
        /// </summary>
        public string Test { get; set; }
        /// <summary>
        /// Get or set list of answers
        /// </summary>
        public List<TAnswer> Answers { get; set; }
        /// <summary>
        /// Get or set unique identifier right answer
        /// </summary>
        public int RightAnswerId { get; set; }
        /// <summary>
        /// Get or set right answer explanation
        /// </summary>
        public string RightAnswerExplanation  { get; set; }
        /// <summary>
        /// Get or set unique identitfier of category
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Category object
        /// </summary>
        public TCategory Category { get; set; }
        /// <summary>
        /// Get or set tags list
        /// </summary>
        public List<TTag> Tags { get; set; } = new List<TTag>();
    }
}