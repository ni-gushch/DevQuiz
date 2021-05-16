using System;
using System.Collections.Generic;
using DevQuiz.Libraries.Core.Models.Dto;

namespace DevQuiz.Libraries.Core.Services
{
    /// <summary>
    ///     Service for manage Question entries
    /// </summary>
    /// <typeparam name="TQuestionDto"> Generic Question dto </typeparam>
    /// <typeparam name="TAnswerDto"> Generic Question Answer dto </typeparam>
    /// <typeparam name="TCategoryDto"> Generic Question Answer dto </typeparam>
    /// <typeparam name="TTagDto"> Generic Question Tag dto </typeparam>
    public interface ICategoryService<TCategoryDto, TQuestionDto, TAnswerDto, TTagDto>
        : IBaseService<TCategoryDto, TCategoryDto, IList<TCategoryDto>, int, bool, bool, int>
        where TCategoryDto : CategoryDtoBase<TQuestionDto>
        where TQuestionDto : QuestionDtoBase<TAnswerDto, TCategoryDto, TTagDto>
        where TAnswerDto : AnswerDtoBase
        where TTagDto : TagDtoBase<TQuestionDto>
    {
    }
}