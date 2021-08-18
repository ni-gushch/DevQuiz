using DevQuiz.Admin.Client.UI.Models.InputModels;
using DevQuiz.Shared.Models;
using DevQuiz.Shared.Models.Abstractions;

namespace DevQuiz.Admin.Hosting.Models.InputModels
{
    /// <summary>
    ///     Input model for update question
    /// </summary>
    public class UpdateQuestionInputModel : CreateQuestionInputModel, IHasKey<int>
    {
        /// <inheritdoc cref="IHasKey{TKey}.Id" />
        public int Id { get; set; }
    }
}