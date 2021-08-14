using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Models.Dto;

namespace DevQuiz.Libraries.Core.Services
{
    /// <summary>
    /// Service for manage Question entries
    /// </summary>
    public interface IQuestionService : IBaseService<QuestionDto, QuestionDto, IList<QuestionDto>, int, bool, bool, int>
    {
        #region Categories

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <param name="includeQuestions">Flag to include questions</param>
        /// <returns>Categories information collection</returns>
        Task<List<CategoryDto>> GetAllCategoriesAsync(bool includeQuestions);
        /// <summary>
        /// Get question category by id
        /// </summary>
        /// <param name="categoryId">Category unique identifier</param>
        /// <param name="includeQuestions">Flag to include questions</param>
        /// <returns>Question category information</returns>
        Task<CategoryDto> GetCategoryByIdAsync(int categoryId, bool includeQuestions);
        /// <summary>
        /// Get category by name
        /// </summary>
        /// <param name="categoryName">Category name</param>
        /// <param name="includeQuestions">Flag to include questions</param>
        /// <returns>Information about category</returns>
        Task<CategoryDto> GetCategoryByNameAsync(string categoryName, bool includeQuestions);
        /// <summary>
        /// Create new category entry
        /// </summary>
        /// <param name="categoryToCreate">Category new entry</param>
        /// <returns>Unique identifier of new category</returns>
        Task<int> CreateCategoryAsync(CategoryDto categoryToCreate);
        /// <summary>
        /// Update category information
        /// </summary>
        /// <param name="categoryToUpdate">Category to update</param>
        /// <returns>Method execution status</returns>
        Task<bool> UpdateCategoryAsync(CategoryDto categoryToUpdate);
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
        Task<List<TagDto>> GetAllTagsAsync(bool includeQuestions);
        /// <summary>
        /// Get tag by id
        /// </summary>
        /// <param name="tagId">Unique identifier of tag</param>
        /// <param name="includeQuestions">Flag to include questions</param>
        /// <returns>Information about tag</returns>
        Task<TagDto> GetTagByIdAsync(int tagId, bool includeQuestions);
        /// <summary>
        /// Get tag by name
        /// </summary>
        /// <param name="tagName">Name of tag</param>
        /// <param name="includeQuestions">Flag to include questions</param>
        /// <returns>Information about tag</returns>
        Task<TagDto> GetTagByNameAsync(string tagName, bool includeQuestions);
        /// <summary>
        /// Create new tag entry
        /// </summary>
        /// <param name="tagToCreate">Tag new entry</param>
        /// <returns>Unique identifier of new tag</returns>
        Task<int> CreateTagAsync(TagDto tagToCreate);
        /// <summary>
        /// Update tag information
        /// </summary>
        /// <param name="tagToUpdate">Tag to update</param>
        /// <returns>Method execution status</returns>
        Task<bool> UpdateTagAsync(TagDto tagToUpdate);
        /// <summary>
        /// Delete tag from store
        /// </summary>
        /// <param name="tagId">Unique identifier of tag</param>
        /// <returns>Method execution status</returns>
        Task<bool> DeleteTagAsync(int tagId);

        #endregion
    }
}
