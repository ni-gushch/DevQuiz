using DevQuiz.Admin.Core.Models;

namespace DevQuiz.Admin.Hosting.Models.InputModels
{
    /// <summary>
    /// Input model for update question
    /// </summary>
    public class UpdateQuestionInputModel : CreateQuestionInputModel, IHasKey<int>
    {
        /// <inheritdoc cref="IHasKey{TKey}.Id"/>
        public int Id { get; set; }
    }
}
