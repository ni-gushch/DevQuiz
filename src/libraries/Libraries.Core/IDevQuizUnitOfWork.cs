using System;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;

namespace DevQuiz.Libraries.Core
{
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

    /// <summary>
    /// DevQuiz UnitOfWork
    /// </summary>
    /// <typeparam name="TUser">Generic User Entity</typeparam>
    /// <typeparam name="TUserKey">Generic Key for User Entity</typeparam>
    public interface IDevQuizUnitOfWork<TUser, TUserKey> : IUnitOfWork
        where TUser : UserBase<TUserKey>       
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// User repository
        /// </summary>
        IGenericRepository<TUser> UserRepository { get; }        
    }
}