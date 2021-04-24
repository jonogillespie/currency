using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.External;
using Moq;

namespace ApplicationTests.Infrastructure
{
    public static class BitcoinPriceServiceFactory
    {
        public static Dictionary<string, CurrencyResponse> GetDictionary()
        {
           
            var dictionary = new Dictionary<string, CurrencyResponse>
            {
                
                {
                    "USD", new CurrencyResponse
                    {
                        _15m = 133.12,
                        Last = 162.1,
                        Buy = 4114.12,
                        Sell = 125.124,
                        Symbol = "$"
                    }
                },
                {
                    "GBP", new CurrencyResponse
                    {
                        _15m = 123.12,
                        Last = 142.1,
                        Buy = 4124.12,
                        Sell = 124.124,
                        Symbol = "£"
                    }
                }
            };

            return dictionary;
        }
        
        public static Mock<IBitcoinPriceService> Create()
        {
            var dictionary = GetDictionary();

            var bitcoinPriceService = new Mock<IBitcoinPriceService>();

            bitcoinPriceService.Setup(x => x.GetCurrentPrices(CancellationToken.None))
                .Returns(() => Task.Run(() => dictionary));

            return bitcoinPriceService;
        }
    }
}