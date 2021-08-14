using System;
using AutoMapper;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Services.Commands;
using DevQuiz.Libraries.Services.Commands.CreateQuestion;

namespace DevQuiz.Libraries.Services.MapperProfiles
{
    /// <summary>
    /// Mapper profile for business logic layer
    /// </summary>
    public class DevQuizBusinessLogicMapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public DevQuizBusinessLogicMapperProfile()
        {
            CreateCommandsToEntitiesMaps();
            CreateEntitiesToCommandResponsesMaps();
        }

        private void CreateCommandsToEntitiesMaps()
        {
            CreateMap<CreateQuestionCommand, Question>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest) => default(int)))
                ;
            
            CreateMap<UpdateQuestionCommand, Question>(MemberList.Destination)
                ;
        }

        private void CreateEntitiesToCommandResponsesMaps()
        {
            
        }
    }
}