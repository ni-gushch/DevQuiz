using System.ComponentModel.DataAnnotations;

namespace DevQuiz.TelegramBot.Models.InputModels
{
    /// <summary>
    /// Input model for creating new question tag
    /// </summary>
    public class CreateTagInputModel
    {
        /// <summary>
        /// Tag name
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
