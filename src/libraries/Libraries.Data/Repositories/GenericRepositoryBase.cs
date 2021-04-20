using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.Libraries.Data.Repositories
{
    /// <summary>
    /// Generic base repository implementation
    /// </summary>
    /// <typeparam name="TDbContext">TDBContext instance</typeparam>
    /// <typeparam name="TEntity">Entity instance</typeparam>
    public class GenericRepositoryBase<TDbContext, TEntity> : IGenericRepositoryBase<TEntity>
        where TDbContext : DbContext
        where TEntity : class
    {
        private readonly ILogger<GenericRepositoryEntityFramework<TDbContext, TEntity>> _logger;

        /// <summary>
        /// DbSet for current type of TEntity
        /// </summary>
        protected DbSet<TEntity> DbSet { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="genericDbContext">Generic db context</param>
        /// <param name="logger">Logger instance</param>
        public GenericRepositoryBase(TDbContext genericDbContext, ILogger<GenericRepositoryEntityFramework<TDbContext, TEntity>> logger = null)
        {
            DbSet = genericDbContext.Set<TEntity>();
            _logger = logger ?? NullLogger<GenericRepositoryEntityFramework<TDbContext, TEntity>>.Instance;
        }

        #region GetAll

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.GetAll()"/>
        public virtual IQueryable<TEntity> GetAll()
        {
            return GetAll(predicate: null);
        }
        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.GetAll(Expression{Func{TEntity, bool}})"/>
        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            var query = GetQueryableWithFilter(predicate);
            return query;
        }

        #endregion

        #region List

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.List()"/>
        public virtual List<TEntity> List()
        {
            return List(predicate: null);
        }
        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.List(Expression{Func{TEntity, bool}})"/>
        public virtual List<TEntity> List(Expression<Func<TEntity, bool>> predicate)
        {
            var query = GetQueryableWithFilter(predicate);
            return query.ToList();
        }

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.ListAsync(CancellationToken)"/>
        public virtual Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return ListAsync(predicate: null, cancellationToken: cancellationToken);
        }
        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.ListAsync(Expression{Func{TEntity, bool}}, CancellationToken)"/>
        public virtual Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var query = GetQueryableWithFilter(predicate);
            return query.ToListAsync(cancellationToken);
        }

        #endregion

        #region Count

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.Count()"/>
        public int Count()
        {
            return DbSet.Count();
        }
        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.Count(Expression{Func{TEntity, bool}})"/>
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Count(predicate);
        }

        #endregion

        #region Create

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.Create(TEntity)"/>
        public virtual void Create(TEntity entityToAdd)
        {
            DbSet.Add(entityToAdd);
        }
        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.CreateAsync(TEntity, CancellationToken)"/>
        public virtual async Task CreateAsync(TEntity entityToAdd, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entityToAdd, cancellationToken);
        }

        #endregion

        #region Update

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.Update(TEntity)"/>
        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Update(entityToUpdate);
        }

        #endregion

        #region Delete

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.Delete(TEntity)"/>
        public virtual void Delete(TEntity entityToDelete)
        {
            DbSet.Remove(entityToDelete);
        }

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.Delete(Expression{Func{TEntity, bool}})"/>
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entityToDelete = GetOne(predicate: predicate);
            DbSet.Remove(entityToDelete);
        }

        #endregion

        #region GetOne

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.GetOne(Expression{Func{TEntity, bool}})"/>
        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> predicate = null)
        {
            var baseQuery = GetQueryableWithFilter(predicate: predicate);

            return baseQuery.FirstOrDefault();
        }

        #endregion

        #region Protected

        /// <summary>
        /// Get query from passed params
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> GetQueryableWithFilter(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (predicate != null)
                query = query.Where(predicate);

            return query;
        }

        #endregion
    }
}
