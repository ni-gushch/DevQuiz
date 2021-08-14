using System;
using Microsoft.EntityFrameworkCore;

namespace DevQuiz.Libraries.Core.Models.Entities
{
    /// <summary>
    /// Base UserModel
    /// </summary>
    [Index(nameof(TelegramChatId))]
    public class User : AggregateEntity<Guid>
    {
        /// <summary>
        /// Get or set user name (login)
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Get or set user first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Get or set user last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Unique identifier of telegram chat for current user
        /// </summary>
        public long TelegramChatId { get; set; }

        /// <summary>
        /// Unique identifier of user in Telegram
        /// </summary>
        public int TelegramId { get; set; }
    }
}