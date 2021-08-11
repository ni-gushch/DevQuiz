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
    /// Handler for update question action
    /// </summary>
    public class UpdateQuestionCommandHandler<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey> :
        BaseHandler<UpdateQuestionCommand>
        where TUser : UserBase<TUserKey>
        where TQuestion : QuestionBase<TAnswer, TCategory, TTag>
        where TAnswer : AnswerBase
        where TCategory : CategoryBase<TQuestion>
        where TTag : TagBase<TQuestion>
        where TUserKey : IEquatable<TUserKey>
    {
        private readonly IDevQuizUnitOfWork<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey> _unitOfWork;
        
        /// <summary>
        /// Constructor with params
        /// </summary>
        /// <param name="mapper">Instance of type <see cref="IMapper"/></param>
        /// <param name="unitOfWork">Instance of <see cref="IDevQuizUnitOfWork{TUser,TUserKey}"/></param>
        /// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/></param>
        public UpdateQuestionCommandHandler(IMapper mapper,
            IDevQuizUnitOfWork<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey> unitOfWork,
            ILogger<UpdateQuestionCommandHandler<TUser, TQuestion, TAnswer, TCategory, TTag, TUserKey>> logger) : base(mapper, logger)
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