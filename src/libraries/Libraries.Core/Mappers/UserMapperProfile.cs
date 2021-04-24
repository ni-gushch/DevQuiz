using System;
using AutoMapper;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Models.Entities;

namespace DevQuiz.Libraries.Core.Mappers
{
    /// <summary>
    /// Profile class for mapper user models
    /// </summary>
    public class UserMapperProfile<TUser, TUserDto, TUserKey> : Profile
        where TUser : UserBase<TUserKey>
        where TUserDto : UserDtoBase<TUserKey>
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserMapperProfile()
        {
            /*---------------*/
            /* Entity to Dto */
            /*---------------*/
            CreateMap<TUser, TUserDto>(MemberList.Destination)
                .ReverseMap();

            /*---------------*/
            /* Dto to Entity */
            /*---------------*/

            /*------------------*/
            /* Entity to Entity */
            /*------------------*/
        }
    }
}