using System;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;

namespace DevQuiz.Libraries.Core
{
    /// <summary>
    /// DevQuiz UnitOfWork
    /// </summary>
    public interface IDevQuizUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// User repository
        /// </summary>
        IGenericRepository<User> UserRepository { get; }
        /// <summary>
        /// Question repository
        /// </summary>
        IGenericRepository<Question> QuestionRepository { get; }
        /// <summary>
        /// Category repository
        /// </summary>
        IGenericRepository<Category> CategoryRepository { get; }
        /// <summary>
        /// Tag repository
        /// </summary>
        IGenericRepository<Tag> TagRepository { get; }
        /// <summary>
        /// Answer repository
        /// </summary>
        IGenericRepository<Answer> AnswerRepository { get; }
    }

    /// <summary>
    /// DevQuiz UnitOfWork
    /// </summary>
    public interface IDevQuizUserUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// User repository
        /// </summary>
        IGenericRepository<User> UserRepository { get; }        
    }
}