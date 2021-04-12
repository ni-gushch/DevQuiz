using AutoMapper;
using DevQuiz.Libraries.Services.Dto;
using Telegram.Bot.Types;

namespace DevQuiz.TelegramBot.Mappers
{
    /// <summary>
    /// Mapper profile for convert Telegram User information to Dto
    /// </summary>
    public class UserBotMapperProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserBotMapperProfile()
        {
            /*-----------------*/
            /* TG model to Dto */
            /*-----------------*/
            CreateMap<User, UserDto>(MemberList.Destination)
                .ForMember(dest => dest.TelegramId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));


            /*-----------------*/
            /* Dto to TG models */
            /*-----------------*/
        }
    }
}