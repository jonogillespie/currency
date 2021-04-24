using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.External;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Infrastructure.Mediator
{
    public class RequestPerformanceBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    {

        private const int PerformanceThreshold = 500;
        private readonly ILogger<TRequest> _logger;
        private readonly IRequestIdService _requestIdService;
        private readonly Stopwatch _timer;
        
        public RequestPerformanceBehaviour(ILogger<TRequest> logger,
            IRequestIdService requestIdService)
        {
            _timer = new Stopwatch();
            _logger = logger;
            _requestIdService = requestIdService;
        }
        
        public async Task<TResponse> Handle(TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            if (_timer.ElapsedMilliseconds <= PerformanceThreshold)
                return response;
            
            var name = typeof(TRequest).Name;

            _logger.LogWarning(
                "Long Running Request ({RequestId}): {Date} {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                _requestIdService.GetRequestId(),
                DateTime.Now,
                name,
                _timer.ElapsedMilliseconds,
                request);

            return response;
        }
        
    }
}