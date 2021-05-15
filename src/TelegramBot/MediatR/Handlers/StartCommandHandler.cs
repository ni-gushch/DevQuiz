using AutoMapper;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Services;
using DevQuiz.TelegramBot.Interfaces;
using DevQuiz.TelegramBot.MediatR.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace DevQuiz.TelegramBot.MediatR.Handlers
{
    /// <summary>
    ///     Command "/start" handler
    /// </summary>
    /// <typeparam name="TUserDto"> User dto for add or update </typeparam>
    /// <typeparam name="TKey"> Parameter with unique identifier of entry </typeparam>
    public class StartCommandHandler<TUserDto, TKey> : BaseBotCommandHandler, IRequestHandler<StartCommand>
        where TUserDto : UserDtoBase<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly IUserService<TUserDto, TKey> _userService;
        private readonly IMapper _mapper;

        private CancellationToken _cancellationToken;
        private Chat _chat;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="userService"> Service for manage users </param>
        /// <param name="mapper"> Mapper instance </param>
        /// <param name="botService"> Bot service </param>
        public StartCommandHandler(IUserService<TUserDto, TKey> userService,
            IMapper mapper,
            ILogger logger,
            IBotService botService = null) 
            : base(logger, botService) => 
            (_userService, _mapper) = (userService, mapper);

        /// <summary>
        ///     Handle command
        /// </summary>
        /// <param name="request"> Command </param>
        /// <param name="cancellationToken"> Cancellation token </param>
        /// <returns></returns>
        public async Task<Unit> Handle(StartCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));
            
            _chat = request.Chat;
            if (_chat is null)
            {
                _logger.LogWarning("The chat is null. The handle is impossible.");
                return Unit.Value;
            }
         
            _logger.LogInformation($"{nameof(StartCommand)}Handle begins for chatId={_chat.Id}.");
            _cancellationToken = cancellationToken;

            var userInDb =  await _userService.GetByChatIdAsync((int)_chat.Id, cancellationToken);

            userInDb = await CheckUserAsync(userInDb); 

            var answer = $"Hy, {userInDb?.FirstName} {userInDb?.LastName}!";

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

        private async Task SendCategories()
        {
            return; 
        }
    }
}
