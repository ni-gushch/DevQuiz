using System;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data.DbContexts;
using DevQuiz.Libraries.Data.Models;

namespace DevQuiz.Libraries.Data.Repositories
{
    /// <inheritdoc cref="IUserRepository{TUser,TKey}" />
    public class UserRepository : Repository<DevQuizDbContext, User, Guid>, IUserRepository<User, Guid>
    {
        private readonly DevQuizDbContext _devQuizDbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">DevQuizDbContext instance</param>
        public UserRepository(DevQuizDbContext dbContext) : base(dbContext: dbContext)
        {
            _devQuizDbContext = dbContext;
        }

        /// <inheritdoc cref="IUserRepository{TUser,TKey}.GetOne" />
        public User GetOne()
        {
            throw new NotImplementedException();
        }
    }
}