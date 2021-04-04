using System;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.Libraries.Data.Repositories
{
    /// <inheritdoc cref="IUserRepository{TUser,TKey}" />
    public class UserRepository<TUser, TKey> : Repository<DevQuizDbContext, TUser, TKey>, IUserRepository<TUser, TKey>
        where TUser : UserBase<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly DevQuizDbContext _devQuizDbContext;
        private readonly ILogger<UserRepository<TUser, TKey>> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">DevQuizDbContext instance</param>
        /// <param name="logger">Logger instance</param>
        public UserRepository(DevQuizDbContext dbContext,
            ILogger<UserRepository<TUser, TKey>> logger = null) : base(dbContext: dbContext)
        {
            _devQuizDbContext = dbContext;
            _logger = logger ?? NullLogger<UserRepository<TUser, TKey>>.Instance;
        }

        /// <inheritdoc cref="IUserRepository{TUser,TKey}.GetOneAsync(TKey)" />
        public async Task<TUser> GetOneAsync(TKey entityId)
        {
            return await this.EntityDbSet.SingleOrDefaultAsync(it => it.Id.Equals(entityId));
        }
    }
}