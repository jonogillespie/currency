using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Presentation;
using Xunit;

namespace PresentationTests.Integration.Controllers.Currencies
{
    public class GetAllCurrencyAbbreviationsTest
    {
        public GetAllCurrencyAbbreviationsTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();

            _testClient = appFactory.CreateClient();
        }

        private readonly HttpClient _testClient;

        [Fact]
        public async Task GetAllCurrencyAbbreviations_Returns200Code()
        {
            var res = 
                await _testClient.GetAsync("v1/currencies/abbreviations");
            
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
        
        [Fact]
        public async Task GetAllCurrencyAbbreviations_GetFromCache_Returns200Code()
        {
            var res = 
                await _testClient.GetAsync("v1/currencies/abbreviations");
            
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);

            var resCached =
                await _testClient.GetAsync("v1/currencies/abbreviations");
            
            Assert.Equal(HttpStatusCode.OK,resCached.StatusCode);
        }
    }
}