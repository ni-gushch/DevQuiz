using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.Libraries.Data.Repositories
{
    /// <summary>
    /// Base repository class
    /// </summary>
    public class Repository<TDbContext, TEntity> : IRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : class
    {
        private readonly DbFactory<TDbContext> _dbFactory;
        private readonly DbSet<TEntity> _dbSet;
        private readonly ILogger<Repository<TDbContext, TEntity>> _logger;
        /// <summary>
        /// DbSet for current type of TEntity
        /// </summary>
        protected DbSet<TEntity> DbSet => _dbSet ?? _dbFactory.DbContext.Set<TEntity>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbFactory">Factory for creating Db context</param>
        /// <param name="logger">Logger instance</param>
        public Repository(DbFactory<TDbContext> dbFactory, ILogger<Repository<TDbContext, TEntity>> logger = null)
        {
            _dbFactory = dbFactory;
            _logger = logger ?? NullLogger<Repository<TDbContext, TEntity>>.Instance;
        }

        /// <inheritdoc cref="IRepository{TEntity}.GetAll" />
        public virtual IQueryable<TEntity> GetAll() => DbSet;

        /// <inheritdoc cref="IRepository{TEntity}.List(Expression{Func{TEntity, bool}})" />
        public virtual IQueryable<TEntity> List(Expression<Func<TEntity, bool>> expression)
        {
            return DbSet.Where(expression);
        }

        /// <inheritdoc cref="IRepository{TEntity}.CreateAsync(TEntity, CancellationToken)" />
        public virtual async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace($"Start create entry {typeof(TEntity)} in repository");
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(TEntity)))   
            {   
                _logger.LogTrace("Set creation date in creating entry");
                ((IAuditEntity)entity).CreatedDate = DateTime.UtcNow;   
            }   
            await DbSet.AddAsync(entity, cancellationToken);  
        }       

        /// <inheritdoc cref="IRepository{TEntity}.Update(TEntity)" />
        public virtual void Update(TEntity entity)
        {
            _logger.LogTrace($"Start update entry {typeof(TEntity)} in repository");
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(TEntity)))   
            {   _logger.LogTrace("Set update date in updating entry");
                ((IAuditEntity)entity).UpdatedDate = DateTime.UtcNow;   
            }   
            DbSet.Update(entity);  
        }

        /// <inheritdoc cref="IRepository{TEntity}.Delete(TEntity)" />
        public virtual void Delete(TEntity entity)
        {
            _logger.LogTrace($"Start delete entry {typeof(TEntity)} in repository");
            DbSet.Remove(entity);
        }
    }
}