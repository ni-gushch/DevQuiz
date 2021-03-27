using System.Collections.Generic;

namespace DevQuiz.Libraries.Core.Models.Base
{
    /// <summary>
    /// Base question model
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Get or set unique identifier of question
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Get or set question text
        /// </summary>
        public string Test { get; set; }
        /// <summary>
        /// Get or set list of answers
        /// </summary>
        public List<Answer> Answers { get; set; }
        /// <summary>
        /// Get or set unique identifier right answer
        /// </summary>
        public int RightAnswerId { get; set; }
        /// <summary>
        /// Get or set right answer explanation
        /// </summary>
        public string RightAnswerExplanation  { get; set; }
    }
}