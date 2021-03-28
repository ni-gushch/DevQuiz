using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data.DbContexts;

namespace DevQuiz.Libraries.Data.Repositories
{
    /// <inheritdoc cref="IQuestionRepository{TQuestion, TAnswer, TCategory, TTag}" />
    public class QuestionRepository<TQuestion, TAnswer, TCategory, TTag> : Repository<DevQuizDbContext, TQuestion, int>, 
        IQuestionRepository<TQuestion, TAnswer, TCategory, TTag>
        where TQuestion : QuestionBase<TAnswer, TCategory, TTag>
        where TAnswer : AnswerBase
        where TCategory : CategoryBase<TQuestion>
        where TTag : TagBase<TQuestion>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">instance of DevQuizDbContext</param>
        public QuestionRepository(DevQuizDbContext dbContext) : base(dbContext: dbContext)
        {
            
        }
    }
}