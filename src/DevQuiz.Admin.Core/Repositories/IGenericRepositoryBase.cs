using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DevQuiz.Admin.Core.Repositories
{
    /// <summary>
    ///     Base generic repository interface
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    public interface IGenericRepositoryBase<TEntity>
        where TEntity : class
    {
        #region GetAll

        /// <summary>
        ///     Get all entities with filter
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <returns>IQueryable collection of entities</returns>
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);

        #endregion

        #region Update

        /// <summary>
        ///     Update entity information
        /// </summary>
        /// <param name="entityToUpdate">Entity information</param>
        void Update(TEntity entityToUpdate);

        #endregion

        #region GetOne

        /// <summary>
        ///     Get one entity
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <returns>One entity</returns>
        TEntity GetOne(Expression<Func<TEntity, bool>> predicate = null);

        #endregion

        #region List

        /// <summary>
        ///     Get list entities by passed expression
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <returns>List collection of entities</returns>
        List<TEntity> List(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        ///     Get list entities by passed expression
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List collection of entities</returns>
        Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate = null,
            CancellationToken cancellationToken = default);

        #endregion

        #region Count

        /// <summary>
        ///     Count of entities
        /// </summary>
        /// <returns>Number of entities in db</returns>
        int Count();

        /// <summary>
        ///     Count of entities with filter
        /// </summary>
        /// <param name="predicate">Filter for entities</param>
        /// <returns>Number of entities in db</returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Create

        /// <summary>
        ///     Create new entity
        /// </summary>
        /// <param name="entityToAdd">Entity to add model</param>
        void Create(TEntity entityToAdd);

        /// <summary>
        ///     Create new entity
        /// </summary>
        /// <param name="entityToAdd">Entity model</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task CreateAsync(TEntity entityToAdd, CancellationToken cancellationToken = default);

        #endregion

        #region Delete

        /// <summary>
        ///     Delete entity from the store
        /// </summary>
        /// <param name="entityToDelete">Entity instance</param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        ///     Delete entity from the store
        /// </summary>
        /// <param name="predicate">Searching function</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        #endregion
    }
}