using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.External;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using RichardSzalay.MockHttp;
using Xunit;

namespace InfrastructureTests.Services
{
    public class BitcoinPriceServiceTests
    {
        private readonly Mock<IConfiguration> _configuration;

        public BitcoinPriceServiceTests()
        {
            _configuration = new Mock<IConfiguration>();
            _configuration
                .SetupGet(x => 
                    x[It.Is<string>(s => s == "Blockchain:Url")])
                .Returns("https://blockchain.info");
        }

        [Fact]
        public async Task GetAllCurrencies_MappingCorrect()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://blockchain.info/ticker")
                .Respond("application/json",
                    "{\r\n  \"USD\" : {\"15m\" : 52374.71, \"last\" : 52374.71, \"buy\" : 52374.71, \"sell\" : 52374.71, \"symbol\" : \"$\"}\r\n}");

            var service = new BitcoinPriceService(_configuration.Object,
                mockHttp.ToHttpClient());

            var res =
                await service.GetCurrentPrices(CancellationToken.None);

            var dto = res.FirstOrDefault()
                .Value;

            Assert.Single(res);
            Assert.Equal("USD",
                res.FirstOrDefault()
                    .Key);
            Assert.Equal(52374.71,
                dto._15m);
            Assert.Equal(52374.71,
                dto.Last);
            Assert.Equal(52374.71,
                dto.Buy);
            Assert.Equal(52374.71,
                dto.Sell);
            Assert.Equal("$",
                dto.Symbol);
        }

        [Fact]
        public async Task GetAllCurrencies_UnsuccessfulHttpRequest_ThrowsBitcoinPriceServiceException()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://blockchain.info/ticker")
                .Respond(HttpStatusCode.InternalServerError);

            var service = new BitcoinPriceService(_configuration.Object,
                mockHttp.ToHttpClient());

            await Assert
                .ThrowsAsync<BitcoinPriceService.BitcoinPriceServiceException>(async () =>
                    await service.GetCurrentPrices(CancellationToken.None));
        }

        [Fact]
        public async Task ConvertToBitcoin_AbbreviationNull_ThrowsArgumentException()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://blockchain.info/tobtc")
                .Respond("application/json",
                    "200");

            var service = new BitcoinPriceService(_configuration.Object,
                mockHttp.ToHttpClient());

            var conversionInfo = new BitcoinConversionInfo
            {
                Abbreviation = null,
                Amount = 1323
            };

            await Assert
                .ThrowsAsync<ArgumentException>(async () =>
                    await service.ConvertToBitcoin(conversionInfo,
                        CancellationToken.None));
        }

        [Fact]
        public async Task ConvertToBitcoin_ReturnsCorrectResponse()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://blockchain.info/tobtc")
                .Respond("application/json",
                    "200");

            var service = new BitcoinPriceService(_configuration.Object,
                mockHttp.ToHttpClient());

            var conversionInfo = new BitcoinConversionInfo
            {
                Abbreviation = "GBP",
                Amount = 1323
            };

            var value =
                await service.ConvertToBitcoin(conversionInfo,
                    CancellationToken.None);

            Assert.Equal(200,
                value);
        }

        [Fact]
        public async Task ConvertToBitcoin_UnsuccessfulHttpRequest_ThrowsBitcoinPriceServiceException()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://blockchain.info/tobtc")
                .Respond(HttpStatusCode.InternalServerError);

            var service = new BitcoinPriceService(_configuration.Object,
                mockHttp.ToHttpClient());

            var conversionInfo = new BitcoinConversionInfo
            {
                Abbreviation = "GBP",
                Amount = 1323
            };

            await Assert
                .ThrowsAsync<BitcoinPriceService.BitcoinPriceServiceException>(async () =>
                    await service.ConvertToBitcoin(conversionInfo,
                        CancellationToken.None));
        }
    }
}