using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Services;

namespace DevQuiz.Libraries.Services
{
    public class FakeUserService<TUser, TUserDto, TUserKey, TQuestion, TAnswer, TCategory, TTag> 
        : IUserService<TUserDto, TUserKey>
        where TUserDto : UserDtoBase<TUserKey>
        where TUser : UserBase<TUserKey>
        where TUserKey : IEquatable<TUserKey>
        where TQuestion : QuestionBase<TAnswer, TCategory, TTag>
        where TAnswer : AnswerBase
        where TCategory : CategoryBase<TQuestion>
        where TTag : TagBase<TQuestion>
    {
        public async Task<IList<TUserDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TUserDto> GetByIdAsync(TUserKey idDto, CancellationToken cancellationToken = default)
        {
            return Task.FromResult((TUserDto)null);
        }

        public Task<TUserKey> CreateAsync(TUserDto entryToAdd, CancellationToken cancellationToken = default)
        {
            return Task.FromResult((TUserKey)default);
        }

        public async Task<bool> UpdateAsync(TUserDto entryToUpdate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(TUserKey idDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TUserDto> GetByChatIdAsync(int telegramChatId, CancellationToken cancellationToken = default)
        {
            return Task.FromResult((TUserDto)null);
        }
    }
}