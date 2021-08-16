using System.Collections.Generic;

namespace DevQuiz.Admin.Core.Models.Entities
{
    /// <summary>
    /// Base question model
    /// </summary>
    public class Question : AggregateEntity<int>
    {
        /// <summary>
        /// Get or set question text
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Get or set unique identifier right answer
        /// </summary>
        public int RightAnswerId { get; set; }
        /// <summary>
        /// Get or set right answer explanation
        /// </summary>
        public string RightAnswerExplanation  { get; set; }
        /// <summary>
        /// Get or set unique identifier of category
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Get or set list of answers
        /// </summary>
        public List<Answer> Answers { get; set; }
        /// <summary>
        /// Category object
        /// </summary>
        public Category Category { get; set; }
        /// <summary>
        /// Get or set tags list
        /// </summary>
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}