using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevQuiz.Libraries.Data
{
    /// <inheritdoc cref="IUnitOfWork" />
    public class UnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        /// <summary>
        /// Set of registered repositories
        /// </summary>
        protected Dictionary<string, object> Repositories { get; } = new();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">TDbContext instance</param>
        public UnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc cref="IUnitOfWork.Commit" />
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        /// <inheritdoc cref="IUnitOfWork.CommitAsync" />
        public Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc cref="IUnitOfWork.ClearChangeTracker" />
        public void ClearChangeTracker()
        {
            _dbContext.ChangeTracker.Clear();
        }

        /// <inheritdoc cref="IUnitOfWork.GetBaseRepository{TEntity}"/>
        public IGenericRepositoryBase<TEntity> GetBaseRepository<TEntity>() where TEntity : class
        {
            Repositories.TryGetValue(nameof(TEntity), out var returnRepoObject);
            if (returnRepoObject is IGenericRepositoryBase<TEntity> returnRepo)
                return returnRepo;
            throw new ArgumentNullException($"Repository for type {typeof(TEntity)} is not registered");
        }

        /// <inheritdoc cref="IUnitOfWork.GetRepository{TRepository, TEntity}"/>
        public TRepository GetRepository<TRepository, TEntity>() where TRepository : IGenericRepositoryBase<TEntity> where TEntity : class
        {
            Repositories.TryGetValue(typeof(TEntity).Name, out var returnRepoObject);
            if (returnRepoObject is TRepository returnRepo)
                return returnRepo;
            throw new ArgumentNullException($"Repository for type {typeof(TEntity)} is not registered");
        }

        /// <summary>
        /// Dispose unit of work instance
        /// </summary>
        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Add repository to repository dictionary
        /// </summary>
        /// <typeparam name="T"> Base repository </typeparam>
        /// <typeparam name="TEntity"> Entity </typeparam>
        /// <param name="source"> Repository instance </param>
        /// <returns> Registered repository instance </returns>
        protected T RegisterRepository<T, TEntity>(T source)
            where T : IGenericRepositoryBase<TEntity>
            where TEntity : class
        {
            if (source == null) return default;
            Repositories.TryAdd(typeof(TEntity).Name, source);
            return source;
        }
    }

    /// <inheritdoc cref="IDevQuizUnitOfWork"/>
    public class DevQuizUnitOfWork<TDbContext> : UnitOfWork<TDbContext>, IDevQuizUnitOfWork
        where TDbContext : DbContext
    {
        /// <summary>
        /// User repository
        /// </summary>
        public IGenericRepository<User> UserRepository { get; }
        /// <summary>
        /// Question repository
        /// </summary>
        public IGenericRepository<Question> QuestionRepository { get; }
        /// <summary>
        /// Category repository
        /// </summary>
        public IGenericRepository<Category> CategoryRepository { get; }
        /// <summary>
        /// Tag repository
        /// </summary>
        public IGenericRepository<Tag> TagRepository { get; }
        /// <summary>
        /// Answer repository
        /// </summary>
        public IGenericRepository<Answer> AnswerRepository { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="genericDbContext">Current db context</param>
        /// <param name="userRepository">User repository instance</param>
        /// <param name="questionRepository">Question repository instance</param>
        /// <param name="answerRepository">Answer repository instance</param>
        /// <param name="categoryRepository">Category repository instance</param>
        /// <param name="tagRepository">Tag repository instance</param>
        public DevQuizUnitOfWork(TDbContext genericDbContext,
            IGenericRepository<User> userRepository = null,
            IGenericRepository<Question> questionRepository = null,
            IGenericRepository<Answer> answerRepository = null,
            IGenericRepository<Category> categoryRepository = null,
            IGenericRepository<Tag> tagRepository = null) : base(genericDbContext)
        {
            UserRepository = RegisterRepository<IGenericRepository<User>, User>(userRepository);
            QuestionRepository = RegisterRepository<IGenericRepository<Question>, Question>(questionRepository);
            CategoryRepository = RegisterRepository<IGenericRepository<Category>, Category>(categoryRepository);
            TagRepository = RegisterRepository<IGenericRepository<Tag>, Tag>(tagRepository);
            AnswerRepository = RegisterRepository<IGenericRepository<Answer>, Answer>(answerRepository);
        }        
    }

    /// <inheritdoc cref="IDevQuizUserUnitOfWork"/>
    public class DevQuizUserUnitOfWork<TDbContext> : UnitOfWork<TDbContext>, IDevQuizUserUnitOfWork
        where TDbContext : DbContext
    {
        /// <summary>
        /// User repository
        /// </summary>
        public IGenericRepository<User> UserRepository { get; }
       
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="genericDbContext">Current db context</param>
        /// <param name="userRepository">User repository instance</param>        
        public DevQuizUserUnitOfWork(TDbContext genericDbContext,
            IGenericRepository<User> userRepository = null) : base(genericDbContext)
        {
            UserRepository = RegisterRepository<IGenericRepository<User>, User>(userRepository);
        }        
    }
}