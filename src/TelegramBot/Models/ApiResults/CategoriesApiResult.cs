using System.Collections.Generic;
using DevQuiz.Libraries.Core.Models;

namespace DevQuiz.TelegramBot.Models.ApiResults
{
    /// <summary>
    /// ApiResult for represent categories collection
    /// </summary>
    public class CategoriesApiResult
    {
        /// <summary>
        /// Available categories collection
        /// </summary>
        public List<CategoryModel> Categories { get; set; }
    }
}
