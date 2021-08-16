using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Admin.Core;
using DevQuiz.Admin.Services.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DevQuiz.Admin.Services.Handlers.Admin
{
    /// <summary>
    /// Handler for update question action
    /// </summary>
    public class UpdateQuestionCommandHandler : BaseHandler<UpdateQuestionCommand>
    {
        private readonly IDevQuizUnitOfWork _unitOfWork;
        
        /// <summary>
        /// Constructor with params
        /// </summary>
        /// <param name="mapper">Instance of type <see cref="IMapper"/></param>
        /// <param name="unitOfWork">Instance of <see cref="IDevQuizUnitOfWork"/></param>
        /// <param name="logger">Instance of <see cref="ILogger"/></param>
        public UpdateQuestionCommandHandler(IMapper mapper,
            IDevQuizUnitOfWork unitOfWork,
            ILogger<UpdateQuestionCommandHandler> logger) : base(mapper, logger)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc cref="Handle"/> 
        public override Task<Unit> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}