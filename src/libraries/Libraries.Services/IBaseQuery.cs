using MediatR;

namespace DevQuiz.Libraries.Services
{
    public interface IBaseQuery<TQueryResponse> : IRequest<TQueryResponse>
        where TQueryResponse : class
    {
    }
}