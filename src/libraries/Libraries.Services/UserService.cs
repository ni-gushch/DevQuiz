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
    /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TStatusResult, TKey}" />
    public class UserService<TUser, TKey> : IUserService<UserDto, UserDto, List<UserDto>, bool, Guid>
        where TUser : UserBase<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly IUserRepository<TUser, TKey> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService<TUser, TKey>> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="logger">Logger instance</param>
        public UserService(IUserRepository<TUser, TKey> userRepository, 
            IMapper mapper,
            ILogger<UserService<TUser, TKey>> logger = null)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger ?? NullLogger<UserService<TUser, TKey>>.Instance;
        }

        /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TStatusResult, TKey}" />
        public Task<UserDto> Create(UserDto entryToAdd)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TStatusResult, TKey}" />
        public Task<bool> Delete(Guid idDto)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TStatusResult, TKey}" />
        public Task<List<UserDto>> GetAll()
        {
            var allUsers = _userRepository.GetAll().ToList();
            var result = _mapper.Map<List<UserDto>>(allUsers);
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TStatusResult, TKey}" />
        public Task<UserDto> GetOne(Guid idDto)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TStatusResult, TKey}" />
        public Task<UserDto> Update(UserDto entryToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}