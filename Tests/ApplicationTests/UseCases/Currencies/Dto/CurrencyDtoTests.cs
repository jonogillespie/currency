using System.Linq;
using Application.UseCases.Currencies.Dto;
using ApplicationTests.Infrastructure;
using Xunit;

namespace ApplicationTests.UseCases.Currencies.Dto
{
    public class CurrencyDtoTests
    {
        
        [Fact]
        public void Map_MappingsCorrect()
        {
            var dictionary = BitcoinPriceServiceFactory.GetDictionary();
            var (abbreviation, currencyResponse) = dictionary.First();

            var keyValuePair = dictionary.First();
            var mapper = AutoMapperFactory.Create();

            var res = mapper.Map<CurrencyDto>(keyValuePair);
            
            Assert.Equal(abbreviation, res.Abbreviation);
            Assert.Equal(currencyResponse._15m, res._15m);
            Assert.Equal(currencyResponse.Last, res.Last);
            Assert.Equal(currencyResponse.Buy, res.Buy);
            Assert.Equal(currencyResponse.Sell, res.Sell);
            Assert.Equal(currencyResponse.Symbol, res.Symbol);
        }
    }
}