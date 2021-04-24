using AutoMapper;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Models.Entities;

namespace DevQuiz.Libraries.Core.Mappers
{
    /// <summary>
    /// Profile class for mapper user models
    /// </summary>
    /// <typeparam name="TQuestion"></typeparam>
    /// <typeparam name="TAnswer"></typeparam>
    /// <typeparam name="TCategory"></typeparam>
    /// <typeparam name="TTag"></typeparam>
    /// <typeparam name="TQuestionDto">Generic Question dto</typeparam>
    /// <typeparam name="TAnswerDto">Generic Question Answer dto</typeparam>
    /// <typeparam name="TCategoryDto">Generic Question Answer dto</typeparam>
    /// <typeparam name="TTagDto">Generic Question Tag dto</typeparam>
    public class QuestionMapperProfile<TQuestion, TAnswer, TCategory, TTag,
        TQuestionDto, TAnswerDto, TCategoryDto, TTagDto> : Profile
        where TQuestion : QuestionBase<TAnswer, TCategory, TTag>
        where TAnswer : AnswerBase
        where TCategory : CategoryBase<TQuestion>
        where TTag : TagBase<TQuestion>
        where TQuestionDto : QuestionDtoBase<TAnswerDto, TCategoryDto, TTagDto>
        where TAnswerDto : AnswerDtoBase
        where TCategoryDto : CategoryDtoBase<TQuestionDto>
        where TTagDto : TagDtoBase<TQuestionDto>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public QuestionMapperProfile()
        {
            /*---------------*/
            /* Entity to Dto */
            /*---------------*/
            CreateMap<TQuestion, TQuestionDto>(MemberList.Destination);
            CreateMap<TAnswer, TAnswerDto>(MemberList.Destination);
            CreateMap<TCategory, TCategoryDto>(MemberList.Destination);
            CreateMap<TTag, TTagDto>(MemberList.Destination);

            /*---------------*/
            /* Dto to Entity */
            /*---------------*/

            /*------------------*/
            /* Entity to Entity */
            /*------------------*/
        }
    }
}
