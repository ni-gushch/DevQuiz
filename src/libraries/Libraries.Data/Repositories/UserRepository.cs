using System;
using System.Linq;
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
    public class UserRepository<TDbContext, TUser, TKey> : Repository<TDbContext, TUser>, IUserRepository<TUser, TKey>
        where TDbContext : DbContext
        where TUser : UserBase<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly DevQuizDbContext _devQuizDbContext;
        private readonly ILogger<UserRepository<TDbContext, TUser, TKey>> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbFactory">TDbContext factory instance</param>
        /// <param name="logger">Logger instance</param>
        public UserRepository(DbFactory<TDbContext> dbFactory,
            ILogger<UserRepository<TDbContext, TUser, TKey>> logger = null) : base(dbFactory: dbFactory, logger)
        {
            _logger = logger ?? NullLogger<UserRepository<TDbContext, TUser, TKey>>.Instance;
        }

        /// <inheritdoc cref="Repository{TDbContext, TUser}.GetAll()" />
        public override IQueryable<TUser> GetAll()
        {
            return base.GetAll();
        }

        /// <inheritdoc cref="IUserRepository{TUser,TKey}.GetByIdAsync(TKey)" />
        public async Task<TUser> GetByIdAsync(TKey entityId)
        {
            return await this.DbSet.SingleOrDefaultAsync(it => it.Id.Equals(entityId));
        }

        /// <inheritdoc cref="IUserRepository{TUser,TKey}.GetByIdAsync(TKey)" />
        public async Task<TUser> GetByChatIdAsync(int telegramChatId)
        {
            return await this.DbSet.SingleOrDefaultAsync(it => it.TelegramChatId.Equals(telegramChatId));
        }
    }
}