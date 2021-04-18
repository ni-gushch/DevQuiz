using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.Libraries.Data.Repositories
{
    /// <summary>
    /// Base repository class
    /// </summary>
    public class GenericRepositoryEntityFramework<TDbContext, TEntity> : IGenericRepositoryEntityFramework<TEntity>
        where TDbContext : DbContext
        where TEntity : class
    {
        private readonly TDbContext _genericDbContext;
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
        public GenericRepositoryEntityFramework(TDbContext genericDbContext, ILogger<GenericRepositoryEntityFramework<TDbContext, TEntity>> logger = null)
        {
            _genericDbContext = genericDbContext;
            DbSet = genericDbContext.Set<TEntity>();
            _logger = logger ?? NullLogger<GenericRepositoryEntityFramework<TDbContext, TEntity>>.Instance;
        }

        #region GetAll

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.GetAll()"/>
        public virtual IQueryable<TEntity> GetAll()
        {
            return GetAll(predicate: null, include: null, orderBy: null, skip: null, take: null);
        }
        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.GetAll(Expression{Func{TEntity, bool}})"/>
        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll(predicate: predicate, include: null, orderBy: null, skip: null, take: null);
        }
        /// <inheritdoc cref="IGenericRepositoryEntityFramework{TEntity}.GetAll(Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}})"/>
        public virtual IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            return GetAll(predicate: null, include: include, orderBy: null, skip: null, take: null);
        }
        /// <inheritdoc cref="IGenericRepositoryEntityFramework{TEntity}.GetAll(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}})"/>
        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            return GetAll(predicate: predicate, include: include, orderBy: null, skip: null, take: null);
        }
        /// <inheritdoc cref="IGenericRepositoryEntityFramework{TEntity}.GetAll(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}, int?, int?)"/>
        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            int? skip = null,
            int? take = null)
        {
            var baseQuery = GetQueryable(predicate, include);

            if (orderBy != null)
                baseQuery = orderBy(baseQuery);
            
            if (skip.HasValue)
                baseQuery = baseQuery.Skip(skip.Value);

            if (take.HasValue)
                baseQuery = baseQuery.Take(take.Value);

            return baseQuery;
        }

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.GetAllAsync(CancellationToken)"/>
        public virtual Task<IQueryable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return GetAllAsync(predicate: null, include: null, orderBy: null, skip: null, take: null, cancellationToken: cancellationToken);
        }
        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.GetAllAsync(Expression{Func{TEntity, bool}}, CancellationToken)"/>
        public virtual Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return GetAllAsync(predicate: predicate, include: null, orderBy: null, skip: null, take: null, cancellationToken: cancellationToken);
        }
        /// <inheritdoc cref="IGenericRepositoryEntityFramework{TEntity}.GetAllAsync(Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, CancellationToken)"/>
        public virtual Task<IQueryable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            CancellationToken cancellationToken = default)
        {
            return GetAllAsync(predicate: null, include: include, orderBy: null, skip: null, take: null, cancellationToken: cancellationToken);
        }
        /// <inheritdoc cref="IGenericRepositoryEntityFramework{TEntity}.GetAllAsync(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, CancellationToken)"/>
        public virtual Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            CancellationToken cancellationToken = default)
        {
            return GetAllAsync(predicate: predicate, include: include, orderBy: null, skip: null, take: null, cancellationToken: cancellationToken);
        }
        /// <inheritdoc cref="IGenericRepositoryEntityFramework{TEntity}.GetAllAsync(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}, int?, int?, CancellationToken)"/>
        public virtual Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int? skip = null, int? take = null,
            CancellationToken cancellationToken = default)
        {
            var query = GetQueryable(predicate, include);

            if (orderBy != null)
                query = orderBy(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return new Task<IQueryable<TEntity>>(() => query, cancellationToken);
        }

        #endregion

        #region List

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.List()"/>
        public virtual List<TEntity> List()
        {
            return List(predicate: null, include: null, orderBy: null, skip: null, take: null);
        }
        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.List(Expression{Func{TEntity, bool}})"/>
        public virtual List<TEntity> List(Expression<Func<TEntity, bool>> predicate)
        {
            return List(predicate: predicate, include: null, orderBy: null, skip: null, take: null);
        }
        /// <inheritdoc cref="IGenericRepositoryEntityFramework{TEntity}.List(Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}})"/>
        public virtual List<TEntity> List(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            return List(predicate: null, include: include, orderBy: null, skip: null, take: null);
        }
        /// <inheritdoc cref="IGenericRepositoryEntityFramework{TEntity}.List(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}})"/>
        public virtual List<TEntity> List(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            return List(predicate: predicate, include: include, orderBy: null, skip: null, take: null);
        }
        /// <inheritdoc cref="IGenericRepositoryEntityFramework{TEntity}.List(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}, int?, int?)"/>
        public virtual List<TEntity> List(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            int? skip = null,
            int? take = null)
        {
            var query = GetQueryable(predicate, include);

            if (orderBy != null)
                query = orderBy(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return query.ToList();
        }

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.ListAsync(CancellationToken)"/>
        public virtual Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return ListAsync(predicate: null, include: null, orderBy: null, skip: null, take: null,
                cancellationToken: cancellationToken);
        }
        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.ListAsync(Expression{Func{TEntity, bool}}, CancellationToken)"/>
        public virtual Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return ListAsync(predicate: predicate, include: null, orderBy: null, skip: null, take: null,
                cancellationToken: cancellationToken);
        }
        /// <inheritdoc cref="IGenericRepositoryEntityFramework{TEntity}.ListAsync(Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, CancellationToken)"/>
        public virtual Task<List<TEntity>> ListAsync(Func<IQueryable<TEntity>,
            IIncludableQueryable<TEntity, object>> include,
            CancellationToken cancellationToken = default)
        {
            return ListAsync(predicate: null, include: include, orderBy: null, skip: null, take: null,
                cancellationToken: cancellationToken);
        }
        /// <inheritdoc cref="IGenericRepositoryEntityFramework{TEntity}.ListAsync(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, CancellationToken)"/>
        public virtual Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            CancellationToken cancellationToken = default)
        {
            return ListAsync(predicate: predicate, include: include, orderBy: null, skip: null, take: null,
                cancellationToken: cancellationToken);
        }
        /// <inheritdoc cref="IGenericRepositoryEntityFramework{TEntity}.ListAsync(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}, int?, int?, CancellationToken)"/>
        public virtual Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default)
        {
            var query = GetQueryable(predicate, include);

            if (orderBy != null)
                query = orderBy(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

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

        /// <inheritdoc cref="IGenericRepositoryEntityFramework{TEntity}.GetOne(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}})"/>
        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var baseQuery = GetQueryable(predicate: predicate, include: include);

            return baseQuery.FirstOrDefault();
        }
        /// <inheritdoc cref="IGenericRepositoryEntityFramework{TEntity}.GetOneAsync(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, CancellationToken)"/>
        public virtual Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, CancellationToken cancellationToken = default)
        {
            var baseQuery = GetQueryable(predicate: predicate, include: include);

            return baseQuery.FirstOrDefaultAsync(cancellationToken);
        }

        #endregion

        #region Private

        private IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            return query;
        }
        #endregion
    }
}