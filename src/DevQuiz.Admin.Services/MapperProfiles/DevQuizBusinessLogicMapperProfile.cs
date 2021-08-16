using AutoMapper;
using DevQuiz.Admin.Core.Models;
using DevQuiz.Admin.Core.Models.Entities;
using DevQuiz.Admin.Services.Commands;

namespace DevQuiz.Admin.Services.MapperProfiles
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
            CreateMap<Category, CategoryModel>(MemberList.Destination);
            
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