using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Admin.Core;
using DevQuiz.Admin.Core.Models;
using DevQuiz.Admin.Services.Queries;
using Microsoft.Extensions.Logging;

namespace DevQuiz.Admin.Services.Handlers.Admin
{
    /// <summary>
    /// Handler for get all available categories
    /// </summary>
    public class GetAllCategoriesHandler : BaseHandler<GetAllCategoriesQuery, GetAllCategoriesQueryResponse>
    {
        private readonly IDevQuizUnitOfWork _devQuizUnitOfWork;
        
        /// <summary>
        /// Constructor with params
        /// </summary>
        /// <param name="mapper">Instance of <see cref="IMapper"/></param>
        /// <param name="devQuizUnitOfWork">Instance of <see cref="IDevQuizUnitOfWork"/></param>
        /// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/></param>
        public GetAllCategoriesHandler(IMapper mapper, IDevQuizUnitOfWork devQuizUnitOfWork, 
            ILogger<GetAllCategoriesHandler> logger = null) : base(mapper, logger)
        {
            _devQuizUnitOfWork = devQuizUnitOfWork;
        }

        /// <inheritdoc cref="Handle"/>
        public override async Task<GetAllCategoriesQueryResponse> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _devQuizUnitOfWork.CategoryRepository.ListAsync(cancellationToken: cancellationToken);
            return new GetAllCategoriesQueryResponse()
            {
                Categories = Mapper.Map<List<CategoryModel>>(result)
            };
        }
    }
}