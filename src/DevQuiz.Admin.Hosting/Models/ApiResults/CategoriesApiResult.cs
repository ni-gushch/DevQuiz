using System.Collections.Generic;
using DevQuiz.Admin.Core.Models;

namespace DevQuiz.Admin.Hosting.Models.ApiResults
{
    /// <summary>
    ///     ApiResult for represent categories collection
    /// </summary>
    public class CategoriesApiResult
    {
        /// <summary>
        ///     Available categories collection
        /// </summary>
        public List<CategoryModel> Categories { get; set; }
    }
}