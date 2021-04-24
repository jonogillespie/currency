using System.Net;

namespace Presentation.Middleware.ErrorHandling.Responses
{
    public class InternalServerErrorResponse : BaseErrorResponse
    {
        public InternalServerErrorResponse(string requestId) : 
            base((int) HttpStatusCode.InternalServerError,
            "An unexpected error has occurred.",
            requestId)
        {
        }
    }
}