// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Presentation.Middleware.ErrorHandling.Responses
{
    public abstract class BaseErrorResponse
    {
        public int StatusCode { get; }
        public string Message { get; }
        public string RequestId { get; }

        protected BaseErrorResponse(int statusCode,
            string message,
            string requestId)
        {
            StatusCode = statusCode;
            Message = message;
            RequestId = requestId;
        }
    }
}