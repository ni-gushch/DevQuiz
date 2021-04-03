using System;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data.DbContexts;
using DevQuiz.Libraries.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.Libraries.Data.Repositories
{
    /// <inheritdoc cref="IUserRepository{TUser,TKey}" />
    public class UserRepository : Repository<DevQuizDbContext, User, Guid>, IUserRepository<User, Guid>
    {
        private readonly DevQuizDbContext _devQuizDbContext;
        private readonly ILogger<UserRepository> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">DevQuizDbContext instance</param>
        /// <param name="logger">Logger instance</param>
        public UserRepository(DevQuizDbContext dbContext,
            ILogger<UserRepository> logger = null) : base(dbContext: dbContext)
        {
            _devQuizDbContext = dbContext;
            _logger = logger ?? NullLogger<UserRepository>.Instance;
        }

        /// <inheritdoc cref="IUserRepository{TUser,TKey}.GetOneAsync(TKey)" />
        public async Task<User> GetOneAsync(Guid entityId)
        {
            return await this.EntityDbSet.SingleOrDefaultAsync(it => it.Id.Equals(entityId));
        }
    }
}