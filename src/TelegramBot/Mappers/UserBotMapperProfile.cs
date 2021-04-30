using AutoMapper;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Services.Dto;
using System;
using Telegram.Bot.Types;

namespace DevQuiz.TelegramBot.Mappers
{
    /// <summary>
    /// Mapper profile for convert Telegram User information to Dto
    /// </summary>
    public class UserBotMapperProfile<TUserDto, TKey> : Profile
        where TUserDto : UserDtoBase<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public UserBotMapperProfile()
        {
            /*-----------------*/
            /* TG model to Dto */
            /*-----------------*/

            CreateMap<Chat, TUserDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TelegramChatId, opt => opt.MapFrom(src => (int)src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForAllOtherMembers(opt => opt.Ignore());

            /*-----------------*/
            /* Dto to TG models */
            /*-----------------*/
        }
    }
}