namespace DevQuiz.Libraries.Core.Models.Entities
{
    /// <summary>
    /// Base entity class
    /// </summary>
    /// <typeparam name="TKey">Type of unique identifier</typeparam>
    public class Entity<TKey> : IHasKey<TKey>
    {
        /// <summary>
        /// Get or set unique identifier of entity
        /// </summary>
        public TKey Id { get; set; }
    }
}