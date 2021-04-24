using System;
using Infrastructure.Services;
using Xunit;

namespace InfrastructureTests.Services
{
    public class RequestIdServiceTests
    {
        [Fact]
        public void GetRequestId_ReturnsGuidString()
        {
            var service = new RequestIdService();
            
            var response = service.GetRequestId();
            var isGuid = Guid.TryParse(response, out _);
            
            Assert.True(isGuid);
        }
    }
}