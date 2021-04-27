using AutoMapper;
using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Services;
using DevQuiz.Libraries.Data.Models;
using DevQuiz.Libraries.Services.Dto;
using DevQuiz.TelegramBot.MediatR.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevQuiz.TelegramBot.MediatR.Handlers
{
    public class StartCommandHandler<TUserDto, TKey> : IRequestHandler<StartCommand>
        where TUserDto : UserDtoBase<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly IUserService<TUserDto, TKey> _userService;
        private readonly IMapper _mapper;

        public StartCommandHandler(IUserService<TUserDto, TKey> userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(StartCommand request, CancellationToken cancellationToken)
        {
            var message = request.Message;
            var chat = message.Chat;
            var chatInDb =  await _userService.GetByChatIdAsync((int)chat.Id, cancellationToken);

            var userForCreate = _mapper.Map<TUserDto>(chat);
            //TUserDto userForCreate = new UserDto()
            //{
            //    TelegramChatId = (int)chat.Id,
            //    UserName = chat.Username,
            //    FirstName = chat.FirstName,
            //    LastName = chat.LastName
            //};
            await _userService.CreateAsync(userForCreate, cancellationToken);
            return Unit.Value;
        }
    }
}
