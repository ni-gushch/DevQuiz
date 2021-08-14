using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Libraries.Services.Queries;
using DevQuiz.Libraries.Services.Queries.GetQuestionById;
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
        private readonly IMapper _mapper;
        private readonly ILogger<QuestionController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">Instance of <see cref="IMediator"/></param>
        /// <param name="mapper">Instance of <see cref="IMapper"/></param>
        /// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/></param>
        public QuestionController(IMediator mediator, IMapper mapper, ILogger<QuestionController> logger = null)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger ?? NullLogger<QuestionController>.Instance;
        }

        /// <summary>
        /// Get information about all available questions
        /// </summary>
        /// <param name="cancellationToken">Instance of <see cref="CancellationToken"/></param>
        /// <returns>Collection of available questions</returns>
        [HttpGet("getall")]
        public async Task<List<QuestionApiResult>> GetAll(CancellationToken cancellationToken)
        {
            return _mapper.Map<List<QuestionApiResult>>(await _mediator.Send(new GetAllQuestionsQuery(),
                cancellationToken));
        }

        /// <summary>
        /// Get information about question by passed id
        /// </summary>
        /// <param name="id">Identifier of searched question</param>
        /// <param name="cancellationToken">Instance of <see cref="CancellationToken"/></param>
        /// <returns>Concrete question information</returns>
        [HttpGet("get/{id:int}")]
        public async Task<QuestionApiResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            return _mapper.Map<QuestionApiResult>(await _mediator.Send(new GetQuestionByIdQuery() {Id = id},
                cancellationToken));
        }

        /// <summary>
        /// Create new Question
        /// </summary>
        /// <param name="value">Create question model</param>
        /// <param name="cancellationToken">Instance of <see cref="CancellationToken"/></param>
        /// <returns>Identifier of new question</returns>
        [HttpPost("create")]
        public async Task<IdApiResult<int>> CreateQuestion([FromBody] CreateQuestionInputModel value,
            CancellationToken cancellationToken)
        {
            return await HandleActionAsync<CreateQuestionInputModel, GetQuestionByIdQuery, IdApiResult<int>>(value,
                cancellationToken);
        }

        private async Task<TResponse> HandleActionAsync<TRequest, TCommand, TResponse>(TRequest request,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<TCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);
            return _mapper.Map<TResponse>(result);
        }
    }
}