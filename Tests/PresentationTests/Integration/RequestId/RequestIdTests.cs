using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Presentation;
using Xunit;

namespace PresentationTests.Integration.RequestId
{
    public class RequestIdTests
    {
        public RequestIdTests()
        {
            var appFactory = new WebApplicationFactory<Startup>();

            _testClient = appFactory.CreateClient();
        }

        private readonly HttpClient _testClient;

        [Fact]
        public async Task GetAllCurrencies_HasXRequestIdHeader()
        {
            var res = 
                await _testClient.GetAsync("v1/currencies");
            
            Assert.Contains("X-Request-ID", 
                res.Headers.Select(x=> x.Key));
        }
    }
}