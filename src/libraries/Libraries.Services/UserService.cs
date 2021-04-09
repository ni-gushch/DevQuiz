using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Core.Services;
using DevQuiz.Libraries.Services.Dto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.Libraries.Services
{
    /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TCreateUserResult, TUpdateUserResult, TDeleteUserResult, TKey}" />
    public class UserService<TUser, TUserDto, TKey> : IUserService<TUserDto, TUserDto, List<TUserDto>, bool, bool, bool, TKey>
        where TUserDto : UserDtoBase<TKey>
        where TUser : UserBase<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly IUserRepository<TUser, TKey> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService<TUser, TUserDto, TKey>> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="logger">Logger instance</param>
        public UserService(IUserRepository<TUser, TKey> userRepository, 
            IMapper mapper,
            ILogger<UserService<TUser, TUserDto, TKey>> logger = null)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger ?? NullLogger<UserService<TUser, TUserDto, TKey>>.Instance;
        }

        /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TCreateUserResult, TUpdateUserResult, TDeleteUserResult, TKey}.Create(TUserDto)" />
        public async Task<bool> Create(TUserDto entryToAdd)
        {
            _logger.LogDebug("Start creating new user");
            var addUserEntity = _mapper.Map<TUser>(entryToAdd);
            addUserEntity.CreatedDate = DateTime.Now;
            await _userRepository.CreateAsync(addUserEntity);
            return true;
        }

        /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TStatusResult, TKey}" />
        public async Task<bool> Delete(TKey idDto)
        {
            _logger.LogDebug($"Start deleting user with id {idDto}");
            var userToDelete = await _userRepository.GetOneAsync(idDto);
            _userRepository.Delete(userToDelete);
            
            return true;
        }

        /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TStatusResult, TKey}" />
        public Task<List<TUserDto>> GetAll()
        {
            var allUsers = _userRepository.GetAll().ToList();
            var result = _mapper.Map<List<TUserDto>>(allUsers);
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TStatusResult, TKey}" />
        public Task<TUserDto> GetOne(TKey idDto)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TStatusResult, TKey}" />
        public Task<bool> Update(TUserDto entryToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}