using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        private IEnumerable<TUserDto> _userDtos;

        public IList<TUserDto> UserDtos { get; set; } = new List<TUserDto>();
        
        public Task<IList<TUserDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TUserDto> GetByIdAsync(TUserKey idDto, CancellationToken cancellationToken = default)
        {
            var userDto = UserDtos.FirstOrDefault(d => d.Id.Equals(idDto));
            return Task.FromResult(userDto);
        }

        public virtual Task<TUserKey> CreateAsync(TUserDto entryToAdd, CancellationToken cancellationToken = default)
        {
            if (entryToAdd == null)
                return Task.FromResult((TUserKey)default);
            var id = entryToAdd.Id;
            UserDtos.Add(entryToAdd);
            return Task.FromResult(id);
        }

        public virtual Task<bool> UpdateAsync(TUserDto entryToUpdate, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(TUserKey idDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TUserDto> GetByChatIdAsync(long telegramChatId, CancellationToken cancellationToken = default)
        {
            var userDto = UserDtos.FirstOrDefault(ud => ud.TelegramChatId == telegramChatId);
            return Task.FromResult(userDto);
        }
    }
}