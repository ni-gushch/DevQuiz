using MediatR;

namespace DevQuiz.Admin.Services
{
    /// <summary>
    /// Base interface for query with response
    /// </summary>
    /// <typeparam name="TQueryResponse">Type of query response</typeparam>
    public interface IBaseQuery<out TQueryResponse> : IRequest<TQueryResponse>
        where TQueryResponse : class
    {
    }
}