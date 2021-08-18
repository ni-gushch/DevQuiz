using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Admin.Core;
using DevQuiz.Admin.Core.Models.Entities;
using DevQuiz.Admin.Services.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DevQuiz.Admin.Services.Handlers.Admin
{
    /// <summary>
    ///     Handler for delete question action
    /// </summary>
    public class DeleteQuestionCommandHandler : BaseHandler<DeleteQuestionCommand>
    {
        private readonly IDevQuizUnitOfWork _unitOfWork;

        /// <summary>
        ///     Constructor with params
        /// </summary>
        /// <param name="mapper">Instance of type <see cref="IMapper" /></param>
        /// <param name="unitOfWork">Instance of <see cref="IDevQuizUnitOfWork" /></param>
        /// <param name="logger">Instance of <see cref="ILogger" /></param>
        public DeleteQuestionCommandHandler(IMapper mapper,
            IDevQuizUnitOfWork unitOfWork,
            ILogger<CreateQuestionCommandHandler> logger) : base(
            mapper, logger)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc cref="Handle" />
        public override async Task<Unit> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var entityInDb = await _unitOfWork.QuestionRepository.GetOneAsync(it => it.Id.Equals(request.Id),
                cancellationToken: cancellationToken);
            if (entityInDb == null)
                throw new Exception($"Entity of type {typeof(Question)} with id {request.Id} not found in store");
            _unitOfWork.QuestionRepository.Delete(entityInDb);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}