using AutoMapper;
using DevQuiz.Admin.Core.Models.Dto;
using DevQuiz.Admin.Core.Models.Entities;

namespace DevQuiz.Admin.Core.Mappers
{
    /// <summary>
    ///     Profile class for mapper user models
    /// </summary>
    public class UserMapperProfile : Profile
    {
        /// <summary>
        ///     Constructor
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