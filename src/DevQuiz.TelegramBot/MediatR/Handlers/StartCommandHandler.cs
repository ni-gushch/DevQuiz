using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevQuiz.TelegramBot.Interfaces;
using DevQuiz.TelegramBot.MediatR.Commands;
using MediatR;
using Telegram.Bot.Types;

namespace DevQuiz.TelegramBot.MediatR.Handlers
{
    /// <summary>
    ///     Command "/start" handler
    /// </summary>
    public class StartCommandHandler : IRequestHandler<StartCommand>
    {
        private readonly IBotService _botService;

        //private readonly IUserService _userService;
        private readonly IMapper _mapper;

        private CancellationToken _cancellationToken;
        private Chat _chat;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="userService"> Service for manage users </param>
        /// <param name="mapper"> Mapper instance </param>
        public StartCommandHandler( /*IUserService userService,*/
            IMapper mapper,
            IBotService botService = null)
        {
            /*_userService = userService;*/
            _mapper = mapper;
            _botService = botService;
        }

        /// <summary>
        ///     Handle command
        /// </summary>
        /// <param name="request"> Command </param>
        /// <param name="cancellationToken"> Cancellation token </param>
        /// <returns></returns>
        public async Task<Unit> Handle(StartCommand request, CancellationToken cancellationToken)
        {
            _chat = request.Chat;
            _cancellationToken = cancellationToken;

            /*var userInDb =  await _userService.GetByChatIdAsync(_chat.Id, cancellationToken);

            userInDb = await CheckUserAsync(userInDb); */

            //var answer = $"Hy, {userInDb?.FirstName} {userInDb?.LastName}!";

            // Совсем необязательно здороваться на команду \start.
            // if (_botService != null)
            //     await _botService.Client.SendTextMessageAsync(request.Chat.Id, answer, cancellationToken: cancellationToken);

            return Unit.Value;
        }

        /*private async Task<UserDto> CheckUserAsync(UserDto userDto)
        {
            if (userDto is null)
            {
                var userForCreate = _mapper.Map<UserDto>(_chat);
                var userId = await _userService.CreateAsync(userForCreate, _cancellationToken);
                return await _userService.GetByIdAsync(userId, _cancellationToken);
            }
            
            var isSameNames = userDto.FirstName == _chat.FirstName
                && userDto.LastName == _chat.LastName;

            if (isSameNames) 
                return userDto;
            
            userDto.FirstName = _chat.FirstName;
            userDto.LastName = _chat.LastName;
            await _userService.UpdateAsync(userDto, _cancellationToken);
            return userDto;
        }*/
    }
}