using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DevQuiz.Libraries.Services
{
    /// <summary>
    /// Base abstract handler
    /// </summary>
    /// <typeparam name="TRequest">Type of handler request</typeparam>
    /// <typeparam name="TResponse">Type of handler response</typeparam>
    public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        /// <summary>
        /// Logger
        /// </summary>
        protected ILogger Logger { get; }
        /// <summary>
        /// Mapper object
        /// </summary>
        protected IMapper Mapper { get; }

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="mapper">Instance of type <see cref="IMapper"/></param>
        /// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/></param>
        protected BaseHandler(IMapper mapper, 
            ILogger logger = null)
        {
            Logger = logger ?? NullLogger.Instance;
            Mapper = mapper;
        }
        
        /// <summary>
        /// Method for handle command/query
        /// </summary>
        /// <param name="request">Request instance</param>
        /// <param name="cancellationToken">Instance of type <see cref="CancellationToken"/></param>
        /// <returns>Response of executed handler</returns>
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
    
    /// <summary>
    /// Base abstract handler
    /// </summary>
    /// <typeparam name="TRequest">Type of handler request</typeparam>
    public abstract class BaseHandler<TRequest> : IRequestHandler<TRequest>
        where TRequest : IRequest
    {
        /// <summary>
        /// Logger
        /// </summary>
        protected ILogger Logger { get; }
        /// <summary>
        /// Mapper object
        /// </summary>
        protected IMapper Mapper { get; }

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="mapper">Instance of type <see cref="IMapper"/></param>
        /// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/></param>
        protected BaseHandler(IMapper mapper, 
            ILogger logger = null)
        {
            Logger = logger ?? NullLogger.Instance;
            Mapper = mapper;
        }
        
        /// <summary>
        /// Method for handle command/query
        /// </summary>
        /// <param name="request">Request instance</param>
        /// <param name="cancellationToken">Instance of type <see cref="CancellationToken"/></param>
        /// <returns>Response of executed handler</returns>
        public abstract Task<Unit> Handle(TRequest request, CancellationToken cancellationToken);
    }
}