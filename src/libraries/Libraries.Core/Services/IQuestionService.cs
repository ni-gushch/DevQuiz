using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public interface IQuestionService<TQuestionDto, TAnswerDto, TCategoryDto, TTagDto> : IBaseService<TQuestionDto, TQuestionDto, IList<TQuestionDto>, int, bool, bool, int>
        where TQuestionDto : QuestionDtoBase<TAnswerDto, TCategoryDto, TTagDto>
        where TAnswerDto : AnswerDtoBase
        where TCategoryDto : CategoryDtoBase<TQuestionDto>
        where TTagDto : TagDtoBase<TQuestionDto>
    {
        #region Categories

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<TCategoryDto>> GetAllCategoriesAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<TCategoryDto> GetCategoryByIdAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<TCategoryDto> GetCategoryByNameAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<int> CreateCategoryAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> DeleteCategoryAsync();

        #endregion

        #region Tags
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<TTagDto>> GetAllTagsAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<TTagDto> GetTagByIdAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<TTagDto> GetTagByNameAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<int> CreateTagAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> DeleteTagAsync();

        #endregion
    }
}
