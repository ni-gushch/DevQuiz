using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Libraries.Core;
using DevQuiz.Libraries.Services.Queries.GetQuestionById;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevQuiz.Libraries.Services.Handlers.Admin.GetQuestionById
{
    public class GetQuestionByIdQueryHandler : BaseHandler<GetQuestionByIdQuery, GetQuestionByIdQueryResponse>
    {
        private readonly IDevQuizUnitOfWork _devQuizUnitOfWork;
        
        
        public GetQuestionByIdQueryHandler(IMapper mapper, IDevQuizUnitOfWork devQuizUnitOfWork, ILogger logger = null) : base(mapper, logger)
        {
            _devQuizUnitOfWork = devQuizUnitOfWork;
        }

        /// <inheritdoc cref="Handle"/> 
        public override async Task<GetQuestionByIdQueryResponse> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _devQuizUnitOfWork.QuestionRepository.GetOneAsync(it => it.Id.Equals(request.Id),
                include: it => it.Include(q => q.Category),
                cancellationToken: cancellationToken);
            return Mapper.Map<GetQuestionByIdQueryResponse>(request);
        }
    }
}