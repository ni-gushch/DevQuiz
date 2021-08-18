using System;
using DevQuiz.Shared.Models;
using DevQuiz.Shared.Models.Abstractions;

namespace DevQuiz.Admin.Client.Models
{
    /// <summary>
    ///     Model with value
    /// </summary>
    public class ValueModel
    {
        /// <summary>
        ///     Value of entry
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    ///     Model with value and unique identifier
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class ValueModel<TKey> : ValueModel, IHasKey<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     Unique identifier of entry
        /// </summary>
        public TKey Id { get; set; }
    }
}