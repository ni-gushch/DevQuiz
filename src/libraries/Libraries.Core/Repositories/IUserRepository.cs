using System;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Models.Entities;

namespace DevQuiz.Libraries.Core.Repositories
{
    /// <summary>
    /// Repository for manage DevQuiz user entities
    /// </summary>
    public interface IUserRepository<TUser, TKey> : IRepository<TUser>
        where TUser : UserBase<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Get one user from the store
        /// </summary>
        /// <param name="entityId">Unique identifier of entity</param>
        /// <returns>Information about one user</returns>
        Task<TUser> GetByIdAsync(TKey entityId);
        /// <summary>
        /// Get user information by telegram chat id
        /// </summary>
        /// <param name="telegramChatId">Uniq identifier of telegram chat</param>
        /// <returns>Information about one user</returns>
        Task<TUser> GetByChatIdAsync(int telegramChatId);
    }
}