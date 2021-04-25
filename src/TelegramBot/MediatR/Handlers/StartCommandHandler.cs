using DevQuiz.Libraries.Core.Models.Dto;
using DevQuiz.Libraries.Core.Services;
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

        public StartCommandHandler(IUserService<TUserDto, TKey> userService)
        {
            _userService = userService;
        }

        public Task<Unit> Handle(StartCommand request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }
    }
}
