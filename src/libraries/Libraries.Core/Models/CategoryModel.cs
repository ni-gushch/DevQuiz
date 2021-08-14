using DevQuiz.Libraries.Core.Models.Dto;

namespace DevQuiz.Libraries.Core.Models
{
    /// <summary>
    /// Model with base category info
    /// </summary>
    public class CategoryModel : DtoBase<int>
    {
        /// <summary>
        /// CAtegory name
        /// </summary>
        public string Name { get; set; }
    }
}