using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Libraries.Services.Queries;
using DevQuiz.TelegramBot.Models.ApiResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.TelegramBot.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/question/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;
        
        public CategoryController(IMediator mediator, IMapper mapper, ILogger<CategoryController> logger = null)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger ?? NullLogger<CategoryController>.Instance;
        }

        [HttpGet("getall")]
        public async Task<CategoriesApiResult> GetAll(CancellationToken cancellationToken)
        {
            return _mapper.Map<CategoriesApiResult>(await _mediator.Send(new GetAllCategoriesQuery(), 
                cancellationToken));
        }
    }
}