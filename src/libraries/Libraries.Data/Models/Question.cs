using DevQuiz.Libraries.Core.Models.Entities;

namespace DevQuiz.Libraries.Data.Models
{
    /// <summary>
    /// Question model
    /// </summary>
    public class Question : QuestionBase<Answer, Category, Tag>
    {
    }
}