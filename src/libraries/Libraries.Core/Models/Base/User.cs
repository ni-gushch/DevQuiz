using System;

namespace DevQuiz.Libraries.Core.Models.Base
{
    /// <summary>
    /// Base UserModel
    /// </summary>
    public class User<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Get or set unique identifier of user
        /// </summary>
        public TKey Id { get; set; }
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
    }
}