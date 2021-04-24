using System.Collections.Generic;

namespace DevQuiz.Libraries.Core.Models.Dto
{
    /// <summary>
    /// Base question model
    /// </summary>
    public class QuestionDtoBase<TAnswerDto, TCategoryDto, TTagDto> : DtoBase<int>
        where TAnswerDto : class
        where TCategoryDto : class
        where TTagDto : class
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
        public List<TAnswerDto> Answers { get; set; }
        /// <summary>
        /// Category object
        /// </summary>
        public TCategoryDto Category { get; set; }
        /// <summary>
        /// Get or set tags list
        /// </summary>
        public List<TTagDto> Tags { get; set; } = new List<TTagDto>();
    }
}