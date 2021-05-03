using System.ComponentModel.DataAnnotations;

namespace DevQuiz.TelegramBot.Models.InputModels
{
    /// <summary>
    /// Input model for creating new question category
    /// </summary>
    public class CreateCategoryInputModel
    {
        /// <summary>
        /// Category name
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
