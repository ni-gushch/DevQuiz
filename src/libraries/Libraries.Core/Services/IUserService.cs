using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Models.Dto;

namespace DevQuiz.Libraries.Core.Services
{
    /// <summary>
    /// Service for manage users
    /// </summary>
    /// <typeparam name="TUserDto">User dto for add or update</typeparam>
    /// <typeparam name="TKey">Parameter with unique identifier of entry</typeparam>
    public interface IUserService<TUserDto, TKey> 
        : IBaseService<TUserDto, TUserDto, IList<TUserDto>, TKey, bool, bool, TKey>
        where TUserDto : UserDtoBase<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Get user information by chat id
        /// </summary>
        /// <param name="telegramChatId">User chat id with current bot</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Information about user</returns>
        Task<TUserDto> GetByChatIdAsync(int telegramChatId, CancellationToken cancellationToken = default);
    }
}