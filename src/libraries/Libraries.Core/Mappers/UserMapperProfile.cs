using System;
using AutoMapper;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Models.Entities;

namespace DevQuiz.Libraries.Core.Mappers
{
    /// <summary>
    /// Profile class for mapper user models
    /// </summary>
    public class UserMapperProfile<TUser, TUserDto, TKey> : Profile
        where TUser : UserBase<TKey>
        where TUserDto : UserBaseDto<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserMapperProfile()
        {
            CreateMap<TUser, TUserDto>(MemberList.Destination)
                .ReverseMap();
        }
    }
}