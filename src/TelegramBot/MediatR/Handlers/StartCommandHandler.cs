using DevQuiz.TelegramBot.MediatR.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevQuiz.TelegramBot.MediatR.Handlers
{
    public class StartCommandHandler : IRequestHandler<StartCommand>
    {
        public Task<Unit> Handle(StartCommand request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }
    }
}
