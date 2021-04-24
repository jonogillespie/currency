using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.External;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Infrastructure.Mediator
{
    public class LogExceptionsBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly IRequestIdService _requestIdService;
        private readonly IConfiguration _configuration;

        public LogExceptionsBehaviour(ILogger<TRequest> logger,
            IRequestIdService requestIdService,
            IConfiguration configuration)
        {
            _logger = logger;
            _requestIdService = requestIdService;
            _configuration = configuration;
        }


        public async Task<TResponse> Handle(TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                var response = await next();
                return response;
            }
            catch (Exception ex)
            {
                var name = typeof(TRequest).Name;

                ex.Source = _configuration["HandledExceptionSource"];

                _logger.LogError("Error ({RequestId}): {Date} {Name} {@Exception}",
                    _requestIdService.GetRequestId(),
                    DateTime.Now,
                    name,
                    ex);

                throw;
            }
        }
    }
}