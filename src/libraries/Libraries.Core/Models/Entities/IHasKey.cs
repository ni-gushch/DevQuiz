namespace DevQuiz.Libraries.Core.Models.Entities
{
    /// <summary>
    /// Generic interface for entities whitch has a unique key
    /// </summary>
    /// <typeparam name="T">Unique key type</typeparam>
    public interface IHasKey<T>
    {
        /// <summary>
        /// Unique key
        /// </summary>
        T Id { get; set; }
    }
}