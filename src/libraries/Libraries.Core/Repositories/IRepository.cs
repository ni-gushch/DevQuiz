using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DevQuiz.Libraries.Core.Repositories
{
    /// <summary>
    /// Base repository interface
    /// </summary>
    public interface IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Get all entities query from the store
        /// </summary>
        /// <returns>List of all users</returns>
        IQueryable<TEntity> GetAll();
        /// <summary>
        /// Get list entities by passed expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryable<TEntity> List(Expression<Func<TEntity, bool>> expression); 
        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="entity">Entity model</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// Update entity information
        /// </summary>
        /// <param name="entity">Entity information</param>
        void Update(TEntity entity);
        /// <summary>
        /// Delete user from the store
        /// </summary>
        /// <param name="entity">Entity instance</param>
        void Delete(TEntity entity);
    }
}