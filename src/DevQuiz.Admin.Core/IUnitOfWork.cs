using System;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Admin.Core.Repositories;

namespace DevQuiz.Admin.Core
{
    /// <summary>
    ///     Unit of work interface
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///     Save all changes
        /// </summary>
        /// <returns>Operation status</returns>
        int Commit();

        /// <summary>
        ///     Save all changes
        /// </summary>
        /// <param name="cancellationToken">Token for cancel operation</param>
        /// <returns>Operation status</returns>
        Task<int> CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Clear db context tracker
        ///     <remarks>Need if you don't want Dispose DbContext but want change tracked elements</remarks>
        /// </summary>
        void ClearChangeTracker();

        /// <summary>
        ///     Get base repository instance
        /// </summary>
        /// <typeparam name="TEntity">Type of TEntity</typeparam>
        /// <returns>Base repository instance</returns>
        IGenericRepositoryBase<TEntity> GetBaseRepository<TEntity>() where TEntity : class;

        /// <summary>
        ///     Get instance of concrete repository
        /// </summary>
        /// <typeparam name="TRepository">Type of repository</typeparam>
        /// <typeparam name="TEntity">Type of TEntity</typeparam>
        /// <returns>Concrete repository instance</returns>
        TRepository GetRepository<TRepository, TEntity>()
            where TRepository : IGenericRepositoryBase<TEntity>
            where TEntity : class;
    }
}