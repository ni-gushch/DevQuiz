using AutoMapper;
using DevQuiz.Admin.Core.Models.Dto;
using DevQuiz.Admin.Core.Models.Entities;

namespace DevQuiz.Admin.Core.Mappers
{
    /// <summary>
    ///     Profile class for mapper user models
    /// </summary>
    public class QuestionMapperProfile : Profile
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public QuestionMapperProfile()
        {
            /*---------------*/
            /* Entity to Dto */
            /*---------------*/
            CreateMap<Question, QuestionDto>(MemberList.Destination)
                .ReverseMap();
            CreateMap<Answer, AnswerDto>(MemberList.Destination)
                .ReverseMap();
            CreateMap<Category, CategoryDto>(MemberList.Destination)
                .ReverseMap();
            CreateMap<Tag, TagDto>(MemberList.Destination)
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