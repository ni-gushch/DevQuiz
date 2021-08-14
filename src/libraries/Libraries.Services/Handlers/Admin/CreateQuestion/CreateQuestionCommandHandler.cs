using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Libraries.Core;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Services.Commands.CreateQuestion;
using Microsoft.Extensions.Logging;

namespace DevQuiz.Libraries.Services.Handlers.Admin
{
    /// <summary>
    /// Handler for action on create question
    /// </summary>
    public class CreateQuestionCommandHandler<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey> :
        BaseHandler<CreateQuestionCommand, CreateQuestionCommandResponse>
        where TUser : User<TUserKey>
        where TQuestion : Question
        where TAnswer : Answer
        where TCategory : Category
        where TTag : Tag
        where TUserKey : IEquatable<TUserKey>
    {
        private readonly IDevQuizUnitOfWork<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey> _unitOfWork;

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="mapper">Instance of type <see cref="IMapper"/></param>
        /// <param name="unitOfWork">Instance of <see cref="IDevQuizUnitOfWork{TUser,TQuestion,TAnswer,TCategory,TTag,TUserKey}"/></param>
        /// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/></param>
        public CreateQuestionCommandHandler(IMapper mapper,
            IDevQuizUnitOfWork<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey> unitOfWork,
            ILogger<CreateQuestionCommandHandler<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey>> logger)
            : base(mapper, logger)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc cref="Handle"/> 
        public override async Task<CreateQuestionCommandResponse> Handle(CreateQuestionCommand request,
            CancellationToken cancellationToken)
        {
            var questionForCreate = Mapper.Map<TQuestion>(request);
            await _unitOfWork.QuestionRepository.CreateAsync(questionForCreate, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new CreateQuestionCommandResponse() {Id = questionForCreate.Id};
        }
    }
}