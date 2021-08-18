using System.Collections.Generic;

namespace DevQuiz.Admin.Core.Models.Dto
{
    /// <summary>
    ///     Base question model
    /// </summary>
    public class QuestionDto : DtoBase<int>
    {
        /// <summary>
        ///     Get or set question text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     Get or set unique identifier right answer
        /// </summary>
        public int RightAnswerId { get; set; }

        /// <summary>
        ///     Get or set right answer explanation
        /// </summary>
        public string RightAnswerExplanation { get; set; }

        /// <summary>
        ///     Get or set unique identifier of category
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        ///     Get or set list of answers
        /// </summary>
        public List<AnswerDto> Answers { get; set; }

        /// <summary>
        ///     Category object
        /// </summary>
        public CategoryDto Category { get; set; }

        /// <summary>
        ///     Get or set tags list
        /// </summary>
        public List<TagDto> Tags { get; set; } = new();
    }
}