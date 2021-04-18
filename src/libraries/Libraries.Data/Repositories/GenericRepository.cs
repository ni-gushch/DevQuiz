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
    public class GenericRepository<TDbContext, TEntity> : IGenericRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : class
    {
        private readonly TDbContext _genericDbContext;
        private readonly ILogger<GenericRepository<TDbContext, TEntity>> _logger;

        /// <summary>
        /// DbSet for current type of TEntity
        /// </summary>
        protected DbSet<TEntity> DbSet { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="genericDbContext">Generic db context</param>
        /// <param name="logger">Logger instance</param>
        public GenericRepository(TDbContext genericDbContext, ILogger<GenericRepository<TDbContext, TEntity>> logger = null)
        {
            _genericDbContext = genericDbContext;
            DbSet = genericDbContext.Set<TEntity>();
            _logger = logger ?? NullLogger<GenericRepository<TDbContext, TEntity>>.Instance;
        }

        #region GetAll

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.GetAll()"/>
        public IQueryable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.GetAll(Expression{Func{TEntity, bool}})"/>
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepository{TEntity}.GetAll(Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}})"/>
        public IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepository{TEntity}.GetAll(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}})"/>
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepository{TEntity}.GetAll(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}, int?, int?)"/>
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            int? skip = null,
            int? take = null)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepository{TEntity}.GetAll(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, string, string, int?, int?)"/>
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            string orderBy,
            string orderDirection = "asc",
            int? skip = null,
            int? take = null)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.GetAllAsync()"/>
        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.GetAllAsync(Expression{Func{TEntity, bool}})"/>
        public Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepository{TEntity}.GetAllAsync(Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, CancellationToken)"/>
        public Task<IQueryable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepository{TEntity}.GetAllAsync(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, CancellationToken)"/>
        public Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepository{TEntity}.GetAllAsync(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}, int?, int?, CancellationToken)"/>
        public Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int? skip = null, int? take = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepository{TEntity}.GetAllAsync(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, string, string, int?, int?, CancellationToken)"/>
        public Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, string orderBy, string orderDirection = "asc", int? skip = null,
            int? take = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region List

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.List()"/>
        public List<TEntity> List()
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.List(Expression{Func{TEntity, bool}})"/>
        public List<TEntity> List(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepository{TEntity}.List(Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}})"/>
        public List<TEntity> List(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepository{TEntity}.List(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}})"/>
        public List<TEntity> List(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepository{TEntity}.List(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}, int?, int?)"/>
        public List<TEntity> List(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            int? skip = null,
            int? take = null)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepository{TEntity}.List(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, string, string, int?, int?)"/>
        public List<TEntity> List(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            string orderBy,
            string orderDirection = "asc",
            int? skip = null,
            int? take = null)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.ListAsync()"/>
        public List<TEntity> ListAsync()
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.ListAsync(Expression{Func{TEntity, bool}})"/>
        public List<TEntity> ListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepository{TEntity}.ListAsync(Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, CancellationToken)"/>
        public List<TEntity> ListAsync(Func<IQueryable<TEntity>,
            IIncludableQueryable<TEntity, object>> include,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepository{TEntity}.ListAsync(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, CancellationToken)"/>
        public List<TEntity> ListAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepository{TEntity}.ListAsync(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}, int?, int?, CancellationToken)"/>
        public List<TEntity> ListAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepository{TEntity}.ListAsync(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, string, string, int?, int?, CancellationToken)"/>
        public List<TEntity> ListAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
            string orderBy,
            string orderDirection = "asc",
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Count

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.Count()"/>
        public int Count()
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.Count(Expression{Func{TEntity, bool}})"/>
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Create

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.Create(TEntity)"/>
        public void Create(TEntity entityToAdd)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.CreateAsync(TEntity, CancellationToken)"/>
        public Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Update

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.Update(TEntity)"/>
        public void Update(TEntity entityToUpdate)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Delete

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.Delete(TEntity)"/>
        public void Delete(TEntity entityToDelete)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IGenericRepositoryBase{TEntity}.Delete(Expression{Func{TEntity, bool}})"/>
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region GetOne

        /// <inheritdoc cref="IGenericRepository{TEntity}.GetOne(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}})"/>
        public TEntity GetOne(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IGenericRepository{TEntity}.GetOneAsync(Expression{Func{TEntity, bool}}, Func{IQueryable{TEntity}, IIncludableQueryable{TEntity, object}}, CancellationToken)"/>
        public Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}