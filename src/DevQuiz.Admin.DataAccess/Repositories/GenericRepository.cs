using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Admin.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.Admin.DataAccess.Repositories
{
    /// <summary>
    ///     Base repository class
    /// </summary>
    public class GenericRepository<TDbContext, TEntity> : GenericRepositoryBase<TDbContext, TEntity>,
        IGenericRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : class
    {
        private readonly TDbContext _genericDbContext;
        private readonly ILogger<GenericRepository<TDbContext, TEntity>> _logger;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="genericDbContext">Generic db context</param>
        /// <param name="logger">Logger instance</param>
        public GenericRepository(TDbContext genericDbContext,
            ILogger<GenericRepository<TDbContext, TEntity>> logger = null) : base(genericDbContext, logger)
        {
            _genericDbContext = genericDbContext;
            _logger = logger ?? NullLogger<GenericRepository<TDbContext, TEntity>>.Instance;
        }

        #region GetAll

        /// <inheritdoc
        ///     cref="IGenericRepository{TEntity}.GetAll(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}, int?, int?)" />
        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null)
        {
            return GetQueryable(predicate, include, orderBy, skip, take);
        }

        #endregion

        #region Private

        private IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return query;
        }

        #endregion

        #region List

        /// <inheritdoc
        ///     cref="IGenericRepository{TEntity}.List(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}, int?, int?)" />
        public virtual List<TEntity> List(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null)
        {
            return GetQueryable(predicate, include, orderBy, skip, take)
                .ToList();
        }


        /// <inheritdoc
        ///     cref="IGenericRepository{TEntity}.ListAsync(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}, int?, int?, CancellationToken)" />
        public virtual Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default)
        {
            return GetQueryable(predicate, include, orderBy, skip, take)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region GetOne

        /// <inheritdoc
        ///     cref="IGenericRepository{TEntity}.GetOne(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}})" />
        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            return GetQueryable(predicate, include)
                .FirstOrDefault();
        }

        /// <inheritdoc
        ///     cref="IGenericRepository{TEntity}.GetOneAsync(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, CancellationToken)" />
        public virtual Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            CancellationToken cancellationToken = default)
        {
            return GetQueryable(predicate, include)
                .FirstOrDefaultAsync(cancellationToken);
        }

        #endregion
    }
}