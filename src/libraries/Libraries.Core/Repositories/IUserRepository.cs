using System;
using System.Collections.Generic;
using DevQuiz.Libraries.Core.Models.Entities;

namespace DevQuiz.Libraries.Core.Repositories
{
    /// <summary>
    /// Repository for manage DevQuiz user entities
    /// </summary>
    public interface IUserRepository<TUser, TKey>
        where TUser : UserBase<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Get all users from the store
        /// </summary>
        /// <returns>List of all users</returns>
        List<TUser> GetAll();
        /// <summary>
        /// Get one user from the store
        /// </summary>
        /// <returns>User information about one user</returns>
        TUser GetOne();
        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user">User information</param>
        /// <returns>User information</returns>
        TUser Create(TUser user);
        /// <summary>
        /// Update user information
        /// </summary>
        /// <param name="user">User information</param>
        /// <returns>User information</returns>
        TUser Update(TUser user);
        /// <summary>
        /// Delete user from the store
        /// </summary>
        /// <param name="userId">Unique identifier of user</param>
        /// <returns>Operation status</returns>
        bool Delete(TKey userId);
    }
}