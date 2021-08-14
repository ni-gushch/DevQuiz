using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Libraries.Core;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Services.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DevQuiz.Libraries.Services.Handlers.Admin
{
    /// <summary>
    /// Handler for delete question action
    /// </summary>
    public class DeleteQuestionCommandHandler<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey> :
        BaseHandler<DeleteQuestionCommand>
        where TUser : User<TUserKey>
        where TQuestion : Question
        where TAnswer : Answer
        where TCategory : Category
        where TTag : Tag
        where TUserKey : IEquatable<TUserKey>
    {
        private readonly IDevQuizUnitOfWork<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey> _unitOfWork;
        
        /// <summary>
        /// Constructor with params
        /// </summary>
        /// <param name="mapper">Instance of type <see cref="IMapper"/></param>
        /// <param name="unitOfWork">Instance of <see cref="IDevQuizUnitOfWork{TUser,TUserKey}"/></param>
        /// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/></param>
        public DeleteQuestionCommandHandler(IMapper mapper,
            IDevQuizUnitOfWork<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey> unitOfWork,
            ILogger<CreateQuestionCommandHandler<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey>> logger) : base(
            mapper, logger)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc cref="Handle"/> 
        public override async Task<Unit> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var entityInDb = await _unitOfWork.QuestionRepository.GetOneAsync(it => it.Id.Equals(request.Id),
                cancellationToken: cancellationToken);
            if (entityInDb == null)
                throw new Exception($"Entity of type {typeof(TQuestion)} with id {request.Id} not found in store");
            _unitOfWork.QuestionRepository.Delete(entityInDb);
            await _unitOfWork.CommitAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}