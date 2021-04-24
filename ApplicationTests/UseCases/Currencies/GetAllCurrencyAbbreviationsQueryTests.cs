using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.UseCases.Currencies;
using ApplicationTests.Infrastructure;
using Xunit;

namespace ApplicationTests.UseCases.Currencies
{
    public class GetAllCurrencyAbbreviationsQueryTests
    {
        [Fact]
        public async Task Handle_ReturnsResponse()
        {
            var dictionary = BitcoinPriceServiceFactory.GetDictionary();
            var bitcoinPriceService = BitcoinPriceServiceFactory.Create();

            var handler = new GetAllCurrencyAbbreviationsQuery.Handler(bitcoinPriceService.Object);

            var res =
                await handler.Handle(new GetAllCurrencyAbbreviationsQuery(),
                    CancellationToken.None);

            Assert.Equal(dictionary.Count, res.Count);
            Assert.Equal(dictionary.OrderBy(x=> x.Key).First().Key,
                res.First());
        }
    }
}