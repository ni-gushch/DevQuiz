using DevQuiz.Libraries.Core.Models;

namespace DevQuiz.TelegramBot.Models.InputModels
{
    /// <summary>
    /// Input model for updating question category
    /// </summary>
    public class UpdateCategoryInputModel : CreateCategoryInputModel, IHasKey<int>
    {
        /// <inheritdoc cref="IHasKey{TKey}.Id"/>
        public int Id { get; set; }
    }
}
