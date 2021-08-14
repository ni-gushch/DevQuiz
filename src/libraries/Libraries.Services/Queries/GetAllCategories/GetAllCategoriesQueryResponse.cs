using System.Collections.Generic;
using DevQuiz.Libraries.Core.Models;

namespace DevQuiz.Libraries.Services.Queries
{
    /// <summary>
    /// Response from get all categories info
    /// </summary>
    public class GetAllCategoriesQueryResponse
    {
        public List<CategoryModel> Categories { get; set; }
    }
}