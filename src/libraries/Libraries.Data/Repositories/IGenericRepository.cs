using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Repositories;
using Microsoft.EntityFrameworkCore.Query;

namespace DevQuiz.Libraries.Data.Repositories
{
    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    public interface IGenericRepository<TEntity> : IGenericRepositoryBase<TEntity>
        where TEntity : class
    {
        #region GetAll

        /// <summary>
        /// Get all entities and included tables
        /// </summary>
        /// <param name="include">Tables to include</param>
        /// <returns>IQueryable collection of entities</returns>
        IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include);
        /// <summary>
        /// Get all entities with filter and included tables
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="include">Tables to include</param>
        /// <returns>IQueryable collection of entities</returns>
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include);
        /// <summary>
        /// Get all entities with filter order and paging and included tables
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="include">Tables to include</param>
        /// <param name="orderBy">Order by function</param>
        /// <param name="skip">Number of skip entities</param>
        /// <param name="take">Number of take entities</param>
        /// <returns>IQueryable collection of entities</returns>
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
           int? skip = null, int? take = null);
        /// <summary>
        /// Get all entities with filter order and paging and included tables
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="include">Tables to include</param>
        /// <param name="orderBy">Name of the field by which you want to sort</param>
        /// <param name="orderDirection">Order direction</param>
        /// <param name="skip">Number of skip entities</param>
        /// <param name="take">Number of take entities</param>
        /// <returns>IQueryable collection of entities</returns>
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            string orderBy,
            string orderDirection = "asc",
            int? skip = null,
            int? take = null);

        /// <summary>
        /// Get all entities and included tables
        /// </summary>
        /// <param name="include">Tables to include</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>IQueryable collection of entities</returns>
        Task<IQueryable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, 
            IIncludableQueryable<TEntity, object>> include,
            CancellationToken cancellationToken = default);
        /// <summary>
        /// Get all entities and included tables
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="include">Tables to include</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>IQueryable collection of entities</returns>
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            CancellationToken cancellationToken = default);
        /// <summary>
        /// Get all entities with filter order and paging and included tables
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="include">Tables to include</param>
        /// <param name="orderBy">Order by function</param>
        /// <param name="skip">Number of skip entities</param>
        /// <param name="take">Number of take entities</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>IQueryable collection of entities</returns>
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default);
        /// <summary>
        /// Get all entities with filter order and paging and included tables
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="include">Tables to include</param>
        /// <param name="orderBy">Name of the field by which you want to sort</param>
        /// <param name="orderDirection">Order direction</param>
        /// <param name="skip">Number of skip entities</param>
        /// <param name="take">Number of take entities</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>IQueryable collection of entities</returns>
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
             Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
             string orderBy,
             string orderDirection = "asc",
             int? skip = null,
             int? take = null,
             CancellationToken cancellationToken = default);

        #endregion

        #region List

        /// <summary>
        /// Get all entities and included tables
        /// </summary>
        /// <param name="include">Tables to include</param>
        /// <returns>List of entities</returns>
        List<TEntity> List(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include);
        /// <summary>
        /// Get all entities with filter and included tables
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="include">Tables to include</param>
        /// <returns>List of entities</returns>
        List<TEntity> List(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include);
        /// <summary>
        /// Get all entities with filter order and paging and included tables
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="include">Tables to include</param>
        /// <param name="orderBy">Order by function</param>
        /// <param name="skip">Number of skip entities</param>
        /// <param name="take">Number of take entities</param>
        /// <returns>List of entities</returns>
        List<TEntity> List(Expression<Func<TEntity, bool>> predicate,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
           int? skip = null, int? take = null);
        /// <summary>
        /// Get all entities with filter order and paging and included tables
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="include">Tables to include</param>
        /// <param name="orderBy">Name of the field by which you want to sort</param>
        /// <param name="orderDirection">Order direction</param>
        /// <param name="skip">Number of skip entities</param>
        /// <param name="take">Number of take entities</param>
        /// <returns>List of entities</returns>
        List<TEntity> List(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            string orderBy,
            string orderDirection = "asc",
            int? skip = null,
            int? take = null);

        /// <summary>
        /// Get all entities and included tables
        /// </summary>
        /// <param name="include">Tables to include</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of entities</returns>
        List<TEntity> ListAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            CancellationToken cancellationToken = default);
        /// <summary>
        /// Get all entities with filter and included tables
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="include">Tables to include</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of entities</returns>
        List<TEntity> ListAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            CancellationToken cancellationToken = default);
        /// <summary>
        /// Get all entities with filter order and paging and included tables
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="include">Tables to include</param>
        /// <param name="orderBy">Order by function</param>
        /// <param name="skip">Number of skip entities</param>
        /// <param name="take">Number of take entities</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of entities</returns>
        List<TEntity> ListAsync(Expression<Func<TEntity, bool>> predicate,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
           int? skip = null, int? take = null,
           CancellationToken cancellationToken = default);
        /// <summary>
        /// Get all entities with filter order and paging and included tables
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="include">Tables to include</param>
        /// <param name="orderBy">Name of the field by which you want to sort</param>
        /// <param name="orderDirection">Order direction</param>
        /// <param name="skip">Number of skip entities</param>
        /// <param name="take">Number of take entities</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of entities</returns>
        List<TEntity> ListAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            string orderBy,
            string orderDirection = "asc",
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default);

        #endregion

        #region GetOne

        /// <summary>
        /// Get one entity
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="include">Tables to include</param>
        /// <returns>One entity</returns>
        TEntity GetOne(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        /// <summary>
        /// Get one entity
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