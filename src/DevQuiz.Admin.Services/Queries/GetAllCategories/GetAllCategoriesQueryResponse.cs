using System.Collections.Generic;
using DevQuiz.Admin.Core.Models;

namespace DevQuiz.Admin.Services.Queries
{
    /// <summary>
    /// Response from get all categories info
    /// </summary>
    public class GetAllCategoriesQueryResponse
    {
        /// <summary>
        /// Categories collection
        /// </summary>
        public List<CategoryModel> Categories { get; set; }
    }
}