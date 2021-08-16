using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Admin.Core;
using DevQuiz.Admin.Services.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevQuiz.Admin.Services.Handlers.Admin
{
    /// <summary>
    /// Handler for get question by id action
    /// </summary>
    public class GetQuestionByIdQueryHandler : BaseHandler<GetQuestionByIdQuery, GetQuestionByIdQueryResponse>
    {
        private readonly IDevQuizUnitOfWork _devQuizUnitOfWork;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper">Instance of <see cref="IMapper"/></param>
        /// <param name="devQuizUnitOfWork">Instance of <see cref="IDevQuizUnitOfWork"/></param>
        /// <param name="logger">Instance of <see cref="ILogger"/></param>
        public GetQuestionByIdQueryHandler(IMapper mapper, IDevQuizUnitOfWork devQuizUnitOfWork, 
            ILogger<GetQuestionByIdQueryHandler> logger = null) : base(mapper, logger)
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