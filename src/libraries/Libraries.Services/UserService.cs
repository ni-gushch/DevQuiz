using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Libraries.Core;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.Libraries.Services
{
    /// <inheritdoc cref="IUserService{TUserDto, TKey}" />
    public class UserService<TUser, TUserDto, TUserKey, 
        TQuestion, TAnswer, TCategory, TTag> : IUserService<TUserDto, TUserKey>
        where TUserDto : UserDtoBase<TUserKey>
        where TUser : UserBase<TUserKey>
        where TUserKey : IEquatable<TUserKey>
        where TQuestion : QuestionBase<TAnswer, TCategory, TTag>
        where TAnswer : AnswerBase
        where TCategory : CategoryBase<TQuestion>
        where TTag : TagBase<TQuestion>
    {
        private readonly IDevQuizUnitOfWork<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey> _unitOfWork;
        private readonly IGenericRepository<TUser> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService<TUser, TUserDto, TUserKey, TQuestion, TAnswer, TCategory, TTag>> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Instance of UnitOfWork</param>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="logger">Logger instance</param>
        public UserService(IDevQuizUnitOfWork<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey> unitOfWork,
            IMapper mapper,
            ILogger<UserService<TUser, TUserDto, TUserKey, TQuestion, TAnswer, TCategory, TTag>> logger = null)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.UserRepository;
            _mapper = mapper;
            _logger = logger ?? NullLogger<UserService<TUser, TUserDto, TUserKey, TQuestion, TAnswer, TCategory, TTag>>.Instance;
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.GetAllAsync" />
        public async Task<IList<TUserDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var allUsers = await _userRepository.ListAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            var result = _mapper.Map<List<TUserDto>>(allUsers);
            return result;
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.GetByIdAsync" />
        public async Task<TUserDto> GetByIdAsync(TUserKey idDto, CancellationToken cancellationToken = default)
        {
            var userEntity = await _userRepository.GetOneAsync(it => it.Id.Equals(idDto),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            return _mapper.Map<TUserDto>(userEntity);
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.CreateAsync" />
        public async Task<TUserKey> CreateAsync(TUserDto entryToAdd, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Start creating new user");
            var addUserEntity = _mapper.Map<TUser>(entryToAdd);
            await _userRepository.CreateAsync(addUserEntity, cancellationToken)
                .ConfigureAwait(false);
            _logger.LogDebug("Create new user save changes");
            var commitStatus = await _unitOfWork.CommitAsync(cancellationToken)
                .ConfigureAwait(false);
            if (commitStatus.Equals(0))
                throw new DbUpdateException("Some error occurred white creating new user");

            return addUserEntity.Id;
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.DeleteAsync" />
        public async Task<bool> DeleteAsync(TUserKey idDto, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug($"Start deleting user with id {idDto}");
            var userToDelete = await _userRepository.GetOneAsync(it => it.Id.Equals(idDto), cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            _userRepository.Delete(userToDelete);
            _logger.LogDebug("Delete user save changes");
            var commitStatus = await _unitOfWork.CommitAsync(cancellationToken)
                .ConfigureAwait(false);
            if (commitStatus.Equals(0))
                throw new DbUpdateException($"Some error occurred white deleting user with id {idDto}");

            return commitStatus > 0;
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.UpdateAsync" />
        public async Task<bool> UpdateAsync(TUserDto entryToUpdate, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Start updating user");
            var userEntity = _mapper.Map<TUser>(entryToUpdate);
            userEntity.UpdatedDate = DateTime.UtcNow;
            _userRepository.Update(userEntity);
            var commitStatus = await _unitOfWork.CommitAsync(cancellationToken)
                .ConfigureAwait(false);
            if (commitStatus.Equals(0))
                throw new DbUpdateException($"Some error occurred white updating user with id {entryToUpdate.Id}");
            return commitStatus > 0;
        }

        /// <inheritdoc cref="IUserService{TUserDto, TKey}.GetByChatIdAsync(int, CancellationToken)" />
        public async Task<TUserDto> GetByChatIdAsync(int telegramChatId, CancellationToken cancellationToken = default)
        {
            var userEntity = await _userRepository.GetOneAsync(it => it.TelegramChatId.Equals(telegramChatId),
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            return _mapper.Map<TUserDto>(userEntity);
        }
    }
}