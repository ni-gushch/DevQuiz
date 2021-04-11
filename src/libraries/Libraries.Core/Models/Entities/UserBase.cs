using System;

namespace DevQuiz.Libraries.Core.Models.Entities
{
    /// <summary>
    /// Base UserModel
    /// </summary>
    public class UserBase<TKey> : AggregateEntity<TKey>
        where TKey : IEquatable<TKey>
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
        public int TelegramChatId { get; set; }
    }
}