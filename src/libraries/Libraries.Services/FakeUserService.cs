using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Services;

namespace DevQuiz.Libraries.Services
{
    /// <inheritdoc cref="IUserService"/>
    public class FakeUserService : IUserService
    {
        /// <summary>
        ///     List of DTO of user.
        /// </summary>
        public IList<UserDto> UserDtoes { get; set; } = new List<UserDto>();


        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.GetAllAsync" />
        public Task<IList<UserDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.GetByIdAsync" />
        public virtual Task<UserDto> GetByIdAsync(Guid idDto, CancellationToken cancellationToken = default)
        {
            var userDto = UserDtoes.FirstOrDefault(d => d.Id.Equals(idDto));
            return Task.FromResult(userDto);
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.CreateAsync" />
        public virtual Task<Guid> CreateAsync(UserDto entryToAdd, CancellationToken cancellationToken = default)
        {
            if (entryToAdd == null)
                return Task.FromResult((Guid)default);
            var id = entryToAdd.Id;
            UserDtoes.Add(entryToAdd);
            return Task.FromResult(id);
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.UpdateAsync" />
        public virtual Task<bool> UpdateAsync(UserDto entryToUpdate, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.DeleteAsync" />
        public Task<bool> DeleteAsync(Guid idDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IUserService.GetByChatIdAsync(long, CancellationToken)" />
        public Task<UserDto> GetByChatIdAsync(long telegramChatId, CancellationToken cancellationToken = default)
        {
            var userDto = UserDtoes.FirstOrDefault(ud => ud.TelegramChatId == telegramChatId);
            return Task.FromResult(userDto);
        }
    }
}