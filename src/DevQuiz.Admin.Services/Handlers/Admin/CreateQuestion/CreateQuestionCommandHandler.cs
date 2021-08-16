using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Admin.Core;
using DevQuiz.Admin.Core.Models.Entities;
using DevQuiz.Admin.Services.Commands;
using Microsoft.Extensions.Logging;

namespace DevQuiz.Admin.Services.Handlers.Admin
{
    /// <summary>
    /// Handler for action on create question
    /// </summary>
    public class CreateQuestionCommandHandler : BaseHandler<CreateQuestionCommand, CreateQuestionCommandResponse>
    {
        private readonly IDevQuizUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="mapper">Instance of type <see cref="IMapper"/></param>
        /// <param name="unitOfWork">Instance of <see cref="IDevQuizUnitOfWork"/></param>
        /// <param name="logger">Instance of <see cref="ILogger"/></param>
        public CreateQuestionCommandHandler(IMapper mapper,
            IDevQuizUnitOfWork unitOfWork,
            ILogger<CreateQuestionCommandHandler> logger)
            : base(mapper, logger)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc cref="Handle"/> 
        public override async Task<CreateQuestionCommandResponse> Handle(CreateQuestionCommand request,
            CancellationToken cancellationToken)
        {
            var questionForCreate = Mapper.Map<Question>(request);
            await _unitOfWork.QuestionRepository.CreateAsync(questionForCreate, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new CreateQuestionCommandResponse() {Id = questionForCreate.Id};
        }
    }
}