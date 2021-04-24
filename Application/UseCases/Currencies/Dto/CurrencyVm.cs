using System.Collections.Generic;
using System.Linq;
using Application.Infrastructure.AutoMapper;
using Application.Interfaces.External;
using AutoMapper;

namespace Application.UseCases.Currencies.Dto
{
    public class CurrencyVm : IHaveCustomMapping
    {
        public List<CurrencyDto> Data { get; init; }
        public int Count { get; init; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<IOrderedEnumerable<KeyValuePair<string, CurrencyResponse>>, CurrencyVm>()
                .ForMember(x => x.Data,
                    opt => opt.MapFrom(x => x))
                .ForMember(x => x.Count,
                    opt => opt.MapFrom(x => x.AsEnumerable().Count()));
        }
    }
}