using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.Libraries.Core.Services;
using DevQuiz.Libraries.Services.Commands.CreateQuestion;
using DevQuiz.Libraries.Services.Dto;
using DevQuiz.TelegramBot.Models.ApiResults;
using DevQuiz.TelegramBot.Models.InputModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.TelegramBot.Controllers
{
    /// <summary>
    /// Controller for manage Questions Tags and Categories of DevQuiz
    /// </summary>
    [ApiController]
    [Route("/api/[controller]")]
    public class AdminController : Controller
    {
        private readonly IQuestionService<QuestionDto, AnswerDto, CategoryDto, TagDto> _questionService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<AdminController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="questionService">Question service instance</param>
        /// <param name="mediator"><see cref="IMediator"/> instance</param>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="logger">Logger instance</param>
        public AdminController(IQuestionService<QuestionDto, AnswerDto, CategoryDto, TagDto> questionService,
            IMediator mediator,
            IMapper mapper,
            ILogger<AdminController> logger = null)
        {
            _mediator = mediator;
            _questionService = questionService;
            _mapper = mapper;
            _logger = logger ?? NullLogger<AdminController>.Instance;
        }

        /// <summary>
        /// Method for getting all questions
        /// </summary>
        /// <returns>All questions collection</returns>
        [HttpGet("/Questions/GetAll")]
        public async Task<ActionResult<List<QuestionApiResult>>> GetAllQuestions()
        {
            var questions = _questionService.GetAllAsync();
            var result = _mapper.Map<List<QuestionApiResult>>(await questions);

            return result;
        }

        /// <summary>
        /// Get question information by his id
        /// </summary>
        /// <param name="id">Unique identifier of question</param>
        /// <returns>Information about question</returns>
        [HttpGet("/Questions/GetById/{id:int}")]
        public async Task<ActionResult<QuestionApiResult>> GetQuestionById([FromRoute] int id)
        {
            var question = _questionService.GetByIdAsync(id);
            var result = _mapper.Map<QuestionApiResult>(await question);

            return result;
        }

        /// <summary>
        /// Method for create new question
        /// </summary>
        /// <param name="value">New question model</param>
        /// <returns>Unique identifier of new question</returns>
        [HttpPost("/Questions/Create")]
        public async Task<ActionResult<IdApiResult<int>>> CreateQuestion([FromBody] CreateQuestionInputModel value)
        {
            var createCommand = _mapper.Map<CreateQuestionCommand>(value);
            
            var result = await _mediator.Send(createCommand);
            
            return new IdApiResult<int>(result.Id);
        }

        /// <summary>
        /// Method for update question information
        /// </summary>
        /// <param name="value">Question to update</param>
        /// <returns>Operation status</returns>
        [HttpPut("/Questions/Update")]
        public async Task<ActionResult<ActionStatusApiResult>> UpdateQuestion([FromBody] UpdateQuestionInputModel value)
        {
            var entryDto = _mapper.Map<QuestionDto>(value);
            var status = _questionService.UpdateAsync(entryDto);

            return new ActionStatusApiResult(await status);
        }

        /// <summary>
        /// Method for delete question
        /// </summary>
        /// <param name="id">Unique identifier of question</param>
        /// <returns>Operation status</returns>
        [HttpDelete("/Questions/Delete/{id:int}")]
        public async Task<ActionResult<ActionStatusApiResult>> DeleteQuestion([FromRoute] int id)
        {
            var status = _questionService.DeleteAsync(id);

            return new ActionStatusApiResult(await status);
        }

        #region Categories

        /// <summary>
        /// Method for getting all question categories
        /// </summary>
        /// <param name="includeQuestions">Flag for include questions in answer</param>
        /// <returns>All questions collection</returns>
        [HttpGet("/Categories/GetAll")]
        public async Task<ActionResult<List<CategoriesApiResult>>> GetAllCategories([FromQuery] bool includeQuestions)
        {
            var categories = _questionService.GetAllCategoriesAsync(includeQuestions);
            var result = _mapper.Map<List<CategoriesApiResult>>(await categories);

            return result;
        }

        /// <summary>
        /// Get category information by his id
        /// </summary>
        /// <param name="id">Unique identifier of question</param>
        /// <param name="includeQuestions">Flag for include questions in answer</param>
        /// <returns>Information about question category</returns>
        [HttpGet("/Categories/GetById/{id:int}")]
        public async Task<ActionResult<QuestionApiResult>> GetCategoryById([FromRoute] int id, [FromQuery] bool includeQuestions)
        {
            var question = _questionService.GetCategoryByIdAsync(id, includeQuestions);
            var result = _mapper.Map<QuestionApiResult>(await question);

            return result;
        }

        /// <summary>
        /// Method for create new category
        /// </summary>
        /// <param name="value">New category model</param>
        /// <returns>Unique identifier of new category</returns>
        [HttpPost("/Categories/Create")]
        public async Task<ActionResult<IdApiResult<int>>> CreateCategory([FromBody] CreateQuestionInputModel value)
        {
            var entryDto = _mapper.Map<CategoryDto>(value);
            var newQuestionId = _questionService.CreateCategoryAsync(entryDto);

            return new IdApiResult<int>(await newQuestionId);
        }

        /// <summary>
        /// Method for update question category information
        /// </summary>
        /// <param name="value">Category to update</param>
        /// <returns>Operation status</returns>
        [HttpPut("/Categories/Update")]
        public async Task<ActionResult<ActionStatusApiResult>> UpdateCategory([FromBody] UpdateQuestionInputModel value)
        {
            var entryDto = _mapper.Map<CategoryDto>(value);
            var status = _questionService.UpdateCategoryAsync(entryDto);

            return new ActionStatusApiResult(await status);
        }

        /// <summary>
        /// Method for delete category
        /// </summary>
        /// <param name="id">Unique identifier of question</param>
        /// <returns>Operation status</returns>
        [HttpDelete("/Categories/Delete/{id:int}")]
        public async Task<ActionResult<ActionStatusApiResult>> DeleteCategory([FromRoute] int id)
        {
            var status = _questionService.DeleteCategoryAsync(id);

            return new ActionStatusApiResult(await status);
        }

        #endregion

        #region Tags

        /// <summary>
        /// Method for getting all questions tags
        /// </summary>
        /// <param name="includeQuestions">Flag for include questions in answer</param>
        /// <returns>All tags collection</returns>
        [HttpGet("/Tags/GetAll")]
        public async Task<ActionResult<List<QuestionApiResult>>> GetAllTags([FromQuery] bool includeQuestions)
        {
            var questions = _questionService.GetAllTagsAsync(includeQuestions);
            var result = _mapper.Map<List<QuestionApiResult>>(await questions);

            return result;
        }

        /// <summary>
        /// Get tag information by his id
        /// </summary>
        /// <param name="id">Unique identifier of tag</param>
        /// <param name="includeQuestions">Flag for include questions in answer</param>
        /// <returns>Information about tag</returns>
        [HttpGet("/Tags/GetById/{id:int}")]
        public async Task<ActionResult<QuestionApiResult>> GetTagById([FromRoute] int id, [FromQuery] bool includeQuestions)
        {
            var question = _questionService.GetTagByIdAsync(id, includeQuestions);
            var result = _mapper.Map<QuestionApiResult>(await question);

            return result;
        }

        /// <summary>
        /// Method for create new tag
        /// </summary>
        /// <param name="value">New tag model</param>
        /// <returns>Unique identifier of new tag</returns>
        [HttpPost("/Tags/Create")]
        public async Task<ActionResult<IdApiResult<int>>> CreateTag([FromBody] CreateQuestionInputModel value)
        {
            var entryDto = _mapper.Map<TagDto>(value);
            var newQuestionId = _questionService.CreateTagAsync(entryDto);

            return new IdApiResult<int>(await newQuestionId);
        }

        /// <summary>
        /// Method for update tag information
        /// </summary>
        /// <param name="value">Tag to update</param>
        /// <returns>Operation status</returns>
        [HttpPut("/Tags/Update")]
        public async Task<ActionResult<ActionStatusApiResult>> UpdateTag([FromBody] UpdateQuestionInputModel value)
        {
            var entryDto = _mapper.Map<TagDto>(value);
            var status = _questionService.UpdateTagAsync(entryDto);

            return new ActionStatusApiResult(await status);
        }

        /// <summary>
        /// Method for delete tag
        /// </summary>
        /// <param name="id">Unique identifier of tag</param>
        /// <returns>Operation status</returns>
        [HttpDelete("/Tags/Delete/{id:int}")]
        public async Task<ActionResult<ActionStatusApiResult>> DeleteTag([FromRoute] int id)
        {
            var status = _questionService.DeleteTagAsync(id);

            return new ActionStatusApiResult(await status);
        }

        #endregion
    }
}
