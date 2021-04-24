using System.Collections.Generic;

namespace Presentation.Middleware.ErrorHandling.Responses
{
    public class BadRequestResponse: BaseErrorResponse
    {
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Dictionary<string, string[]> Errors { get; }

        public BadRequestResponse(string message, string requestId, Dictionary<string,  string[]> errors) 
            : base(400, message, requestId)
        {
            Errors = errors;
        }
    }
}