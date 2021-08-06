using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Libraries.Core;
using DevQuiz.Libraries.Core.Models.Commands.Question;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.TelegramBot.MediatR.Handlers
{
    /// <summary>
    /// Handler for creation new Question
    /// </summary>
    public class CreateQuestionHandler : IRequestHandler<CreateQuestionCommand, CreateQuestionCommandResponse>
    {
        private readonly IDevQuizUnitOfWork<User, Question, Answer, Category, Tag, Guid> _unitOfWork;
        private readonly IGenericRepository<Question> _questionsRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateQuestionHandler> _logger;
        
        /// <summary>
        /// Constructor with params
        /// </summary>
        /// <param name="unitOfWork">Instance of <see cref="IDevQuizUnitOfWork{TUser,TQuestion,TAnswer,TCategory,TTag,TUserKey}"/></param>
        /// <param name="mapper">Instance of <see cref="IMapper"/></param>
        /// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/></param>
        public CreateQuestionHandler(IDevQuizUnitOfWork<User, Question, Answer, Category, Tag, Guid> unitOfWork, 
            IMapper mapper, 
            ILogger<CreateQuestionHandler> logger = null)
        {
            _unitOfWork = unitOfWork;
            _questionsRepo = _unitOfWork.GetRepository<IGenericRepository<Question>, Question>();
            _mapper = mapper;
            _logger = logger ?? NullLogger<CreateQuestionHandler>.Instance;
        }
        
        public async Task<CreateQuestionCommandResponse> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var entityToAdd = _mapper.Map<Question>(request);
            await _questionsRepo.CreateAsync(entityToAdd, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new CreateQuestionCommandResponse();
        }
    }
}