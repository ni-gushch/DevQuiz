using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Libraries.Core;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Services;
using Microsoft.Extensions.Logging;

namespace DevQuiz.Libraries.Services
{
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

        private readonly ILogger<CategoryService<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey,
            TQuestionDto, TAnswerDto, TCategoryDto, TTagDto>> _logger;
        
        public async Task<IList<TCategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<TCategoryDto> GetByIdAsync(int idDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateAsync(TCategoryDto entryToAdd, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(TCategoryDto entryToUpdate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int idDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}