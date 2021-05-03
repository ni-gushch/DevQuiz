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
        where TUser : UserBase<TUserKey>
        where TQuestion : QuestionBase<TAnswer, TCategory, TTag>
        where TAnswer : AnswerBase
        where TCategory : CategoryBase<TQuestion>
        where TTag : TagBase<TQuestion>
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
                .ListAsync(include: include => include.Include(it => it.Category)
                        .Include(it => it.Tags)
                        .Include(it => it.Answers),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return _mapper.Map<List<TQuestionDto>>(await questions);
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.GetByIdAsync" />
        public async Task<TQuestionDto> GetByIdAsync(int questionId, CancellationToken cancellationToken = default)
        {
            var questionEntity = await _unitOfWork.QuestionRepository
                .GetOneAsync(predicate: it => it.Id.Equals(questionId),
                    include: quest => quest.Include(it => it.Answers),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);
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
        public Task<bool> UpdateAsync(TQuestionDto entryToUpdate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.DeleteAsync" />
        public Task<bool> DeleteAsync(int idDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TQuestionDto>> GetByCategoryIdAsync(int categoryId,
            CancellationToken cancellationToken = default)
        {
            var questions = _unitOfWork.QuestionRepository
                .ListAsync(predicate: it => it.CategoryId.Equals(categoryId),
                    include: quest => quest.Include(it => it.Answers),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            return _mapper.Map<List<TQuestionDto>>(await questions);
        }

        public async Task<List<TCategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken = default)
        {
            var categories = _unitOfWork.CategoryRepository
                .ListAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            return _mapper.Map<List<TCategoryDto>>(await categories);
        }

        #region Categories

        public async Task<List<TTagDto>> GetAllCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TTagDto> GetCategoryByIdAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TTagDto> GetCategoryByNameAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCategoryAsync()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tags

        public async Task<List<TTagDto>> GetAllTagsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TTagDto> GetTagByIdAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TTagDto> GetTagByNameAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateTagAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteTagAsync()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
