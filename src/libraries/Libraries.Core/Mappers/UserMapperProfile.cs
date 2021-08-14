using System;
using AutoMapper;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Models.Entities;

namespace DevQuiz.Libraries.Core.Mappers
{
    /// <summary>
    /// Profile class for mapper user models
    /// </summary>
    public class UserMapperProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserMapperProfile()
        {
            /*---------------*/
            /* Entity to Dto */
            /*---------------*/
            CreateMap<User, UserDto>(MemberList.Destination)
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