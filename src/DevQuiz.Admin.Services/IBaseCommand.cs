using MediatR;

namespace DevQuiz.Admin.Services
{
    /// <summary>
    ///     Base interface for command with response
    /// </summary>
    /// <typeparam name="TCommandResponse">Type of response from command</typeparam>
    public interface IBaseCommand<out TCommandResponse> : IRequest<TCommandResponse>
        where TCommandResponse : class
    {
    }

    /// <summary>
    ///     Base interface for command without response
    /// </summary>
    public interface IBaseCommand : IRequest
    {
    }
}