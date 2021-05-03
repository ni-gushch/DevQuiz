using System;
using DevQuiz.Libraries.Core.Models;

namespace DevQuiz.TelegramBot.Models.ApiResults
{
    /// <summary>
    /// Api Result with id of new entry
    /// </summary>
    public class IdApiResult<TKey> : IHasKey<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Constructor without parameters
        /// </summary>
        public IdApiResult()
        { }

        /// <summary>
        /// Constructor with parameter
        /// </summary>
        /// <param name="id">Unique identifier</param>
        public IdApiResult(TKey id)
        {
            Id = id;
        }

        /// <inheritdoc cref="IHasKey{TKey}.Id"/>
        public TKey Id { get; set; }
    }
}
