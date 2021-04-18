using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Core.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.Libraries.Services
{
    /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TCreateUserResult, TUpdateUserResult, TDeleteUserResult, TKey}" />
    public class UserService<TUser, TUserDto, TKey> : IUserService<TUserDto, TUserDto, List<TUserDto>, TKey, bool, bool, TKey>
        where TUserDto : UserDtoBase<TKey>
        where TUser : UserBase<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepositoryEntityFramework<TUser> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService<TUser, TUserDto, TKey>> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Instance of UnitOfWork</param>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="logger">Logger instance</param>
        public UserService(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<UserService<TUser, TUserDto, TKey>> logger = null)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetRepository<IGenericRepositoryEntityFramework<TUser>, TUser>();
            _mapper = mapper;
            _logger = logger ?? NullLogger<UserService<TUser, TUserDto, TKey>>.Instance;
        }

        /// <inheritdoc cref="IBaseService{TUserDto, TOneUserResult, TAllUsersResult, TCreateUserResult, TUpdateUserResult, TDeleteUserResult, TKey}.Create(TUserDto)" />
        public async Task<TKey> Create(TUserDto entryToAdd)
        {
            _logger.LogDebug("Start creating new user");
            var addUserEntity = _mapper.Map<TUser>(entryToAdd);
            await _userRepository.CreateAsync(addUserEntity);
            _logger.LogDebug("Create new user save changes");
            var commitStatus = await _unitOfWork.CommitAsync();

            return addUserEntity.Id;
        }

        /// <inheritdoc cref="IBaseService{TUserDto, TOneUserResult, TAllUsersResult, TCreateUserResult, TUpdateUserResult, TDeleteUserResult, TKey}.Delete(TKey)" />
        public async Task<bool> Delete(TKey idDto)
        {
            _logger.LogDebug($"Start deleting user with id {idDto}");
            var userToDelete = await _userRepository.GetOneAsync(it => it.Id.Equals(idDto));
            _userRepository.Delete(userToDelete);
            _logger.LogDebug("Delete user save changes");
            var commitStatus = await _unitOfWork.CommitAsync();
            
            return commitStatus > 0;
        }

        /// <inheritdoc cref="IBaseService{TUserDto, TOneUserResult, TAllUsersResult, TCreateUserResult, TUpdateUserResult, TDeleteUserResult, TKey}.GetAll" />
        public Task<List<TUserDto>> GetAll()
        {
            var allUsers = _userRepository.GetAll().ToList();
            var result = _mapper.Map<List<TUserDto>>(allUsers);
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IBaseService{TUserDto, TOneUserResult, TAllUsersResult, TCreateUserResult, TUpdateUserResult, TDeleteUserResult, TKey}.GetById(TKey)" />
        public async Task<TUserDto> GetById(TKey idDto)
        {
            var userEntity = await _userRepository.GetOneAsync(it => it.Id.Equals(idDto));
            return _mapper.Map<TUserDto>(userEntity);
        }

        /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TCreateUserResult, TUpdateUserResult, TDeleteUserResult, TKey}.GetByChatIdAsync(int)" />
        public async Task<TUserDto> GetByChatIdAsync(int telegramChatId)
        {
            var userEntity = await _userRepository.GetOneAsync(it => it.TelegramChatId.Equals(telegramChatId));
            return _mapper.Map<TUserDto>(userEntity);
        }

        /// <inheritdoc cref="IBaseService{TUserDto, TOneUserResult, TAllUsersResult, TCreateUserResult, TUpdateUserResult, TDeleteUserResult, TKey}.Update(TUserDto)" />
        public async Task<bool> Update(TUserDto entryToUpdate)
        {
            _logger.LogDebug("Start updating user");
            var userEntity = _mapper.Map<TUser>(entryToUpdate);
            _userRepository.Update(userEntity);
            var commitResult = await _unitOfWork.CommitAsync();
            return commitResult > 0;
        }
    }
}