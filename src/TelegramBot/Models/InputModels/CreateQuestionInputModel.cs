using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevQuiz.TelegramBot.Models.InputModels
{
    /// <summary>
    /// Input model for creating new question
    /// </summary>
    public class CreateQuestionInputModel
    {
        /// <summary>
        /// Set question text
        /// </summary>
        [Required]
        public string Text { get; set; }
        /// <summary>
        /// Set category
        /// </summary>
        public ValueModel<int> Category { get; set; }
        /// <summary>
        /// Get or set list of answers
        /// </summary>
        public List<ValueModel<int>> Answers { get; set; }
        /// <summary>
        /// Get or set tags list
        /// </summary>
        public List<ValueModel<int>> Tags { get; set; }
    }
}
