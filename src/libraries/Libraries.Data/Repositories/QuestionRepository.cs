using System.Linq;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.Libraries.Data.Repositories
{
    /// <inheritdoc cref="IQuestionRepository{TQuestion, TAnswer, TCategory, TTag}" />
    public class QuestionRepository<TDbContext, TQuestion, TAnswer, TCategory, TTag> : GenericRepository<TDbContext, TQuestion>,
        IQuestionRepository<TQuestion, TAnswer, TCategory, TTag>
        where TDbContext : DbContext
        where TQuestion : QuestionBase<TAnswer, TCategory, TTag>
        where TAnswer : AnswerBase
        where TCategory : CategoryBase<TQuestion>
        where TTag : TagBase<TQuestion>
    {
        private readonly ILogger<QuestionRepository<TDbContext, TQuestion, TAnswer, TCategory, TTag>> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbFactory">TDbContext factory instance</param>
        /// <param name="logger">Logger instance</param>
        public QuestionRepository(DbFactory<TDbContext> dbFactory,
            ILogger<QuestionRepository<TDbContext, TQuestion, TAnswer, TCategory, TTag>> logger = null) : base(dbFactory: dbFactory, logger)
        {
            _logger = logger ?? NullLogger<QuestionRepository<TDbContext, TQuestion, TAnswer, TCategory, TTag>>.Instance;
        }

        /// <inheritdoc cref="GenericRepository{TDbContext, TUser}.GetAll()" />
        public override IQueryable<TQuestion> GetAll()
        {
            return base.GetAll()
                .Include(it => it.Answers)
                .Include(it => it.Category)
                .Include(it => it.Tags);
        }
    }
}