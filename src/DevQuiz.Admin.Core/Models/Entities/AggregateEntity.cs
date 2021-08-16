using System;

namespace DevQuiz.Admin.Core.Models.Entities
{
    /// <summary>
    /// Base model for aggregated entities
    /// </summary>
    /// <typeparam name="TKey">Type of entity unique index </typeparam>
    public class AggregateEntity<TKey> : EntityBase<TKey>, IAuditEntity
        where TKey : IEquatable<TKey>
    {
        /// <inheritdoc cref="IAuditEntity.CreatedDate" />
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <inheritdoc cref="IAuditEntity.CreatedBy" />
        public string CreatedBy { get; set; }

        /// <inheritdoc cref="IAuditEntity.UpdatedDate" />
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}