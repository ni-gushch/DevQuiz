using MediatR;

namespace DevQuiz.Libraries.Services
{
    public interface IBaseCommand<out TCommandResponse> : IRequest<TCommandResponse>
        where TCommandResponse : class
    {
        
    }

    public interface IBaseCommand : IRequest
    {
        
    }
}