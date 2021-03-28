using System;
using System.Collections.Generic;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data.Models;

namespace DevQuiz.Libraries.Data.Repositories
{
    /// <inheritdoc cref="IUserRepository{TUser,TKey}" />
    public class UserRepository : IUserRepository<User, Guid>
    {
        /// <inheritdoc cref="IUserRepository{TUser,TKey}.Create" />
        public User Create(User user)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IUserRepository{TUser,TKey}.Delete" />
        public bool Delete(Guid userId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IUserRepository{TUser,TKey}.GetAll" />
        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IUserRepository{TUser,TKey}.GetOne" />
        public User GetOne()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IUserRepository{TUser,TKey}.Update" />
        public User Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}