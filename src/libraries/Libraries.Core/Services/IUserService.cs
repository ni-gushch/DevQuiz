using System;
using DevQuiz.Libraries.Core.Models.Dto;

namespace DevQuiz.Libraries.Core.Services
{
    /// <summary>
    /// Service for manage users
    /// </summary>
    public interface IUserService<in TUserDto, TOneUserResult, TAllUsersResult, TStatusResult, TKey> : 
        IBaseService<TUserDto, TOneUserResult, TAllUsersResult, TStatusResult, TKey>
        where TUserDto : UserDtoBase<TKey>
        where TOneUserResult : class
        where TAllUsersResult : class
        where TKey : IEquatable<TKey>
    {

    }
}