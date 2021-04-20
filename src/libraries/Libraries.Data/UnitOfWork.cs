using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DevQuiz.Libraries.Data
{
    /// <inheritdoc cref="IUnitOfWork" />
    public class UnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private readonly Dictionary<string, object> _repositories = new();

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
        /// <param name="dbContext">TDbContext instance</param>
        /// <param name="userRepository">User repository instance</param>
        /// <param name="questionRepository">Question repository instance</param>
        /// <param name="categoryRepository">Category repository instance</param>
        /// <param name="tagRepository">Tag repository instance</param>
        /// <param name="answerRepository">Answer repository instance</param>
        public UnitOfWork(TDbContext dbContext,
            IGenericRepository<User> userRepository = null,
            IGenericRepository<Question> questionRepository = null,
            IGenericRepository<Category> categoryRepository = null,
            IGenericRepository<Tag> tagRepository = null,
            IGenericRepository<Answer> answerRepository = null)   
        {
            _dbContext = dbContext;
            UserRepository = userRepository; 
            if (UserRepository != null)
                _repositories.TryAdd(nameof(User), UserRepository);

            QuestionRepository = questionRepository;
            if (QuestionRepository != null) 
                _repositories.TryAdd(nameof(Question), QuestionRepository);

            CategoryRepository = categoryRepository;
            if (CategoryRepository != null)
                _repositories.TryAdd(nameof(Category), CategoryRepository);

            TagRepository = tagRepository;
            if (TagRepository != null)
                _repositories.TryAdd(nameof(Tag), TagRepository);

            AnswerRepository = answerRepository;
            if (AnswerRepository != null) 
                _repositories.TryAdd(nameof(Answer), AnswerRepository);
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
        /// <inheritdoc cref="IUnitOfWork.GetBaseRepository{TEntity}"/>
        public IGenericRepositoryBase<TEntity> GetBaseRepository<TEntity>() where TEntity : class
        {
            _repositories.TryGetValue(nameof(TEntity), out var returnRepoObject);
            if (returnRepoObject is IGenericRepositoryBase<TEntity> returnRepo)
                return returnRepo;
            throw new ArgumentNullException($"Repository for type {typeof(TEntity)} is not registered");
        }
        /// <inheritdoc cref="IUnitOfWork.GetRepository{TRepository, TEntity}"/>
        public TRepository GetRepository<TRepository, TEntity>() where TRepository : IGenericRepositoryBase<TEntity> where TEntity : class
        {
            _repositories.TryGetValue(typeof(TEntity).Name, out var returnRepoObject);
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
    }
}