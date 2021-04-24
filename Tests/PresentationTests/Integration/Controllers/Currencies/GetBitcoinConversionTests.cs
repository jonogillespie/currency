using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Presentation;
using Xunit;

namespace PresentationTests.Integration.Controllers.Currencies
{
    public class GetBitcoinConversionTests
    {
        public GetBitcoinConversionTests()
        {
            var appFactory = new WebApplicationFactory<Startup>();

            _testClient = appFactory.CreateClient();
        }

        private readonly HttpClient _testClient;

        [Fact]
        public async Task GetBitcoinConversion_Returns200Code()
        {
            var res = 
                await _testClient.GetAsync("v1/currencies/GBP/conversions/123.12");
            
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
    }
}