using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Libraries.Core;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevQuiz.Libraries.Services
{
    /// <inheritdoc cref="ICategoryService{TUserDto, TKey}" />
    public class CategoryService<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey, 
            TQuestionDto, TAnswerDto, TCategoryDto, TTagDto> 
        : ICategoryService<TCategoryDto, TQuestionDto, TAnswerDto, TTagDto>
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
        private readonly IGenericRepository<TCategory> _categoryRepository;
        private readonly ILogger<CategoryService<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey,
            TQuestionDto, TAnswerDto, TCategoryDto, TTagDto>> _logger;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="unitOfWork"> Instance of UnitOfWork </param>
        /// <param name="mapper"> Mapper instance </param>
        /// <param name="logger"> Logger instance </param>
        public CategoryService(
            IDevQuizUnitOfWork<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey> unitOfWork,
            IMapper mapper,
            ILogger<CategoryService<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey,
                TQuestionDto, TAnswerDto, TCategoryDto, TTagDto>> logger)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = _unitOfWork.CategoryRepository;
            _mapper = mapper;
            _logger = logger;
        }
        
        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.GetAllAsync" />
        public async Task<IList<TCategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Getting all categories started");
            var categories = await _categoryRepository
                .ListAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            _logger.LogDebug("All results are received");
            var dto = _mapper.Map<List<TCategoryDto>>(categories);
            return dto;
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.GetByIdAsync" />
        public async Task<TCategoryDto> GetByIdAsync(int idDto, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug($"Getting category {idDto} started");
            var category = await _categoryRepository
                .GetOneAsync(it => it.Id.Equals(idDto),
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            var dto = _mapper.Map<TCategoryDto>(category);
            return dto;
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.CreateAsync" />
        public async Task<int> CreateAsync(TCategoryDto entryToAdd, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Start creating new category");
            var addCategoryEntity = _mapper.Map<TCategory>(entryToAdd);
            await _categoryRepository
                .CreateAsync(addCategoryEntity, cancellationToken)
                .ConfigureAwait(false);
            
            _logger.LogDebug("Create new category save changes");
            var commitStatus = await _unitOfWork
                .CommitAsync(cancellationToken)
                .ConfigureAwait(false);
            
            if (commitStatus.Equals(0))
                throw new DbUpdateException("Some error occurred while creating new category");

            return addCategoryEntity.Id;
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.UpdateAsync" />
        public async Task<bool> UpdateAsync(TCategoryDto entryToUpdate, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Start updating category");
            var categoryEntity = _mapper.Map<TCategory>(entryToUpdate);
            
            _unitOfWork.ClearChangeTracker();
            _categoryRepository.Update(categoryEntity);
            
            var commitStatus = await _unitOfWork
                .CommitAsync(cancellationToken)
                .ConfigureAwait(false);
            
            if (commitStatus.Equals(0))
                throw new DbUpdateException($"Some error occurred white updating category with id {entryToUpdate.Id}");
            
            return commitStatus > 0;
        }

        /// <inheritdoc cref="IBaseService{TEntryDto,TOneEntryResult,TAllEntriesResult,TCreateEntryResult,TUpdateEntryResult,TDeleteEntryResult,TKey}.DeleteAsync" />
        public async Task<bool> DeleteAsync(int idDto, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug($"Start deleting category with id {idDto}");
            var userToDelete = await _categoryRepository
                .GetOneAsync(it => it.Id.Equals(idDto), cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            _categoryRepository.Delete(userToDelete);
            
            _logger.LogDebug("Delete user save changes");
            var commitStatus = await _unitOfWork
                .CommitAsync(cancellationToken)
                .ConfigureAwait(false);
            
            if (commitStatus.Equals(0))
                throw new DbUpdateException($"Some error occurred white deleting user with id {idDto}");

            return commitStatus > 0;
        }
    }
}