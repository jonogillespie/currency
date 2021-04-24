using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.UseCases.Currencies;
using ApplicationTests.Infrastructure;
using Xunit;

namespace ApplicationTests.UseCases.Currencies
{
    public class GetAllCurrenciesQueryTests
    {
        [Fact]
        public async Task Handle_ReturnsResponse()
        {
            var bitcoinPriceService = BitcoinPriceServiceFactory.Create();
            var dictionary = BitcoinPriceServiceFactory.GetDictionary();
            var mapper = AutoMapperFactory.Create();
            var handler = new GetAllCurrenciesQuery.Handler(bitcoinPriceService.Object,
                mapper);

            var res = 
                await handler.Handle(new GetAllCurrenciesQuery(), CancellationToken.None);
            
            Assert.Equal(dictionary.Count, res.Count);
            Assert.Equal(dictionary.Count,res.Data.Count);
        }
        
        [Fact]
        public async Task Handle_ResponseOrderedByAbbreviation()
        {
            var bitcoinPriceService = BitcoinPriceServiceFactory.Create();
            var dictionary = BitcoinPriceServiceFactory.GetDictionary();
            var mapper = AutoMapperFactory.Create();
            var handler = new GetAllCurrenciesQuery.Handler(bitcoinPriceService.Object,
                mapper);

            var res = 
                await handler.Handle(new GetAllCurrenciesQuery(), CancellationToken.None);
            
            Assert.Equal(dictionary.OrderBy(x=> x.Key).First().Key, res.Data.First().Abbreviation);
        }
    }
}