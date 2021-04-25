using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.External;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class BitcoinPriceService : IBitcoinPriceService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public BitcoinPriceService(IConfiguration configuration,
            HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public class BitcoinPriceServiceException : Exception
        {
            public BitcoinPriceServiceException(string message) : base(message)
            {
            }
        }

        public async Task<Dictionary<string, CurrencyResponse>> GetCurrentPrices(CancellationToken cancellationToken) =>
            await GetResponseFromService<Dictionary<string, CurrencyResponse>>("ticker",
                cancellationToken);

        public async Task<double> ConvertToBitcoin(BitcoinConversionInfo bitcoinConversionInfo,
            CancellationToken cancellationToken)
        {
            if (bitcoinConversionInfo.Abbreviation == null)
            {
                throw new ArgumentException($"{nameof(bitcoinConversionInfo.Abbreviation)} is required for method" +
                                            $"{nameof(ConvertToBitcoin)}");
            }

            var path =
                // ReSharper disable once StringLiteralTypo
                $"tobtc?currency={bitcoinConversionInfo.Abbreviation}" +
                $"&value={bitcoinConversionInfo.Amount}";

            return
                await GetResponseFromService<double>(path,
                    cancellationToken);
        }

        private async Task<T> GetResponseFromService<T>(string path,
            CancellationToken cancellationToken)
        {
            var url = _configuration["Blockchain:Url"];

            var response =
                await _httpClient.GetAsync(
                    $"{url}/{path}",
                    cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new BitcoinPriceServiceException(
                    $"{nameof(GetCurrentPrices)} failed with http status code {response.StatusCode}");
            }

            return await Deserialize<T>(response,
                cancellationToken);
        }

        private static async Task<T> Deserialize<T>(HttpResponseMessage response,
            CancellationToken cancellationToken)
        {
            await using var contentStream =
                await response.Content.ReadAsStreamAsync(cancellationToken);

            var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                PropertyNameCaseInsensitive = true
            };

            return await JsonSerializer.DeserializeAsync<T>(contentStream,
                jsonSerializerOptions,
                cancellationToken);
        }
    }
}