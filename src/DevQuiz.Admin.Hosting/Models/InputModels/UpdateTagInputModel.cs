using DevQuiz.Shared.Models;
using DevQuiz.Shared.Models.Abstractions;

namespace DevQuiz.Admin.Hosting.Models.InputModels
{
    /// <summary>
    ///     Input model for update question tag
    /// </summary>
    public class UpdateTagInputModel : CreateTagInputModel, IHasKey<int>
    {
        /// <inheritdoc cref="IHasKey{TKey}.Id" />
        public int Id { get; set; }
    }
}