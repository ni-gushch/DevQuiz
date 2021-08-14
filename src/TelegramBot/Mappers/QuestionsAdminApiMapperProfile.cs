using AutoMapper;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Services.Commands.CreateQuestion;
using DevQuiz.Libraries.Services.Queries;
using DevQuiz.TelegramBot.Models;
using DevQuiz.TelegramBot.Models.ApiResults;
using DevQuiz.TelegramBot.Models.InputModels;

namespace DevQuiz.TelegramBot.Mappers
{
    /// <summary>
    /// Mapper profile for Question Category and Tags input models
    /// </summary>
    public class QuestionsAdminApiMapperProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public QuestionsAdminApiMapperProfile()
        {
            CreateMap<CreateQuestionInputModel, QuestionDto>();

            CreateMap<CreateQuestionInputModel, CreateQuestionCommand>(MemberList.Destination);

            CreateMap<CreateCategoryInputModel, CategoryDto>();
            CreateMap<CreateTagInputModel, TagDto>();

            CreateMap<QuestionDto, QuestionApiResult>()
                .ForMember(dest => dest.RightAnswer, opt => opt.MapFrom(src => new ValueModel<int>()
                {
                    Id = src.RightAnswerId,
                    Value = src.RightAnswerExplanation
                }));

            CreateMap<ValueModel<int>, CategoryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Value))
                .ReverseMap();
            CreateMap<ValueModel, CategoryDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Value))
                .ReverseMap();

            CreateMap<ValueModel<int>, TagDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Value))
                .ReverseMap();
            CreateMap<ValueModel, TagDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Value))
                .ReverseMap();

            CreateMap<ValueModel<int>, AnswerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Value))
                .ReverseMap();
            CreateMap<ValueModel, AnswerDto>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Value))
                .ReverseMap();
        }

        private void ResponsesToApiResultMaps()
        {
            CreateMap<GetAllCategoriesQueryResponse, CategoriesApiResult>(MemberList.Destination);
        }
    }
}