using DevQuiz.Libraries.Core.Models.Entities;

namespace DevQuiz.Libraries.Core.Repositories
{
    /// <summary>
    /// Repository for manage DevQuiz question entities
    /// </summary>
    /// <typeparam name="TQuestion">Question model</typeparam>
    /// <typeparam name="TAnswer">Question answer model</typeparam>
    /// <typeparam name="TCategory">Question category model</typeparam>
    /// <typeparam name="TTag">Question tag model</typeparam>
    public interface IQuestionRepository<TQuestion, TAnswer, TCategory, TTag> : IRepository<TQuestion>
        where TQuestion : QuestionBase<TAnswer, TCategory, TTag>
        where TAnswer : AnswerBase
        where TCategory : CategoryBase<TQuestion>
        where TTag : TagBase<TQuestion>
    {
         
    }
}