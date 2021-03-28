using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace DevQuiz.Libraries.Core.Repositories
{
    /// <summary>
    /// Unit of work interface
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Save all changes
        /// </summary>
        /// <returns>Operation status</returns>
        int SaveChanges();

        /// <summary>
        /// Save all changes
        /// </summary>
        /// <param name="cancellationToken">Token for cancel operation</param>
        /// <returns>Operation status</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Start transaction
        /// </summary>
        /// <param name="isolationLevel">Isolation level</param>
        /// <returns><see cref="IDisposable"/> object</returns>
        IDisposable BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        /// <summary>
        /// Start transaction
        /// </summary>
        /// <param name="isolationLevel">Isolation level</param>
        /// <param name="cancellationToken">Token for cancel operation</param>
        /// <returns><see cref="System.IDisposable"/> object</returns>
        Task<IDisposable> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default);

        /// <summary>
        /// Save all changes in transaction
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Save all changes in transaction
        /// </summary>
        /// <param name="cancellationToken">Token for cancel operation</param>
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    }
}