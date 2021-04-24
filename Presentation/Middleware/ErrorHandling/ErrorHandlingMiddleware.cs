using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.External;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Presentation.Middleware.ErrorHandling.Helpers;
using Presentation.Middleware.ErrorHandling.Responses;

namespace Presentation.Middleware.ErrorHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ValidationException e)
            {
                await WriteValidationErrorResponse(context, e);
            }
            catch (Exception e)
            {
                LogErrorIfNotHandled(context, e);
                await WriteInternalServerErrorResponse(context);
            }
        }

        private static void LogErrorIfNotHandled(HttpContext context, Exception e)
        {
            var logger = GetLogger(context);

            var requestIdService = GetRequestIdService(context);

            var handledExceptionSource = GetHandledExceptionSource(context);

            if (e.Source != handledExceptionSource)
            {
                logger.LogError("Error ({RequestId}): {Date} {@Exception}",
                    requestIdService.GetRequestId(),
                    DateTime.Now,
                    e);
            }
        }

        private static ILogger GetLogger(HttpContext context)
        {
            var logger =
                (ILogger) context.RequestServices.GetService(typeof(ILogger));
            return logger;
        }

        private static string GetHandledExceptionSource(HttpContext context)
        {
            var configuration =
                (IConfiguration) context.RequestServices.GetService(typeof(IConfiguration));

            var handledExceptionSource =
                configuration.GetValue<string>("HandledExceptionSource");
            return handledExceptionSource;
        }

        private static async Task WriteInternalServerErrorResponse(HttpContext context)
        {
            var requestIdService = GetRequestIdService(context);
            
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            
            var response =
                new InternalServerErrorResponse(requestIdService.GetRequestId());
            
            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task WriteValidationErrorResponse(HttpContext context, ValidationException e)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            
            var response = BuildBadRequestResponse(context,
                e);
            
            await context.Response.WriteAsJsonAsync(response);
        }

        private static BadRequestResponse BuildBadRequestResponse(HttpContext context,
            ValidationException e)
        {
            var requestIdService = GetRequestIdService(context);

            var dictionary = BuildBadRequestDictionary(e);

            var response = new BadRequestResponse("A bad request has occurred",
                requestIdService?.GetRequestId(),
                dictionary);
            
            return response;
        }

        private static Dictionary<string, string[]> BuildBadRequestDictionary(ValidationException ex)
        {
            var groupedErrors =
                ex.Errors.GroupBy(x => x.PropertyName);
            
            var dictionary = new Dictionary<string, string[]>();

            foreach (var groupedError in groupedErrors)
            {
                var errors = groupedError
                    .Select(x => x.ErrorMessage)
                    .ToArray();

                dictionary.Add(groupedError.Key.ToCamelCase(),
                    errors);
            }

            return dictionary;
        }

        private static IRequestIdService GetRequestIdService(HttpContext context)
        {
            var requestIdService =
                (IRequestIdService) context.RequestServices.GetService(typeof(IRequestIdService));
            return requestIdService;
        }
    }
}