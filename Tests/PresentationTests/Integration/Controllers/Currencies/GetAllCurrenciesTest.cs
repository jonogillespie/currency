using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Presentation;
using Xunit;

namespace PresentationTests.Integration.Controllers.Currencies
{
    public class GetAllCurrenciesTest
    {
        public GetAllCurrenciesTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();

            _testClient = appFactory.CreateClient();
        }

        private readonly HttpClient _testClient;

        [Fact]
        public async Task GetAllCurrencies_Returns200Code()
        {
            var res = 
                await _testClient.GetAsync("v1/currencies");
            
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
    }
}