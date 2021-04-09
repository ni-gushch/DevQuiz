using System;
using DevQuiz.Libraries.Core.Models.Dto;

namespace DevQuiz.Libraries.Core.Services
{
    /// <summary>
    /// Service for manage users
    /// </summary>
    /// <typeparam name="TUserDto">User dto for add or update</typeparam>
    /// <typeparam name="TKey">Parameter with unique identifier of entry</typeparam>
    /// <typeparam name="TOneUserResult">Get one user result</typeparam>
    /// <typeparam name="TAllUsersResult">Get all users result</typeparam>
    /// <typeparam name="TCreateUserResult">Result after create user</typeparam>
    /// <typeparam name="TUpdateUserResult">Result after update user</typeparam>
    /// <typeparam name="TDeleteUserResult">Result after delete user</typeparam>
    public interface IUserService<in TUserDto, 
        TOneUserResult, TAllUsersResult, TCreateUserResult, TUpdateUserResult, TDeleteUserResult, TKey> : 
        IBaseService<TUserDto, 
        TOneUserResult, TAllUsersResult, TCreateUserResult, TUpdateUserResult, TDeleteUserResult, TKey>
        where TUserDto : UserDtoBase<TKey>
        where TOneUserResult : class
        where TAllUsersResult : class
        where TKey : IEquatable<TKey>
    {

    }
}