using DevQuiz.Libraries.Core.Models;

namespace DevQuiz.TelegramBot.Models.ApiResults
{
    /// <summary>
    /// ApiResult for represent question category information
    /// </summary>
    public class CategoriesApiResult : IHasKey<int>
    {
        /// <inheritdoc cref="IHasKey{TKey}.Id"/>
        public int Id { get; set; }
        /// <summary>
        /// Category name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Questions in category
        /// </summary>
        public ValueModel<int> Questions { get; set; }
    }
}
