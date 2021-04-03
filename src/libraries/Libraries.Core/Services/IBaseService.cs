using System;
using System.Threading.Tasks;

namespace DevQuiz.Libraries.Core.Services
{
    /// <summary>
    /// Base service for manage entries
    /// </summary>
    /// <typeparam name="TEntryDto">Entry dto for add or update</typeparam>
    /// <typeparam name="TKey">Parameter with unique identifier of entry</typeparam>
    /// <typeparam name="TOneEntryResult">Get one entry result</typeparam>
    /// <typeparam name="TAllEntriesResult">Get all entries result</typeparam>
    /// <typeparam name="TStatusResult">Method status result</typeparam>
    public interface IBaseService<TEntryDto, TOneEntryResult, TAllEntriesResult, TStatusResult, TKey>
        where TEntryDto : class
        where TOneEntryResult : class
        where TAllEntriesResult : class
        where TKey :IEquatable<TKey>
    {
        /// <summary>
        /// Get all entries
        /// </summary>
        /// <returns>All entries result</returns>
        Task<TAllEntriesResult> GetAll();
        /// <summary>
        /// Get one entry
        /// </summary>
        /// <param name="idDto">Parameter with unique identifier</param>
        /// <returns>One entry result</returns>
        Task<TOneEntryResult> GetOne(TKey idDto);
        /// <summary>
        /// Create new entry
        /// </summary>
        /// <param name="entryToAdd">Model with information about new entry</param>
        /// <returns>New entry information</returns>
        Task<TOneEntryResult> Create(TEntryDto entryToAdd);
        /// <summary>
        /// Update entry
        /// </summary>
        /// <param name="entryToUpdate">Model with information to update</param>
        /// <returns>Update entry information</returns>
        Task<TOneEntryResult> Update(TEntryDto entryToUpdate);
        /// <summary>
        /// Delete entry
        /// </summary>
        /// <param name="idDto">Parameter with unique identifier</param>
        /// <returns>Method execution status</returns>
        Task<TStatusResult> Delete(TKey idDto);
    }
}