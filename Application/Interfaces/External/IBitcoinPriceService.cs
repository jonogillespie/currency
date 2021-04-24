using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.Interfaces.External
{
    public class CurrencyResponse
    {
        [JsonPropertyName("15m")]
        public double _15m { get; init; }
        public double Last { get; init; }
        public double Buy { get; init; }
        public double Sell { get; init; }
        public string Symbol { get; init; }
    }

    public class BitcoinConversionInfo
    {
        public string Abbreviation { get; init; }
        public double Amount { get; init; }
    }
    
    public interface IBitcoinPriceService
    {
        Task<Dictionary<string, CurrencyResponse>> 
            GetCurrentPrices(CancellationToken cancellationToken);

        Task<double> 
            ConvertToBitcoin(BitcoinConversionInfo bitcoinConversionInfo, CancellationToken cancellationToken);
    }
}