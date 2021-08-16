using AutoMapper;
using Telegram.Bot.Types;

namespace DevQuiz.TelegramBot.Mappers
{
    /// <summary>
    /// Mapper profile for convert Telegram User information to Dto
    /// </summary>
    public class UserBotMapperProfile : Profile
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public UserBotMapperProfile()
        {
            /*-----------------*/
            /* TG model to Dto */
            /*-----------------*/

            // CreateMap<Chat, UserDto>()
            //     .ForMember(dest => dest.Id, opt => opt.Ignore())
            //     .ForMember(dest => dest.TelegramChatId, opt => opt.MapFrom(src => src.Id))
            //     .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
            //     .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            //     .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            //     .ForAllOtherMembers(opt => opt.Ignore());

            /*-----------------*/
            /* Dto to TG models */
            /*-----------------*/
        }
    }
}