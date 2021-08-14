using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Libraries.Core;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.Libraries.Services
{
    /// <summary>
    /// Service for manage Question entries
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TQuestion"></typeparam>
    /// <typeparam name="TAnswer"></typeparam>
    /// <typeparam name="TCategory"></typeparam>
    /// <typeparam name="TTag"></typeparam>
    /// <typeparam name="TUserKey"></typeparam>
    /// <typeparam name="TQuestionDto">Generic Question dto</typeparam>
    /// <typeparam name="TAnswerDto">Generic Question Answer dto</typeparam>
    /// <typeparam name="TCategoryDto">Generic Question Answer dto</typeparam>
    /// <typeparam name="TTagDto">Generic Question Tag dto</typeparam>
    public class QuestionService<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey,
        TQuestionDto, TAnswerDto, TCategoryDto, TTagDto> : IQuestionService<TQuestionDto, TAnswerDto, TCategoryDto, TTagDto>
        where TUser : User<TUserKey>
        where TQuestion : Question
        where TAnswer : Answer
        where TCategory : Category
        where TTag : Tag
        where TUserKey : IEquatable<TUserKey>
        where TQuestionDto : QuestionDtoBase<TAnswerDto, TCategoryDto, TTagDto>
        where TAnswerDto : AnswerDtoBase
        where TCategoryDto : CategoryDtoBase<TQuestionDto>
        where TTagDto : TagDtoBase<TQuestionDto>
    {
        private readonly IDevQuizUnitOfWork<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey> _unitOfWork;
        private readonly IMapper _mapper;

        private readonly ILogger<QuestionService<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey,
            TQuestionDto, TAnswerDto, TCategoryDto, TTagDto>> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work instance</param>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="logger">Logger instance</param>
        public QuestionService(IDevQuizUnitOfWork<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey> unitOfWork,
            IMapper mapper,
            ILogger<QuestionService<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey,
                TQuestionDto, TAnswerDto, TCategoryDto, TTagDto>> logger = null)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            _logger = NullLogger<QuestionService<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey, TQuestionDto, TAnswerDto, TCategoryDto, TTagDto>>.Instance;
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.GetAllAsync" />
        public async Task<IList<TQuestionDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var questions = _unitOfWork.QuestionRepository
                .ListAsync(include: include => include.Include(it => it.Answers)
                        .Include(it => it.Category)
                        .Include(it => it.Tags),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return _mapper.Map<List<TQuestionDto>>(await questions);
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.GetByIdAsync" />
        public async Task<TQuestionDto> GetByIdAsync(int questionId, CancellationToken cancellationToken = default)
        {
            var questionEntity = await _unitOfWork.QuestionRepository
                .GetOneAsync(predicate: it => it.Id.Equals(questionId),
                    include: quest => quest.Include(it => it.Answers)
                        .Include(it => it.Category)
                        .Include(it => it.Tags),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            if (questionEntity == null)
                throw new Exception($"{typeof(TQuestion).Name} with id {questionId} not found in store");
            return _mapper.Map<TQuestionDto>(questionEntity);
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.CreateAsync" />
        public async Task<int> CreateAsync(TQuestionDto entryToAdd, CancellationToken cancellationToken = default)
        {
            var questionEntity = _mapper.Map<TQuestion>(entryToAdd);
            await _unitOfWork.QuestionRepository.CreateAsync(questionEntity, cancellationToken);
            var commitStatus = await _unitOfWork.CommitAsync(cancellationToken);
            if (commitStatus == 0)
                throw new Exception("Error while creating new Question");
            return questionEntity.Id;
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.UpdateAsync" />
        public async Task<bool> UpdateAsync(TQuestionDto entryToUpdate, CancellationToken cancellationToken = default)
        {
            var entityToUpdate = _mapper.Map<TQuestion>(entryToUpdate);
            var entityInDb = await _unitOfWork.QuestionRepository.GetOneAsync(it => it.Id.Equals(entityToUpdate.Id),
                    include: inc => inc.Include(it => it.Answers)
                        .Include(it => it.Category)
                        .Include(it => it.Tags),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            if (entityInDb == null)
                throw new Exception($"{typeof(TQuestion).Name} with id {entityToUpdate.Id} not found in store");
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
                throw new Exception($"{typeof(TQuestion).Name} with id {idDto} not found in store");
            
            _unitOfWork.QuestionRepository.Delete(entityInDb);
            var commitStatus = await _unitOfWork.CommitAsync(cancellationToken);
            return commitStatus > 0;
        }

        #region Categories

        /// <inheritdoc cref="IQuestionService{TQuestionDto,TAnswerDto,TCategoryDto,TTagDto}.GetAllCategoriesAsync"/>
        public async Task<List<TCategoryDto>> GetAllCategoriesAsync(bool includeQuestions)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService{TQuestionDto,TAnswerDto,TCategoryDto,TTagDto}.GetCategoryByIdAsync"/>
        public async Task<TCategoryDto> GetCategoryByIdAsync(int categoryId, bool includeQuestions)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService{TQuestionDto,TAnswerDto,TCategoryDto,TTagDto}.GetCategoryByNameAsync"/>
        public async Task<TCategoryDto> GetCategoryByNameAsync(string categoryName, bool includeQuestions)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService{TQuestionDto,TAnswerDto,TCategoryDto,TTagDto}.CreateCategoryAsync"/>
        public async Task<int> CreateCategoryAsync(TCategoryDto categoryToAdd)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService{TQuestionDto,TAnswerDto,TCategoryDto,TTagDto}.UpdateCategoryAsync"/>
        public async Task<bool> UpdateCategoryAsync(TCategoryDto categoryToUpdate)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService{TQuestionDto,TAnswerDto,TCategoryDto,TTagDto}.DeleteCategoryAsync"/>
        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tags

        /// <inheritdoc cref="IQuestionService{TQuestionDto,TAnswerDto,TCategoryDto,TTagDto}.GetAllTagsAsync"/>
        public async Task<List<TTagDto>> GetAllTagsAsync(bool includeQuestions)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService{TQuestionDto,TAnswerDto,TCategoryDto,TTagDto}.GetTagByIdAsync"/>
        public async Task<TTagDto> GetTagByIdAsync(int tagId, bool includeQuestions)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService{TQuestionDto,TAnswerDto,TCategoryDto,TTagDto}.GetTagByNameAsync"/>
        public async Task<TTagDto> GetTagByNameAsync(string tagName, bool includeQuestions)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService{TQuestionDto,TAnswerDto,TCategoryDto,TTagDto}.CreateTagAsync"/>
        public async Task<int> CreateTagAsync(TTagDto tagToAdd)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService{TQuestionDto,TAnswerDto,TCategoryDto,TTagDto}.UpdateCategoryAsync"/>
        public async Task<bool> UpdateTagAsync(TTagDto tagToUpdate)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IQuestionService{TQuestionDto,TAnswerDto,TCategoryDto,TTagDto}.DeleteTagAsync"/>
        public async Task<bool> DeleteTagAsync(int tagId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
