using System;

namespace DevQuiz.Libraries.Core.Models.Entities
{
    /// <summary>
    /// Base model for aggregated entities
    /// </summary>
    /// <typeparam name="TKey">Type of entity unique index </typeparam>
    public class AggregateEntity<TKey> : Entity<TKey>
    {
        /// <summary>
        /// DateTime when entity was created
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// DateTime when entity was updated
        /// </summary>
        public DateTime UpdatedTime { get; set; }
    }
}