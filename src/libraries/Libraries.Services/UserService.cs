using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Core.Services;

namespace DevQuiz.Libraries.Services
{
    /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TStatusResult, TKey}" />
    public class UserService<TUser, TKey> : IUserService<UserDto<Guid>, UserDto<Guid>, List<UserDto<Guid>>, bool, Guid>
        where TUser : UserBase<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly IUserRepository<TUser, TKey> _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="mapper">Mapper instance</param>
        public UserService(IUserRepository<TUser, TKey> userRepository, 
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TStatusResult, TKey}" />
        public Task<UserDto<Guid>> Create(UserDto<Guid> entryToAdd)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TStatusResult, TKey}" />
        public Task<bool> Delete(Guid idDto)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TStatusResult, TKey}" />
        public Task<List<UserDto<Guid>>> GetAll()
        {
            var allUsers = _userRepository.GetAll().ToList();
            var result = _mapper.Map<List<UserDto<Guid>>>(allUsers);
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TStatusResult, TKey}" />
        public Task<UserDto<Guid>> GetOne(Guid idDto)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc cref="IUserService{TUserDto, TOneUserResult, TAllUsersResult, TStatusResult, TKey}" />
        public Task<UserDto<Guid>> Update(UserDto<Guid> entryToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}