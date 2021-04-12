using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DevQuiz.Libraries.Data.Repositories
{
    /// <inheritdoc cref="IUnitOfWork" />
    public class UnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : DbContext
    {
        private DbFactory<TDbContext> _dbFactory;   
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbFactory">Factory for TDbContext instances</param>
        public UnitOfWork (DbFactory<TDbContext> dbFactory)   
        {   
            _dbFactory = dbFactory;   
        }   

        /// <inheritdoc cref="IUnitOfWork.Commit" />
        public int Commit()
        {
            return _dbFactory.DbContext.SaveChanges();
        }

        /// <inheritdoc cref="IUnitOfWork.CommitAsync" />
        public Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return _dbFactory.DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}