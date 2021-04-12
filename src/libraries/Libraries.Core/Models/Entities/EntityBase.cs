using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevQuiz.Libraries.Core.Models.Entities
{
    /// <summary>
    /// Base entity class
    /// </summary>
    /// <typeparam name="TKey">Type of unique identifier</typeparam>
    public class EntityBase<TKey> : IHasKey<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Get or set unique identifier of entity
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }
    }
}