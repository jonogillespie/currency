using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.Infrastructure.AutoMapper;
using Application.Interfaces.External;
using AutoMapper;

namespace Application.UseCases.Currencies.Dto
{
    public class CurrencyDto: IHaveCustomMapping
    {
        public string Abbreviation { get; init; }

        [JsonPropertyName("15m")] 
        public double _15m { get; init; }
        public double Last { get; init; }
        public double Buy { get; init; }
        public double Sell { get; init; }
        public string Symbol { get; init; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<KeyValuePair<string, CurrencyResponse>, CurrencyDto>()
                .ForMember(x => x.Abbreviation,
                    opt =>
                        opt.MapFrom(x => x.Key))
                .ForMember(x => x._15m,
                    opt =>
                        opt.MapFrom(x => x.Value._15m))
                .ForMember(x => x.Last,
                    opt =>
                        opt.MapFrom(x => x.Value.Last))
                .ForMember(x => x.Buy,
                    opt =>
                        opt.MapFrom(x => x.Value.Buy))
                .ForMember(x => x.Sell,
                    opt =>
                        opt.MapFrom(x => x.Value.Sell))
                .ForMember(x => x.Symbol,
                    opt =>
                        opt.MapFrom(x => x.Value.Symbol));
        }
    }
}