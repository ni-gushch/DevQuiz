using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Admin.Core.Models.Dto;

namespace DevQuiz.Admin.Core.Services
{
    /// <summary>
    ///     Service for manage users
    /// </summary>
    public interface IUserService : IBaseService<UserDto, UserDto, IList<UserDto>, Guid, bool, bool, Guid>
    {
        /// <summary>
        ///     Get user information by chat id
        /// </summary>
        /// <param name="telegramChatId">User chat id with current bot</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Information about user</returns>
        Task<UserDto> GetByChatIdAsync(long telegramChatId, CancellationToken cancellationToken = default);
    }
}