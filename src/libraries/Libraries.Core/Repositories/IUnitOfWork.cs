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
        int Commit();

        /// <summary>
        /// Save all changes
        /// </summary>
        /// <param name="cancellationToken">Token for cancel operation</param>
        /// <returns>Operation status</returns>
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}