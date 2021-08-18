using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevQuiz.Admin.Core.Services
{
    /// <summary>
    ///     Base service for manage entries
    /// </summary>
    /// <typeparam name="TEntryDto">Entry dto for add or update</typeparam>
    /// <typeparam name="TKey">Parameter with unique identifier of entry</typeparam>
    /// <typeparam name="TOneEntryResult">Get one entry result</typeparam>
    /// <typeparam name="TAllEntriesResult">Get all entries result</typeparam>
    /// <typeparam name="TCreateEntryResult">Result after create entry</typeparam>
    /// <typeparam name="TUpdateEntryResult">Result after update entry</typeparam>
    /// <typeparam name="TDeleteEntryResult">Result after delete entry</typeparam>
    public interface IBaseService<in TEntryDto,
        TOneEntryResult, TAllEntriesResult, TCreateEntryResult, TUpdateEntryResult, TDeleteEntryResult, TKey>
        where TEntryDto : class
        where TOneEntryResult : class
        where TAllEntriesResult : class
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     Get all entries
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>All entries result</returns>
        Task<TAllEntriesResult> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get one entry
        /// </summary>
        /// <param name="idDto">Parameter with unique identifier</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>One entry result</returns>
        Task<TOneEntryResult> GetByIdAsync(TKey idDto, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Create new entry
        /// </summary>
        /// <param name="entryToAdd">Model with information about new entry</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>New entry information</returns>
        Task<TCreateEntryResult> CreateAsync(TEntryDto entryToAdd, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Update entry
        /// </summary>
        /// <param name="entryToUpdate">Model with information to update</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Update entry information</returns>
        Task<TUpdateEntryResult> UpdateAsync(TEntryDto entryToUpdate, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Delete entry
        /// </summary>
        /// <param name="idDto">Parameter with unique identifier</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Method execution status</returns>
        Task<TDeleteEntryResult> DeleteAsync(TKey idDto, CancellationToken cancellationToken = default);
    }
}