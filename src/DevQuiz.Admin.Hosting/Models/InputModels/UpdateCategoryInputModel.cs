using DevQuiz.Shared.Models;
using DevQuiz.Shared.Models.Abstractions;

namespace DevQuiz.Admin.Hosting.Models.InputModels
{
    /// <summary>
    ///     Input model for updating question category
    /// </summary>
    public class UpdateCategoryInputModel : CreateCategoryInputModel, IHasKey<int>
    {
        /// <inheritdoc cref="IHasKey{TKey}.Id" />
        public int Id { get; set; }
    }
}