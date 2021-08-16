using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Admin.Core;
using DevQuiz.Admin.Core.Models.Dto;
using DevQuiz.Admin.Core.Models.Entities;
using DevQuiz.Admin.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.Admin.Services
{
    /// <summary>
    /// Service for manage Question entries
    /// </summary>
    public class QuestionService : IQuestionService
    {
        private readonly IDevQuizUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<QuestionService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work instance</param>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="logger">Logger instance</param>
        public QuestionService(IDevQuizUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<QuestionService> logger = null)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            _logger = NullLogger<QuestionService>.Instance;
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.GetAllAsync" />
        public async Task<IList<QuestionDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var questions = _unitOfWork.QuestionRepository
                .ListAsync(include: include => include.Include(it => it.Answers)
                        .Include(it => it.Category)
                        .Include(it => it.Tags),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return _mapper.Map<List<QuestionDto>>(await questions);
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.GetByIdAsync" />
        public async Task<QuestionDto> GetByIdAsync(int questionId, CancellationToken cancellationToken = default)
        {
            var questionEntity = await _unitOfWork.QuestionRepository
                .GetOneAsync(predicate: it => it.Id.Equals(questionId),
                    include: quest => quest.Include(it => it.Answers)
                        .Include(it => it.Category)
                        .Include(it => it.Tags),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            if (questionEntity == null)
                throw new Exception($"{nameof(Question)} with id {questionId} not found in store");
            return _mapper.Map<QuestionDto>(questionEntity);
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.CreateAsync" />
        public async Task<int> CreateAsync(QuestionDto entryToAdd, CancellationToken cancellationToken = default)
        {
            var questionEntity = _mapper.Map<Question>(entryToAdd);
            await _unitOfWork.QuestionRepository.CreateAsync(questionEntity, cancellationToken);
            var commitStatus = await _unitOfWork.CommitAsync(cancellationToken);
            if (commitStatus == 0)
                throw new Exception("Error while creating new Question");
            return questionEntity.Id;
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.UpdateAsync" />
        public async Task<bool> UpdateAsync(QuestionDto entryToUpdate, CancellationToken cancellationToken = default)
        {
            var entityToUpdate = _mapper.Map<Question>(entryToUpdate);
            var entityInDb = await _unitOfWork.QuestionRepository.GetOneAsync(it => it.Id.Equals(entityToUpdate.Id),
                    include: inc => inc.Include(it => it.Answers)
                        .Include(it => it.Category)
                        .Include(it => it.Tags),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            if (entityInDb == null)
                throw new Exception($"{nameof(Question)} with id {entityToUpdate.Id} not found in store");
            _mapper.Map(entryToUpdate, entityInDb);
            _unitOfWork.QuestionRepository.Update(entityInDb);
            var commitStatus = await _unitOfWork.CommitAsync(cancellationToken);
            return commitStatus > 0;
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.DeleteAsync" />
        public async Task<bool> DeleteAsync(int idDto, CancellationToken cancellationToken = default)
        {
            var entityInDb = await _unitOfWork.QuestionRepository.GetOneAsync(it => it.Id.Equals(idDto),
                    include: inc => inc.Include(it => it.Answers)
                        .Include(it => it.Category)
                        .Include(it => it.Tags),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            if (entityInDb == null)
                throw new Exception($"{nameof(Question)} with id {idDto} not found in store");
            
            _unitOfWork.QuestionRepository.Delete(entityInDb);
            var commitStatus = await _unitOfWork.CommitAsync(cancellationToken);
            return commitStatus > 0;
        }

        #region Categories

        /// <inheritdoc cref="IQuestionService.GetAllCategoriesAsync"/>
        public async Task<List<CategoryDto>> GetAllCategoriesAsync(bool includeQuestions)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService.GetCategoryByIdAsync"/>
        public async Task<CategoryDto> GetCategoryByIdAsync(int categoryId, bool includeQuestions)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService.GetCategoryByNameAsync"/>
        public async Task<CategoryDto> GetCategoryByNameAsync(string categoryName, bool includeQuestions)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService.CreateCategoryAsync"/>
        public async Task<int> CreateCategoryAsync(CategoryDto categoryToAdd)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService.UpdateCategoryAsync"/>
        public async Task<bool> UpdateCategoryAsync(CategoryDto categoryToUpdate)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService.DeleteCategoryAsync"/>
        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tags

        /// <inheritdoc cref="IQuestionService.GetAllTagsAsync"/>
        public async Task<List<TagDto>> GetAllTagsAsync(bool includeQuestions)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService.GetTagByIdAsync"/>
        public async Task<TagDto> GetTagByIdAsync(int tagId, bool includeQuestions)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService.GetTagByNameAsync"/>
        public async Task<TagDto> GetTagByNameAsync(string tagName, bool includeQuestions)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService.CreateTagAsync"/>
        public async Task<int> CreateTagAsync(TagDto tagToAdd)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService.UpdateCategoryAsync"/>
        public async Task<bool> UpdateTagAsync(TagDto tagToUpdate)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService.DeleteTagAsync"/>
        public async Task<bool> DeleteTagAsync(int tagId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
