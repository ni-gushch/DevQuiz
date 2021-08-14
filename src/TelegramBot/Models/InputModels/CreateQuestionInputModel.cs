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
        /// Get or set right answer explanation
        /// </summary>
        public string RightAnswerExplanation { get; set; }
        /// <summary>
        /// Identifier of selected category
        /// </summary>
        public int CategoryId { get; set; }
    }
}
