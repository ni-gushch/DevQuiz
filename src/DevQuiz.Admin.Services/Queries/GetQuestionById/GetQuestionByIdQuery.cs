using DevQuiz.Admin.Core.Models;

namespace DevQuiz.Admin.Services.Queries
{
    /// <summary>
    /// Query to get information about question by id
    /// </summary>
    public class GetQuestionByIdQuery : IBaseQuery<GetQuestionByIdQueryResponse>, IHasKey<int>
    {
        /// <summary>
        /// Identifier of searched question
        /// </summary>
        public int Id { get; set; }
    }
}