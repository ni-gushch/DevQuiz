using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.TelegramBot.Models.ApiResults;
using DevQuiz.TelegramBot.Models.InputModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.TelegramBot.Controllers.Admin
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("/api/admin/[controller]")]
    public class QuestionController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<QuestionController> _logger;
        
        public QuestionController(IMediator mediator, 
            ILogger<QuestionController> logger = null)
        {
            _mediator = mediator;
            _logger = logger ?? NullLogger<QuestionController>.Instance;
        }

        [HttpGet("getall")]
        public async Task<List<QuestionApiResult>> GetAll()
        {
            
        }

        [HttpGet("get/{id:int}")]
        public async Task<QuestionApiResult> GetById([FromRoute] int id)
        {
            
        }

        [HttpPost("create")]
        public async Task<IdApiResult<int>> CreateQuestion([FromBody] CreateQuestionInputModel,
            CancellationToken cancellationToken)
        {
            
        }
    }
}