using System;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Models.Entities;

namespace DevQuiz.Libraries.Core.Repositories
{
    /// <summary>
    /// Repository for manage DevQuiz user entities
    /// </summary>
    public interface IUserRepository<TUser, TKey> : IRepository<TUser, TKey>
        where TUser : UserBase<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Get one user from the store
        /// </summary>
        /// <param name="entityId">Unique identifier of entity</param>
        /// <returns>User information about one user</returns>
        Task<TUser> GetOneAsync(TKey entityId);
    }
}