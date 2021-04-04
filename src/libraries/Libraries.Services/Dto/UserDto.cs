using System;
using DevQuiz.Libraries.Core.Models.Dto;

namespace DevQuiz.Libraries.Services.Dto
{
    /// <summary>
    /// Dto model for user
    /// </summary>
    public class UserDto : UserDtoBase<Guid>
    {
        /// <summary>
        /// Unique identifier of user in Telegram
        /// </summary>
        public int TelegramId { get; set; }
    }
}