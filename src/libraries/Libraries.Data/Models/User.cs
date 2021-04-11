using System;
using DevQuiz.Libraries.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevQuiz.Libraries.Data.Models
{
    /// <summary>
    /// User db model
    /// </summary>
    [Index(nameof(TelegramChatId))]
    public class User : UserBase<Guid>
    {
        /// <summary>
        /// Unique identifier of user in Telegram
        /// </summary>
        public int TelegramId { get; set; }
        /// <summary>
        /// Unique identifier of telegram chat for current user
        /// </summary>
        public int TelegramChatId{ get; set; }
    }
}