using System;

namespace DevQuiz.Libraries.Core.Models.Entities
{
    /// <summary>
    /// Generic interface for entities whitch has a unique key
    /// </summary>
    /// <typeparam name="TKey">Unique key type</typeparam>
    public interface IHasKey<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Unique key
        /// </summary>
        TKey Id { get; set; }
    }
}