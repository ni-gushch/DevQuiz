using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Admin.Hosting.Models.ApiResults;
using DevQuiz.Admin.Services.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.Admin.Hosting.Controllers
{
    /// <summary>
    ///     Controller for manage question categories
    /// </summary>
    [ApiController]
    [Route("api/admin/question/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        /// <summary>
        ///     Constructor with parameters
        /// </summary>
        /// <param name="mediator">Instance of <see cref="IMediator" /></param>
        /// <param name="mapper">Instance of <see cref="IMapper" /></param>
        /// <param name="logger">Instance of <see cref="ILogger{TCategoryName}" /></param>
        public CategoryController(IMediator mediator, IMapper mapper, ILogger<CategoryController> logger = null)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger ?? NullLogger<CategoryController>.Instance;
        }

        /// <summary>
        ///     Get all categories from store
        /// </summary>
        /// <param name="cancellationToken">Instance of <see cref="CancellationToken" /></param>
        /// <returns>All available categories</returns>
        [HttpGet("getall")]
        public async Task<CategoriesApiResult> GetAll(CancellationToken cancellationToken)
        {
            return _mapper.Map<CategoriesApiResult>(await _mediator.Send(new GetAllCategoriesQuery(),
                cancellationToken));
        }
    }
}