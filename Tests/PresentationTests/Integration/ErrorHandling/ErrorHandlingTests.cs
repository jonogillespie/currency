using System;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.External;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Presentation;
using Presentation.Middleware.ErrorHandling.Responses;
using Xunit;

namespace PresentationTests.Integration.ErrorHandling
{
    public class ErrorHandlingTests
    {
        [Fact]
        public async Task BadRequests_ReturnsCorrectResponse()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            var testClient = appFactory.CreateClient();

            var res =
                await testClient.GetAsync("v1/currencies/GBP/conversions/-123.12");

            var responseBody = await res.Content.ReadAsStringAsync();

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var response = JsonSerializer.Deserialize<BadRequestResponse>(responseBody,
                jsonSerializerOptions);

            Assert.Equal((int) HttpStatusCode.BadRequest,
                response?.StatusCode);
            Assert.Equal("A bad request has occurred",
                response?.Message);
            Assert.NotNull(response?.Errors);
        }
        
        [Fact]
        public async Task InternalServerError_ReturnsCorrectResponse()
        {
            var exceptionThrowingMockBitcoinService = new Mock<IBitcoinPriceService>();
            exceptionThrowingMockBitcoinService.Setup(x =>
                    x.ConvertToBitcoin(It.IsAny<BitcoinConversionInfo>(), CancellationToken.None))
                .Returns(() => throw new Exception());
            
            var appFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(IBitcoinPriceService));
                    services.AddSingleton(exceptionThrowingMockBitcoinService.Object);
                });
            });
            var testClient = appFactory.CreateClient();

            var res =
                await testClient.GetAsync("v1/currencies/GBP/conversions/123.12");

            var responseBody = await res.Content.ReadAsStringAsync();

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var response = JsonSerializer.Deserialize<InternalServerErrorResponse>(responseBody,
                jsonSerializerOptions);
            
            Assert.Equal(500, response?.StatusCode);
        }
    }
}