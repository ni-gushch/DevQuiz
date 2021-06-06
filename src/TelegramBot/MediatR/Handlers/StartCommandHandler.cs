using AutoMapper;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Services;
using DevQuiz.TelegramBot.Interfaces;
using DevQuiz.TelegramBot.MediatR.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace DevQuiz.TelegramBot.MediatR.Handlers
{
    /// <summary>
    ///     Command "/start" handler
    /// </summary>
    /// <typeparam name="TUserDto"> User dto for add or update </typeparam>
    /// <typeparam name="TKey"> Parameter with unique identifier of entry </typeparam>
    public class StartCommandHandler<TUserDto, TKey> : IRequestHandler<StartCommand>
        where TUserDto : UserDtoBase<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly IUserService<TUserDto, TKey> _userService;
        private readonly IMapper _mapper;
        private readonly IBotService _botService;

        private CancellationToken _cancellationToken;
        private Chat _chat;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="userService"> Service for manage users </param>
        /// <param name="mapper"> Mapper instance </param>
        public StartCommandHandler(IUserService<TUserDto, TKey> userService,
            IMapper mapper,
            IBotService botService = null)
        {
            _userService = userService;
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

            var userInDb =  await _userService.GetByChatIdAsync(_chat.Id, cancellationToken);

            userInDb = await CheckUserAsync(userInDb); 

            var answer = $"Hy, {userInDb?.FirstName} {userInDb?.LastName}!";

            // Совсем необязательно здороваться на команду \start.
            if (_botService != null)
                await _botService.Client.SendTextMessageAsync(request.Chat.Id, answer, cancellationToken: cancellationToken);

            return Unit.Value;
        }

        private async Task<TUserDto> CheckUserAsync(TUserDto userDto)
        {
            if (userDto is null)
            {
                var userForCreate = _mapper.Map<TUserDto>(_chat);
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
        }
    }
}
