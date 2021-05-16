using System.Collections.Generic;
using DevQuiz.Libraries.Core.Models.Dto;

namespace DevQuiz.Libraries.Core.Services
{
    /// <summary>
    /// Service for manage Question entries
    /// </summary>
    /// <typeparam name="TQuestionDto">Generic Question dto</typeparam>
    /// <typeparam name="TAnswerDto">Generic Question Answer dto</typeparam>
    /// <typeparam name="TCategoryDto">Generic Question Answer dto</typeparam>
    /// <typeparam name="TTagDto">Generic Question Tag dto</typeparam>
    public interface IQuestionService<TQuestionDto, TAnswerDto, TCategoryDto, TTagDto> 
        : IBaseService<TQuestionDto, TQuestionDto, IList<TQuestionDto>, int, bool, bool, int>
        where TQuestionDto : QuestionDtoBase<TAnswerDto, TCategoryDto, TTagDto>
        where TAnswerDto : AnswerDtoBase
        where TCategoryDto : CategoryDtoBase<TQuestionDto>
        where TTagDto : TagDtoBase<TQuestionDto>
    {
    }
}
