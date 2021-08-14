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
    /// <inheritdoc cref="IUserService{TUserDto, TKey}"/>
    public class FakeUserService<TUser, TUserDto, TUserKey, TQuestion, TAnswer, TCategory, TTag> 
        : IUserService<TUserDto, TUserKey>
        where TUserDto : UserDtoBase<TUserKey>
        where TUser : User<TUserKey>
        where TUserKey : IEquatable<TUserKey>
        where TQuestion : Question
        where TAnswer : Answer
        where TCategory : Category
        where TTag : Tag
    {
        /// <summary>
        ///     List of DTO of user.
        /// </summary>
        public IList<TUserDto> UserDtoes { get; set; } = new List<TUserDto>();


        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.GetAllAsync" />
        public Task<IList<TUserDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.GetByIdAsync" />
        public virtual Task<TUserDto> GetByIdAsync(TUserKey idDto, CancellationToken cancellationToken = default)
        {
            var userDto = UserDtoes.FirstOrDefault(d => d.Id.Equals(idDto));
            return Task.FromResult(userDto);
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.CreateAsync" />
        public virtual Task<TUserKey> CreateAsync(TUserDto entryToAdd, CancellationToken cancellationToken = default)
        {
            if (entryToAdd == null)
                return Task.FromResult((TUserKey)default);
            var id = entryToAdd.Id;
            UserDtoes.Add(entryToAdd);
            return Task.FromResult(id);
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.UpdateAsync" />
        public virtual Task<bool> UpdateAsync(TUserDto entryToUpdate, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.DeleteAsync" />
        public Task<bool> DeleteAsync(TUserKey idDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IUserService{TUserDto, TKey}.GetByChatIdAsync(long, CancellationToken)" />
        public Task<TUserDto> GetByChatIdAsync(long telegramChatId, CancellationToken cancellationToken = default)
        {
            var userDto = UserDtoes.FirstOrDefault(ud => ud.TelegramChatId == telegramChatId);
            return Task.FromResult(userDto);
        }
    }
}