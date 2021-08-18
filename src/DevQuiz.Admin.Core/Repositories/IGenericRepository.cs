using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace DevQuiz.Admin.Core.Repositories
{
    /// <summary>
    ///     Generic repository
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    public interface IGenericRepository<TEntity> : IGenericRepositoryBase<TEntity>
        where TEntity : class
    {
        #region GetAll

        /// <summary>
        ///     Get all entities with filter order and paging and included tables
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="include">Tables to include</param>
        /// <param name="orderBy">Order by function</param>
        /// <param name="skip">Number of skip entities</param>
        /// <param name="take">Number of take entities</param>
        /// <returns>IQueryable collection of entities</returns>
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null);

        #endregion

        #region List

        /// <summary>
        ///     Get all entities with filter order and paging and included tables
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="include">Tables to include</param>
        /// <param name="orderBy">Order by function</param>
        /// <param name="skip">Number of skip entities</param>
        /// <param name="take">Number of take entities</param>
        /// <returns>List of entities</returns>
        List<TEntity> List(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null);

        /// <summary>
        ///     Get all entities with filter order and paging and included tables
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="include">Tables to include</param>
        /// <param name="orderBy">Order by function</param>
        /// <param name="skip">Number of skip entities</param>
        /// <param name="take">Number of take entities</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of entities</returns>
        Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null,
            CancellationToken cancellationToken = default);

        #endregion

        #region GetOne

        /// <summary>
        ///     Get one entity
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="include">Tables to include</param>
        /// <returns>One entity</returns>
        TEntity GetOne(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        /// <summary>
        ///     Get one entity
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="include">Tables to include</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>One entity</returns>
        Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            CancellationToken cancellationToken = default);

        #endregion
    }
}