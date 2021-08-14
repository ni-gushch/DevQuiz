using System;
using System.Collections.Generic;
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
    /// <inheritdoc cref="IUserService" />
    public class UserService : IUserService
    {
        private readonly IDevQuizUnitOfWork _unitOfWork;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Instance of UnitOfWork</param>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="logger">Logger instance</param>
        public UserService(IDevQuizUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<UserService> logger = null)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.UserRepository;
            _mapper = mapper;
            _logger = logger ?? NullLogger<UserService>.Instance;
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.GetAllAsync" />
        public async Task<IList<UserDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var allUsers = await _userRepository.ListAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            var result = _mapper.Map<List<UserDto>>(allUsers);
            return result;
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.GetByIdAsync" />
        public async Task<UserDto> GetByIdAsync(Guid idDto, CancellationToken cancellationToken = default)
        {
            var userEntity = await _userRepository.GetOneAsync(it => it.Id.Equals(idDto),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            return _mapper.Map<UserDto>(userEntity);
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.CreateAsync" />
        public async Task<Guid> CreateAsync(UserDto entryToAdd, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Start creating new user");
            var addUserEntity = _mapper.Map<User>(entryToAdd);
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
        public async Task<bool> DeleteAsync(Guid idDto, CancellationToken cancellationToken = default)
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
        public async Task<bool> UpdateAsync(UserDto entryToUpdate, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Start updating user");
            var userEntity = _mapper.Map<User>(entryToUpdate);
            userEntity.UpdatedDate = DateTime.UtcNow;
            _unitOfWork.ClearChangeTracker();
            _userRepository.Update(userEntity);
            var commitStatus = await _unitOfWork.CommitAsync(cancellationToken)
                .ConfigureAwait(false);
            if (commitStatus.Equals(0))
                throw new DbUpdateException($"Some error occurred white updating user with id {entryToUpdate.Id}");
            return commitStatus > 0;
        }

        /// <inheritdoc cref="IUserService.GetByChatIdAsync" />
        public async Task<UserDto> GetByChatIdAsync(long telegramChatId, CancellationToken cancellationToken = default)
        {
            var userEntity = await _userRepository.GetOneAsync(it => it.TelegramChatId.Equals(telegramChatId),
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            return _mapper.Map<UserDto>(userEntity);
        }
    }
}