using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevQuiz.Libraries.Core.Repositories
{
    /// <summary>
    /// Base repository interface
    /// </summary>
    public interface IRepository<TEntity, TKey>
    {
        /// <summary>
        /// Unit of work
        /// </summary>
        /// <value>Unit of work instance</value>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Get all entities query from the store
        /// </summary>
        /// <returns>List of all users</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="entity">Entity model</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>New entity information</returns>
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update entity information
        /// </summary>
        /// <param name="entity">Entity information</param>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Delete user from the store
        /// </summary>
        /// <param name="entity">Entity instance</param>
        /// <returns>Operation status</returns>
        void Delete(TEntity entity);
    }
}