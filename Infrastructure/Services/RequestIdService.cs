using System;
using Application.Interfaces.External;

namespace Infrastructure.Services
{
    public class RequestIdService: IRequestIdService
    {
        private readonly Guid _id;

        public RequestIdService()
        {
            _id = Guid.NewGuid();
        }
        public string GetRequestId() => _id.ToString();
    }
}