using System;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;

namespace DevQuiz.Libraries.Core
{
    /// <summary>
    /// Unit of work interface
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Save all changes
        /// </summary>
        /// <returns>Operation status</returns>
        int Commit();
        /// <summary>
        /// Save all changes
        /// </summary>
        /// <param name="cancellationToken">Token for cancel operation</param>
        /// <returns>Operation status</returns>
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Clear db context tracker
        /// <remarks>Need if you don't want Dispose DbContext but want change tracked elements</remarks>
        /// </summary>
        void ClearChangeTracker();
        /// <summary>
        /// Get base repository instance
        /// </summary>
        /// <typeparam name="TEntity">Type of TEntity</typeparam>
        /// <returns>Base repository instance</returns>
        IGenericRepositoryBase<TEntity> GetBaseRepository<TEntity>() where TEntity : class;
        /// <summary>
        /// Get instance of concrete repository
        /// </summary>
        /// <typeparam name="TRepository">Type of repository</typeparam>
        /// <typeparam name="TEntity">Type of TEntity</typeparam>
        /// <returns>Concrete repository instance</returns>
        TRepository GetRepository<TRepository, TEntity>()
            where TRepository : IGenericRepositoryBase<TEntity>
            where TEntity : class;
    }

    /// <summary>
    /// DevQuiz UnitOfWork
    /// </summary>
    /// <typeparam name="TUser">Generic User Entity</typeparam>
    /// <typeparam name="TQuestion">Generic Question Entity</typeparam>
    /// <typeparam name="TAnswer">Generic Question Answer Entity</typeparam>
    /// <typeparam name="TCategory">Generic Question Category Entity</typeparam>
    /// <typeparam name="TTag">Generic Question Tag Entity</typeparam>
    /// <typeparam name="TUserKey">Generic Key for User Entity</typeparam>
    public interface IDevQuizUnitOfWork<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey> : IUnitOfWork
        where TUser : UserBase<TUserKey>
        where TQuestion : QuestionBase<TAnswer, TCategory, TTag>
        where TAnswer : AnswerBase
        where TCategory : CategoryBase<TQuestion>
        where TTag : TagBase<TQuestion>
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// User repository
        /// </summary>
        IGenericRepository<TUser> UserRepository { get; }
        /// <summary>
        /// Question repository
        /// </summary>
        IGenericRepository<TQuestion> QuestionRepository { get; }
        /// <summary>
        /// Category repository
        /// </summary>
        IGenericRepository<TCategory> CategoryRepository { get; }
        /// <summary>
        /// Tag repository
        /// </summary>
        IGenericRepository<TTag> TagRepository { get; }
        /// <summary>
        /// Answer repository
        /// </summary>
        IGenericRepository<TAnswer> AnswerRepository { get; }
    }
}