using System;
using Microsoft.EntityFrameworkCore;

namespace DevQuiz.Libraries.Data.DbContexts
{
    /// <summary>
    /// Factory for creating DbContexts
    /// </summary>
    public class DbFactory<TDbContext> : IDisposable
        where TDbContext : DbContext
    {
        private bool _disposed;
        private readonly Func<TDbContext> _instanceFunc;
        private TDbContext _dbContext;
        /// <summary>
        /// Get DbContext instance
        /// </summary>
        /// <returns></returns>
        public TDbContext DbContext => _dbContext ??= _instanceFunc.Invoke();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextFactory">Function for create DbContext instance</param>
        public DbFactory(Func<TDbContext> dbContextFactory)
        {
            _instanceFunc = dbContextFactory;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting <br/>
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if(!_disposed && _dbContext != null)
            {
                _disposed = true;
                _dbContext.Dispose();
            }
        }
    }
}