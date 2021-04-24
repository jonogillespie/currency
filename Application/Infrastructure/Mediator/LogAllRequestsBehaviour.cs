using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.External;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Infrastructure.Mediator
{
    public class LogAllRequestsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly IRequestIdService _requestIdService;

        public LogAllRequestsBehavior(ILogger<TRequest> logger, 
            IRequestIdService requestIdService)
        {
            _logger = logger;
            _requestIdService = requestIdService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var name = typeof(TRequest).Name;
            
            _logger.LogInformation("Request ({RequestId}): {Date} {Name}", 
                _requestIdService.GetRequestId(),DateTime.Now, name);

            return await next();
        }
    }
}