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
        /// Get all categories
        /// </summary>
        /// <param name="includeQuestions">Flag to include questions</param>
        /// <returns>Categories information collection</returns>
        Task<List<TCategoryDto>> GetAllCategoriesAsync(bool includeQuestions);
        /// <summary>
        /// Get question category by id
        /// </summary>
        /// <param name="categoryId">Category unique identifier</param>
        /// <param name="includeQuestions">Flag to include questions</param>
        /// <returns>Question category information</returns>
        Task<TCategoryDto> GetCategoryByIdAsync(int categoryId, bool includeQuestions);
        /// <summary>
        /// Get category by name
        /// </summary>
        /// <param name="categoryName">Category name</param>
        /// <param name="includeQuestions">Flag to include questions</param>
        /// <returns>Information about category</returns>
        Task<TCategoryDto> GetCategoryByNameAsync(string categoryName, bool includeQuestions);
        /// <summary>
        /// Create new category entry
        /// </summary>
        /// <param name="categoryToCreate">Category new entry</param>
        /// <returns>Unique identifier of new category</returns>
        Task<int> CreateCategoryAsync(TCategoryDto categoryToCreate);
        /// <summary>
        /// Update category information
        /// </summary>
        /// <param name="categoryToUpdate">Category to update</param>
        /// <returns>Method execution status</returns>
        Task<bool> UpdateCategoryAsync(TCategoryDto categoryToUpdate);
        /// <summary>
        /// Delete category from store
        /// </summary>
        /// <param name="categoryId">Category unique identifier</param>
        /// <returns>Method execution status</returns>
        Task<bool> DeleteCategoryAsync(int categoryId);

        #endregion

        #region Tags

        /// <summary>
        /// Get all tags
        /// </summary>
        /// <param name="includeQuestions">Flag to include questions</param>
        /// <returns>Tag information collection</returns>
        Task<List<TTagDto>> GetAllTagsAsync(bool includeQuestions);
        /// <summary>
        /// Get tag by id
        /// </summary>
        /// <param name="tagId">Unique identifier of tag</param>
        /// <param name="includeQuestions">Flag to include questions</param>
        /// <returns>Information about tag</returns>
        Task<TTagDto> GetTagByIdAsync(int tagId, bool includeQuestions);
        /// <summary>
        /// Get tag by name
        /// </summary>
        /// <param name="tagName">Name of tag</param>
        /// <param name="includeQuestions">Flag to include questions</param>
        /// <returns>Information about tag</returns>
        Task<TTagDto> GetTagByNameAsync(string tagName, bool includeQuestions);
        /// <summary>
        /// Create new tag entry
        /// </summary>
        /// <param name="tagToCreate">Tag new entry</param>
        /// <returns>Unique identifier of new tag</returns>
        Task<int> CreateTagAsync(TTagDto tagToCreate);
        /// <summary>
        /// Update tag information
        /// </summary>
        /// <param name="tagToUpdate">Tag to update</param>
        /// <returns>Method execution status</returns>
        Task<bool> UpdateTagAsync(TTagDto tagToUpdate);
        /// <summary>
        /// Delete tag from store
        /// </summary>
        /// <param name="tagId">Unique identifier of tag</param>
        /// <returns>Method execution status</returns>
        Task<bool> DeleteTagAsync(int tagId);

        #endregion
    }
}
