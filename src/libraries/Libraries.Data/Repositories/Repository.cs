using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevQuiz.Libraries.Data.Repositories
{
    /// <summary>
    /// Base repository class
    /// </summary>
    public class Repository<TDbContext, TEntity, TKey> : IRepository<TEntity, TKey>
        where TDbContext : DbContext, IUnitOfWork
        where TEntity : AggregateEntity<TKey>
    {
        private readonly TDbContext _dbContext;

        /// <summary>
        /// Get db set for entity
        /// </summary>
        /// <returns>DbSet of entities</returns>
        protected DbSet<TEntity> EntityDbSet => _dbContext.Set<TEntity>();
        
        /// <inheritdoc cref="IRepository{TEntity, TKey}.UnitOfWork" />
        public IUnitOfWork UnitOfWork => _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">Db context</param>
        public Repository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc cref="IRepository{TEntity, TKey}.GetAll" />
        public IQueryable<TEntity> GetAll() => _dbContext.Set<TEntity>();

        /// <inheritdoc cref="IRepository{TEntity, TKey}.CreateAsync(TEntity, CancellationToken)" />
        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.CreatedTime = DateTime.Now;
            var result = await EntityDbSet.AddAsync(entity, cancellationToken);
            
            return result.Entity;
        }       

        /// <inheritdoc cref="IRepository{TEntity, TKey}.Update(TEntity)" />
        public TEntity Update(TEntity entity)
        {
            entity.UpdatedTime = DateTime.Now;
            var result = EntityDbSet.Update(entity);
            
            return result.Entity;
        }

        /// <inheritdoc cref="IRepository{TEntity, TKey}.Delete(TEntity)" />
        public void Delete(TEntity entity)
        {
            EntityDbSet.Remove(entity);
        }
    }
}