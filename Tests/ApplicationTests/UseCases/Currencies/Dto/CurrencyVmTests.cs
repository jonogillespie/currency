using System.Linq;
using Application.UseCases.Currencies.Dto;
using ApplicationTests.Infrastructure;
using Xunit;
// ReSharper disable PossibleMultipleEnumeration

namespace ApplicationTests.UseCases.Currencies.Dto
{
    public class CurrencyVmTests
    {
        [Fact]
        public void Map_MappingsCorrect()
        {
            var dictionary = BitcoinPriceServiceFactory
                .GetDictionary()
                .OrderBy(x => x.Key)
                .AsEnumerable();
            var abbreviation = dictionary.First().Key;
            var currencyResponse = dictionary.First().Value;
            
            var mapper = AutoMapperFactory.Create();

            var res = mapper.Map<CurrencyVm>(dictionary);
            var first = res.Data.First();
            Assert.Equal(dictionary.AsEnumerable().Count(), res.Count);
            Assert.Equal(abbreviation, first.Abbreviation);
            Assert.Equal(currencyResponse._15m, first._15m);
            Assert.Equal(currencyResponse.Last, first.Last);
            Assert.Equal(currencyResponse.Buy, first.Buy);
            Assert.Equal(currencyResponse.Sell, first.Sell);
            Assert.Equal(currencyResponse.Symbol, first.Symbol);
        }
    }
}