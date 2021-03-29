using System;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data.DbContexts;
using DevQuiz.Libraries.Data.Models;
using Microsoft.EntityFrameworkCore;

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

        /// <inheritdoc cref="IUserRepository{TUser,TKey}.GetOneAsync(Guid)" />
        public async Task<User> GetOneAsync(Guid entityId)
        {
            return await this.EntityDbSet.SingleOrDefaultAsync(it => it.Id.Equals(entityId));
        }
    }
}