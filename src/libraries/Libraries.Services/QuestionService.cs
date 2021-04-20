using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.Libraries.Services
{
    public class QuestionService<TQuestion, TAnswer, TCategory, TTag, 
        TQuestionDto, TAnswerDto, TCategoryDto, TTagDto>
        where TQuestion : QuestionBase<TAnswer, TCategory, TTag>
        where TAnswer : AnswerBase
        where TCategory : CategoryBase<TQuestion>
        where TTag : TagBase<TQuestion>
        where TQuestionDto : QuestionDtoBase<TAnswerDto, TCategoryDto, TTagDto>
        where TAnswerDto : AnswerDtoBase
        where TCategoryDto : CategoryDtoBase<TQuestionDto>
        where TTagDto : TagDtoBase<TQuestionDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TQuestion> _questionRepository;
        private readonly IGenericRepository<TAnswer> _answerRepository;
        private readonly IGenericRepository<TCategory> _categoryRepository;
        private readonly IGenericRepository<TTag> _tagRepository;

        private readonly ILogger<QuestionService<TQuestion, TAnswer, TCategory, TTag,
            TQuestionDto, TAnswerDto, TCategoryDto, TTagDto>> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work instance</param>
        /// <param name="logger">Logger instance</param>
        public QuestionService(IUnitOfWork unitOfWork, 
            ILogger<QuestionService<TQuestion, TAnswer, TCategory, TTag,
                TQuestionDto, TAnswerDto, TCategoryDto, TTagDto>> logger = null)
        {
            _unitOfWork = unitOfWork;
            _questionRepository = _unitOfWork.GetRepository<IGenericRepository<TQuestion>, TQuestion>();
            _answerRepository = _unitOfWork.GetRepository<IGenericRepository<TAnswer>, TAnswer>();
            _categoryRepository = _unitOfWork.GetRepository<IGenericRepository<TCategory>, TCategory>();
            _tagRepository = _unitOfWork.GetRepository<IGenericRepository<TTag>, TTag>();

            _logger = NullLogger<QuestionService<TQuestion, TAnswer, TCategory, TTag, TQuestionDto, TAnswerDto, TCategoryDto, TTagDto>>.Instance;
        }
    }
}
